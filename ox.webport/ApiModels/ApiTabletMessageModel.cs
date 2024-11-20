using OX.IO;
using OX.Network.P2P.Payloads;
using OX.SmartContract;
using OX.Wallets;

namespace OX.Mix.ApiModels
{
    public class ApiTabletMessageModel
    {
        public string Msg;
        public string TimeStamp;
        public ApiTabletMessageModel()
        {
            this.Msg = "0";
            this.TimeStamp = "0";
        }
    }
    public class ApiPortMessageModel
    {
        public string PortIName;
        public string Msg;
        public string TimeStamp;
        public ApiPortMessageModel()
        {
            this.PortIName = "0";
            this.Msg = "0";
            this.TimeStamp = "0";
        }
    }
}
