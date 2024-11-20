namespace OX.Tablet
{
    partial class MasterAssetsSubPage
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
            lb_assetDetails = new Sunny.UI.UIListBox();
            panel1 = new System.Windows.Forms.Panel();
            pn_master_assets = new System.Windows.Forms.FlowLayoutPanel();
            bt_asset_record = new Sunny.UI.UIButton();
            bt_refresh_balance = new Sunny.UI.UIButton();
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
            Box.Panel1.Controls.Add(lb_assetDetails);
            Box.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // Box.Panel2
            // 
            Box.Panel2.BackColor = System.Drawing.Color.White;
            Box.Panel2.Controls.Add(panel1);
            Box.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            Box.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            Box.Size = new System.Drawing.Size(1647, 985);
            Box.SplitterDistance = 494;
            Box.SplitterWidth = 30;
            Box.TabIndex = 0;
            // 
            // lb_assetDetails
            // 
            lb_assetDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            lb_assetDetails.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_assetDetails.HoverColor = System.Drawing.Color.FromArgb(155, 200, 255);
            lb_assetDetails.ItemSelectForeColor = System.Drawing.Color.White;
            lb_assetDetails.Location = new System.Drawing.Point(0, 0);
            lb_assetDetails.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            lb_assetDetails.MinimumSize = new System.Drawing.Size(1, 1);
            lb_assetDetails.Name = "lb_assetDetails";
            lb_assetDetails.Padding = new System.Windows.Forms.Padding(2);
            lb_assetDetails.ShowText = false;
            lb_assetDetails.Size = new System.Drawing.Size(494, 985);
            lb_assetDetails.TabIndex = 0;
            lb_assetDetails.Text = "uiListBox1";
            // 
            // panel1
            // 
            panel1.Controls.Add(pn_master_assets);
            panel1.Controls.Add(bt_asset_record);
            panel1.Controls.Add(bt_refresh_balance);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1123, 985);
            panel1.TabIndex = 7;
            // 
            // pn_master_assets
            // 
            pn_master_assets.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pn_master_assets.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            pn_master_assets.Location = new System.Drawing.Point(4, 14);
            pn_master_assets.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pn_master_assets.MinimumSize = new System.Drawing.Size(1, 1);
            pn_master_assets.Name = "pn_master_assets";
            pn_master_assets.Padding = new System.Windows.Forms.Padding(2, 10, 2, 2);
            pn_master_assets.RightToLeft = System.Windows.Forms.RightToLeft.No;
            pn_master_assets.Size = new System.Drawing.Size(1115, 881);
            pn_master_assets.TabIndex = 24;
            pn_master_assets.Text = "uiFlowLayoutPanel1";
            pn_master_assets.SizeChanged += pn_assets_SizeChanged;
            // 
            // bt_asset_record
            // 
            bt_asset_record.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            bt_asset_record.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_asset_record.Location = new System.Drawing.Point(834, 913);
            bt_asset_record.MinimumSize = new System.Drawing.Size(1, 1);
            bt_asset_record.Name = "bt_asset_record";
            bt_asset_record.Size = new System.Drawing.Size(270, 52);
            bt_asset_record.TabIndex = 5;
            bt_asset_record.Text = "uiButton1";
            bt_asset_record.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_asset_record.Click += bt_asset_record_Click;
            // 
            // bt_refresh_balance
            // 
            bt_refresh_balance.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            bt_refresh_balance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_refresh_balance.Location = new System.Drawing.Point(14, 913);
            bt_refresh_balance.MinimumSize = new System.Drawing.Size(1, 1);
            bt_refresh_balance.Name = "bt_refresh_balance";
            bt_refresh_balance.Size = new System.Drawing.Size(270, 52);
            bt_refresh_balance.TabIndex = 4;
            bt_refresh_balance.Text = "uiButton1";
            bt_refresh_balance.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_refresh_balance.Click += bt_refresh_balance_Click;
            // 
            // MasterAssetsSubPage
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1647, 985);
            Controls.Add(Box);
            Name = "MasterAssetsSubPage";
            Box.Panel1.ResumeLayout(false);
            Box.Panel2.ResumeLayout(false);
            Box.EndInit();
            Box.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UISplitContainer Box;
        private Sunny.UI.UIListBox lb_assetDetails;
        private Sunny.UI.UIButton bt_refresh_balance;
        private Sunny.UI.UIButton bt_asset_record;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel pn_master_assets;
    }
}