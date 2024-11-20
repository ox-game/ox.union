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
    public partial class SimpleMethodSettingControl : UserControl
    {
        MarkOneBetMethod Method;
        public SimpleMethodSettingControl(MarkOneBetMethod method, BMSPlayMethod setting = default)
        {
            Method = method;
            InitializeComponent();
            this.BackColor = Color.Gray;
            var attr = method.GetMethodSetting();
            this.lb_method.Text = $"{UIHelper.LocalString(attr.Name, attr.EngName)}:";
            this.nd_odd.Maximum = attr.MaxOdds;
            this.nd_odd.Minimum = attr.MinOdds;
            this.nd_fee.Maximum = attr.MaxCommission;
            this.nd_fee.Minimum = attr.MinCommission;
            if (setting.IsNotNull())
            {
                this.nd_odd.Value = setting.Odds;
                this.nd_fee.Value = setting.Fee;
            }
            else
            {
                this.nd_odd.Value = attr.DefaultOdds;
                this.nd_fee.Value = attr.DefaultCommission;
            }        
           
        }
        public BMSPlayMethod GetMethodSetting()
        {
            return new BMSPlayMethod { Method = (byte)this.Method, Odds = (ushort)this.nd_odd.Value, Fee = (ushort)this.nd_fee.Value };
        }
    }
}
