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
using OX.Cryptography.ECC;
using OX.BMS;
using OX.Casino;
using Akka.Actor;
using OX.SmartContract;
using static OX.BMS.MarkPortPlayerTermKey;
using OX.WebPort.Models;

namespace OX.WebPort
{
    public class MarkBlockIndex : BaseSubBlockIndex
    {
        public const string IndexKey = "Data_MarkIndex";
        public override bool CandRebuild => true;
        public MarkBlockIndex(BlockIndex blockIndex) : base(blockIndex, IndexKey)
        {
            var LBS = this.Get<LastBitSixBankerId>(CasinoBizPersistencePrefixes.BMS_Last_BankerId, casino.CasinoMasterAccountAddress);
            if (LBS.IsNull())
            {
                LBS = new LastBitSixBankerId { BitSixBankerId = 8000 };
            }
            this.LastBitSixBankerId = LBS;
            this.MarkMembers = new Dictionary<UInt160, MixMarkMember>(this.GetAll<UInt160, MixMarkMember>(CasinoBizPersistencePrefixes.Mark_Member));
            foreach (var p in this.GetAll<UInt256, MarkEncodedBetOrderMix>(CasinoBizPersistencePrefixes.BMS_Recent_Orders))
            {
                if (!this.RecentOrders.TryGetValue(p.Value.Order.Player, out var dic))
                {
                    dic = new Dictionary<UInt256, MarkEncodedBetOrderMix>();
                    this.RecentOrders[p.Value.Order.Player] = dic;
                }
                dic[p.Key] = p.Value;
            }
            foreach (var p in this.GetAll<UInt256, PortMessageMerge>(CasinoBizPersistencePrefixes.BMS_Port_Message))
            {
                if (!this.RecentPortMessages.TryGetValue(p.Value.PortId, out var dic))
                {
                    dic = new Dictionary<UInt256, PortMessageMerge>();
                    this.RecentPortMessages[p.Value.PortId] = dic;
                }
                dic[p.Key] = p.Value;
            }
            this.TabletMessages = new Dictionary<UInt256, TabletMessageMerge>(this.GetAll<UInt256, TabletMessageMerge>(CasinoBizPersistencePrefixes.Tablet_Message));
        }
        public LastBitSixBankerId LastBitSixBankerId { get; private set; } = new LastBitSixBankerId { BitSixBankerId = 8000 };

        /// <summary>
        /// key is  member holder address
        /// </summary>
        public Dictionary<UInt160, MixMarkMember> MarkMembers { get; private set; } = new Dictionary<UInt160, MixMarkMember>();
        public MixMarkMember[] AllMarkMembers
        {
            get
            {
                return MarkMembers.Values.ToArray();
            }
        }

