namespace OX.Tablet
{
    public class EthID
    {
        public string EthAddress { get; set; }
        public UInt160 MapAddress { get; private set; }
        public uint AddressID { get; private set; }
        public EthID(string ethAddress)
        {
            EthAddress = ethAddress;
            MapAddress = EthAddress.BuildMapAddress();
            AddressID = MapAddress.BuildAddressId();
        }

        public override bool Equals(object obj)
        {
            if (obj is EthID ethid)
            {
                return ethid.EthAddress.ToLower() == this.EthAddress.ToLower();
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return this.EthAddress.ToLower().GetHashCode();
        }
       
    }
}
