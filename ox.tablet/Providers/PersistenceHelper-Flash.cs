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
using OX.Bapps;
using OX.IO.Wrappers;
using OX.Wallets;
using OX.Tablet.FlashMessages;
using OX.Persistence;

namespace OX.Tablet
{
    public static partial class FlashPersistenceHelper
    {
        public static void Save_FlashUnicast(this WriteBatch batch, FlashBlockIndex provider, UInt160 localAddress, FlashUnicast fu, TalkKind TalkKind)
        {
            if (fu.IsNotNull())
            {
                if (!provider.TalkCount.TryGetValue(fu.TalkLine, out UInt32Wrapper count))
                {
                    count = (UInt32Wrapper)0;
                    provider.TalkCount[fu.TalkLine] = count;
                }
                count = count + 1;
                provider.TalkCount[fu.TalkLine] = count;
                batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashTalk_Count).Add(fu.TalkLine), SliceBuilder.Begin().Add(count.ToArray()));

                TalkLineKey tlk = new TalkLineKey { FMHash = fu.Hash, Range = (uint)count / FlashMemoryHelper.RangeSize, TalkKind = TalkKind, TalkLine = fu.TalkLine };
                var fur = new FlashUnicastRecord { FlashUnicast = fu, Timestamp = DateTime.Now.ToTimestamp(), RecordIndex = (uint)count };
                batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashUnicast_Record).Add(tlk), SliceBuilder.Begin().Add(fur));
                FlashMemoryHelper.UnicastQueue.Enqueue(new Tuple<TalkLineKey, FlashUnicastRecord>(tlk, fur));
                if (TalkKind == TalkKind.Inbox)
                {
                    if (!provider.UnicastTalkLines.TryGetValue(fu.TalkLine, out UnicastTalkLineValue utlv))
                    {
                        var label = string.Empty;

                        var senderSH = Contract.CreateSignatureRedeemScript(fu.Sender).ToScriptHash();
                        var dmbs = FlashMessageHelper.GetDomain(senderSH);
                        if (dmbs.IsNotNullAndEmpty())
                        {
                            label = System.Text.Encoding.UTF8.GetString(dmbs);
                        }

                        utlv = new UnicastTalkLineValue { Local = localAddress, Remote = fu.Sender, Timestamp = DateTime.Now.ToTimestamp(), Label = label };
                        batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashUnicast_TalkLine).Add(fu.TalkLine), SliceBuilder.Begin().Add(utlv));
                        provider.UnicastTalkLines[fu.TalkLine] = utlv;
                    }
                    else
                    {
                        utlv.Timestamp = DateTime.Now.ToTimestamp();
                        batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashUnicast_TalkLine).Add(fu.TalkLine), SliceBuilder.Begin().Add(utlv));
                    }
                }
            }
        }
        public static void Save_FlashMulticast(this WriteBatch batch, FlashBlockIndex provider, FlashMulticast fm, TalkKind TalkKind)
        {
            if (fm.IsNotNull())
            {
                if (!provider.TalkCount.TryGetValue(fm.TalkLine, out UInt32Wrapper count))
                {
                    count = (UInt32Wrapper)0;
                    provider.TalkCount[fm.TalkLine] = count;
                }
                count = count + 1;
                provider.TalkCount[fm.TalkLine] = count;
                batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashTalk_Count).Add(fm.TalkLine), SliceBuilder.Begin().Add(count.ToArray()));

                TalkLineKey tlk = new TalkLineKey { FMHash = fm.Hash, Range = (uint)count / FlashMemoryHelper.RangeSize, TalkKind = TalkKind, TalkLine = fm.TalkLine };
                var fmr = new FlashMulticastRecord { FlashUnicast = fm, Timestamp = DateTime.Now.ToTimestamp(), RecordIndex = (uint)count };
                batch.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashMulticast_Record).Add(tlk), SliceBuilder.Begin().Add(fmr));
                FlashMemoryHelper.MulticastQueue.Enqueue(new Tuple<TalkLineKey, FlashMulticastRecord>(tlk, fmr));
            }
        }
    }
}
