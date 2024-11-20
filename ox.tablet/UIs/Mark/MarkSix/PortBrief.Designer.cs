namespace OX.Tablet
{
    partial class PortBrief
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
            lb_count_bet = new Sunny.UI.UILabel();
            lb_total_bet = new Sunny.UI.UILabel();
            lb_bond_balance = new Sunny.UI.UILabel();
            uiGroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // uiGroupBox1
            // 
            uiGroupBox1.Controls.Add(lb_count_bet);
            uiGroupBox1.Controls.Add(lb_total_bet);
            uiGroupBox1.Controls.Add(lb_bond_balance);
            uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            uiGroupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            uiGroupBox1.Name = "uiGroupBox1";
            uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            uiGroupBox1.RectSize = 2;
            uiGroupBox1.Size = new System.Drawing.Size(192, 151);
            uiGroupBox1.TabIndex = 0;
            uiGroupBox1.Text = "uiGroupBox1";
            uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            uiGroupBox1.Click += lb_master_balance_Click;
            // 
            // lb_count_bet
            // 
            lb_count_bet.AutoSize = true;
            lb_count_bet.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_count_bet.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_count_bet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_count_bet.Location = new System.Drawing.Point(9, 110);
            lb_count_bet.Name = "lb_count_bet";
            lb_count_bet.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            lb_count_bet.Size = new System.Drawing.Size(83, 24);
            lb_count_bet.TabIndex = 20;
            lb_count_bet.Text = "uiLabel1";
            lb_count_bet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            lb_count_bet.Click += lb_master_balance_Click;
            lb_count_bet.DoubleClick += BankerInfo_DoubleClick;
            // 
            // lb_total_bet
            // 
            lb_total_bet.AutoSize = true;
            lb_total_bet.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_total_bet.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_total_bet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_total_bet.Location = new System.Drawing.Point(9, 76);
            lb_total_bet.Name = "lb_total_bet";
            lb_total_bet.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            lb_total_bet.Size = new System.Drawing.Size(83, 24);
            lb_total_bet.TabIndex = 9;
            lb_total_bet.Text = "uiLabel1";
            lb_total_bet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            lb_total_bet.Click += lb_master_balance_Click;
            lb_total_bet.DoubleClick += BankerInfo_DoubleClick;
            // 
            // lb_bond_balance
            // 
            lb_bond_balance.AutoSize = true;
            lb_bond_balance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_bond_balance.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_bond_balance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_bond_balance.Location = new System.Drawing.Point(9, 42);
            lb_bond_balance.Name = "lb_bond_balance";
            lb_bond_balance.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            lb_bond_balance.Size = new System.Drawing.Size(83, 24);
            lb_bond_balance.TabIndex = 8;
            lb_bond_balance.Text = "uiLabel1";
            lb_bond_balance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            lb_bond_balance.Click += lb_master_balance_Click;
            lb_bond_balance.DoubleClick += BankerInfo_DoubleClick;
            // 
            // PortBrief
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(uiGroupBox1);
            Name = "PortBrief";
            Size = new System.Drawing.Size(192, 151);
            Click += lb_master_balance_Click;
            uiGroupBox1.ResumeLayout(false);
            uiGroupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UILabel lb_bond_balance;
        private Sunny.UI.UILabel lb_total_bet;
        private Sunny.UI.UILabel lb_count_bet;
    }
}
