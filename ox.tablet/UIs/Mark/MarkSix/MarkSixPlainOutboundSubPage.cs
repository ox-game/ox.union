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
    public partial class MarkSixPlainOutboundSubPage : BaseBoundView
    {

        MarkSixPlazaForm ParentPage;
        public MarkSixPlainOutboundSubPage(MarkSixPlazaForm parentPage)
        {
            this.ParentPage = parentPage;
            InitializeComponent();
            this.setColor(this.bt_cipher_bet, FocusColor);
            this.Text = UIHelper.LocalString("出库订单", "Outbound Orders");
            this.bt_cipher_bet.Text = UIHelper.LocalString("加密复制订单", "Cipher Order");
            this.bt_plain_bet.Text = UIHelper.LocalString("复制订单", "Copy Order");
            //this.bt_go_bet.UseDoubleClick = true;
            //this.bt_go_bet.DoubleClick += Bt_go_bet_DoubleClick;
            this.bt_cipher_bet.Enabled = SecureHelper.IsAgent() || SecureHelper.IsPort();
            this.bt_plain_bet.Enabled = SecureHelper.IsAgent() || SecureHelper.IsPort();
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

            var agentIndex = SecureHelper.BlockIndex.GetSubBlockIndex<AgentBlockIndex>();
            foreach (var orderMerge in agentIndex.GetMarkPlainOutboundOrders(SecureHelper.MasterAccount.ScriptHash, this.ParentPage.CurrentSelectedTerm, this.ParentPage.ChannelRound))
            {
                uint prize = 0;
                if (member.IsNotNull() && answer.IsNotNull())
                    prize = member.MarkSetting.CheckBet(answer, orderMerge.Value.Order.BetItems);
                TreeNode orderNode = new TreeNode($"{orderMerge.Value.Order.Amount}#{orderMerge.Key.Nonce.ToString()}#{prize}");
                orderNode.Tag = new MarkPlainTermBetOrder { Key = orderMerge.Key, Value = orderMerge.Value };
                foreach (var betItem in orderMerge.Value.Order.BetItems)
                {
                    MarkSixBetMethod m = (MarkSixBetMethod)betItem.BetTarget.Method;
                    var methodSetting = m.GetMethodSetting();
                    var name = UIHelper.LocalString(methodSetting.Name, methodSetting.EngName);
                    var str = betItem.BetTarget.BetPoint.ToDisplayString(this.ParentPage.ChannelRound.Channel, betItem.BetTarget.Method, UIHelper.IsChina());
                    var texstr = $"{name}[{str}]:{betItem.Amount}";
                    TreeNode subnode = new TreeNode { Text = texstr };
                    orderNode.Nodes.Add(subnode);
                }
                this.tv_outbound_orders.Nodes.Add(orderNode);
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

        private void bt_plain_bet_Click(object sender, EventArgs e)
        {
            var node = tv_outbound_orders.SelectedNode;
            if (node.IsNotNull() && node.Level == 0 && node.Tag is MarkPlainTermBetOrder order)
            {
                MarkPlainCopyOrder copyOrder = new MarkPlainCopyOrder
                {
                    Nonce = order.Key.Nonce,
                    Order = order.Value.Order
                };
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

                StringBuilder sb = new StringBuilder();
                sb.Append($"#UNID:{copyOrder.Nonce}\n");
                sb.Append($"#{name}总计:{copyOrder.Order.Amount}\n");
                foreach (var g in copyOrder.Order.BetItems.GroupBy(m => m.BetTarget.Method))
                {
                    var method = (MarkSixBetMethod)g.Key;
                    var names = method.GetMethodSetting();
                    var methodName = UIHelper.LocalString(names.Name, names.EngName);
                    sb.Append($"#{methodName}:\n");
                    foreach (var betItem in g)
                    {
                        var betName = betItem.BetTarget.BetPoint.ToDisplayString(BetChannel.MarkSix, g.Key, UIHelper.IsChina());
                        sb.Append($"{betName}:{betItem.Amount}\n");
                    }
                }
                Clipboard.SetText(sb.ToString());
                this.ShowSuccessTip(UIHelper.LocalString("订单数据已复制，请及时粘贴到微信转发", "The order data has been copied, please paste it into WeChat and forward it in a timely manner"));
            }
        }
    }
}
