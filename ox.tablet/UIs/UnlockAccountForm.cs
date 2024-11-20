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
    public partial class UnlockAccountForm : Form
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
        bool allowClose = false;
        public UnlockAccountForm()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("解锁账户", "Unlock Account");
            this.lb_pwd.Text = UIHelper.LocalString("密码:", "Password:");
            this.bt_unlock.Text = UIHelper.LocalString("解锁", "Unlock");
            this.bt_exit.Text = UIHelper.LocalString("退出", "Exit");
            this.tb_pwd.PasswordChar = '*';
            this.tb_pwd.Focus();
            this.AcceptButton = this.bt_unlock;
            this.setColor(this.bt_unlock, FocusColor);
        }

        private void bt_unlock_Click(object sender, EventArgs e)
        {
            if (!doUnlock())
                this.tb_pwd.Clear();
        }
        bool doUnlock()
        {
            bool ret = false;
            var pwd = this.tb_pwd.Text.Trim();
            if (pwd.IsNotNullAndEmpty())
            {
                var cpk = NodeConfig.Instance.CipherPriKey;
                if (EthAccount.TryBuildAccount(pwd, cpk, out EthAccount act))
                {
                    SecureHelper.SetAccount(act);
                    allowClose = true;
                    ret = true;
                    this.Close();
                }
            }
            return ret;
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
            Application.Exit();
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

        private void btn_0_Click(object sender, EventArgs e)
        {
            this.tb_pwd.Text += "0";
        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            this.tb_pwd.Text += "1";
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            this.tb_pwd.Text += "2";
        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            this.tb_pwd.Text += "3";
        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            this.tb_pwd.Text += "4";
        }

        private void btn_5_Click(object sender, EventArgs e)
        {
            this.tb_pwd.Text += "5";
        }

        private void btn_6_Click(object sender, EventArgs e)
        {
            this.tb_pwd.Text += "6";
        }

        private void btn_7_Click(object sender, EventArgs e)
        {
            this.tb_pwd.Text += "7";
        }

        private void btn_8_Click(object sender, EventArgs e)
        {
            this.tb_pwd.Text += "8";
        }

        private void btn_9_Click(object sender, EventArgs e)
        {
            this.tb_pwd.Text += "9";
        }
    }
}
