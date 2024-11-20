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

namespace OX.Tablet
{
    public static partial class CasinoPersistenceHelper
    {
        public static void Save_Web3Node(this WriteBatch batch, BMSBlockIndex provider, Block block, ReplyTransaction rt, Web3NodeSet web3NodeSet)
        {
            if (web3NodeSet.IsNotNull() && web3NodeSet.Nodes.IsNotNullAndEmpty())
            {
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
        public static void Save_MarkMember(this WriteBatch batch, BMSBlockIndex provider, MixMarkMember member)
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

        public static void Save_GuessAnswer(this WriteBatch batch, BMSBlockIndex provider, GuessAnswerReply guessAnswerReply)
        {
            foreach (var answer in guessAnswerReply.Answers)
            {
                batch.Put(SliceBuilder.Begin(CasinoBizPersistencePrefixes.BMS_GuessAnswer).Add(answer.Key), SliceBuilder.Begin().Add(answer.Value));
                provider.GuessAnswers[answer.Key.ToString()] = new GuessAnswer { Key = answer.Key, Value = answer.Value };
            }
        }
    }
}
