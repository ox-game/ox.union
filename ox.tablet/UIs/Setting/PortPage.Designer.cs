namespace OX.Tablet.UIs.MarkSix
{
    partial class PortPage
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
            bt_push_message = new Sunny.UI.UIButton();
            tb_msg = new Sunny.UI.UIRichTextBox();
            lb_msg = new Sunny.UI.UILabel();
            bt_refresh = new Sunny.UI.UIButton();
            L1 = new Sunny.UI.UILine();
            pn_msg = new Sunny.UI.UIFlowLayoutPanel();
            SuspendLayout();
            // 
            // bt_push_message
            // 
            bt_push_message.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_push_message.Location = new System.Drawing.Point(22, 419);
            bt_push_message.MinimumSize = new System.Drawing.Size(1, 1);
            bt_push_message.Name = "bt_push_message";
            bt_push_message.Size = new System.Drawing.Size(196, 52);
            bt_push_message.TabIndex = 25;
            bt_push_message.Text = "uiButton1";
            bt_push_message.Click += bt_push_message_Click;
            // 
            // tb_msg
            // 
            tb_msg.FillColor = System.Drawing.Color.White;
            tb_msg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_msg.Location = new System.Drawing.Point(22, 121);
            tb_msg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_msg.MaxLength = 250;
            tb_msg.MinimumSize = new System.Drawing.Size(1, 1);
            tb_msg.Name = "tb_msg";
            tb_msg.Padding = new System.Windows.Forms.Padding(2);
            tb_msg.ShowText = false;
            tb_msg.Size = new System.Drawing.Size(538, 270);
            tb_msg.TabIndex = 26;
            tb_msg.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_msg
            // 
            lb_msg.AutoSize = true;
            lb_msg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_msg.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_msg.Location = new System.Drawing.Point(22, 78);
            lb_msg.Name = "lb_msg";
            lb_msg.Size = new System.Drawing.Size(106, 24);
            lb_msg.TabIndex = 27;
            lb_msg.Text = "Chinese:";
            // 
            // bt_refresh
            // 
            bt_refresh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            bt_refresh.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_refresh.Location = new System.Drawing.Point(1227, 20);
            bt_refresh.MinimumSize = new System.Drawing.Size(1, 1);
            bt_refresh.Name = "bt_refresh";
            bt_refresh.Size = new System.Drawing.Size(196, 52);
            bt_refresh.TabIndex = 28;
            bt_refresh.Text = "uiButton1";
            bt_refresh.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_refresh.Click += bt_refresh_Click_1;
            // 
            // L1
            // 
            L1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            L1.BackColor = System.Drawing.Color.Transparent;
            L1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            L1.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            L1.Location = new System.Drawing.Point(12, 34);
            L1.MinimumSize = new System.Drawing.Size(1, 1);
            L1.Name = "L1";
            L1.Size = new System.Drawing.Size(1200, 26);
            L1.TabIndex = 29;
            L1.Text = "uiLine1";
            // 
            // pn_msg
            // 
            pn_msg.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pn_msg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            pn_msg.Location = new System.Drawing.Point(581, 80);
            pn_msg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pn_msg.MinimumSize = new System.Drawing.Size(1, 1);
            pn_msg.Name = "pn_msg";
            pn_msg.Padding = new System.Windows.Forms.Padding(2, 10, 2, 2);
            pn_msg.RectColor = System.Drawing.Color.Transparent;
            pn_msg.RightToLeft = System.Windows.Forms.RightToLeft.No;
            pn_msg.ScrollBarHandleWidth = 100;
            pn_msg.ShowText = false;
            pn_msg.Size = new System.Drawing.Size(842, 736);
            pn_msg.TabIndex = 30;
            pn_msg.Text = "uiFlowLayoutPanel1";
            pn_msg.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PortPage
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1445, 845);
            Controls.Add(pn_msg);
            Controls.Add(bt_refresh);
            Controls.Add(L1);
            Controls.Add(lb_msg);
            Controls.Add(tb_msg);
            Controls.Add(bt_push_message);
            Name = "PortPage";
            Text = "PlazaForm";
            Initialize += OrderHistory_Initialize;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Sunny.UI.UIButton uiButton1;
        private Sunny.UI.UIButton uiButton2;
        private Sunny.UI.UIButton uiButton3;
        private Sunny.UI.UIButton uiButton4;
        private Sunny.UI.UIButton uiButton5;
        private Sunny.UI.UIButton uiButton6;
        private Sunny.UI.UIButton bt_push_message;
        private Sunny.UI.UIRichTextBox tb_msg;
        private Sunny.UI.UILabel lb_msg;
        private Sunny.UI.UIButton bt_refresh;
        private Sunny.UI.UILine L1;
        private Sunny.UI.UIFlowLayoutPanel pn_msg;
    }
}