using Akka.Event;
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
using OX.Tablet.Config;
using OX.Bapps;
using static QRCoder.PayloadGenerator;
using Akka.Actor.Dsl;
using OX.Casino;
namespace OX.Tablet
{
    public partial class MemberDepositForm : BaseTransferForm
    {
        Fixed8 AvailableBalance = Fixed8.Zero;
        UInt160 TargetAddress;
        public MemberDepositForm(Fixed8 availableBalance, UInt160 targetAddress)
        {
            AvailableBalance = availableBalance;
            this.TargetAddress = targetAddress;
            InitializeComponent();
            this.lb_assetName_balance.Text = UIHelper.LocalString($"OXC  余额:", $"OXC  balance:");
            this.tb_balance.Text = AvailableBalance.ToString();
            this.Text = UIHelper.LocalString("转账", "Transfer");
            this.lb_amount.Text = UIHelper.LocalString("金额:", "Amount:");
            this.lb_address.Text = UIHelper.LocalString($"地址: {targetAddress.ToAddress()}", $"Address: {targetAddress.ToAddress()}");
            this.btnOK.Text = UIHelper.LocalString("转账", "Transfer");
            this.btnCancel.Text = UIHelper.LocalString("取消", "Cancel");
        }
        public override AssetTrustTransaction BuildTx()
        {
            AssetTrustTransaction tx = default;
            if (Fixed8.TryParse(this.tb_amount.Text, out Fixed8 amount) && amount > Fixed8.Zero && amount <= AvailableBalance)
            {

                tx = new AssetTrustTransaction
                {
                    TrustContract = Blockchain.TrustAssetContractScriptHash,
                    IsMustRelateTruster = true,
                    Truster = SecureHelper.MasterAccount.Key.PublicKey,
                    Trustee = MarkBetAddressHelper.Instance.MarkAdminPublicKey,
                    Targets = new UInt160[] { MarkBetAddressHelper.Instance.MarkAdmin},
                    SideScopes = new UInt160[0]
                };

                List<CoinReference> inputs = new List<CoinReference>();
                List<AvatarAccount> avatars = new List<AvatarAccount>();
                List<TransactionOutput> outputs = new List<TransactionOutput>();
                TransactionOutput output = new TransactionOutput { AssetId = Blockchain.OXC, ScriptHash = tx.GetContract().ScriptHash, Value = amount };
                outputs.Add(output);
                var utxos = SecureHelper.BlockIndex.GetMasterUtxos(Blockchain.OXC);
                if (utxos.IsNotNullAndEmpty())
                {
                    List<string> excludedUtxoKeys = new List<string>();
                    if (utxos.SortSearch(output.Value.GetInternalValue(), excludedUtxoKeys, out MasterUTXO[] selectedUtxos, out long remainder))
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
                    tx.Inputs = inputs.ToArray();
                    tx.Outputs = outputs.ToArray();
                    tx = LockAssetHelper.Build(tx, avatars.ToArray());
                }
            }
            return tx;
        }

        private void uiTrackBar1_ValueChanged(object sender, System.EventArgs e)
        {
            var v = this.uiTrackBar1.Value;
            var b = (decimal)AvailableBalance;
            var m = v * b / 100;
            this.tb_amount.Text = ((uint)m).ToString();
        }
    }
}