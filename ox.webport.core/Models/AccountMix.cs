using OX.Network.P2P.Payloads;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OX.IO;

namespace OX.WebPort
{
    public class AccountMix : ISerializable
    {
        public UInt160 Holder;
        public UInt160 Address;

        public virtual int Size => Holder.Size+Address.Size;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Holder);
            writer.Write(Address);
        }
        public void Deserialize(BinaryReader reader)
        {
            Holder = reader.ReadSerializable<UInt160>();
            Address = reader.ReadSerializable<UInt160>();
        }
    }
}
