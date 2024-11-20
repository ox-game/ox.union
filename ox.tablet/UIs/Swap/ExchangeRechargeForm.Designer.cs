using Sunny.UI;
namespace OX.Tablet
{
    partial class ExchangeRechargeForm
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
            st_asset = new UISwitch();
            tb_amount = new UINumPadTextBox();
            uiTrackBar1 = new UITrackBar();
            uiRuler1 = new UIRuler();
            pnlBtm.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBtm
            // 
            pnlBtm.Size = new System.Drawing.Size(576, 78);
            pnlBtm.TabIndex = 7;
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(448, 12);
            btnCancel.Size = new System.Drawing.Size(100, 51);
            // 
            // btnOK
            // 
            btnOK.Location = new System.Drawing.Point(333, 12);
            btnOK.Size = new System.Drawing.Size(100, 51);
            btnOK.Click += btnOK_Click;
            // 
            // tb_balance
            // 
            tb_balance.Cursor = System.Windows.Forms.Cursors.IBeam;
            tb_balance.EnterAsTab = true;
            tb_balance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_balance.Location = new System.Drawing.Point(216, 118);
            tb_balance.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_balance.MinimumSize = new System.Drawing.Size(1, 16);
            tb_balance.Name = "tb_balance";
            tb_balance.Padding = new System.Windows.Forms.Padding(5);
            tb_balance.ReadOnly = true;
            tb_balance.ShowText = false;
            tb_balance.Size = new System.Drawing.Size(276, 43);
            tb_balance.TabIndex = 0;
            tb_balance.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_balance.Watermark = "";
            // 
            // lb_assetName_balance
            // 
            lb_assetName_balance.AutoSize = true;
            lb_assetName_balance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_assetName_balance.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_assetName_balance.Location = new System.Drawing.Point(64, 124);
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
            lb_amount.Location = new System.Drawing.Point(64, 186);
            lb_amount.Name = "lb_amount";
            lb_amount.Size = new System.Drawing.Size(62, 31);
            lb_amount.TabIndex = 15;
            lb_amount.Text = "住址";
            lb_amount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // st_asset
            // 
            st_asset.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            st_asset.Location = new System.Drawing.Point(216, 57);
            st_asset.MinimumSize = new System.Drawing.Size(1, 1);
            st_asset.Name = "st_asset";
            st_asset.Size = new System.Drawing.Size(158, 44);
            st_asset.SwitchShape = UISwitch.UISwitchShape.Square;
            st_asset.TabIndex = 18;
            st_asset.Text = "uiSwitch1";
            st_asset.ValueChanged += st_self_ValueChanged;
            // 
            // tb_amount
            // 
            tb_amount.FillColor = System.Drawing.Color.White;
            tb_amount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_amount.Location = new System.Drawing.Point(216, 185);
            tb_amount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_amount.Maximum = 10000000D;
            tb_amount.Minimum = 0D;
            tb_amount.MinimumSize = new System.Drawing.Size(63, 0);
            tb_amount.Name = "tb_amount";
            tb_amount.NumPadType = NumPadType.Integer;
            tb_amount.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            tb_amount.Size = new System.Drawing.Size(276, 44);
            tb_amount.SymbolSize = 24;
            tb_amount.TabIndex = 27;
            tb_amount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_amount.Watermark = "";
            // 
            // uiTrackBar1
            // 
            uiTrackBar1.BarSize = 50;
            uiTrackBar1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiTrackBar1.Location = new System.Drawing.Point(34, 250);
            uiTrackBar1.MinimumSize = new System.Drawing.Size(1, 1);
            uiTrackBar1.Name = "uiTrackBar1";
            uiTrackBar1.Size = new System.Drawing.Size(515, 44);
            uiTrackBar1.TabIndex = 29;
            uiTrackBar1.Text = "uiTrackBar1";
            uiTrackBar1.ValueChanged += uiTrackBar1_ValueChanged;
            // 
            // uiRuler1
            // 
            uiRuler1.BackColor = System.Drawing.Color.Transparent;
            uiRuler1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiRuler1.Location = new System.Drawing.Point(31, 294);
            uiRuler1.MinimumSize = new System.Drawing.Size(1, 1);
            uiRuler1.Name = "uiRuler1";
            uiRuler1.Size = new System.Drawing.Size(515, 44);
            uiRuler1.TabIndex = 28;
            uiRuler1.Text = "uiRuler1";
            // 
            // ExchangeRechargeForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(578, 473);
            Controls.Add(uiTrackBar1);
            Controls.Add(uiRuler1);
            Controls.Add(tb_amount);
            Controls.Add(st_asset);
            Controls.Add(lb_amount);
            Controls.Add(tb_balance);
            Controls.Add(lb_assetName_balance);
            Name = "ExchangeRechargeForm";
            Text = "UIEditFrom";
            Load += ExchangeRechargeForm_Load;
            Controls.SetChildIndex(pnlBtm, 0);
            Controls.SetChildIndex(lb_assetName_balance, 0);
            Controls.SetChildIndex(tb_balance, 0);
            Controls.SetChildIndex(lb_amount, 0);
            Controls.SetChildIndex(st_asset, 0);
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
        private UISwitch st_asset;
        private UINumPadTextBox tb_amount;
        private UITrackBar uiTrackBar1;
        private UIRuler uiRuler1;
    }
}