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
using OX.Tablet.Config;
using OX.Tablet.UIs;
using static OX.Tablet.NewMutualLockSellerTx;
using AntDesign;
using OX.Cryptography.ECC;
using OX.SmartContract;

namespace OX.Tablet
{
    public partial class NewDirectSaleOrder : UIForm
    {
        DirectSaleRequestData Data { get; set; }
        public NewDirectSaleOrder(DirectSaleRequestData data)
        {
            this.Data = data;
            InitializeComponent();
        }

        private void ClaimForm_Load(object sender, EventArgs e)
        {
            var assetState = Blockchain.Singleton.CurrentSnapshot.Assets.TryGet(this.Data.AssetId);
            this.lb_AssetName.Text = string.Empty;
            this.lb_balance.Text = UIHelper.LocalString("余额:", "Balance:");
            if (assetState.IsNotNull())
            {
                this.lb_AssetName.Text = UIHelper.LocalString($"资产:{assetState.GetName()}", $"Asset:{assetState.GetName()}");
                var balance = SecureHelper.GetMasterAvailableBalance(this.Data.AssetId);
                this.lb_balance.Text = UIHelper.LocalString($"余额:{balance}", $"Balance:{balance}");
            }
            var address = Contract.CreateSignatureRedeemScript(this.Data.Buyer).ToScriptHash().ToAddress();
            var dt = this.Data.TimeStamp.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss");
            this.Text = UIHelper.LocalString("挂单", "New Order");
            this.lb_amount.Text = UIHelper.LocalString("卖出金额:", "Sell Amount:");
            this.lb_address.Text = UIHelper.LocalString($"买家:{address}", $"Buyer:{address}");
            this.lb_time.Text = UIHelper.LocalString($"时间:{dt}", $"Buy Time:{dt}");
            this.btnOK.Text = UIHelper.LocalString("挂单", "New");
            this.btnCancel.Text = UIHelper.LocalString("取消", "Cancel");

            this.lb_delivery_expire.Text = UIHelper.LocalString($"锁定时间:", $"Lock Duration:");


            //this.cb_delivery_expire.Items.Add(new DeliveryExpire(UIHelper.LocalString("5分钟", "5 minutes"), 60 * 3));
            this.cb_delivery_expire.Items.Add(new DeliveryExpire(UIHelper.LocalString("6小时", "6 Hours"), 60 * 60 * 6));
            this.cb_delivery_expire.Items.Add(new DeliveryExpire(UIHelper.LocalString("24小时", "24 Hours"), 60 * 60 * 24));
            this.cb_delivery_expire.Items.Add(new DeliveryExpire(UIHelper.LocalString("72小时", "72 Hours"), 60 * 60 * 72));
            this.cb_delivery_expire.Items.Add(new DeliveryExpire(UIHelper.LocalString("30天", "30 Days"), 60 * 60 * 24 * 30));
            this.cb_delivery_expire.SelectedIndex = 0;
        }
        public MutualLockSellerTransaction BuildTx()
        {
            if (!uint.TryParse(this.tb_amount.Text, out var selleramount)) return default;

            var dle = this.cb_delivery_expire.SelectedItem as DeliveryExpire;

            var tx = new MutualLockSellerTransaction
            {
                Arbiter = casino.CasinoMasterAccountPubKey,
                Seller = SecureHelper.MasterAccount.Key.PublicKey,
                Buyer = this.Data.Buyer,
                AssetId = this.Data.AssetId,
                ApproveHash = OX.Cryptography.MutualLockHelper.CreateRandomApproveSource(),
                Amount = selleramount,
                ExpireTimestamp = System.DateTime.Now.ToTimestamp() + dle.Seconds,
                ApproveSource = OX.Cryptography.MutualLockHelper.CreateRandomApproveSource()
            };

            var amount = Fixed8.One * selleramount * 2;
            if (SecureHelper.GetMasterAvailableBalance(this.Data.AssetId) < amount) return default;
            List<TransactionOutput> outputs = new List<TransactionOutput>();
            TransactionOutput output = new TransactionOutput()
            {
                AssetId = this.Data.AssetId,
                ScriptHash = tx.GetContract().ScriptHash,
                Value = amount
            };
            outputs.Add(output);

            List<CoinReference> inputs = new List<CoinReference>();
            List<AvatarAccount> avatars = new List<AvatarAccount>();

            var utxos = SecureHelper.BlockIndex.GetMasterUtxos(this.Data.AssetId);
            if (utxos.IsNotNullAndEmpty())
            {
                List<string> excludedUtxoKeys = new List<string>();
                var totalAmt = outputs.Sum(m => m.Value);
                if (utxos.SortSearch(totalAmt.GetInternalValue(), excludedUtxoKeys, out MasterUTXO[] selectedUtxos, out long remainder))
                {
                    if (remainder > 0)
                        outputs.Add(new TransactionOutput { AssetId = this.Data.AssetId, ScriptHash = SecureHelper.MasterAccount.ScriptHash, Value = new Fixed8(remainder) });
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
        private void ClaimForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        //public DirectSaleReplyContent GetContent(UInt256 assetId)
        //{
        //    var text = this.rtb_remarks.Text.Trim();
        //    if (text.IsNullOrEmpty()) text = string.Empty;
        //    return new DirectSaleReplyContent { Amount = Fixed8.One * uint.Parse(this.tb_amount.Text), AssetId = assetId, Msg = text, ApproveSource = OX.Cryptography.MutualLockHelper.CreateRandomApproveSource() };
        //}



        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Clipboard.Clear();
            this.Close();
        }


    }
}
