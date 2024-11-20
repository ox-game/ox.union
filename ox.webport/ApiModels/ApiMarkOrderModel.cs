using OX.IO;
using OX.Network.P2P.Payloads;
using OX.SmartContract;
using OX.Wallets;

namespace OX.Mix.ApiModels
{
    public class ApiMarkOrderModel
    {
        public string Term;
        public string ChannelRound;
        public string TimeStamp;
        public string EncodedHex;
        public ApiMarkOrderModel()
        {
            this.Term = "0";
            this.ChannelRound = "0-0";
            this.TimeStamp = "0";
            this.EncodedHex = "0";
        }
    }
  
}
