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
using Sunny.UI;
using OX.Tablet.UIs.MarkSix;
using OX.Tablet.Config;
using OX.Tablet.UIs.Mark.BetForms;

namespace OX.Tablet.UIs.MarkOne
{
    public partial class SpecialZodiacBetForm : BaseBetForm
    {
        public virtual uint LeastCodeNumber { get; } = 1;
        public SpecialZodiacBetForm()
        {
            InitializeComponent();
        }


        public override void InitPoints()
        {
            foreach (var msc in NoneFlagEnumHelper.All<Zodiac>())
            {
                var name = UIHelper.LocalString(msc.GetName().Name, msc.GetName().EngName);
                PointSwitch ps = new PointSwitch((byte)msc, 120, name);
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
        public override string GetTitleText()
        {
            var methodSetting = MarkOneBetMethod.Zodiac.GetMethodSetting();
            return UIHelper.LocalString(methodSetting.Name, methodSetting.EngName);
        }
        public override BitMarkSixBetTarget[] GetBetTargets()
        {
            List<BitMarkSixBetTarget> list = new List<BitMarkSixBetTarget>();
            foreach (PointSwitch ps in this.GetPointControls())
            {
                if (ps.Checked)
                {
                    var target = new BitMarkSixBetTarget
                    {
                        Method = (byte)MarkOneBetMethod.Zodiac,
                        BetPoint = new BetPoint(new byte[] { ps.Value })
                    };
                    list.Add(target);
                }
            }
            return list.ToArray();
        }
    }
}
