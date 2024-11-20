using OX.IO;
using OX.Network.P2P.Payloads;
using OX.SmartContract;
using OX.Wallets;

namespace OX.Mix.ApiModels
{
    public class ApiDirectSaleOrderSetModel
    {
        public ApiDirectSaleOrderModel[] SellOrders;
        public ApiDirectSaleOrderModel[] BuyOrders;
        public ApiDirectSaleOrderSetModel()
        {
            this.SellOrders = new ApiDirectSaleOrderModel[0];
            this.BuyOrders = new ApiDirectSaleOrderModel[0];
        }
    }
    public class ApiDirectSaleOrderModel
    {
        public string LockAddress;
        public string AssetId;
        public string Amount;
        public string Seller;
        public string Buyer;
        public string RedeemDeadline;
        public string IsLock;
        public string TimeStamp;
        public string ApproveHash;
        public string MLSTTxId;
        public string MLSTN;
        public string MLBTTxId;
        public string MLBTN;
        public ApiDirectSaleOrderModel()
        {
            this.LockAddress = "0";
            this.AssetId = "0";
            this.Amount = "0";
            this.Seller = "0";
            this.Buyer = "0";
            this.RedeemDeadline = "0";
            this.IsLock = "False";
            this.TimeStamp = "0";
            this.ApproveHash = "0";
            this.MLSTTxId = "0";
            this.MLBTTxId = "0";
            this.MLSTN = "0";
            this.MLBTN = "0";
        }
    }

}
