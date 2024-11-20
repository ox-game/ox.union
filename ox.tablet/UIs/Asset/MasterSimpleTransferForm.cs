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
using System.Linq;
namespace OX.Tablet
{
    public partial class MasterSimpleTransferForm : BaseTransferForm
    {
        MasterAssetBalanceState MasterAssetBalanceState;
        Fixed8 AvailableBalance = Fixed8.Zero;
        public MasterSimpleTransferForm(MasterAssetBalanceState balanceState, UInt160 targetAddress = default)
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
            this.Text = UIHelper.LocalString("转账", "Transfer");
            this.lb_amount.Text = UIHelper.LocalString("金额:", "Amount:");
            this.lb_address.Text = UIHelper.LocalString("地址:", "Address:");
            this.btnOK.Text = UIHelper.LocalString("转账", "Transfer");
            this.btnCancel.Text = UIHelper.LocalString("取消", "Cancel");
            if (targetAddress.IsNotNull())
            {
                this.cb_contacts.Text = targetAddress.ToAddress();
                this.cb_contacts.Enabled = false;
            }
            else
            {
                ContactSet contactSet = default;
                try
                {
                    var cs = NodeConfig.Instance.Contacts;
                    contactSet = cs.HexToBytes().AsSerializable<ContactSet>();
                }
                catch
                {

                }
                if (contactSet.IsNotNull())
                {
                    cb_contacts.ValueMember = "Address";
                    cb_contacts.DisplayMember = "Display";
                    cb_contacts.DataSource = contactSet.Contacts;
                    cb_contacts.ShowClearButton = true;
                    cb_contacts.SelectedIndex = -1;
                }
            }
        }
        public override Transaction BuildTx()
        {
            Transaction tx = default;
            Contact contact = default;
            var sv = this.cb_contacts.SelectedValue;
            if (sv.IsNotNull() && sv is Contact ct)
            {
                contact = ct;
            }
            if (contact.IsNull())
            {
                var text = this.cb_contacts.Text.Trim();
                if (text.IsNotNullAndEmpty())
                {
                    if (uint.TryParse(text, out var memberId))
                    {
                        var bmsIndex = SecureHelper.BlockIndex.GetSubBlockIndex<BMSBlockIndex>();
                        var Member = bmsIndex.MarkMembers.Values?.Where(m => m.MarkMemberId == memberId).FirstOrDefault();
                        if (Member.IsNotNull())
                            contact = new Contact { Name = string.Empty, Kind = 3, Address = Member.Holder.ToAddress() };
                    }
                    else if (text.IsValidEthereumAddressHexFormat())
                    {
                        contact = new Contact { Name = string.Empty, Kind = 2, Address = text.ToLower() };
                    }
                    else
                    {
                        try
                        {
                            text.ToScriptHash();
                            contact = new Contact { Name = string.Empty, Kind = 1, Address = text };
                        }
                        catch
                        {

                        }
                    }
                }
            }
            if (contact.IsNotNull())
            {
                UInt160 sh = default;
                if (contact.Kind == 3)
                {
                    sh = contact.Address.ToScriptHash();
                }
                else if (contact.Kind == 2)
                {
                    sh = contact.Address.ToLower().BuildMapAddress();
                }
                else
                {
                    sh = contact.Address.ToScriptHash();
                }
                if (Fixed8.TryParse(this.tb_amount.Text, out Fixed8 amount) && amount > Fixed8.Zero && amount <= MasterAssetBalanceState.AvailableBalance)
                {
                    List<CoinReference> inputs = new List<CoinReference>();
                    List<AvatarAccount> avatars = new List<AvatarAccount>();
                    List<TransactionOutput> outputs = new List<TransactionOutput>();
                    TransactionOutput output = new TransactionOutput { AssetId = MasterAssetBalanceState.AssetId, ScriptHash = sh, Value = amount };
                    outputs.Add(output);
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

                        if (contact.Kind == 2)
                        {
                            tx = new EthereumMapTransaction
                            {
                                LockExpirationIndex = 0,
                                EthereumAddress = contact.Address,
                                EthMapContract = Blockchain.EthereumMapContractScriptHash,
                                Attributes = new TransactionAttribute[0],
                                Outputs = outputs.ToArray(),
                                Inputs = inputs.ToArray(),
                                Witnesses = new Witness[0]
                            };
                        }
                        else
                        {
                            tx = new ContractTransaction
                            {
                                Attributes = new TransactionAttribute[0],
                                Outputs = outputs.ToArray(),
                                Inputs = inputs.ToArray(),
                                Witnesses = new Witness[0]
                            };
                        }
                        tx = LockAssetHelper.Build(tx, avatars.ToArray());
                    }
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