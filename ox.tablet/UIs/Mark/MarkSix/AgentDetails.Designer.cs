namespace OX.Tablet
{
    partial class AgentDetails
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
            bt_today = new Sunny.UI.UIButton();
            bt_next_day = new Sunny.UI.UIButton();
            bt_pre_day = new Sunny.UI.UIButton();
            pn_orders = new Sunny.UI.UIFlowLayoutPanel();
            SuspendLayout();
            // 
            // bt_today
            // 
            bt_today.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_today.Location = new System.Drawing.Point(238, 30);
            bt_today.MinimumSize = new System.Drawing.Size(1, 1);
            bt_today.Name = "bt_today";
            bt_today.Size = new System.Drawing.Size(209, 52);
            bt_today.TabIndex = 17;
            bt_today.Text = "uiButton1";
            bt_today.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_today.Click += bt_today_Click;
            // 
            // bt_next_day
            // 
            bt_next_day.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_next_day.Location = new System.Drawing.Point(482, 30);
            bt_next_day.MinimumSize = new System.Drawing.Size(1, 1);
            bt_next_day.Name = "bt_next_day";
            bt_next_day.Size = new System.Drawing.Size(150, 52);
            bt_next_day.TabIndex = 16;
            bt_next_day.Text = "uiButton1";
            bt_next_day.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_next_day.Click += bt_next_day_Click;
            // 
            // bt_pre_day
            // 
            bt_pre_day.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_pre_day.Location = new System.Drawing.Point(52, 30);
            bt_pre_day.MinimumSize = new System.Drawing.Size(1, 1);
            bt_pre_day.Name = "bt_pre_day";
            bt_pre_day.Size = new System.Drawing.Size(150, 52);
            bt_pre_day.TabIndex = 15;
            bt_pre_day.Text = "uiButton1";
            bt_pre_day.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_pre_day.Click += bt_pre_day_Click;
            // 
            // pn_orders
            // 
            pn_orders.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pn_orders.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            pn_orders.Location = new System.Drawing.Point(4, 103);
            pn_orders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pn_orders.MinimumSize = new System.Drawing.Size(1, 1);
            pn_orders.Name = "pn_orders";
            pn_orders.Padding = new System.Windows.Forms.Padding(2, 10, 2, 2);
            pn_orders.RectColor = System.Drawing.Color.Transparent;
            pn_orders.RightToLeft = System.Windows.Forms.RightToLeft.No;
            pn_orders.ScrollBarHandleWidth = 100;
            pn_orders.ShowText = false;
            pn_orders.Size = new System.Drawing.Size(1321, 795);
            pn_orders.TabIndex = 18;
            pn_orders.Text = "uiFlowLayoutPanel1";
            pn_orders.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PortDetails
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(pn_orders);
            Controls.Add(bt_today);
            Controls.Add(bt_next_day);
            Controls.Add(bt_pre_day);
            Name = "PortDetails";
            Size = new System.Drawing.Size(1329, 903);
            Click += lb_master_balance_Click;
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIButton bt_today;
        private Sunny.UI.UIButton bt_next_day;
        private Sunny.UI.UIButton bt_pre_day;
        private Sunny.UI.UIFlowLayoutPanel pn_orders;
    }
}
