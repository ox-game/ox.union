using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.BMS;
using OX.Tablet.Config;
using Sunny.UI;
using Sunny.UI.Win32;

namespace OX.Tablet.UIs.Mark
{
    public delegate void BetPointRemoveHandler(BetPointButton bpb);
    public class BetPointButton : UIButton
    {
        public BetChannel JudgeChannel { get; private set; }
        public BitMarkSixBetTarget Target { get; private set; }
        public uint Amount { get; private set; }
        public event BetPointRemoveHandler OnBetPointRemoved;

        public BetPointButton(BetChannel channel, BitMarkSixBetTarget target, uint amount) : base()
        {
            JudgeChannel = channel;
            Target = target;
            Amount = amount;
            Text = target.BetPoint.ToDisplayString(channel, target.Method, UIHelper.IsChina());
            //this.TipsText = amount.ToString();

            Cursor = Cursors.Hand;
            FillColor = Color.DarkOrange;
            FillColor2 = Color.DarkOrange;
            FillHoverColor = Color.DarkOrange;
            FillPressColor = Color.DarkOrange;
            FillSelectedColor = Color.DarkOrange;
            Font = new Font("微软雅黑", 9F);
            //this.Location = new System.Drawing.Point(600, 135);
            //this.MinimumSize = new System.Drawing.Size(100, 50);
            Name = "uiButton13";
            RectColor = Color.DarkOrange;
            RectHoverColor = Color.DarkOrange;
            RectPressColor = Color.DarkOrange;
            RectSelectedColor = Color.DarkOrange;
            Size = new Size(120, 50);
            Style = UIStyle.Custom;
            StyleCustomMode = true;
            Radius = 5;
            TextAlign = ContentAlignment.BottomCenter;

            //this.TouchPressClick = true;
            UseDoubleClick = true;
            DoubleClick += BetPointButton_DoubleClick;
        }

        private void BetPointButton_DoubleClick(object sender, EventArgs e)
        {
            OnBetPointRemoved?.Invoke(this);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var tt = Amount.ToString();
            e.Graphics.SetHighQuality();
            using var TempFont = new Font("黑体", 10F);
            Size sf = TextRenderer.MeasureText(tt, TempFont);
            int sfMax = Math.Max(sf.Width, sf.Height);
            int x = Width - 1 - 2 - sfMax;
            int y = 1 + 2;
            e.Graphics.FillRectangle(Color.White, x - 1, y, sfMax, sf.Height);
            e.Graphics.DrawString(tt, TempFont, Color.Red, new Rectangle(x, y, sfMax, sf.Height), ContentAlignment.MiddleCenter);
        }

    }
}
