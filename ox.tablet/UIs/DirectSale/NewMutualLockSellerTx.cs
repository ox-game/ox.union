using Akka.Actor;
using OX.IO.Actors;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Persistence;
using OX.Wallets;
using OX.Bapps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Org.BouncyCastle.Asn1.X509;
using System.Security.Claims;
using Akka.Actor.Dsl;
using Akka.Util;
using OX.Network.P2P;
using Sunny.UI;
using OX.Tablet.UIs.MarkSix;
using OX.DirectSales;
using OX.SmartContract;
using OX.Cryptography.ECC;
using OX.Tablet.Config;

namespace OX.Tablet
{
    public partial class NewMutualLockSellerTx : UIForm
    {
        public class DeliveryExpire
        {
            public DeliveryExpire(string title, uint seconds)
            {
                this.Title = title;
                this.Seconds = seconds;
            }
            public string Title;
            public uint Seconds;
            public override string ToString()
            {
                return Title.ToString();
            }
        }
        DirectSaleReply Reply;
        DirectSaleReplyContent Content;
        public NewMutualLockSellerTx(DirectSaleReply replay)
        {
            this.Reply = replay;
            InitializeComponent();

        }

        private void ClaimForm_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("新建交割单", "New delivery contract");
            if (!this.Reply.SellerVerify(SecureHelper.MasterAccount.Key, out Content)) this.Close();
            var assetState = Blockchain.Singleton.CurrentSnapshot.Assets.TryGet(Content.AssetId);
            if (assetState.IsNull()) this.Close();
            var buyerAddress = Contract.CreateSignatureRedeemScript(Reply.Buyer).ToScriptHash().ToAddress();
            this.lb_AssetName.Text = UIHelper.LocalString($"求购资产:{assetState.GetName()}", $"Asset:{assetState.GetName()}");
            this.lb_buyer_address.Text = UIHelper.LocalString($"买家地址: {buyerAddress}", $"Buyer Address: {buyerAddress}");
            this.lb_amount.Text = UIHelper.LocalString($"求购金额:{Content.Amount}", $"Amount:{Content.Amount}");
            this.lb_remarks.Text = Content.Msg;
            this.lb_notice.Text = UIHelper.LocalString($"交割单需要暂时质押双倍金额:{Content.Amount * 2}", $"Delivery will pledge double  amount:{Content.Amount * 2}");

            var balance = SecureHelper.GetMasterAvailableBalance(Content.AssetId);
            this.lb_balance.Text = UIHelper.LocalString($"可用余额:{balance}", $"Available Balance:{balance}");
            if (balance < Content.Amount * 2)
                this.btnOK.Enabled = false;
            this.btnOK.Text = UIHelper.LocalString("生成交割单", "New delivery");
            this.btnCancel.Text = UIHelper.LocalString("取消", "Cancel");
            this.lb_delivery_expire.Text= UIHelper.LocalString($"交割截止时间:", $"Delivery deadline:");
            this.cb_delivery_expire.Items.Add(new DeliveryExpire(UIHelper.LocalString("5分钟", "5 minutes"), 60 *5));
            this.cb_delivery_expire.Items.Add(new DeliveryExpire(UIHelper.LocalString("6小时", "6 Hours"), 60 * 60 * 6));
            this.cb_delivery_expire.Items.Add(new DeliveryExpire(UIHelper.LocalString("24小时", "24 Hours"), 60 * 60 * 24));
            this.cb_delivery_expire.Items.Add(new DeliveryExpire(UIHelper.LocalString("72小时", "72 Hours"), 60 * 60 * 72));
            this.cb_delivery_expire.Items.Add(new DeliveryExpire(UIHelper.LocalString("30天", "30 Days"), 60 * 60 * 24 * 30));
            this.cb_delivery_expire.SelectedIndex = 0;
        }

        private void ClaimForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        public MutualLockSellerTransaction BuildTx()
        {
            var dle = this.cb_delivery_expire.SelectedItem as DeliveryExpire;
            var approveHash = OX.Cryptography.MutualLockHelper.RebuildApproveHash(SecureHelper.MasterAccount.Key, Content.ApproveSource, out var approveCode);
            var tx = new MutualLockSellerTransaction
            {
                Arbiter = casino.CasinoMasterAccountPubKey,
                Seller = SecureHelper.MasterAccount.Key.PublicKey,
                Buyer = this.Reply.Buyer,
                AssetId = Content.AssetId,
                ApproveHash = approveHash,
                Amount = (uint)(Content.Amount.GetInternalValue() / Fixed8.D),
                ExpireTimestamp = System.DateTime.Now.ToTimestamp() + dle.Seconds,
                ApproveSource = Content.ApproveSource
            };

            var amount = Content.Amount * 2;

            List<TransactionOutput> outputs = new List<TransactionOutput>();
            TransactionOutput output = new TransactionOutput()
            {
                AssetId =Content.AssetId,
                ScriptHash = tx.GetContract().ScriptHash,
                Value = amount
            };
            outputs.Add(output);

            List<CoinReference> inputs = new List<CoinReference>();
            List<AvatarAccount> avatars = new List<AvatarAccount>();

            var utxos = SecureHelper.BlockIndex.GetMasterUtxos(Content.AssetId);
            if (utxos.IsNotNullAndEmpty())
            {
                List<string> excludedUtxoKeys = new List<string>();
                var totalAmt = outputs.Sum(m => m.Value);
                if (utxos.SortSearch(totalAmt.GetInternalValue(), excludedUtxoKeys, out MasterUTXO[] selectedUtxos, out long remainder))
                {
                    if (remainder > 0)
                        outputs.Add(new TransactionOutput { AssetId = Content.AssetId, ScriptHash = SecureHelper.MasterAccount.ScriptHash, Value = new Fixed8(remainder) });
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
            return tx;
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
