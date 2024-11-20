using Akka.Event;
using OX.Ledger;
using Sunny.UI;
using System.Linq;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using OX.IO;
using OX.Network.P2P.Payloads;
using Nethereum.Util;
using OX.Wallets;
using Nethereum.Signer.Crypto;
using OX.SmartContract;
using OX.Persistence;
using OX.Cryptography.ECC;
using System;
using OX.Tablet.Config;
using static QRCoder.PayloadGenerator.SwissQrCode;
namespace OX.Tablet
{
    public partial class MasterSeniorTransferForm : BaseTransferForm
    {
        MasterAssetBalanceState MasterAssetBalanceState;
        Fixed8 AvailableBalance = Fixed8.Zero;
        int TabSelectedIndex = 0;
        public MasterSeniorTransferForm(MasterAssetBalanceState balanceState)
        {
            MasterAssetBalanceState = balanceState;
            InitializeComponent();
            var assetState = Blockchain.Singleton.Store.GetAssets().TryGet(balanceState.AssetId);
            if (assetState.IsNotNull())
            {
                this.lb_assetName_balance.Text = UIHelper.LocalString($"{assetState.GetName()}  余额:", $"{assetState.GetName()}  balance:");
            }
            AvailableBalance=balanceState.AvailableBalance;
            this.tb_balance.Text = balanceState.AvailableBalance.ToString();
            this.Text = UIHelper.LocalString("转账", "Transfer");
            this.lb_amount.Text = UIHelper.LocalString("金额:", "Amount:");
            this.lb_ox_address.Text = UIHelper.LocalString("地址:", "Address:");
            this.tb_ox_transfer.Text = UIHelper.LocalString("原生转帐", "Native Transfer");
            this.tb_lock_transfer.Text = UIHelper.LocalString("锁仓转帐", "Lock Transfer");
            this.tb_eth_transfer.Text = UIHelper.LocalString("转帐到以太坊地址", "Transfer to Ethereum address");
            this.lb_eth_address.Text = UIHelper.LocalString("以太坊地址:", "Eth Address:");
            this.lb_eth_lockindex.Text = UIHelper.LocalString("解锁高度:", "Unlock Index:");
            this.lb_lock_pubkey.Text = UIHelper.LocalString("收款公钥:", "Payee Public Key:");
            this.lb_lock_expire.Text = UIHelper.LocalString("锁仓到期:", "Lock Expire:");

            this.st_self.ActiveText = UIHelper.LocalString("自主锁仓", "Lock Self");
            this.st_self.InActiveText = UIHelper.LocalString("转账锁仓", "Lock Transfer");

            this.st_timelock.ActiveText = UIHelper.LocalString("锁定时间", "Lock Time");
            this.st_timelock.InActiveText = UIHelper.LocalString("锁定区块", "Lock Block");
            this.btnOK.Text = UIHelper.LocalString("转账", "Transfer");
            this.btnCancel.Text = UIHelper.LocalString("取消", "Cancel");
            this.dp_lock_time.Visible = false;
            ContactSet contactSet = default;
            IEnumerable<Contact> oxContracts = default;
            IEnumerable<Contact> ethContracts = default;
            try
            {
                var cs = NodeConfig.Instance.Contacts;
                contactSet = cs.HexToBytes().AsSerializable<ContactSet>();
                oxContracts = contactSet.Contacts?.Where(m => m.Kind == 1);
                ethContracts = contactSet.Contacts?.Where(m => m.Kind == 2);
            }
            catch
            {

            }
            if (oxContracts.IsNotNullAndEmpty())
            {
                cb_ox_targetAddress.ValueMember = "Address";
                cb_ox_targetAddress.DisplayMember = "Display";
                cb_ox_targetAddress.DataSource = oxContracts.ToArray();
                cb_ox_targetAddress.ShowClearButton = true;
                cb_ox_targetAddress.SelectedIndex = -1;
            }
            if (ethContracts.IsNotNullAndEmpty())
            {
                cb_eth_address.ValueMember = "Address";
                cb_eth_address.DisplayMember = "Display";
                cb_eth_address.DataSource = ethContracts.ToArray();
                cb_eth_address.ShowClearButton = true;
                cb_eth_address.SelectedIndex = -1;
            }
        }
        TransactionOutput buildTransactionOutput(out ECPoint recipient, out uint ethLockIndex, out string ethAddress)
        {
            recipient = default;
            ethLockIndex = 0;
            ethAddress = string.Empty;
            Fixed8 amount = default;
            try
            {
                amount = Fixed8.Parse(tb_amount.Text);
            }
            catch
            {
                return default;
            }
            switch (TabSelectedIndex)
            {
                case 0:
                    UInt160 sh = default;
                    var sv = this.cb_ox_targetAddress.SelectedValue;
                    if (sv.IsNotNull() && sv is Contact ct)
                    {
                        sh = ct.Address.ToScriptHash();
                    }
                    if (sh.IsNull())
                    {
                        var text = this.cb_ox_targetAddress.Text.Trim();
                        if (text.IsNotNullAndEmpty())
                        {
                            if (uint.TryParse(text, out var memberId))
                            {
                                var bmsIndex = SecureHelper.BlockIndex.GetSubBlockIndex<BMSBlockIndex>();
                                var Member = bmsIndex.MarkMembers.Values?.Where(m => m.MarkMemberId == memberId).FirstOrDefault();
                                if (Member.IsNotNull())
                                    sh = Member.Holder;
                            }
                            else
                            {
                                try
                                {
                                    sh = text.ToScriptHash();
                                }
                                catch
                                {

                                }
                            }
                        }
                    }
                    if (sh.IsNotNull())
                        return new TransactionOutput { AssetId = MasterAssetBalanceState.AssetId, Value = amount, ScriptHash = sh };
                    else
                        return default;
                case 1:
                    try
                    {
                        recipient = ECPoint.Parse(this.tb_lock_pubkey.Text, ECCurve.Secp256r1);

                        return new TransactionOutput
                        {
                            AssetId = MasterAssetBalanceState.AssetId,
                            Value = amount,
                            ScriptHash = Contract.CreateSignatureRedeemScript(recipient).ToScriptHash()
                        };
                    }
                    catch
                    {
                        return default;
                    }
                case 2:
                    ethLockIndex = uint.Parse(this.tb_eth_lockindex.Text);
                    UInt160 sh2 = default;
                    var sv2 = this.cb_ox_targetAddress.SelectedValue;
                    if (sv2.IsNotNull() && sv2 is Contact ct2)
                    {
                        ethAddress = ct2.Address.ToLower();
                        if (!ethAddress.IsValidEthereumAddressHexFormat()) return default;
                        sh2 = ethAddress.BuildMapAddress(ethLockIndex);
                    }
                    if (sh2.IsNull())
                    {
                        ethAddress = this.cb_eth_address.Text.Trim().ToLower();
                        if (ethAddress.IsNotNullAndEmpty())
                        {
                            try
                            {
                                sh2 = ethAddress.BuildMapAddress(ethLockIndex);
                            }
                            catch
                            {

                            }
                        }
                    }

                    if (sh2.IsNotNull())
                        return new TransactionOutput { AssetId = MasterAssetBalanceState.AssetId, Value = amount, ScriptHash = sh2 };
                    else
                        return default;
            }
            return default;
        }
        public override Transaction BuildTx()
        {
            Transaction tx = default;
            var output = buildTransactionOutput(out ECPoint recipient, out uint ethLockIndex, out string ethAddress);
            if (output.IsNotNull())
            {
                switch (TabSelectedIndex)
                {
                    case 0:
                        tx = new ContractTransaction
                        {
                            Outputs = new[] { output },
                            Attributes = new TransactionAttribute[0],
                            Inputs = new CoinReference[0],
                            Witnesses = new Witness[0]
                        };
                        break;
                    case 1:
                        var isTime = this.st_timelock.Active;
                        uint expiration = 0;

                        if (isTime)
                        {
                            expiration = this.dp_lock_time.Value.ToTimestamp();
                            if (expiration - DateTime.Now.ToTimestamp() < 150)
                            {
                                string msg = $"{UIHelper.LocalString("锁仓的时间太短", "Locking time is too short")}";
                                this.ShowErrorTip(msg);
                                return default;
                            }
                        }
                        else
                        {
                            expiration = uint.Parse(this.tb_lock_index.Text);
                            if (expiration - Blockchain.Singleton.Height < 10)
                            {
                                string msg = $"{UIHelper.LocalString("锁仓的区块高度太低", "Locked block height is too low")}";
                                this.ShowErrorTip(msg);
                                return default;
                            }
                        }
                        LockAssetTransaction lat = new LockAssetTransaction
                        {
                            LockContract = Blockchain.LockAssetContractScriptHash,
                            IsTimeLock = isTime,
                            LockExpiration = expiration,
                            Recipient = recipient,
                            Attributes = new TransactionAttribute[0],
                            Inputs = new CoinReference[0],
                            Witnesses = new Witness[0]
                        };
                        output.ScriptHash = lat.GetContract().ScriptHash;
                        lat.Outputs = new TransactionOutput[] { output };
                        tx = lat;
                        break;
                    case 2:
                        if (ethLockIndex - Blockchain.Singleton.Height < 10)
                        {
                            string msg = $"{UIHelper.LocalString("锁仓的区块高度太低", "Locked block height is too low")}";
                            this.ShowErrorTip(msg);
                            return default;
                        }
                        tx = new EthereumMapTransaction
                        {
                            EthereumAddress = ethAddress,
                            LockExpirationIndex = ethLockIndex,
                            EthMapContract = Blockchain.EthereumMapContractScriptHash,
                            Outputs = new TransactionOutput[] { output },
                            Attributes = new TransactionAttribute[0],
                            Inputs = new CoinReference[0],
                            Witnesses = new Witness[0]
                        };
                        break;
                }
            }
            if (tx.IsNotNull())
            {
                if (Fixed8.TryParse(this.tb_amount.Text, out Fixed8 amount) && amount > Fixed8.Zero && amount <= MasterAssetBalanceState.AvailableBalance)
                {
                    List<CoinReference> inputs = new List<CoinReference>();
                    List<AvatarAccount> avatars = new List<AvatarAccount>();
                    List<TransactionOutput> outputs = new List<TransactionOutput>();
                    outputs.AddRange(tx.Outputs);
                    var utxos = SecureHelper.BlockIndex.GetMasterUtxos(MasterAssetBalanceState.AssetId);
                    if (utxos.IsNotNullAndEmpty())
                    {
                        List<string> excludedUtxoKeys = new List<string>();
                        if (utxos.SortSearch(output.Value.GetInternalValue(), excludedUtxoKeys, out MasterUTXO[] selectedUtxos, out long remainder))
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
                        tx.Outputs = outputs.ToArray();
                        tx.Inputs = inputs.ToArray();
                        tx = LockAssetHelper.Build(tx, avatars.ToArray());
                    }
                }
            }
            return tx;
        }


        private void st_self_ValueChanged(object sender, bool value)
        {
            this.tb_lock_pubkey.ReadOnly = this.st_self.Active;
            if (this.st_self.Active)
            {
                this.tb_lock_pubkey.Text = SecureHelper.MasterAccount.Key.PublicKey.EncodePoint(true).ToHexString();
            }
        }

        private void st_timelock_ValueChanged(object sender, bool value)
        {
            this.tb_lock_index.Visible = !this.st_timelock.Active;
            this.dp_lock_time.Visible = this.st_timelock.Active;
        }

        private void uiTabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            TabSelectedIndex = this.uiTabControl1.SelectedIndex;
        }

        private void uiTrackBar1_ValueChanged(object sender, EventArgs e)
        {
            var v = this.uiTrackBar1.Value;
            var b = (decimal)AvailableBalance;
            var m = v * b / 100;
            this.tb_amount.Text = ((uint)m).ToString();
        }
    }
}