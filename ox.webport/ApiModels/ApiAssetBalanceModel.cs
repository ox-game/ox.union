using OX.IO;
using OX.Network.P2P.Payloads;
using OX.SmartContract;
using OX.Wallets;

namespace OX.Mix.ApiModels
{
    public class ApiMarkMemberModel
    {
        public string MarkMemberId;
        public string MarkMemberHolder;
        public string MarkMemberPubKey;
        public string PortMemberId;
        public string PortHolder;
        public string PortHolderPubKey;
        public string ExpireTimeStamp;
        public string TotalBetAmount;
        public string TotalPrizeAmount;
        public string MemberType;
        public ApiMarkMemberModel()
        {
            this.MarkMemberId = "0";
            this.MarkMemberHolder = UInt160.Zero.ToString();
            this.MarkMemberPubKey = "0";
            this.PortMemberId = "0";
            this.PortHolder = UInt160.Zero.ToString();
            this.PortHolderPubKey = "0";
            this.ExpireTimeStamp = "0";
            this.TotalBetAmount = "0";
            this.TotalPrizeAmount = "0";
            this.MemberType = "0";
        }
    }
    public class ApiAssetBalanceModel
    {
        public string AssetId;
        public string AssetName;
        public string MasterBalance;
        public string AvailableBalance;
        public string TotalBalance;
        public string Price;
        public string IDOPrice;
        public string IDOEndIndex;
        public string CurrentIndex;
        public string MarkMemberId;
        public string PortMemberId;
        public string PortHolder;

        public string MinerType;
        public string RootSeedAddress;
        public string ParentHolder;
        public string ParentMinerMemberId;
        public string IsEthMap;
        public ApiAssetBalanceOutput[] Outputs = new ApiAssetBalanceOutput[0];
        public ApiAssetBalanceModel()
        {
            this.AssetId = string.Empty;
            this.AssetName = string.Empty;
            this.MasterBalance = "0";
            this.AvailableBalance = "0";
            this.TotalBalance = "0";
            this.Price = "0";
            this.IDOPrice = "0";
            this.IDOEndIndex = "0";
            this.CurrentIndex = "0";
            this.MarkMemberId = "0";
            this.PortMemberId = "0";
            this.PortHolder = UInt160.Zero.ToString();
            this.MinerType = "-1";
            this.RootSeedAddress = UInt160.Zero.ToString();
            this.ParentHolder = UInt160.Zero.ToString();
            this.ParentMinerMemberId = "0";
            this.IsEthMap = "False";
        }
    }
    public class ApiAssetUtxoModel
    {
        public string AssetId;
        public string AssetName;
        public string MasterBalance;
        public string AvailableBalance;
        public string TotalBalance;
        public string CurrentIndex;
        public ApiAssetBalanceOutput[] Outputs = new ApiAssetBalanceOutput[0];
        public ApiAssetUtxoModel()
        {
            this.AssetId = string.Empty;
            this.AssetName = string.Empty;
            this.MasterBalance = "0";
            this.AvailableBalance = "0";
            this.TotalBalance = "0";
            this.CurrentIndex = "0";
        }
    }
    public class ApiAssetBalanceOutput
    {
        public string IsLockCoin;
        public string IsTimeLock;
        public string LockExpirationIndex;
        public string Amount;
        public string TxId;
        public string N;
        public string TimeStamp;
        public string FromHolder;
        public string FromMemberId="0";
    }
    public class ApiClaimModel
    {
        public string SelectedAmount = "0";
        public string AvailableAmount = "0";
        public string UnavailableAmount = "0";
        public string CurrentIndex;
        public CoinReferenceWrapper[] Claims = new CoinReferenceWrapper[0];
        public string[] Contracts = new string[0];
    }
    public class CoinReferenceWrapper
    {
        public string PrevHash;
        public string PrevIndex;
    }
    public class ContractWrapper : ISerializable
    {
        public byte[] Script;
        public byte[] ParameterList;

        public virtual int Size => Script.GetVarSize() + ParameterList.GetVarSize();
        public void Serialize(BinaryWriter writer)
        {
            writer.WriteVarBytes(Script);
            writer.WriteVarBytes(ParameterList);
        }
        public void Deserialize(BinaryReader reader)
        {
            Script = reader.ReadVarBytes();
            ParameterList = reader.ReadVarBytes();
        }

    }
}
