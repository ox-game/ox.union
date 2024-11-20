namespace OX.Tablet.UIs.MarkSix
{
    partial class MarkSixCipherOutboundSubPage
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
            tv_outbound_orders = new Sunny.UI.UITreeView();
            SuspendLayout();
            // 
            // tv_outbound_orders
            // 
            tv_outbound_orders.AllowDrop = true;
            tv_outbound_orders.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tv_outbound_orders.FillColor = System.Drawing.Color.White;
            tv_outbound_orders.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tv_outbound_orders.ItemHeight = 58;
            tv_outbound_orders.Location = new System.Drawing.Point(12, 23);
            tv_outbound_orders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tv_outbound_orders.MinimumSize = new System.Drawing.Size(1, 1);
            tv_outbound_orders.Name = "tv_outbound_orders";
            tv_outbound_orders.ScrollBarStyleInherited = false;
            tv_outbound_orders.ShowLines = true;
            tv_outbound_orders.ShowText = false;
            tv_outbound_orders.Size = new System.Drawing.Size(586, 808);
            tv_outbound_orders.TabIndex = 1;
            tv_outbound_orders.Text = "uiTreeView1";
            tv_outbound_orders.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            tv_outbound_orders.BeforeCheck += tv_inbound_orders_BeforeCheck;
            // 
            // MarkSixCipherOutboundSubPage
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(611, 845);
            Controls.Add(tv_outbound_orders);
            Name = "MarkSixCipherOutboundSubPage";
            Text = "PlazaForm";
            Initialize += OrderHistory_Initialize;
            ResumeLayout(false);
        }

        #endregion
        private Sunny.UI.UIButton uiButton1;
        private Sunny.UI.UIButton uiButton2;
        private Sunny.UI.UIButton uiButton3;
        private Sunny.UI.UIButton uiButton4;
        private Sunny.UI.UIButton uiButton5;
        private Sunny.UI.UIButton uiButton6;
        private Sunny.UI.UITreeView tv_outbound_orders;
    }
}