using OX.BMS;
using OX.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OX.WebPort
{
    public class TabletMessageMerge : ISerializable
    {
        public uint TimeStamp;
        public TabletMessage Message;
        public virtual int Size => sizeof(uint) + Message.Size;
        public void Deserialize(BinaryReader reader)
        {
            TimeStamp = reader.ReadUInt32();
            Message = reader.ReadSerializable<TabletMessage>();
        }
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(TimeStamp);
            writer.Write(Message);
        }
    }
}
