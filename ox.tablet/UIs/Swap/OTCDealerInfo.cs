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
using OX.Wallets;
using OX.Tablet.Config;

namespace OX.Tablet
{

    public partial class OTCDealerInfo : UserControl
    {
        OTCDealerMerge OTCDealerMerge;
        Fixed8 Balance;
        public OTCDealerInfo(OTCDealerMerge otcDealerMerge, Fixed8 balance)
        {
            this.OTCDealerMerge = otcDealerMerge;
            this.Balance = balance;
            InitializeComponent();
            this.uiGroupBox1.RectColor = Color.LightSkyBlue;
            this.uiGroupBox1.Text = otcDealerMerge.InPoolAddress.ToAddress();
            this.bt_transfer.Text = UIHelper.LocalString("去入金", "Go deposit");
            this.lb_usdt_balance.Text = UIHelper.LocalString($"USDT 余额:{balance.ToString()}", $"USDT Balance:{balance.ToString()}");
            this.lb_state.Text = UIHelper.LocalString($"状态:{OTCDealerMerge.Setting.State.StringValue()}", $"State:{OTCDealerMerge.Setting.State.EngStringValue()}");
            this.lb_fee.Text = UIHelper.LocalString($"手续费率:{OTCDealerMerge.Setting.InRate}%", $"Fee Ratio:{OTCDealerMerge.Setting.InRate}%");

        }

        private void bt_transfer_Click(object sender, EventArgs e)
        {
            new DepositForm(OTCDealerMerge).ShowDialog();
        }





        private void lb_master_balance_Click(object sender, EventArgs e)
        {

        }

        private void bt_transfer_ClientSizeChanged(object sender, EventArgs e)
        {

        }
    }
}
