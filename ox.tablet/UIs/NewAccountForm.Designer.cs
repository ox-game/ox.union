namespace OX.Tablet
{
    partial class NewAccountForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewAccountForm));
            bt_register = new Sunny.UI.UIButton();
            lb_pwd = new Sunny.UI.UILabel();
            tb_privateKey = new Sunny.UI.UIRichTextBox();
            lb_prikey = new Sunny.UI.UILabel();
            lb_pubkey = new Sunny.UI.UILabel();
            bt_refresh = new Sunny.UI.UIButton();
            bt_copy = new Sunny.UI.UIButton();
            tb_address = new Sunny.UI.UITextBox();
            lb_address = new Sunny.UI.UILabel();
            tb_pubkey = new Sunny.UI.UIRichTextBox();
            bt_clear = new Sunny.UI.UIButton();
            bt_clip = new Sunny.UI.UIButton();
            bt_exit = new Sunny.UI.UIButton();
            lb_pwd_confirm = new Sunny.UI.UILabel();
            tb_pwd = new Sunny.UI.UINumPadTextBox();
            tb_pwd_confirm = new Sunny.UI.UINumPadTextBox();
            SuspendLayout();
            // 
            // bt_register
            // 
            bt_register.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_register.Location = new System.Drawing.Point(600, 595);
            bt_register.MinimumSize = new System.Drawing.Size(1, 1);
            bt_register.Name = "bt_register";
            bt_register.Size = new System.Drawing.Size(191, 52);
            bt_register.TabIndex = 5;
            bt_register.Text = "uiButton1";
            bt_register.Click += bt_register_Click;
            // 
            // lb_pwd
            // 
            lb_pwd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_pwd.ForeColor = System.Drawing.Color.White;
            lb_pwd.Location = new System.Drawing.Point(42, 26);
            lb_pwd.Name = "lb_pwd";
            lb_pwd.Size = new System.Drawing.Size(150, 34);
            lb_pwd.TabIndex = 4;
            lb_pwd.Text = "uiLabel1";
            // 
            // tb_privateKey
            // 
            tb_privateKey.FillColor = System.Drawing.Color.White;
            tb_privateKey.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_privateKey.Location = new System.Drawing.Point(204, 164);
            tb_privateKey.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_privateKey.MinimumSize = new System.Drawing.Size(1, 1);
            tb_privateKey.Name = "tb_privateKey";
            tb_privateKey.Padding = new System.Windows.Forms.Padding(2);
            tb_privateKey.ShowText = false;
            tb_privateKey.Size = new System.Drawing.Size(587, 133);
            tb_privateKey.TabIndex = 6;
            tb_privateKey.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            tb_privateKey.TextChanged += tb_privateKey_TextChanged;
            // 
            // lb_prikey
            // 
            lb_prikey.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_prikey.ForeColor = System.Drawing.Color.White;
            lb_prikey.Location = new System.Drawing.Point(42, 170);
            lb_prikey.Name = "lb_prikey";
            lb_prikey.Size = new System.Drawing.Size(150, 34);
            lb_prikey.TabIndex = 7;
            lb_prikey.Text = "uiLabel1";
            // 
            // lb_pubkey
            // 
            lb_pubkey.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_pubkey.ForeColor = System.Drawing.Color.White;
            lb_pubkey.Location = new System.Drawing.Point(42, 400);
            lb_pubkey.Name = "lb_pubkey";
            lb_pubkey.Size = new System.Drawing.Size(150, 34);
            lb_pubkey.TabIndex = 8;
            lb_pubkey.Text = "uiLabel2";
            // 
            // bt_refresh
            // 
            bt_refresh.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_refresh.Location = new System.Drawing.Point(204, 305);
            bt_refresh.MinimumSize = new System.Drawing.Size(1, 1);
            bt_refresh.Name = "bt_refresh";
            bt_refresh.Size = new System.Drawing.Size(116, 52);
            bt_refresh.TabIndex = 10;
            bt_refresh.Text = "uiButton1";
            bt_refresh.Click += bt_refresh_Click;
            // 
            // bt_copy
            // 
            bt_copy.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_copy.Location = new System.Drawing.Point(361, 305);
            bt_copy.MinimumSize = new System.Drawing.Size(1, 1);
            bt_copy.Name = "bt_copy";
            bt_copy.Size = new System.Drawing.Size(116, 52);
            bt_copy.TabIndex = 11;
            bt_copy.Text = "uiButton1";
            bt_copy.Click += bt_copy_Click;
            // 
            // tb_address
            // 
            tb_address.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_address.Location = new System.Drawing.Point(204, 536);
            tb_address.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_address.MinimumSize = new System.Drawing.Size(1, 16);
            tb_address.Name = "tb_address";
            tb_address.Padding = new System.Windows.Forms.Padding(5);
            tb_address.ReadOnly = true;
            tb_address.ShowText = false;
            tb_address.Size = new System.Drawing.Size(587, 44);
            tb_address.TabIndex = 13;
            tb_address.Text = "uiTextBox2";
            tb_address.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_address.Watermark = "";
            // 
            // lb_address
            // 
            lb_address.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_address.ForeColor = System.Drawing.Color.White;
            lb_address.Location = new System.Drawing.Point(42, 541);
            lb_address.Name = "lb_address";
            lb_address.Size = new System.Drawing.Size(150, 34);
            lb_address.TabIndex = 12;
            lb_address.Text = "uiLabel2";
            // 
            // tb_pubkey
            // 
            tb_pubkey.FillColor = System.Drawing.Color.White;
            tb_pubkey.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_pubkey.Location = new System.Drawing.Point(204, 388);
            tb_pubkey.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_pubkey.MinimumSize = new System.Drawing.Size(1, 1);
            tb_pubkey.Name = "tb_pubkey";
            tb_pubkey.Padding = new System.Windows.Forms.Padding(2);
            tb_pubkey.ReadOnly = true;
            tb_pubkey.ShowText = false;
            tb_pubkey.Size = new System.Drawing.Size(587, 133);
            tb_pubkey.TabIndex = 14;
            tb_pubkey.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_clear
            // 
            bt_clear.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_clear.Location = new System.Drawing.Point(675, 305);
            bt_clear.MinimumSize = new System.Drawing.Size(1, 1);
            bt_clear.Name = "bt_clear";
            bt_clear.Size = new System.Drawing.Size(116, 52);
            bt_clear.TabIndex = 16;
            bt_clear.Text = "uiButton1";
            bt_clear.Click += bt_clear_Click;
            // 
            // bt_clip
            // 
            bt_clip.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_clip.Location = new System.Drawing.Point(520, 305);
            bt_clip.MinimumSize = new System.Drawing.Size(1, 1);
            bt_clip.Name = "bt_clip";
            bt_clip.Size = new System.Drawing.Size(116, 52);
            bt_clip.TabIndex = 15;
            bt_clip.Text = "uiButton1";
            bt_clip.Click += bt_clip_Click;
            // 
            // bt_exit
            // 
            bt_exit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_exit.Location = new System.Drawing.Point(361, 595);
            bt_exit.MinimumSize = new System.Drawing.Size(1, 1);
            bt_exit.Name = "bt_exit";
            bt_exit.Size = new System.Drawing.Size(191, 52);
            bt_exit.TabIndex = 17;
            bt_exit.Text = "uiButton1";
            bt_exit.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_exit.Click += bt_exit_Click;
            // 
            // lb_pwd_confirm
            // 
            lb_pwd_confirm.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_pwd_confirm.ForeColor = System.Drawing.Color.White;
            lb_pwd_confirm.Location = new System.Drawing.Point(42, 80);
            lb_pwd_confirm.Name = "lb_pwd_confirm";
            lb_pwd_confirm.Size = new System.Drawing.Size(150, 34);
            lb_pwd_confirm.TabIndex = 19;
            lb_pwd_confirm.Text = "uiLabel1";
            // 
            // tb_pwd
            // 
            tb_pwd.FillColor = System.Drawing.Color.White;
            tb_pwd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_pwd.Location = new System.Drawing.Point(204, 21);
            tb_pwd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_pwd.Maximum = 1000000000D;
            tb_pwd.Minimum = 0D;
            tb_pwd.MinimumSize = new System.Drawing.Size(63, 0);
            tb_pwd.Name = "tb_pwd";
            tb_pwd.NumPadType = Sunny.UI.NumPadType.Integer;
            tb_pwd.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            tb_pwd.Size = new System.Drawing.Size(587, 44);
            tb_pwd.SymbolSize = 24;
            tb_pwd.TabIndex = 41;
            tb_pwd.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_pwd.Watermark = "";
            // 
            // tb_pwd_confirm
            // 
            tb_pwd_confirm.FillColor = System.Drawing.Color.White;
            tb_pwd_confirm.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_pwd_confirm.Location = new System.Drawing.Point(204, 75);
            tb_pwd_confirm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_pwd_confirm.Maximum = 1000000000D;
            tb_pwd_confirm.Minimum = 0D;
            tb_pwd_confirm.MinimumSize = new System.Drawing.Size(63, 0);
            tb_pwd_confirm.Name = "tb_pwd_confirm";
            tb_pwd_confirm.NumPadType = Sunny.UI.NumPadType.Integer;
            tb_pwd_confirm.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            tb_pwd_confirm.Size = new System.Drawing.Size(587, 44);
            tb_pwd_confirm.SymbolSize = 24;
            tb_pwd_confirm.TabIndex = 42;
            tb_pwd_confirm.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_pwd_confirm.Watermark = "";
            // 
            // NewAccountForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            AutoSize = true;
            BackColor = System.Drawing.Color.FromArgb(56, 56, 56);
            ClientSize = new System.Drawing.Size(834, 677);
            ControlBox = false;
            Controls.Add(tb_pwd_confirm);
            Controls.Add(tb_pwd);
            Controls.Add(lb_pwd_confirm);
            Controls.Add(bt_exit);
            Controls.Add(bt_clear);
            Controls.Add(bt_clip);
            Controls.Add(tb_pubkey);
            Controls.Add(tb_address);
            Controls.Add(lb_address);
            Controls.Add(bt_copy);
            Controls.Add(bt_refresh);
            Controls.Add(lb_pubkey);
            Controls.Add(lb_prikey);
            Controls.Add(tb_privateKey);
            Controls.Add(bt_register);
            Controls.Add(lb_pwd);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "NewAccountForm";
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "UnlockAccountForm";
            TopMost = true;
            FormClosing += NewAccountForm_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIButton bt_register;
        private Sunny.UI.UILabel lb_pwd;
        private Sunny.UI.UIRichTextBox tb_privateKey;
        private Sunny.UI.UILabel lb_prikey;
        private Sunny.UI.UILabel lb_pubkey;
        private Sunny.UI.UIButton bt_refresh;
        private Sunny.UI.UIButton bt_copy;
        private Sunny.UI.UITextBox tb_address;
        private Sunny.UI.UILabel lb_address;
        private Sunny.UI.UIRichTextBox tb_pubkey;
        private Sunny.UI.UIButton bt_clear;
        private Sunny.UI.UIButton bt_clip;
        private Sunny.UI.UIButton bt_exit;
        private Sunny.UI.UILabel lb_pwd_confirm;
        private Sunny.UI.UINumPadTextBox tb_pwd;
        private Sunny.UI.UINumPadTextBox tb_pwd_confirm;
    }
}