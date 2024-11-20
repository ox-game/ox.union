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
using OX.Tablet.Models;
using Org.BouncyCastle.Asn1.X509;

namespace OX.Tablet
{
    public delegate void BalanceChanged(Block block);
    public delegate void BlockIndexHandler(uint Index, Block block);
    public abstract class BaseBlockIndex : IDisposable
    {
        protected DB Db;
        public OXAccount OXAccount { get; protected set; }
        public EthAccount EthAccount { get; protected set; }
        public BaseBlockIndex(OXAccount oxAccount, EthAccount ethAccount, string path)
        {
            this.OXAccount = oxAccount;
            this.EthAccount = ethAccount;
            //path = Path.GetFullPath(path);
            path = $"c://oxtablet/index_data/{path}";
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
        public BaseSubBlockIndex(BlockIndex blockIndex, OXAccount oxAccount, EthAccount ethAccount, string path) : base(oxAccount, ethAccount, path)
        {
            this.MasterBlockIndex = blockIndex;
        }
        public abstract bool CanRebuild { get; }
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
        public static event BlockIndexHandler BlockIndexed;
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
        internal Dictionary<UInt160, EthereumMapTransactionMerge> EthMaps { get; set; } = new Dictionary<UInt160, EthereumMapTransactionMerge>();
        internal Dictionary<CoinReference, EthOutputMerge> EthMapUTXOs { get; set; } = new Dictionary<CoinReference, EthOutputMerge>();
        internal Dictionary<UInt160, LockAssetTransactionMerge> MasterMaps { get; set; } = new Dictionary<UInt160, LockAssetTransactionMerge>();
        internal Dictionary<CoinReference, MasterOutputMerge> MasterUTXOs { get; set; } = new Dictionary<CoinReference, MasterOutputMerge>();


        internal Dictionary<CoinReference, uint> UnconfirmCoins = new Dictionary<CoinReference, uint>();
        internal Dictionary<UInt32Wrapper, BlockAssetRecord> BlockAssetRecords = new Dictionary<UInt32Wrapper, BlockAssetRecord>();
        public BlockIndex(OXAccount oxAccount, EthAccount ethAccount, string path, bool autoStart = true) : base(oxAccount, ethAccount, path)
        {
            InitSubIndexes(oxAccount, ethAccount);
            LastIndex = (uint)GetLatestBlockIndex();
            if (LastIndex == 0) Rebuild();
            this.EthMaps = new Dictionary<UInt160, EthereumMapTransactionMerge>(this.GetAll<UInt160, EthereumMapTransactionMerge>(TabletPersistencePrefixes.Emt_Tx));
            this.EthMapUTXOs = new Dictionary<CoinReference, EthOutputMerge>(this.GetAll<CoinReference, EthOutputMerge>(TabletPersistencePrefixes.Emt_UTXO));
            this.EthMaps[this.EthAccount.MapAddress] = new EthereumMapTransactionMerge { EthereumMapTransaction = new EthereumMapTransaction { EthereumAddress = this.EthAccount.EthAddress, LockExpirationIndex = 0 }, LastIndex = 0 };

            this.MasterMaps = new Dictionary<UInt160, LockAssetTransactionMerge>(this.GetAll<UInt160, LockAssetTransactionMerge>(TabletPersistencePrefixes.Master_Lock_Tx));
            this.MasterUTXOs = new Dictionary<CoinReference, MasterOutputMerge>(this.GetAll<CoinReference, MasterOutputMerge>(TabletPersistencePrefixes.Master_UTXO));

            this.BlockAssetRecords = new Dictionary<UInt32Wrapper, BlockAssetRecord>(this.GetAll<UInt32Wrapper, BlockAssetRecord>(TabletPersistencePrefixes.Index_BlockAssetRecord));
            if (autoStart)
            {
                StartIndex();
            }
        }
        void InitSubIndexes(OXAccount oxAccount, EthAccount ethAccount)
        {
            SubIndexes[typeof(BMSBlockIndex)] = new BMSBlockIndex(this, oxAccount, ethAccount);
            SubIndexes[typeof(CasinoBlockIndex)] = new CasinoBlockIndex(this, oxAccount, ethAccount);
            SubIndexes[typeof(MiningBlockIndex)] = new MiningBlockIndex(this, oxAccount, ethAccount);
            SubIndexes[typeof(DirectSaleBlockIndex)] = new DirectSaleBlockIndex(this, oxAccount, ethAccount);

            SubIndexes[typeof(AgentBlockIndex)] = new AgentBlockIndex(this, oxAccount, ethAccount);
            SubIndexes[typeof(FlashBlockIndex)] = new FlashBlockIndex(this, oxAccount, ethAccount);
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
            this.EthMaps.Clear();
            this.EthMapUTXOs.Clear();
            this.MasterMaps.Clear();
            this.MasterUTXOs.Clear();
            this.BlockAssetRecords.Clear();
            foreach (var subIndex in SubIndexes)
            {
                if (subIndex.Value.CanRebuild)
                    subIndex.Value.Rebuild();
            }
            this.EthMaps[this.EthAccount.MapAddress] = new EthereumMapTransactionMerge { EthereumMapTransaction = new EthereumMapTransaction { EthereumAddress = this.EthAccount.EthAddress, LockExpirationIndex = 0 }, LastIndex = 0 };
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
                            SecureHelper.BalanceChangedLastIndex = block.Index;
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
            bool balanceChanged = false;
            BlockAssetRecord bar = new BlockAssetRecord { Index = block.Index, TimeStamp = block.Timestamp };
            for (ushort i = 0; i < block.Transactions.Length; i++)
            {
                var tx = block.Transactions[i];
                if (tx is EthereumMapTransaction emt)
                {
                    if (Save_EthereumMapTransaction(batch, block, emt, i)) balanceChanged = true;
                }
                else if (tx is LockAssetTransaction lat)
                {
                    if (Save_LockAssetTransaction(batch, block, lat, i)) balanceChanged = true;
                }
                else if (tx is ClaimTransaction clmTx)
                {
                    foreach (var kp in clmTx.Claims)
                    {
                        batch.Delete(SliceBuilder.Begin(TabletPersistencePrefixes.OXS_Claim_OXC).Add(kp));
                    }
                }
                //txo
                foreach (KeyValuePair<CoinReference, TransactionOutput> kp in tx.References)
                {
                    this.UnconfirmCoins.Remove(kp.Key);
                    if (kp.Value.AssetId == Blockchain.OXS)
                    {
                        var lockOXS = this.Get<LockOXS>(TabletPersistencePrefixes.OXS_Claim_OXC, kp.Key);
                        if (lockOXS.IsNotNull())
                        {
                            lockOXS.Flag = LockOXSFlag.Spend;
                            lockOXS.SpendIndex = block.Index;
                            batch.Put(SliceBuilder.Begin(TabletPersistencePrefixes.OXS_Claim_OXC).Add(kp.Key), SliceBuilder.Begin().Add(lockOXS));
                        }
                    }
                    if (this.EthMapUTXOs.Remove(kp.Key))
                    {
                        batch.Delete(SliceBuilder.Begin(TabletPersistencePrefixes.Emt_UTXO).Add(kp.Key));
                        balanceChanged = true;
                    }
                    if (this.MasterUTXOs.Remove(kp.Key))
                    {
                        batch.Delete(SliceBuilder.Begin(TabletPersistencePrefixes.Master_UTXO).Add(kp.Key));
                        balanceChanged = true;
                    }
                    if (this.OXAccount.ScriptHash == kp.Value.ScriptHash || MasterMaps.ContainsKey(kp.Value.ScriptHash) || EthMaps.ContainsKey(kp.Value.ScriptHash))
                    {
                        Fixed8 amt = -kp.Value.Value;
                        if (bar.Balances.TryGetValue(kp.Value.AssetId, out Fixed8 rmd))
                        {
                            amt += rmd;
                        }
                        bar.Balances[kp.Value.AssetId] = amt;
                    }
                }
                //utxo
                if (tx.Outputs.IsNotNullAndEmpty())
                {
                    for (ushort k = 0; k < tx.Outputs.Length; k++)
                    {
                        var output = tx.Outputs[k];
                        //grab oxs records
                        if (output.AssetId == Blockchain.OXS)
                        {
                            var coin = new CoinReference { PrevHash = tx.Hash, PrevIndex = k };
                            if (output.ScriptHash == this.OXAccount.ScriptHash)
                            {
                                batch.Put(SliceBuilder.Begin(TabletPersistencePrefixes.OXS_Claim_OXC).Add(coin), SliceBuilder.Begin().Add(new LockOXS { Holder = this.OXAccount.ScriptHash, Output = output, Tx = new LockAssetTransaction() { Recipient = this.OXAccount.Key.PublicKey, Witnesses = new Witness[0] }, IsLockAssetTx = false, Flag = LockOXSFlag.Unspend, Index = block.Index, SpendIndex = 0 }));
                            }
                            if (this.MasterMaps.TryGetValue(output.ScriptHash, out var latMerge))
                            {
                                batch.Put(SliceBuilder.Begin(TabletPersistencePrefixes.OXS_Claim_OXC).Add(coin), SliceBuilder.Begin().Add(new LockOXS { Holder = this.OXAccount.ScriptHash, Output = output, Tx = latMerge.LockAssetTransaction, IsLockAssetTx = true, Flag = LockOXSFlag.Unspend, Index = block.Index, SpendIndex = 0 }));
                            }
                        }
                        if (this.EthMaps.TryGetValue(output.ScriptHash, out EthereumMapTransactionMerge emtm))
                        {
                            CoinReference cr = new CoinReference { PrevHash = tx.Hash, PrevIndex = k };
                            EthOutputMerge eom = new EthOutputMerge { Output = output, LockExpirationIndex = emtm.EthereumMapTransaction.LockExpirationIndex, EthAddress = emtm.EthereumMapTransaction.EthereumAddress };
                            batch.Put(SliceBuilder.Begin(TabletPersistencePrefixes.Emt_UTXO).Add(cr), SliceBuilder.Begin().Add(eom));
                            this.EthMapUTXOs[cr] = eom;
                            Fixed8 amt = output.Value;
                            if (bar.Balances.TryGetValue(output.AssetId, out Fixed8 rmd))
                            {
                                amt += rmd;
                            }
                            bar.Balances[output.AssetId] = amt;
                            balanceChanged = true;
                        }
                        if (output.ScriptHash == this.OXAccount.ScriptHash)
                        {
                            CoinReference cr = new CoinReference { PrevHash = tx.Hash, PrevIndex = k };
                            MasterOutputMerge mom = new MasterOutputMerge { Output = output, IsTimeLock = false, LockExpirationIndex = 0, IsLockCoin = false };
                            batch.Put(SliceBuilder.Begin(TabletPersistencePrefixes.Master_UTXO).Add(cr), SliceBuilder.Begin().Add(mom));
                            this.MasterUTXOs[cr] = mom;
                            Fixed8 amt = output.Value;
                            if (bar.Balances.TryGetValue(output.AssetId, out Fixed8 rmd))
                            {
                                amt += rmd;
                            }
                            bar.Balances[output.AssetId] = amt;
                            balanceChanged = true;
                        }
                        if (this.MasterMaps.TryGetValue(output.ScriptHash, out LockAssetTransactionMerge lat))
                        {
                            CoinReference cr = new CoinReference { PrevHash = tx.Hash, PrevIndex = k };
                            MasterOutputMerge mom = new MasterOutputMerge { Output = output, IsTimeLock = lat.LockAssetTransaction.IsTimeLock, LockExpirationIndex = lat.LockAssetTransaction.LockExpiration, IsLockCoin = true };
                            batch.Put(SliceBuilder.Begin(TabletPersistencePrefixes.Master_UTXO).Add(cr), SliceBuilder.Begin().Add(mom));
                            this.MasterUTXOs[cr] = mom;
                            Fixed8 amt = output.Value;
                            if (bar.Balances.TryGetValue(output.AssetId, out Fixed8 rmd))
                            {
                                amt += rmd;
                            }
                            bar.Balances[output.AssetId] = amt;
                            balanceChanged = true;
                        }
                    }
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
            if (bar.Balances.IsNotNullAndEmpty())
            {
                var bs = bar.Balances.Where(m => m.Value != Fixed8.Zero);
                if (bs.IsNotNullAndEmpty())
                {
                    Save_BlockAssetRecord(batch, bar);
                }
            }
            return balanceChanged;
        }
        #region
        public UInt32Wrapper GetLatestBlockIndex()
        {
            var w = this.Get<UInt32Wrapper>(TabletPersistencePrefixes.Index_Position, this.EthAccount.MapAddress);
            if (w.IsNull()) w = (UInt32Wrapper)0;
            return w;
        }
        public void UpdateLatestBlockIndex(WriteBatch batch, uint lastIndex)
        {
            batch.Put(SliceBuilder.Begin(TabletPersistencePrefixes.Index_Position).Add(this.EthAccount.MapAddress), SliceBuilder.Begin().Add(lastIndex));
            this.LastIndex = lastIndex;
        }
        #endregion

        #region
        public bool Save_EthereumMapTransaction(WriteBatch batch, Block block, EthereumMapTransaction emt, ushort blockN)
        {
            if (emt.IsNotNull() && emt.EthMapContract.Equals(Blockchain.EthereumMapContractScriptHash) && emt.EthereumAddress.ToLower() == this.EthAccount.EthAddress.ToLower())
            {
                var sh = emt.GetContract().ScriptHash;
                EthereumMapTransactionMerge emtm = new EthereumMapTransactionMerge { EthereumMapTransaction = emt, LastIndex = block.Index };
                batch.Put(SliceBuilder.Begin(TabletPersistencePrefixes.Emt_Tx).Add(sh), SliceBuilder.Begin().Add(emtm));
                this.EthMaps[sh] = emtm;
                return true;
            }
            return false;
        }
        public bool Save_LockAssetTransaction(WriteBatch batch, Block block, LockAssetTransaction lat, ushort blockN)
        {
            if (lat.IsNotNull() && lat.LockContract.Equals(Blockchain.LockAssetContractScriptHash) && lat.Recipient.Equals(this.OXAccount.Key.PublicKey))
            {
                var sh = lat.GetContract().ScriptHash;
                LockAssetTransactionMerge latm = new LockAssetTransactionMerge { LockAssetTransaction = lat, LastIndex = block.Index };
                batch.Put(SliceBuilder.Begin(TabletPersistencePrefixes.Master_Lock_Tx).Add(sh), SliceBuilder.Begin().Add(latm));
                this.MasterMaps[sh] = latm;
                return true;
            }
            return false;
        }
        public bool Save_BlockAssetRecord(WriteBatch batch, BlockAssetRecord bar)
        {
            if (bar.IsNotNull())
            {
                batch.Put(SliceBuilder.Begin(TabletPersistencePrefixes.Index_BlockAssetRecord).Add(bar.Index), SliceBuilder.Begin().Add(bar));
                this.BlockAssetRecords[bar.Index] = bar;
                return true;
            }
            return false;
        }
        #endregion
        #region 
        public MasterUTXO[] GetMasterUtxos(UInt256 assetId)
        {
            List<MasterUTXO> list = new List<MasterUTXO>();
            if (this.MasterUTXOs.IsNotNullAndEmpty())
            {
                var H = Blockchain.Singleton.Height;
                var ws = this.MasterUTXOs.Where(m => m.Value.Output.AssetId == assetId && ((m.Value.IsTimeLock && DateTime.Now.ToTimestamp() > m.Value.LockExpirationIndex) || (!m.Value.IsTimeLock && Blockchain.Singleton.Height > m.Value.LockExpirationIndex)) && !this.UnconfirmCoins.ContainsKey(m.Key));
                if (ws.IsNotNullAndEmpty())
                {
                    foreach (var pair in ws)
                    {
                        list.Add(new MasterUTXO
                        {
                            Address = pair.Value.Output.ScriptHash,
                            Value = pair.Value.Output.Value.GetInternalValue(),
                            TxId = pair.Key.PrevHash,
                            N = pair.Key.PrevIndex,
                            IsTimeLock = pair.Value.IsTimeLock,
                            LockExpirationIndex = pair.Value.LockExpirationIndex,
                            IsLockCoin = pair.Value.IsLockCoin
                        });
                    }
                }
            }
            return list.ToArray();
        }
        public EthMapUTXO[] GetExchangeUtxos(UInt256 assetId)
        {
            List<EthMapUTXO> list = new List<EthMapUTXO>();
            if (this.EthMapUTXOs.IsNotNullAndEmpty())
            {
                var H = Blockchain.Singleton.Height;
                var ws = this.EthMapUTXOs.Where(m => m.Value.Output.AssetId == assetId && m.Value.LockExpirationIndex < H && !this.UnconfirmCoins.ContainsKey(m.Key));
                if (ws.IsNotNullAndEmpty())
                {
                    foreach (var pair in ws)
                    {
                        list.Add(new EthMapUTXO
                        {
                            Address = pair.Value.Output.ScriptHash,
                            Value = pair.Value.Output.Value.GetInternalValue(),
                            TxId = pair.Key.PrevHash,
                            N = pair.Key.PrevIndex,
                            EthAddress = pair.Value.EthAddress,
                            LockExpirationIndex = pair.Value.LockExpirationIndex,
                        });
                    }
                }
            }
            return list.ToArray();
        }
        #endregion
    }
}
