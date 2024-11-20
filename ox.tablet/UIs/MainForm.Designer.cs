namespace OX.Tablet
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            uiPanel1 = new Sunny.UI.UIPanel();
            lb_msg = new Sunny.UI.UIMarkLabel();
            lb_accountIndex = new Sunny.UI.UIMarkLabel();
            lb_blockchain_nodes = new Sunny.UI.UIMarkLabel();
            lb_blockchain_state = new Sunny.UI.UIMarkLabel();
            uiPanel2 = new Sunny.UI.UIPanel();
            Box = new Sunny.UI.UISplitContainer();
            MainMenu = new Sunny.UI.UINavMenu();
            ContentPanel = new System.Windows.Forms.Panel();
            timer1 = new System.Windows.Forms.Timer(components);
            notifyIcon1 = new System.Windows.Forms.NotifyIcon(components);
            uiPanel1.SuspendLayout();
            uiPanel2.SuspendLayout();
            Box.BeginInit();
            Box.Panel1.SuspendLayout();
            Box.Panel2.SuspendLayout();
            Box.SuspendLayout();
            SuspendLayout();
            // 
            // uiPanel1
            // 
            uiPanel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            uiPanel1.Controls.Add(lb_msg);
            uiPanel1.Controls.Add(lb_accountIndex);
            uiPanel1.Controls.Add(lb_blockchain_nodes);
            uiPanel1.Controls.Add(lb_blockchain_state);
            uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiPanel1.Location = new System.Drawing.Point(0, 905);
            uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.RectColor = System.Drawing.Color.Transparent;
            uiPanel1.Size = new System.Drawing.Size(1511, 40);
            uiPanel1.TabIndex = 0;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_msg
            // 
            lb_msg.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lb_msg.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_msg.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_msg.Location = new System.Drawing.Point(786, 6);
            lb_msg.Name = "lb_msg";
            lb_msg.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            lb_msg.RightToLeft = System.Windows.Forms.RightToLeft.No;
            lb_msg.Size = new System.Drawing.Size(722, 34);
            lb_msg.TabIndex = 14;
            lb_msg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_accountIndex
            // 
            lb_accountIndex.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_accountIndex.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_accountIndex.Location = new System.Drawing.Point(480, 6);
            lb_accountIndex.Name = "lb_accountIndex";
            lb_accountIndex.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            lb_accountIndex.Size = new System.Drawing.Size(251, 34);
            lb_accountIndex.TabIndex = 2;
            // 
            // lb_blockchain_nodes
            // 
            lb_blockchain_nodes.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_blockchain_nodes.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_blockchain_nodes.Location = new System.Drawing.Point(10, 6);
            lb_blockchain_nodes.Name = "lb_blockchain_nodes";
            lb_blockchain_nodes.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            lb_blockchain_nodes.Size = new System.Drawing.Size(163, 34);
            lb_blockchain_nodes.TabIndex = 1;
            // 
            // lb_blockchain_state
            // 
            lb_blockchain_state.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_blockchain_state.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_blockchain_state.Location = new System.Drawing.Point(206, 6);
            lb_blockchain_state.Name = "lb_blockchain_state";
            lb_blockchain_state.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            lb_blockchain_state.Size = new System.Drawing.Size(251, 34);
            lb_blockchain_state.TabIndex = 0;
            // 
            // uiPanel2
            // 
            uiPanel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            uiPanel2.Controls.Add(Box);
            uiPanel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiPanel2.Location = new System.Drawing.Point(0, 36);
            uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            uiPanel2.Name = "uiPanel2";
            uiPanel2.Size = new System.Drawing.Size(1511, 869);
            uiPanel2.TabIndex = 1;
            uiPanel2.Text = null;
            uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
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
            Box.Panel1.Controls.Add(MainMenu);
            // 
            // Box.Panel2
            // 
            Box.Panel2.Controls.Add(ContentPanel);
            Box.Size = new System.Drawing.Size(1511, 869);
            Box.SplitterDistance = 189;
            Box.SplitterWidth = 30;
            Box.TabIndex = 0;
            // 
            // MainMenu
            // 
            MainMenu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            MainMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            MainMenu.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            MainMenu.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            MainMenu.FullRowSelect = true;
            MainMenu.HotTracking = true;
            MainMenu.HoverColor = System.Drawing.Color.FromArgb(80, 160, 255);
            MainMenu.ItemHeight = 100;
            MainMenu.Location = new System.Drawing.Point(0, 0);
            MainMenu.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            MainMenu.Name = "MainMenu";
            MainMenu.ShowLines = false;
            MainMenu.ShowPlusMinus = false;
            MainMenu.ShowRootLines = false;
            MainMenu.Size = new System.Drawing.Size(189, 869);
            MainMenu.TabIndex = 0;
            MainMenu.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            MainMenu.MenuItemClick += MainMenu_MenuItemClick;
            // 
            // ContentPanel
            // 
            ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            ContentPanel.Location = new System.Drawing.Point(0, 0);
            ContentPanel.Name = "ContentPanel";
            ContentPanel.Size = new System.Drawing.Size(1292, 869);
            ContentPanel.TabIndex = 0;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (System.Drawing.Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            notifyIcon1.Click += notifyIcon1_Click;
            // 
            // MainForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1515, 947);
            ControlBox = false;
            Controls.Add(uiPanel2);
            Controls.Add(uiPanel1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new System.Drawing.Size(2160, 1380);
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "MainForm";
            Text = "MainForm";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 800, 450);
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            uiPanel1.ResumeLayout(false);
            uiPanel2.ResumeLayout(false);
            Box.Panel1.ResumeLayout(false);
            Box.Panel2.ResumeLayout(false);
            Box.EndInit();
            Box.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UIPanel uiPanel2;
        private Sunny.UI.UISplitContainer Box;
        private Sunny.UI.UINavMenu MainMenu;
        private Sunny.UI.UIMarkLabel lb_blockchain_state;
        private Sunny.UI.UIMarkLabel lb_blockchain_nodes;
        private System.Windows.Forms.Panel ContentPanel;
        private Sunny.UI.UIMarkLabel lb_accountIndex;
        private System.Windows.Forms.Timer timer1;
        public Sunny.UI.UIMarkLabel lb_msg;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}