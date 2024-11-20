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
using static OX.BMS.MarkPortPlayerTermKey;

namespace OX.Tablet
{

    public partial class PortOrder : UserControl, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
        bool _selected = false;
        public bool Selected
        {
            get { return _selected; }
            private set
            {
                _selected = value;
                this.gb_memberId.RectColor = _selected ? Color.Red : Color.LightSkyBlue;
            }
        }
        public MixMarkMember Member { get; private set; }
        public MarkPortPlayerTermRecord Record { get; private set; }
        public Fixed8 BondAmount { get; private set; } = Fixed8.Zero;
        public PortOrder(MixMarkMember member, MarkPortPlayerTermRecord record)
        {
            this.Member = member;
            this.Record = record;
            InitializeComponent();
            this.gb_memberId.Text = member.MarkMemberId.ToString();
            this.gb_memberId.RectColor = Color.LightSkyBlue;
            this.DoubleClick += BankerInfo_DoubleClick;
            this.gb_memberId.DoubleClick += BankerInfo_DoubleClick;
            this.lb_bet_amount.Text = UIHelper.LocalString($"合计下注:{record.MarkPortPlayerTermValue.TotalBetAmount.ToString("f0")}", $"Total Bet:{record.MarkPortPlayerTermValue.TotalBetAmount.ToString("f0")}");
            this.lb_prize_amount.Text = UIHelper.LocalString($"合计奖金:{record.MarkPortPlayerTermValue.TotalPrizeAmount.ToString("f0")}", $"Total Prize:{record.MarkPortPlayerTermValue.TotalPrizeAmount.ToString("f0")}");
            this.lb_fee.Text = UIHelper.LocalString($"已付佣金:{record.MarkPortPlayerTermValue.FeeAmount.ToString("f0")}", $"Paid Fees:{record.MarkPortPlayerTermValue.FeeAmount.ToString("f0")}");
            this.bt_pay_fee.Text = UIHelper.LocalString("支付佣金", "Pay fee");
        }

        private void BankerInfo_DoubleClick(object sender, EventArgs e)
        {
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



        public void SetSelectState(bool selected)
        {
            _selected = selected;
            this.gb_memberId.RectColor = _selected ? Color.Red : Color.LightSkyBlue;
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

        private void bt_pay_fee_Click(object sender, EventArgs e)
        {
            new PortPayFeeForm(this.Member,this.Record).ShowDialog();
        }
    }
}
