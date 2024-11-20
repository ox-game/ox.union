namespace OX.Tablet
{
    partial class BackupPrivateKeyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupPrivateKeyForm));
            lb_pwd = new Sunny.UI.UILabel();
            bt_exit = new Sunny.UI.UIButton();
            tb_prikey = new Sunny.UI.UIRichTextBox();
            bt_show = new Sunny.UI.UIButton();
            bt_copy = new Sunny.UI.UIButton();
            tb_pwd = new Sunny.UI.UINumPadTextBox();
            SuspendLayout();
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
            // bt_exit
            // 
            bt_exit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_exit.Location = new System.Drawing.Point(281, 298);
            bt_exit.MinimumSize = new System.Drawing.Size(1, 1);
            bt_exit.Name = "bt_exit";
            bt_exit.Size = new System.Drawing.Size(196, 52);
            bt_exit.TabIndex = 3;
            bt_exit.Text = "uiButton1";
            bt_exit.Click += bt_exit_Click;
            // 
            // tb_prikey
            // 
            tb_prikey.FillColor = System.Drawing.Color.White;
            tb_prikey.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_prikey.Location = new System.Drawing.Point(37, 127);
            tb_prikey.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_prikey.MinimumSize = new System.Drawing.Size(1, 1);
            tb_prikey.Name = "tb_prikey";
            tb_prikey.Padding = new System.Windows.Forms.Padding(2);
            tb_prikey.ReadOnly = true;
            tb_prikey.ShowText = false;
            tb_prikey.Size = new System.Drawing.Size(440, 133);
            tb_prikey.TabIndex = 15;
            tb_prikey.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_show
            // 
            bt_show.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_show.Location = new System.Drawing.Point(484, 135);
            bt_show.MinimumSize = new System.Drawing.Size(1, 1);
            bt_show.Name = "bt_show";
            bt_show.Size = new System.Drawing.Size(116, 52);
            bt_show.TabIndex = 16;
            bt_show.Text = "uiButton1";
            bt_show.Click += bt_show_Click;
            // 
            // bt_copy
            // 
            bt_copy.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_copy.Location = new System.Drawing.Point(484, 201);
            bt_copy.MinimumSize = new System.Drawing.Size(1, 1);
            bt_copy.Name = "bt_copy";
            bt_copy.Size = new System.Drawing.Size(116, 52);
            bt_copy.TabIndex = 17;
            bt_copy.Text = "uiButton2";
            bt_copy.Click += bt_copy_Click;
            // 
            // tb_pwd
            // 
            tb_pwd.DecimalPlaces = 0;
            tb_pwd.FillColor = System.Drawing.Color.White;
            tb_pwd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_pwd.Location = new System.Drawing.Point(221, 48);
            tb_pwd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_pwd.Maximum = 1000000000D;
            tb_pwd.Minimum = 0D;
            tb_pwd.MinimumSize = new System.Drawing.Size(63, 0);
            tb_pwd.Name = "tb_pwd";
            tb_pwd.NumPadType = Sunny.UI.NumPadType.Integer;
            tb_pwd.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            tb_pwd.Size = new System.Drawing.Size(256, 44);
            tb_pwd.SymbolSize = 24;
            tb_pwd.TabIndex = 39;
            tb_pwd.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_pwd.Watermark = "";
            // 
            // BackupPrivateKeyForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            AutoSize = true;
            BackColor = System.Drawing.Color.FromArgb(56, 56, 56);
            ClientSize = new System.Drawing.Size(626, 384);
            ControlBox = false;
            Controls.Add(tb_pwd);
            Controls.Add(bt_copy);
            Controls.Add(bt_show);
            Controls.Add(tb_prikey);
            Controls.Add(bt_exit);
            Controls.Add(lb_pwd);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "BackupPrivateKeyForm";
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "UnlockAccountForm";
            TopMost = true;
            FormClosing += UnlockAccountForm_FormClosing;
            ResumeLayout(false);
        }

        #endregion
        private Sunny.UI.UILabel lb_pwd;
        private Sunny.UI.UIButton bt_exit;
        private Sunny.UI.UIRichTextBox tb_prikey;
        private Sunny.UI.UIButton bt_show;
        private Sunny.UI.UIButton bt_copy;
        private Sunny.UI.UINumPadTextBox tb_pwd;
    }
}