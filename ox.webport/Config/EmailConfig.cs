using OX.IO.Json;
using OX.Wallets;
using OX.IO;
using System.IO;

namespace OX.WebPort.Config
{
    public class EmailConfig : ISerializable
    {
        public string SmtpServer;
        public string SmtpPort;
        public string Pop3Server;
        public string Pop3Port;
        public string UserName;
        public string Pwd;
        public virtual int Size => SmtpServer.GetVarSize() + SmtpPort.GetVarSize() + Pop3Server.GetVarSize() + Pop3Port.GetVarSize() + UserName.GetVarSize() + Pwd.GetVarSize();
        public void Serialize(BinaryWriter writer)
        {
            writer.WriteVarString(SmtpServer);
            writer.WriteVarString(SmtpPort);
            writer.WriteVarString(Pop3Server);
            writer.WriteVarString(Pop3Port);
            writer.WriteVarString(UserName);
            writer.WriteVarString(Pwd);
        }
        public void Deserialize(BinaryReader reader)
        {
            SmtpServer = reader.ReadVarString();
            SmtpPort = reader.ReadVarString();
            Pop3Server = reader.ReadVarString();
            Pop3Port = reader.ReadVarString();
            UserName = reader.ReadVarString();
            Pwd = reader.ReadVarString();
        }       
    }
}
