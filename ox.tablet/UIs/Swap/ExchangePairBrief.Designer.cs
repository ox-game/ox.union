namespace OX.Tablet
{
    partial class ExchangePairBrief
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
            lb_ido_lock_price = new Sunny.UI.UILabel();
            lb_ido_lock_index = new Sunny.UI.UILabel();
            bt_swap_go = new Sunny.UI.UIButton();
            lb_oxc_balance = new Sunny.UI.UILabel();
            lb_lock_index = new Sunny.UI.UILabel();
            lb_target_balance = new Sunny.UI.UILabel();
            lb_price = new Sunny.UI.UILabel();
            lb_stamp = new Sunny.UI.UILabel();
            uiGroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // uiGroupBox1
            // 
            uiGroupBox1.Controls.Add(lb_stamp);
            uiGroupBox1.Controls.Add(lb_ido_lock_price);
            uiGroupBox1.Controls.Add(lb_ido_lock_index);
            uiGroupBox1.Controls.Add(bt_swap_go);
            uiGroupBox1.Controls.Add(lb_oxc_balance);
            uiGroupBox1.Controls.Add(lb_lock_index);
            uiGroupBox1.Controls.Add(lb_target_balance);
            uiGroupBox1.Controls.Add(lb_price);
            uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            uiGroupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            uiGroupBox1.Name = "uiGroupBox1";
            uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            uiGroupBox1.RectSize = 2;
            uiGroupBox1.Size = new System.Drawing.Size(383, 403);
            uiGroupBox1.TabIndex = 0;
            uiGroupBox1.Text = "uiGroupBox1";
            uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            uiGroupBox1.Click += lb_master_balance_Click;
            // 
            // lb_ido_lock_price
            // 
            lb_ido_lock_price.AutoSize = true;
            lb_ido_lock_price.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_ido_lock_price.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_ido_lock_price.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_ido_lock_price.Location = new System.Drawing.Point(12, 243);
            lb_ido_lock_price.Name = "lb_ido_lock_price";
            lb_ido_lock_price.RightToLeft = System.Windows.Forms.RightToLeft.No;
            lb_ido_lock_price.Size = new System.Drawing.Size(91, 27);
            lb_ido_lock_price.TabIndex = 23;
            lb_ido_lock_price.Text = "uiLabel1";
            lb_ido_lock_price.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_ido_lock_index
            // 
            lb_ido_lock_index.AutoSize = true;
            lb_ido_lock_index.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_ido_lock_index.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_ido_lock_index.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_ido_lock_index.Location = new System.Drawing.Point(13, 206);
            lb_ido_lock_index.Name = "lb_ido_lock_index";
            lb_ido_lock_index.RightToLeft = System.Windows.Forms.RightToLeft.No;
            lb_ido_lock_index.Size = new System.Drawing.Size(91, 27);
            lb_ido_lock_index.TabIndex = 22;
            lb_ido_lock_index.Text = "uiLabel1";
            lb_ido_lock_index.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bt_swap_go
            // 
            bt_swap_go.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_swap_go.Location = new System.Drawing.Point(110, 331);
            bt_swap_go.MinimumSize = new System.Drawing.Size(1, 1);
            bt_swap_go.Name = "bt_swap_go";
            bt_swap_go.Size = new System.Drawing.Size(150, 52);
            bt_swap_go.TabIndex = 21;
            bt_swap_go.Text = "uiButton8";
            bt_swap_go.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_swap_go.Click += bt_bet_banker_Click;
            // 
            // lb_oxc_balance
            // 
            lb_oxc_balance.AutoSize = true;
            lb_oxc_balance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_oxc_balance.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_oxc_balance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_oxc_balance.Location = new System.Drawing.Point(13, 130);
            lb_oxc_balance.Name = "lb_oxc_balance";
            lb_oxc_balance.RightToLeft = System.Windows.Forms.RightToLeft.No;
            lb_oxc_balance.Size = new System.Drawing.Size(91, 27);
            lb_oxc_balance.TabIndex = 20;
            lb_oxc_balance.Text = "uiLabel1";
            lb_oxc_balance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_oxc_balance.Click += lb_master_balance_Click;
            // 
            // lb_lock_index
            // 
            lb_lock_index.AutoSize = true;
            lb_lock_index.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_lock_index.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_lock_index.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_lock_index.Location = new System.Drawing.Point(13, 168);
            lb_lock_index.Name = "lb_lock_index";
            lb_lock_index.RightToLeft = System.Windows.Forms.RightToLeft.No;
            lb_lock_index.Size = new System.Drawing.Size(91, 27);
            lb_lock_index.TabIndex = 10;
            lb_lock_index.Text = "uiLabel1";
            lb_lock_index.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_lock_index.Click += lb_master_balance_Click;
            // 
            // lb_target_balance
            // 
            lb_target_balance.AutoSize = true;
            lb_target_balance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_target_balance.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_target_balance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_target_balance.Location = new System.Drawing.Point(13, 92);
            lb_target_balance.Name = "lb_target_balance";
            lb_target_balance.RightToLeft = System.Windows.Forms.RightToLeft.No;
            lb_target_balance.Size = new System.Drawing.Size(91, 27);
            lb_target_balance.TabIndex = 9;
            lb_target_balance.Text = "uiLabel1";
            lb_target_balance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_target_balance.Click += lb_master_balance_Click;
            // 
            // lb_price
            // 
            lb_price.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_price.ForeColor = System.Drawing.Color.Red;
            lb_price.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_price.Location = new System.Drawing.Point(56, 44);
            lb_price.Name = "lb_price";
            lb_price.RightToLeft = System.Windows.Forms.RightToLeft.No;
            lb_price.Size = new System.Drawing.Size(271, 34);
            lb_price.TabIndex = 8;
            lb_price.Text = "uiLabel1";
            lb_price.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lb_price.Click += lb_master_balance_Click;
            // 
            // lb_stamp
            // 
            lb_stamp.AutoSize = true;
            lb_stamp.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_stamp.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_stamp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_stamp.Location = new System.Drawing.Point(12, 278);
            lb_stamp.Name = "lb_stamp";
            lb_stamp.RightToLeft = System.Windows.Forms.RightToLeft.No;
            lb_stamp.Size = new System.Drawing.Size(91, 27);
            lb_stamp.TabIndex = 24;
            lb_stamp.Text = "uiLabel1";
            lb_stamp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ExchangePairBrief
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(uiGroupBox1);
            Name = "ExchangePairBrief";
            Size = new System.Drawing.Size(383, 403);
            Click += lb_master_balance_Click;
            uiGroupBox1.ResumeLayout(false);
            uiGroupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UILabel lb_price;
        private Sunny.UI.UILabel lb_lock_index;
        private Sunny.UI.UILabel lb_target_balance;
        private Sunny.UI.UILabel lb_oxc_balance;
        private Sunny.UI.UIButton bt_swap_go;
        private Sunny.UI.UILabel lb_ido_lock_index;
        private Sunny.UI.UILabel lb_ido_lock_price;
        private Sunny.UI.UILabel lb_stamp;
    }
}
