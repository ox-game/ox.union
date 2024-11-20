namespace OX.Tablet.UIs.MarkSix
{
    partial class EmailConfigPage
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
            bt_do_setting = new Sunny.UI.UIButton();
            L1 = new Sunny.UI.UILine();
            lb_smtp = new Sunny.UI.UILabel();
            lb_pop3 = new Sunny.UI.UILabel();
            tb_smtp = new Sunny.UI.UITextBox();
            tb_pop3 = new Sunny.UI.UITextBox();
            tb_pwd = new Sunny.UI.UITextBox();
            tb_username = new Sunny.UI.UITextBox();
            lb_pwd = new Sunny.UI.UILabel();
            lb_username = new Sunny.UI.UILabel();
            tb_smtp_port = new Sunny.UI.UITextBox();
            tb_pop3_port = new Sunny.UI.UITextBox();
            SuspendLayout();
            // 
            // bt_do_setting
            // 
            bt_do_setting.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_do_setting.Location = new System.Drawing.Point(304, 440);
            bt_do_setting.MinimumSize = new System.Drawing.Size(1, 1);
            bt_do_setting.Name = "bt_do_setting";
            bt_do_setting.Radius = 50;
            bt_do_setting.Size = new System.Drawing.Size(196, 52);
            bt_do_setting.TabIndex = 16;
            bt_do_setting.Text = "uiButton1";
            bt_do_setting.Click += bt_refresh_Click;
            // 
            // L1
            // 
            L1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            L1.BackColor = System.Drawing.Color.Transparent;
            L1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            L1.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            L1.Location = new System.Drawing.Point(17, 23);
            L1.MinimumSize = new System.Drawing.Size(1, 1);
            L1.Name = "L1";
            L1.Size = new System.Drawing.Size(1119, 26);
            L1.TabIndex = 17;
            L1.Text = "uiLine1";
            // 
            // lb_smtp
            // 
            lb_smtp.AutoSize = true;
            lb_smtp.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_smtp.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_smtp.Location = new System.Drawing.Point(29, 90);
            lb_smtp.Name = "lb_smtp";
            lb_smtp.Size = new System.Drawing.Size(62, 31);
            lb_smtp.TabIndex = 18;
            lb_smtp.Text = "住址";
            lb_smtp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_pop3
            // 
            lb_pop3.AutoSize = true;
            lb_pop3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_pop3.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_pop3.Location = new System.Drawing.Point(29, 157);
            lb_pop3.Name = "lb_pop3";
            lb_pop3.Size = new System.Drawing.Size(62, 31);
            lb_pop3.TabIndex = 19;
            lb_pop3.Text = "住址";
            lb_pop3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_smtp
            // 
            tb_smtp.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_smtp.Location = new System.Drawing.Point(195, 88);
            tb_smtp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_smtp.MinimumSize = new System.Drawing.Size(1, 16);
            tb_smtp.Name = "tb_smtp";
            tb_smtp.Padding = new System.Windows.Forms.Padding(5);
            tb_smtp.ShowText = false;
            tb_smtp.Size = new System.Drawing.Size(493, 44);
            tb_smtp.TabIndex = 20;
            tb_smtp.Text = "smtp.qq.com";
            tb_smtp.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_smtp.Watermark = "";
            // 
            // tb_pop3
            // 
            tb_pop3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_pop3.Location = new System.Drawing.Point(195, 150);
            tb_pop3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_pop3.MinimumSize = new System.Drawing.Size(1, 16);
            tb_pop3.Name = "tb_pop3";
            tb_pop3.Padding = new System.Windows.Forms.Padding(5);
            tb_pop3.ShowText = false;
            tb_pop3.Size = new System.Drawing.Size(493, 44);
            tb_pop3.TabIndex = 21;
            tb_pop3.Text = "pop.qq.com";
            tb_pop3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_pop3.Watermark = "";
            // 
            // tb_pwd
            // 
            tb_pwd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_pwd.Location = new System.Drawing.Point(195, 328);
            tb_pwd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_pwd.MinimumSize = new System.Drawing.Size(1, 16);
            tb_pwd.Name = "tb_pwd";
            tb_pwd.Padding = new System.Windows.Forms.Padding(5);
            tb_pwd.PasswordChar = '*';
            tb_pwd.ShowText = false;
            tb_pwd.Size = new System.Drawing.Size(493, 44);
            tb_pwd.TabIndex = 25;
            tb_pwd.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_pwd.Watermark = "";
            // 
            // tb_username
            // 
            tb_username.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_username.Location = new System.Drawing.Point(195, 266);
            tb_username.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_username.MinimumSize = new System.Drawing.Size(1, 16);
            tb_username.Name = "tb_username";
            tb_username.Padding = new System.Windows.Forms.Padding(5);
            tb_username.ShowText = false;
            tb_username.Size = new System.Drawing.Size(493, 44);
            tb_username.TabIndex = 24;
            tb_username.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_username.Watermark = "";
            // 
            // lb_pwd
            // 
            lb_pwd.AutoSize = true;
            lb_pwd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_pwd.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_pwd.Location = new System.Drawing.Point(29, 335);
            lb_pwd.Name = "lb_pwd";
            lb_pwd.Size = new System.Drawing.Size(62, 31);
            lb_pwd.TabIndex = 23;
            lb_pwd.Text = "住址";
            lb_pwd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_username
            // 
            lb_username.AutoSize = true;
            lb_username.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_username.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_username.Location = new System.Drawing.Point(29, 268);
            lb_username.Name = "lb_username";
            lb_username.Size = new System.Drawing.Size(62, 31);
            lb_username.TabIndex = 22;
            lb_username.Text = "住址";
            lb_username.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_smtp_port
            // 
            tb_smtp_port.DoubleValue = 587D;
            tb_smtp_port.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_smtp_port.IntValue = 587;
            tb_smtp_port.Location = new System.Drawing.Point(727, 86);
            tb_smtp_port.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_smtp_port.MinimumSize = new System.Drawing.Size(1, 16);
            tb_smtp_port.Name = "tb_smtp_port";
            tb_smtp_port.Padding = new System.Windows.Forms.Padding(5);
            tb_smtp_port.ShowText = false;
            tb_smtp_port.Size = new System.Drawing.Size(140, 44);
            tb_smtp_port.TabIndex = 26;
            tb_smtp_port.Text = "587";
            tb_smtp_port.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_smtp_port.Watermark = "";
            // 
            // tb_pop3_port
            // 
            tb_pop3_port.DoubleValue = 995D;
            tb_pop3_port.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_pop3_port.IntValue = 995;
            tb_pop3_port.Location = new System.Drawing.Point(727, 148);
            tb_pop3_port.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_pop3_port.MinimumSize = new System.Drawing.Size(1, 16);
            tb_pop3_port.Name = "tb_pop3_port";
            tb_pop3_port.Padding = new System.Windows.Forms.Padding(5);
            tb_pop3_port.ShowText = false;
            tb_pop3_port.Size = new System.Drawing.Size(140, 44);
            tb_pop3_port.TabIndex = 27;
            tb_pop3_port.Text = "995";
            tb_pop3_port.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_pop3_port.Watermark = "";
            // 
            // EmailConfigPage
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1163, 845);
            Controls.Add(tb_pop3_port);
            Controls.Add(tb_smtp_port);
            Controls.Add(tb_pwd);
            Controls.Add(tb_username);
            Controls.Add(lb_pwd);
            Controls.Add(lb_username);
            Controls.Add(tb_pop3);
            Controls.Add(tb_smtp);
            Controls.Add(lb_pop3);
            Controls.Add(lb_smtp);
            Controls.Add(bt_do_setting);
            Controls.Add(L1);
            Name = "EmailConfigPage";
            Text = "PlazaForm";
            Initialize += OrderHistory_Initialize;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Sunny.UI.UIButton bt_do_setting;
        private Sunny.UI.UILine L1;
        private Sunny.UI.UILabel lb_smtp;
        private Sunny.UI.UILabel lb_pop3;
        private Sunny.UI.UITextBox tb_smtp;
        private Sunny.UI.UITextBox tb_pop3;
        private Sunny.UI.UITextBox tb_pwd;
        private Sunny.UI.UITextBox tb_username;
        private Sunny.UI.UILabel lb_pwd;
        private Sunny.UI.UILabel lb_username;
        private Sunny.UI.UITextBox tb_smtp_port;
        private Sunny.UI.UITextBox tb_pop3_port;
    }
}