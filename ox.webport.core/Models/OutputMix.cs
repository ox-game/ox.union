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
    public class OutputMix : ISerializable
    {
        public UInt160 Holder;
        public bool IsLockCoin;
        public bool IsTimeLock;
        public UInt160 FromHolder;
        public uint LockExpirationIndex;
        public uint TimeStamp;
        public CoinReference Input;
        public TransactionOutput Output;

        public virtual int Size => Holder.Size  + sizeof(bool) + sizeof(bool) + FromHolder.Size + sizeof(uint) + sizeof(uint) + Input.Size + Output.Size;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Holder);
            writer.Write(IsLockCoin);
            writer.Write(IsTimeLock);
            writer.Write(FromHolder);
            writer.Write(LockExpirationIndex);
            writer.Write(TimeStamp);
            writer.Write(Input);
            writer.Write(Output);
        }
        public void Deserialize(BinaryReader reader)
        {
            Holder = reader.ReadSerializable<UInt160>();
            IsLockCoin = reader.ReadBoolean();
            IsTimeLock = reader.ReadBoolean();
            FromHolder = reader.ReadSerializable<UInt160>();
            LockExpirationIndex = reader.ReadUInt32();
            TimeStamp = reader.ReadUInt32();
            Input = reader.ReadSerializable<CoinReference>();
            Output = reader.ReadSerializable<TransactionOutput>();
        }
    }
}
