namespace OX.Tablet
{
    partial class NewContact
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
            tb_name = new Sunny.UI.UITextBox();
            tb_address = new Sunny.UI.UITextBox();
            lb_name = new Sunny.UI.UILabel();
            bt_delete = new Sunny.UI.UIButton();
            lb_address = new Sunny.UI.UILabel();
            uiGroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // uiGroupBox1
            // 
            uiGroupBox1.Controls.Add(tb_name);
            uiGroupBox1.Controls.Add(tb_address);
            uiGroupBox1.Controls.Add(lb_name);
            uiGroupBox1.Controls.Add(bt_delete);
            uiGroupBox1.Controls.Add(lb_address);
            uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            uiGroupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            uiGroupBox1.Name = "uiGroupBox1";
            uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            uiGroupBox1.RectSize = 2;
            uiGroupBox1.Size = new System.Drawing.Size(834, 187);
            uiGroupBox1.TabIndex = 0;
            uiGroupBox1.Text = "uiGroupBox1";
            uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            uiGroupBox1.Click += lb_master_balance_Click;
            // 
            // tb_name
            // 
            tb_name.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_name.Location = new System.Drawing.Point(175, 89);
            tb_name.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_name.MinimumSize = new System.Drawing.Size(1, 16);
            tb_name.Name = "tb_name";
            tb_name.Padding = new System.Windows.Forms.Padding(5);
            tb_name.ShowText = false;
            tb_name.Size = new System.Drawing.Size(279, 44);
            tb_name.TabIndex = 22;
            tb_name.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_name.Watermark = "";
            // 
            // tb_address
            // 
            tb_address.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_address.Location = new System.Drawing.Point(175, 32);
            tb_address.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_address.MinimumSize = new System.Drawing.Size(1, 16);
            tb_address.Name = "tb_address";
            tb_address.Padding = new System.Windows.Forms.Padding(5);
            tb_address.ShowText = false;
            tb_address.Size = new System.Drawing.Size(570, 44);
            tb_address.TabIndex = 21;
            tb_address.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_address.Watermark = "";
            // 
            // lb_name
            // 
            lb_name.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_name.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_name.Location = new System.Drawing.Point(13, 99);
            lb_name.Name = "lb_name";
            lb_name.Size = new System.Drawing.Size(155, 34);
            lb_name.TabIndex = 20;
            lb_name.Text = "uiLabel1";
            lb_name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bt_delete
            // 
            bt_delete.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_delete.Location = new System.Drawing.Point(554, 89);
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
            lb_address.Size = new System.Drawing.Size(155, 34);
            lb_address.TabIndex = 8;
            lb_address.Text = "uiLabel1";
            lb_address.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_address.Click += lb_master_balance_Click;
            // 
            // NewContact
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(uiGroupBox1);
            Name = "NewContact";
            Size = new System.Drawing.Size(834, 187);
            Click += lb_master_balance_Click;
            uiGroupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UILabel lb_address;
        private Sunny.UI.UIButton bt_delete;
        private Sunny.UI.UILabel lb_name;
        private Sunny.UI.UITextBox tb_name;
        private Sunny.UI.UITextBox tb_address;
    }
}
