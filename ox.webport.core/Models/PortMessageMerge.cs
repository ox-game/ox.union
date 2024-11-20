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
    public class PortMessageMerge : ISerializable
    {
        public uint PortId;
        public PortMessage Message;
        public uint TimeStamp;

        public virtual int Size => sizeof(uint) + Message.Size + sizeof(uint);

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(PortId);
            writer.Write(Message);
            writer.Write(TimeStamp);
        }
        public void Deserialize(BinaryReader reader)
        {
            PortId = reader.ReadUInt32();
            Message = reader.ReadSerializable<PortMessage>();
            TimeStamp = reader.ReadUInt32();
        }
    }
    public class TabletMessageMerge : ISerializable
    {
        public TabletMessage Message;
        public uint TimeStamp;

        public virtual int Size => Message.Size + sizeof(uint);

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Message);
            writer.Write(TimeStamp);
        }
        public void Deserialize(BinaryReader reader)
        {
            Message = reader.ReadSerializable<TabletMessage>();
            TimeStamp = reader.ReadUInt32();
        }
    }
}
