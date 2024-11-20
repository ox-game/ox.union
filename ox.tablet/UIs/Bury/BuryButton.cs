using OX.Wallets;
using OX.Bapps;
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using Sunny.UI;
using OX.Tablet.UIs.MarkSix;
using OX.Tablet;

namespace OX.Tablet.UIs.MarkSix
{
    public class BuryButton : UIButton
    {
        public static readonly Color FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
        BuryRecord BuryRecord;
        BuryMergeTx BuryMergeTx;
        BuryBetView BuryView;
        public BuryButton(BuryBetView buryView, BuryRecord br, BuryMergeTx buryMergeTx, int index)
        {
            this.Width = 80;
            this.Height = 25;
            this.BuryRecord = br;
            this.BuryMergeTx = buryMergeTx;
            this.BuryView = buryView;
            this.Text = $"{br.Request.PlainBuryPoint}";
            string ms = string.Empty;
            if (br.Request.From == SecureHelper.MasterAccount.ScriptHash)
            {
                setColor(Color.Black);
                //this.foreColor = FocusColor;
            }
            else if (index <= 100)
            {
                setColor(FocusColor);
            }


            this.Click += BettingButton_Click;

            this.Margin = new Padding() { All = 5 };
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
        private void BettingButton_Click(object sender, EventArgs e)
        {
            if (this.BuryMergeTx.IsNotNull())
            {
                //new ReplyBuryDetail(this.BuryRecord, this.BuryMergeTx).ShowDialog();
            }
            else
            {
                var msg = $"{this.BuryRecord.BuryAmount.ToString()}  :  {BuryRecord.Request.From.ToAddress()}  :  {BuryRecord.Request.VerifyHash.ToString()}";
                this.BuryView.ShowSuccessTip(msg);
            }
        }
    }
}
