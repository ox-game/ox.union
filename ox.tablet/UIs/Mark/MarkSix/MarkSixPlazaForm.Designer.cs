namespace OX.Tablet.UIs.MarkSix
{
    partial class MarkSixPlazaForm
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
            Box = new Sunny.UI.UISplitContainer();
            tbc_order = new Sunny.UI.UITabControl();
            MainPanel = new System.Windows.Forms.Panel();
            pn_betmanage = new System.Windows.Forms.Panel();
            bt_clear = new Sunny.UI.UIButton();
            bt_clear_page = new Sunny.UI.UIButton();
            bt_new_inbound = new Sunny.UI.UIButton();
            bt_newPoint = new Sunny.UI.UIButton();
            bt_today = new Sunny.UI.UIButton();
            bt_next_day = new Sunny.UI.UIButton();
            bt_pre_day = new Sunny.UI.UIButton();
            pn_methods = new System.Windows.Forms.FlowLayoutPanel();
            pn_bettings = new System.Windows.Forms.FlowLayoutPanel();
            L1 = new Sunny.UI.UILine();
            Box.BeginInit();
            Box.Panel1.SuspendLayout();
            Box.Panel2.SuspendLayout();
            Box.SuspendLayout();
            MainPanel.SuspendLayout();
            pn_betmanage.SuspendLayout();
            SuspendLayout();
            // 
            // Box
            // 
            Box.Dock = System.Windows.Forms.DockStyle.Fill;
            Box.Location = new System.Drawing.Point(0, 0);
            Box.MinimumSize = new System.Drawing.Size(20, 20);
            Box.Name = "Box";
            // 
            // Box.Panel1
            // 
            Box.Panel1.Controls.Add(tbc_order);
            Box.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // Box.Panel2
            // 
            Box.Panel2.Controls.Add(MainPanel);
            Box.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            Box.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            Box.Size = new System.Drawing.Size(1800, 1108);
            Box.SplitterDistance = 692;
            Box.SplitterWidth = 30;
            Box.TabIndex = 0;
            // 
            // tbc_order
            // 
            tbc_order.Dock = System.Windows.Forms.DockStyle.Fill;
            tbc_order.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            tbc_order.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tbc_order.ItemSize = new System.Drawing.Size(150, 40);
            tbc_order.Location = new System.Drawing.Point(0, 0);
            tbc_order.MainPage = "";
            tbc_order.MenuStyle = Sunny.UI.UIMenuStyle.White;
            tbc_order.Name = "tbc_order";
            tbc_order.SelectedIndex = 0;
            tbc_order.Size = new System.Drawing.Size(692, 1108);
            tbc_order.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            tbc_order.TabBackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            tbc_order.TabIndex = 0;
            tbc_order.TabSelectedColor = System.Drawing.Color.FromArgb(250, 250, 250);
            tbc_order.TabUnSelectedForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            tbc_order.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // MainPanel
            // 
            MainPanel.Controls.Add(pn_betmanage);
            MainPanel.Controls.Add(bt_today);
            MainPanel.Controls.Add(bt_next_day);
            MainPanel.Controls.Add(bt_pre_day);
            MainPanel.Controls.Add(pn_methods);
            MainPanel.Controls.Add(pn_bettings);
            MainPanel.Controls.Add(L1);
            MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            MainPanel.Location = new System.Drawing.Point(0, 0);
            MainPanel.Name = "MainPanel";
            MainPanel.Size = new System.Drawing.Size(1078, 1108);
            MainPanel.TabIndex = 7;
            MainPanel.SizeChanged += uiTabControl1_SizeChanged;
            // 
            // pn_betmanage
            // 
            pn_betmanage.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pn_betmanage.Controls.Add(bt_clear);
            pn_betmanage.Controls.Add(bt_clear_page);
            pn_betmanage.Controls.Add(bt_new_inbound);
            pn_betmanage.Controls.Add(bt_newPoint);
            pn_betmanage.Location = new System.Drawing.Point(13, 1010);
            pn_betmanage.Name = "pn_betmanage";
            pn_betmanage.Size = new System.Drawing.Size(1058, 95);
            pn_betmanage.TabIndex = 0;
            // 
            // bt_clear
            // 
            bt_clear.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            bt_clear.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_clear.Location = new System.Drawing.Point(206, 20);
            bt_clear.MinimumSize = new System.Drawing.Size(1, 1);
            bt_clear.Name = "bt_clear";
            bt_clear.Radius = 50;
            bt_clear.Size = new System.Drawing.Size(150, 52);
            bt_clear.TabIndex = 3;
            bt_clear.Text = "uiButton1";
            bt_clear.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_clear.Click += bt_clear_Click;
            // 
            // bt_clear_page
            // 
            bt_clear_page.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            bt_clear_page.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_clear_page.Location = new System.Drawing.Point(8, 20);
            bt_clear_page.MinimumSize = new System.Drawing.Size(1, 1);
            bt_clear_page.Name = "bt_clear_page";
            bt_clear_page.Radius = 50;
            bt_clear_page.Size = new System.Drawing.Size(150, 52);
            bt_clear_page.TabIndex = 4;
            bt_clear_page.Text = "uiButton1";
            bt_clear_page.Click += bt_clear_page_Click;
            // 
            // bt_new_inbound
            // 
            bt_new_inbound.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            bt_new_inbound.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_new_inbound.Location = new System.Drawing.Point(894, 20);
            bt_new_inbound.MinimumSize = new System.Drawing.Size(1, 1);
            bt_new_inbound.Name = "bt_new_inbound";
            bt_new_inbound.Radius = 50;
            bt_new_inbound.Size = new System.Drawing.Size(150, 52);
            bt_new_inbound.TabIndex = 5;
            bt_new_inbound.Text = "uiButton1";
            // 
            // bt_newPoint
            // 
            bt_newPoint.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            bt_newPoint.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_newPoint.Location = new System.Drawing.Point(700, 20);
            bt_newPoint.MinimumSize = new System.Drawing.Size(1, 1);
            bt_newPoint.Name = "bt_newPoint";
            bt_newPoint.Radius = 50;
            bt_newPoint.Size = new System.Drawing.Size(150, 52);
            bt_newPoint.TabIndex = 6;
            bt_newPoint.Text = "uiButton1";
            bt_newPoint.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_newPoint.Click += bt_newPoint_Click;
            // 
            // bt_today
            // 
            bt_today.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_today.Location = new System.Drawing.Point(225, 24);
            bt_today.MinimumSize = new System.Drawing.Size(1, 1);
            bt_today.Name = "bt_today";
            bt_today.Size = new System.Drawing.Size(230, 52);
            bt_today.TabIndex = 14;
            bt_today.Text = "uiButton1";
            bt_today.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_today.Click += bt_today_Click;
            // 
            // bt_next_day
            // 
            bt_next_day.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_next_day.Location = new System.Drawing.Point(496, 24);
            bt_next_day.MinimumSize = new System.Drawing.Size(1, 1);
            bt_next_day.Name = "bt_next_day";
            bt_next_day.Size = new System.Drawing.Size(171, 52);
            bt_next_day.TabIndex = 13;
            bt_next_day.Text = " ";
            bt_next_day.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_next_day.Click += bt_next_day_Click;
            // 
            // bt_pre_day
            // 
            bt_pre_day.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_pre_day.Location = new System.Drawing.Point(13, 24);
            bt_pre_day.MinimumSize = new System.Drawing.Size(1, 1);
            bt_pre_day.Name = "bt_pre_day";
            bt_pre_day.Size = new System.Drawing.Size(171, 52);
            bt_pre_day.TabIndex = 12;
            bt_pre_day.Text = "uiButton1";
            bt_pre_day.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_pre_day.Click += bt_pre_day_Click;
            // 
            // pn_methods
            // 
            pn_methods.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pn_methods.AutoSize = true;
            pn_methods.Location = new System.Drawing.Point(3, 95);
            pn_methods.Name = "pn_methods";
            pn_methods.RightToLeft = System.Windows.Forms.RightToLeft.No;
            pn_methods.Size = new System.Drawing.Size(1072, 130);
            pn_methods.TabIndex = 0;
            pn_methods.SizeChanged += pn_methods_SizeChanged;
            // 
            // pn_bettings
            // 
            pn_bettings.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pn_bettings.AutoScroll = true;
            pn_bettings.Location = new System.Drawing.Point(3, 271);
            pn_bettings.Name = "pn_bettings";
            pn_bettings.RightToLeft = System.Windows.Forms.RightToLeft.No;
            pn_bettings.Size = new System.Drawing.Size(1072, 724);
            pn_bettings.TabIndex = 1;
            // 
            // L1
            // 
            L1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            L1.BackColor = System.Drawing.Color.Transparent;
            L1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            L1.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            L1.Location = new System.Drawing.Point(3, 226);
            L1.MinimumSize = new System.Drawing.Size(1, 1);
            L1.Name = "L1";
            L1.Size = new System.Drawing.Size(1072, 44);
            L1.TabIndex = 2;
            L1.Text = "uiLine1";
            // 
            // MarkSixPlazaForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1800, 1108);
            Controls.Add(Box);
            Name = "MarkSixPlazaForm";
            Text = "PlazaForm";
            Box.Panel1.ResumeLayout(false);
            Box.Panel2.ResumeLayout(false);
            Box.EndInit();
            Box.ResumeLayout(false);
            MainPanel.ResumeLayout(false);
            MainPanel.PerformLayout();
            pn_betmanage.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UISplitContainer Box;
        private System.Windows.Forms.FlowLayoutPanel pn_methods;
        private System.Windows.Forms.FlowLayoutPanel pn_bettings;
        private Sunny.UI.UILine L1;
        private Sunny.UI.UIButton bt_clear;
        private Sunny.UI.UIButton bt_new_inbound;
        private Sunny.UI.UIButton bt_clear_page;
        private Sunny.UI.UIButton bt_newPoint;
        private System.Windows.Forms.Panel MainPanel;
        private Sunny.UI.UITabControl tbc_order;
        private Sunny.UI.UIButton bt_today;
        private Sunny.UI.UIButton bt_next_day;
        private Sunny.UI.UIButton bt_pre_day;
        private System.Windows.Forms.Panel pn_betmanage;
    }
}