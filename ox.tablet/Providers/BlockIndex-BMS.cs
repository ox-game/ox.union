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
using static OX.BMS.MarkPortPlayerTermKey;

namespace OX.Tablet
{
    public class BMSBlockIndex : BaseSubBlockIndex
    {
        public const string IndexKey = "Data_BMSIndex";
        public override bool CanRebuild => true;
        public BMSBlockIndex(BlockIndex blockIndex, OXAccount oxAccount, EthAccount ethAccount) : base(blockIndex, oxAccount, ethAccount, IndexKey)
        {
            var LBS = this.Get<LastBitSixBankerId>(CasinoBizPersistencePrefixes.BMS_Last_BankerId, casino.CasinoMasterAccountAddress);
            if (LBS.IsNull())
            {
                LBS = new LastBitSixBankerId { BitSixBankerId = 8000 };
            }
            this.LastBitSixBankerId = LBS;
            this.MarkMembers = new Dictionary<UInt160, MixMarkMember>(this.GetAll<UInt160, MixMarkMember>(CasinoBizPersistencePrefixes.Mark_Member));



            foreach (var ga in this.GetAll<GuessAnswerKey, GuessAnswerValue>(CasinoBizPersistencePrefixes.BMS_GuessAnswer))
            {
                this.GuessAnswers[ga.Key.ToString()] = new GuessAnswer { Key = ga.Key, Value = ga.Value };
            }
            foreach (var ga in this.GetAll<Web3Node, UInt32Wrapper>(CasinoBizPersistencePrefixes.Casino_Web3Node_Publish).GroupBy(m => m.Key.Catalog))
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

        public Dictionary<string, GuessAnswer> GuessAnswers { get; private set; } = new Dictionary<string, GuessAnswer>();
        public Dictionary<ushort, Dictionary<string, uint>> Web3Nodes = new Dictionary<ushort, Dictionary<string, uint>>();
        public override void Rebuild()
        {
            this.MarkMembers.Clear();
            this.GuessAnswers.Clear();
            this.Web3Nodes.Clear();
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

                                var portOk = portHolder == SecureHelper.MasterAccount.ScriptHash;
                                var agentOk = output.ScriptHash == SecureHelper.MasterAccount.ScriptHash;
                                if (portOk || agentOk)
                                {
                                    MarkPortPlayerTermKey key = new MarkPortPlayerTermKey { Term = termClear.Term, PortHolder = portHolder, Player = output.ScriptHash };
                                    var recordValue = this.Get<MarkPortPlayerTermValue>(CasinoBizPersistencePrefixes.BMS_PortPlayerTerm_Record, key);
                                    if (recordValue.IsNull())
                                    {
                                        recordValue = new MarkPortPlayerTermValue();
                                    }
                                    recordValue.TotalPrizeAmount += output.Value;
                                    batch.Put(SliceBuilder.Begin(CasinoBizPersistencePrefixes.BMS_PortPlayerTerm_Record).Add(key), SliceBuilder.Begin().Add(recordValue));
                                }
                            }
                        }
                    }
                    break;
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
                        if (at.From.Equals(this.OXAccount.Key.PublicKey))
                        {
                            PortMessageMix message = new PortMessageMix { Message = portMessage, BlockIndex = block.Index, TimeStamp = block.Timestamp };
                            batch.Put(SliceBuilder.Begin(CasinoBizPersistencePrefixes.BMS_Port_Message).Add(at.Hash), SliceBuilder.Begin().Add(message));
                        }                        
                    }
                    break;
                case (byte)CasinoType.MarkPortPayFee:
                    if (at.GetDataModel<MarkPortPlayerTermKey>(bizshs, (byte)CasinoType.MarkPortPayFee, out MarkPortPlayerTermKey mpptk))
                    {
                        var output = at.Outputs.FirstOrDefault(m => m.AssetId == Blockchain.OXC && m.ScriptHash == mpptk.Player);
                        if (output.IsNotNull())
                        {
                            var recordValue = this.Get<MarkPortPlayerTermValue>(CasinoBizPersistencePrefixes.BMS_PortPlayerTerm_Record, mpptk);
                            if (recordValue.IsNotNull())
                            {
                                recordValue.FeeAmount += output.Value;
                                batch.Put(SliceBuilder.Begin(CasinoBizPersistencePrefixes.BMS_PortPlayerTerm_Record).Add(mpptk), SliceBuilder.Begin().Add(recordValue));
                            }
                        }
                    }
                    break;
                case (byte)CasinoType.MarkCipherBet:
                    if (at.GetDataModel<MarkEncodedBetOrder>(bizshs, (byte)CasinoType.MarkCipherBet, out MarkEncodedBetOrder order))
                    {
                        var output = at.Outputs.FirstOrDefault(m => m.ScriptHash == MarkBetAddressHelper.Instance.GetMarkUnionBetAddress() && m.AssetId == Blockchain.OXC);
                        if (output.IsNotNull())
                        {
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
                            var portOk = order.PortHolder == SecureHelper.MasterAccount.ScriptHash;
                            var agentOk = order.Player == SecureHelper.MasterAccount.ScriptHash;
                            if (portOk || agentOk)
                            {
                                MarkPortPlayerTermKey key = new MarkPortPlayerTermKey { Term = order.Term, PortHolder = order.PortHolder, Player = order.Player };
                                var recordValue = this.Get<MarkPortPlayerTermValue>(CasinoBizPersistencePrefixes.BMS_PortPlayerTerm_Record, key);
                                if (recordValue.IsNull())
                                {
                                    recordValue = new MarkPortPlayerTermValue();
                                }
                                recordValue.TotalBetAmount += output.Value;
                                batch.Put(SliceBuilder.Begin(CasinoBizPersistencePrefixes.BMS_PortPlayerTerm_Record).Add(key), SliceBuilder.Begin().Add(recordValue));
                            }
                        }
                    }
                    break;
            }
        }
        public IEnumerable<KeyValuePair<MarkPortPlayerTermKey, MarkPortPlayerTermValue>> MarkPortPlayerTermRecords(MarkTerm Term, UInt160 PortHolder, UInt160 Player = default)
        {
            var builder = SliceBuilder.Begin(CasinoBizPersistencePrefixes.BMS_PortPlayerTerm_Record).Add(Term).Add(PortHolder);
            if (Player.IsNotNull())
            {
                builder = builder.Add(Player);
            }
            return this.Db.Find(ReadOptions.Default, builder, (k, v) =>
            {
                var ks = k.ToArray();
                var length = ks.Length - sizeof(byte);
                ks = ks.TakeLast(length).ToArray();
                byte[] data = v.ToArray();
                return new KeyValuePair<MarkPortPlayerTermKey, MarkPortPlayerTermValue>(ks.AsSerializable<MarkPortPlayerTermKey>(), data.AsSerializable<MarkPortPlayerTermValue>());
            });
        }
        public IEnumerable<KeyValuePair<MarkPortPlayerTermKey, MarkPortPlayerTermValue>> MarTermAllRecords(MarkTerm Term)
        {
            var builder = SliceBuilder.Begin(CasinoBizPersistencePrefixes.BMS_PortPlayerTerm_Record).Add(Term);

            return this.Db.Find(ReadOptions.Default, builder, (k, v) =>
            {
                var ks = k.ToArray();
                var length = ks.Length - sizeof(byte);
                ks = ks.TakeLast(length).ToArray();
                byte[] data = v.ToArray();
                return new KeyValuePair<MarkPortPlayerTermKey, MarkPortPlayerTermValue>(ks.AsSerializable<MarkPortPlayerTermKey>(), data.AsSerializable<MarkPortPlayerTermValue>());
            });
        }
    }
}
