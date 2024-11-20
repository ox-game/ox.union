namespace OX.Tablet
{
    partial class ConfirmDeleteAccountForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfirmDeleteAccountForm));
            lb_pwd = new Sunny.UI.UILabel();
            bt_unlock = new Sunny.UI.UIButton();
            bt_exit = new Sunny.UI.UIButton();
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
            // bt_unlock
            // 
            bt_unlock.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_unlock.Location = new System.Drawing.Point(281, 123);
            bt_unlock.MinimumSize = new System.Drawing.Size(1, 1);
            bt_unlock.Name = "bt_unlock";
            bt_unlock.Size = new System.Drawing.Size(196, 52);
            bt_unlock.TabIndex = 2;
            bt_unlock.Text = "uiButton1";
            bt_unlock.Click += bt_unlock_Click;
            // 
            // bt_exit
            // 
            bt_exit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_exit.Location = new System.Drawing.Point(37, 123);
            bt_exit.MinimumSize = new System.Drawing.Size(1, 1);
            bt_exit.Name = "bt_exit";
            bt_exit.Size = new System.Drawing.Size(196, 52);
            bt_exit.TabIndex = 4;
            bt_exit.Text = "uiButton1";
            bt_exit.Click += bt_exit_Click;
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
            tb_pwd.TabIndex = 40;
            tb_pwd.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_pwd.Watermark = "";
            // 
            // ConfirmDeleteAccountForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            AutoSize = true;
            BackColor = System.Drawing.Color.FromArgb(56, 56, 56);
            ClientSize = new System.Drawing.Size(533, 202);
            ControlBox = false;
            Controls.Add(tb_pwd);
            Controls.Add(bt_exit);
            Controls.Add(bt_unlock);
            Controls.Add(lb_pwd);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "ConfirmDeleteAccountForm";
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "UnlockAccountForm";
            TopMost = true;
            FormClosing += UnlockAccountForm_FormClosing;
            ResumeLayout(false);
        }

        #endregion
        private Sunny.UI.UILabel lb_pwd;
        private Sunny.UI.UIButton bt_unlock;
        private Sunny.UI.UIButton bt_exit;
        private Sunny.UI.UINumPadTextBox tb_pwd;
    }
}