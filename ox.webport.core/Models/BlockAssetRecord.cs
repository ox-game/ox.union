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
using OX.Wallets.NEP6;
using OX.Ledger;

namespace OX.WebPort.Models
{
    public class BlockAssetRecord : ISerializable
    {
        public uint Index;
        public uint TimeStamp;
        public Dictionary<UInt256, Fixed8> Balances;

        public virtual int Size => sizeof(uint) + sizeof(uint) + sizeof(uint) + Balances.Count * (32 + 8);

        public BlockAssetRecord()
        {
            this.Balances = new Dictionary<UInt256, Fixed8>();
        }


        public void Deserialize(BinaryReader reader)
        {
            Index = reader.ReadUInt32();
            TimeStamp = reader.ReadUInt32();
            uint count = reader.ReadUInt32();
            Balances = new Dictionary<UInt256, Fixed8>();
            for (uint i = 0; i < count; i++)
            {
                UInt256 assetId = reader.ReadSerializable<UInt256>();
                Fixed8 value = reader.ReadSerializable<Fixed8>();
                Balances[assetId] = value;
            }
        }
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Index);
            writer.Write(TimeStamp);
            var balances = Balances.Where(p => p.Value != Fixed8.Zero).ToArray();
            writer.Write((uint)balances.Length);
            foreach (var pair in balances)
            {
                writer.Write(pair.Key);
                writer.Write(pair.Value);
            }
        }

    }
}
