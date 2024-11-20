using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OX.IO;

namespace OX.WebPort
{
    public class AssetBalanceState
    {
        public UInt256 AssetId;
        public string AssetName;
        public Fixed8 MasterBalance = Fixed8.Zero;
        public Fixed8 AvailableBalance = Fixed8.Zero;
        public Fixed8 TotalBalance = Fixed8.Zero;
        public OutputMix[] OMS = new OutputMix[0];
    }
    public class ApiTransactionMessage : ISerializable
    {
        public byte TxKind;
        public byte[] Data;
        public virtual int Size => sizeof(byte) + Data.GetVarSize();

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(TxKind);
            writer.WriteVarBytes(Data);
        }
        public void Deserialize(BinaryReader reader)
        {
            TxKind = reader.ReadByte();
            Data = reader.ReadVarBytes();
        }
    }
}
