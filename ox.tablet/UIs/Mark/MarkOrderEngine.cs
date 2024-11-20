using OX.BMS;
using OX.Tablet.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using System.Web;
using Microsoft.AspNetCore.Http.HttpResults;
using OX.Tablet.Config;
using AntDesign.Charts;
using OX.IO;
namespace OX.Tablet.UIs.Mark
{
    public abstract class InBoundBuilder
    {
        public abstract MarkInboundOrder Build();
        public MarknboundOrderValue GetInboundOrder(MarkInboundOrderKey key)
        {
            var agentIndex = SecureHelper.BlockIndex.GetSubBlockIndex<AgentBlockIndex>();
            if (agentIndex.IsNotNull())
            {
                return agentIndex.GetInBoundOrder(key);
            }
            return default;
        }
        public void SaveInboundOrder(MarkInboundOrder order)
        {
            var agentIndex = SecureHelper.BlockIndex.GetSubBlockIndex<AgentBlockIndex>();
            if (agentIndex.IsNotNull())
            {
                agentIndex.SaveMarkInboundOrder(order);
            }
        }
        public IEnumerable<KeyValuePair<MarkInboundOrderKey, MarknboundOrderValue>> GetMarkInboundOrders(UInt160 masterAddress, MarkTerm Term, MarkChannelRound channelRound = default)
        {
            var agentIndex = SecureHelper.BlockIndex.GetSubBlockIndex<AgentBlockIndex>();
            if (agentIndex.IsNotNull())
            {
                return agentIndex.GetMarkInboundOrders(masterAddress, Term, channelRound);
            }
            return default;
        }
    }
    public class TextInBoundBuilder : InBoundBuilder
    {
        public const string MarkOneTitle = "一合";
        public const string MarkUnionTitle = "港澳联合";
        public const string MarkHKTitle = "香港";
        public const string MarkMacauTitle = "澳门";
        public TextInput TextInput { get; private set; }
        public TextInBoundBuilder(TextInput textInput)
        {
            this.TextInput = textInput;
        }
        public override MarkInboundOrder Build()
        {
            MarkInboundOrder neworder = default;
            var agentIndex = SecureHelper.BlockIndex.GetSubBlockIndex<AgentBlockIndex>();
            if (TryParse(this.TextInput, out var order))
            {
                neworder = order;
                return neworder;
            }
            else
            {
                if (TryDecodeOrderText(this.TextInput.Text, out var str))
                {
                    if (TryParse(new TextInput { Text = str, FromName = this.TextInput.FromName }, out var order2))
                    {
                        neworder = order2;
                        return neworder;
                    }
                }
            }
            return neworder;
        }
        public bool TryParse(TextInput textInput, out MarkInboundOrder order)
        {
            order = default;
            try
            {
                var str = textInput.Text.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                var ss = str.Split("#", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                var pattarn_betitem = "(?<POINTS>[\\s\\S]*?)=(?<AMOUNT>\\d*)";

                var pattarn_unid = "UNID:(?<UNID>\\d*)";
                var match = Regex.Match(ss[0], pattarn_unid);
                if (!match.Success) return false;
                var unid = match.Groups["UNID"].Value;
                if (!ulong.TryParse(unid, out ulong sn)) return false;

                var pattarn_kind = "(?<KIND>[\\s\\S]*?)总计:(?<TOTAL>\\d*)";
                match = Regex.Match(ss[1], pattarn_kind);
                if (!match.Success) return false;
                var kind = match.Groups["KIND"].Value;
                var total = match.Groups["TOTAL"].Value;

                var count = ss.Length;
                List<string> methodStrs = new List<string>();
                for (int i = 2; i < count; i++)
                {
                    methodStrs.Add(ss[i].ToString());
                }
                if (kind == MarkUnionTitle || kind == MarkHKTitle || kind == MarkMacauTitle)
                {
                    MarkSixRound sixRound = default;
                    switch (kind)
                    {
                        case MarkUnionTitle:
                            sixRound = MarkSixRound.MarkUnion;
                            break;
                        case MarkHKTitle:
                            sixRound = MarkSixRound.MarkHK;
                            break;
                        case MarkMacauTitle:
                            sixRound = MarkSixRound.MarkMacau;
                            break;
                    }
                    List<BitMarkSixBet> bets = new List<BitMarkSixBet>();
                    foreach (var method in methodStrs)
                    {
                        var ms = method.Split(':');
                        if (ms.Length != 2) return false;
                        var methodName = ms[0];
                        MarkSixBetMethod FullMarkSixBetMethod = default;
                        foreach (var bm in NoneFlagEnumHelper.All<MarkSixBetMethod>())
                        {
                            if (bm.GetMethodSetting().Name == methodName)
                            {
                                FullMarkSixBetMethod = bm;
                            }
                        }
                        var items = ms[1].Split('米', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in items)
                        {
                            var betItemmatch = Regex.Match(item, pattarn_betitem);
                            if (!betItemmatch.Success) return false;
                            var points = betItemmatch.Groups["POINTS"].Value;
                            var amount = betItemmatch.Groups["AMOUNT"].Value;
                            if (!uint.TryParse(amount, out uint amt)) return false;
                            if (!BetPoint.TryFromChinaDisplayString(BetChannel.MarkSix, (byte)FullMarkSixBetMethod, points, out var betpoint))
                                return false;
                            BitMarkSixBet betItem = new BitMarkSixBet { Amount = amt, BetTarget = new BitMarkSixBetTarget { Method = (byte)FullMarkSixBetMethod, BetPoint = betpoint } };
                            bets.Add(betItem);
                        }
                    }
                    var term = System.DateTime.Now.GetBetTermForPlayer(SecureHelper.GetMarkSixOpenSeconds());
                    MarkInboundOrderKey key = new MarkInboundOrderKey()
                    {
                        Reception = SecureHelper.MasterAccount.ScriptHash,
                        ChannelRound = new MarkChannelRound { Channel = BetChannel.MarkSix, Round = (byte)sixRound },
                        InboundOrigin = InboundOrigin.DirectSale,
                        CNO = sn,
                        Term = term
                    };
                    MarknboundOrderValue value = new MarknboundOrderValue { State = 0, BetItems = bets.ToArray(), FromName = textInput.FromName };
                    order = new MarkInboundOrder { OrderHead = key, OrderBody = value };
                    return true;
                }
                else if (kind == MarkOneTitle)
                {
                    List<BitMarkSixBet> bets = new List<BitMarkSixBet>();
                    foreach (var method in methodStrs)
                    {
                        var ms = method.Split(':');
                        if (ms.Length != 2) return false;
                        var methodName = ms[0];
                        MarkOneBetMethod SimpleMarkSixBetMethod = default;
                        foreach (var bm in NoneFlagEnumHelper.All<MarkOneBetMethod>())
                        {
                            if (bm.GetMethodSetting().Name == methodName)
                            {
                                SimpleMarkSixBetMethod = bm;
                            }
                        }
                        var items = ms[1].Split('米', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in items)
                        {
                            var betItemmatch = Regex.Match(item, pattarn_betitem);
                            if (!betItemmatch.Success) return false;
                            var points = betItemmatch.Groups["POINTS"].Value;
                            var amount = betItemmatch.Groups["AMOUNT"].Value;
                            if (!uint.TryParse(amount, out uint amt)) return false;
                            if (!BetPoint.TryFromChinaDisplayString(BetChannel.MarkOne, (byte)SimpleMarkSixBetMethod, points, out var betpoint))
                                return false;
                            BitMarkSixBet betItem = new BitMarkSixBet { Amount = amt, BetTarget = new BitMarkSixBetTarget { Method = (byte)SimpleMarkSixBetMethod, BetPoint = betpoint } };
                            bets.Add(betItem);
                        }
                    }
                    var term = System.DateTime.Now.GetBetTermForPlayer(SecureHelper.GetMarkSixOpenSeconds());
                    var hour = BitSixBetHelper.BeijingNow().Hour;
                    MarkOneRound round = default;
                    if (hour < 13)
                    {
                        round = MarkOneRound.PM_1;
                    }
                    else if (hour < 15)
                    {
                        round = MarkOneRound.PM_3;
                    }
                    else if (hour < 17)
                    {
                        round = MarkOneRound.PM_5;
                    }
                    else if (hour < 19)
                    {
                        round = MarkOneRound.PM_7;
                    }
                    if (round == default) return false;
                    MarkInboundOrderKey key = new MarkInboundOrderKey()
                    {
                        Reception = SecureHelper.MasterAccount.ScriptHash,
                        ChannelRound = new MarkChannelRound { Channel = BetChannel.MarkOne, Round = (byte)round },
                        InboundOrigin = InboundOrigin.DirectSale,
                        CNO = sn,
                        Term = term
                    };
                    MarknboundOrderValue value = new MarknboundOrderValue { State = 0, BetItems = bets.ToArray(), FromName = textInput.FromName };
                    order = new MarkInboundOrder { OrderHead = key, OrderBody = value };
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// js encode:
        /// btoa(encodeURIComponent(str));
        /// </summary>
        /// <param name="orderChiper"></param>
        /// <returns></returns>
        public bool TryDecodeOrderText(string orderChiper, out string text)
        {
            text = string.Empty;
            var ss = orderChiper;
            if (ss.Contains('#'))
            {
                var cs = ss.Trim().Split('#', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (cs.Length == 1) ss = cs[0]; ;
                if (cs.Length == 2) ss = cs[1];
            }
            try
            {
                text = HttpUtility.UrlDecode(System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(ss)));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    public class PortInBoundBuilder : InBoundBuilder
    {
        public TextInput TextInput { get; private set; }
        public PortInBoundBuilder(TextInput textInput)
        {
            this.TextInput = textInput;
        }
        public override MarkInboundOrder Build()
        {
            MarkInboundOrder neworder = default;
            try
            {
                var cs = TextInput.Text.Split('#');
                if (cs.IsNotNullAndEmpty() && cs.Length == 4)
                {
                    var str = cs[3];
                    var bs = Convert.FromBase64String(str);
                    if (bs.TryAsSerializable<MarkPlainCopyOrder>(out var order))
                    {
                        if (order.Order.Player != SecureHelper.MasterAccount.ScriptHash)
                        {
                            var term = System.DateTime.Now.GetBetTermForPlayer(SecureHelper.GetMarkSixOpenSeconds());
                            MarkInboundOrderKey key = new MarkInboundOrderKey()
                            {
                                Reception = SecureHelper.MasterAccount.ScriptHash,
                                ChannelRound = order.Order.ChannelRound,
                                InboundOrigin = InboundOrigin.LevelSale,
                                CNO = order.Nonce,
                                Term = order.Order.Term
                            };
                            MarknboundOrderValue value = new MarknboundOrderValue { State = 0, BetItems = order.Order.BetItems, FromName = TextInput.FromName };
                            neworder = new MarkInboundOrder { OrderHead = key, OrderBody = value };
                        }
                    }
                }
            }
            catch
            {
                return default;
            }
            return neworder;
        }

    }
    public class ManualBoundBuilder : InBoundBuilder
    {
        static Dictionary<MarkChannelRound, MarkMemoryOrderSet> MemoryOrders = new Dictionary<MarkChannelRound, MarkMemoryOrderSet>();
        public MarkChannelRound ChannelRouond { get; private set; }
        public ManualBoundBuilder(MarkChannelRound channelRound)
        {
            ChannelRouond = channelRound;
            if (!MemoryOrders.TryGetValue(channelRound, out var order))
            {
                MemoryOrders[channelRound] = new MarkMemoryOrderSet();
            }
        }
        public MarkMemoryOrderSet GetMemoryOrderSet()
        {
            return MemoryOrders[this.ChannelRouond];
        }
        public MarkMemoryBetSet GetMemoryBetSet(byte Method)
        {
            MarkMemoryBetSet betSet = default;
            var orderSet = GetMemoryOrderSet();
            if (!orderSet.BetSets.TryGetValue(Method, out betSet))
            {
                betSet = new MarkMemoryBetSet { Method = Method };
                orderSet.BetSets[Method] = betSet;
            }
            return betSet;
        }
        public bool PushAllInbound(MarkInboundOrderKey orderKey)
        {
            var orderSet = GetMemoryOrderSet();
            List<BitMarkSixBet> betList = new List<BitMarkSixBet>();
            foreach (var bs in orderSet.BetSets)
            {
                foreach (var b in bs.Value.BetItems)
                {
                    betList.Add(new BitMarkSixBet { Amount = b.Value, BetTarget = b.Key });
                }
            }
            if (betList.IsNotNullAndEmpty())
            {
                MarknboundOrderValue agentOrderValue = new MarknboundOrderValue { State = 0, BetItems = betList.ToArray(), FromName = UIHelper.LocalString("自投", "Self") };
                MarkInboundOrder order = new MarkInboundOrder { OrderHead = orderKey, OrderBody = agentOrderValue };
                this.SaveInboundOrder(order);
                orderSet.ClearAll();
                return true;
            }
            return false;
        }

        public override MarkInboundOrder Build()
        {
            return default;
        }
    }
    public class ChainInBoundBuilder : InBoundBuilder
    {
        MarkPlainBetOrder Order;
        public ChainInBoundBuilder(MarkPlainBetOrder order)
        {
            this.Order = order;
        }
        public override MarkInboundOrder Build()
        {
            //var nonce = BetUIHelper.GetNonce();
            //MarkInboundOrderKey orderKey = new MarkInboundOrderKey
            //{
            //    ChannelRound = this.Order.ChannelRound,
            //    InboundOrigin = InboundOrigin.LevelSale,
            //    CNO = nonce,
            //    Term =
            //};
            //MarknboundOrderValue orderValue;
            //return new MarkInboundOrder
            //{
            //    OrderHead = orderKey,
            //    OrderBody = orderValue
            //};
            return default;
        }
    }
}
