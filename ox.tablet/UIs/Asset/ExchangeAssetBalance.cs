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
using OX.Tablet.Config;

namespace OX.Tablet
{
    public delegate void ExchangeAssetBalanceSelected(ExchangeAssetBalance assetBalance);
    public partial class ExchangeAssetBalance : UserControl
    {
        EthAssetBalanceState BalanceState;
        UIListBox Box;
        public event ExchangeAssetBalanceSelected AssetSelected;
        public ExchangeAssetBalance(EthAssetBalanceState balanceState, UIListBox lb)
        {
            this.BalanceState = balanceState;
            this.Box = lb;
            InitializeComponent();
            this.uiGroupBox1.Text = balanceState.AssetId.ToString();
            this.bt_transfer.Text = UIHelper.LocalString("转帐", "Transfer");
            this.lb_assetName.Text = UIHelper.LocalString($"资产名:{balanceState.AssetName}", $"Asset Name:{balanceState.AssetName}");
            this.lb_available_balance.Text = UIHelper.LocalString($"总余额:{balanceState.TotalBalance.ToString()}", $"Total Balance:{balanceState.TotalBalance.ToString()}");
            this.lb_master_balance.Text = UIHelper.LocalString($"主余额:{balanceState.MasterBalance.ToString()}", $"Master Balance:{balanceState.MasterBalance.ToString()}");
            this.lb_total_balance.Text = UIHelper.LocalString($"可用余额:{balanceState.AvailableBalance.ToString()}", $"Available Balance:{balanceState.AvailableBalance.ToString()}");
            this.uiGroupBox1.RectColor = Color.LightSkyBlue;
        }

        private void bt_transfer_Click(object sender, EventArgs e)
        {
            var frm = new ExchangeSimpleTransferForm(BalanceState);
            frm.Render();
            frm.ShowDialog();
            if (frm.IsOK)
            {
                var tx = frm.BuildTx();
                if (tx.IsNotNull())
                {
                    Program.BlockHandler.Tell(tx);
                    foreach (var coin in tx.Inputs)
                    {
                        SecureHelper.BlockIndex.UnconfirmCoins[coin] = Blockchain.Singleton.Height + 100;
                    }
                    new WaitTxForm(tx, UIHelper.LocalString("等待交易确认...", "Waiting  confirm transaction")).ShowDialog();
                }
            }
            frm.Dispose();
        }





        private void lb_master_balance_Click(object sender, EventArgs e)
        {
            this.uiGroupBox1.RectColor = Color.Red;
            this.AssetSelected?.Invoke(this);
            this.Box.Items.Clear();
            foreach (var item in this.BalanceState.OMS)
            {
                var s = UIHelper.LocalString($"金额:{item.Output.Value}   |    解锁区块:{item.LockExpirationIndex}", $"Amount:{item.Output.Value}   |    Unlock Block Height:{item.LockExpirationIndex}");
                this.Box.Items.Add(s);
            }
        }
        public void UnSelect()
        {
            this.uiGroupBox1.RectColor = Color.LightSkyBlue;
        }
    }
}
