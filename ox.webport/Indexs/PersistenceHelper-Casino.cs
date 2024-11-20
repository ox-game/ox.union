using OX.IO;
using OX.IO.Data.LevelDB;
using OX.Network.P2P;
using OX.Network.P2P.Payloads;
using OX.SmartContract;
using OX;
using OX.VM;
using OX.Ledger;
using System.Linq;
using System;
using System.Runtime.CompilerServices;
using OX.BMS;
using OX.Wallets;
using System.Collections.Generic;
using OX.IO.Wrappers;
using OX.DirectSales;

namespace OX.WebPort
{

    public static partial class CasinoPersistenceHelper
    {
        public static void Save_MutualLockSellerTransaction(this WriteBatch batch, CasinoBlockIndex provider, Block block, MutualLockSellerTransaction mlst, bool isSeller, ushort TxN)
        {
            var key = new MutualLockSellerTransactionKey
            {
                Holder = isSeller ? Contract.CreateSignatureRedeemScript(mlst.Seller).ToScriptHash() : Contract.CreateSignatureRedeemScript(mlst.Buyer).ToScriptHash(),
                IsSeller = isSeller,
                BlockIndex = block.Index,
                TimeStamp = block.Timestamp,
                TxHash = mlst.Hash
            };
            var value = new MutualLockSellerTransactionValue { MLST = mlst };
            batch.Put(SliceBuilder.Begin(CasinoBizPersistencePrefixes.DirectSale_SellerTx_My).Add(key), SliceBuilder.Begin().Add(value));
            provider.MutualLockRecords[value.MLST.GetContract().ScriptHash] = new MutualLockSellerTransactionMerge { Key = key, Value = value };
        }
        public static void Save_DirectSalePublish(this WriteBatch batch, CasinoBlockIndex provider, Block block, DirectSalePublish publish, AskTransaction at, ushort TxN)
        {
            var N = (ulong)block.Index * 10000 + TxN;
            var key = (UInt64Wrapper)N;
            var value = new DirectSalePublishMerge { N = N, TimeStamp = block.Timestamp, Publish = publish, Tx = at };
            batch.Put(SliceBuilder.Begin(CasinoBizPersistencePrefixes.DirectSale_Publish).Add(key), SliceBuilder.Begin().Add(value));
            provider.DirectSalePublishs[key] = value;
            if (provider.DirectSalePublishs.Count > 100)
            {
                var ks = provider.DirectSalePublishs.Keys.Select(m => m.Value).OrderDescending().Skip(100).ToArray();
                foreach (var kv in ks)
                {
                    var vv = (UInt64Wrapper)kv;
                    batch.Delete(SliceBuilder.Begin(CasinoBizPersistencePrefixes.DirectSale_Publish).Add(vv));
                    provider.DirectSalePublishs.Remove(vv);
                }
            }
        }
        public static void Save_CasinoSettingRecord(this WriteBatch batch, CasinoBlockIndex provider, BizRecordModel model, CasinoSettingRecord record)
        {
            if (record != default && record.Value != default)
                batch.Put(SliceBuilder.Begin(CasinoBizPersistencePrefixes.Casino_Setting).Add(model.Key), SliceBuilder.Begin().Add(record));
            provider.CasinoSettings.Clear();
        }
        public static void Save_Web3Node(this WriteBatch batch, CasinoBlockIndex provider, Block block, ReplyTransaction rt, Web3NodeSet web3NodeSet)
        {
            if (web3NodeSet.IsNotNull() && web3NodeSet.Nodes.IsNotNullAndEmpty())
            {
                batch.Delete(SliceBuilder.Begin(CasinoBizPersistencePrefixes.Casino_Web3Node_Publish));
                provider.Web3Nodes.Clear();
                foreach (var node in web3NodeSet.Nodes)
                {
                    var ts = (UInt32Wrapper)block.Timestamp;
                    batch.Put(SliceBuilder.Begin(CasinoBizPersistencePrefixes.Casino_Web3Node_Publish).Add(node), SliceBuilder.Begin().Add(ts.ToArray()));
                    if (!provider.Web3Nodes.TryGetValue(node.Catalog, out var dic))
                    {
                        dic = new Dictionary<string, uint>();
                        provider.Web3Nodes[node.Catalog] = dic;
                    }
                    dic[$"{node.NodeAddress}:{node.Port}"] = block.Timestamp;
                }
            }
        }
        public static void Save_GuessAnswer(this WriteBatch batch, CasinoBlockIndex provider, GuessAnswerReply guessAnswerReply)
        {
            foreach (var answer in guessAnswerReply.Answers)
            {
                batch.Put(SliceBuilder.Begin(CasinoBizPersistencePrefixes.BMS_GuessAnswer).Add(answer.Key), SliceBuilder.Begin().Add(answer.Value));
                var record = new GuessAnswer { Key = answer.Key, Value = answer.Value };
                provider.GuessAnswers[answer.Key.ToString()] = record;
                if (!provider.LatestGuessAnswer.TryGetValue(record.Key.ChannelRound, out var v) || record.Key.Term.ToDateTime() > v.Key.Term.ToDateTime())
                {
                    provider.LatestGuessAnswer[record.Key.ChannelRound] = record;
                }
            }
        }
        

    }
}
