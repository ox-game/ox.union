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

    public class PrizeButton : UIButton
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
        Form ParentForm;
        TransactionOutput Output;
        public PrizeButton(Form parentForm, TransactionOutput output) : base()
        {
            this.ParentForm = parentForm;
            this.Output = output;
            this.Text=output.Value.ToString();
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Name = "RoundButton";
            this.Size = new System.Drawing.Size(120, 50);
            this.Style = Sunny.UI.UIStyle.Custom;
            this.StyleCustomMode = true;
            this.AutoSize = true;
            this.Radius = 5;
            this.TextAlign = ContentAlignment.MiddleCenter;
            if (this.Output.ScriptHash == SecureHelper.MasterAccount.ScriptHash)
            {
                setColor(FocusColor);
            }
            else
            {
                setColor(Color.DarkOrange);
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
            this.ParentForm.ShowSuccessTip(Output.ScriptHash.ToAddress());
        }



    }
}
