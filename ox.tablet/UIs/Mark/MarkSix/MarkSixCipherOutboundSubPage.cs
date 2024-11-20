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
using OX.IO;
using OX.Tablet.UIs.Mark.MarkSix;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class MarkSixCipherOutboundSubPage : BaseBoundView
    {

        MarkSixPlazaForm ParentPage;
        public MarkSixCipherOutboundSubPage(MarkSixPlazaForm parentPage)
        {
            this.ParentPage = parentPage;
            InitializeComponent();
            this.Text = UIHelper.LocalString("出库订单", "Outbound Orders");
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

        }
        public override void ReloadOrders()
        {
            var builder = new ManualBoundBuilder(this.ParentPage.ChannelRound);
            this.tv_outbound_orders.Nodes.Clear();
            GuessAnswer answer = default;
            var member = SecureHelper.GetMarkAdminMember();
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

            var agentIndex = SecureHelper.BlockIndex.GetSubBlockIndex<AgentBlockIndex>();
            if (agentIndex.TermCipherOrders.TryGetValue(this.ParentPage.CurrentSelectedTerm, out var list))
            {
                if (list.IsNotNullAndEmpty())
                {
                    foreach (var g in list.GroupBy(m => m.Value.State))
                    {
                        var names = g.Key.GetName();
                        TreeNode roundNode = new TreeNode(UIHelper.LocalString(names.Name, names.EngName));
                        foreach (var orderMix in g)
                        {
                            uint prize = 0;
                            if (member.IsNotNull() && answer.IsNotNull())
                                prize = member.MarkSetting.CheckBet(answer, orderMix.Value.Order.BetItems);
                            TreeNode orderNode = new TreeNode($"{orderMix.Value.Order.Amount}#{orderMix.Key.Nonce.ToString()}#{prize}");
                            orderNode.Tag = orderMix;
                            foreach (var betItem in orderMix.Value.Order.BetItems)
                            {
                                MarkSixBetMethod m = (MarkSixBetMethod)betItem.BetTarget.Method;
                                var methodSetting = m.GetMethodSetting();
                                var name = UIHelper.LocalString(methodSetting.Name, methodSetting.EngName);
                                var str = betItem.BetTarget.BetPoint.ToDisplayString(this.ParentPage.ChannelRound.Channel, betItem.BetTarget.Method, UIHelper.IsChina());
                                var texstr = $"{name}[{str}]:{betItem.Amount}";
                                TreeNode subnode = new TreeNode { Text = texstr };
                                orderNode.Nodes.Add(subnode);
                            }
                            roundNode.Nodes.Add(orderNode);
                        }
                        this.tv_outbound_orders.Nodes.Add(roundNode);
                    }
                }
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
            this.tv_outbound_orders.CollapseAll();
        }

        private void tv_inbound_orders_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (!BetUIHelper.AllowOrderSwitchCheck)
                e.Cancel = true;
        }

        private void bt_delete_Click(object sender, EventArgs e)
        {

        }

        private void bt_go_bet_Click(object sender, EventArgs e)
        {
            var node = tv_outbound_orders.SelectedNode;
            if (node.IsNotNull() && node.Level == 0 && node.Tag is MarkPlainTermBetOrder order)
            {
                MarkPlainCopyOrder copyOrder = new MarkPlainCopyOrder
                {
                    Nonce = order.Key.Nonce,
                    Order = order.Value.Order
                };
                var bs = Convert.ToBase64String(copyOrder.ToArray());
                var round = (MarkSixRound)copyOrder.Order.ChannelRound.Round;
                var name = string.Empty;
                if (round == MarkSixRound.MarkHK)
                {
                    name = TextInBoundBuilder.MarkHKTitle;
                }
                else if (round == MarkSixRound.MarkMacau)
                {
                    name = TextInBoundBuilder.MarkMacauTitle;
                }
                var s = $"{copyOrder.Nonce}#{name}#{copyOrder.Order.Amount}#{bs}";
                Clipboard.SetText(s);
                this.ShowSuccessTip(UIHelper.LocalString("订单数据已复制，请及时粘贴到微信转发", "The order data has been copied, please paste it into WeChat and forward it in a timely manner"));
            }
        }
    }
}
