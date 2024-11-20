using OX.DirectSales;
using OX.IO;
using System.IO;

namespace OX.WeChat
{
    public class TextInputArray : ISerializable
    {
        public TextInput[] Inputs;
        public virtual int Size => Inputs.GetVarSize();


        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Inputs);
        }
        public void Deserialize(BinaryReader reader)
        {
            Inputs = reader.ReadSerializableArray<TextInput>();
        }
    }
    public class TextInput : ISerializable
    {
        public string Text;
        public string FromName;
        public virtual int Size => Text.GetVarSize() + FromName.GetVarSize();


        public void Serialize(BinaryWriter writer)
        {
            writer.WriteVarString(Text);
            writer.WriteVarString(FromName);
        }
        public void Deserialize(BinaryReader reader)
        {
            Text = reader.ReadVarString();
            FromName = reader.ReadVarString();
        }
    }
     
}
