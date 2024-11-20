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
    public partial class TailBetForm : BaseBetForm
    {
        public virtual uint LeastCodeNumber { get; } = 1;
        public TailBetForm()
        {
            InitializeComponent();
        }

        public override string GetTitleText()
        {
            var methodSetting = MarkSixBetMethod.PTTail.GetMethodSetting();
            return UIHelper.LocalString(methodSetting.Name, methodSetting.EngName);
        }
        public override void InitPoints()
        {
            for (byte i = 0; i < 10; i++)
            {
                PointSwitch ps = new PointSwitch(i, 80, i.ToString());
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
        public override BitMarkSixBetTarget[] GetBetTargets()
        {
            List<BitMarkSixBetTarget> list = new List<BitMarkSixBetTarget>();
            foreach (PointSwitch ps in this.GetPointControls())
            {
                if (ps.Checked)
                {
                    var target = new BitMarkSixBetTarget
                    {
                        Method = (byte)MarkSixBetMethod.PTTail,
                        BetPoint = new BetPoint(new byte[] { ps.Value })
                    };
                    list.Add(target);
                }
            }
            return list.ToArray();
        }
    }
}
