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
    public partial class SyncLockForm : Form
    {
        public bool allowClose = false;
        MainForm ParentForm;
        public SyncLockForm(MainForm form)
        {
            ParentForm = form;
            InitializeComponent();
            this.Text = UIHelper.LocalString("正在同步区块链数据", "Syncing blockchain data");
            this.bt_exit.Text = UIHelper.LocalString("退出", "Exit");
            this.bt_confirm_seed.Text = UIHelper.LocalString("种子节点", "Seed Node");
            this.lb_msg.Text = UIHelper.LocalString("请耐心等待完成同步", "Please wait...");
        }

        public void SetProgress(string msg)
        {
            this.lb_pwd.Text = msg;
        }

        private void UnlockAccountForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!allowClose)
                e.Cancel = true;
        }

        private void bt_exit_Click(object sender, EventArgs e)
        {
            allowClose = true;
            this.ParentForm.NeedExit = true;
            this.Close();
            Application.Exit();
        }

        private void bt_confirm_seed_Click(object sender, EventArgs e)
        {
            new SeedManageForm().ShowDialog();
        }
    }
}
