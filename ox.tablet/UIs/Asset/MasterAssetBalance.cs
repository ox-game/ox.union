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
    public delegate void MasterAssetBalanceSelected(MasterAssetBalance assetBalance);
    public partial class MasterAssetBalance : UserControl
    {
        MasterAssetBalanceState BalanceState;
        IEnumerable<KeyValuePair<LockMiningAssetKey, LockMiningAssetReply>> LockMiningAssets = default;
        UIListBox Box;
        public event MasterAssetBalanceSelected AssetSelected;
        public MasterAssetBalance(MasterAssetBalanceState balanceState, MutualNode miner, IEnumerable<KeyValuePair<LockMiningAssetKey, LockMiningAssetReply>> lockAssets, UIListBox lb)
        {
            this.BalanceState = balanceState;
            this.LockMiningAssets = lockAssets;
            this.Box = lb;
            InitializeComponent();
            this.uiGroupBox1.Text = balanceState.AssetId.ToString();
            this.bt_transfer.Text = UIHelper.LocalString("转帐", "Transfer");
            this.bt_claim.Text = UIHelper.LocalString("提取OXC", "Claim OXC");
            this.lb_assetName.Text = UIHelper.LocalString($"资产名:{balanceState.AssetName}", $"Asset Name:{balanceState.AssetName}");
            this.lb_available_balance.Text = UIHelper.LocalString($"总余额:{balanceState.TotalBalance.ToString()}", $"Total Balance:{balanceState.TotalBalance.ToString()}");
            this.lb_master_balance.Text = UIHelper.LocalString($"主余额:{balanceState.MasterBalance.ToString()}", $"Master Balance:{balanceState.MasterBalance.ToString()}");
            this.lb_total_balance.Text = UIHelper.LocalString($"可用余额:{balanceState.AvailableBalance.ToString()}", $"Available Balance:{balanceState.AvailableBalance.ToString()}");
            this.uiGroupBox1.RectColor = Color.LightSkyBlue;
            this.bt_reg_miner.Text = UIHelper.LocalString("注册矿机", "Register Miner");
            this.bt_reg_miner.Visible = miner.IsNull() && SecureHelper.IsSeniorRunMode && balanceState.AssetId == Blockchain.OXC;
            this.bt_self_lock.Text = UIHelper.LocalString("锁仓挖矿", "Lock Mining");
            this.bt_self_lock.Visible = miner.IsNotNull() && SecureHelper.IsAuthenticated && this.LockMiningAssets.IsNotNullAndEmpty() && this.LockMiningAssets.Select(m => m.Key.AssetId).Contains(this.BalanceState.AssetId);
            this.bt_claim.Visible = this.BalanceState.AssetId == Blockchain.OXS;
        }

        private void bt_transfer_Click(object sender, EventArgs e)
        {
            BaseTransferForm frm = SecureHelper.IsSeniorRunMode ? new MasterSeniorTransferForm(BalanceState) : new MasterSimpleTransferForm(BalanceState);
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
                string s = string.Empty;
                if (item.IsTimeLock)
                    s = UIHelper.LocalString($"金额:{item.Output.Value}   |    解锁时间:{item.LockExpirationIndex.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss")}", $"Amount:{item.Output.Value}   |    Unlock Time:{item.LockExpirationIndex.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss")}");
                else
                    s = UIHelper.LocalString($"金额:{item.Output.Value}   |    解锁区块:{item.LockExpirationIndex}", $"Amount:{item.Output.Value}   |    Unlock Block Height:{item.LockExpirationIndex}");
                this.Box.Items.Add(s);
            }
        }
        public void UnSelect()
        {
            this.uiGroupBox1.RectColor = Color.LightSkyBlue;
        }

        private void bt_reg_miner_Click(object sender, EventArgs e)
        {
            var frm = new RegMinerForm(BalanceState);
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

        private void bt_self_lock_Click(object sender, EventArgs e)
        {
            var frm = new SelfLockMiningForm(BalanceState);
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

        private void bt_claim_Click(object sender, EventArgs e)
        {
            new SingleClaimOXC().ShowDialog();
        }
    }
}
