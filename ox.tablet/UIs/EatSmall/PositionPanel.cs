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
using OX.Tablet.Config;

namespace OX.Tablet.UIs.MarkSix
{

    public class PositionPanel : FlowLayoutPanel, IBlockchainHandler
    {
        RoomView ParentView;
        BetForm BetForm;
        MixRoom Room;
        public byte Position { get; private set; }
        Fixed8 totalBet = Fixed8.Zero;
        Fixed8 MinBet = Fixed8.Zero;
        string key = default;
        public PositionPanel(RoomView parentView, MixRoom room, Fixed8 minBet, byte position) : base()
        {
            this.Room = room;
            this.ParentView = parentView;
            this.Position = position;
            this.MinBet = minBet;

            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Name = "PositionPanel";
            this.Dock = DockStyle.Fill;
            this.Padding = new System.Windows.Forms.Padding(1, 40, 1, 1);
            this.DoubleClick += PositionPanel_DoubleClick;
            this.Click += PositionPanel_Click;
        }

        private void PositionPanel_DoubleClick(object sender, EventArgs e)
        {
            if (this.ParentView.AllowBet()&&this.Room.Request.AssetId==Blockchain.OXC)
            {
                this.BetForm = new BetForm(ParentView, Room, MinBet, Position);

                using (this.BetForm = new BetForm(ParentView, Room, MinBet, Position))
                {
                    if(BetForm.ShowDialog()== DialogResult.OK)
                    {
                        var tx = BetForm.BuildTx();
                        if (tx.IsNotNull())
                        {
                            Program.BlockHandler.Tell(tx);
                            foreach (var coin in tx.Inputs)
                            {
                                SecureHelper.BlockIndex.UnconfirmCoins[coin] = Blockchain.Singleton.Height + 100;
                            }
                            new WaitTxForm(tx, UIHelper.LocalString("等待下注确认...", "Waiting  confirm bet transaction")).ShowDialog();
                        }
                    }                    
                }
            }
        }

        private void PositionPanel_Click(object sender, EventArgs e)
        {

        }

        public void RefreshData(Betting[] bettings, char[] keys)
        {
            this.Controls.Clear();
            if (keys.IsNotNullAndEmpty())
            {
                this.key = keys[this.Position].ToString();
            }
            else
                this.key = default;
            totalBet = bettings.Sum(m => m.Amount);
            foreach (var betting in bettings.OrderByDescending(m => m.Amount))
            {
                var btn = new BettingButton(this.ParentView, betting);
                btn.DoubleClick += PositionPanel_DoubleClick;
                this.Controls.Add(btn);
            }
            this.Invalidate();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SetHighQuality();
            using var TempFont = new Font("黑体", 14F);

            int mx = 1;
            int my = 1;
            var multiStr = this.Position.ToString();
            Size msf = TextRenderer.MeasureText(multiStr, TempFont);
            int msfMax = Math.Max(msf.Width, msf.Height);
            e.Graphics.FillRectangle(Color.DarkOrange, mx, my, msfMax, msf.Height + 1);
            e.Graphics.DrawString(multiStr, TempFont, Color.White, new Rectangle(mx, my, msfMax, msf.Height), ContentAlignment.MiddleCenter);

            if (this.key.IsNotNullAndEmpty())
            {
                Size sf = TextRenderer.MeasureText(this.key, TempFont);
                int sfMax = Math.Max(sf.Width, sf.Height);
                int x = mx + sfMax + 10;
                int y = 1;
                e.Graphics.FillEllipse(Color.Red, x - 2, y, sfMax, sfMax);
                e.Graphics.DrawString(this.key, TempFont, Color.White, new Rectangle(x, y, sfMax, sf.Height), ContentAlignment.MiddleCenter);
            }

            var amtStr = this.totalBet.ToString();
            Size amtsf = TextRenderer.MeasureText(amtStr, TempFont);
            int amtsfMax = Math.Max(amtsf.Width, amtsf.Height);
            int x_amt = Width - amtsfMax - 50;
            int y_amt = 1;
            e.Graphics.FillRectangle(Color.DarkOrange, x_amt, y_amt, amtsfMax, amtsf.Height + 1);
            e.Graphics.DrawString(amtStr, TempFont, Color.White, new Rectangle(x_amt , y_amt, amtsfMax, amtsf.Height), ContentAlignment.MiddleCenter);
        }

        public void HeartBeat(HeartBeatContext beatContext)
        {
            if (this.BetForm.IsNotNull() && this.BetForm.Visible)
            {
                this.BetForm.HeartBeat(beatContext);
            }
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
