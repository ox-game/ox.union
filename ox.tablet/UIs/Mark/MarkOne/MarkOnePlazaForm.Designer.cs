namespace OX.Tablet.UIs.MarkSix
{
    partial class MarkOnePlazaForm
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
            bt_remvoe_order = new Sunny.UI.UIButton();
            lb_player_term = new Sunny.UI.UILabel();
            tv_orders = new Sunny.UI.UITreeView();
            panel1 = new System.Windows.Forms.Panel();
            pn_methods = new System.Windows.Forms.FlowLayoutPanel();
            bt_post_order = new Sunny.UI.UIButton();
            bt_newPoint = new Sunny.UI.UIButton();
            pn_bettings = new System.Windows.Forms.FlowLayoutPanel();
            bt_newOrder = new Sunny.UI.UIButton();
            L1 = new Sunny.UI.UILine();
            bt_clear_page = new Sunny.UI.UIButton();
            bt_clear = new Sunny.UI.UIButton();
            Box.BeginInit();
            Box.Panel1.SuspendLayout();
            Box.Panel2.SuspendLayout();
            Box.SuspendLayout();
            panel1.SuspendLayout();
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
            Box.Panel1.Controls.Add(bt_remvoe_order);
            Box.Panel1.Controls.Add(lb_player_term);
            Box.Panel1.Controls.Add(tv_orders);
            Box.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // Box.Panel2
            // 
            Box.Panel2.Controls.Add(panel1);
            Box.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            Box.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            Box.Size = new System.Drawing.Size(1445, 1064);
            Box.SplitterDistance = 400;
            Box.SplitterWidth = 30;
            Box.TabIndex = 0;
            // 
            // bt_remvoe_order
            // 
            bt_remvoe_order.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            bt_remvoe_order.FillColor = System.Drawing.Color.Gray;
            bt_remvoe_order.FillColor2 = System.Drawing.Color.Silver;
            bt_remvoe_order.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_remvoe_order.Location = new System.Drawing.Point(314, 12);
            bt_remvoe_order.MinimumSize = new System.Drawing.Size(1, 1);
            bt_remvoe_order.Name = "bt_remvoe_order";
            bt_remvoe_order.RectColor = System.Drawing.Color.Gray;
            bt_remvoe_order.RectPressColor = System.Drawing.Color.Gray;
            bt_remvoe_order.RectSelectedColor = System.Drawing.Color.Gray;
            bt_remvoe_order.Size = new System.Drawing.Size(65, 52);
            bt_remvoe_order.TabIndex = 13;
            bt_remvoe_order.Text = "uiButton1";
            bt_remvoe_order.Click += bt_remvoe_order_Click;
            // 
            // lb_player_term
            // 
            lb_player_term.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_player_term.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_player_term.Location = new System.Drawing.Point(5, 23);
            lb_player_term.Name = "lb_player_term";
            lb_player_term.RightToLeft = System.Windows.Forms.RightToLeft.No;
            lb_player_term.Size = new System.Drawing.Size(303, 34);
            lb_player_term.TabIndex = 11;
            lb_player_term.Text = "uiLabel1";
            lb_player_term.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tv_orders
            // 
            tv_orders.AllowDrop = true;
            tv_orders.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tv_orders.CheckBoxes = true;
            tv_orders.FillColor = System.Drawing.Color.White;
            tv_orders.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tv_orders.Location = new System.Drawing.Point(4, 82);
            tv_orders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tv_orders.MinimumSize = new System.Drawing.Size(1, 1);
            tv_orders.Name = "tv_orders";
            tv_orders.ScrollBarStyleInherited = false;
            tv_orders.ShowLines = true;
            tv_orders.ShowText = false;
            tv_orders.Size = new System.Drawing.Size(392, 977);
            tv_orders.TabIndex = 0;
            tv_orders.Text = "uiTreeView1";
            tv_orders.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            tv_orders.BeforeCheck += tv_orders_BeforeCheck;
            tv_orders.AfterCheck += tv_orders_AfterCheck;
            // 
            // panel1
            // 
            panel1.Controls.Add(pn_methods);
            panel1.Controls.Add(bt_post_order);
            panel1.Controls.Add(bt_newPoint);
            panel1.Controls.Add(pn_bettings);
            panel1.Controls.Add(bt_newOrder);
            panel1.Controls.Add(L1);
            panel1.Controls.Add(bt_clear_page);
            panel1.Controls.Add(bt_clear);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1015, 1064);
            panel1.TabIndex = 7;
            // 
            // pn_methods
            // 
            pn_methods.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pn_methods.AutoSize = true;
            pn_methods.Location = new System.Drawing.Point(3, 5);
            pn_methods.Name = "pn_methods";
            pn_methods.RightToLeft = System.Windows.Forms.RightToLeft.No;
            pn_methods.Size = new System.Drawing.Size(1009, 130);
            pn_methods.TabIndex = 0;
            pn_methods.SizeChanged += pn_methods_SizeChanged;
            // 
            // bt_post_order
            // 
            bt_post_order.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            bt_post_order.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_post_order.Location = new System.Drawing.Point(851, 989);
            bt_post_order.MinimumSize = new System.Drawing.Size(1, 1);
            bt_post_order.Name = "bt_post_order";
            bt_post_order.Radius = 50;
            bt_post_order.Size = new System.Drawing.Size(150, 52);
            bt_post_order.TabIndex = 6;
            bt_post_order.Text = "uiButton1";
            bt_post_order.Click += bt_post_order_Click;
            // 
            // bt_newPoint
            // 
            bt_newPoint.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            bt_newPoint.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_newPoint.Location = new System.Drawing.Point(467, 989);
            bt_newPoint.MinimumSize = new System.Drawing.Size(1, 1);
            bt_newPoint.Name = "bt_newPoint";
            bt_newPoint.Radius = 50;
            bt_newPoint.Size = new System.Drawing.Size(150, 52);
            bt_newPoint.TabIndex = 6;
            bt_newPoint.Text = "uiButton1";
            bt_newPoint.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_newPoint.Click += bt_newPoint_Click;
            // 
            // pn_bettings
            // 
            pn_bettings.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pn_bettings.AutoScroll = true;
            pn_bettings.Location = new System.Drawing.Point(3, 158);
            pn_bettings.Name = "pn_bettings";
            pn_bettings.RightToLeft = System.Windows.Forms.RightToLeft.No;
            pn_bettings.Size = new System.Drawing.Size(1009, 793);
            pn_bettings.TabIndex = 1;
            // 
            // bt_newOrder
            // 
            bt_newOrder.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            bt_newOrder.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_newOrder.Location = new System.Drawing.Point(661, 989);
            bt_newOrder.MinimumSize = new System.Drawing.Size(1, 1);
            bt_newOrder.Name = "bt_newOrder";
            bt_newOrder.Radius = 50;
            bt_newOrder.Size = new System.Drawing.Size(150, 52);
            bt_newOrder.TabIndex = 5;
            bt_newOrder.Text = "uiButton1";
            // 
            // L1
            // 
            L1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            L1.BackColor = System.Drawing.Color.Transparent;
            L1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            L1.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            L1.Location = new System.Drawing.Point(12, 122);
            L1.MinimumSize = new System.Drawing.Size(1, 1);
            L1.Name = "L1";
            L1.Size = new System.Drawing.Size(960, 44);
            L1.TabIndex = 2;
            L1.Text = "uiLine1";
            // 
            // bt_clear_page
            // 
            bt_clear_page.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            bt_clear_page.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_clear_page.Location = new System.Drawing.Point(14, 989);
            bt_clear_page.MinimumSize = new System.Drawing.Size(1, 1);
            bt_clear_page.Name = "bt_clear_page";
            bt_clear_page.Radius = 50;
            bt_clear_page.Size = new System.Drawing.Size(150, 52);
            bt_clear_page.TabIndex = 4;
            bt_clear_page.Text = "uiButton1";
            bt_clear_page.Click += bt_clear_page_Click;
            // 
            // bt_clear
            // 
            bt_clear.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            bt_clear.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_clear.Location = new System.Drawing.Point(212, 989);
            bt_clear.MinimumSize = new System.Drawing.Size(1, 1);
            bt_clear.Name = "bt_clear";
            bt_clear.Radius = 50;
            bt_clear.Size = new System.Drawing.Size(150, 52);
            bt_clear.TabIndex = 3;
            bt_clear.Text = "uiButton1";
            bt_clear.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_clear.Click += bt_clear_Click;
            // 
            // PlazaForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1445, 1064);
            Controls.Add(Box);
            Name = "PlazaForm";
            Text = "PlazaForm";
            Box.Panel1.ResumeLayout(false);
            Box.Panel2.ResumeLayout(false);
            Box.EndInit();
            Box.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UISplitContainer Box;
        private Sunny.UI.UITreeView tv_orders;
        private System.Windows.Forms.FlowLayoutPanel pn_methods;
        private System.Windows.Forms.FlowLayoutPanel pn_bettings;
        private Sunny.UI.UILine L1;
        private Sunny.UI.UIButton bt_clear;
        private Sunny.UI.UIButton bt_newOrder;
        private Sunny.UI.UIButton bt_clear_page;
        private Sunny.UI.UIButton bt_newPoint;
        private Sunny.UI.UIButton bt_post_order;
        private Sunny.UI.UILabel lb_player_term;
        private Sunny.UI.UIButton bt_remvoe_order;
        private System.Windows.Forms.Panel panel1;
    }
}