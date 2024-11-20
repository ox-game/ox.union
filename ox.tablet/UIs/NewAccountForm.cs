using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nethereum.Signer;
using Sunny.UI;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Model;
using System.Security.Principal;
using OX.Tablet.Config;
namespace OX.Tablet
{
    public partial class NewAccountForm : Form
    {
        EthAccount ExchangeAccount;
        OXAccount MasterAccount;
        bool allowClose = false;
        public NewAccountForm()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("新建账户", "New Account");
            this.lb_pwd.Text = UIHelper.LocalString("密码:", "Password:");
            this.lb_pwd_confirm.Text = UIHelper.LocalString("确认密码:", "Re-Password:");
            this.lb_prikey.Text = UIHelper.LocalString("私钥:", "Private Key:");
            this.lb_pubkey.Text = UIHelper.LocalString("公钥:", "Public Key:");
            this.lb_address.Text = UIHelper.LocalString("地址:", "Address:");
            this.bt_register.Text = UIHelper.LocalString("注册", "Register");
            this.bt_refresh.Text = UIHelper.LocalString("更换", "Refresh");
            this.bt_copy.Text = UIHelper.LocalString("复制", "Copy");
            this.bt_clip.Text = UIHelper.LocalString("粘贴", "Paste");
            this.bt_clear.Text = UIHelper.LocalString("清除", "Clear");
            this.bt_exit.Text = UIHelper.LocalString("退出", "Exit");
            resetPrivateKey();
        }
        void resetPrivateKey()
        {
            var key = EthECKey.GenerateKey();
            this.tb_privateKey.Text = key.GetPrivateKey();
        }
        void refreshAccount()
        {
            this.tb_privateKey.Text = this.ExchangeAccount.Key.GetPrivateKey();
            this.tb_pubkey.Text = this.MasterAccount.Key.PublicKey.EncodePoint(true).ToHexString();
            this.tb_address.Text = this.MasterAccount.Address;
        }
        private void bt_refresh_Click(object sender, EventArgs e)
        {
            resetPrivateKey();
        }

        private void bt_copy_Click(object sender, EventArgs e)
        {
            var prikeyHex = this.tb_privateKey.Text;
            Clipboard.SetText(prikeyHex);
        }

        private void tb_privateKey_TextChanged(object sender, EventArgs e)
        {
            ExchangeAccount = default;
            var prikeyHex = this.tb_privateKey.Text;
            try
            {
                var key = new EthECKey(prikeyHex);
                //var prikey = prikeyHex.HexToByteArray();
                var publickey = key.GetPubKey().ToHex(false);
                var address = key.GetPublicAddress().ToLower();
                ExchangeAccount = new EthAccount(address, publickey, key);
                MasterAccount = new OXAccount(ExchangeAccount.Key.GetPrivateKeyAsBytes());
                if (ExchangeAccount.IsNotNull() && MasterAccount.IsNotNull())
                    refreshAccount();
            }
            catch
            {

            }
        }

        private void bt_clip_Click(object sender, EventArgs e)
        {
            this.tb_privateKey.Text = Clipboard.GetText();
        }

        private void bt_clear_Click(object sender, EventArgs e)
        {
            this.tb_privateKey.Text = string.Empty;
        }

        private void bt_exit_Click(object sender, EventArgs e)
        {
            allowClose = true;
            Application.Exit();
        }

        private void bt_register_Click(object sender, EventArgs e)
        {
            var pwd = this.tb_pwd.Text.Trim();
            var pwd_confirm = this.tb_pwd_confirm.Text.Trim();
            if (pwd.IsNotNullAndEmpty() && pwd_confirm == pwd && this.ExchangeAccount.IsNotNull())
            {
                var bs = this.ExchangeAccount.Key.GetPrivateKeyAsBytes();
                NodeConfig.Instance.CipherPriKey = SecureHelper.Encrypt(pwd, bs).ToHexString(); ;
                NodeConfig.Instance.Save();
                SecureHelper.SetAccount(this.ExchangeAccount);
                allowClose = true;
                this.Close();
            }
        }

        private void NewAccountForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!allowClose)
                e.Cancel = true;
        }
    }
}
