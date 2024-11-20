using Sunny.UI;
using System.Windows.Forms;
namespace OX.Tablet.UIs.MarkSix
{
    partial class PortPayFeeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortPayFeeForm));
            panel = new Panel();
            lb_term = new UILabel();
            tb_fee_amount = new UITextBox();
            lb_fee_amount = new UILabel();
            tb_bet_amount = new UITextBox();
            uiLabel1 = new UILabel();
            nd_rate = new NumericUpDown();
            lb_bet_amount = new UILabel();
            bt_close = new UIButton();
            lb_rate = new UILabel();
            tb_balance = new UITextBox();
            lb_balance = new UILabel();
            bt_NewRoom = new UIButton();
            darkLabel1 = new UILabel();
            panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nd_rate).BeginInit();
            SuspendLayout();
            // 
            // panel
            // 
            panel.Controls.Add(lb_term);
            panel.Controls.Add(tb_fee_amount);
            panel.Controls.Add(lb_fee_amount);
            panel.Controls.Add(tb_bet_amount);
            panel.Controls.Add(uiLabel1);
            panel.Controls.Add(nd_rate);
            panel.Controls.Add(lb_bet_amount);
            panel.Controls.Add(bt_close);
            panel.Controls.Add(lb_rate);
            panel.Controls.Add(tb_balance);
            panel.Controls.Add(lb_balance);
            panel.Controls.Add(bt_NewRoom);
            resources.ApplyResources(panel, "panel");
            panel.Name = "panel";
            panel.Paint += panel_Paint;
            // 
            // lb_term
            // 
            resources.ApplyResources(lb_term, "lb_term");
            lb_term.ForeColor = System.Drawing.Color.Black;
            lb_term.Name = "lb_term";
            // 
            // tb_fee_amount
            // 
            tb_fee_amount.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            resources.ApplyResources(tb_fee_amount, "tb_fee_amount");
            tb_fee_amount.ForeColor = System.Drawing.Color.Black;
            tb_fee_amount.MaxLength = 20;
            tb_fee_amount.Name = "tb_fee_amount";
            tb_fee_amount.ReadOnly = true;
            tb_fee_amount.ShowText = false;
            tb_fee_amount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_fee_amount.Watermark = "";
            // 
            // lb_fee_amount
            // 
            resources.ApplyResources(lb_fee_amount, "lb_fee_amount");
            lb_fee_amount.ForeColor = System.Drawing.Color.Black;
            lb_fee_amount.Name = "lb_fee_amount";
            // 
            // tb_bet_amount
            // 
            tb_bet_amount.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            resources.ApplyResources(tb_bet_amount, "tb_bet_amount");
            tb_bet_amount.ForeColor = System.Drawing.Color.Black;
            tb_bet_amount.MaxLength = 20;
            tb_bet_amount.Name = "tb_bet_amount";
            tb_bet_amount.ReadOnly = true;
            tb_bet_amount.ShowText = false;
            tb_bet_amount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_bet_amount.Watermark = "";
            // 
            // uiLabel1
            // 
            resources.ApplyResources(uiLabel1, "uiLabel1");
            uiLabel1.ForeColor = System.Drawing.Color.Black;
            uiLabel1.Name = "uiLabel1";
            // 
            // nd_rate
            // 
            resources.ApplyResources(nd_rate, "nd_rate");
            nd_rate.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            nd_rate.Name = "nd_rate";
            nd_rate.Value = new decimal(new int[] { 5, 0, 0, 0 });
            nd_rate.ValueChanged += nd_rate_ValueChanged;
            // 
            // lb_bet_amount
            // 
            resources.ApplyResources(lb_bet_amount, "lb_bet_amount");
            lb_bet_amount.ForeColor = System.Drawing.Color.Black;
            lb_bet_amount.Name = "lb_bet_amount";
            // 
            // bt_close
            // 
            resources.ApplyResources(bt_close, "bt_close");
            bt_close.Name = "bt_close";
            bt_close.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_close.Click += bt_close_Click;
            // 
            // lb_rate
            // 
            resources.ApplyResources(lb_rate, "lb_rate");
            lb_rate.ForeColor = System.Drawing.Color.Black;
            lb_rate.Name = "lb_rate";
            // 
            // tb_balance
            // 
            tb_balance.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            resources.ApplyResources(tb_balance, "tb_balance");
            tb_balance.ForeColor = System.Drawing.Color.Black;
            tb_balance.MaxLength = 20;
            tb_balance.Name = "tb_balance";
            tb_balance.ReadOnly = true;
            tb_balance.ShowText = false;
            tb_balance.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_balance.Watermark = "";
            // 
            // lb_balance
            // 
            resources.ApplyResources(lb_balance, "lb_balance");
            lb_balance.ForeColor = System.Drawing.Color.Black;
            lb_balance.Name = "lb_balance";
            // 
            // bt_NewRoom
            // 
            resources.ApplyResources(bt_NewRoom, "bt_NewRoom");
            bt_NewRoom.Name = "bt_NewRoom";
            bt_NewRoom.Click += bt_NewRoom_Click;
            // 
            // darkLabel1
            // 
            resources.ApplyResources(darkLabel1, "darkLabel1");
            darkLabel1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            darkLabel1.Name = "darkLabel1";
            // 
            // PortPayFeeForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            ControlBox = false;
            Controls.Add(panel);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PortPayFeeForm";
            ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 1247, 878);
            FormClosing += ClaimForm_FormClosing;
            Load += ClaimForm_Load;
            panel.ResumeLayout(false);
            panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nd_rate).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private UIButton bt_NewRoom;
        private UILabel darkLabel1;
        private UILabel lb_balance;
        private UITextBox tb_balance;
        private UILabel darkLabel5;
        private UILabel lb_rate;
        private UIButton bt_close;
        private UILabel lb_bet_amount;
        private UITextBox tb_bet_amount;
        private UILabel uiLabel1;
        private NumericUpDown nd_rate;
        private UITextBox tb_fee_amount;
        private UILabel lb_fee_amount;
        private UILabel lb_term;
    }
}