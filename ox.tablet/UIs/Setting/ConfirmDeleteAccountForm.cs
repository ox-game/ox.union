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
    public partial class ConfirmDeleteAccountForm : Form
    {
        bool allowClose = false;
        public ConfirmDeleteAccountForm()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("删除账户", "Delete Account");
            this.lb_pwd.Text = UIHelper.LocalString("密码:", "Password:");
            this.bt_unlock.Text = UIHelper.LocalString("删除", "Delete");
            this.bt_exit.Text = UIHelper.LocalString("取消", "Cancel");
            this.tb_pwd.PasswordChar = '*';
            this.tb_pwd.Focus();
            this.AcceptButton = this.bt_unlock;
        }

        private void bt_unlock_Click(object sender, EventArgs e)
        {
            doUnlock();
        }
        void doUnlock()
        {
            var pwd = this.tb_pwd.Text.Trim();
            if (pwd.IsNotNullAndEmpty())
            {
                var cpk = NodeConfig.Instance.CipherPriKey;
                if (EthAccount.TryBuildAccount(pwd, cpk, out EthAccount act))
                {
                    NodeConfig.Instance.CipherPriKey = string.Empty;
                    NodeConfig.Instance.Save();
                    allowClose = true;
                    Application.Exit();
                }
            }
        }

        private void tb_pwd_KeyDown(object sender, KeyEventArgs e)
        {

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
    }
}
