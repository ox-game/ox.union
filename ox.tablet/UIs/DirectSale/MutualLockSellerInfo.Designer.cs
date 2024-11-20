﻿namespace OX.Tablet
{
    partial class MutualLockSellerInfo
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
            lb_lock_state = new Sunny.UI.UILabel();
            lb_delivery_expire = new Sunny.UI.UILabel();
            lb_assetName = new Sunny.UI.UILabel();
            bt_delivery = new Sunny.UI.UIButton();
            lb_amount = new Sunny.UI.UILabel();
            bt_redeem = new Sunny.UI.UIButton();
            lb_buyer = new Sunny.UI.UILabel();
            uiGroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // uiGroupBox1
            // 
            uiGroupBox1.BackColor = System.Drawing.Color.Transparent;
            uiGroupBox1.Controls.Add(lb_lock_state);
            uiGroupBox1.Controls.Add(lb_delivery_expire);
            uiGroupBox1.Controls.Add(lb_assetName);
            uiGroupBox1.Controls.Add(bt_delivery);
            uiGroupBox1.Controls.Add(lb_amount);
            uiGroupBox1.Controls.Add(bt_redeem);
            uiGroupBox1.Controls.Add(lb_buyer);
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
            uiGroupBox1.Size = new System.Drawing.Size(685, 343);
            uiGroupBox1.TabIndex = 0;
            uiGroupBox1.Text = "uiGroupBox1";
            uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            uiGroupBox1.Click += lb_master_balance_Click;
            // 
            // lb_lock_state
            // 
            lb_lock_state.AutoSize = true;
            lb_lock_state.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_lock_state.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_lock_state.Location = new System.Drawing.Point(13, 187);
            lb_lock_state.Name = "lb_lock_state";
            lb_lock_state.Size = new System.Drawing.Size(83, 24);
            lb_lock_state.TabIndex = 26;
            lb_lock_state.Text = "uiLabel1";
            lb_lock_state.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_delivery_expire
            // 
            lb_delivery_expire.AutoSize = true;
            lb_delivery_expire.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_delivery_expire.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_delivery_expire.Location = new System.Drawing.Point(13, 147);
            lb_delivery_expire.Name = "lb_delivery_expire";
            lb_delivery_expire.Size = new System.Drawing.Size(83, 24);
            lb_delivery_expire.TabIndex = 25;
            lb_delivery_expire.Text = "uiLabel1";
            lb_delivery_expire.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_assetName
            // 
            lb_assetName.AutoSize = true;
            lb_assetName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_assetName.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_assetName.Location = new System.Drawing.Point(13, 77);
            lb_assetName.Name = "lb_assetName";
            lb_assetName.Size = new System.Drawing.Size(83, 24);
            lb_assetName.TabIndex = 24;
            lb_assetName.Text = "uiLabel1";
            lb_assetName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bt_delivery
            // 
            bt_delivery.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            bt_delivery.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_delivery.Location = new System.Drawing.Point(32, 264);
            bt_delivery.MinimumSize = new System.Drawing.Size(1, 1);
            bt_delivery.Name = "bt_delivery";
            bt_delivery.Radius = 50;
            bt_delivery.Size = new System.Drawing.Size(191, 52);
            bt_delivery.TabIndex = 23;
            bt_delivery.Text = "uiButton1";
            bt_delivery.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_delivery.Click += bt_delivery_Click;
            // 
            // lb_amount
            // 
            lb_amount.AutoSize = true;
            lb_amount.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_amount.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_amount.Location = new System.Drawing.Point(13, 110);
            lb_amount.Name = "lb_amount";
            lb_amount.Size = new System.Drawing.Size(83, 24);
            lb_amount.TabIndex = 20;
            lb_amount.Text = "uiLabel1";
            lb_amount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_amount.Click += lb_master_balance_Click;
            // 
            // bt_redeem
            // 
            bt_redeem.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            bt_redeem.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_redeem.Location = new System.Drawing.Point(464, 264);
            bt_redeem.MinimumSize = new System.Drawing.Size(1, 1);
            bt_redeem.Name = "bt_redeem";
            bt_redeem.Radius = 50;
            bt_redeem.Size = new System.Drawing.Size(191, 52);
            bt_redeem.TabIndex = 19;
            bt_redeem.Text = "uiButton1";
            bt_redeem.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_redeem.Click += bt_redeem_Click;
            // 
            // lb_buyer
            // 
            lb_buyer.AutoSize = true;
            lb_buyer.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_buyer.ForeColor = System.Drawing.Color.FromArgb(56, 56, 56);
            lb_buyer.Location = new System.Drawing.Point(13, 42);
            lb_buyer.Name = "lb_buyer";
            lb_buyer.Size = new System.Drawing.Size(83, 24);
            lb_buyer.TabIndex = 8;
            lb_buyer.Text = "uiLabel1";
            lb_buyer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_buyer.Click += lb_master_balance_Click;
            // 
            // MutualLockSellerInfo
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Transparent;
            Controls.Add(uiGroupBox1);
            Name = "MutualLockSellerInfo";
            Size = new System.Drawing.Size(685, 343);
            Click += lb_master_balance_Click;
            uiGroupBox1.ResumeLayout(false);
            uiGroupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UILabel lb_buyer;
       
        private Sunny.UI.UIButton bt_redeem;
        private Sunny.UI.UILabel lb_amount;
        private Sunny.UI.UIButton bt_delivery;
        private Sunny.UI.UILabel lb_assetName;
        private Sunny.UI.UILabel lb_delivery_expire;
        private Sunny.UI.UILabel lb_lock_state;
    }
}