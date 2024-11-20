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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using OX.BMS;
using OX.Casino;
using OX.Cryptography.ECC;
using OX.SmartContract;
using OX.DirectSales;

namespace OX.WebPort
{
    public class CasinoBlockIndex : BaseSubBlockIndex
    {
        public const string IndexKey = "Data_CasinoIndex";
        public override bool CandRebuild => true;
        public CasinoBlockIndex(BlockIndex blockIndex) : base(blockIndex, IndexKey)
        {
            this.LatestGuessAnswer = new Dictionary<MarkChannelRound, GuessAnswer>();
            foreach (var ga in this.GetAll<GuessAnswerKey, GuessAnswerValue>(CasinoBizPersistencePrefixes.BMS_GuessAnswer))
            {
                var record = new GuessAnswer { Key = ga.Key, Value = ga.Value };
                this.GuessAnswers[ga.Key.ToString()] = record;
                if (!this.LatestGuessAnswer.TryGetValue(record.Key.ChannelRound, out var v) || record.Key.Term.ToDateTime() > v.Key.Term.ToDateTime())
                {
                    this.LatestGuessAnswer[record.Key.ChannelRound] = record;
                }
            }
            foreach (var ga in this.GetAll<Web3Node, OX.IO.Wrappers.UInt32Wrapper>(CasinoBizPersistencePrefixes.Casino_Web3Node_Publish).GroupBy(m => m.Key.Catalog))
            {
                if (!Web3Nodes.TryGetValue(ga.Key, out var dic))
                {
                    dic = new Dictionary<string, uint>();
                    Web3Nodes[ga.Key] = dic;
                }
                foreach (var node in ga)
                {
                    dic[$"{node.Key.NodeAddress}:{node.Key.Port}"] = node.Value;
                }
            }
            this.InitCasinoSetting();
            this.DirectSalePublishs = new Dictionary<UInt64Wrapper, DirectSalePublishMerge>(this.GetAll<UInt64Wrapper, DirectSalePublishMerge>(CasinoBizPersistencePrefixes.DirectSale_Publish));
            foreach (var p in this.GetAll<MutualLockSellerTransactionKey, MutualLockSellerTransactionValue>(CasinoBizPersistencePrefixes.DirectSale_SellerTx_My))
            {
                MutualLockRecords[p.Value.MLST.GetContract().ScriptHash] = new MutualLockSellerTransactionMerge { Key = p.Key, Value = p.Value };
            }
        }
        public Dictionary<string, GuessAnswer> GuessAnswers { get; private set; } = new Dictionary<string, GuessAnswer>();
        public Dictionary<MarkChannelRound, GuessAnswer> LatestGuessAnswer { get; internal set; } = new Dictionary<MarkChannelRound, GuessAnswer>();
        public Dictionary<ushort, Dictionary<string, uint>> Web3Nodes { get; internal set; } = new Dictionary<ushort, Dictionary<string, uint>>();
        public Dictionary<byte, string> CasinoSettings { get; private set; } = new Dictionary<byte, string>();
        public Dictionary<UInt64Wrapper, DirectSalePublishMerge> DirectSalePublishs = new Dictionary<UInt64Wrapper, DirectSalePublishMerge>();
        public Dictionary<UInt160, MutualLockSellerTransactionMerge> MutualLockRecords = new Dictionary<UInt160, MutualLockSellerTransactionMerge>();
        public override void Rebuild()
        {
            this.GuessAnswers.Clear();
            this.LatestGuessAnswer.Clear();
            this.Web3Nodes.Clear();
            this.CasinoSettings.Clear();
            this.DirectSalePublishs.Clear();
            this.MutualLockRecords.Clear();
            base.Rebuild();           
        }

