namespace OX.WebPort
{
    public static class MiningPersistencePrefixes
    {
        public const byte Exchange_Pair = 0x01;
        public const byte Exchange_Pair_State = 0x02;
        public const byte Exchange_Pair_Record_Last = 0x03;
        public const byte Exchange_IDO_Record = 0x04;
        public const byte OTC_Dealer = 0x05;
        public const byte Invest_Setting = 0x06;
        public const byte AMI_USDTCastRecord = 0x07;
        public const byte AMI_USDTDestroyRecord = 0x08;
        public const byte MutualLockNode = 0x09;
        public const byte MutualLockMiningAssetReply = 0x0A;
        public const byte LockMiningAssetReply = 0x0B;
        public const byte LockMiningRecords = 0x0C;
        public const byte LockMiningOXSTotal = 0x0D;
        public const byte LockMiningInterestRecords = 0x0E;
        public const byte MutualLockRecords = 0x0F;
        public const byte MutualLockInterestRecords = 0x10;
        public const byte TotalMutualLockSpaceTimeLockVolume = 0x11;
        public const byte MarkMiningCount = 0x12;
    }

}
