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
using Sunny.UI;
using System.Windows.Forms;

namespace OX.Tablet
{
    public class OXAccount
    {
        public string Address { get; private set; }
        public UInt160 ScriptHash { get; private set; }
        public KeyPair Key { get; set; }
        public Contract Contract { get; private set; }
        public OXAccount(byte[] privateKey)
        {
            Key = new KeyPair(privateKey);
            Contract = new NEP6Contract
            {
                Script = Contract.CreateSignatureRedeemScript(Key.PublicKey),
                ParameterList = new[] { ContractParameterType.Signature },
                ParameterNames = new[] { "signature" },
                Deployed = false
            };
            this.ScriptHash = Contract.ScriptHash;
            this.Address = this.ScriptHash.ToAddress();
        }

    }
    public class EthAccount
    {
        public string EthAddress { get; private set; }
        public string PublicKey { get; private set; }
        public EthECKey Key { get; private set; }
        public UInt160 MapAddress { get; private set; }
        public uint AddressID { get; private set; }

        public EthAccount(string ethAddress, string publickey, EthECKey key)
        {
            EthAddress = ethAddress;
            PublicKey = publickey;
            Key = key;
            MapAddress = EthAddress.BuildMapAddress();
            AddressID = MapAddress.BuildAddressId();
        }
        public static bool TryBuildAccount(string password, string cipherPriKey, out EthAccount account)
        {
            account = default;
            try
            {
                var cipherBS = cipherPriKey.HexToBytes();
                var privateKey = SecureHelper.Decrypt(password, cipherBS);
                if (privateKey.IsNullOrEmpty()) return false;
                var ecKey = new Nethereum.Signer.EthECKey(privateKey, true);
                var publickey = ecKey.GetPubKey().ToHex(false);
                var address = ecKey.GetPublicAddress();
                account = new EthAccount(address, publickey, ecKey);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is EthAccount ethid)
            {
                return ethid.EthAddress.ToLower() == this.EthAddress.ToLower();
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return this.EthAddress.ToLower().GetHashCode();
        }
    }
    public class MasterOutputMerge : ISerializable
    {
        public bool IsLockCoin;
        public bool IsTimeLock;
        public uint LockExpirationIndex;
        public TransactionOutput Output;

        public virtual int Size =>sizeof(bool) + sizeof(bool) + sizeof(uint) + Output.Size;
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(IsLockCoin);
            writer.Write(IsTimeLock);
            writer.Write(LockExpirationIndex);
            writer.Write(Output);
        }
        public void Deserialize(BinaryReader reader)
        {
            IsLockCoin = reader.ReadBoolean();
            IsTimeLock = reader.ReadBoolean();
            LockExpirationIndex = reader.ReadUInt32();
            Output = reader.ReadSerializable<TransactionOutput>();
        }
    }
    public class EthOutputMerge : ISerializable
    {
        public string EthAddress;
        public uint LockExpirationIndex;
        public TransactionOutput Output;


        public virtual int Size => EthAddress.GetVarSize() + sizeof(uint) + Output.Size;
        public void Serialize(BinaryWriter writer)
        {
            writer.WriteVarString(EthAddress);
            writer.Write(LockExpirationIndex);
            writer.Write(Output);
        }
        public void Deserialize(BinaryReader reader)
        {
            EthAddress = reader.ReadVarString();
            LockExpirationIndex = reader.ReadUInt32();
            Output = reader.ReadSerializable<TransactionOutput>();
        }
    }
    public class LockAssetTransactionMerge : ISerializable
    {
        public LockAssetTransaction LockAssetTransaction;
        public uint LastIndex;


        public virtual int Size => LockAssetTransaction.Size + sizeof(uint);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(LockAssetTransaction);
            writer.Write(LastIndex);
        }
        public void Deserialize(BinaryReader reader)
        {
            LockAssetTransaction = reader.ReadSerializable<LockAssetTransaction>();
            LastIndex = reader.ReadUInt32();
        }
    }
    public class EthereumMapTransactionMerge : ISerializable
    {
        public EthereumMapTransaction EthereumMapTransaction;
        public uint LastIndex;


        public virtual int Size => EthereumMapTransaction.Size + sizeof(uint);
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(EthereumMapTransaction);
            writer.Write(LastIndex);
        }
        public void Deserialize(BinaryReader reader)
        {
            EthereumMapTransaction = reader.ReadSerializable<EthereumMapTransaction>();
            LastIndex = reader.ReadUInt32();
        }
    }
    public class EthAssetBalanceState
    {
        public UInt256 AssetId;
        public string AssetName;
        public Fixed8 MasterBalance = Fixed8.Zero;
        public Fixed8 AvailableBalance = Fixed8.Zero;
        public Fixed8 TotalBalance = Fixed8.Zero;
        public EthOutputMerge[] OMS = new EthOutputMerge[0];
    }
    public class MasterAssetBalanceState
    {
        public UInt256 AssetId;
        public string AssetName;
        public Fixed8 MasterBalance = Fixed8.Zero;
        public Fixed8 AvailableBalance = Fixed8.Zero;
        public Fixed8 TotalBalance = Fixed8.Zero;
        public MasterOutputMerge[] OMS = new MasterOutputMerge[0];
    }
}
