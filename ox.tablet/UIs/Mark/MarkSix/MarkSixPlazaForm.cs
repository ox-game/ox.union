using Org.BouncyCastle.Bcpg;
using OX.BMS;
using OX.Network.P2P.Payloads;
using OX.Tablet.Config;
using OX.Tablet.UIs.Mark;
using OX.Tablet.UIs.Mark.MarkSix;
using Sunny.UI;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml.Linq;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class MarkSixPlazaForm : UIPage, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));

        public MarkChannelRound ChannelRound { get; private set; }
        MethodButton CurrentMethodButton = default;
        public BaseBoundView InboundPage { get; private set; }
        public BaseBoundView OutboundSubPage { get; private set; }
        public BaseBoundView AwardSubPage { get; private set; }
        public MarkTerm RealTimeTerm { get; private set; }
        MarkTerm _term;
        public MarkTerm CurrentSelectedTerm
        {
            get { return _term; }
            set
            {
                _term = value;
                this.bt_today.Text = _term.ToString();
            }
        }

        public MarkSixPlazaForm(MarkChannelRound channelRound)
        {
            this.ChannelRound = channelRound;
            InitializeComponent();
            this.setColor(bt_newPoint, FocusColor);
            this.setColor(bt_new_inbound, FocusColor);
            this.setColor(this.bt_today, FocusColor);
            MarkSixRound round = (MarkSixRound)channelRound.Round;
            var names = round.GetName();
            this.Text = UIHelper.LocalString(names.Name, names.EngName);
            this.bt_next_day.Text = UIHelper.LocalString("后一天", "Next Day");
            this.bt_pre_day.Text = UIHelper.LocalString("前一天", "Pre Day");
            this.L1.Text = UIHelper.LocalString("明细", "Details");
            this.bt_clear.Text = UIHelper.LocalString("清除全部", "Clear All");
            this.bt_clear_page.Text = UIHelper.LocalString("清除本项", "Clear Item");
            this.bt_new_inbound.Text = UIHelper.LocalString("双击入库", "2-hit Store");
            this.bt_newPoint.Text = UIHelper.LocalString("下注", "Bet");
            this.bt_new_inbound.UseDoubleClick = true;
            this.bt_new_inbound.DoubleClick += Bt_new_inbound_DoubleClick;
            InitMethods();
            CurrentSelectedTerm = System.DateTime.Now.GetBetTermForPlayer(SecureHelper.GetMarkSixOpenSeconds());
        }
        public void HeartBeat(HeartBeatContext beatContext)
        {
            RefreshReaTimeTerms();
            if (this.InboundPage.IsNotNull())
                this.InboundPage.HeartBeat(beatContext);
            if (this.OutboundSubPage.IsNotNull())
                this.OutboundSubPage.HeartBeat(beatContext);
        }
        public void BeforeBlock(Block block)
        {
            if (this.InboundPage.IsNotNull())
                this.InboundPage.BeforeBlock(block);
            if (this.OutboundSubPage.IsNotNull())
                this.OutboundSubPage.BeforeBlock(block);
        }
        public void OnBlock(Block block)
        {
            if (this.InboundPage.IsNotNull())
                this.InboundPage.OnBlock(block);
            if (this.OutboundSubPage.IsNotNull())
                this.OutboundSubPage.OnBlock(block);
        }
        public void AfterBlock(Block block)
        {
            if (this.InboundPage.IsNotNull())
                this.InboundPage.AfterBlock(block);
            if (this.OutboundSubPage.IsNotNull())
                this.OutboundSubPage.AfterBlock(block);
        }
        public void OnFlashMessage(FlashMessage message)
        {

        }
        public void OnClipboardString(ClipboardMessageType messageType, string msg)
        {
            if (this.InboundPage.IsNotNull())
                this.InboundPage.OnClipboardString(messageType, msg);
            if (this.OutboundSubPage.IsNotNull())
                this.OutboundSubPage.OnClipboardString(messageType, msg);
        }
        public void MenuPageSelected()
        {
            ReloadBettings();
            if (this.InboundPage.IsNotNull())
                this.InboundPage.MenuPageSelected();
            if (this.OutboundSubPage.IsNotNull())
                this.OutboundSubPage.MenuPageSelected();
            this.AwardSubPage.MenuPageSelected();
        }
        private void Bt_new_inbound_DoubleClick(object sender, EventArgs e)
        {
            var nonce = BetUIHelper.GetNonce();
            MarkInboundOrderKey inboundOrderKey = new MarkInboundOrderKey
            {
                Reception = SecureHelper.MasterAccount.ScriptHash,
                ChannelRound = this.ChannelRound,
                InboundOrigin = InboundOrigin.SelfBuy,
                Term = this.RealTimeTerm,
                CNO = nonce
            };
            var builder = new ManualBoundBuilder(this.ChannelRound);
            if (builder.PushAllInbound(inboundOrderKey))
            {
                ReloadBettings();
                this.InboundPage.ReloadOrders();
            }
        }

        void InitMethods()
        {
            foreach (var m in NoneFlagEnumHelper.All<MarkSixBetMethod>())
            {
                var mb = new FullMethodButton(m);
                var setting = m.GetMethodSetting();
                mb.Text = UIHelper.LocalString(setting.Name, setting.EngName);
                mb.OnMethodSelected += Mb_OnMethodSelected;
                this.pn_methods.Controls.Add(mb);
            }

            if (this.ChannelRound.Channel == BetChannel.MarkSix)
            {
                if (this.ChannelRound.Round == (byte)MarkSixRound.MarkUnion)
                {
                    this.InboundPage = new MarkSixCipherInboundSubPage(this);
                    this.tbc_order.AddPage(this.InboundPage);
                    this.OutboundSubPage = new MarkSixCipherOutboundSubPage(this);
                    this.tbc_order.AddPage(this.OutboundSubPage);
                }
                else
                {
                    this.InboundPage = new MarkSixPlainInboundSubPage(this);
                    this.tbc_order.AddPage(this.InboundPage);
                    this.OutboundSubPage = new MarkSixPlainOutboundSubPage(this);
                    this.tbc_order.AddPage(this.OutboundSubPage);
                }
                this.AwardSubPage = new MarkSixAwardSubPage(this);
                this.tbc_order.AddPage(this.AwardSubPage);
            }
        }
        void RefreshReaTimeTerms()
        {
            this.RealTimeTerm = System.DateTime.Now.GetBetTermForPlayer(SecureHelper.GetMarkSixOpenSeconds());
            this.DoInvoke(() =>
            {
                var enable = this.RealTimeTerm == this.CurrentSelectedTerm;
                this.pn_betmanage.Enabled = enable;
                this.pn_methods.Enabled = enable;
                this.pn_bettings.Enabled = enable;
                this.InboundPage.AllowEdit = enable;
                if (this.OutboundSubPage.IsNotNull())
                    this.OutboundSubPage.AllowEdit = enable;
            });
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
                if (this.CurrentSelectedTerm == this.RealTimeTerm)
                {
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
                MarkSixBetMethod method = (MarkSixBetMethod)this.CurrentMethodButton.Method;
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



        private void bt_post_order_Click(object sender, EventArgs e)
        {
            var term = System.DateTime.Now.GetBetTermForPlayer(SecureHelper.GetMarkSixOpenSeconds());
            var agentIndex = SecureHelper.BlockIndex.GetSubBlockIndex<AgentBlockIndex>();
            List<TreeNode> orders = new List<TreeNode>();
            //foreach (var node in this.tv_orders.Nodes)
            //{
            //    if (node is TreeNode tn && !tn.Checked)
            //    {
            //        if (tn.Tag is MarkInboundOrder order)
            //            orders.Add(tn);
            //    }
            //}
            //new PushOrderForm(orders.ToArray()).ShowDialog();
        }

        private void bt_remvoe_order_Click(object sender, EventArgs e)
        {
            //var node = tv_orders.SelectedNode;
            //if (node.IsNotNull() && node.Level == 0 && !node.Checked && node.Tag is MarkInboundOrder order)
            //{
            //    var agentIndex = SecureHelper.BlockIndex.GetSubBlockIndex<AgentBlockIndex>();
            //    agentIndex.DeleteMarkSixAgentOrder(order.OrderHead);
            //    ReloadOrders();
            //}
        }

        private void tv_orders_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (!BetUIHelper.AllowOrderSwitchCheck)
                e.Cancel = true;
        }

        private void tv_orders_AfterCheck(object sender, TreeViewEventArgs e)
        {

        }

        private void uiTabControl1_SizeChanged(object sender, EventArgs e)
        {
            var size = this.tbc_order.ItemSize;
            size.Width = (this.tbc_order.Width / 3) - 3;
            this.tbc_order.ItemSize = size;
        }

        private void bt_pre_day_Click(object sender, EventArgs e)
        {
            this.CurrentSelectedTerm = this.CurrentSelectedTerm.ToDateTime(12).AddDays(-1).GetBetTermForPlayer(SecureHelper.GetMarkSixOpenSeconds());
            ReloadBettings();
            this.InboundPage.ReloadOrders();
            if (this.OutboundSubPage.IsNotNull())
                this.OutboundSubPage.ReloadOrders();
            this.AwardSubPage.ReloadOrders();
        }

        private void bt_next_day_Click(object sender, EventArgs e)
        {
            this.CurrentSelectedTerm = this.CurrentSelectedTerm.ToDateTime(12).AddDays(1).GetBetTermForPlayer(SecureHelper.GetMarkSixOpenSeconds());
            ReloadBettings();
            this.InboundPage.ReloadOrders();
            if (this.OutboundSubPage.IsNotNull())
                this.OutboundSubPage.ReloadOrders();
            this.AwardSubPage.ReloadOrders();
        }

        private void bt_today_Click(object sender, EventArgs e)
        {
            this.CurrentSelectedTerm = System.DateTime.Now.GetBetTermForPlayer(SecureHelper.GetMarkSixOpenSeconds());
            ReloadBettings();
            this.InboundPage.ReloadOrders();
            if (this.OutboundSubPage.IsNotNull())
                this.OutboundSubPage.ReloadOrders();
            this.AwardSubPage.ReloadOrders();
        }
    }
}
