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
using Sunny.UI;
using OX.Persistence;
using OX.Network.P2P.Payloads;
using System.Threading;
using OX.Tablet.Config;

namespace OX.Tablet
{
    public partial class WaitTxForm : Form
    {
        Transaction Tx;
        int count = 0;
        bool success = false;
        Action<bool> Action;
        public WaitTxForm(Transaction tx, string title, Action<bool> action = default)
        {
            Tx = tx;
            this.Action = action;
            InitializeComponent();
            this.Text = UIHelper.LocalString("正在确认交易...", "Confirming transaction...");
            this.lb_title.Text = title;
            this.bt_close.Text = UIHelper.LocalString("关闭", "Close");
        }

        public void SetProgress(string msg)
        {
            this.lb_msg.Text = msg;
        }

        private void UnlockAccountForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void bt_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (count == 0)
            {
                using (var snapshot = Blockchain.Singleton.GetSnapshot())
                {
                    var confirmTx = snapshot.Transactions.TryGet(this.Tx.Hash);
                    if (confirmTx.IsNotNull())
                    {
                        success = true;
                        count++;
                        this.uiWaitingBar1.Stop();
                        this.lb_msg.Text = UIHelper.LocalString("交易成功", "Transaction Completed");
                    }
                }
            }
            else
            {
                count++;
                if (count >= 8)
                {
                    this.Close();
                    if (this.Action.IsNotNull())
                        this.Action(success);
                }
            }
        }
    }
}
