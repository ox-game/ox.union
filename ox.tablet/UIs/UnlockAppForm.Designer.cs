namespace OX.Tablet
{
    partial class UnlockAppForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnlockAppForm));
            tb_pwd = new Sunny.UI.UITextBox();
            lb_pwd = new Sunny.UI.UILabel();
            bt_unlock = new Sunny.UI.UIButton();
            btn_0 = new Sunny.UI.UIButton();
            btn_1 = new Sunny.UI.UIButton();
            btn_2 = new Sunny.UI.UIButton();
            btn_3 = new Sunny.UI.UIButton();
            btn_4 = new Sunny.UI.UIButton();
            btn_9 = new Sunny.UI.UIButton();
            btn_8 = new Sunny.UI.UIButton();
            btn_7 = new Sunny.UI.UIButton();
            btn_6 = new Sunny.UI.UIButton();
            btn_5 = new Sunny.UI.UIButton();
            SuspendLayout();
            // 
            // tb_pwd
            // 
            tb_pwd.CanEmpty = true;
            tb_pwd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_pwd.Location = new System.Drawing.Point(198, 55);
            tb_pwd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_pwd.Maximum = 1000000000D;
            tb_pwd.Minimum = 0D;
            tb_pwd.MinimumSize = new System.Drawing.Size(1, 16);
            tb_pwd.Name = "tb_pwd";
            tb_pwd.Padding = new System.Windows.Forms.Padding(5);
            tb_pwd.PasswordChar = '*';
            tb_pwd.ShowText = false;
            tb_pwd.Size = new System.Drawing.Size(279, 44);
            tb_pwd.TabIndex = 0;
            tb_pwd.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_pwd.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            tb_pwd.Watermark = "";
            tb_pwd.KeyDown += tb_pwd_KeyDown;
            // 
            // lb_pwd
            // 
            lb_pwd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_pwd.ForeColor = System.Drawing.Color.White;
            lb_pwd.Location = new System.Drawing.Point(37, 58);
            lb_pwd.Name = "lb_pwd";
            lb_pwd.Size = new System.Drawing.Size(150, 34);
            lb_pwd.TabIndex = 1;
            lb_pwd.Text = "uiLabel1";
            // 
            // bt_unlock
            // 
            bt_unlock.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_unlock.Location = new System.Drawing.Point(198, 397);
            bt_unlock.MinimumSize = new System.Drawing.Size(1, 1);
            bt_unlock.Name = "bt_unlock";
            bt_unlock.Radius = 50;
            bt_unlock.Size = new System.Drawing.Size(196, 52);
            bt_unlock.TabIndex = 2;
            bt_unlock.Text = "uiButton1";
            bt_unlock.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_unlock.Click += bt_unlock_Click;
            // 
            // btn_0
            // 
            btn_0.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_0.Location = new System.Drawing.Point(37, 136);
            btn_0.MinimumSize = new System.Drawing.Size(1, 1);
            btn_0.Name = "btn_0";
            btn_0.Radius = 80;
            btn_0.Size = new System.Drawing.Size(80, 80);
            btn_0.TabIndex = 4;
            btn_0.Text = "0";
            btn_0.Click += btn_0_Click;
            // 
            // btn_1
            // 
            btn_1.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_1.Location = new System.Drawing.Point(155, 136);
            btn_1.MinimumSize = new System.Drawing.Size(1, 1);
            btn_1.Name = "btn_1";
            btn_1.Radius = 80;
            btn_1.Size = new System.Drawing.Size(80, 80);
            btn_1.TabIndex = 5;
            btn_1.Text = "1";
            btn_1.Click += btn_1_Click;
            // 
            // btn_2
            // 
            btn_2.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_2.Location = new System.Drawing.Point(277, 136);
            btn_2.MinimumSize = new System.Drawing.Size(1, 1);
            btn_2.Name = "btn_2";
            btn_2.Radius = 80;
            btn_2.Size = new System.Drawing.Size(80, 80);
            btn_2.TabIndex = 6;
            btn_2.Text = "2";
            btn_2.Click += btn_2_Click;
            // 
            // btn_3
            // 
            btn_3.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_3.Location = new System.Drawing.Point(396, 136);
            btn_3.MinimumSize = new System.Drawing.Size(1, 1);
            btn_3.Name = "btn_3";
            btn_3.Radius = 80;
            btn_3.Size = new System.Drawing.Size(80, 80);
            btn_3.TabIndex = 7;
            btn_3.Text = "3";
            btn_3.Click += btn_3_Click;
            // 
            // btn_4
            // 
            btn_4.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_4.Location = new System.Drawing.Point(514, 136);
            btn_4.MinimumSize = new System.Drawing.Size(1, 1);
            btn_4.Name = "btn_4";
            btn_4.Radius = 80;
            btn_4.Size = new System.Drawing.Size(80, 80);
            btn_4.TabIndex = 8;
            btn_4.Text = "4";
            btn_4.Click += btn_4_Click;
            // 
            // btn_9
            // 
            btn_9.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_9.Location = new System.Drawing.Point(514, 243);
            btn_9.MinimumSize = new System.Drawing.Size(1, 1);
            btn_9.Name = "btn_9";
            btn_9.Radius = 80;
            btn_9.Size = new System.Drawing.Size(80, 80);
            btn_9.TabIndex = 13;
            btn_9.Text = "9";
            btn_9.Click += btn_9_Click;
            // 
            // btn_8
            // 
            btn_8.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_8.Location = new System.Drawing.Point(396, 243);
            btn_8.MinimumSize = new System.Drawing.Size(1, 1);
            btn_8.Name = "btn_8";
            btn_8.Radius = 80;
            btn_8.Size = new System.Drawing.Size(80, 80);
            btn_8.TabIndex = 12;
            btn_8.Text = "8";
            btn_8.Click += btn_8_Click;
            // 
            // btn_7
            // 
            btn_7.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_7.Location = new System.Drawing.Point(277, 243);
            btn_7.MinimumSize = new System.Drawing.Size(1, 1);
            btn_7.Name = "btn_7";
            btn_7.Radius = 80;
            btn_7.Size = new System.Drawing.Size(80, 80);
            btn_7.TabIndex = 11;
            btn_7.Text = "7";
            btn_7.Click += btn_7_Click;
            // 
            // btn_6
            // 
            btn_6.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_6.Location = new System.Drawing.Point(155, 243);
            btn_6.MinimumSize = new System.Drawing.Size(1, 1);
            btn_6.Name = "btn_6";
            btn_6.Radius = 80;
            btn_6.Size = new System.Drawing.Size(80, 80);
            btn_6.TabIndex = 10;
            btn_6.Text = "6";
            btn_6.Click += btn_6_Click;
            // 
            // btn_5
            // 
            btn_5.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_5.Location = new System.Drawing.Point(37, 243);
            btn_5.MinimumSize = new System.Drawing.Size(1, 1);
            btn_5.Name = "btn_5";
            btn_5.Radius = 80;
            btn_5.Size = new System.Drawing.Size(80, 80);
            btn_5.TabIndex = 9;
            btn_5.Text = "5";
            btn_5.Click += btn_5_Click;
            // 
            // UnlockAppForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            AutoSize = true;
            BackColor = System.Drawing.Color.FromArgb(56, 56, 56);
            ClientSize = new System.Drawing.Size(629, 475);
            ControlBox = false;
            Controls.Add(btn_9);
            Controls.Add(btn_8);
            Controls.Add(btn_7);
            Controls.Add(btn_6);
            Controls.Add(btn_5);
            Controls.Add(btn_4);
            Controls.Add(btn_3);
            Controls.Add(btn_2);
            Controls.Add(btn_1);
            Controls.Add(btn_0);
            Controls.Add(bt_unlock);
            Controls.Add(lb_pwd);
            Controls.Add(tb_pwd);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "UnlockAppForm";
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "UnlockAccountForm";
            TopMost = true;
            FormClosing += UnlockAccountForm_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UITextBox tb_pwd;
        private Sunny.UI.UILabel lb_pwd;
        private Sunny.UI.UIButton bt_unlock;
        private Sunny.UI.UIButton btn_0;
        private Sunny.UI.UIButton btn_1;
        private Sunny.UI.UIButton btn_2;
        private Sunny.UI.UIButton btn_3;
        private Sunny.UI.UIButton btn_4;
        private Sunny.UI.UIButton btn_9;
        private Sunny.UI.UIButton btn_8;
        private Sunny.UI.UIButton btn_7;
        private Sunny.UI.UIButton btn_6;
        private Sunny.UI.UIButton btn_5;
    }
}