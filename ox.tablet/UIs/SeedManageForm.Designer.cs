namespace OX.Tablet
{
    partial class SeedManageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeedManageForm));
            bt_close = new Sunny.UI.UIButton();
            tb_ip = new Sunny.UI.UIIPTextBox();
            lb_prikey = new Sunny.UI.UILabel();
            lb_port = new Sunny.UI.UILabel();
            tb_port = new Sunny.UI.UITextBox();
            bt_add = new Sunny.UI.UIButton();
            pn_pairs = new Sunny.UI.UIFlowLayoutPanel();
            SuspendLayout();
            // 
            // bt_close
            // 
            bt_close.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_close.Location = new System.Drawing.Point(663, 500);
            bt_close.MinimumSize = new System.Drawing.Size(1, 1);
            bt_close.Name = "bt_close";
            bt_close.Size = new System.Drawing.Size(196, 52);
            bt_close.TabIndex = 3;
            bt_close.Text = "uiButton1";
            bt_close.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_close.Click += bt_exit_Click;
            // 
            // tb_ip
            // 
            tb_ip.FillColor2 = System.Drawing.Color.FromArgb(235, 243, 255);
            tb_ip.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_ip.Location = new System.Drawing.Point(90, 27);
            tb_ip.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_ip.MinimumSize = new System.Drawing.Size(1, 1);
            tb_ip.Name = "tb_ip";
            tb_ip.Padding = new System.Windows.Forms.Padding(1);
            tb_ip.ShowText = false;
            tb_ip.Size = new System.Drawing.Size(225, 44);
            tb_ip.TabIndex = 4;
            tb_ip.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_prikey
            // 
            lb_prikey.AutoSize = true;
            lb_prikey.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_prikey.ForeColor = System.Drawing.Color.White;
            lb_prikey.Location = new System.Drawing.Point(12, 33);
            lb_prikey.Name = "lb_prikey";
            lb_prikey.Size = new System.Drawing.Size(42, 31);
            lb_prikey.TabIndex = 8;
            lb_prikey.Text = "IP:";
            // 
            // lb_port
            // 
            lb_port.AutoSize = true;
            lb_port.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_port.ForeColor = System.Drawing.Color.White;
            lb_port.Location = new System.Drawing.Point(355, 33);
            lb_port.Name = "lb_port";
            lb_port.Size = new System.Drawing.Size(42, 31);
            lb_port.TabIndex = 9;
            lb_port.Text = "IP:";
            // 
            // tb_port
            // 
            tb_port.DoubleValue = 11333D;
            tb_port.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_port.IntValue = 11333;
            tb_port.Location = new System.Drawing.Point(421, 27);
            tb_port.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_port.Maximum = 65535D;
            tb_port.Minimum = 1D;
            tb_port.MinimumSize = new System.Drawing.Size(1, 16);
            tb_port.Name = "tb_port";
            tb_port.Padding = new System.Windows.Forms.Padding(5);
            tb_port.ShowText = false;
            tb_port.Size = new System.Drawing.Size(168, 44);
            tb_port.TabIndex = 10;
            tb_port.Text = "11333";
            tb_port.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_port.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            tb_port.Watermark = "";
            // 
            // bt_add
            // 
            bt_add.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_add.Location = new System.Drawing.Point(663, 24);
            bt_add.MinimumSize = new System.Drawing.Size(1, 1);
            bt_add.Name = "bt_add";
            bt_add.Size = new System.Drawing.Size(196, 52);
            bt_add.TabIndex = 11;
            bt_add.Text = "uiButton1";
            bt_add.Click += bt_add_Click;
            // 
            // pn_pairs
            // 
            pn_pairs.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pn_pairs.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            pn_pairs.Location = new System.Drawing.Point(13, 104);
            pn_pairs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pn_pairs.MinimumSize = new System.Drawing.Size(1, 1);
            pn_pairs.Name = "pn_pairs";
            pn_pairs.Padding = new System.Windows.Forms.Padding(2, 10, 2, 2);
            pn_pairs.Radius = 1;
            pn_pairs.RectColor = System.Drawing.Color.Transparent;
            pn_pairs.RightToLeft = System.Windows.Forms.RightToLeft.No;
            pn_pairs.ScrollBarHandleWidth = 100;
            pn_pairs.ShowText = false;
            pn_pairs.Size = new System.Drawing.Size(873, 376);
            pn_pairs.TabIndex = 15;
            pn_pairs.Text = "uiFlowLayoutPanel1";
            pn_pairs.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SeedManageForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            AutoSize = true;
            BackColor = System.Drawing.Color.FromArgb(56, 56, 56);
            ClientSize = new System.Drawing.Size(899, 586);
            ControlBox = false;
            Controls.Add(pn_pairs);
            Controls.Add(bt_add);
            Controls.Add(tb_port);
            Controls.Add(lb_port);
            Controls.Add(lb_prikey);
            Controls.Add(tb_ip);
            Controls.Add(bt_close);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "SeedManageForm";
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "UnlockAccountForm";
            TopMost = true;
            FormClosing += UnlockAccountForm_FormClosing;
            Load += SeedManageForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Sunny.UI.UIButton bt_close;
        private Sunny.UI.UIIPTextBox tb_ip;
        private Sunny.UI.UILabel lb_prikey;
        private Sunny.UI.UILabel lb_port;
        private Sunny.UI.UITextBox tb_port;
        private Sunny.UI.UIButton bt_add;
        private Sunny.UI.UIFlowLayoutPanel pn_pairs;
    }
}