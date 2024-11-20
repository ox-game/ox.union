namespace OX.Tablet.UIs.MarkSix
{
    partial class MarkSixPostPlainOrderForm
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
            bt_post_order = new Sunny.UI.UIButton();
            bt_close = new Sunny.UI.UIButton();
            L1 = new Sunny.UI.UILine();
            tb_transfer_inbound = new Sunny.UI.MarkTransfer();
            lb_msg = new Sunny.UI.UILabel();
            SuspendLayout();
            // 
            // bt_post_order
            // 
            bt_post_order.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            bt_post_order.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_post_order.Location = new System.Drawing.Point(935, 886);
            bt_post_order.MinimumSize = new System.Drawing.Size(1, 1);
            bt_post_order.Name = "bt_post_order";
            bt_post_order.Size = new System.Drawing.Size(150, 52);
            bt_post_order.TabIndex = 8;
            bt_post_order.Text = "uiButton1";
            bt_post_order.Click += bt_post_order_Click;
            // 
            // bt_close
            // 
            bt_close.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            bt_close.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_close.Location = new System.Drawing.Point(17, 886);
            bt_close.MinimumSize = new System.Drawing.Size(1, 1);
            bt_close.Name = "bt_close";
            bt_close.Size = new System.Drawing.Size(150, 52);
            bt_close.TabIndex = 7;
            bt_close.Text = "uiButton1";
            bt_close.Click += bt_close_Click;
            // 
            // L1
            // 
            L1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            L1.BackColor = System.Drawing.Color.Transparent;
            L1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            L1.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            L1.Location = new System.Drawing.Point(17, 35);
            L1.MinimumSize = new System.Drawing.Size(1, 1);
            L1.Name = "L1";
            L1.Size = new System.Drawing.Size(1068, 44);
            L1.TabIndex = 10;
            L1.Text = "uiLine1";
            // 
            // tb_transfer_inbound
            // 
            tb_transfer_inbound.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_transfer_inbound.ItemHeight = 58;
            tb_transfer_inbound.Location = new System.Drawing.Point(17, 79);
            tb_transfer_inbound.Margin = new System.Windows.Forms.Padding(7, 9, 7, 9);
            tb_transfer_inbound.MinimumSize = new System.Drawing.Size(1, 1);
            tb_transfer_inbound.Name = "tb_transfer_inbound";
            tb_transfer_inbound.Padding = new System.Windows.Forms.Padding(1);
            tb_transfer_inbound.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            tb_transfer_inbound.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            tb_transfer_inbound.ShowText = false;
            tb_transfer_inbound.Size = new System.Drawing.Size(1068, 742);
            tb_transfer_inbound.TabIndex = 12;
            tb_transfer_inbound.Text = null;
            tb_transfer_inbound.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_msg
            // 
            lb_msg.AutoSize = true;
            lb_msg.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_msg.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_msg.Location = new System.Drawing.Point(17, 832);
            lb_msg.Name = "lb_msg";
            lb_msg.Size = new System.Drawing.Size(0, 31);
            lb_msg.TabIndex = 17;
            lb_msg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MarkSixPostPlaintOrderForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(1102, 966);
            ControlBox = false;
            Controls.Add(lb_msg);
            Controls.Add(tb_transfer_inbound);
            Controls.Add(L1);
            Controls.Add(bt_post_order);
            Controls.Add(bt_close);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MarkSixPostPlaintOrderForm";
            Text = "PushOrderForm";
            ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 800, 450);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Sunny.UI.UIButton bt_post_order;
        private Sunny.UI.UIButton bt_close;
        private Sunny.UI.UILine L1;
        private Sunny.UI.MarkTransfer tb_transfer_inbound;
        private Sunny.UI.UILabel lb_msg;
    }
}