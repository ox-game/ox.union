using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.BMS;
using OX.Casino;
using OX.Network.P2P.Payloads;
using OX.Wallets;
using Sunny.UI;
using Sunny.UI.Win32;

namespace OX.Tablet.UIs.MarkSix
{

    public class BettingButton : UIButton
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
        Form ParentForm;
        Betting Betting;
        public BettingButton(Form parentForm, Betting betting) : base()
        {
            this.ParentForm = parentForm;
            this.Betting = betting;
            this.Text = betting.Amount.ToString();
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Name = "RoundButton";
            this.Size = new System.Drawing.Size(120, 50);
            this.Style = Sunny.UI.UIStyle.Custom;
            this.StyleCustomMode = true;
            this.AutoSize = true;
            this.Radius = 5;
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.UseDoubleClick = true;
            if (this.Betting.BetRequest.From == SecureHelper.MasterAccount.ScriptHash)
            {
                setColor(FocusColor);
            }

            this.Click += PrizeButton_Click;
        }
        void setColor(Color color)
        {
            this.FillColor = color;
            this.FillColor2 = color;
            this.FillHoverColor = color;
            this.FillPressColor = color;
            this.FillSelectedColor = color;
            this.RectColor = color;
            this.RectHoverColor = color;
            this.RectPressColor = color;
            this.RectSelectedColor = color;
        }
        private void PrizeButton_Click(object sender, EventArgs e)
        {
            var cc = string.Empty;
            var spccs = this.Betting.BetRequest.BetPoint.Split('|');
            if (spccs.IsNotNullAndEmpty() && spccs.Length > 1)
            {
                cc = spccs[1];
            }
            var msg = $"{this.Betting.BetRequest.From.ToAddress()} : {this.Betting.Amount.ToString()} : {cc}";
            this.ParentForm.ShowSuccessTip(msg);
        }



    }
}
