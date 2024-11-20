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
using System.ComponentModel;
using System.Collections.Generic;
using OX.IO.Wrappers;
using OX.WebPort.Models;

namespace OX.WebPort
{
    public static partial class MarkPersistenceHelper
    {
        public static void Save_TabletMessage(this WriteBatch batch, MarkBlockIndex provider, Block block, ReplyTransaction rt, TabletMessage message)
        {
            var merge = new TabletMessageMerge { Message = message, TimeStamp = block.Timestamp };
            batch.Put(SliceBuilder.Begin(CasinoBizPersistencePrefixes.Tablet_Message).Add(rt.Hash), SliceBuilder.Begin().Add(merge));
            provider.TabletMessages[rt.Hash] = merge;
        }
        public static void Save_PortMessage(this WriteBatch batch, MarkBlockIndex provider, UInt256 txid, PortMessageMerge message)
        {
            batch.Put(SliceBuilder.Begin(CasinoBizPersistencePrefixes.BMS_Port_Message).Add(txid), SliceBuilder.Begin().Add(message));
            if (!provider.RecentPortMessages.TryGetValue(message.PortId, out var dic))
            {
                dic = new Dictionary<UInt256, PortMessageMerge>();
                provider.RecentPortMessages[message.PortId] = dic;
            }
            dic[txid] = message;
            var c = dic.Count();
            if (c > 50)
            {
                var key = dic.OrderBy(m => m.Value.TimeStamp).FirstOrDefault().Key;
                dic.Remove(key);
                batch.Delete(SliceBuilder.Begin(CasinoBizPersistencePrefixes.BMS_Port_Message).Add(key));
            }
        }
        public static void Save_MarkOrder(this WriteBatch batch, MarkBlockIndex provider, UInt256 txid, MarkEncodedBetOrderMix order)
        {
            batch.Put(SliceBuilder.Begin(CasinoBizPersistencePrefixes.BMS_Recent_Orders).Add(txid), SliceBuilder.Begin().Add(order));
            if (!provider.RecentOrders.TryGetValue(order.Order.Player, out var dic))
            {
                dic = new Dictionary<UInt256, MarkEncodedBetOrderMix>();
                provider.RecentOrders[order.Order.Player] = dic;
            }
            dic[txid] = order;
            var c = dic.Count();
            if (c > 50)
            {
                var key = dic.OrderBy(m => m.Value.TimeStamp).FirstOrDefault().Key;
                dic.Remove(key);
                batch.Delete(SliceBuilder.Begin(CasinoBizPersistencePrefixes.BMS_Recent_Orders).Add(key));
            }
        }
        public static void Save_MarkMember(this WriteBatch batch, MarkBlockIndex provider, MixMarkMember member)
        {
            batch.Put(SliceBuilder.Begin(CasinoBizPersistencePrefixes.Mark_Member).Add(member.Holder), SliceBuilder.Begin().Add(member));
            provider.MarkMembers[member.Holder] = member;
        }

        public static void Save_LastBitSixBankerId(this WriteBatch batch, LastBitSixBankerId lastBitSixBankerId)
        {
            batch.Put(SliceBuilder.Begin(CasinoBizPersistencePrefixes.BMS_Last_BankerId).Add(casino.CasinoMasterAccountAddress), SliceBuilder.Begin().Add(lastBitSixBankerId));
        }
        public static void Save_LastBitSixMemberId(this WriteBatch batch, LastBitSixMemberId lastBitSixMemberId)
        {
            batch.Put(SliceBuilder.Begin(CasinoBizPersistencePrefixes.BMS_Last_MemberId).Add(casino.CasinoMasterAccountAddress), SliceBuilder.Begin().Add(lastBitSixMemberId));
        }


    }
}
