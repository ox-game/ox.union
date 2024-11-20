using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.BouncyCastle.Asn1.Crmf;
using OX.BMS;
using Sunny.UI;

namespace OX.Tablet.UIs.Mark
{
    public delegate void MethodSelectedHandler(MethodButton methodButton);
    public abstract class MethodButton : UIButton
    {
        public static readonly Color SelectedColor = Color.FromArgb(230, 80, 80);
        public static readonly Color UnSelectedColor = Color.LightBlue;
        public byte Method { get; protected set; }
        bool _selected = false;
        public event MethodSelectedHandler OnMethodSelected;
        public bool MethodSelected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                setColor(_selected ? SelectedColor : UnSelectedColor);
            }
        }
        public abstract BetChannel JudgeChannel { get; }
        public MethodButton() : base()
        {
            Cursor = Cursors.Hand;
            Font = new Font("微软雅黑", 9F);
            Location = new Point(600, 135);
            MinimumSize = new Size(1, 1);
            Name = "uiButton13";
            Size = new Size(120, 120);
            Style = UIStyle.Custom;
            StyleCustomMode = true;
            Radius = 15;
            setColor(UnSelectedColor);
            Click += MethodButton_Click;
        }

        private void MethodButton_Click(object sender, EventArgs e)
        {
            MethodSelected = true;
            OnMethodSelected?.Invoke(this);
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
    public class FullMethodButton : MethodButton
    {
        public override BetChannel JudgeChannel => BetChannel.MarkSix;
        public FullMethodButton(MarkSixBetMethod method)
        {
            Method = (byte)method;
        }

    }
    public class SimpleMethodButton : MethodButton
    {
        public override BetChannel JudgeChannel => BetChannel.MarkOne;
        public SimpleMethodButton(MarkOneBetMethod method)
        {
            Method = (byte)method;
        }
    }
}
