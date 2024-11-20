using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.BouncyCastle.Asn1.Mozilla;
using OX.BMS;
using OX.Tablet.Config;
using Sunny.UI;

namespace OX.Tablet.UIs.MarkSix
{
    public partial class BaseBetForm : UIForm, IBetUI
    {
        protected bool allowClose = false;
        protected uint Amount = 0;
        public BaseBetForm()
        {
            InitializeComponent();
            this.CancelButton = this.bt_cancel;
            this.AcceptButton = this.bt_do_bet;
            this.bt_do_bet.Text = "0";
            this.bt_cancel.Text = UIHelper.LocalString("取消", "Cancel");
            this.bt_plus_5.Text = "+5";
            this.bt_plus_50.Text = "+50";
            this.bt_plus_500.Text = "+500";
            this.bt_subtract_5.Text = "-5";
            this.bt_subtract_50.Text = "-50";
            this.bt_subtract_500.Text = "-500";
            this.Text = GetTitleText();
            this.InitPoints();
        }

        public virtual BitMarkSixBetTarget[] GetBetTargets()
        {
            return default;
        }
        public uint GetAmount()
        {
            return Amount;
        }
        public virtual string GetTitleText()
        {
            return string.Empty;
        }
        public virtual void InitPoints()
        {

        }
        public void AddPoint(Control c)
        {
            this.pn_points.Controls.Add(c);
        }
        public System.Windows.Forms.Control.ControlCollection GetPointControls()
        {
            return this.pn_points.Controls;
        }
        public void ReInit()
        {

        }
        public virtual bool PrePost()
        {
            return true;
        }
        private void bt_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            allowClose = true;
            this.Close();
        }

        private void BaseBetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!allowClose)
                e.Cancel = true;
        }

        private void bt_do_bet_Click(object sender, EventArgs e)
        {
            allowClose = PrePost();
            this.DialogResult = DialogResult.OK;
        }

        private void bt_subtract_5_Click(object sender, EventArgs e)
        {
            if (Amount >= 5)
            {
                Amount -= 5;
            }
            else
                Amount = 0;
            this.bt_do_bet.Text = this.Amount.ToString();
        }

        private void bt_subtract_50_Click(object sender, EventArgs e)
        {
            if (Amount >= 50)
            {
                Amount -= 50;
            }
            else
                Amount = 0;
            this.bt_do_bet.Text = this.Amount.ToString();
        }

        private void bt_subtract_500_Click(object sender, EventArgs e)
        {
            if (Amount >= 500)
            {
                Amount -= 500;
            }
            else
                Amount = 0;
            this.bt_do_bet.Text = this.Amount.ToString();
        }

        private void bt_plus_5_Click(object sender, EventArgs e)
        {
            Amount += 5;
            this.bt_do_bet.Text = this.Amount.ToString();
        }

        private void bt_plus_50_Click(object sender, EventArgs e)
        {
            Amount += 50;
            this.bt_do_bet.Text = this.Amount.ToString();
        }

        private void bt_plus_500_Click(object sender, EventArgs e)
        {
            Amount += 500;
            this.bt_do_bet.Text = this.Amount.ToString();
        }
        #region
        public BetPoint[] Ergodic2(byte[] bs)
        {
            List<BetPoint> list = new List<BetPoint>();
            var len = bs.Length;
            for (var j = 0; j < len - 1; j++)
            {
                for (var k = j + 1; k < len; k++)
                {
                    BetPoint bp = new BetPoint(new byte[] { bs[j], bs[k] });
                    list.Add(bp);
                }
            }
            return list.ToArray();
        }
        public BetPoint[] Ergodic3(byte[] bs)
        {
            List<BetPoint> list = new List<BetPoint>();
            var len = bs.Length;
            for (var i = 0; i < len - 2; i++)
            {
                for (var j = i + 1; j < len - 1; j++)
                {
                    for (var k = j + 1; k < len; k++)
                    {
                        BetPoint bp = new BetPoint(new byte[] { bs[i], bs[j], bs[k] });
                        list.Add(bp);
                    }
                }
            }
            return list.ToArray();
        }
        public BetPoint[] Ergodic4(byte[] bs)
        {
            List<BetPoint> list = new List<BetPoint>();
            var len = bs.Length;
            for (var h = 0; h < len - 3; h++)
            {
                for (var i = h + 1; i < len - 2; i++)
                {
                    for (var j = i + 1; j < len - 1; j++)
                    {
                        for (var k = j + 1; k < len; k++)
                        {
                            BetPoint bp = new BetPoint(new byte[] { bs[h], bs[i], bs[j], bs[k] });
                            list.Add(bp);
                        }
                    }
                }
            }
            return list.ToArray();
        }
        public BetPoint[] Ergodic5(byte[] bs)
        {
            List<BetPoint> list = new List<BetPoint>();
            var len = bs.Length;
            for (var g = 0; g < len - 4; g++)
            {
                for (var h = g + 1; h < len - 3; h++)
                {
                    for (var i = h + 1; i < len - 2; i++)
                    {
                        for (var j = i + 1; j < len - 1; j++)
                        {
                            for (var k = j + 1; k < len; k++)
                            {
                                BetPoint bp = new BetPoint(new byte[] { bs[g], bs[h], bs[i], bs[j], bs[k] });
                                list.Add(bp);
                            }
                        }
                    }
                }
            }
            return list.ToArray();
        }
        #endregion
    }
}
