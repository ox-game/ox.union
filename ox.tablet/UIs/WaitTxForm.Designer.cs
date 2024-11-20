namespace OX.Tablet
{
    partial class WaitTxForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaitTxForm));
            lb_msg = new Sunny.UI.UILabel();
            bt_close = new Sunny.UI.UIButton();
            uiWaitingBar1 = new Sunny.UI.UIWaitingBar();
            timer1 = new System.Windows.Forms.Timer(components);
            lb_title = new Sunny.UI.UILabel();
            SuspendLayout();
            // 
            // lb_msg
            // 
            lb_msg.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_msg.ForeColor = System.Drawing.Color.Red;
            lb_msg.Location = new System.Drawing.Point(68, 215);
            lb_msg.Name = "lb_msg";
            lb_msg.Size = new System.Drawing.Size(328, 34);
            lb_msg.TabIndex = 1;
            // 
            // bt_close
            // 
            bt_close.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_close.Location = new System.Drawing.Point(527, 197);
            bt_close.MinimumSize = new System.Drawing.Size(1, 1);
            bt_close.Name = "bt_close";
            bt_close.Size = new System.Drawing.Size(196, 52);
            bt_close.TabIndex = 3;
            bt_close.Text = "uiButton1";
            bt_close.Click += bt_exit_Click;
            // 
            // uiWaitingBar1
            // 
            uiWaitingBar1.FillColor = System.Drawing.Color.FromArgb(243, 249, 255);
            uiWaitingBar1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiWaitingBar1.ForeColor = System.Drawing.Color.FromArgb(80, 160, 255);
            uiWaitingBar1.Location = new System.Drawing.Point(40, 102);
            uiWaitingBar1.MinimumSize = new System.Drawing.Size(70, 23);
            uiWaitingBar1.Name = "uiWaitingBar1";
            uiWaitingBar1.Size = new System.Drawing.Size(683, 44);
            uiWaitingBar1.TabIndex = 4;
            uiWaitingBar1.Text = "uiWaitingBar1";
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 500;
            timer1.Tick += timer1_Tick;
            // 
            // lb_title
            // 
            lb_title.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_title.ForeColor = System.Drawing.Color.White;
            lb_title.Location = new System.Drawing.Point(40, 25);
            lb_title.Name = "lb_title";
            lb_title.Size = new System.Drawing.Size(683, 34);
            lb_title.TabIndex = 5;
            // 
            // WaitTxForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            AutoSize = true;
            BackColor = System.Drawing.Color.FromArgb(56, 56, 56);
            ClientSize = new System.Drawing.Size(786, 290);
            ControlBox = false;
            Controls.Add(lb_title);
            Controls.Add(uiWaitingBar1);
            Controls.Add(bt_close);
            Controls.Add(lb_msg);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "WaitTxForm";
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "UnlockAccountForm";
            TopMost = true;
            FormClosing += UnlockAccountForm_FormClosing;
            ResumeLayout(false);
        }

        #endregion
        private Sunny.UI.UILabel lb_msg;
        private Sunny.UI.UIButton bt_close;
        private Sunny.UI.UIWaitingBar uiWaitingBar1;
        private System.Windows.Forms.Timer timer1;
        private Sunny.UI.UILabel lb_title;
    }
}