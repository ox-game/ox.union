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

namespace OX.Tablet.UIs.MarkSix
{

    public class RoundButton : UIButton
    {
        MixRoom Room = default;
        string Tip = string.Empty;
        public RoundButton() : base()
        {
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FillColor = Color.DarkOrange;
            this.FillColor2 = Color.DarkOrange;
            this.FillHoverColor = Color.DarkOrange;
            this.FillPressColor = Color.DarkOrange;
            this.FillSelectedColor = Color.DarkOrange;
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Name = "RoundButton";
            this.RectColor = Color.DarkOrange;
            this.RectHoverColor = Color.DarkOrange;
            this.RectPressColor = Color.DarkOrange;
            this.RectSelectedColor = Color.DarkOrange;
            this.Size = new System.Drawing.Size(120, 50);
            this.Style = Sunny.UI.UIStyle.Custom;
            this.StyleCustomMode = true;
            this.Radius = 5;
            this.TextAlign = ContentAlignment.BottomCenter;

            this.UseDoubleClick = true;
            this.DoubleClick += BetPointButton_DoubleClick;
        }

        private void BetPointButton_DoubleClick(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SetHighQuality();
            using var TempFont = new Font("黑体", 10F);
            if (Tip.IsNotNullAndEmpty())
            {
                Size sf = TextRenderer.MeasureText(this.Tip, TempFont);
                int sfMax = Math.Max(sf.Width, sf.Height);
                int x = Width - 1 - 2 - sfMax;
                int y = 1 + 2;
                e.Graphics.FillRectangle(Color.White, x - 1, y, sfMax, sf.Height+1);
                e.Graphics.DrawString(this.Tip, TempFont, Color.Red, new Rectangle(x, y, sfMax, sf.Height), ContentAlignment.MiddleCenter);
            }
            if (this.Room.IsNotNull())
            {
                int mx = 1 + 2;
                int my = 1 + 2;
                var multiStr = this.Room.Request.BonusMultiple.ToString();
                Size msf = TextRenderer.MeasureText(multiStr, TempFont);
                int msfMax = Math.Max(msf.Width, msf.Height);
                e.Graphics.FillRectangle(Color.White, mx - 1, my, msfMax, msf.Height+1);
                e.Graphics.DrawString(multiStr, TempFont, Color.Red, new Rectangle(mx, my, msfMax, msf.Height), ContentAlignment.MiddleCenter);
            }
        }
        public void Update(MixRoom room, uint currentIndex, string tip)
        {
            this.Room = room;
            this.Text = currentIndex.ToString();
            this.Tip = tip;
            this.Invalidate();
        }
    }
}
