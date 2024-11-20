namespace OX.Tablet
{
    partial class DirectSalePublishInfo
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
            bt_create_order = new Sunny.UI.UIButton();
            lb_timestamp = new Sunny.UI.UILabel();
            lb_contact = new Sunny.UI.UILabel();
            bt_copy_contact = new Sunny.UI.UIButton();
            bt_show_balance = new Sunny.UI.UIButton();
            lb_remarks = new Sunny.UI.UILabel();
            lb_seller = new Sunny.UI.UILabel();
            uiGroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // uiGroupBox1
            // 
            uiGroupBox1.BackColor = System.Drawing.Color.Transparent;
            uiGroupBox1.Controls.Add(bt_create_order);
            uiGroupBox1.Controls.Add(lb_timestamp);
            uiGroupBox1.Controls.Add(lb_contact);
            uiGroupBox1.Controls.Add(bt_copy_contact);
            uiGroupBox1.Controls.Add(bt_show_balance);
            uiGroupBox1.Controls.Add(lb_remarks);
            uiGroupBox1.Controls.Add(lb_seller);
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
            uiGroupBox1.Size = new System.Drawing.Size(951, 243);
            uiGroupBox1.TabIndex = 0;
            uiGroupBox1.Text = "uiGroupBox1";
            uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            uiGroupBox1.Click += lb_master_balance_Click;
            // 
            // bt_create_order
            // 
            bt_create_order.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            bt_create_order.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_create_order.Location = new System.Drawing.Point(723, 165);
            bt_create_order.MinimumSize = new System.Drawing.Size(1, 1);
            bt_create_order.Name = "bt_create_order";
            bt_create_order.Radius = 50;
            bt_create_order.Size = new System.Drawing.Size(191, 52);
            bt_create_order.TabIndex = 26;
            bt_create_order.Text = "uiButton1";
            bt_create_order.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_create_order.Click += bt_create_order_Click;
            // 
            // lb_timestamp
            // 
            lb_timestamp.AutoSize = true;
            lb_timestamp.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_timestamp.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_timestamp.Location = new System.Drawing.Point(23, 186);
            lb_timestamp.Name = "lb_timestamp";
            lb_timestamp.Size = new System.Drawing.Size(73, 21);
            lb_timestamp.TabIndex = 25;
            lb_timestamp.Text = "uiLabel1";
            lb_timestamp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_contact
            // 
            lb_contact.AutoSize = true;
            lb_contact.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_contact.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_contact.Location = new System.Drawing.Point(13, 77);
            lb_contact.Name = "lb_contact";
            lb_contact.Size = new System.Drawing.Size(83, 24);
            lb_contact.TabIndex = 24;
            lb_contact.Text = "uiLabel1";
            lb_contact.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bt_copy_contact
            // 
            bt_copy_contact.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_copy_contact.Location = new System.Drawing.Point(475, 165);
            bt_copy_contact.MinimumSize = new System.Drawing.Size(1, 1);
            bt_copy_contact.Name = "bt_copy_contact";
            bt_copy_contact.Radius = 50;
            bt_copy_contact.Size = new System.Drawing.Size(191, 52);
            bt_copy_contact.TabIndex = 22;
            bt_copy_contact.Text = "uiButton1";
            bt_copy_contact.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_copy_contact.Click += bt_self_lock_Click;
            // 
            // bt_show_balance
            // 
            bt_show_balance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_show_balance.Location = new System.Drawing.Point(235, 165);
            bt_show_balance.MinimumSize = new System.Drawing.Size(1, 1);
            bt_show_balance.Name = "bt_show_balance";
            bt_show_balance.Radius = 50;
            bt_show_balance.Size = new System.Drawing.Size(191, 52);
            bt_show_balance.TabIndex = 21;
            bt_show_balance.Text = "uiButton1";
            bt_show_balance.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_show_balance.Click += bt_reg_miner_Click;
            // 
            // lb_remarks
            // 
            lb_remarks.AutoSize = true;
            lb_remarks.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_remarks.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_remarks.Location = new System.Drawing.Point(13, 110);
            lb_remarks.Name = "lb_remarks";
            lb_remarks.Size = new System.Drawing.Size(83, 24);
            lb_remarks.TabIndex = 20;
            lb_remarks.Text = "uiLabel1";
            lb_remarks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_remarks.Click += lb_master_balance_Click;
            // 
            // lb_seller
            // 
            lb_seller.AutoSize = true;
            lb_seller.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_seller.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_seller.Location = new System.Drawing.Point(13, 42);
            lb_seller.Name = "lb_seller";
            lb_seller.Size = new System.Drawing.Size(83, 24);
            lb_seller.TabIndex = 8;
            lb_seller.Text = "uiLabel1";
            lb_seller.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_seller.Click += lb_master_balance_Click;
            // 
            // DirectSalePublishInfo
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Transparent;
            Controls.Add(uiGroupBox1);
            Name = "DirectSalePublishInfo";
            Size = new System.Drawing.Size(951, 243);
            Click += lb_master_balance_Click;
            uiGroupBox1.ResumeLayout(false);
            uiGroupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UILabel lb_seller;
        private Sunny.UI.UILabel lb_remarks;
        private Sunny.UI.UIButton bt_show_balance;
        private Sunny.UI.UIButton bt_copy_contact;
        private Sunny.UI.UILabel lb_contact;
        private Sunny.UI.UILabel lb_timestamp;
        private Sunny.UI.UIButton bt_create_order;
    }
}
