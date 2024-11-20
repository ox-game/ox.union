namespace OX.Tablet.UIs.MarkSix
{
    partial class MarkSixPlainInboundSubPage
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
            tv_inbound_orders = new Sunny.UI.UITreeView();
            bt_collapse = new Sunny.UI.UIButton();
            bt_delete = new Sunny.UI.UIButton();
            bt_go_bet = new Sunny.UI.UIButton();
            SuspendLayout();
            // 
            // tv_inbound_orders
            // 
            tv_inbound_orders.AllowDrop = true;
            tv_inbound_orders.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tv_inbound_orders.CheckBoxes = true;
            tv_inbound_orders.FillColor = System.Drawing.Color.White;
            tv_inbound_orders.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tv_inbound_orders.ItemHeight = 58;
            tv_inbound_orders.Location = new System.Drawing.Point(12, 23);
            tv_inbound_orders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tv_inbound_orders.MinimumSize = new System.Drawing.Size(1, 1);
            tv_inbound_orders.Name = "tv_inbound_orders";
            tv_inbound_orders.ScrollBarStyleInherited = false;
            tv_inbound_orders.ShowLines = true;
            tv_inbound_orders.ShowText = false;
            tv_inbound_orders.Size = new System.Drawing.Size(586, 719);
            tv_inbound_orders.TabIndex = 1;
            tv_inbound_orders.Text = "uiTreeView1";
            tv_inbound_orders.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            tv_inbound_orders.BeforeCheck += tv_inbound_orders_BeforeCheck;
            // 
            // bt_collapse
            // 
            bt_collapse.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            bt_collapse.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_collapse.Location = new System.Drawing.Point(12, 765);
            bt_collapse.MinimumSize = new System.Drawing.Size(1, 1);
            bt_collapse.Name = "bt_collapse";
            bt_collapse.Size = new System.Drawing.Size(95, 52);
            bt_collapse.TabIndex = 14;
            bt_collapse.Text = "uiButton1";
            bt_collapse.Click += bt_collapse_Click;
            // 
            // bt_delete
            // 
            bt_delete.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            bt_delete.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_delete.Location = new System.Drawing.Point(503, 765);
            bt_delete.MinimumSize = new System.Drawing.Size(1, 1);
            bt_delete.Name = "bt_delete";
            bt_delete.Size = new System.Drawing.Size(95, 52);
            bt_delete.TabIndex = 15;
            bt_delete.Text = "uiButton1";
            bt_delete.Click += bt_delete_Click;
            // 
            // bt_go_bet
            // 
            bt_go_bet.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            bt_go_bet.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_go_bet.Location = new System.Drawing.Point(136, 765);
            bt_go_bet.MinimumSize = new System.Drawing.Size(1, 1);
            bt_go_bet.Name = "bt_go_bet";
            bt_go_bet.Radius = 50;
            bt_go_bet.Size = new System.Drawing.Size(335, 52);
            bt_go_bet.TabIndex = 16;
            bt_go_bet.Text = "uiButton1";
            bt_go_bet.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // InboundSubPage
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(611, 845);
            Controls.Add(bt_go_bet);
            Controls.Add(bt_delete);
            Controls.Add(bt_collapse);
            Controls.Add(tv_inbound_orders);
            Name = "InboundSubPage";
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
        private Sunny.UI.UITreeView tv_inbound_orders;
        private Sunny.UI.UIButton bt_collapse;
        private Sunny.UI.UIButton bt_delete;
        private Sunny.UI.UIButton bt_go_bet;
    }
}