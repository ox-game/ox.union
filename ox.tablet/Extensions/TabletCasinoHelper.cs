using OX.Casino;
using OX.Ledger;
using System.Collections.Generic;
using System.Linq;

namespace OX.Tablet
{
    public static class TabletCasinoHelper
    {
        public static ulong GetMineNonce(uint blockIndex)
        {
            var hash = Blockchain.Singleton.GetBlockHash(blockIndex);
            if (hash.IsNotNull())
            {
                var block = Blockchain.Singleton.GetBlock(hash);
                if (block.IsNotNull())
                {
                    return block.ConsensusData;
                }
            }
            return 0;
        }
        public static bool VerifyPartnerLock(this CasinoBlockIndex provider, MixRoom room, out IEnumerable<RoomPartnerLockRecord> validRecords, out Fixed8 haveLockTotal, out Fixed8 needLockTotal, out uint EarliestExpiration)
        {
            validRecords = default;
            haveLockTotal = Fixed8.Zero;
            needLockTotal = Fixed8.Zero;
            EarliestExpiration = 0;
           
 
            var privateV = provider.GetCasinoSetting(CasinoSettingTypes.PrivateRoomOXSLockVolume);
            if (privateV.IsNullOrEmpty()) return false;
            var PrivateRoomOXSLockAmount = Fixed8.FromDecimal(decimal.Parse(privateV));


            var publicV = provider.GetCasinoSetting(CasinoSettingTypes.PublicRoomOXSLockVolume);
            if (publicV.IsNullOrEmpty()) return false;
            var PublicRoomOXSLockAmount = Fixed8.FromDecimal(decimal.Parse(publicV));
 
            Fixed8 TotalLockVolume = Fixed8.Zero;
            if (room.Request.Permission == RoomPermission.Public)
            {
                TotalLockVolume = PublicRoomOXSLockAmount;
            }
            else
            {
                TotalLockVolume = PrivateRoomOXSLockAmount;
            }
            needLockTotal = TotalLockVolume;
            Fixed8 trs = Fixed8.Zero;
            var lrs = provider.GetRoomPartnerLockRecords(room.BetAddress);
            if (lrs.IsNotNullAndEmpty())
            {
                trs = lrs.Select(m => m.Key).Calculate(Blockchain.Singleton.HeaderHeight, out validRecords);
                if (validRecords.IsNotNullAndEmpty()) EarliestExpiration = validRecords.OrderBy(m => m.EndIndex).FirstOrDefault().EndIndex;
            }
            haveLockTotal = trs;
            return trs >= TotalLockVolume;
        }
    }
}
