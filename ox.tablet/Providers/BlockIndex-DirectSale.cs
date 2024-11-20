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
using OX.Tablet.Models;
using OX.Cryptography.ECC;
using OX.BMS;
using OX.Casino;
using Akka.Actor;
using OX.SmartContract;
using OX.DirectSales;

namespace OX.Tablet
{
    public class DirectSaleBlockIndex : BaseSubBlockIndex
    {
        public const string IndexKey = "Data_DirectSaleIndex";
        public override bool CanRebuild => true;
        public Dictionary<UInt64Wrapper, DirectSalePublishMerge> DirectSalePublishs = new Dictionary<UInt64Wrapper, DirectSalePublishMerge>();
        public Dictionary<UInt160, MutualLockSellerTransactionMerge> MutualLockRecords = new Dictionary<UInt160, MutualLockSellerTransactionMerge>();
        public DirectSaleBlockIndex(BlockIndex blockIndex, OXAccount oxAccount, EthAccount ethAccount) : base(blockIndex, oxAccount, ethAccount, IndexKey)
        {
            this.DirectSalePublishs = new Dictionary<UInt64Wrapper, DirectSalePublishMerge>(this.GetAll<UInt64Wrapper, DirectSalePublishMerge>(CasinoBizPersistencePrefixes.DirectSale_Publish));
            foreach (var p in this.GetAll<MutualLockSellerTransactionKey, MutualLockSellerTransactionValue>(CasinoBizPersistencePrefixes.DirectSale_SellerTx_My))
            {
                MutualLockRecords[p.Value.MLST.GetContract().ScriptHash] = new MutualLockSellerTransactionMerge { Key = p.Key, Value = p.Value };
            }
        }

        public override void Rebuild()
        {
            this.DirectSalePublishs.Clear();
            this.MutualLockRecords.Clear();
            base.Rebuild();
        }
        public override void HandleBlock(Block block)
        {
            WriteBatch batch = new WriteBatch();
            BlockContext context = new BlockContext();
            for (ushort i = 0; i < block.Transactions.Length; i++)
            {
                var tx = block.Transactions[i];
                if (this.IsBizTransaction(tx, casino.CasinoMasterAccountAddress, out BizTransaction biztx))
                {
                    ushort? n = null;
                    if (biztx is BillTransaction bt)
                    {

                    }
                    else if (biztx is ReplyTransaction rt)
                    {
                        this.OnCasinoReplayTransaction(batch, context, block, rt, i);
                    }
                    else if (biztx is AskTransaction at)
                    {
                        this.OnCasinoAskTransaction(batch, context, block, at, i, out n);
                    }
                }
                else if (tx is MutualLockSellerTransaction mlst)
                {
                    if (mlst.LockContract == Blockchain.MutualLockContractScriptHash && mlst.Arbiter.Equals(casino.CasinoMasterAccountPubKey))
                    {
                        if (mlst.Seller.Equals(this.OXAccount.Key.PublicKey))
                        {
                            batch.Save_MutualLockSellerTransaction(this, block, mlst, true, i);
                        }
                        else if (mlst.Buyer.Equals(this.OXAccount.Key.PublicKey))
                        {
                            batch.Save_MutualLockSellerTransaction(this, block, mlst, false, i);
                        }
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
                foreach (var coin in tx.References)
                {
                    if (this.MutualLockRecords.TryGetValue(coin.Value.ScriptHash, out var merge))
                    {
                        batch.Delete(SliceBuilder.Begin(CasinoBizPersistencePrefixes.DirectSale_SellerTx_My).Add(merge.Key));
                        this.MutualLockRecords.Remove(coin.Value.ScriptHash);
                    }
                }
            }
            this.Db.Write(WriteOptions.Default, batch);
        }
        public void OnCasinoReplayTransaction(WriteBatch batch, BlockContext context, Block block, ReplyTransaction rt, ushort txindex)
        {
            IReadOnlyDictionary<CoinReference, TransactionOutput> rfs = rt.References;
            var shs = rfs.Values.GroupBy(m => m.ScriptHash).Select(n => n.Key.ToAddress());
            var bizshs = new UInt160[] { casino.CasinoMasterAccountAddress };

            //1.验证Tip标签的真实性,不真实则返回不做后续处理
            switch (rt.DataType)
            {

                case (byte)CasinoType.TabletMessage:
                    if (rt.GetDataModel<TabletMessage>(bizshs, (byte)CasinoType.TabletMessage, out TabletMessage message))
                    {
                        batch.Save_TabletMessage(this, block, rt, message);
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
