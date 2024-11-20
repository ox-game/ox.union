using Akka.IO;
using Akka.Actor;
using Microsoft.AspNetCore.Http.HttpResults;
using Org.BouncyCastle.Bcpg;
using OX.BMS;
using OX.DirectSales;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Sunny.UI.Win32;
using OX.Tablet.Config;
using static OX.Network.P2P.LocalNode;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class SellerPage : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));


        public SellerPage()
        {
            InitializeComponent();
            this.L1.Text = UIHelper.LocalString("我的卖单", "My sales orders");
            this.bt_refresh.Text = UIHelper.LocalString("刷新", "Refresh");
            this.bt_advance.Text = UIHelper.LocalString("挂单", "New Order");
        }
        public void HeartBeat(HeartBeatContext beatContext)
        {

        }
        public void BeforeBlock(Block block)
        {

        }
        public void OnBlock(Block block)
        {

        }
        public void AfterBlock(Block block)
        {

        }
        public void OnFlashMessage(FlashMessage message)
        {

        }
        public void OnClipboardString(ClipboardMessageType messageType, string msg)
        {
            //if (messageType == ClipboardMessageType.DirectSaleReply)
            //{
            //    if (DirectSaleReply.SellerTryParser(SecureHelper.MasterAccount.Key, msg, out var reply, out var content) && reply.Seller.Equals(SecureHelper.MasterAccount.Key.PublicKey))
            //    {
            //        ShowAndNewMutualLockSellerTx(reply);
            //    }
            //}
        }
        void ShowAndNewMutualLockSellerTx(DirectSaleReply reply)
        {
            Clipboard.Clear();
            using (var Dialog = new NewMutualLockSellerTx(reply))
            {
                if (Dialog.ShowDialog() == DialogResult.OK)
                {
                    var tx = Dialog.BuildTx();
                    if (tx.IsNotNull())
                    {
                        var sh = tx.GetContract().ScriptHash;
                        var mutualLockState = Blockchain.Singleton.CurrentSnapshot.MutualLockStates.TryGet(sh);
                        if (mutualLockState.IsNull())
                        {
                            Program.BlockHandler.Tell(tx);
                            foreach (var coin in tx.Inputs)
                            {
                                SecureHelper.BlockIndex.UnconfirmCoins[coin] = Blockchain.Singleton.Height + 100;
                            }
                            new WaitTxForm(tx, UIHelper.LocalString("等待交割单确认...", "Waiting  confirm delivery transaction")).ShowDialog();
                        }
                        else
                        {
                            this.ShowErrorTip(UIHelper.LocalString("不能重复交割", "Cannot repeat delivery"));
                        }
                    }
                }
            }
        }
        public virtual void MenuPageSelected() { ReloadRecord(); }

        void setColor(UIButton control, Color color)
        {
            control.FillColor = color;
            control.FillColor2 = color;
            control.FillHoverColor = color;
            control.FillPressColor = color;
            control.FillSelectedColor = color;
            control.RectColor = color;
            control.RectHoverColor = color;
            control.RectPressColor = color;
            control.RectSelectedColor = color;
        }

        void ReloadRecord()
        {
            this.DoInvoke(() =>
            {
                this.pn_pairs.Clear();
                if (SecureHelper.BlockIndex.IsNotNull())
                {
                    var directSaleIndex = SecureHelper.BlockIndex.GetSubBlockIndex<DirectSaleBlockIndex>();
                    foreach (var ml in directSaleIndex.MutualLockRecords.Where(m => m.Value.Key.IsSeller).OrderByDescending(m => m.Value.Key.TimeStamp))
                    {
                        this.pn_pairs.Add(new MutualLockSellerInfo(this, ml.Value));
                    }
                }
            });
        }

        private void OrderHistory_Initialize(object sender, EventArgs e)
        {

        }

        private void bt_refresh_Click(object sender, EventArgs e)
        {
            ReloadRecord();
        }

        private void bt_advance_Click(object sender, EventArgs e)
        {
            var str = Clipboard.GetText();
            if (DirectSaleRequest.TryParse(SecureHelper.MasterAccount.Key.PublicKey, str, out var data))
            {
                using (var Dialog = new NewDirectSaleOrder(data))
                {
                    if (Dialog.ShowDialog() == DialogResult.OK)
                    {
                        var tx = Dialog.BuildTx();
                        if (tx.IsNotNull())
                        {
                            var sh = tx.GetContract().ScriptHash;
                            var mutualLockState = Blockchain.Singleton.CurrentSnapshot.MutualLockStates.TryGet(sh);
                            if (mutualLockState.IsNull())
                            {
                                Program.BlockHandler.Tell(tx);
                                foreach (var coin in tx.Inputs)
                                {
                                    SecureHelper.BlockIndex.UnconfirmCoins[coin] = Blockchain.Singleton.Height + 100;
                                }
                                new WaitTxForm(tx, UIHelper.LocalString("等待新挂单确认...", "Waiting  confirm new order transaction")).ShowDialog();
                            }
                            else
                            {
                                this.ShowErrorTip(UIHelper.LocalString("挂单重复", "Duplicate sales orders"));
                            }
                        }
                    }
                }
            }
            else
            {
                this.ShowErrorTip(UIHelper.LocalString("买单无效", "buy order invalid"));
            }          
        }
    }
}
