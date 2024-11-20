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
using OX.Tablet.UIs.Mark.BetForms;
using Sunny.UI;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class CodeBetForm : BaseBetForm
    {
        public virtual uint LeastCodeNumber { get; } = 1;
        public CodeBetForm()
        {
            InitializeComponent();
        }


        public override void InitPoints()
        {
            for (byte i = 1; i < 50; i++)
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
    }
}
