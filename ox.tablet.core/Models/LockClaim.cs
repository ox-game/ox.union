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

namespace OX.Tablet
{
    public enum LockOXSFlag : byte
    {
        Unspend = 1 << 0,
        Spend = 1 << 1,
        Claimed = 1 << 2,
    }
    public class LockOXS : ISerializable
    {
        public UInt160 Holder;
        public TransactionOutput Output;
        public LockAssetTransaction Tx;
        public bool IsLockAssetTx;
        public LockOXSFlag Flag;
        public uint Index;
        public uint SpendIndex;
        public virtual int Size => Holder.Size + Output.Size + Tx.Size + sizeof(bool) + sizeof(LockOXSFlag) + sizeof(uint) + sizeof(uint);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Holder);
            writer.Write(Output);
            writer.Write(Tx);
            writer.Write(IsLockAssetTx);
            writer.Write((byte)Flag);
            writer.Write(Index);
            writer.Write(SpendIndex);
        }
        public void Deserialize(BinaryReader reader)
        {
            Holder = reader.ReadSerializable<UInt160>();
            Output = reader.ReadSerializable<TransactionOutput>();
            Tx = reader.ReadSerializable<LockAssetTransaction>();
            IsLockAssetTx = reader.ReadBoolean();
            Flag = (LockOXSFlag)reader.ReadByte();
            Index = reader.ReadUInt32();
            SpendIndex = reader.ReadUInt32();
        }

    }
}
