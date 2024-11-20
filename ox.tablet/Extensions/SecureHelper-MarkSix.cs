using OX.BMS;
using OX.Casino;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OX.Tablet
{
    public static partial class SecureHelper
    {
        public static uint GetMarkSixOpenSeconds()
        {
            var casinoIndex = SecureHelper.BlockIndex.GetSubBlockIndex<CasinoBlockIndex>();
            var v = casinoIndex.GetCasinoSetting(CasinoSettingTypes.MarkSixOpenSeconds);
            if (v.IsNotNullAndEmpty())
            {
                return uint.Parse(v);
            }
            return 0;
        }
        public static Fixed8 GetPortMinDeposit()
        {
            var casinoIndex = SecureHelper.BlockIndex.GetSubBlockIndex<CasinoBlockIndex>();
            var v = casinoIndex.GetCasinoSetting(CasinoSettingTypes.PortMinBalance);
            if (v.IsNotNullAndEmpty())
            {
                return Fixed8.One* uint.Parse(v);
            }
            return Fixed8.Zero;
        }
        public static MixMarkMember GetMarkMember()
        {
            if (SecureHelper.BlockIndex.IsNull()) return default;
            var bmsIndex = SecureHelper.BlockIndex.GetSubBlockIndex<BMSBlockIndex>();
            if (bmsIndex.IsNull()) return default;
            if (!bmsIndex.MarkMembers.TryGetValue(SecureHelper.MasterAccount.ScriptHash, out var member)) return default;
            return member;
        }
        public static MixMarkMember GetMarkMember(UInt160 MemberHodler)
        {
            if (SecureHelper.BlockIndex.IsNull()) return default;
            var bmsIndex = SecureHelper.BlockIndex.GetSubBlockIndex<BMSBlockIndex>();
            if (bmsIndex.IsNull()) return default;
            if (!bmsIndex.MarkMembers.TryGetValue(MemberHodler, out var member)) return default;
            return member;
        }
        public static MixMarkMember GetMarkAdminMember()
        {
            if (SecureHelper.BlockIndex.IsNull()) return default;
            var bmsIndex = SecureHelper.BlockIndex.GetSubBlockIndex<BMSBlockIndex>();
            if (bmsIndex.IsNull()) return default;
            if (!bmsIndex.MarkMembers.TryGetValue(MarkBetAddressHelper.Instance.MarkAdmin, out var member)) return default;
            return member;
        }
        public static bool IsAgent(this MixMarkMember member)
        {
            if (member.IsNull()) return false;
            if (member.Request.MemberType != MarkMemberType.Agent) return false;
            return member.ExpireTimeStamp > DateTime.Now.ToTimestamp();
        }
        public static bool IsAgent()
        {
            var member = GetMarkMember();
            if (member.IsNull()) return false;
            return member.IsAgent();
        }

        public static bool IsPort(this MixMarkMember member)
        {
            if (member.IsNull()) return false;
            if (member.Request.MemberType != MarkMemberType.Port) return false;
            return member.ExpireTimeStamp > DateTime.Now.ToTimestamp();
        }
        public static bool IsPort()
        {
            var member = GetMarkMember();
            if (member.IsNull()) return false;
            return member.IsPort();
        }
        public static bool IsCasinoSlot()
        {
            return SecureHelper.MasterAccount.ScriptHash == casino.CasinoMasterAccountAddress;
        }
    }
}
