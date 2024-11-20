using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Network.P2P;
using Sunny.UI;
using Akka.Actor;
using OX.Ledger;
using OX.SmartContract;
using OX.Wallets;
using OX.Tablet.UIs.MarkSix;
using OX.DirectSales;
using OX.IO;
using OX.Tablet.Config;
using System.Security.Cryptography;

namespace OX.Tablet
{

    public partial class DirectSalePublishInfo : UserControl
    {
        MarketPlacePage ParentPage;
        DirectSalePublishMerge DirectSalePublishMerge;
        string AssetName;

        public DirectSalePublishInfo(MarketPlacePage parentPage, DirectSalePublishMerge dspMerger, string assetName)
        {
            this.ParentPage = parentPage;
            this.DirectSalePublishMerge = dspMerger;
            this.AssetName = assetName;
            InitializeComponent();
            this.uiGroupBox1.Text = assetName;
            var from = Contract.CreateSignatureRedeemScript(dspMerger.Tx.From).ToScriptHash().ToAddress();
            this.lb_seller.Text = UIHelper.LocalString($"卖家:{from}", $"Seller:{from}");
            this.lb_timestamp.Text = dspMerger.TimeStamp.ToDateTime().ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            this.lb_contact.Text = dspMerger.Publish.Contact;
            this.lb_remarks.Text = dspMerger.Publish.Remarks;
            this.bt_show_balance.Text = UIHelper.LocalString("查看余额", "Balance");
            this.bt_copy_contact.Text = UIHelper.LocalString("复制联系方式", "Copy Contact");
            this.bt_create_order.Text = UIHelper.LocalString("制单", "Create Order");
        }

        private void bt_create_order_Click(object sender, EventArgs e)
        {
            var request = new DirectSaleRequest(DirectSalePublishMerge.Publish.AssetId, DirectSalePublishMerge.Tx.From, SecureHelper.MasterAccount.Key);
            var hex = "??" + request.ToArray().ToHexString();
            Clipboard.SetText(hex);
            var msg = UIHelper.LocalString("买单已生成并复制，请粘贴并发送给卖家", "Order generated and copied. Please paste and send it to the seller");
            this.ParentPage.ShowSuccessTip(msg);
        }





        private void lb_master_balance_Click(object sender, EventArgs e)
        {

        }


        private void bt_reg_miner_Click(object sender, EventArgs e)
        {
            Fixed8 balance = Fixed8.Zero;
            var sh = Contract.CreateSignatureRedeemScript(DirectSalePublishMerge.Tx.From).ToScriptHash();
            var act = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(sh);
            if (act.IsNotNull())
            {
                balance = act.GetBalance(DirectSalePublishMerge.Publish.AssetId);
            }
            var msg = $"{AssetName} : {balance}";
            this.ParentPage.ShowSuccessTip(msg);
        }

        private void bt_self_lock_Click(object sender, EventArgs e)
        {
            if (DirectSalePublishMerge.Publish.IsNotNull() && DirectSalePublishMerge.Publish.Contact.IsNotNullAndEmpty())
            {
                Clipboard.SetText(DirectSalePublishMerge.Publish.Contact);
                var msg = UIHelper.LocalString("卖家联系方式已复制", "Seller contact copied");
                this.ParentPage.ShowSuccessTip(msg);
            }
        }

    }
}