        void InitCasinoSetting()
        {
            this.CasinoSettings.Clear();
            foreach (var setting in this.GetAllCasinoSettings())
            {
                this.CasinoSettings[setting.Key[0]] = setting.Value.Value;
            }
        }
        public string GetCasinoSetting(byte key)
        {
            if (this.CasinoSettings.IsNullOrEmpty())
                this.InitCasinoSetting();
            var setting = string.Empty;
            this.CasinoSettings.TryGetValue(key, out setting);
            return setting;
        }
        public override void HandleBlock(Block block)
        {
            WriteBatch batch = new WriteBatch();
            BlockContext context = new BlockContext();
            for (ushort i = 0; i < block.Transactions.Length; i++)
            {
                var tx = block.Transactions[i];

                bool isAsk = false;
                if (this.IsBizTransaction(tx, casino.CasinoMasterAccountAddress, out BizTransaction biztx))
                {
                    ushort? n = null;
                    if (biztx is BillTransaction bt)
                    {
                        foreach (var record in bt.Records)
                        {
                            var bizModel = CasinoBizRecordHelper.BuildModel(record);
                            if (bizModel.Model is CasinoSettingRecord CasinoSettingRecord)
                            {
                                batch.Save_CasinoSettingRecord(this, bizModel, CasinoSettingRecord);
                            }                             
                        }
                    }
                    else if (biztx is ReplyTransaction rt)
                    {
                        this.OnCasinoReplayTransaction(batch, context, block, rt, i);
                    }
                    else if (biztx is AskTransaction at)
                    {
                        isAsk = true;
                        this.OnCasinoAskTransaction(batch, context, block, at, i, out n);
                    }
                }
               
                else if (tx is MutualLockSellerTransaction mlst)
                {
                    if (mlst.LockContract == Blockchain.MutualLockContractScriptHash&&mlst.Arbiter.Equals(casino.CasinoMasterAccountPubKey))
                    {
                        batch.Save_MutualLockSellerTransaction(this, block, mlst, true, i);
                    }                     
                }
                else if (tx is MutualLockBuyerTransaction mlbt)
                {
                    if (this.MutualLockRecords.TryGetValue(mlbt.MutualLockScriptHash, out var sellerMerge))
                    {
                        sellerMerge.Value.MLBT = mlbt;
                        batch.Put(SliceBuilder.Begin(CasinoBizPersistencePrefixes.DirectSale_SellerTx_My).Add(sellerMerge.Key), SliceBuilder.Begin().Add(sellerMerge.Value));
                    }
                }
                //2.txo
                foreach (var coin in tx.References)
                {
                    if (this.MutualLockRecords.TryGetValue(coin.Value.ScriptHash, out var merge))
                    {
                        batch.Delete(SliceBuilder.Begin(CasinoBizPersistencePrefixes.DirectSale_SellerTx_My).Add(merge.Key));
                        this.MutualLockRecords.Remove(coin.Value.ScriptHash);
                    }
                }
                //2.utxo
                if (tx.Outputs.IsNotNullAndEmpty())
                {
                    for (ushort k = 0; k < tx.Outputs.Length; k++)
                    {
                        TransactionOutput output = tx.Outputs[k];

                    }
                }
                
               
            }

            this.Db.Write(WriteOptions.Default, batch);

        }
        public IEnumerable<KeyValuePair<byte[], CasinoSettingRecord>> GetAllCasinoSettings()
        {
            return this.Db.Find(ReadOptions.Default, SliceBuilder.Begin(CasinoBizPersistencePrefixes.Casino_Setting), (k, v) =>
            {
                var ks = k.ToArray();
                var length = ks.Length - sizeof(byte);
                ks = ks.TakeLast(length).ToArray();
                byte[] data = v.ToArray();
                return new KeyValuePair<byte[], CasinoSettingRecord>(ks, data.AsSerializable<CasinoSettingRecord>());
            });
        }
        public bool TryGetCasinoSetting(byte settingKey, out string settingValue)
        {
            settingValue = string.Empty;
            var r = this.Get<CasinoSettingRecord>(CasinoBizPersistencePrefixes.Casino_Setting, new byte[] { settingKey });
            if (r.IsNotNull())
            {
                settingValue = r.Value;
                return true;
            }
            return false;
        }
        public void OnCasinoReplayTransaction(WriteBatch batch, BlockContext context, Block block, ReplyTransaction rt, ushort txindex)
        {
            IReadOnlyDictionary<CoinReference, TransactionOutput> rfs = rt.References;
            var shs = rfs.Values.GroupBy(m => m.ScriptHash).Select(n => n.Key.ToAddress());
            var bizshs = new UInt160[] { casino.CasinoMasterAccountAddress };

            //1.验证Tip标签的真实性,不真实则返回不做后续处理
            switch (rt.DataType)
            {
                case (byte)CasinoType.MarkGuessAnswer:
                    if (rt.GetDataModel<GuessAnswerReply>(bizshs, (byte)CasinoType.MarkGuessAnswer, out GuessAnswerReply guessAnswerReply))
                    {
                        batch.Save_GuessAnswer(this, guessAnswerReply);
                    }
                    break;
                case (byte)CasinoType.CasinoWeb3NodePublish:
                    if (rt.GetDataModel<Web3NodeSet>(bizshs, (byte)CasinoType.CasinoWeb3NodePublish, out Web3NodeSet web3NodeSet))
                    {
                        batch.Save_Web3Node(this, block, rt, web3NodeSet);
                    }
                    break;
            }

        }
        public void OnCasinoAskTransaction(WriteBatch batch, BlockContext context, Block block, AskTransaction at, ushort txindex, out ushort? n)
        {

            n = null;
            IReadOnlyDictionary<CoinReference, TransactionOutput> rfs = at.References;
            var shs = rfs.Values.GroupBy(m => m.ScriptHash).Select(n => n.Key.ToAddress());
            var bizshs = new UInt160[] { casino.CasinoMasterAccountAddress };

            //1.验证Tip标签的真实性,不真实则返回不做后续处理
            switch (at.DataType)
            {
                case (byte)CasinoType.DirectSalePublish:
                    if (at.GetDataModel<DirectSalePublish>(bizshs, (byte)CasinoType.DirectSalePublish, out DirectSalePublish publish))
                    {
                        if (publish.VerifyDirectSalePublish(at))
                        {
                            batch.Save_DirectSalePublish(this, block, publish, at, txindex);
                        }
                    }
                    break;
            }

        }
    
    }
}
