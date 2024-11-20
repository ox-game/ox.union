using OX.IO;
using OX.Network.P2P.Payloads;
using OX.SmartContract;
using OX.Wallets;

namespace OX.Mix.ApiModels
{
    public class ApiDirectSalePublishModel
    {
        public string Seller;
        public string AssetId;
        public string Contact;
        public string Remarks;
        public string TimeStamp;
        public string N;
        public ApiDirectSalePublishModel()
        {
            this.Seller = "0";
            this.AssetId = "0";
            this.Contact = "0";
            this.Remarks = "0";
            this.TimeStamp = "0";
            this.N = "0";
        }
    }
  
}
