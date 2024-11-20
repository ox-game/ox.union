using Org.BouncyCastle.Bcpg;
using OX.BMS;
using OX.Network.P2P.Payloads;
using OX.Tablet.Config;
using OX.Tablet.UIs.Mark;
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

namespace OX.Tablet.UIs.MarkSix
{
    public partial class MarkOnePlazaForm : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));

        MarkChannelRound ChannelRound;
        MethodButton CurrentMethodButton = default;
        private bool isMouseDown = false;

        MarkTerm CurrentPlayerTerm;


        public MarkOnePlazaForm(MarkChannelRound channelRound)
        {
            this.ChannelRound = channelRound;
            InitializeComponent();
            this.setColor(bt_newPoint, FocusColor);
            this.setColor(bt_newOrder, FocusColor);
            this.setColor(bt_post_order, FocusColor);
            MarkOneRound round = (MarkOneRound)channelRound.Round;
            var names = round.GetName();
            this.Text = UIHelper.LocalString(names.Name, names.EngName);
            this.L1.Text = UIHelper.LocalString("明细", "Details");
            this.bt_clear.Text = UIHelper.LocalString("清除全部", "Clear All");
            this.bt_clear_page.Text = UIHelper.LocalString("清除本项", "Clear Item");
            this.bt_newOrder.Text = UIHelper.LocalString("双击入库", "2-hit Store");
            this.bt_newPoint.Text = UIHelper.LocalString("下注", "Bet");
            this.bt_post_order.Text = UIHelper.LocalString("下单", "Order");
            this.bt_remvoe_order.Text = UIHelper.LocalString("删单", "Delete");
            this.bt_newOrder.UseDoubleClick = true;
            this.bt_newOrder.DoubleClick += Bt_newOrder_DoubleClick;
            InitMethods();
        }
        public void HeartBeat(HeartBeatContext beatContext)
        {
            RefreshTerms();
        }
        public void BeforeBlock(Block block)
        {

        }
        public void OnBlock(Block block)
        {

        }
        public void AfterBlock(Block block)
        {

        }
        public void OnFlashMessage(FlashMessage message)
        {

        }
        public void OnClipboardString(ClipboardMessageType messageType, string msg)
        {
            if (messageType == ClipboardMessageType.MarkOrder)
                ReloadOrders();
        }
        public void MenuPageSelected()
        {
            ReloadOrders();
            RefreshTerms();
        }
        private void Bt_newOrder_DoubleClick(object sender, EventArgs e)
        {
            MarkTerm term = System.DateTime.Now.GetBetTermForPlayer(SecureHelper.GetMarkSixOpenSeconds());
            var nonce = BetUIHelper.GetNonce();
            MarkInboundOrderKey inboundOrderKey = new MarkInboundOrderKey
            {
                 Reception=SecureHelper.MasterAccount.ScriptHash,
                ChannelRound = this.ChannelRound,
                InboundOrigin =  InboundOrigin.SelfBuy,
                Term = term,
                CNO = nonce
            };
            var builder = new ManualBoundBuilder(this.ChannelRound);
            if (builder.PushAllInbound(inboundOrderKey))
            {
                ReloadBettings();
                ReloadOrders();
            }
        }

        void InitMethods()
        {
            foreach (var m in NoneFlagEnumHelper.All<MarkOneBetMethod>())
            {
                var mb = new SimpleMethodButton(m);
                var setting = m.GetMethodSetting();
                mb.Text = UIHelper.LocalString(setting.Name, setting.EngName);
                mb.OnMethodSelected += Mb_OnMethodSelected;
                this.pn_methods.Controls.Add(mb);
            }
        }

        private void Mb_OnMethodSelected(MethodButton methodButton)
        {
            this.CurrentMethodButton = methodButton;
            foreach (var c in this.pn_methods.Controls)
            {
                if (c is MethodButton mb)
                {
                    if (mb != methodButton)
                    {
                        mb.MethodSelected = false;
                    }
                }
            }
            ReloadBettings();
        }
        void ReloadBettings()
        {
            if (this.CurrentMethodButton.IsNotNull())
            {
                this.pn_bettings.Controls.Clear();
                var builder = new ManualBoundBuilder(this.ChannelRound);
                var betSet = builder.GetMemoryBetSet(this.CurrentMethodButton.Method);
                foreach (var item in betSet.BetItems)
                {
                    var bpb = new BetPointButton(this.ChannelRound.Channel, item.Key, item.Value);
                    bpb.OnBetPointRemoved += Bpb_OnBetPointRemoved;
                    this.pn_bettings.Controls.Add(bpb);
                }
            }
        }
        void ReloadOrders()
        {
            MarkTerm term = System.DateTime.Now.GetBetTermForPlayer(SecureHelper.GetMarkSixOpenSeconds());
            var builder=new ManualBoundBuilder(this.ChannelRound);
            this.tv_orders.Nodes.Clear();
            foreach (var order in builder.GetMarkInboundOrders(SecureHelper.MasterAccount.ScriptHash, term, this.ChannelRound).OrderBy(m => m.Value.State))
            {
                TreeNode orderNode = new TreeNode { Text = order.Key.CNO.ToString() };
                orderNode.Tag = new MarkInboundOrder { OrderHead = order.Key, OrderBody = order.Value };
                orderNode.Checked = order.Value.State == 1;
                foreach (var betItem in order.Value.BetItems)
                {
                    MarkOneBetMethod m = (MarkOneBetMethod)betItem.BetTarget.Method;
                    var methodSetting = m.GetMethodSetting();
                    var name = UIHelper.LocalString(methodSetting.Name, methodSetting.EngName);
                    var str = betItem.BetTarget.BetPoint.ToDisplayString(this.ChannelRound.Channel, betItem.BetTarget.Method, UIHelper.IsChina());
                    var texstr = $"{name}[{str}]:{betItem.Amount}";
                    TreeNode subnode = new TreeNode { Text = texstr };
                    subnode.Checked = order.Value.State == 1;
                    orderNode.Nodes.Add(subnode);
                }
                this.tv_orders.Nodes.Add(orderNode);
            }
        }
        private void Bpb_OnBetPointRemoved(BetPointButton bpb)
        {
            var builder = new ManualBoundBuilder(this.ChannelRound);
            var betSet = builder.GetMemoryBetSet(this.CurrentMethodButton.Method);
            betSet.RemoveBetTarget(bpb.Target);
            ReloadBettings();
        }

        private void pn_methods_SizeChanged(object sender, EventArgs e)
        {
            this.L1.Top = this.pn_methods.Bottom + 10;
            this.L1.Width = this.pn_methods.Width - 20;
            this.pn_bettings.Top = this.pn_methods.Bottom + 50;
        }

        private void bt_clear_Click(object sender, EventArgs e)
        {
            var builder = new ManualBoundBuilder(this.ChannelRound);
            var memoryOrderSet = builder.GetMemoryOrderSet();
            memoryOrderSet.ClearAll();
            ReloadBettings();
        }

        private void bt_clear_page_Click(object sender, EventArgs e)
        {
            var builder = new ManualBoundBuilder(this.ChannelRound);
            var betSet = builder.GetMemoryBetSet(this.CurrentMethodButton.Method);
            betSet.ClearAll();
            ReloadBettings();
        }


        private void bt_newPoint_Click(object sender, EventArgs e)
        {
            if (this.CurrentMethodButton.IsNotNull())
            {
                MarkOneBetMethod method = (MarkOneBetMethod)this.CurrentMethodButton.Method;
                var form = method.GetBetForm();
                if (form.IsNotNull())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var amt = (int)form.GetAmount();
                        var targets = form.GetBetTargets();
                        var builder = new ManualBoundBuilder(this.ChannelRound);
                        var memoryOrderSet = builder.GetMemoryBetSet(this.CurrentMethodButton.Method);
                        foreach (var t in targets)
                        {
                            memoryOrderSet.PlusBetTargetAmount(t, amt);
                        }
                        ReloadBettings();
                    }
                }

            }
        }
        void setColor(UIButton control, Color color)
        {
            control.FillColor = color;
            control.FillColor2 = color;
            control.FillHoverColor = color;
            control.FillPressColor = color;
            control.FillSelectedColor = color;
            control.RectColor = color;
            control.RectHoverColor = color;
            control.RectPressColor = color;
            control.RectSelectedColor = color;
        }


        void RefreshTerms()
        {
            this.DoInvoke(() =>
            {
                var newPlayerTerm = System.DateTime.Now.GetBetTermForPlayer(SecureHelper.GetMarkSixOpenSeconds());
                if (this.CurrentPlayerTerm != newPlayerTerm)
                {
                    this.lb_player_term.Text = UIHelper.LocalString($"闲家注期:{newPlayerTerm.ToString()}", $"Player:{newPlayerTerm.ToString()}");
                }
                if (this.CurrentPlayerTerm != newPlayerTerm)
                {
                    ReloadOrders();
                }
                this.CurrentPlayerTerm = newPlayerTerm;

                var dt = BitSixBetHelper.BeijingNow();
                switch ((MarkOneRound)this.ChannelRound.Round)
                {
                    case MarkOneRound.PM_1:
                        this.Enabled = dt.Hour < 13;
                        break;
                    case MarkOneRound.PM_3:
                        this.Enabled = dt.Hour < 15;
                        break;
                    case MarkOneRound.PM_5:
                        this.Enabled = dt.Hour < 17;
                        break;
                    case MarkOneRound.PM_7:
                        this.Enabled = dt.Hour < 19;
                        break;
                }

            });
        }
        private void bt_post_order_Click(object sender, EventArgs e)
        {
            var agentIndex = SecureHelper.BlockIndex.GetSubBlockIndex<AgentBlockIndex>();
            List<TreeNode> orders = new List<TreeNode>();
            foreach (var node in this.tv_orders.Nodes)
            {
                if (node is TreeNode tn && !tn.Checked)
                {
                    if (tn.Tag is MarkInboundOrder order)
                        orders.Add(tn);
                }
            }           
            //new PushOrderForm(orders.ToArray()).ShowDialog();
        }

        private void bt_remvoe_order_Click(object sender, EventArgs e)
        {
            var node = tv_orders.SelectedNode;
            if (node.IsNotNull() && node.Level == 0 && !node.Checked && node.Tag is MarkInboundOrder order)
            {
                var agentIndex = SecureHelper.BlockIndex.GetSubBlockIndex<AgentBlockIndex>();
                agentIndex.DeleteMarkInboundOrder(order.OrderHead);
                ReloadOrders();
            }
        }

        private void tv_orders_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (!BetUIHelper.AllowOrderSwitchCheck)
                e.Cancel = true;
        }

        private void tv_orders_AfterCheck(object sender, TreeViewEventArgs e)
        {

        }
    }
}
