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
namespace OX.Tablet.FlashMessages
{
    public partial class NoticeInfo : UserControl
    {
        public NoticeInfo(uint timeStamp,string msg)
        {
            InitializeComponent();
            //this.Dock = DockStyle.Fill;
            this.uiGroupBox1.Text = timeStamp.ToDateTime().ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            this.lb_msg.Text = msg;
        }
        private void lb_master_balance_Click(object sender, EventArgs e)
        {
        }
    }
}
