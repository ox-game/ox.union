namespace OX.Tablet
{
    partial class ContactItem
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
            bt_copy = new Sunny.UI.UIButton();
            bt_delete = new Sunny.UI.UIButton();
            lb_address = new Sunny.UI.UILabel();
            uiGroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // uiGroupBox1
            // 
            uiGroupBox1.Controls.Add(bt_copy);
            uiGroupBox1.Controls.Add(bt_delete);
            uiGroupBox1.Controls.Add(lb_address);
            uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            uiGroupBox1.FillColor = System.Drawing.Color.Transparent;
            uiGroupBox1.FillColor2 = System.Drawing.Color.Transparent;
            uiGroupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            uiGroupBox1.Name = "uiGroupBox1";
            uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            uiGroupBox1.RectSize = 2;
            uiGroupBox1.Size = new System.Drawing.Size(834, 159);
            uiGroupBox1.TabIndex = 0;
            uiGroupBox1.Text = "uiGroupBox1";
            uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            uiGroupBox1.Click += lb_master_balance_Click;
            // 
            // bt_copy
            // 
            bt_copy.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            bt_copy.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_copy.Location = new System.Drawing.Point(377, 90);
            bt_copy.MinimumSize = new System.Drawing.Size(1, 1);
            bt_copy.Name = "bt_copy";
            bt_copy.Size = new System.Drawing.Size(191, 52);
            bt_copy.TabIndex = 20;
            bt_copy.Text = "uiButton1";
            bt_copy.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_copy.Click += bt_copy_Click;
            // 
            // bt_delete
            // 
            bt_delete.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            bt_delete.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_delete.Location = new System.Drawing.Point(595, 90);
            bt_delete.MinimumSize = new System.Drawing.Size(1, 1);
            bt_delete.Name = "bt_delete";
            bt_delete.Size = new System.Drawing.Size(191, 52);
            bt_delete.TabIndex = 19;
            bt_delete.Text = "uiButton1";
            bt_delete.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_delete.Click += bt_transfer_Click;
            // 
            // lb_address
            // 
            lb_address.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_address.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_address.Location = new System.Drawing.Point(13, 42);
            lb_address.Name = "lb_address";
            lb_address.Size = new System.Drawing.Size(757, 34);
            lb_address.TabIndex = 8;
            lb_address.Text = "uiLabel1";
            lb_address.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_address.Click += lb_master_balance_Click;
            // 
            // ContactItem
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Transparent;
            Controls.Add(uiGroupBox1);
            Name = "ContactItem";
            Size = new System.Drawing.Size(834, 159);
            Click += lb_master_balance_Click;
            uiGroupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UILabel lb_address;
        private Sunny.UI.UIButton bt_delete;
        private Sunny.UI.UIButton bt_copy;
    }
}
