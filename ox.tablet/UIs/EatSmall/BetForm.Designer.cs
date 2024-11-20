using Sunny.UI;
namespace OX.Tablet
{
    partial class BetForm
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
            lb_balance = new UILabel();
            lb_amount = new UILabel();
            lb_special_code = new UILabel();
            cb_codes = new UIComboBox();
            cb_Height = new UIComboBox();
            lb_bet_height = new UILabel();
            lb_roomid = new UILabel();
            btnCancel = new UIButton();
            btnOK = new UIButton();
            uiRuler1 = new UIRuler();
            uiTrackBar1 = new UITrackBar();
            tb_amount = new UINumPadTextBox();
            SuspendLayout();
            // 
            // tb_balance
            // 
            tb_balance.Cursor = System.Windows.Forms.Cursors.IBeam;
            tb_balance.EnterAsTab = true;
            tb_balance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_balance.Location = new System.Drawing.Point(642, 156);
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
            // lb_balance
            // 
            lb_balance.AutoSize = true;
            lb_balance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_balance.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_balance.Location = new System.Drawing.Point(489, 161);
            lb_balance.Name = "lb_balance";
            lb_balance.Size = new System.Drawing.Size(62, 31);
            lb_balance.TabIndex = 4;
            lb_balance.Text = "姓名";
            lb_balance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_amount
            // 
            lb_amount.AutoSize = true;
            lb_amount.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_amount.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_amount.Location = new System.Drawing.Point(42, 163);
            lb_amount.Name = "lb_amount";
            lb_amount.Size = new System.Drawing.Size(62, 31);
            lb_amount.TabIndex = 15;
            lb_amount.Text = "住址";
            lb_amount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_special_code
            // 
            lb_special_code.AutoSize = true;
            lb_special_code.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_special_code.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_special_code.Location = new System.Drawing.Point(493, 242);
            lb_special_code.Name = "lb_special_code";
            lb_special_code.Size = new System.Drawing.Size(62, 31);
            lb_special_code.TabIndex = 16;
            lb_special_code.Text = "住址";
            lb_special_code.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cb_codes
            // 
            cb_codes.DataSource = null;
            cb_codes.DropDownStyle = UIDropDownStyle.DropDownList;
            cb_codes.FillColor = System.Drawing.Color.White;
            cb_codes.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cb_codes.ItemHoverColor = System.Drawing.Color.FromArgb(155, 200, 255);
            cb_codes.ItemSelectForeColor = System.Drawing.Color.FromArgb(235, 243, 255);
            cb_codes.Location = new System.Drawing.Point(645, 237);
            cb_codes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cb_codes.MinimumSize = new System.Drawing.Size(63, 0);
            cb_codes.Name = "cb_codes";
            cb_codes.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cb_codes.Size = new System.Drawing.Size(284, 44);
            cb_codes.SymbolSize = 24;
            cb_codes.TabIndex = 17;
            cb_codes.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            cb_codes.Watermark = "";
            // 
            // cb_Height
            // 
            cb_Height.DataSource = null;
            cb_Height.DropDownStyle = UIDropDownStyle.DropDownList;
            cb_Height.FillColor = System.Drawing.Color.White;
            cb_Height.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cb_Height.ItemHoverColor = System.Drawing.Color.FromArgb(155, 200, 255);
            cb_Height.ItemSelectForeColor = System.Drawing.Color.FromArgb(235, 243, 255);
            cb_Height.Location = new System.Drawing.Point(642, 75);
            cb_Height.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cb_Height.MinimumSize = new System.Drawing.Size(63, 0);
            cb_Height.Name = "cb_Height";
            cb_Height.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cb_Height.Size = new System.Drawing.Size(287, 44);
            cb_Height.SymbolSize = 24;
            cb_Height.TabIndex = 19;
            cb_Height.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            cb_Height.Watermark = "";
            // 
            // lb_bet_height
            // 
            lb_bet_height.AutoSize = true;
            lb_bet_height.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_bet_height.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_bet_height.Location = new System.Drawing.Point(489, 82);
            lb_bet_height.Name = "lb_bet_height";
            lb_bet_height.Size = new System.Drawing.Size(62, 31);
            lb_bet_height.TabIndex = 18;
            lb_bet_height.Text = "住址";
            lb_bet_height.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_roomid
            // 
            lb_roomid.AutoSize = true;
            lb_roomid.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_roomid.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_roomid.Location = new System.Drawing.Point(42, 86);
            lb_roomid.Name = "lb_roomid";
            lb_roomid.Size = new System.Drawing.Size(62, 31);
            lb_roomid.TabIndex = 20;
            lb_roomid.Text = "住址";
            lb_roomid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnCancel.Location = new System.Drawing.Point(734, 354);
            btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(192, 52);
            btnCancel.TabIndex = 21;
            btnCancel.Text = "uiButton1";
            btnCancel.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // btnOK
            // 
            btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnOK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnOK.Location = new System.Drawing.Point(42, 354);
            btnOK.MinimumSize = new System.Drawing.Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.Size = new System.Drawing.Size(192, 52);
            btnOK.TabIndex = 22;
            btnOK.Text = "uiButton1";
            btnOK.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnOK.Click += btnOK_Click;
            // 
            // uiRuler1
            // 
            uiRuler1.BackColor = System.Drawing.Color.Transparent;
            uiRuler1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiRuler1.Location = new System.Drawing.Point(54, 258);
            uiRuler1.MinimumSize = new System.Drawing.Size(1, 1);
            uiRuler1.Name = "uiRuler1";
            uiRuler1.Size = new System.Drawing.Size(421, 44);
            uiRuler1.TabIndex = 23;
            uiRuler1.Text = "uiRuler1";
            // 
            // uiTrackBar1
            // 
            uiTrackBar1.BarSize = 50;
            uiTrackBar1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiTrackBar1.Location = new System.Drawing.Point(57, 214);
            uiTrackBar1.MinimumSize = new System.Drawing.Size(1, 1);
            uiTrackBar1.Name = "uiTrackBar1";
            uiTrackBar1.Size = new System.Drawing.Size(421, 44);
            uiTrackBar1.TabIndex = 24;
            uiTrackBar1.Text = "uiTrackBar1";
            uiTrackBar1.ValueChanged += uiTrackBar1_ValueChanged;
            // 
            // tb_amount
            // 
            tb_amount.FillColor = System.Drawing.Color.White;
            tb_amount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_amount.Location = new System.Drawing.Point(199, 155);
            tb_amount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_amount.Maximum = 1000000D;
            tb_amount.Minimum = 10D;
            tb_amount.MinimumSize = new System.Drawing.Size(63, 0);
            tb_amount.Name = "tb_amount";
            tb_amount.NumPadType = NumPadType.Integer;
            tb_amount.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            tb_amount.Size = new System.Drawing.Size(276, 44);
            tb_amount.SymbolSize = 24;
            tb_amount.TabIndex = 39;
            tb_amount.Text = "10";
            tb_amount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_amount.Watermark = "";
            // 
            // BetForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(958, 436);
            ControlBox = false;
            Controls.Add(tb_amount);
            Controls.Add(uiTrackBar1);
            Controls.Add(uiRuler1);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Controls.Add(lb_roomid);
            Controls.Add(cb_Height);
            Controls.Add(lb_bet_height);
            Controls.Add(cb_codes);
            Controls.Add(lb_special_code);
            Controls.Add(lb_amount);
            Controls.Add(tb_balance);
            Controls.Add(lb_balance);
            ExtendSymbolSize = 50;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BetForm";
            Text = "UIEditFrom";
            ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 958, 398);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private UITextBox tb_balance;
        private UILabel lb_balance;
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
        private UILabel lb_special_code;
        private UIComboBox cb_codes;
        private UIComboBox cb_Height;
        private UILabel lb_bet_height;
        private UILabel lb_roomid;
        private UIButton btnCancel;
        private UIButton btnOK;
        private UIRuler uiRuler1;
        private UITrackBar uiTrackBar1;
        private UINumPadTextBox tb_amount;
    }
}