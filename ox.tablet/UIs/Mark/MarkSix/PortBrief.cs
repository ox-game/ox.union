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
using OX.BMS;
using OX.Network.P2P.Payloads;
using OX.Bapps;
using OX.Tablet.UIs.MarkSix;
using System.Runtime;
using OX.Tablet.Config;

namespace OX.Tablet
{
    public delegate void BankerBriefSelected(PortBrief bankerBrief);
    public partial class PortBrief : UserControl, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
        bool _selected = false;
        public bool Selected
        {
            get { return _selected; }
            private set
            {
                _selected = value;
                this.uiGroupBox1.RectColor = _selected ? Color.Red : Color.LightSkyBlue;
                this.PortSelected?.Invoke(this);
            }
        }
        public MixMarkMember Member { get; private set; }
        public Fixed8 BondAmount { get; private set; } = Fixed8.Zero;
        public event BankerBriefSelected PortSelected;
        public PortBrief(MixMarkMember member)
        {
            this.Member = member;
            InitializeComponent();
            this.uiGroupBox1.Text = member.MarkMemberId.ToString();
            this.uiGroupBox1.RectColor = Color.LightSkyBlue;
            this.DoubleClick += BankerInfo_DoubleClick;
            this.uiGroupBox1.DoubleClick += BankerInfo_DoubleClick;
            this.lb_bond_balance.Text = string.Empty;
            this.lb_total_bet.Text = string.Empty;
            this.lb_count_bet.Text = string.Empty;
            RefreshState();
        }

        private void BankerInfo_DoubleClick(object sender, EventArgs e)
        {
            new ShowPortSettingForm(this.Member).ShowDialog();
        }

        private void lb_master_balance_Click(object sender, EventArgs e)
        {
            this.Selected = !this.Selected;
        }

        public void HeartBeat(HeartBeatContext beatContext)
        {

        }
        public void BeforeBlock(Block block) { }
        public void OnBlock(Block block)
        {

        }
        public void AfterBlock(Block block)
        {

        }
        public void OnFlashMessage(FlashMessage message)
        {

        }
        public void OnClipboardString(ClipboardMessageType messageType, string msg) { }
        public void RefreshState()
        {
            var act = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(this.Member.DepositAddress);
            if (act.IsNotNull())
            {
                this.BondAmount = act.GetBalance(Blockchain.OXC);
                this.lb_bond_balance.Text = UIHelper.LocalString($"保证金:{this.BondAmount.ToString("f0")}", $"Bond:{this.BondAmount.ToString("f0")}");
            }
        }


        public void SetSelectState(bool selected)
        {
            _selected = selected;
            this.uiGroupBox1.RectColor = _selected ? Color.Red : Color.LightSkyBlue;
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
    }
}
