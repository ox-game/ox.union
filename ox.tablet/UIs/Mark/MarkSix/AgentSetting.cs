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
    public partial class AgentSetting : UserControl
    {
        MarkSetting BitSixSetting;
        public AgentSetting()
        {
            InitializeComponent();
            this.BackColor = Color.Gray;
            this.LoadSetting(new MarkSetting());
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.lb_name.Text = UIHelper.LocalString("玩法", "Method");
            this.lb_odds.Text = UIHelper.LocalString("赔率", "Odds");
            //this.lb_fee.Text = UIHelper.LocalString("全单佣金:", "Order Commission:");
        }
        public void LoadSetting(MarkSetting setting)
        {
            if (setting.IsNotNull())
            {
                this.BitSixSetting = setting;
                this.pfull.Controls.Clear();
                this.BitSixSetting = setting;
                //this.nd_fee.Value = this.BitSixSetting.Flag;
                List<MarkSixBetMethod> methods = new List<MarkSixBetMethod>();
                foreach (var fs in this.BitSixSetting.MarkSixMethods)
                {
                    MarkSixBetMethod m = (MarkSixBetMethod)fs.Method;
                    methods.Add(m);
                    this.pfull.Controls.Add(new MarkSixMethodSettingItem(m, fs));
                }
                foreach(var m in NoneFlagEnumHelper.All<MarkSixBetMethod>())
                {
                    if(!methods.Contains(m))
                    {
                        var methodsetting = m.GetMethodSetting();
                        if (methodsetting.IsNotNull())
                        {
                            this.pfull.Controls.Add(new MarkSixMethodSettingItem(m, new BMSPlayMethod { Method = (byte)m, Odds = methodsetting.DefaultOdds, Fee = methodsetting.DefaultCommission }));
                        }
                    }
                }
                //this.nd_fee.Visible = showFee;
                //this.lb_fee.Visible= showFee;
                //this.lb_sm.Visible= showFee;
            }
        }
        public MarkSetting GetSetting()
        {
            List<BMSPlayMethod> l1 = new List<BMSPlayMethod>();
            List<BMSPlayMethod> l2 = new List<BMSPlayMethod>();
            foreach (MarkSixMethodSettingItem f in this.pfull.Controls)
            {
                l1.Add(f.GetMethodSetting());
            }
            return new MarkSetting { MarkSixMethods = l1.ToArray(), MarkOneMethods = l2.ToArray(), Flag = 0 };
        }
    }
}
