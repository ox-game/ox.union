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
using System.Linq;
using Org.BouncyCastle.Cms;
using OX.Tablet.Config;
namespace OX.Tablet
{
    public partial class SelfLockMiningForm : BaseTransferForm
    {
        MasterAssetBalanceState MasterAssetBalanceState;
        Fixed8 AvailableBalance = Fixed8.Zero;
        public SelfLockMiningForm(MasterAssetBalanceState balanceState)
        {
            MasterAssetBalanceState = balanceState;
            InitializeComponent();
            var assetState = Blockchain.Singleton.Store.GetAssets().TryGet(balanceState.AssetId);
            if (assetState.IsNotNull())
            {
                this.lb_assetName_balance.Text = UIHelper.LocalString($"{assetState.GetName()}  余额:", $"{assetState.GetName()}  balance:");
            }
            AvailableBalance = balanceState.AvailableBalance;
            this.tb_balance.Text = balanceState.AvailableBalance.ToString();
            this.Text = UIHelper.LocalString("锁仓挖矿", "Lock Mining");
            this.lb_amount.Text = UIHelper.LocalString("金额:", "Amount:");
            this.lb_expire.Text = UIHelper.LocalString("锁仓周期:", "Lock Duration:");
            this.btnOK.Text = UIHelper.LocalString("挖矿", "Mine");
            this.btnCancel.Text = UIHelper.LocalString("取消", "Cancel");
            this.cb_expire.SelectedIndex = 0;
        }
        public override Transaction BuildTx()
        {
            LockAssetTransaction tx = default;

            if (Fixed8.TryParse(this.tb_amount.Text, out Fixed8 amount) && amount > Fixed8.Zero && amount <= MasterAssetBalanceState.AvailableBalance)
            {
                List<CoinReference> inputs = new List<CoinReference>();
                List<AvatarAccount> avatars = new List<AvatarAccount>();
                List<TransactionOutput> outputs = new List<TransactionOutput>();

                var utxos = SecureHelper.BlockIndex.GetMasterUtxos(MasterAssetBalanceState.AssetId);
                if (utxos.IsNotNullAndEmpty())
                {
                    List<string> excludedUtxoKeys = new List<string>();
                    if (utxos.SortSearch(amount.GetInternalValue(), excludedUtxoKeys, out MasterUTXO[] selectedUtxos, out long remainder))
                    {
                        if (remainder > 0)
                            outputs.Add(new TransactionOutput { AssetId = MasterAssetBalanceState.AssetId, ScriptHash = SecureHelper.MasterAccount.ScriptHash, Value = new Fixed8(remainder) });
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

                    tx = new LockAssetTransaction
                    {
                        LockContract = Blockchain.LockAssetContractScriptHash,
                        IsTimeLock = false,
                        LockExpiration = uint.Parse(this.cb_expire.Text) + Blockchain.Singleton.Height,
                        Recipient = SecureHelper.MasterAccount.Key.PublicKey,
                        Attributes = new TransactionAttribute[0],
                        Inputs = inputs.ToArray(),
                        Witnesses = new Witness[0]
                    };
                    TransactionOutput output = new TransactionOutput { AssetId = MasterAssetBalanceState.AssetId, ScriptHash = tx.GetContract().ScriptHash, Value = amount };
                    outputs.Add(output);
                    tx.Outputs = outputs.ToArray();
                    tx = LockAssetHelper.Build(tx, avatars.ToArray());
                }
            }
            return tx;
        }

        private void SelfLockMiningForm_Load(object sender, System.EventArgs e)
        {

        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {

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