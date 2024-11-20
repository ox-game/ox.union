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
using OX.Cryptography;
using OX.Tablet.FlashMessages;

namespace OX.Tablet
{
    public class FlashBlockIndex : BaseSubBlockIndex
    {
        public const string IndexKey = "Data_FlashIndex";
        public override bool CanRebuild => false;
        public Dictionary<UInt256, UnicastTalkLineValue> UnicastTalkLines = new Dictionary<UInt256, UnicastTalkLineValue>();
        public Dictionary<UInt256, MulticastTalkLineValue> MulticastTalkLines = new Dictionary<UInt256, MulticastTalkLineValue>();
        public Dictionary<UInt256, UInt32Wrapper> TalkCount = new Dictionary<UInt256, UInt32Wrapper>();
        public FlashBlockIndex(BlockIndex blockIndex, OXAccount oxAccount, EthAccount ethAccount) : base(blockIndex, oxAccount, ethAccount, IndexKey)
        {
            UnicastTalkLines = new Dictionary<UInt256, UnicastTalkLineValue>(this.GetAll<UInt256, UnicastTalkLineValue>(FlashStatePersistencePrefixes.FlashUnicast_TalkLine));
            MulticastTalkLines = new Dictionary<UInt256, MulticastTalkLineValue>(this.GetAll<UInt256, MulticastTalkLineValue>(FlashStatePersistencePrefixes.FlashMulticast_TalkLine));
            TalkCount = new Dictionary<UInt256, UInt32Wrapper>(this.GetAll<UInt256, UInt32Wrapper>(FlashStatePersistencePrefixes.FlashTalk_Count));
        }

        public override void Rebuild()
        {
            base.Rebuild();
        }
        public override void HandleBlock(Block block)
        {

        }
        public void Do(Action<WriteBatch> action)
        {
            WriteBatch batch = new WriteBatch();
            if (action != default)
                action(batch);
            this.Db.Write(WriteOptions.Default, batch);
        }
        public void OnFlashMessage(FlashMessage flashMessage)
        {
            if (!FlashMemoryHelper.FlashHashs.Contains(flashMessage.Hash))
            {
                FlashMemoryHelper.FlashHashs.Enqueue(flashMessage.Hash);
                switch (flashMessage.Type)
                {
                    case FlashMessageType.FlashUnicast:
                        FlashUnicast fu = flashMessage as FlashUnicast;
                        if (fu.IsNotNull())
                            Do(wb =>
                            {
                                if (SecureHelper.MasterAccount.Key.PublicKey.Equals(fu.Sender))
                                {
                                    wb.Save_FlashUnicast(this, SecureHelper.MasterAccount.ScriptHash, fu, TalkKind.OutBox);
                                }
                                else if (fu.RecipientHash == SecureHelper.MasterAccount.ScriptHash.Hash)
                                {
                                    wb.Save_FlashUnicast(this, SecureHelper.MasterAccount.ScriptHash, fu, TalkKind.Inbox);
                                }
                            });
                        break;
                    case FlashMessageType.FlashMulticast:
                        FlashMulticast fm = flashMessage as FlashMulticast;
                        if (fm.IsNotNull())
                            Do(wb =>
                            {
                                if (fm.TalkLine == MarkBetAddressHelper.Instance.NoticeTalkLine && fm.Sender.Equals(MarkBetAddressHelper.Instance.MarkAdminPublicKey))
                                {
                                    wb.Save_FlashMulticast(this, fm, TalkKind.Inbox);
                                }
                            });
                        break;


                }
            }
        }
        public IEnumerable<KeyValuePair<TalkLineKey, FlashUnicastRecord>> GetLastUnicastRecords(UInt256 talkLine, out uint range)
        {
            range = 0;
            if (this.TalkCount.TryGetValue(talkLine, out UInt32Wrapper w))
            {
                range = (uint)w / FlashMemoryHelper.RangeSize;
                return GetUnicastRecords(talkLine, range);
            }
            return default;
        }
        public IEnumerable<KeyValuePair<TalkLineKey, FlashUnicastRecord>> GetUnicastRecords(UInt256 talkLine, uint range)
        {
            var builder = SliceBuilder.Begin().Add(talkLine).Add(range);
            return this.GetAll<TalkLineKey, FlashUnicastRecord>(FlashStatePersistencePrefixes.FlashUnicast_Record, builder.ToArray());
        }
        public IEnumerable<KeyValuePair<TalkLineKey, FlashMulticastRecord>> GetLastMulticastRecords(UInt256 talkLine, out uint range)
        {
            range = 0;
            if (this.TalkCount.TryGetValue(talkLine, out UInt32Wrapper w))
            {
                range = (uint)w / FlashMemoryHelper.RangeSize;
                return GetMulticastRecords(talkLine, range);
            }
            return default;
        }
        public IEnumerable<KeyValuePair<TalkLineKey, FlashMulticastRecord>> GetMulticastRecords(UInt256 talkLine, uint range)
        {
            var builder = SliceBuilder.Begin().Add(talkLine).Add(range);
            return this.GetAll<TalkLineKey, FlashMulticastRecord>(FlashStatePersistencePrefixes.FlashMulticast_Record, builder.ToArray());
        }
    }
}
