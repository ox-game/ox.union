using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Org.BouncyCastle.Asn1.Mozilla;
using OX.BMS;
using OX.Tablet.Config;
using OX.Tablet.UIs.Mark;
using OX.Tablet.UIs.Mark.MarkSix;
using Sunny.UI;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class MarkSixPostCipherOrderForm : UIForm
    {
        TreeNode[] Nodes;
        List<TreeNode> validNodes = new List<TreeNode>();
        List<MarkInboundOrder> list = new List<MarkInboundOrder>();
        MarkSixPlazaForm MarkSixPlazaForm;
        public MarkSixPostCipherOrderForm(TreeNode[] nodes, MarkSixPlazaForm parentForm)
        {
            this.Nodes = nodes;
            this.MarkSixPlazaForm = parentForm;
            InitializeComponent();
            this.Text = UIHelper.LocalString("提交订单", "Post Order");
            this.bt_post_order.Text = UIHelper.LocalString("下单", "Order");
            this.bt_close.Text = UIHelper.LocalString("取消", "Cancel");
            this.L1.Text = UIHelper.LocalString("选择入库单", "Select Inbound Orders");
            this.tb_transfer_inbound.ItemsRightCountChange += Tb_transfer_inbound_ItemsRightCountChange;
            List<MarkInboundOrder> orders = new List<MarkInboundOrder>();
            foreach (var node in this.Nodes)
            {
                if (node is TreeNode tn && !tn.Checked)
                {
                    if (tn.Tag is MarkInboundOrder order)
                    {
                        validNodes.Add(tn);
                        orders.Add(order);
                    }
                }
            }

            this.tb_transfer_inbound.ItemsLeft.AddRange(validNodes.ToArray().Select(m => new TreeNodeView { Node = m }).ToArray());


        }




        private void Tb_transfer_inbound_ItemsRightCountChange(object sender, EventArgs e)
        {
            list.Clear();
            foreach (TreeNodeView tnv in this.tb_transfer_inbound.ItemsRight)
            {
                list.Add(tnv.Order);
            }
            var total = list.Sum(m => m.Amount);
            this.lb_msg.Text = UIHelper.LocalString($"所选订单合计金额：{total}", $"Total amount of selected orders:{total}");

        }

        private void DoPost()
        {
            if (SecureHelper.IsAgent() || SecureHelper.IsPort())
            {
                var member = SecureHelper.GetMarkMember();
                if (member.IsNotNull())
                {
                    var term = System.DateTime.Now.GetBetTermForPlayer(SecureHelper.GetMarkSixOpenSeconds());
                    var OrderMerge = BitSixAgentHelper.CombineCipherOrders(SecureHelper.MasterAccount.ScriptHash, member.Request.PortHolder, term, list.ToArray(), NodeConfig.Instance.AutoCutMarkBet);
                    BetUIHelper.AllowOrderSwitchCheck = true;
                    var agentIndex = SecureHelper.BlockIndex.GetSubBlockIndex<AgentBlockIndex>();
                    agentIndex.Do(batch =>
                    {
                        foreach (var node in this.validNodes)
                        {
                            node.Checked = true;
                            foreach (var subNode in node.Nodes)
                            {
                                if (subNode is TreeNode sn)
                                    sn.Checked = true;
                            }
                            if (node is TreeNode tn)
                            {
                                if (tn.Tag is MarkInboundOrder order)
                                {
                                    order.OrderBody.State = 1;
                                    agentIndex.UpdateMarkInboundOrder(batch, order);
                                }
                            }
                        }
                        if (OrderMerge.IsNotNull())
                        {
                            var orders = OrderMerge.Order.CipherShard();
                            if (orders.IsNotNull())
                            {
                                var nonce = BetUIHelper.GetNonce();

                                foreach (var order in orders)
                                {
                                    var key = new MarkCipherBetOrderKey { Player = order.Player, ChannelRound = order.ChannelRound, Nonce = nonce, Term = term, OrderHash = order.Hash };
                                    var value = new MarkCipherBetOrderValue { Order = order, State = MarkCipherOrderStatus.Inited };
                                    agentIndex.SaveCipherOutboundOrder(batch, new MarkCipherTermBetOrder { Key = key, Value = value });
                                    this.MarkSixPlazaForm.OutboundSubPage.ReloadOrders();
                                }
                            }
                        }
                    });
                    BetUIHelper.AllowOrderSwitchCheck = false;
                }
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void bt_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_post_order_Click(object sender, EventArgs e)
        {
            DoPost();
        }
    }
}
