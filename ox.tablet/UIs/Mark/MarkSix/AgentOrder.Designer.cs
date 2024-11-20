namespace OX.Tablet
{
    partial class AgentOrder
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
            gb_memberId = new Sunny.UI.UIGroupBox();
            lb_fee = new Sunny.UI.UILabel();
            lb_prize_amount = new Sunny.UI.UILabel();
            lb_bet_amount = new Sunny.UI.UILabel();
            gb_memberId.SuspendLayout();
            SuspendLayout();
            // 
            // gb_memberId
            // 
            gb_memberId.Controls.Add(lb_fee);
            gb_memberId.Controls.Add(lb_prize_amount);
            gb_memberId.Controls.Add(lb_bet_amount);
            gb_memberId.Dock = System.Windows.Forms.DockStyle.Fill;
            gb_memberId.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            gb_memberId.Location = new System.Drawing.Point(0, 0);
            gb_memberId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gb_memberId.MinimumSize = new System.Drawing.Size(1, 1);
            gb_memberId.Name = "gb_memberId";
            gb_memberId.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            gb_memberId.RectSize = 2;
            gb_memberId.Size = new System.Drawing.Size(265, 155);
            gb_memberId.TabIndex = 0;
            gb_memberId.Text = "uiGroupBox1";
            gb_memberId.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gb_memberId.Click += lb_master_balance_Click;
            // 
            // lb_fee
            // 
            lb_fee.AutoSize = true;
            lb_fee.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_fee.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_fee.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_fee.Location = new System.Drawing.Point(9, 112);
            lb_fee.Name = "lb_fee";
            lb_fee.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            lb_fee.Size = new System.Drawing.Size(83, 24);
            lb_fee.TabIndex = 19;
            lb_fee.Text = "uiLabel1";
            lb_fee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_prize_amount
            // 
            lb_prize_amount.AutoSize = true;
            lb_prize_amount.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_prize_amount.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_prize_amount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_prize_amount.Location = new System.Drawing.Point(9, 76);
            lb_prize_amount.Name = "lb_prize_amount";
            lb_prize_amount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            lb_prize_amount.Size = new System.Drawing.Size(83, 24);
            lb_prize_amount.TabIndex = 9;
            lb_prize_amount.Text = "uiLabel1";
            lb_prize_amount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            lb_prize_amount.Click += lb_master_balance_Click;
            lb_prize_amount.DoubleClick += BankerInfo_DoubleClick;
            // 
            // lb_bet_amount
            // 
            lb_bet_amount.AutoSize = true;
            lb_bet_amount.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_bet_amount.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_bet_amount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_bet_amount.Location = new System.Drawing.Point(9, 42);
            lb_bet_amount.Name = "lb_bet_amount";
            lb_bet_amount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            lb_bet_amount.Size = new System.Drawing.Size(83, 24);
            lb_bet_amount.TabIndex = 8;
            lb_bet_amount.Text = "uiLabel1";
            lb_bet_amount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            lb_bet_amount.Click += lb_master_balance_Click;
            lb_bet_amount.DoubleClick += BankerInfo_DoubleClick;
            // 
            // AgentOrder
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gb_memberId);
            Name = "AgentOrder";
            Size = new System.Drawing.Size(265, 155);
            Click += lb_master_balance_Click;
            gb_memberId.ResumeLayout(false);
            gb_memberId.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIGroupBox gb_memberId;
        private Sunny.UI.UILabel lb_bet_amount;
        private Sunny.UI.UILabel lb_prize_amount;
        private Sunny.UI.UILabel lb_fee;
    }
}
