using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using OX.IO;
using OX.Network.P2P.Payloads;
using System.Runtime.CompilerServices;
using OX.Ledger;
using OX.Casino;
using OX.SmartContract;
using OX.Cryptography.ECC;

namespace OX.WebPort
{

    public static class BlockchainHelper
    {
        static uint _lastTimestamp = 0;
        static uint _lastIndex = 0;
        public static uint LastIndex
        {
            get
            {
                var ts = System.DateTime.Now.ToTimestamp();
                if(ts>_lastTimestamp+15)
                {
                    _lastTimestamp = ts;
                    _lastIndex = Blockchain.Singleton.HeaderHeight;
                }
                return _lastIndex;
            }
        }
    }
}
