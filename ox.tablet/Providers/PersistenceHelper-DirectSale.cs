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
using System.Net.Http.Headers;
using OX.Casino;
using OX.DirectSales;

namespace OX.Tablet
{
    public static partial class CasinoPersistenceHelper
    {
     
        public static void Save_TabletMessage(this WriteBatch batch, DirectSaleBlockIndex provider, Block block, ReplyTransaction rt, TabletMessage message)
        {
            batch.Put(SliceBuilder.Begin(CasinoBizPersistencePrefixes.Tablet_Message).Add(rt.Hash), SliceBuilder.Begin().Add(new TabletMessageMerge { Message = message, TimeStamp = block.Timestamp }));
        }
        public static void Save_DirectSalePublish(this WriteBatch batch, DirectSaleBlockIndex provider, Block block, DirectSalePublish publish, AskTransaction at, ushort TxN)
        {
            var N = (ulong)block.Index * 10000 + TxN;
            var key = (UInt64Wrapper)N;
            var value = new DirectSalePublishMerge { N = N, TimeStamp = block.Timestamp, Publish = publish, Tx = at };
            batch.Put(SliceBuilder.Begin(CasinoBizPersistencePrefixes.DirectSale_Publish).Add(key), SliceBuilder.Begin().Add(value));
            provider.DirectSalePublishs[key] = value;
            if (provider.DirectSalePublishs.Count > 500)
            {
                var ks = provider.DirectSalePublishs.Keys.Select(m => m.Value).OrderDescending().Skip(500).ToArray();
                foreach (var kv in ks)
                {
                    var vv = (UInt64Wrapper)kv;
                    batch.Delete(SliceBuilder.Begin(CasinoBizPersistencePrefixes.DirectSale_Publish).Add(vv));
                    provider.DirectSalePublishs.Remove(vv);
                }
            }
        }
        public static void Save_MutualLockSellerTransaction(this WriteBatch batch, DirectSaleBlockIndex provider, Block block, MutualLockSellerTransaction mlst, bool isSeller, ushort TxN)
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
    }
}
