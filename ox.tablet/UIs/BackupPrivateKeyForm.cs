using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Ledger;
using OX.Tablet.Config;
using Sunny.UI;

namespace OX.Tablet
{
    public partial class BackupPrivateKeyForm : Form
    {
        bool allowClose = false;
        public BackupPrivateKeyForm()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("备份私钥", "Backup Private Key");
            this.lb_pwd.Text = UIHelper.LocalString("密码:", "Password:");
            this.bt_copy.Text = UIHelper.LocalString("复制", "Copy");
            this.bt_show.Text = UIHelper.LocalString("查看", "View");
            this.bt_exit.Text = UIHelper.LocalString("关闭", "Close");
            this.tb_pwd.PasswordChar = '*';
            this.tb_pwd.Focus();
        }


        void doUnlock()
        {
            var pwd = this.tb_pwd.Text.Trim();
            if (pwd.IsNotNullAndEmpty())
            {
                var cpk = NodeConfig.Instance.CipherPriKey;
                if (EthAccount.TryBuildAccount(pwd, cpk, out EthAccount act))
                {
                    this.tb_prikey.Text = act.Key.GetPrivateKey();
                }
            }
        }

        private void tb_pwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                doUnlock();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void UnlockAccountForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!allowClose)
                e.Cancel = true;
        }

        private void bt_exit_Click(object sender, EventArgs e)
        {
            allowClose = true;
            this.Close();
        }

        private void bt_show_Click(object sender, EventArgs e)
        {
            doUnlock();
        }

        private void bt_copy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.tb_prikey.Text);
            this.ShowSuccessTip(this.tb_prikey.Text);
        }
    }
}
