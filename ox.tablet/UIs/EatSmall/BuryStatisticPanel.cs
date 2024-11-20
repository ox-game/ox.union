using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.BMS;
using OX.Casino;
using Sunny.UI;
using Sunny.UI.Win32;
using OX.Network.P2P.Payloads;
using Akka.IO;
using OX.Ledger;
using OX.Network.P2P;
using Akka.Actor;
using System.Runtime.CompilerServices;

namespace OX.Tablet.UIs.MarkSix
{

    public class BuryStatisticPanel : FlowLayoutPanel, IBlockchainHandler
    {
        BurySummaryView ParentView;
        string PanelTitle;

        public BuryStatisticPanel(BurySummaryView parentView, string title) : base()
        {
            this.ParentView = parentView;
            this.PanelTitle = title;
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Name = "BuryStatisticPanel";
            this.Dock = DockStyle.Fill;
            this.Padding = new System.Windows.Forms.Padding(1, 40, 1, 1);
            this.DoubleClick += PositionPanel_DoubleClick;
            this.Click += PositionPanel_Click;
        }

        private void PositionPanel_DoubleClick(object sender, EventArgs e)
        {
             
        }

        private void PositionPanel_Click(object sender, EventArgs e)
        {

        }

         

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SetHighQuality();
            using var TempFont = new Font("黑体", 14F);

            int mx = 1;
            int my = 1;
            Size msf = TextRenderer.MeasureText(this.PanelTitle, TempFont);
            int msfMax = Math.Max(msf.Width, msf.Height);
            e.Graphics.FillRectangle(Color.DarkOrange, mx, my, msfMax, msf.Height + 1);
            e.Graphics.DrawString(PanelTitle, TempFont, Color.White, new Rectangle(mx, my, msfMax, msf.Height), ContentAlignment.MiddleCenter);
        }

        public void HeartBeat(HeartBeatContext beatContext)
        {
          
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

        }
        public void MenuPageSelected()
        {

        }

    }
}
