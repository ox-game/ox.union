using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Tablet.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OX.Tablet
{
    public static partial class SecureHelper
    {
        public static event Action RunModeChanged;
        public static bool IsSeniorRunMode { get; private set; } = false;
        public static bool NeedExit { get; set; } = false;
        static SecureHelper()
        {
            IsSeniorRunMode = NodeConfig.Instance.RunMode == "senior";
        }
        public static void SwitchRunMode()
        {
            if (IsSeniorRunMode)
            {
                NodeConfig.Instance.RunMode = "simple";
                IsSeniorRunMode = false;
                NodeConfig.Instance.Save();
            }
            else
            {
                NodeConfig.Instance.RunMode = "senior";
                IsSeniorRunMode = true;
                NodeConfig.Instance.Save();
            }
            RunModeChanged?.Invoke();
        }
        public static BlockIndex BlockIndex { get; private set; }
        public static OXAccount MasterAccount { get; private set; } = default;
        public static EthAccount ExchangeAccount { get; private set; } = default;
        public static bool IsAuthenticated { get { return ExchangeAccount.IsNotNull() && MasterAccount.IsNotNull(); } }
        public static uint BalanceChangedLastIndex { get; internal set; }
        public static bool IndexComplated
        {
            get
            {
                if (SecureHelper.BlockIndex.IsNull()) return false;
                return SecureHelper.BlockIndex.IndexHeight > Blockchain.Singleton.HeaderHeight - 100;
            }
        }
        public static void SetAccount(EthAccount account)
        {
            ExchangeAccount = account;
            MasterAccount = new OXAccount(account.Key.GetPrivateKeyAsBytes());
            if (BlockIndex.IsNotNull())
            {
                BlockIndex.Dispose();
                Thread.Sleep(1000);
            }
            BlockIndex = new BlockIndex(MasterAccount, account, "Data_BlockIndex");
            Program.BlockIndex = BlockIndex;
        }
        public static Dictionary<UInt256, EthAssetBalanceState> GetExchangeBalanceStates()
        {
            Dictionary<UInt256, EthAssetBalanceState> ExchangeBalanceStates = new Dictionary<UInt256, EthAssetBalanceState>();
            if (SecureHelper.BlockIndex.EthMapUTXOs.IsNotNullAndEmpty())
            {
                foreach (var g in SecureHelper.BlockIndex.EthMapUTXOs.Values.GroupBy(m => m.Output.AssetId))
                {
                    EthAssetBalanceState balanceState = new EthAssetBalanceState
                    {
                        AssetId = g.Key,
                    };
                    var assetState = Blockchain.Singleton.CurrentSnapshot.Assets.TryGet(g.Key);
                    if (assetState.IsNotNull())
                    {
                        balanceState.AssetName = assetState.GetName();
                    }
                    balanceState.TotalBalance = g.Sum(m => m.Output.Value);
                    var gs = g.Where(m => m.LockExpirationIndex < Blockchain.Singleton.Height);
                    balanceState.AvailableBalance = gs.IsNotNullAndEmpty() ? gs.Sum(m => m.Output.Value) : Fixed8.Zero;
                    var ms = g.Where(m => m.LockExpirationIndex == 0);
                    balanceState.MasterBalance = ms.IsNotNullAndEmpty() ? ms.Sum(m => m.Output.Value) : Fixed8.Zero;
                    balanceState.OMS = g.ToArray();
                    ExchangeBalanceStates[g.Key] = balanceState;
                }
            }
            return ExchangeBalanceStates;
        }
        public static Dictionary<UInt256, MasterAssetBalanceState> GetMasterBalanceStates()
        {
            Dictionary<UInt256, MasterAssetBalanceState> MasterBalanceStates = new Dictionary<UInt256, MasterAssetBalanceState>();
            if (SecureHelper.BlockIndex.MasterUTXOs.IsNotNullAndEmpty())
            {
                foreach (var g in SecureHelper.BlockIndex.MasterUTXOs.Values.GroupBy(m => m.Output.AssetId))
                {
                    MasterAssetBalanceState balanceState = new MasterAssetBalanceState
                    {
                        AssetId = g.Key,
                    };
                    var assetState = Blockchain.Singleton.CurrentSnapshot.Assets.TryGet(g.Key);
                    if (assetState.IsNotNull())
                    {
                        balanceState.AssetName = assetState.GetName();
                    }
                    balanceState.TotalBalance = g.Sum(m => m.Output.Value);
                    var gs = g.Where(m => m.IsTimeLock ? m.LockExpirationIndex < System.DateTime.Now.ToTimestamp() : m.LockExpirationIndex < Blockchain.Singleton.Height);
                    balanceState.AvailableBalance = gs.IsNotNullAndEmpty() ? gs.Sum(m => m.Output.Value) : Fixed8.Zero;
                    var ms = g.Where(m => m.LockExpirationIndex == 0);
                    balanceState.MasterBalance = ms.IsNotNullAndEmpty() ? ms.Sum(m => m.Output.Value) : Fixed8.Zero;
                    balanceState.OMS = g.ToArray();
                    MasterBalanceStates[g.Key] = balanceState;
                }
            }
            return MasterBalanceStates;
        }
        public static Fixed8 GetMasterAvailableBalance(UInt256 assetId)
        {
            var balance = Fixed8.Zero;
            if (GetMasterBalanceStates().TryGetValue(assetId, out var balanceState))
            {
                balance = balanceState.AvailableBalance;
            }
            return balance;
        }
        public static Fixed8 GetExchangeAvailableBalance(UInt256 assetId)
        {
            var balance = Fixed8.Zero;
            if (GetExchangeBalanceStates().TryGetValue(assetId, out var balanceState))
            {
                balance = balanceState.AvailableBalance;
            }
            return balance;
        }
        public static Transaction BuildAndSignNoneOutput(this Transaction tx, Fixed8 otherfee, AvatarAccount defaultAccouont = default)
        {
            if (tx.IsNull() || tx.Outputs.Count() > 0) return default;
            var fee = tx.SystemFee+ otherfee;
            var masterBalanceStates = GetMasterBalanceStates();
            MasterAssetBalanceState balanceState = default;
            if (!masterBalanceStates.TryGetValue(Blockchain.OXC, out balanceState) || balanceState.AvailableBalance < fee)
                return default;

            Fixed8 Amount = fee;
            List<CoinReference> inputs = new List<CoinReference>();
            List<AvatarAccount> avatars = new List<AvatarAccount>();
            if (defaultAccouont.IsNotNull())
                avatars.Add(defaultAccouont);
            List<TransactionOutput> outputs = new List<TransactionOutput>();
            var utxos = SecureHelper.BlockIndex.GetMasterUtxos(Blockchain.OXC);
            if (utxos.IsNotNullAndEmpty())
            {
                List<string> excludedUtxoKeys = new List<string>();
                if (utxos.SortSearch(Amount.GetInternalValue(), excludedUtxoKeys, out MasterUTXO[] selectedUtxos, out long remainder))
                {
                    if (remainder > 0)
                        outputs.Add(new TransactionOutput { AssetId = Blockchain.OXC, ScriptHash = SecureHelper.MasterAccount.ScriptHash, Value = new Fixed8(remainder) });
                }
                foreach (var utxo in selectedUtxos)
                {
                    inputs.Add(new CoinReference { PrevHash = utxo.TxId, PrevIndex = utxo.N });

                    if (utxo.IsLockCoin)
                    {
                        LockAssetTransaction lat = new LockAssetTransaction { IsTimeLock = utxo.IsTimeLock, LockExpiration = utxo.LockExpirationIndex, Recipient = SecureHelper.MasterAccount.Key.PublicKey };
                        avatars.Add(LockAssetHelper.CreateAccount(lat.GetContract(), SecureHelper.MasterAccount.Key));
                    }
                    else
                    {
                        avatars.Add(LockAssetHelper.CreateAccount(SecureHelper.MasterAccount.Contract, SecureHelper.MasterAccount.Key));
                    }
                }
                tx.Outputs = outputs.ToArray();
                tx.Inputs = inputs.ToArray();
                tx = LockAssetHelper.Build(tx, avatars.ToArray());
                return tx;
            }
            return default;
        }
        public static Transaction BuildAndSignOneOXCOutput(this Transaction tx, Fixed8 otherfee)
        {
            if (tx.IsNull() || tx.Outputs.Count() != 1) return default;
            var output = tx.Outputs.First();
            if (output.AssetId != Blockchain.OXC) return default;
            var fee = tx.SystemFee+ otherfee;
            var masterBalanceStates = GetMasterBalanceStates();
            MasterAssetBalanceState balanceState = default;
            if (!masterBalanceStates.TryGetValue(output.AssetId, out balanceState) || balanceState.AvailableBalance < output.Value + fee)
                return default;

            Fixed8 Amount = output.Value + fee;
            List<CoinReference> inputs = new List<CoinReference>();
            List<AvatarAccount> avatars = new List<AvatarAccount>();
            List<TransactionOutput> outputs = new List<TransactionOutput>();
            outputs.Add(output);
            var utxos = SecureHelper.BlockIndex.GetMasterUtxos(output.AssetId);
            if (utxos.IsNotNullAndEmpty())
            {
                List<string> excludedUtxoKeys = new List<string>();
                if (utxos.SortSearch(Amount.GetInternalValue(), excludedUtxoKeys, out MasterUTXO[] selectedUtxos, out long remainder))
                {
                    if (remainder > 0)
                        outputs.Add(new TransactionOutput { AssetId = output.AssetId, ScriptHash = SecureHelper.MasterAccount.ScriptHash, Value = new Fixed8(remainder) });
                }
                foreach (var utxo in selectedUtxos)
                {
                    inputs.Add(new CoinReference { PrevHash = utxo.TxId, PrevIndex = utxo.N });

                    if (utxo.IsLockCoin)
                    {
                        LockAssetTransaction lat = new LockAssetTransaction { IsTimeLock = utxo.IsTimeLock, LockExpiration = utxo.LockExpirationIndex, Recipient = SecureHelper.MasterAccount.Key.PublicKey };
                        avatars.Add(LockAssetHelper.CreateAccount(lat.GetContract(), SecureHelper.MasterAccount.Key));
                    }
                    else
                    {
                        avatars.Add(LockAssetHelper.CreateAccount(SecureHelper.MasterAccount.Contract, SecureHelper.MasterAccount.Key));
                    }
                }
                tx.Outputs = outputs.ToArray();
                tx.Inputs = inputs.ToArray();
                tx = LockAssetHelper.Build(tx, avatars.ToArray());
                return tx;
            }
            return default;
        }
        public static Transaction BuildAndSignMultiOXCOutput(this Transaction tx, Fixed8 otherfee)
        {
            if (tx.IsNull() || tx.Outputs.Count() <= 1) return default;
            foreach (var output in tx.Outputs)
            {
                if (output.AssetId != Blockchain.OXC) return default;
            }
            var fee = tx.SystemFee+ otherfee;
            if (tx.SystemFee < Fixed8.One)
                fee += Fixed8.Zero;
            var Amount = tx.Outputs.Sum(m => m.Value) + fee;
            var masterBalanceStates = GetMasterBalanceStates();
            MasterAssetBalanceState balanceState = default;

            if (!masterBalanceStates.TryGetValue(Blockchain.OXC, out balanceState) || balanceState.AvailableBalance < Amount)
                return default;

            List<CoinReference> inputs = new List<CoinReference>();
            List<AvatarAccount> avatars = new List<AvatarAccount>();
            List<TransactionOutput> outputs = new List<TransactionOutput>();
            outputs.AddRange(tx.Outputs);
            var utxos = SecureHelper.BlockIndex.GetMasterUtxos(Blockchain.OXC);
            if (utxos.IsNotNullAndEmpty())
            {
                List<string> excludedUtxoKeys = new List<string>();
                if (utxos.SortSearch(Amount.GetInternalValue(), excludedUtxoKeys, out MasterUTXO[] selectedUtxos, out long remainder))
                {
                    if (remainder > 0)
                        outputs.Add(new TransactionOutput { AssetId = Blockchain.OXC, ScriptHash = SecureHelper.MasterAccount.ScriptHash, Value = new Fixed8(remainder) });
                }
                foreach (var utxo in selectedUtxos)
                {
                    inputs.Add(new CoinReference { PrevHash = utxo.TxId, PrevIndex = utxo.N });

                    if (utxo.IsLockCoin)
                    {
                        LockAssetTransaction lat = new LockAssetTransaction { IsTimeLock = utxo.IsTimeLock, LockExpiration = utxo.LockExpirationIndex, Recipient = SecureHelper.MasterAccount.Key.PublicKey };
                        avatars.Add(LockAssetHelper.CreateAccount(lat.GetContract(), SecureHelper.MasterAccount.Key));
                    }
                    else
                    {
                        avatars.Add(LockAssetHelper.CreateAccount(SecureHelper.MasterAccount.Contract, SecureHelper.MasterAccount.Key));
                    }
                }
                tx.Outputs = outputs.ToArray();
                tx.Inputs = inputs.ToArray();
                tx = LockAssetHelper.Build(tx, avatars.ToArray());
                return tx;
            }
            return default;
        }
        public static byte[] Decrypt(string password, byte[] data)
        {
            Rfc2898DeriveBytes deriver = new Rfc2898DeriveBytes(password, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = deriver.GetBytes(16);
            aes.Key = deriver.GetBytes(24);
            aes.Key = deriver.GetBytes(32);
            aes.IV = deriver.GetBytes(16);
            ICryptoTransform transform = aes.CreateDecryptor();
            return transform.TransformFinalBlock(data, 0, data.Length);
        }
        public static byte[] Encrypt(string password, byte[] data)
        {
            Rfc2898DeriveBytes deriver = new Rfc2898DeriveBytes(password, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = deriver.GetBytes(16);
            aes.Key = deriver.GetBytes(24);
            aes.Key = deriver.GetBytes(32);
            aes.IV = deriver.GetBytes(16);
            ICryptoTransform transform = aes.CreateEncryptor();
            return transform.TransformFinalBlock(data, 0, data.Length);
        }
    }
}
