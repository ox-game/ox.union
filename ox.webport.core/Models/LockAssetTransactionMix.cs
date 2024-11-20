using OX.Network.P2P.Payloads;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OX.IO;
using Org.BouncyCastle.Asn1.X509;
using OX.BMS;

namespace OX.WebPort
{
    public class LockAssetTransactionMix : ISerializable
    {
        public UInt160 Holder;
        public uint LastIndex;
        public LockAssetTransaction LockAssetTransaction;
        public virtual int Size => Holder.Size + sizeof(uint) + sizeof(uint) + (LockAssetTransaction.IsNotNull() ? LockAssetTransaction.Size : 0);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Holder);
            writer.Write(LastIndex);
            if(LockAssetTransaction.IsNotNull())
            {
                writer.Write((uint)LockAssetTransaction.Size);
                writer.Write(LockAssetTransaction);
            }
            else
            {
                writer.Write((uint)0);
            }
           
        }
        public void Deserialize(BinaryReader reader)
        {
            Holder = reader.ReadSerializable<UInt160>();
            LastIndex = reader.ReadUInt32();
            uint d = reader.ReadUInt32();
            if (d > 0)
            {
                LockAssetTransaction = reader.ReadSerializable<LockAssetTransaction>();
            }            
        }
    }
}
