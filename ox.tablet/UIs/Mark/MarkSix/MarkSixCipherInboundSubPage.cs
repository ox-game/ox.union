using Akka.IO;
using Akka.Actor;
using Microsoft.AspNetCore.Http.HttpResults;
using Org.BouncyCastle.Bcpg;
using OX.BMS;
using OX.DirectSales;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Sunny.UI.Win32;
using OX.Tablet.UIs.Mark;
using OX.Tablet.Config;
using OX.Tablet.UIs.Mark.MarkSix;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class MarkSixCipherInboundSubPage : BaseBoundView
    {

        MarkSixPlazaForm ParentPage;
        public MarkSixCipherInboundSubPage(MarkSixPlazaForm parentPage)
        {
            this.ParentPage = parentPage;
            InitializeComponent();
            this.setColor(this.bt_go_bet, FocusColor);
            this.Text = UIHelper.LocalString("入库订单", "Inbound Orders");
            this.bt_collapse.Text = UIHelper.LocalString("收缩", "Collapse");
            this.bt_delete.Text = UIHelper.LocalString("删单", "Delete");
            this.bt_go_bet.Text = UIHelper.LocalString("双击出单", "2-hit Order");
            this.bt_go_bet.UseDoubleClick = true;
            this.bt_go_bet.DoubleClick += Bt_go_bet_DoubleClick;
            this.bt_go_bet.Enabled = SecureHelper.IsAgent() || SecureHelper.IsPort();
        }

        private void Bt_go_bet_DoubleClick(object sender, EventArgs e)
        {
            var agentIndex = SecureHelper.BlockIndex.GetSubBlockIndex<AgentBlockIndex>();
            List<TreeNode> orders = new List<TreeNode>();
            foreach (TreeNode node in this.tv_inbound_orders.Nodes)
                foreach (TreeNode subnode in node.Nodes)
                    if (!subnode.Checked)
                    {
                        if (subnode.Tag is MarkInboundOrder order)
                            orders.Add(subnode);
                    }
            if (this.ParentPage.ChannelRound.Channel == BetChannel.MarkSix)
            {
                if (this.ParentPage.ChannelRound.Round == (byte)MarkSixRound.MarkUnion)
                {
                    new MarkSixPostCipherOrderForm(orders.ToArray(), this.ParentPage).ShowDialog();
                   
                }
                else
                {
                    new MarkSixPostPlainOrderForm(orders.ToArray(), this.ParentPage).ShowDialog();
                }
            }
        }

        public override void HeartBeat(HeartBeatContext beatContext)
        {

        }
        public override void BeforeBlock(Block block)
        {

        }
        public override void OnBlock(Block block)
        {

        }
        public override void AfterBlock(Block block)
        {

        }
        public override void OnClipboardString(ClipboardMessageType messageType, string msg)
        {
            if (messageType == ClipboardMessageType.MarkOrder)
                ReloadOrders();
        }
        public override void ReloadOrders()
        {
            var builder = new ManualBoundBuilder(this.ParentPage.ChannelRound);
            this.tv_inbound_orders.Nodes.Clear();

            GuessAnswer answer = default;
            var member = SecureHelper.GetMarkMember();
            if (member.IsNotNull())
            {
                GuessAnswerKey gak = new GuessAnswerKey
                {
                    ChannelRound = this.ParentPage.ChannelRound,
                    Term = this.ParentPage.CurrentSelectedTerm
                };
                var bmsIndex = SecureHelper.BlockIndex.GetSubBlockIndex<BMSBlockIndex>();
                if (bmsIndex.GuessAnswers.TryGetValue(gak.ToString(), out answer))
                {

                }
            }

            foreach (var g in builder.GetMarkInboundOrders(SecureHelper.MasterAccount.ScriptHash, this.ParentPage.CurrentSelectedTerm, this.ParentPage.ChannelRound).GroupBy(m => m.Key.InboundOrigin))
            {
                var names = g.Key.GetName();
                var originName = UIHelper.LocalString(names.Name, names.EngName);
                TreeNode originNode = new TreeNode(originName);
                originNode.Tag = g.Key;
                foreach (var order in g.OrderBy(m => m.Value.State))
                {
                    uint prize = 0;
                    if (member.IsNotNull() && answer.IsNotNull())
                        prize = member.MarkSetting.CheckBet(answer, order.Value.BetItems);

                    TreeNode orderNode = new TreeNode { Text = $"{order.Value.FromName}#{order.Value.Amount}#{order.Key.CNO.ToString()}#{prize}" };
                    orderNode.Tag = new MarkInboundOrder { OrderHead = order.Key, OrderBody = order.Value };
                    orderNode.Checked = order.Value.State == 1;
                    foreach (var betItem in order.Value.BetItems)
                    {
                        MarkSixBetMethod m = (MarkSixBetMethod)betItem.BetTarget.Method;
                        var methodSetting = m.GetMethodSetting();
                        var name = UIHelper.LocalString(methodSetting.Name, methodSetting.EngName);
                        var str = betItem.BetTarget.BetPoint.ToDisplayString(this.ParentPage.ChannelRound.Channel, betItem.BetTarget.Method, UIHelper.IsChina());
                        var texstr = $"{name}[{str}]:{betItem.Amount}";
                        TreeNode subnode = new TreeNode { Text = texstr };
                        subnode.Checked = order.Value.State == 1;
                        orderNode.Nodes.Add(subnode);
                    }
                    originNode.Nodes.Add(orderNode);
                }
                this.tv_inbound_orders.Nodes.Add(originNode);
            }
        }
        public override void MenuPageSelected()
        {
            ReloadOrders();
        }



        private void OrderHistory_Initialize(object sender, EventArgs e)
        {

        }

        private void bt_refresh_Click(object sender, EventArgs e)
        {

        }

        private void bt_paste_Click(object sender, EventArgs e)
        {

        }

        private void bt_collapse_Click(object sender, EventArgs e)
        {
            this.tv_inbound_orders.CollapseAll();
        }

        private void tv_inbound_orders_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (!BetUIHelper.AllowOrderSwitchCheck)
                e.Cancel = true;
        }

        private void bt_delete_Click(object sender, EventArgs e)
        {
            var node = tv_inbound_orders.SelectedNode;
            if (node.IsNotNull() && node.Level == 1 && !node.Checked && node.Tag is MarkInboundOrder order)
            {
                var agentIndex = SecureHelper.BlockIndex.GetSubBlockIndex<AgentBlockIndex>();
                agentIndex.DeleteMarkInboundOrder(order.OrderHead);
                ReloadOrders();
            }
        }


    }
}
