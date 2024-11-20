using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.BMS;
using OX.Tablet.Config;
using OX.Tablet.UIs.Mark.BetForms;
using Sunny.UI;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class ZodiacBetForm : BaseBetForm
    {
        public virtual uint LeastCodeNumber { get; } = 1;
        public ZodiacBetForm()
        {
            InitializeComponent();
        }


        public override void InitPoints()
        {
            foreach (var zodiac in NoneFlagEnumHelper.All<Zodiac>())
            {
                var name = UIHelper.LocalString(zodiac.GetName().Name, zodiac.GetName().EngName);
                PointSwitch ps = new PointSwitch((byte)zodiac, 120, name);
                this.AddPoint(ps);
            }
        }
        public override bool PrePost()
        {
            int c = 0;
            foreach (PointSwitch ps in this.GetPointControls())
            {
                if (ps.Checked) c++;
            }
            return c >= this.LeastCodeNumber && this.Amount > 0;
        }
    }
}