        public Dictionary<UInt160, Dictionary<UInt256, MarkEncodedBetOrderMix>> RecentOrders = new Dictionary<UInt160, Dictionary<UInt256, MarkEncodedBetOrderMix>>();
        public Dictionary<uint, Dictionary<UInt256, PortMessageMerge>> RecentPortMessages = new Dictionary<uint, Dictionary<UInt256, PortMessageMerge>>();
        public Dictionary<UInt256, TabletMessageMerge> TabletMessages = new Dictionary<UInt256, TabletMessageMerge>();
        public override void Rebuild()
        {
            this.MarkMembers.Clear();
            this.RecentOrders.Clear();
            this.RecentPortMessages.Clear();
            this.TabletMessages.Clear();
            base.Rebuild();
            this.LastBitSixBankerId = new LastBitSixBankerId { BitSixBankerId = 8000 };
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
                        this.OnMarkReplayTransaction(batch, context, block, rt, i);
                    }
                    else if (biztx is AskTransaction at)
                    {
                        this.OnMarkAskTransaction(batch, context, block, at, i, out n);
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
                    OnSlotSideTransaction(batch, block, st);
                }
                else if (tx is RangeTransaction rgt)
                {

                }
                //WatchBitMarkSixBanker(batch, block, tx);
            }
            this.Db.Write(WriteOptions.Default, batch);
        }

        public void OnMarkReplayTransaction(WriteBatch batch, BlockContext context, Block block, ReplyTransaction rt, ushort txindex)
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
                case (byte)CasinoType.MarkCipherTermClear:
                    if (rt.GetDataModel<MarkCipherTermClear>(bizshs, (byte)CasinoType.MarkCipherTermClear, out MarkCipherTermClear termClear))
                    {
                        foreach (var output in rt.Outputs)
                        {
                            if (output.AssetId == Blockchain.OXC)
                            {
                                UInt160 portHolder = default;
                                if (this.MarkMembers.TryGetValue(output.ScriptHash, out var playMember))
                                {
                                    var amt = output.Value.GetInternalValue() / Fixed8.D;
                                    playMember.TotalPrizeAmount += (ulong)amt;
                                    batch.Save_MarkMember(this, playMember);
                                    portHolder = playMember.Request.PortHolder;
                                    if (portHolder != output.ScriptHash)
                                    {
                                        if (this.MarkMembers.TryGetValue(playMember.Request.PortHolder, out var portMember))
                                        {
                                            portMember.TotalPrizeAmount += (ulong)amt;
                                            batch.Save_MarkMember(this, portMember);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;

            }

        }
        public void OnSlotSideTransaction(WriteBatch batch, Block block, SlotSideTransaction st)
        {
            if (st.VerifyRegMarkMember(out ECPoint bankerholderPubKey))
            {
                if (block.Index > 685163)
                {
                    if (st.VerifyRegBitSixBankerRequest(out RegMarkMemberRequest request))
                    {
                        byte feeKind = request.MemberType == MarkMemberType.Port ? CasinoSettingTypes.BasicPortDayFee : CasinoSettingTypes.BasicAgentDayFee;
                        var feesetting = this.MasterBlockIndex.GetSubBlockIndex<CasinoBlockIndex>().GetCasinoSetting(feeKind);
                        if (feesetting.IsNullOrEmpty()) return;

                        var fee = uint.Parse(feesetting);

                        if (st.VerifyRegMarkMemberFee(request, fee, out var days))
                        {
                            if (days > 0)
                            {
                                var rang = 60 * 60 * 24 * days;
                                var betSH = st.GetContract().ScriptHash;
                                var addr = Contract.CreateSignatureRedeemScript(bankerholderPubKey).ToScriptHash();
                                var nowTS = System.DateTime.Now.ToTimestamp();
                                if (!this.MarkMembers.TryGetValue(addr, out MixMarkMember member))
                                {
                                    this.LastBitSixBankerId.BitSixBankerId++;
                                    batch.Save_LastBitSixBankerId(this.LastBitSixBankerId);

                                    member = new MixMarkMember
                                    {
                                        MarkMemberId = this.LastBitSixBankerId.BitSixBankerId,
                                        BetAddress = betSH,
                                        PoolAddress = st.GetContractForOtherFlag(0x02, 0x01).ScriptHash,
                                        FeeAddress = st.GetContractForOtherFlag(0x02, 0x02).ScriptHash,
                                        PledgeAddress = st.GetContractForOtherFlag(0x02, 0x03).ScriptHash,
                                        DepositAddress = MarkBetAddressHelper.Instance.GetMemberDepositAddress(bankerholderPubKey, out _),
                                        Holder = addr,
                                        HolderPubkey = bankerholderPubKey,
                                        Request = request
                                    };

                                    if (request.Data.TryAsSerializable<MarkSetting>(out var rms) && rms.Verify())
                                        member.MarkSetting = rms;
                                    member.ExpireTimeStamp = nowTS + rang;
                                    batch.Save_MarkMember(this, member);
                                }
                                else
                                {
                                    member.Request = request;
                                    if (request.Data.TryAsSerializable<MarkSetting>(out var rms) && rms.Verify())
                                        member.MarkSetting = rms;
                                    member.ExpireTimeStamp = Math.Max(member.ExpireTimeStamp, nowTS) + rang;
                                    batch.Save_MarkMember(this, member);

                                }
                            }
                        }
                    }
                }
            }
        }
        public void OnMarkAskTransaction(WriteBatch batch, BlockContext context, Block block, AskTransaction at, ushort txindex, out ushort? n)
        {

            n = null;
            IReadOnlyDictionary<CoinReference, TransactionOutput> rfs = at.References;
            var shs = rfs.Values.GroupBy(m => m.ScriptHash).Select(n => n.Key.ToAddress());
            var bizshs = new UInt160[] { casino.CasinoMasterAccountAddress };

            switch (at.DataType)
            {
                case (byte)CasinoType.PortMessage:
                    if (at.GetDataModel<PortMessage>(bizshs, (byte)CasinoType.PortMessage, out PortMessage portMessage))
                    {
                        var portHolder = Contract.CreateSignatureRedeemScript(at.From).ToScriptHash();
                        if (this.MarkMembers.TryGetValue(portHolder, out var member))
                        {
                            PortMessageMerge messageMerge = new PortMessageMerge { Message = portMessage,  PortId = member.MarkMemberId, TimeStamp = block.Timestamp };
                            batch.Save_PortMessage(this, at.Hash, messageMerge);
                        }
                    }
                    break;
                case (byte)CasinoType.MarkCipherBet:
                    if (at.GetDataModel<MarkEncodedBetOrder>(bizshs, (byte)CasinoType.MarkCipherBet, out MarkEncodedBetOrder order))
                    {
                        var output = at.Outputs.FirstOrDefault(m => m.ScriptHash == MarkBetAddressHelper.Instance.GetMarkUnionBetAddress() && m.AssetId == Blockchain.OXC);
                        if (output.IsNotNull())
                        {
                            batch.Save_MarkOrder(this, at.Hash, new MarkEncodedBetOrderMix { Order = order, TimeStamp = block.Timestamp });
                            if (this.MarkMembers.TryGetValue(order.PortHolder, out var member))
                            {
                                if (output.IsNotNull())
                                {
                                    var amt = output.Value.GetInternalValue() / Fixed8.D;
                                    member.TotalBetAmount += (ulong)amt;
                                    batch.Save_MarkMember(this, member);
                                }
                            }
                            if (this.MarkMembers.TryGetValue(order.Player, out var playMember) && order.Player != order.PortHolder)
                            {
                                if (output.IsNotNull())
                                {
                                    var amt = output.Value.GetInternalValue() / Fixed8.D;
                                    playMember.TotalBetAmount += (ulong)amt;
                                    batch.Save_MarkMember(this, playMember);
                                }
                            }
                        }
                    }
                    break;
            }
        }

    }
}
