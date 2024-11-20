using Sunny.UI;
namespace OX.Tablet
{
    partial class MasterSeniorTransferForm
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
            lb_ox_address = new UILabel();
            cb_ox_targetAddress = new UIComboBox();
            uiTabControl1 = new UITabControl();
            tb_ox_transfer = new System.Windows.Forms.TabPage();
            tb_lock_transfer = new System.Windows.Forms.TabPage();
            tb_lock_index = new UINumPadTextBox();
            dp_lock_time = new UIDatetimePicker();
            st_timelock = new UISwitch();
            lb_lock_expire = new UILabel();
            st_self = new UISwitch();
            tb_lock_pubkey = new UITextBox();
            lb_lock_pubkey = new UILabel();
            tb_eth_transfer = new System.Windows.Forms.TabPage();
            tb_eth_lockindex = new UINumPadTextBox();
            lb_eth_lockindex = new UILabel();
            cb_eth_address = new UIComboBox();
            lb_eth_address = new UILabel();
            tb_amount = new UINumPadTextBox();
            uiTrackBar1 = new UITrackBar();
            uiRuler1 = new UIRuler();
            pnlBtm.SuspendLayout();
            uiTabControl1.SuspendLayout();
            tb_ox_transfer.SuspendLayout();
            tb_lock_transfer.SuspendLayout();
            tb_eth_transfer.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBtm
            // 
            pnlBtm.Location = new System.Drawing.Point(1, 613);
            pnlBtm.Size = new System.Drawing.Size(956, 83);
            pnlBtm.TabIndex = 7;
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(828, 12);
            btnCancel.Size = new System.Drawing.Size(100, 59);
            // 
            // btnOK
            // 
            btnOK.Location = new System.Drawing.Point(713, 12);
            btnOK.Size = new System.Drawing.Size(100, 59);
            // 
            // tb_balance
            // 
            tb_balance.Cursor = System.Windows.Forms.Cursors.IBeam;
            tb_balance.EnterAsTab = true;
            tb_balance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_balance.Location = new System.Drawing.Point(673, 58);
            tb_balance.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_balance.MinimumSize = new System.Drawing.Size(1, 16);
            tb_balance.Name = "tb_balance";
            tb_balance.Padding = new System.Windows.Forms.Padding(5);
            tb_balance.ReadOnly = true;
            tb_balance.ShowText = false;
            tb_balance.Size = new System.Drawing.Size(256, 43);
            tb_balance.TabIndex = 0;
            tb_balance.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_balance.Watermark = "";
            // 
            // lb_assetName_balance
            // 
            lb_assetName_balance.AutoSize = true;
            lb_assetName_balance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_assetName_balance.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_assetName_balance.Location = new System.Drawing.Point(521, 62);
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
            lb_amount.Location = new System.Drawing.Point(42, 63);
            lb_amount.Name = "lb_amount";
            lb_amount.Size = new System.Drawing.Size(62, 31);
            lb_amount.TabIndex = 15;
            lb_amount.Text = "住址";
            lb_amount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_ox_address
            // 
            lb_ox_address.AutoSize = true;
            lb_ox_address.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_ox_address.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_ox_address.Location = new System.Drawing.Point(17, 53);
            lb_ox_address.Name = "lb_ox_address";
            lb_ox_address.Size = new System.Drawing.Size(62, 31);
            lb_ox_address.TabIndex = 16;
            lb_ox_address.Text = "住址";
            lb_ox_address.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cb_ox_targetAddress
            // 
            cb_ox_targetAddress.DataSource = null;
            cb_ox_targetAddress.FillColor = System.Drawing.Color.White;
            cb_ox_targetAddress.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cb_ox_targetAddress.ItemHoverColor = System.Drawing.Color.FromArgb(155, 200, 255);
            cb_ox_targetAddress.ItemSelectForeColor = System.Drawing.Color.FromArgb(235, 243, 255);
            cb_ox_targetAddress.Location = new System.Drawing.Point(169, 48);
            cb_ox_targetAddress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cb_ox_targetAddress.MinimumSize = new System.Drawing.Size(63, 0);
            cb_ox_targetAddress.Name = "cb_ox_targetAddress";
            cb_ox_targetAddress.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cb_ox_targetAddress.Size = new System.Drawing.Size(735, 44);
            cb_ox_targetAddress.SymbolSize = 24;
            cb_ox_targetAddress.TabIndex = 17;
            cb_ox_targetAddress.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            cb_ox_targetAddress.Watermark = "";
            // 
            // uiTabControl1
            // 
            uiTabControl1.Controls.Add(tb_ox_transfer);
            uiTabControl1.Controls.Add(tb_lock_transfer);
            uiTabControl1.Controls.Add(tb_eth_transfer);
            uiTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            uiTabControl1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiTabControl1.ItemSize = new System.Drawing.Size(250, 40);
            uiTabControl1.Location = new System.Drawing.Point(1, 232);
            uiTabControl1.MainPage = "";
            uiTabControl1.Name = "uiTabControl1";
            uiTabControl1.SelectedIndex = 0;
            uiTabControl1.Size = new System.Drawing.Size(956, 373);
            uiTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            uiTabControl1.TabIndex = 18;
            uiTabControl1.TabUnSelectedForeColor = System.Drawing.Color.FromArgb(240, 240, 240);
            uiTabControl1.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiTabControl1.SelectedIndexChanged += uiTabControl1_SelectedIndexChanged;
            // 
            // tb_ox_transfer
            // 
            tb_ox_transfer.Controls.Add(cb_ox_targetAddress);
            tb_ox_transfer.Controls.Add(lb_ox_address);
            tb_ox_transfer.Location = new System.Drawing.Point(0, 40);
            tb_ox_transfer.Name = "tb_ox_transfer";
            tb_ox_transfer.Size = new System.Drawing.Size(956, 333);
            tb_ox_transfer.TabIndex = 0;
            tb_ox_transfer.Text = "tabPage1";
            tb_ox_transfer.UseVisualStyleBackColor = true;
            // 
            // tb_lock_transfer
            // 
            tb_lock_transfer.Controls.Add(tb_lock_index);
            tb_lock_transfer.Controls.Add(dp_lock_time);
            tb_lock_transfer.Controls.Add(st_timelock);
            tb_lock_transfer.Controls.Add(lb_lock_expire);
            tb_lock_transfer.Controls.Add(st_self);
            tb_lock_transfer.Controls.Add(tb_lock_pubkey);
            tb_lock_transfer.Controls.Add(lb_lock_pubkey);
            tb_lock_transfer.Location = new System.Drawing.Point(0, 40);
            tb_lock_transfer.Name = "tb_lock_transfer";
            tb_lock_transfer.Size = new System.Drawing.Size(200, 60);
            tb_lock_transfer.TabIndex = 1;
            tb_lock_transfer.Text = "tabPage2";
            tb_lock_transfer.UseVisualStyleBackColor = true;
            // 
            // tb_lock_index
            // 
            tb_lock_index.FillColor = System.Drawing.Color.White;
            tb_lock_index.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_lock_index.Location = new System.Drawing.Point(184, 226);
            tb_lock_index.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_lock_index.Maximum = 100000000D;
            tb_lock_index.Minimum = 10D;
            tb_lock_index.MinimumSize = new System.Drawing.Size(63, 0);
            tb_lock_index.Name = "tb_lock_index";
            tb_lock_index.NumPadType = NumPadType.Integer;
            tb_lock_index.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            tb_lock_index.Size = new System.Drawing.Size(256, 44);
            tb_lock_index.SymbolSize = 24;
            tb_lock_index.TabIndex = 39;
            tb_lock_index.Text = "10";
            tb_lock_index.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_lock_index.Watermark = "";
            // 
            // dp_lock_time
            // 
            dp_lock_time.FillColor = System.Drawing.Color.White;
            dp_lock_time.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dp_lock_time.Location = new System.Drawing.Point(562, 225);
            dp_lock_time.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            dp_lock_time.MaxLength = 19;
            dp_lock_time.MinimumSize = new System.Drawing.Size(63, 0);
            dp_lock_time.Name = "dp_lock_time";
            dp_lock_time.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            dp_lock_time.Size = new System.Drawing.Size(300, 44);
            dp_lock_time.SymbolDropDown = 61555;
            dp_lock_time.SymbolNormal = 61555;
            dp_lock_time.SymbolSize = 24;
            dp_lock_time.TabIndex = 23;
            dp_lock_time.Text = "2024-09-14 09:20:46";
            dp_lock_time.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            dp_lock_time.Value = new System.DateTime(2024, 9, 14, 9, 20, 46, 69);
            dp_lock_time.Watermark = "";
            // 
            // st_timelock
            // 
            st_timelock.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            st_timelock.Location = new System.Drawing.Point(410, 147);
            st_timelock.MinimumSize = new System.Drawing.Size(1, 1);
            st_timelock.Name = "st_timelock";
            st_timelock.Size = new System.Drawing.Size(161, 44);
            st_timelock.SwitchShape = UISwitch.UISwitchShape.Square;
            st_timelock.TabIndex = 22;
            st_timelock.Text = "uiSwitch1";
            st_timelock.ValueChanged += st_timelock_ValueChanged;
            // 
            // lb_lock_expire
            // 
            lb_lock_expire.AutoSize = true;
            lb_lock_expire.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_lock_expire.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_lock_expire.Location = new System.Drawing.Point(14, 231);
            lb_lock_expire.Name = "lb_lock_expire";
            lb_lock_expire.Size = new System.Drawing.Size(62, 31);
            lb_lock_expire.TabIndex = 21;
            lb_lock_expire.Text = "住址";
            lb_lock_expire.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // st_self
            // 
            st_self.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            st_self.Location = new System.Drawing.Point(184, 147);
            st_self.MinimumSize = new System.Drawing.Size(1, 1);
            st_self.Name = "st_self";
            st_self.Size = new System.Drawing.Size(161, 44);
            st_self.SwitchShape = UISwitch.UISwitchShape.Square;
            st_self.TabIndex = 19;
            st_self.Text = "uiSwitch1";
            st_self.ValueChanged += st_self_ValueChanged;
            // 
            // tb_lock_pubkey
            // 
            tb_lock_pubkey.Cursor = System.Windows.Forms.Cursors.IBeam;
            tb_lock_pubkey.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_lock_pubkey.Location = new System.Drawing.Point(184, 29);
            tb_lock_pubkey.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_lock_pubkey.MinimumSize = new System.Drawing.Size(1, 16);
            tb_lock_pubkey.Multiline = true;
            tb_lock_pubkey.Name = "tb_lock_pubkey";
            tb_lock_pubkey.Padding = new System.Windows.Forms.Padding(5);
            tb_lock_pubkey.ShowText = false;
            tb_lock_pubkey.Size = new System.Drawing.Size(743, 93);
            tb_lock_pubkey.TabIndex = 16;
            tb_lock_pubkey.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_lock_pubkey.Watermark = "";
            // 
            // lb_lock_pubkey
            // 
            lb_lock_pubkey.AutoSize = true;
            lb_lock_pubkey.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_lock_pubkey.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_lock_pubkey.Location = new System.Drawing.Point(14, 59);
            lb_lock_pubkey.Name = "lb_lock_pubkey";
            lb_lock_pubkey.Size = new System.Drawing.Size(62, 31);
            lb_lock_pubkey.TabIndex = 17;
            lb_lock_pubkey.Text = "住址";
            lb_lock_pubkey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_eth_transfer
            // 
            tb_eth_transfer.Controls.Add(tb_eth_lockindex);
            tb_eth_transfer.Controls.Add(lb_eth_lockindex);
            tb_eth_transfer.Controls.Add(cb_eth_address);
            tb_eth_transfer.Controls.Add(lb_eth_address);
            tb_eth_transfer.Location = new System.Drawing.Point(0, 40);
            tb_eth_transfer.Name = "tb_eth_transfer";
            tb_eth_transfer.Size = new System.Drawing.Size(200, 60);
            tb_eth_transfer.TabIndex = 2;
            tb_eth_transfer.Text = "tabPage1";
            tb_eth_transfer.UseVisualStyleBackColor = true;
            // 
            // tb_eth_lockindex
            // 
            tb_eth_lockindex.FillColor = System.Drawing.Color.White;
            tb_eth_lockindex.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_eth_lockindex.Location = new System.Drawing.Point(181, 191);
            tb_eth_lockindex.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_eth_lockindex.Maximum = 100000000D;
            tb_eth_lockindex.Minimum = 0D;
            tb_eth_lockindex.MinimumSize = new System.Drawing.Size(63, 0);
            tb_eth_lockindex.Name = "tb_eth_lockindex";
            tb_eth_lockindex.NumPadType = NumPadType.Integer;
            tb_eth_lockindex.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            tb_eth_lockindex.Size = new System.Drawing.Size(256, 44);
            tb_eth_lockindex.SymbolSize = 24;
            tb_eth_lockindex.TabIndex = 40;
            tb_eth_lockindex.Text = "0";
            tb_eth_lockindex.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_eth_lockindex.Watermark = "";
            // 
            // lb_eth_lockindex
            // 
            lb_eth_lockindex.AutoSize = true;
            lb_eth_lockindex.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_eth_lockindex.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_eth_lockindex.Location = new System.Drawing.Point(29, 204);
            lb_eth_lockindex.Name = "lb_eth_lockindex";
            lb_eth_lockindex.Size = new System.Drawing.Size(62, 31);
            lb_eth_lockindex.TabIndex = 23;
            lb_eth_lockindex.Text = "住址";
            lb_eth_lockindex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cb_eth_address
            // 
            cb_eth_address.DataSource = null;
            cb_eth_address.FillColor = System.Drawing.Color.White;
            cb_eth_address.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cb_eth_address.ItemHoverColor = System.Drawing.Color.FromArgb(155, 200, 255);
            cb_eth_address.ItemSelectForeColor = System.Drawing.Color.FromArgb(235, 243, 255);
            cb_eth_address.Location = new System.Drawing.Point(181, 82);
            cb_eth_address.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cb_eth_address.MinimumSize = new System.Drawing.Size(63, 0);
            cb_eth_address.Name = "cb_eth_address";
            cb_eth_address.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cb_eth_address.Size = new System.Drawing.Size(735, 44);
            cb_eth_address.SymbolSize = 24;
            cb_eth_address.TabIndex = 19;
            cb_eth_address.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            cb_eth_address.Watermark = "";
            // 
            // lb_eth_address
            // 
            lb_eth_address.AutoSize = true;
            lb_eth_address.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_eth_address.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_eth_address.Location = new System.Drawing.Point(29, 87);
            lb_eth_address.Name = "lb_eth_address";
            lb_eth_address.Size = new System.Drawing.Size(62, 31);
            lb_eth_address.TabIndex = 18;
            lb_eth_address.Text = "住址";
            lb_eth_address.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_amount
            // 
            tb_amount.FillColor = System.Drawing.Color.White;
            tb_amount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_amount.Location = new System.Drawing.Point(182, 62);
            tb_amount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_amount.Maximum = 100000000D;
            tb_amount.Minimum = 0D;
            tb_amount.MinimumSize = new System.Drawing.Size(63, 0);
            tb_amount.Name = "tb_amount";
            tb_amount.NumPadType = NumPadType.Integer;
            tb_amount.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            tb_amount.Size = new System.Drawing.Size(256, 44);
            tb_amount.SymbolSize = 24;
            tb_amount.TabIndex = 38;
            tb_amount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_amount.Watermark = "";
            // 
            // uiTrackBar1
            // 
            uiTrackBar1.BarSize = 50;
            uiTrackBar1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiTrackBar1.Location = new System.Drawing.Point(42, 114);
            uiTrackBar1.MinimumSize = new System.Drawing.Size(1, 1);
            uiTrackBar1.Name = "uiTrackBar1";
            uiTrackBar1.Size = new System.Drawing.Size(887, 44);
            uiTrackBar1.TabIndex = 41;
            uiTrackBar1.Text = "uiTrackBar1";
            uiTrackBar1.ValueChanged += uiTrackBar1_ValueChanged;
            // 
            // uiRuler1
            // 
            uiRuler1.BackColor = System.Drawing.Color.Transparent;
            uiRuler1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiRuler1.Location = new System.Drawing.Point(39, 158);
            uiRuler1.MinimumSize = new System.Drawing.Size(1, 1);
            uiRuler1.Name = "uiRuler1";
            uiRuler1.Size = new System.Drawing.Size(887, 44);
            uiRuler1.TabIndex = 40;
            uiRuler1.Text = "uiRuler1";
            // 
            // MasterSeniorTransferForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(958, 699);
            Controls.Add(uiTrackBar1);
            Controls.Add(uiRuler1);
            Controls.Add(tb_amount);
            Controls.Add(uiTabControl1);
            Controls.Add(lb_amount);
            Controls.Add(tb_balance);
            Controls.Add(lb_assetName_balance);
            Name = "MasterSeniorTransferForm";
            Text = "UIEditFrom";
            Controls.SetChildIndex(lb_assetName_balance, 0);
            Controls.SetChildIndex(tb_balance, 0);
            Controls.SetChildIndex(lb_amount, 0);
            Controls.SetChildIndex(uiTabControl1, 0);
            Controls.SetChildIndex(pnlBtm, 0);
            Controls.SetChildIndex(tb_amount, 0);
            Controls.SetChildIndex(uiRuler1, 0);
            Controls.SetChildIndex(uiTrackBar1, 0);
            pnlBtm.ResumeLayout(false);
            uiTabControl1.ResumeLayout(false);
            tb_ox_transfer.ResumeLayout(false);
            tb_ox_transfer.PerformLayout();
            tb_lock_transfer.ResumeLayout(false);
            tb_lock_transfer.PerformLayout();
            tb_eth_transfer.ResumeLayout(false);
            tb_eth_transfer.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private UITextBox tb_balance;
        private UILabel lb_assetName_balance;
        private UIRadioButton rbMale;
        private UIRadioButton rbFemale;
        private UITextBox edtAge;
        private UILabel lb_eth_lockindex;
        private UIComboBox cbDepartment;
        private UILabel uiLabel5;
        private UILabel uiLabel6;
        private UIDatePicker edtDate;
        private UILabel lb_amount;
        private UILabel lb_ox_address;
        private UIComboBox cb_ox_targetAddress;
        private UITabControl uiTabControl1;
        private System.Windows.Forms.TabPage tb_ox_transfer;
        private System.Windows.Forms.TabPage tb_lock_transfer;
        private System.Windows.Forms.TabPage tb_eth_transfer;
        private UITextBox tb_lock_pubkey;
        private UILabel lb_lock_pubkey;
        private UISwitch st_self;
        private UILabel lb_lock_expire;
        private UIDatetimePicker dp_lock_time;
        private UISwitch st_timelock;
        private UIComboBox cb_eth_address;
        private UILabel lb_eth_address;
        private UINumPadTextBox tb_lock_index;
        private UINumPadTextBox tb_eth_lockindex;
        private UINumPadTextBox tb_amount;
        private UITrackBar uiTrackBar1;
        private UIRuler uiRuler1;
    }
}