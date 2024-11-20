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
using Org.BouncyCastle.Asn1.X509;
using System.Windows.Forms.VisualStyles;
using System.Text.RegularExpressions;

namespace OX.Tablet
{
    public class AgentBlockIndex : BaseSubBlockIndex
    {
        public const string IndexKey = "Data_AgentIndex";
        public override bool CanRebuild => false;
        public Dictionary<MarkTerm, List<MarkCipherTermBetOrder>> TermCipherOrders = new Dictionary<MarkTerm, List<MarkCipherTermBetOrder>>();
        public Dictionary<UInt256, UInt256> UnfirmOrders = new Dictionary<UInt256, UInt256>();
        public AgentBlockIndex(BlockIndex blockIndex, OXAccount oxAccount, EthAccount ethAccount) : base(blockIndex, oxAccount, ethAccount, IndexKey)
        {
            this.UnfirmOrders = new Dictionary<UInt256, UInt256>(this.GetAll<UInt256, UInt256>(BMSAgentPersistencePrefixes.Unconfirm_Order));
            var cipherOrders = this.GetAll<MarkCipherBetOrderKey, MarkCipherBetOrderValue>(BMSAgentPersistencePrefixes.Cipher_Order, this.OXAccount.ScriptHash);
            List<MarkCipherTermBetOrder> plainlist = new List<MarkCipherTermBetOrder>();
            foreach (var order in cipherOrders)
            {
                var rd = new MarkCipherTermBetOrder { Key = order.Key, Value = order.Value };
                plainlist.Add(rd);
            }
            foreach (var g in plainlist.GroupBy(m => m.Key.Term))
            {
                TermCipherOrders[g.Key] = g.ToList();
            }
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

                    }
                    else if (biztx is AskTransaction at)
                    {
                        
                    }
                }
                else if (tx is LockAssetTransaction lat)
                {
                }
                else if (tx is IssueTransaction ist)
                {
                }
                else if (tx is SlotSideTransaction st)
                {

                }
                else if (tx is RangeTransaction rgt)
                {

                }
                if (UnfirmOrders.TryGetValue(tx.Hash, out var orderKey))
                {
                    foreach (var tr in this.TermCipherOrders.Values)
                    {
                        foreach (var t in tr)
                        {
                            if (t.Value.TxHash == tx.Hash)
                            {
                                t.Value.State = MarkCipherOrderStatus.Confirmed;
                                UpdateMarkCipherOrder(batch, t.Key, t.Value);
                                DeleteUnconfirmOrder(batch, tx.Hash);
                                UnfirmOrders.Remove(tx.Hash);
                            }
                        }
                    }
                }

            }
            this.Db.Write(WriteOptions.Default, batch);
        }
        public void Do(Action<WriteBatch> action)
        {
            WriteBatch batch = new WriteBatch();
            action(batch);
            this.Db.Write(WriteOptions.Default, batch);
        }
      
        public void AddUnconfirmOrder(WriteBatch batch, UInt256 txhash, MarkCipherBetOrderKey orderKey)
        {
            batch.Put(SliceBuilder.Begin(BMSAgentPersistencePrefixes.Unconfirm_Order).Add(txhash), SliceBuilder.Begin().Add(orderKey));
            this.UnfirmOrders[txhash] = orderKey.OrderHash;
        }
        public void DeleteUnconfirmOrder(WriteBatch batch, UInt256 txhash)
        {
            batch.Delete(SliceBuilder.Begin(BMSAgentPersistencePrefixes.Unconfirm_Order).Add(txhash));
        }
        //public bool TrySaveBitMarkSixAgentOrder(MarkInboundOrder order)
        //{
        //    var body = GetInBoundOrder(order.OrderHead);
        //    if (body.IsNull())
        //    {
        //        WriteBatch batch = new WriteBatch();
        //        batch.Put(SliceBuilder.Begin(BMSAgentPersistencePrefixes.Agent_Order).Add(order.OrderHead), SliceBuilder.Begin().Add(order.OrderBody));
        //        this.Db.Write(WriteOptions.Default, batch);
        //        return true;
        //    }
        //    return false;
        //}
        #region inbound
        public bool SaveMarkInboundOrder(MarkInboundOrder order)
        {
            WriteBatch batch = new WriteBatch();
            batch.Put(SliceBuilder.Begin(BMSAgentPersistencePrefixes.Mark_Inbound_Order).Add(order.OrderHead), SliceBuilder.Begin().Add(order.OrderBody));
            this.Db.Write(WriteOptions.Default, batch);
            return true;
        }
        public MarknboundOrderValue GetInBoundOrder(MarkInboundOrderKey key)
        {
            return this.Get<MarknboundOrderValue>(BMSAgentPersistencePrefixes.Mark_Inbound_Order, key);
        }

        public void UpdateMarkInboundOrder(WriteBatch batch, MarkInboundOrder order)
        {
            batch.Put(SliceBuilder.Begin(BMSAgentPersistencePrefixes.Mark_Inbound_Order).Add(order.OrderHead), SliceBuilder.Begin().Add(order.OrderBody));
        }
        public void DeleteMarkInboundOrder(MarkInboundOrderKey key)
        {
            WriteBatch batch = new WriteBatch();
            ReadOptions options = new ReadOptions { FillCache = false };
            batch.Delete(SliceBuilder.Begin(BMSAgentPersistencePrefixes.Mark_Inbound_Order).Add(key));
            Db.Write(WriteOptions.Default, batch);
        }
        public IEnumerable<KeyValuePair<MarkInboundOrderKey, MarknboundOrderValue>> GetMarkInboundOrders(UInt160 masterAddress, MarkTerm Term, MarkChannelRound channelRound = default)
        {
            var builder = SliceBuilder.Begin(BMSAgentPersistencePrefixes.Mark_Inbound_Order).Add(masterAddress).Add(Term);
            if (channelRound.IsNotNull())
            {
                builder = builder.Add(channelRound);
            }
            return this.Db.Find(ReadOptions.Default, builder, (k, v) =>
            {
                var ks = k.ToArray();
                var length = ks.Length - sizeof(byte);
                ks = ks.TakeLast(length).ToArray();
                byte[] data = v.ToArray();
                return new KeyValuePair<MarkInboundOrderKey, MarknboundOrderValue>(ks.AsSerializable<MarkInboundOrderKey>(), data.AsSerializable<MarknboundOrderValue>());
            });
        }

        #endregion
        #region PlainOutbound
        public bool SavePlainOutboundOrder(WriteBatch batch, MarkPlainTermBetOrder order)
        {            
            batch.Put(SliceBuilder.Begin(BMSAgentPersistencePrefixes.Plain_Order).Add(order.Key), SliceBuilder.Begin().Add(order.Value));
            this.Db.Write(WriteOptions.Default, batch);
            return true;
        }
      
        public IEnumerable<KeyValuePair<MarkPlainBetOrderKey, MarkPlainBetOrderValue>> GetMarkPlainOutboundOrders(UInt160 masterAddress, MarkTerm Term, MarkChannelRound channelRound = default)
        {
            var builder = SliceBuilder.Begin(BMSAgentPersistencePrefixes.Plain_Order).Add(masterAddress).Add(Term);
            if (channelRound.IsNotNull())
            {
                builder = builder.Add(channelRound);
            }
            return this.Db.Find(ReadOptions.Default, builder, (k, v) =>
            {
                var ks = k.ToArray();
                var length = ks.Length - sizeof(byte);
                ks = ks.TakeLast(length).ToArray();
                byte[] data = v.ToArray();
                return new KeyValuePair<MarkPlainBetOrderKey, MarkPlainBetOrderValue>(ks.AsSerializable<MarkPlainBetOrderKey>(), data.AsSerializable<MarkPlainBetOrderValue>());
            });
        }

        #endregion
        #region CipherOutbound
        public void UpdateMarkCipherOrder(WriteBatch batch, MarkCipherBetOrderKey key, MarkCipherBetOrderValue Value)
        {
            batch.Put(SliceBuilder.Begin(BMSAgentPersistencePrefixes.Cipher_Order).Add(key), SliceBuilder.Begin().Add(Value));
        }
        public bool SaveCipherOutboundOrder(WriteBatch batch, MarkCipherTermBetOrder order)
        {
            var body = this.Get<MarkCipherBetOrderValue>(BMSAgentPersistencePrefixes.Cipher_Order, order.Key);
            if (body.IsNull())
            {
                batch.Put(SliceBuilder.Begin(BMSAgentPersistencePrefixes.Cipher_Order).Add(order.Key), SliceBuilder.Begin().Add(order.Value));
                if (!this.TermCipherOrders.TryGetValue(order.Key.Term, out var terms))
                {
                    terms = new List<MarkCipherTermBetOrder>();
                    this.TermCipherOrders[order.Key.Term] = terms;
                }
                terms.Add(order);
                return true;
            }
            return false;            
        }
        public IEnumerable<KeyValuePair<MarkCipherBetOrderKey, MarkCipherBetOrderValue>> GetMarkCipherOutboundOrders(UInt160 masterAddress, MarkTerm Term, MarkChannelRound channelRound = default)
        {
            var builder = SliceBuilder.Begin(BMSAgentPersistencePrefixes.Cipher_Order).Add(masterAddress).Add(Term);
            if (channelRound.IsNotNull())
            {
                builder = builder.Add(channelRound);
            }
            return this.Db.Find(ReadOptions.Default, builder, (k, v) =>
            {
                var ks = k.ToArray();
                var length = ks.Length - sizeof(byte);
                ks = ks.TakeLast(length).ToArray();
                byte[] data = v.ToArray();
                return new KeyValuePair<MarkCipherBetOrderKey, MarkCipherBetOrderValue>(ks.AsSerializable<MarkCipherBetOrderKey>(), data.AsSerializable<MarkCipherBetOrderValue>());
            });
        }
        #endregion
    }
}
