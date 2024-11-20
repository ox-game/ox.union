namespace OX.Tablet
{
    partial class SyncLockForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncLockForm));
            lb_pwd = new Sunny.UI.UILabel();
            bt_exit = new Sunny.UI.UIButton();
            bt_confirm_seed = new Sunny.UI.UIButton();
            lb_msg = new Sunny.UI.UILabel();
            SuspendLayout();
            // 
            // lb_pwd
            // 
            lb_pwd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_pwd.ForeColor = System.Drawing.Color.White;
            lb_pwd.Location = new System.Drawing.Point(127, 73);
            lb_pwd.Name = "lb_pwd";
            lb_pwd.Size = new System.Drawing.Size(328, 34);
            lb_pwd.TabIndex = 1;
            // 
            // bt_exit
            // 
            bt_exit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_exit.Location = new System.Drawing.Point(65, 164);
            bt_exit.MinimumSize = new System.Drawing.Size(1, 1);
            bt_exit.Name = "bt_exit";
            bt_exit.Size = new System.Drawing.Size(196, 52);
            bt_exit.TabIndex = 3;
            bt_exit.Text = "uiButton1";
            bt_exit.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_exit.Click += bt_exit_Click;
            // 
            // bt_confirm_seed
            // 
            bt_confirm_seed.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_confirm_seed.Location = new System.Drawing.Point(310, 164);
            bt_confirm_seed.MinimumSize = new System.Drawing.Size(1, 1);
            bt_confirm_seed.Name = "bt_confirm_seed";
            bt_confirm_seed.Size = new System.Drawing.Size(196, 52);
            bt_confirm_seed.TabIndex = 4;
            bt_confirm_seed.Text = "uiButton1";
            bt_confirm_seed.Click += bt_confirm_seed_Click;
            // 
            // lb_msg
            // 
            lb_msg.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_msg.ForeColor = System.Drawing.Color.White;
            lb_msg.Location = new System.Drawing.Point(125, 117);
            lb_msg.Name = "lb_msg";
            lb_msg.Size = new System.Drawing.Size(328, 34);
            lb_msg.TabIndex = 5;
            // 
            // SyncLockForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            AutoSize = true;
            BackColor = System.Drawing.Color.FromArgb(56, 56, 56);
            ClientSize = new System.Drawing.Size(533, 239);
            ControlBox = false;
            Controls.Add(lb_msg);
            Controls.Add(bt_confirm_seed);
            Controls.Add(bt_exit);
            Controls.Add(lb_pwd);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "SyncLockForm";
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
        private Sunny.UI.UIButton bt_confirm_seed;
        private Sunny.UI.UILabel lb_msg;
    }
}