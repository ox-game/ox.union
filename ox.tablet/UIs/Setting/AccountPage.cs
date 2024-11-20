using Akka.IO;
using Akka.Actor;
using Microsoft.AspNetCore.Http.HttpResults;
using Org.BouncyCastle.Bcpg;
using OX.BMS;
using OX.DirectSales;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Wallets;
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
using Org.BouncyCastle.Utilities.Encoders;
using OX.Tablet.Config;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class AccountPage : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));


        public AccountPage()
        {
            InitializeComponent();
            this.bt_delete_account.Text = UIHelper.LocalString("删除账户", "Delete Account");
            this.bt_copy_ethaddress.Text = UIHelper.LocalString("复制", "Copy");
            this.bt_copy_masteraddress.Text = UIHelper.LocalString("复制", "Copy");
            this.bt_copy_masterpubkey.Text = UIHelper.LocalString("复制", "Copy");
            this.bt_backup_prikey.Text = UIHelper.LocalString("备份私钥", "Backup key");

            this.lb_master_address.Text = UIHelper.LocalString("主账户地址:", "Master Address:");
            this.lb_master_pubkey.Text = UIHelper.LocalString("主账户公钥:", "Master Public Key:");
            this.lb_exchange_address.Text = UIHelper.LocalString("汇兑账户地址:", "Exchange Address:");
            this.L0.Text = UIHelper.LocalString("账户基本信息", "Basic Account Information");
            this.L2.Text = UIHelper.LocalString("矿机信息", "Miner  Information");
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

        }

        public virtual void MenuPageSelected()
        {
            if (SecureHelper.IsAuthenticated)
            {
                this.tb_master_address.Text = SecureHelper.MasterAccount.Address;
                this.tb_master_pubkey.Text = SecureHelper.MasterAccount.Key.PublicKey.EncodePoint(true).ToHexString();
                this.tb_exchange_address.Text = SecureHelper.ExchangeAccount.EthAddress.ToLower();
              
                this.lb_seed_miner.Text = string.Empty;
                if (SecureHelper.BlockIndex.IsNotNull() )
                {
                    var miningIndex = SecureHelper.BlockIndex.GetSubBlockIndex<MiningBlockIndex>();
                    if (miningIndex.MutualLockNodes.TryGetValue(SecureHelper.MasterAccount.ScriptHash.GetMutualLockSeed(), out var miner))
                    {
                        var rm = miner.RegIndex % 100000;
                        var rem = rm;
                        var rem2 = Blockchain.Singleton.Height % 100000;
                        if (rem2 > rem) rem += 100000;
                        var sub = rem - rem2;
                        var s = Mining.MasterAccountAddress.Equals(miner.ParentHolder) ? UIHelper.LocalString("根", "Root ") : string.Empty;
                        this.lb_seed_miner.Text = UIHelper.LocalString($"种子矿机:{miner.ParentHolder.ToAddress()}", $"Seed Miner:{miner.ParentHolder.ToAddress()}");
                    }
                }
            }
        }

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



        private void OrderHistory_Initialize(object sender, EventArgs e)
        {

        }

        private void bt_delete_account_Click(object sender, EventArgs e)
        {
            new ConfirmDeleteAccountForm().ShowDialog();
        }

        private void bt_backup_prikey_Click(object sender, EventArgs e)
        {
            new BackupPrivateKeyForm().ShowDialog();
        }

        private void bt_copy_masteraddress_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(SecureHelper.MasterAccount.Address);
            this.ShowSuccessTip(SecureHelper.MasterAccount.Address);
        }

        private void bt_copy_masterpubkey_Click(object sender, EventArgs e)
        {
            var hex = SecureHelper.MasterAccount.Key.PublicKey.EncodePoint(true).ToHexString();
            Clipboard.SetText(hex);
            this.ShowSuccessTip(hex);
        }

        private void bt_copy_ethaddress_Click(object sender, EventArgs e)
        {
            var ethaddress = SecureHelper.ExchangeAccount.EthAddress.ToLower();
            Clipboard.SetText(ethaddress);
            this.ShowSuccessTip(ethaddress);
        }

        private void bt_charge_basicAgent_Click(object sender, EventArgs e)
        {
            //new MemberRechargeForm(MarkSixRentType.BasicAgent).ShowDialog();
        }

        private void bt_charge_seniorAgent_Click(object sender, EventArgs e)
        {
            //new MemberRechargeForm(MarkSixRentType.SeniorAgent).ShowDialog();
        }

        private void bt_charge_basicPort_Click(object sender, EventArgs e)
        {
            //new MemberRechargeForm(MarkSixRentType.BasicPort).ShowDialog();
        }

        private void bt_charge_seniorPort_Click(object sender, EventArgs e)
        {
            //new MemberRechargeForm(MarkSixRentType.SeniorPort).ShowDialog();
        }
    }
}
