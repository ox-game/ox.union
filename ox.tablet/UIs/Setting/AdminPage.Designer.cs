namespace OX.Tablet.UIs.MarkSix
{
    partial class AdminPage
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
            bt_refresh = new Sunny.UI.UIButton();
            L1 = new Sunny.UI.UILine();
            tb_catalog = new Sunny.UI.UITextBox();
            lb_catalog = new Sunny.UI.UILabel();
            uiLabel1 = new Sunny.UI.UILabel();
            uiLabel2 = new Sunny.UI.UILabel();
            tb_port = new Sunny.UI.UITextBox();
            L2 = new Sunny.UI.UILine();
            bt_push_message = new Sunny.UI.UIButton();
            tb_cn = new Sunny.UI.UIRichTextBox();
            uiLabel3 = new Sunny.UI.UILabel();
            uiLabel4 = new Sunny.UI.UILabel();
            tb_en = new Sunny.UI.UIRichTextBox();
            tb_IPAddress = new Sunny.UI.UIIPTextBox();
            lb_nodes = new Sunny.UI.UIListBox();
            bt_publish = new Sunny.UI.UIButton();
            SuspendLayout();
            // 
            // bt_refresh
            // 
            bt_refresh.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_refresh.Location = new System.Drawing.Point(32, 330);
            bt_refresh.MinimumSize = new System.Drawing.Size(1, 1);
            bt_refresh.Name = "bt_refresh";
            bt_refresh.Size = new System.Drawing.Size(196, 52);
            bt_refresh.TabIndex = 16;
            bt_refresh.Text = "uiButton1";
            bt_refresh.Click += bt_refresh_Click;
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
            L1.Size = new System.Drawing.Size(1370, 26);
            L1.TabIndex = 17;
            L1.Text = "uiLine1";
            // 
            // tb_catalog
            // 
            tb_catalog.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_catalog.Location = new System.Drawing.Point(156, 76);
            tb_catalog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_catalog.MinimumSize = new System.Drawing.Size(1, 16);
            tb_catalog.Name = "tb_catalog";
            tb_catalog.Padding = new System.Windows.Forms.Padding(5);
            tb_catalog.ShowText = false;
            tb_catalog.Size = new System.Drawing.Size(125, 44);
            tb_catalog.TabIndex = 18;
            tb_catalog.Text = "0";
            tb_catalog.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_catalog.Watermark = "";
            // 
            // lb_catalog
            // 
            lb_catalog.AutoSize = true;
            lb_catalog.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_catalog.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_catalog.Location = new System.Drawing.Point(32, 87);
            lb_catalog.Name = "lb_catalog";
            lb_catalog.Size = new System.Drawing.Size(106, 24);
            lb_catalog.TabIndex = 19;
            lb_catalog.Text = "Catalog:";
            // 
            // uiLabel1
            // 
            uiLabel1.AutoSize = true;
            uiLabel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiLabel1.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            uiLabel1.Location = new System.Drawing.Point(26, 170);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new System.Drawing.Size(106, 24);
            uiLabel1.TabIndex = 21;
            uiLabel1.Text = "Address:";
            // 
            // uiLabel2
            // 
            uiLabel2.AutoSize = true;
            uiLabel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiLabel2.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            uiLabel2.Location = new System.Drawing.Point(32, 253);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new System.Drawing.Size(70, 24);
            uiLabel2.TabIndex = 23;
            uiLabel2.Text = "Port:";
            // 
            // tb_port
            // 
            tb_port.DoubleValue = 11332D;
            tb_port.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_port.IntValue = 11332;
            tb_port.Location = new System.Drawing.Point(156, 242);
            tb_port.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_port.MinimumSize = new System.Drawing.Size(1, 16);
            tb_port.Name = "tb_port";
            tb_port.Padding = new System.Windows.Forms.Padding(5);
            tb_port.ShowText = false;
            tb_port.Size = new System.Drawing.Size(125, 44);
            tb_port.TabIndex = 22;
            tb_port.Text = "11332";
            tb_port.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_port.Watermark = "";
            // 
            // L2
            // 
            L2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            L2.BackColor = System.Drawing.Color.Transparent;
            L2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            L2.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            L2.Location = new System.Drawing.Point(17, 409);
            L2.MinimumSize = new System.Drawing.Size(1, 1);
            L2.Name = "L2";
            L2.Size = new System.Drawing.Size(1370, 26);
            L2.TabIndex = 24;
            L2.Text = "uiLine1";
            // 
            // bt_push_message
            // 
            bt_push_message.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_push_message.Location = new System.Drawing.Point(1177, 560);
            bt_push_message.MinimumSize = new System.Drawing.Size(1, 1);
            bt_push_message.Name = "bt_push_message";
            bt_push_message.Size = new System.Drawing.Size(196, 52);
            bt_push_message.TabIndex = 25;
            bt_push_message.Text = "uiButton1";
            bt_push_message.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_push_message.Click += bt_push_message_Click;
            // 
            // tb_cn
            // 
            tb_cn.FillColor = System.Drawing.Color.White;
            tb_cn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_cn.Location = new System.Drawing.Point(32, 497);
            tb_cn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_cn.MinimumSize = new System.Drawing.Size(1, 1);
            tb_cn.Name = "tb_cn";
            tb_cn.Padding = new System.Windows.Forms.Padding(2);
            tb_cn.ShowText = false;
            tb_cn.Size = new System.Drawing.Size(538, 270);
            tb_cn.TabIndex = 26;
            tb_cn.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiLabel3
            // 
            uiLabel3.AutoSize = true;
            uiLabel3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiLabel3.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            uiLabel3.Location = new System.Drawing.Point(32, 454);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new System.Drawing.Size(106, 24);
            uiLabel3.TabIndex = 27;
            uiLabel3.Text = "Chinese:";
            // 
            // uiLabel4
            // 
            uiLabel4.AutoSize = true;
            uiLabel4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiLabel4.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            uiLabel4.Location = new System.Drawing.Point(617, 454);
            uiLabel4.Name = "uiLabel4";
            uiLabel4.Size = new System.Drawing.Size(106, 24);
            uiLabel4.TabIndex = 29;
            uiLabel4.Text = "English:";
            // 
            // tb_en
            // 
            tb_en.FillColor = System.Drawing.Color.White;
            tb_en.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_en.Location = new System.Drawing.Point(617, 497);
            tb_en.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_en.MinimumSize = new System.Drawing.Size(1, 1);
            tb_en.Name = "tb_en";
            tb_en.Padding = new System.Windows.Forms.Padding(2);
            tb_en.ShowText = false;
            tb_en.Size = new System.Drawing.Size(538, 270);
            tb_en.TabIndex = 28;
            tb_en.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_IPAddress
            // 
            tb_IPAddress.FillColor2 = System.Drawing.Color.FromArgb(235, 243, 255);
            tb_IPAddress.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_IPAddress.Location = new System.Drawing.Point(156, 159);
            tb_IPAddress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_IPAddress.MinimumSize = new System.Drawing.Size(1, 1);
            tb_IPAddress.Name = "tb_IPAddress";
            tb_IPAddress.Padding = new System.Windows.Forms.Padding(1);
            tb_IPAddress.ShowText = false;
            tb_IPAddress.Size = new System.Drawing.Size(259, 44);
            tb_IPAddress.TabIndex = 30;
            tb_IPAddress.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_nodes
            // 
            lb_nodes.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_nodes.HoverColor = System.Drawing.Color.FromArgb(155, 200, 255);
            lb_nodes.ItemSelectForeColor = System.Drawing.Color.White;
            lb_nodes.Location = new System.Drawing.Point(541, 57);
            lb_nodes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            lb_nodes.MinimumSize = new System.Drawing.Size(1, 1);
            lb_nodes.Name = "lb_nodes";
            lb_nodes.Padding = new System.Windows.Forms.Padding(2);
            lb_nodes.ShowText = false;
            lb_nodes.Size = new System.Drawing.Size(492, 325);
            lb_nodes.TabIndex = 31;
            lb_nodes.Text = "uiListBox1";
            // 
            // bt_publish
            // 
            bt_publish.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_publish.Location = new System.Drawing.Point(1081, 59);
            bt_publish.MinimumSize = new System.Drawing.Size(1, 1);
            bt_publish.Name = "bt_publish";
            bt_publish.Size = new System.Drawing.Size(196, 52);
            bt_publish.TabIndex = 32;
            bt_publish.Text = "uiButton1";
            bt_publish.Click += bt_publish_Click;
            // 
            // AdminPage
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1445, 845);
            Controls.Add(bt_publish);
            Controls.Add(lb_nodes);
            Controls.Add(tb_IPAddress);
            Controls.Add(uiLabel4);
            Controls.Add(tb_en);
            Controls.Add(uiLabel3);
            Controls.Add(tb_cn);
            Controls.Add(bt_push_message);
            Controls.Add(L2);
            Controls.Add(uiLabel2);
            Controls.Add(tb_port);
            Controls.Add(uiLabel1);
            Controls.Add(lb_catalog);
            Controls.Add(tb_catalog);
            Controls.Add(bt_refresh);
            Controls.Add(L1);
            Name = "AdminPage";
            Text = "PlazaForm";
            Initialize += OrderHistory_Initialize;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Sunny.UI.UIButton bt_publish;
        private Sunny.UI.UIButton uiButton2;
        private Sunny.UI.UIButton uiButton3;
        private Sunny.UI.UIButton uiButton4;
        private Sunny.UI.UIButton uiButton5;
        private Sunny.UI.UIButton uiButton6;
        private Sunny.UI.UIButton bt_refresh;
        private Sunny.UI.UILine L1;
        private Sunny.UI.UITextBox tb_catalog;
        private Sunny.UI.UILabel lb_catalog;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UITextBox tb_port;
        private Sunny.UI.UILine L2;
        private Sunny.UI.UIButton bt_push_message;
        private Sunny.UI.UIRichTextBox tb_cn;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UIRichTextBox tb_en;
        private Sunny.UI.UIIPTextBox tb_IPAddress;
        private Sunny.UI.UIListBox lb_nodes;
    }
}