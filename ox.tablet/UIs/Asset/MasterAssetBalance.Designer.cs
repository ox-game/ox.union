namespace OX.Tablet
{
    partial class MasterAssetBalance
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            uiGroupBox1 = new Sunny.UI.UIGroupBox();
            bt_claim = new Sunny.UI.UIButton();
            bt_self_lock = new Sunny.UI.UIButton();
            bt_reg_miner = new Sunny.UI.UIButton();
            lb_total_balance = new Sunny.UI.UILabel();
            bt_transfer = new Sunny.UI.UIButton();
            lb_master_balance = new Sunny.UI.UILabel();
            lb_available_balance = new Sunny.UI.UILabel();
            lb_assetName = new Sunny.UI.UILabel();
            uiGroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // uiGroupBox1
            // 
            uiGroupBox1.BackColor = System.Drawing.Color.Transparent;
            uiGroupBox1.Controls.Add(bt_claim);
            uiGroupBox1.Controls.Add(bt_self_lock);
            uiGroupBox1.Controls.Add(bt_reg_miner);
            uiGroupBox1.Controls.Add(lb_total_balance);
            uiGroupBox1.Controls.Add(bt_transfer);
            uiGroupBox1.Controls.Add(lb_master_balance);
            uiGroupBox1.Controls.Add(lb_available_balance);
            uiGroupBox1.Controls.Add(lb_assetName);
            uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            uiGroupBox1.FillColor = System.Drawing.Color.Transparent;
            uiGroupBox1.FillColor2 = System.Drawing.Color.Transparent;
            uiGroupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            uiGroupBox1.Name = "uiGroupBox1";
            uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            uiGroupBox1.RectSize = 2;
            uiGroupBox1.Size = new System.Drawing.Size(1195, 156);
            uiGroupBox1.TabIndex = 0;
            uiGroupBox1.Text = "uiGroupBox1";
            uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            uiGroupBox1.Click += lb_master_balance_Click;
            // 
            // bt_claim
            // 
            bt_claim.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            bt_claim.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_claim.Location = new System.Drawing.Point(647, 90);
            bt_claim.MinimumSize = new System.Drawing.Size(1, 1);
            bt_claim.Name = "bt_claim";
            bt_claim.Radius = 50;
            bt_claim.Size = new System.Drawing.Size(191, 52);
            bt_claim.TabIndex = 23;
            bt_claim.Text = "uiButton1";
            bt_claim.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_claim.Click += bt_claim_Click;
            // 
            // bt_self_lock
            // 
            bt_self_lock.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_self_lock.Location = new System.Drawing.Point(299, 90);
            bt_self_lock.MinimumSize = new System.Drawing.Size(1, 1);
            bt_self_lock.Name = "bt_self_lock";
            bt_self_lock.Radius = 50;
            bt_self_lock.Size = new System.Drawing.Size(191, 52);
            bt_self_lock.TabIndex = 22;
            bt_self_lock.Text = "uiButton1";
            bt_self_lock.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_self_lock.Click += bt_self_lock_Click;
            // 
            // bt_reg_miner
            // 
            bt_reg_miner.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_reg_miner.Location = new System.Drawing.Point(22, 90);
            bt_reg_miner.MinimumSize = new System.Drawing.Size(1, 1);
            bt_reg_miner.Name = "bt_reg_miner";
            bt_reg_miner.Radius = 50;
            bt_reg_miner.Size = new System.Drawing.Size(191, 52);
            bt_reg_miner.TabIndex = 21;
            bt_reg_miner.Text = "uiButton1";
            bt_reg_miner.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_reg_miner.Click += bt_reg_miner_Click;
            // 
            // lb_total_balance
            // 
            lb_total_balance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_total_balance.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_total_balance.Location = new System.Drawing.Point(517, 42);
            lb_total_balance.Name = "lb_total_balance";
            lb_total_balance.Size = new System.Drawing.Size(291, 34);
            lb_total_balance.TabIndex = 20;
            lb_total_balance.Text = "uiLabel1";
            lb_total_balance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_total_balance.Click += lb_master_balance_Click;
            // 
            // bt_transfer
            // 
            bt_transfer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            bt_transfer.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_transfer.Location = new System.Drawing.Point(956, 90);
            bt_transfer.MinimumSize = new System.Drawing.Size(1, 1);
            bt_transfer.Name = "bt_transfer";
            bt_transfer.Radius = 50;
            bt_transfer.Size = new System.Drawing.Size(191, 52);
            bt_transfer.TabIndex = 19;
            bt_transfer.Text = "uiButton1";
            bt_transfer.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_transfer.Click += bt_transfer_Click;
            // 
            // lb_master_balance
            // 
            lb_master_balance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_master_balance.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_master_balance.Location = new System.Drawing.Point(826, 42);
            lb_master_balance.Name = "lb_master_balance";
            lb_master_balance.Size = new System.Drawing.Size(312, 34);
            lb_master_balance.TabIndex = 10;
            lb_master_balance.Text = "uiLabel1";
            lb_master_balance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_master_balance.Click += lb_master_balance_Click;
            // 
            // lb_available_balance
            // 
            lb_available_balance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_available_balance.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_available_balance.Location = new System.Drawing.Point(195, 42);
            lb_available_balance.Name = "lb_available_balance";
            lb_available_balance.Size = new System.Drawing.Size(305, 34);
            lb_available_balance.TabIndex = 9;
            lb_available_balance.Text = "uiLabel1";
            lb_available_balance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_available_balance.Click += lb_master_balance_Click;
            // 
            // lb_assetName
            // 
            lb_assetName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_assetName.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_assetName.Location = new System.Drawing.Point(13, 42);
            lb_assetName.Name = "lb_assetName";
            lb_assetName.Size = new System.Drawing.Size(169, 34);
            lb_assetName.TabIndex = 8;
            lb_assetName.Text = "uiLabel1";
            lb_assetName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_assetName.Click += lb_master_balance_Click;
            // 
            // MasterAssetBalance
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Transparent;
            Controls.Add(uiGroupBox1);
            Name = "MasterAssetBalance";
            Size = new System.Drawing.Size(1195, 156);
            Click += lb_master_balance_Click;
            uiGroupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UILabel lb_assetName;
        private Sunny.UI.UILabel lb_master_balance;
        private Sunny.UI.UILabel lb_available_balance;
        private Sunny.UI.UIButton bt_transfer;
        private Sunny.UI.UILabel lb_total_balance;
        private Sunny.UI.UIButton bt_reg_miner;
        private Sunny.UI.UIButton bt_self_lock;
        private Sunny.UI.UIButton bt_claim;
    }
}
