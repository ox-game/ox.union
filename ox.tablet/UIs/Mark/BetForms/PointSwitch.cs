using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunny.UI;

namespace OX.Tablet.UIs.Mark.BetForms
{
    public class PointSwitch : UIButton
    {
        public byte Value { get; private set; } = 0;
        public bool Checked { get; private set; } = false;
        public PointSwitch(byte value, int size, string text) : base()
        {
            Value = value;
            Text = text;
            Cursor = System.Windows.Forms.Cursors.Arrow;

            Font = new Font("微软雅黑", 14F);
            Location = new Point(600, 135);
            MinimumSize = new Size(1, 1);
            Name = "PointSwitch";
            Style = UIStyle.Custom;
            StyleCustomMode = true;
            Radius = size;
            TipsColor = Color.Red;
            TextAlign = ContentAlignment.MiddleCenter;
            Size = new Size { Width = size, Height = size };
            setColor(Color.DarkGray);
            Click += PointSwitch_Click;
        }

        private void PointSwitch_Click(object sender, EventArgs e)
        {
            Checked = !Checked;
            setColor(Checked ? Color.DarkOrange : Color.DarkGray);
        }
        void setColor(Color color)
        {
            FillColor = color;
            FillColor2 = color;
            FillHoverColor = color;
            FillPressColor = color;
            FillSelectedColor = color;
            RectColor = color;
            RectHoverColor = color;
            RectPressColor = color;
            RectSelectedColor = color;
        }
    }
}
