using Akka.Actor;
using OX.Ledger;
using Sunny.UI;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using OX.IO;
using OX.Network.P2P.Payloads;
using Nethereum.Util;
using OX.Wallets;
using Nethereum.Signer.Crypto;
using OX.SmartContract;
using Nethereum.Model;
using OX.Network.P2P;
using OX.Tablet.Config;
namespace OX.Tablet
{
    public partial class ExchangeRechargeForm : BaseTransferForm
    {
        public SwapPairMerge SwapPairMerge;
        public SwapPairIDO IDO;
        public bool IsIDOTime;
        public UInt160 HostSH;
        Dictionary<UInt256, MasterAssetBalanceState> BalanceStates = new Dictionary<UInt256, MasterAssetBalanceState>();
        UInt256 SelectedAsset = Blockchain.OXC;
        Fixed8 balance = Fixed8.Zero;
        public ExchangeRechargeForm(UInt160 hostSH, SwapPairMerge swapPairMerge, bool isIDOTime)
        {
            this.SwapPairMerge = swapPairMerge;

            this.HostSH = hostSH;
            this.IsIDOTime = isIDOTime;
            try
            {
                IDO = this.SwapPairMerge.SwapPairReply.Mark.AsSerializable<SwapPairIDO>();
            }
            catch { }
            InitializeComponent();

            BalanceStates = SecureHelper.GetMasterBalanceStates();

            this.Text = UIHelper.LocalString($"{this.SwapPairMerge.TargetAssetState.GetName()}  资产交易", $"{this.SwapPairMerge.TargetAssetState.GetName()}  Asset Exchange");
            this.lb_amount.Text = UIHelper.LocalString("金额:", "Amount:");
            this.st_asset.InActiveText = UIHelper.LocalString("买入", "Buy");
            this.st_asset.ActiveText = UIHelper.LocalString("卖出", "Sale");
            this.btnOK.Text = UIHelper.LocalString("交易", "Transfer");
            this.btnCancel.Text = UIHelper.LocalString("取消", "Cancel");
            SwitchAsset();
            if (isIDOTime)
            {
                this.st_asset.Enabled = false;
                this.btnOK.Text = UIHelper.LocalString("预购", "IDO");
            }
        }
        public override Transaction BuildTx()
        {
            ContractTransaction ct = default;
            if (Fixed8.TryParse(tb_amount.Text, out Fixed8 amount))
            {
                List<CoinReference> inputs = new List<CoinReference>();
                List<AvatarAccount> avatars = new List<AvatarAccount>();
                List<TransactionOutput> outputs = new List<TransactionOutput>();
                TransactionOutput output = new TransactionOutput { AssetId = this.SelectedAsset, ScriptHash = this.HostSH, Value = amount };
                outputs.Add(output);
                var utxos = SecureHelper.BlockIndex.GetMasterUtxos(this.SelectedAsset);
                if (utxos.IsNotNullAndEmpty())
                {
                    List<string> excludedUtxoKeys = new List<string>();
                    if (utxos.SortSearch(output.Value.GetInternalValue(), excludedUtxoKeys, out MasterUTXO[] selectedUtxos, out long remainder))
                    {
                        if (remainder > 0)
                            outputs.Add(new TransactionOutput { AssetId = this.SelectedAsset, ScriptHash = SecureHelper.MasterAccount.ScriptHash, Value = new Fixed8(remainder) });
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
                    ct = new ContractTransaction
                    {
                        Attributes = new TransactionAttribute[0],
                        Outputs = outputs.ToArray(),
                        Inputs = inputs.ToArray(),
                        Witnesses = new Witness[0]
                    };
                    ct = LockAssetHelper.Build(ct, avatars.ToArray());
                }
            }
            return ct;
        }

        private void st_self_ValueChanged(object sender, bool value)
        {
            SwitchAsset();
        }
        void SwitchAsset()
        {
        
            SelectedAsset = this.st_asset.Active ? this.SwapPairMerge.TargetAssetState.AssetId : Blockchain.OXC;
            var assetName = this.st_asset.Active ? this.SwapPairMerge.TargetAssetState.GetName() : "OXC";
            if (BalanceStates.TryGetValue(SelectedAsset, out var balanceState))
            {
                balance = balanceState.AvailableBalance;
            }
            this.tb_balance.Text = balance.ToString();
            this.lb_assetName_balance.Text = UIHelper.LocalString($"{assetName} 余额:", $"{assetName} Balance:");
        }

        private void tb_amount_TextChanged(object sender, System.EventArgs e)
        {
            if (!Fixed8.TryParse(tb_amount.Text, out Fixed8 amount))
            {
                this.btnOK.Enabled = false;
                return;
            }
            if (amount == Fixed8.Zero)
            {
                btnOK.Enabled = false;
                return;
            }
            if (!Fixed8.TryParse(this.tb_balance.Text, out Fixed8 balance))
            {
                btnOK.Enabled = false;
                return;
            }

            if (amount == Fixed8.Zero || balance == Fixed8.Zero || amount > balance)
            {
                btnOK.Enabled = false;
                return;
            }
            if (this.IsIDOTime)
            {
                if (this.IDO.IsNull())
                {
                    btnOK.Enabled = false;
                    return;
                }
                if (amount.GetInternalValue() % this.IDO.Price.GetInternalValue() > 0)
                {
                    btnOK.Enabled = false;
                    return;
                }
            }
            btnOK.Enabled = true;
        }

        private void ExchangeRechargeForm_Load(object sender, System.EventArgs e)
        {
            tb_amount_TextChanged(null, new System.EventArgs());
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            var tx = this.BuildTx();
            if (tx.IsNotNull())
            {
                Program.BlockHandler.Tell(tx);
                foreach (var coin in tx.Inputs)
                {
                    SecureHelper.BlockIndex.UnconfirmCoins[coin] = Blockchain.Singleton.Height + 100;
                }
                new WaitTxForm(tx, UIHelper.LocalString("等待交易确认...", "Waiting  confirm transaction")).ShowDialog();
            }
        }

        private void uiTrackBar1_ValueChanged(object sender, System.EventArgs e)
        {
            var v = this.uiTrackBar1.Value;
            var b = (decimal)balance;
            var m = v * b / 100;
            this.tb_amount.Text = ((uint)m).ToString();
        }
    }
}