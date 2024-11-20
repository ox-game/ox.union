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
using OX.IO;

namespace OX.Tablet
{

    public partial class AgentDetails : UserControl, IBlockchainHandler
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));

        public MixMarkMember Member { get; private set; }
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
        public AgentDetails(MixMarkMember member)
        {
            this.Member = member;
            InitializeComponent();
            this.bt_next_day.Text = UIHelper.LocalString("后一天", "Next Day");
            this.bt_pre_day.Text = UIHelper.LocalString("前一天", "Pre Day");
            this.setColor(this.bt_today, FocusColor);
            CurrentSelectedTerm = System.DateTime.Now.GetBetTermForPlayer(SecureHelper.GetMarkSixOpenSeconds());
        }

        private void BankerInfo_DoubleClick(object sender, EventArgs e)
        {
        }

        private void lb_master_balance_Click(object sender, EventArgs e)
        {

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
        public void OnClipboardString(ClipboardMessageType messageType, string msg)
        {
        }
        public void MenuPageSelected()
        {
            ReloadRecords();
        }
        void ReloadRecords()
        {
            pn_orders.Clear();
            var bmsIndex = SecureHelper.BlockIndex.GetSubBlockIndex<BMSBlockIndex>();
            if (bmsIndex.IsNotNull())
            {
                var myMember = SecureHelper.GetMarkMember();
                if (myMember.IsNotNull())
                {
                    foreach (var r in bmsIndex.MarkPortPlayerTermRecords(this.CurrentSelectedTerm, myMember.Request.PortHolder, myMember.Holder))
                    {
                        if (bmsIndex.MarkMembers.TryGetValue(r.Key.PortHolder, out var member))
                        {
                            var control = new AgentOrder(member, new MarkPortPlayerTermRecord { MarkPortPlayerTermKey = r.Key, MarkPortPlayerTermValue = r.Value });
                            pn_orders.Add(control);
                        }
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

        private void bt_pre_day_Click(object sender, EventArgs e)
        {
            this.CurrentSelectedTerm = this.CurrentSelectedTerm.ToDateTime(12).AddDays(-1).GetBetTermForPlayer(SecureHelper.GetMarkSixOpenSeconds());
            ReloadRecords();
        }

        private void bt_today_Click(object sender, EventArgs e)
        {
            this.CurrentSelectedTerm = System.DateTime.Now.GetBetTermForPlayer(SecureHelper.GetMarkSixOpenSeconds());
            ReloadRecords();
        }

        private void bt_next_day_Click(object sender, EventArgs e)
        {
            this.CurrentSelectedTerm = this.CurrentSelectedTerm.ToDateTime(12).AddDays(1).GetBetTermForPlayer(SecureHelper.GetMarkSixOpenSeconds());
            ReloadRecords();
        }
    }
}
