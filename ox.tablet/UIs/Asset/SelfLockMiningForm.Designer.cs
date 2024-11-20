using Sunny.UI;
namespace OX.Tablet
{
    partial class SelfLockMiningForm
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
            tb_balance = new UITextBox();
            lb_assetName_balance = new UILabel();
            lb_amount = new UILabel();
            lb_expire = new UILabel();
            cb_expire = new UIComboBox();
            tb_amount = new UINumPadTextBox();
            uiTrackBar1 = new UITrackBar();
            uiRuler1 = new UIRuler();
            pnlBtm.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBtm
            // 
            pnlBtm.Location = new System.Drawing.Point(1, 390);
            pnlBtm.Size = new System.Drawing.Size(618, 88);
            pnlBtm.TabIndex = 7;
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(490, 12);
            btnCancel.Size = new System.Drawing.Size(100, 60);
            // 
            // btnOK
            // 
            btnOK.Location = new System.Drawing.Point(375, 12);
            btnOK.Size = new System.Drawing.Size(100, 60);
            btnOK.Click += btnOK_Click;
            // 
            // tb_balance
            // 
            tb_balance.Cursor = System.Windows.Forms.Cursors.IBeam;
            tb_balance.EnterAsTab = true;
            tb_balance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_balance.Location = new System.Drawing.Point(233, 65);
            tb_balance.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_balance.MinimumSize = new System.Drawing.Size(1, 16);
            tb_balance.Name = "tb_balance";
            tb_balance.Padding = new System.Windows.Forms.Padding(5);
            tb_balance.ReadOnly = true;
            tb_balance.ShowText = false;
            tb_balance.Size = new System.Drawing.Size(284, 43);
            tb_balance.TabIndex = 0;
            tb_balance.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_balance.Watermark = "";
            // 
            // lb_assetName_balance
            // 
            lb_assetName_balance.AutoSize = true;
            lb_assetName_balance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_assetName_balance.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_assetName_balance.Location = new System.Drawing.Point(41, 70);
            lb_assetName_balance.Name = "lb_assetName_balance";
            lb_assetName_balance.Size = new System.Drawing.Size(62, 31);
            lb_assetName_balance.TabIndex = 4;
            lb_assetName_balance.Text = "姓名";
            lb_assetName_balance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_amount
            // 
            lb_amount.AutoSize = true;
            lb_amount.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_amount.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_amount.Location = new System.Drawing.Point(42, 224);
            lb_amount.Name = "lb_amount";
            lb_amount.Size = new System.Drawing.Size(62, 31);
            lb_amount.TabIndex = 15;
            lb_amount.Text = "住址";
            lb_amount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_expire
            // 
            lb_expire.AutoSize = true;
            lb_expire.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_expire.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_expire.Location = new System.Drawing.Point(42, 145);
            lb_expire.Name = "lb_expire";
            lb_expire.Size = new System.Drawing.Size(62, 31);
            lb_expire.TabIndex = 16;
            lb_expire.Text = "住址";
            lb_expire.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cb_expire
            // 
            cb_expire.DataSource = null;
            cb_expire.DropDownStyle = UIDropDownStyle.DropDownList;
            cb_expire.FillColor = System.Drawing.Color.White;
            cb_expire.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cb_expire.ItemHoverColor = System.Drawing.Color.FromArgb(155, 200, 255);
            cb_expire.Items.AddRange(new object[] { "101000", "501000", "1001000", "2001000", "4001000", "6001000" });
            cb_expire.ItemSelectForeColor = System.Drawing.Color.FromArgb(235, 243, 255);
            cb_expire.Location = new System.Drawing.Point(233, 140);
            cb_expire.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cb_expire.MinimumSize = new System.Drawing.Size(63, 0);
            cb_expire.Name = "cb_expire";
            cb_expire.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cb_expire.Size = new System.Drawing.Size(284, 44);
            cb_expire.SymbolSize = 24;
            cb_expire.TabIndex = 17;
            cb_expire.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            cb_expire.Watermark = "";
            // 
            // tb_amount
            // 
            tb_amount.FillColor = System.Drawing.Color.White;
            tb_amount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_amount.Location = new System.Drawing.Point(233, 211);
            tb_amount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_amount.Maximum = 10000000D;
            tb_amount.Minimum = 1000D;
            tb_amount.MinimumSize = new System.Drawing.Size(63, 0);
            tb_amount.Name = "tb_amount";
            tb_amount.NumPadType = NumPadType.Integer;
            tb_amount.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            tb_amount.Size = new System.Drawing.Size(284, 44);
            tb_amount.SymbolSize = 24;
            tb_amount.TabIndex = 38;
            tb_amount.Text = "1000";
            tb_amount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_amount.Watermark = "";
            // 
            // uiTrackBar1
            // 
            uiTrackBar1.BarSize = 50;
            uiTrackBar1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiTrackBar1.Location = new System.Drawing.Point(20, 277);
            uiTrackBar1.MinimumSize = new System.Drawing.Size(1, 1);
            uiTrackBar1.Name = "uiTrackBar1";
            uiTrackBar1.Size = new System.Drawing.Size(574, 44);
            uiTrackBar1.TabIndex = 41;
            uiTrackBar1.Text = "uiTrackBar1";
            uiTrackBar1.ValueChanged += uiTrackBar1_ValueChanged;
            // 
            // uiRuler1
            // 
            uiRuler1.BackColor = System.Drawing.Color.Transparent;
            uiRuler1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiRuler1.Location = new System.Drawing.Point(17, 321);
            uiRuler1.MinimumSize = new System.Drawing.Size(1, 1);
            uiRuler1.Name = "uiRuler1";
            uiRuler1.Size = new System.Drawing.Size(574, 44);
            uiRuler1.TabIndex = 40;
            uiRuler1.Text = "uiRuler1";
            // 
            // SelfLockMiningForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(620, 481);
            Controls.Add(uiTrackBar1);
            Controls.Add(uiRuler1);
            Controls.Add(tb_amount);
            Controls.Add(cb_expire);
            Controls.Add(lb_expire);
            Controls.Add(lb_amount);
            Controls.Add(tb_balance);
            Controls.Add(lb_assetName_balance);
            Name = "SelfLockMiningForm";
            Text = "UIEditFrom";
            Load += SelfLockMiningForm_Load;
            Controls.SetChildIndex(pnlBtm, 0);
            Controls.SetChildIndex(lb_assetName_balance, 0);
            Controls.SetChildIndex(tb_balance, 0);
            Controls.SetChildIndex(lb_amount, 0);
            Controls.SetChildIndex(lb_expire, 0);
            Controls.SetChildIndex(cb_expire, 0);
            Controls.SetChildIndex(tb_amount, 0);
            Controls.SetChildIndex(uiRuler1, 0);
            Controls.SetChildIndex(uiTrackBar1, 0);
            pnlBtm.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private UITextBox tb_balance;
        private UILabel lb_assetName_balance;
        private UILabel uiLabel3;
        private UIRadioButton rbMale;
        private UIRadioButton rbFemale;
        private UITextBox edtAge;
        private UILabel uiLabel4;
        private UIComboBox cbDepartment;
        private UILabel uiLabel5;
        private UILabel uiLabel6;
        private UIDatePicker edtDate;
        private UILabel lb_amount;
        private UILabel lb_expire;
        private UIComboBox cb_expire;
        private UINumPadTextBox tb_amount;
        private UITrackBar uiTrackBar1;
        private UIRuler uiRuler1;
    }
}