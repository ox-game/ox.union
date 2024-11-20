using OX.BMS;
using OX.Tablet.Config;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OX.Tablet.UIs.Mark.MarkSix
{
    public partial class AwardResult : UserControl
    {
        public AwardResult()
        {
            InitializeComponent();
            //SetGroupVisible("u_", false);
            //SetGroupVisible("m_", false);
            //SetGroupVisible("h_", false);
        }
        public void Reload(MarkTerm term)
        {
            this.lb_term.Text = term.ToString();
            SetGroupVisible("u_", false);
            SetGroupVisible("m_", false);
            SetGroupVisible("h_", false);
            var bmsIndex = SecureHelper.BlockIndex.GetSubBlockIndex<BMSBlockIndex>();
            if (bmsIndex.IsNotNull())
            {
                foreach (var r in NoneFlagEnumHelper.All<MarkSixRound>())
                {
                    GuessAnswerKey key = new GuessAnswerKey { Term = term, ChannelRound = new MarkChannelRound(BetChannel.MarkSix, (byte)r) };
                    if (bmsIndex.GuessAnswers.TryGetValue(key.ToString(), out var answer))
                    {
                        if (r == MarkSixRound.MarkHK)
                        {
                            SetGroupVisible("h_", true);
                            var names = r.GetName();
                            this.h_name.Text = UIHelper.LocalString(names.Name, names.EngName);
                            var year = answer.Key.Term.Year;
                            Set(answer.Value.P1, "h", "1", year);
                            Set(answer.Value.P2, "h", "2", year);
                            Set(answer.Value.P3, "h", "3", year);
                            Set(answer.Value.P4, "h", "4", year);
                            Set(answer.Value.P5, "h", "5", year);
                            Set(answer.Value.P6, "h", "6", year);
                            Set(answer.Value.T, "h", "t", year);
                        }
                        else if (r == MarkSixRound.MarkMacau)
                        {
                            SetGroupVisible("m_", true);
                            var names = r.GetName();
                            this.m_name.Text = UIHelper.LocalString(names.Name, names.EngName);
                            var year = answer.Key.Term.Year;
                            Set(answer.Value.P1, "m", "1", year);
                            Set(answer.Value.P2, "m", "2", year);
                            Set(answer.Value.P3, "m", "3", year);
                            Set(answer.Value.P4, "m", "4", year);
                            Set(answer.Value.P5, "m", "5", year);
                            Set(answer.Value.P6, "m", "6", year);
                            Set(answer.Value.T, "m", "t", year);
                        }
                        else if (r == MarkSixRound.MarkUnion)
                        {
                            SetGroupVisible("u_", true);
                            var names = r.GetName();
                            this.u_name.Text = UIHelper.LocalString(names.Name, names.EngName);
                            //this.u_g.Text = answer.Value.G.ToString();
                            var year = answer.Key.Term.Year;
                            Set(answer.Value.P1, "u", "1", year);
                            Set(answer.Value.P2, "u", "2", year);
                            Set(answer.Value.P3, "u", "3", year);
                            Set(answer.Value.P4, "u", "4", year);
                            Set(answer.Value.P5, "u", "5", year);
                            Set(answer.Value.P6, "u", "6", year);
                            Set(answer.Value.T, "u", "t", year);
                        }
                    }
                }
            }
        }
        void Set(byte code, string g, string key, ushort year)
        {
            Color color = Color.Red;
            if (code.TryGetColor(out var codeColor))
            {
                switch (codeColor)
                {
                    case MarkSixColor.Red:
                        color = Color.MediumVioletRed;
                        break;
                    case MarkSixColor.Blue:
                        color = Color.RoyalBlue;
                        break;
                    case MarkSixColor.Green:
                        color = Color.DarkGreen;
                        break;
                }
            }
            var p = GetControl(g + "_p" + key);
            setColor(p as UIButton, color);
            p.Text = code.ToString();
            if (code.TryGetZodiac(year, out var zodiac1))
            {
                var names1 = zodiac1.GetName();
                var z = GetControl(g + "_z" + key);
                setColor(z as UIButton, color);
                z.Text = UIHelper.LocalString(names1.Name, names1.EngName);
            }
        }
        public Image Export()
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));
            return bmp;
        }
        public Control GetControl(string name)
        {
            return this.Controls.Find(name, false).FirstOrDefault();
        }
        public void SetGroupVisible(string key, bool visible)
        {
            foreach (Control c in this.Controls)
            {
                if (c.Name.StartsWith(key))
                    c.Visible = visible;
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
    }
}
