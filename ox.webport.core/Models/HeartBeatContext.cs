using OX.Network.P2P.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OX.WebPort
{
    public class HeartBeatContext
    {
        public static uint BaseTimeStamp { get; private set; }
        public uint TimeStamp { get; private set; }
        public bool IsNormalSync { get; set; }
        public bool BalanceChanged { get; set; }
        public uint BalanceChangedLastIndex { get; set; }
        static HeartBeatContext()
        {
            BaseTimeStamp = DateTime.Now.ToTimestamp();
        }

        public HeartBeatContext()
        {
            TimeStamp = DateTime.Now.ToTimestamp();
        }
        public bool IsOnceHalfMinute
        {
            get
            {
                return ((TimeStamp - BaseTimeStamp) % 30 == 0);
            }
        }
        public bool IsOnceMinute
        {
            get
            {
                return ((TimeStamp - BaseTimeStamp) % 60 == 0);
            }
        }
        public bool Is10Minutes
        {
            get
            {
                return ((TimeStamp - BaseTimeStamp) % 600 == 0);
            }
        }
        public bool IsOnceHour
        {
            get
            {
                return ((TimeStamp - BaseTimeStamp) % 3600 == 0);
            }
        }
        public bool IsOnceDay
        {
            get
            {
                return ((TimeStamp - BaseTimeStamp) % 3600 * 12 == 0);
            }
        }
    }
    public interface IBlockchainHandler
    {
        void HeartBeat(HeartBeatContext beatContext);
        void BeforeBlock(Block block);
        void OnBlock(Block block);
        void AfterBlock(Block block);       
    }
    public interface IBlockchainModule : IBlockchainHandler
    {
        string ModuleKey { get; }

    }
}
