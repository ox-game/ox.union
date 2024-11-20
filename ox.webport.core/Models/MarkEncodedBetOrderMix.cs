using OX.BMS;
using OX.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OX.WebPort.Models
{
    public class MarkEncodedBetOrderMix : ISerializable
    {
        public MarkEncodedBetOrder Order;
        public uint TimeStamp;

        public virtual int Size => Order.Size + sizeof(uint);

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Order);
            writer.Write(TimeStamp);
        }
        public void Deserialize(BinaryReader reader)
        {
            Order = reader.ReadSerializable<MarkEncodedBetOrder>();
            TimeStamp = reader.ReadUInt32();
        }
    }
}
