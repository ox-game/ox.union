using OX.Network.P2P.Payloads;
using OX.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OX.SmartContract;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using Nethereum.Model;
using Nethereum.Signer.Crypto;
using System.IO;
using OX.IO;

namespace OX.Tablet
{

    public class Contact : ISerializable
    {
        public string Name;
        public string Address;
        /// <summary>
        /// 1:OX-ECO
        /// 2:Eth
        /// 3:MemberId
        /// </summary>
        public byte Kind;
        public string Display
        {
            get { return $"{this.Name}--{this.Address}"; }
        }
        public virtual int Size => Name.GetVarSize() + Address.GetVarSize() + sizeof(byte);
        public void Serialize(BinaryWriter writer)
        {
            writer.WriteVarString(Name);
            writer.WriteVarString(Address);
            writer.Write(Kind);
        }
        public void Deserialize(BinaryReader reader)
        {
            Name = reader.ReadVarString();
            Address = reader.ReadVarString();
            Kind = reader.ReadByte();
        }
    }
    public class ContactSet : ISerializable
    {
        public Contact[] Contacts;
        public virtual int Size => Contacts.GetVarSize();
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Contacts);
        }
        public void Deserialize(BinaryReader reader)
        {
            Contacts = reader.ReadSerializableArray<Contact>();
        }
    }
}
