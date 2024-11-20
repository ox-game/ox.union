using Akka.Util;
using OX.Bapps;
using OX.IO;
using OX.IO.Data.LevelDB;
using OX.IO.Wrappers;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Wallets;
using OX.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Org.BouncyCastle.Asn1.X509;
using OX.SmartContract;
using Akka.Actor;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OX.WebPort
{
    public delegate void BalanceChanged(Block block);
    public delegate void BlockIndexHandler(uint Index, Block block);
    public abstract class BaseBlockIndex : IDisposable
    {
        protected DB Db;

        public BaseBlockIndex(string path)
        {
            path = $"c://oxwebport/index_data/{path}";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            Db = DB.Open(path, new Options { CreateIfMissing = true });
        }
        public virtual void Dispose()
        {
            Db.Dispose();
        }
        public virtual void Rebuild()
        {
            WriteBatch batch = new WriteBatch();
            ReadOptions options = new ReadOptions { FillCache = false };
            using (Iterator it = Db.NewIterator(options))
            {
                for (it.SeekToFirst(); it.Valid(); it.Next())
                {
                    batch.Delete(it.Key());
                }
            }
            Db.Write(WriteOptions.Default, batch);
        }
        #region 
        public IEnumerable<KeyValuePair<K, V>> GetAll<K, V>(byte prefix, byte[] keys = default) where K : ISerializable, new() where V : ISerializable, new()
        {
            var builder = SliceBuilder.Begin(prefix);
            if (keys != default)
                builder = builder.Add(keys);
            return this.Db.Find(ReadOptions.Default, builder, (k, v) =>
            {
                var ks = k.ToArray();
                var length = ks.Length - sizeof(byte);
                ks = ks.TakeLast(length).ToArray();
                byte[] data = v.ToArray();
                return new KeyValuePair<K, V>(ks.AsSerializable<K>(), data.AsSerializable<V>());
            });
        }
        public IEnumerable<KeyValuePair<K, V>> GetAll<K, V>(byte prefix, ISerializable key) where K : ISerializable, new() where V : ISerializable, new()
        {
            return GetAll<K, V>(prefix, key.IsNotNull() ? key.ToArray() : default);
        }
        public T Get<T>(byte prefix, byte[] keys) where T : ISerializable, new()
        {
            Slice value;
            if (this.Db.TryGet(ReadOptions.Default, SliceBuilder.Begin(prefix).Add(keys), out value))
            {
                byte[] data = value.ToArray();
                return data.AsSerializable<T>();
            }
            else
                return default;
        }
        public T Get<T>(byte prefix, ISerializable key) where T : ISerializable, new()
        {
            return Get<T>(prefix, key.ToArray());
        }
        #endregion
    }
    public abstract class BaseSubBlockIndex : BaseBlockIndex
    {
        protected BlockIndex MasterBlockIndex { get; private set; }
        public BaseSubBlockIndex(BlockIndex blockIndex, string path) : base(path)
        {
            this.MasterBlockIndex = blockIndex;
        }
        public abstract bool CandRebuild { get; }
        public abstract void HandleBlock(Block block);
        public bool IsBizTransaction(Transaction tx, UInt160 slotScriptHash, out BizTransaction BT)
        {
            if (tx is BizTransaction bt)
            {
                if (bt.BizScriptHash == slotScriptHash)
                {
                    BT = bt;
                    return true;
                }
            }
            BT = default;
            return false;
        }
    }
    public partial class BlockIndex : BaseBlockIndex
    {
        static BlockIndex _instance;
        public static BlockIndex Instance
        {
            get
            {
                if (_instance.IsNull())
                {
                    _instance = new BlockIndex("Data_BlockIndex");
                }
                return _instance;
            }
        }
        public event BlockIndexHandler BlockIndexed;
        Dictionary<Type, BaseSubBlockIndex> SubIndexes = new Dictionary<Type, BaseSubBlockIndex>();
        private Thread thread;
        private readonly object SyncRoot = new object();
        private bool disposed = false;

        private uint LastIndex;
        public uint IndexHeight
        {
            get
            {
                lock (SyncRoot)
                {
                    return LastIndex;
                }
            }
        }



        internal Dictionary<CoinReference, uint> UnconfirmCoins = new Dictionary<CoinReference, uint>();

        internal Dictionary<UInt160, LockAssetTransactionMix> Accounts = new Dictionary<UInt160, LockAssetTransactionMix>();
        internal Dictionary<UInt160, Dictionary<CoinReference, OutputMix>> Account_UTXO { get; set; } = new Dictionary<UInt160, Dictionary<CoinReference, OutputMix>>();
        internal Dictionary<UInt160, Dictionary<CoinReference, LockOXS>> Account_UTXO_OXS { get; set; } = new Dictionary<UInt160, Dictionary<CoinReference, LockOXS>>();
        BlockIndex(string path, bool autoStart = true) : base(path)
        {
            InitSubIndexes();
            LastIndex = (uint)GetLatestBlockIndex();
            if (LastIndex == 0) Rebuild();

            this.Accounts = new Dictionary<UInt160, LockAssetTransactionMix>(this.GetAll<UInt160, LockAssetTransactionMix>(WebPortPersistencePrefixes.Account_Mix));
            foreach (var p in this.GetAll<CoinReference, OutputMix>(WebPortPersistencePrefixes.Account_UTXO))
            {
                if (!this.Account_UTXO.TryGetValue(p.Value.Holder, out var dic))
                {
                    dic = new Dictionary<CoinReference, OutputMix>();
                    this.Account_UTXO[p.Value.Holder] = dic;
                }
                dic[p.Key] = p.Value;
            }

            foreach (var p in this.GetAll<CoinReference, LockOXS>(WebPortPersistencePrefixes.Account_UTXO_OXS))
            {
                if (!this.Account_UTXO_OXS.TryGetValue(p.Value.Holder, out var dic))
                {
                    dic = new Dictionary<CoinReference, LockOXS>();
                    this.Account_UTXO_OXS[p.Value.Holder] = dic;
                }
                dic[p.Key] = p.Value;
            }


            if (autoStart)
            {
                StartIndex();
            }
        }
        void InitSubIndexes()
        {
            SubIndexes[typeof(CasinoBlockIndex)] = new CasinoBlockIndex(this);
            SubIndexes[typeof(MiningBlockIndex)] = new MiningBlockIndex(this);
            SubIndexes[typeof(MarkBlockIndex)] = new MarkBlockIndex(this);
        }
        public T GetSubBlockIndex<T>() where T : BaseSubBlockIndex
        {
            if (SubIndexes.TryGetValue(typeof(T), out var index))
            {
                return index as T;
            }
            return default;
        }
        public void StartIndex()
        {
            thread = new Thread(ProcessBlocks)
            {
                IsBackground = true,
                Name = $"{nameof(BlockIndex)}.{nameof(ProcessBlocks)}"
            };
            thread.Start();
        }
        public override void Dispose()
        {
            disposed = true;
            thread.Join();
            base.Dispose();
        }
        public override void Rebuild()
        {
            base.Rebuild();
            this.LastIndex = 0;
            this.Accounts.Clear();
            this.Account_UTXO.Clear();
            this.Account_UTXO_OXS.Clear();
            foreach (var subIndex in SubIndexes)
            {
                if (subIndex.Value.CandRebuild)
                    subIndex.Value.Rebuild();
            }
        }
        private void ProcessBlocks()
        {
            //Thread.Sleep(10000);
            while (!disposed)
            {
                while (!disposed)
                {
                    lock (SyncRoot)
                    {
                        var block = Blockchain.Singleton.Store.GetBlock(this.LastIndex + 1);
                        if (block == null) break;
                        WriteBatch batch = new WriteBatch();
                        ReadOptions options = ReadOptions.Default;
                        if (HandleBlock(batch, block))
                        {
                            //SecureHelper.BalanceChangedLastIndex = block.Index;
                        }
                        var newIndex = this.LastIndex + 1;
                        BlockIndexed?.Invoke(newIndex, block);
                        UpdateLatestBlockIndex(batch, newIndex);
                        Db.Write(WriteOptions.Default, batch);
                        foreach (var subIndex in SubIndexes)
                        {
                            subIndex.Value.HandleBlock(block);
                        }
                    }
                }
                for (int i = 0; i < 20 && !disposed; i++)
                    Thread.Sleep(100);
            }
        }
        private bool HandleBlock(WriteBatch batch, Block block)
        {
            for (ushort i = 0; i < block.Transactions.Length; i++)
            {
                var tx = block.Transactions[i];
                UInt160 FromSH = UInt160.Zero;
                var from = tx.GetBestOriginAddress(out string ethAddress);
                if (from.IsNotNull())
                    FromSH = from;
                if (tx is EthereumMapTransaction emt)
                {
                    //if (Save_EthereumMapTransaction(batch, block, emt, i)) balanceChanged = true;
                }
                else if (tx is LockAssetTransaction lat)
                {
                    SaveAccount_LockAssetTransaction(batch, block, lat, i);
                }
                else if (tx is ClaimTransaction clmTx)
                {
                    foreach (var kp in clmTx.Claims)
                    {
                        foreach (var dic in this.Account_UTXO_OXS)
                        {
                            dic.Value.Remove(kp);
                        }
                        batch.Delete(SliceBuilder.Begin(WebPortPersistencePrefixes.Account_UTXO_OXS).Add(kp));
                    }
                }
                SaveAccount_NoneLockAssetTransaction(batch, block, tx, i);

                var fromHolder = FromSH;
                if (this.Accounts.TryGetValue(FromSH, out var tm))
                {
                    fromHolder = tm.Holder;
                }

                //txo
                foreach (KeyValuePair<CoinReference, TransactionOutput> kp in tx.References)
                {
                    this.UnconfirmCoins.Remove(kp.Key);

                    foreach (var dic in this.Account_UTXO)
                    {
                        if (dic.Value.ContainsKey(kp.Key))
                        {
                            if (kp.Value.AssetId == Blockchain.OXS)
                            {
                                if (this.Account_UTXO_OXS.TryGetValue(dic.Key, out var dicOXS))
                                {
                                    if (dicOXS.TryGetValue(kp.Key, out var lockOXS))
                                    {
                                        lockOXS.Flag = LockOXSFlag.Spend;
                                        lockOXS.SpendIndex = block.Index;
                                        batch.Put(SliceBuilder.Begin(WebPortPersistencePrefixes.Account_UTXO_OXS).Add(kp.Key), SliceBuilder.Begin().Add(lockOXS));
                                    }
                                }
                            }
                            batch.Delete(SliceBuilder.Begin(WebPortPersistencePrefixes.Account_UTXO).Add(kp.Key));
                            dic.Value.Remove(kp.Key);
                        }
                    }
                }
                //utxo
                if (tx.Outputs.IsNotNullAndEmpty())
                {
                    for (ushort k = 0; k < tx.Outputs.Length; k++)
                    {
                        var output = tx.Outputs[k];
                        if (this.Accounts.TryGetValue(output.ScriptHash, out var latm))
                        {
                            CoinReference cr = new CoinReference { PrevHash = tx.Hash, PrevIndex = k };
                            OutputMix mom = default;
                            if (latm.LockAssetTransaction.IsNotNull())
                            {
                                mom = new OutputMix { Holder = latm.Holder, Input = cr, Output = output, IsTimeLock = latm.LockAssetTransaction.IsTimeLock, LockExpirationIndex = latm.LockAssetTransaction.LockExpiration, IsLockCoin = true, TimeStamp = block.Timestamp, FromHolder=fromHolder };
                                if (output.AssetId == Blockchain.OXS)
                                {
                                    var lockOXS = new LockOXS { Holder = latm.Holder, Output = output, Tx = latm.LockAssetTransaction, IsLockAssetTx = true, Flag = LockOXSFlag.Unspend, Index = block.Index, SpendIndex = 0 };
                                    batch.Put(SliceBuilder.Begin(WebPortPersistencePrefixes.Account_UTXO_OXS).Add(cr), SliceBuilder.Begin().Add(lockOXS));
                                    if (!this.Account_UTXO_OXS.TryGetValue(latm.Holder, out var dicOXS))
                                    {
                                        dicOXS = new Dictionary<CoinReference, LockOXS>();
                                        this.Account_UTXO_OXS[latm.Holder] = dicOXS;
                                    }
                                    dicOXS[cr] = lockOXS;
                                }
                            }
                            else
                            {
                                mom = new OutputMix { Holder = latm.Holder, Input = cr, Output = output, IsTimeLock = false, LockExpirationIndex = 0, IsLockCoin = false, TimeStamp = block.Timestamp, FromHolder= fromHolder };
                                if (output.AssetId == Blockchain.OXS)
                                {
                                    var lockOXS = new LockOXS { Holder = latm.Holder, Output = output, Tx = default, IsLockAssetTx = false, Flag = LockOXSFlag.Unspend, Index = block.Index, SpendIndex = 0 };
                                    batch.Put(SliceBuilder.Begin(WebPortPersistencePrefixes.Account_UTXO_OXS).Add(cr), SliceBuilder.Begin().Add(lockOXS));
                                    if (!this.Account_UTXO_OXS.TryGetValue(latm.Holder, out var dicOXS))
                                    {
                                        dicOXS = new Dictionary<CoinReference, LockOXS>();
                                        this.Account_UTXO_OXS[latm.Holder] = dicOXS;
                                    }
                                    dicOXS[cr] = lockOXS;
                                }
                            }
                            batch.Put(SliceBuilder.Begin(WebPortPersistencePrefixes.Account_UTXO).Add(cr), SliceBuilder.Begin().Add(mom));
                            if (!this.Account_UTXO.TryGetValue(latm.Holder, out var dic))
                            {
                                dic = new Dictionary<CoinReference, OutputMix>();
                                this.Account_UTXO[latm.Holder] = dic;
                            }
                            dic[cr] = mom;
                        }
                    }
                }
                List<UInt160> needRemoveAddress = new List<UInt160>();
                foreach (var dic in this.Account_UTXO)
                {
                    if (!dic.Value.Any())
                    {
                        needRemoveAddress.Add(dic.Key);
                    }
                }
                foreach (var sh in needRemoveAddress)
                {
                    this.Account_UTXO.Remove(sh);
                }
            }
            List<CoinReference> needRemoveCoins = new List<CoinReference>();
            foreach (var p in this.UnconfirmCoins)
            {
                if (p.Value < block.Index)
                {
                    needRemoveCoins.Add(p.Key);
                }
            }
            foreach (var k in needRemoveCoins)
                this.UnconfirmCoins.Remove(k);

            return true;
        }
        #region
        public UInt32Wrapper GetLatestBlockIndex()
        {
            var w = this.Get<UInt32Wrapper>(WebPortPersistencePrefixes.Index_Position, new byte[] { 0x00 });
            if (w.IsNull()) w = (UInt32Wrapper)0;
            return w;
        }
        public void UpdateLatestBlockIndex(WriteBatch batch, uint lastIndex)
        {
            batch.Put(SliceBuilder.Begin(WebPortPersistencePrefixes.Index_Position).Add((byte)0), SliceBuilder.Begin().Add(lastIndex));
            this.LastIndex = lastIndex;
        }
        #endregion
        #region
        public void SaveAccount_LockAssetTransaction(WriteBatch batch, Block block, LockAssetTransaction lat, ushort blockN)
        {
            if (lat.IsNotNull() && lat.LockContract.Equals(Blockchain.LockAssetContractScriptHash))
            {
                var sh = lat.GetContract().ScriptHash;
                if (!this.Accounts.ContainsKey(sh))
                {
                    var holder = Contract.CreateSignatureRedeemScript(lat.Recipient).ToScriptHash();
                    LockAssetTransactionMix latm = new LockAssetTransactionMix { Holder = holder, LockAssetTransaction = lat, LastIndex = block.Index };
                    batch.Put(SliceBuilder.Begin(WebPortPersistencePrefixes.Account_Mix).Add(sh), SliceBuilder.Begin().Add(latm));
                    this.Accounts[sh] = latm;
                }
            }
        }
        public void SaveAccount_NoneLockAssetTransaction(WriteBatch batch, Block block, Transaction tx, ushort blockN)
        {
            if (tx.IsNotNull())
            {
                foreach (var output in tx.Outputs)
                {
                    var address = output.ScriptHash;
                    if (!this.Accounts.ContainsKey(address))
                    {
                        LockAssetTransactionMix latm = new LockAssetTransactionMix { Holder = address, LockAssetTransaction = default, LastIndex = 0 };
                        batch.Put(SliceBuilder.Begin(WebPortPersistencePrefixes.Account_Mix).Add(address), SliceBuilder.Begin().Add(latm));
                        this.Accounts[address] = latm;
                    }
                }
            }
        }
        #endregion
        #region
        public AssetBalanceState GetAssetUtxoStates(UInt160 holder, UInt256 assetId)
        {
            AssetBalanceState balanceState = new AssetBalanceState
            {
                AssetId = assetId,
            };
            var assetState = Blockchain.Singleton.CurrentSnapshot.Assets.TryGet(assetId);
            if (assetState.IsNotNull())
            {
                balanceState.AssetName = assetState.GetName();
            }
            if (this.Account_UTXO.IsNotNullAndEmpty())
            {
                if (this.Account_UTXO.TryGetValue(holder, out var dic))
                {
                    List<OutputMix> oms = new List<OutputMix>();
                    foreach (var d in dic.Where(m => m.Value.Output.AssetId == assetId))
                    {
                        if (!BlockIndex.Instance.UnconfirmCoins.ContainsKey(d.Key))
                        {
                            balanceState.TotalBalance += d.Value.Output.Value;
                            var ok = d.Value.IsTimeLock ? d.Value.LockExpirationIndex < System.DateTime.Now.ToTimestamp() : d.Value.LockExpirationIndex < Blockchain.Singleton.HeaderHeight;
                            if (ok)
                            {
                                balanceState.AvailableBalance += d.Value.Output.Value;
                                oms.Add(d.Value);
                            }
                            if (d.Value.LockExpirationIndex == 0)
                            {
                                balanceState.MasterBalance += d.Value.Output.Value;
                            }

                        }
                    }
                    balanceState.OMS = oms.ToArray();
                }
            }
            return balanceState;
        }
        public Dictionary<UInt256, AssetBalanceState> GetBalanceStates(UInt160 holder)
        {
            Dictionary<UInt256, AssetBalanceState> MasterBalanceStates = new Dictionary<UInt256, AssetBalanceState>();
            if (this.Account_UTXO.IsNotNullAndEmpty())
            {
                if (this.Account_UTXO.TryGetValue(holder, out var dic))
                {
                    foreach (var g in dic.Values.GroupBy(m => m.Output.AssetId))
                    {
                        AssetBalanceState balanceState = new AssetBalanceState
                        {
                            AssetId = g.Key,
                        };
                        var assetState = Blockchain.Singleton.CurrentSnapshot.Assets.TryGet(g.Key);
                        if (assetState.IsNotNull())
                        {
                            balanceState.AssetName = assetState.GetName();
                        }
                        balanceState.TotalBalance = g.Sum(m => m.Output.Value);
                        var gs = g.Where(m => m.IsTimeLock ? m.LockExpirationIndex < System.DateTime.Now.ToTimestamp() : m.LockExpirationIndex < Blockchain.Singleton.HeaderHeight);
                        balanceState.AvailableBalance = gs.IsNotNullAndEmpty() ? gs.Sum(m => m.Output.Value) : Fixed8.Zero;
                        var ms = g.Where(m => m.LockExpirationIndex == 0);
                        balanceState.MasterBalance = ms.IsNotNullAndEmpty() ? ms.Sum(m => m.Output.Value) : Fixed8.Zero;
                        balanceState.OMS = g.ToArray();
                        MasterBalanceStates[g.Key] = balanceState;
                    }
                }
            }
            return MasterBalanceStates;
        }
        public Fixed8 GetAvailableBalance(UInt160 holder, UInt256 assetId)
        {
            var balance = Fixed8.Zero;
            if (GetBalanceStates(holder).TryGetValue(assetId, out var balanceState))
            {
                balance = balanceState.AvailableBalance;
            }
            return balance;
        }
        #endregion
    }
}
