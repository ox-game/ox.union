using OX.Casino;
using OX.Wallets;
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

namespace OX.Tablet.UIs.MarkSix
{
    public partial class MarkSixMethodSettingItem : UserControl
    {
        MarkSixBetMethod Method;
        public MarkSixMethodSettingItem(MarkSixBetMethod method, BMSPlayMethod setting = default)
        {
            Method = method;
            InitializeComponent();
            this.BackColor = Color.Gray;
            var attr = method.GetMethodSetting();
            this.lb_method.Text = $"{UIHelper.LocalString(attr.Name, attr.EngName)}:";
            this.nd_odd.Maximum = attr.MaxOdds;
            this.nd_odd.Minimum = attr.MinOdds;
            this.nd_odd.Value = setting.Odds;
           
        }
        public BMSPlayMethod GetMethodSetting()
        {
            return new BMSPlayMethod { Method = (byte)this.Method, Odds = (ushort)this.nd_odd.Value, Fee = 0};
        }
    }
}
