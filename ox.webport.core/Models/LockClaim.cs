﻿using OX.Network.P2P.Payloads;
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

namespace OX.WebPort
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
        public LockOXSFlag Flag;
        public uint Index;
        public uint SpendIndex;
        public bool IsLockAssetTx;
        public LockAssetTransaction Tx;
        public virtual int Size => Holder.Size + Output.Size + sizeof(LockOXSFlag) + sizeof(uint) + sizeof(uint) + sizeof(bool) + sizeof(uint) + (Tx.IsNotNull() ? Tx.Size : 0);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Holder);
            writer.Write(Output);
            writer.Write((byte)Flag);
            writer.Write(Index);
            writer.Write(SpendIndex);
            writer.Write(IsLockAssetTx);
            if (Tx.IsNotNull())
            {
                writer.Write((uint)Tx.Size);
                writer.Write(Tx);
            }
            else
            {
                writer.Write((uint)0);
            }
          
        }
        public void Deserialize(BinaryReader reader)
        {
            Holder = reader.ReadSerializable<UInt160>();
            Output = reader.ReadSerializable<TransactionOutput>();
            Flag = (LockOXSFlag)reader.ReadByte();
            Index = reader.ReadUInt32();
            SpendIndex = reader.ReadUInt32();
            IsLockAssetTx = reader.ReadBoolean();
            uint d = reader.ReadUInt32();
            if (d > 0)
            {
                Tx = reader.ReadSerializable<LockAssetTransaction>();
            }
        }

    }
}