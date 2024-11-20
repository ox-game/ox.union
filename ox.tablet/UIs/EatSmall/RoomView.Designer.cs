namespace OX.Tablet.UIs.MarkSix
{
    partial class RoomView
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
            Box = new Sunny.UI.UITableLayoutPanel();
            L1 = new Sunny.UI.UILine();
            lb_prize = new Sunny.UI.UIFlowLayoutPanel();
            bt_pre = new Sunny.UI.UIButton();
            bt_next = new Sunny.UI.UIButton();
            lb_bet_info = new Sunny.UI.UILabel();
            bt_go_current = new RoundButton();
            lb_pool_info = new Sunny.UI.UILabel();
            SuspendLayout();
            // 
            // Box
            // 
            Box.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            Box.AutoSize = true;
            Box.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            Box.ColumnCount = 3;
            Box.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.61867F));
            Box.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.38133F));
            Box.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 469F));
            Box.Location = new System.Drawing.Point(12, 83);
            Box.Name = "Box";
            Box.RowCount = 3;
            Box.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            Box.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            Box.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 242F));
            Box.Size = new System.Drawing.Size(1215, 750);
            Box.TabIndex = 0;
            Box.TagString = null;
            // 
            // L1
            // 
            L1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            L1.BackColor = System.Drawing.Color.Transparent;
            L1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            L1.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            L1.Location = new System.Drawing.Point(1234, 71);
            L1.MinimumSize = new System.Drawing.Size(1, 1);
            L1.Name = "L1";
            L1.Size = new System.Drawing.Size(201, 26);
            L1.TabIndex = 16;
            L1.Text = "uiLine1";
            // 
            // lb_prize
            // 
            lb_prize.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            lb_prize.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_prize.Location = new System.Drawing.Point(1235, 105);
            lb_prize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            lb_prize.MinimumSize = new System.Drawing.Size(1, 1);
            lb_prize.Name = "lb_prize";
            lb_prize.Padding = new System.Windows.Forms.Padding(2, 10, 2, 2);
            lb_prize.Radius = 1;
            lb_prize.RectColor = System.Drawing.Color.Transparent;
            lb_prize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            lb_prize.ScrollBarHandleWidth = 100;
            lb_prize.ShowText = false;
            lb_prize.Size = new System.Drawing.Size(201, 726);
            lb_prize.TabIndex = 15;
            lb_prize.Text = "uiFlowLayoutPanel1";
            lb_prize.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_pre
            // 
            bt_pre.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_pre.Location = new System.Drawing.Point(13, 16);
            bt_pre.MinimumSize = new System.Drawing.Size(1, 1);
            bt_pre.Name = "bt_pre";
            bt_pre.Radius = 50;
            bt_pre.Size = new System.Drawing.Size(135, 52);
            bt_pre.TabIndex = 17;
            bt_pre.Text = "uiButton1";
            bt_pre.Click += bt_pre_Click;
            // 
            // bt_next
            // 
            bt_next.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_next.Location = new System.Drawing.Point(379, 16);
            bt_next.MinimumSize = new System.Drawing.Size(1, 1);
            bt_next.Name = "bt_next";
            bt_next.Radius = 50;
            bt_next.Size = new System.Drawing.Size(135, 52);
            bt_next.TabIndex = 18;
            bt_next.Text = "uiButton1";
            bt_next.Click += bt_next_Click;
            // 
            // lb_bet_info
            // 
            lb_bet_info.AutoSize = true;
            lb_bet_info.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_bet_info.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_bet_info.Location = new System.Drawing.Point(562, 10);
            lb_bet_info.Name = "lb_bet_info";
            lb_bet_info.Size = new System.Drawing.Size(106, 24);
            lb_bet_info.TabIndex = 20;
            lb_bet_info.Text = "uiLabel1";
            // 
            // bt_go_current
            // 
            bt_go_current.FillColor = System.Drawing.Color.DarkOrange;
            bt_go_current.FillColor2 = System.Drawing.Color.DarkOrange;
            bt_go_current.FillHoverColor = System.Drawing.Color.DarkOrange;
            bt_go_current.FillPressColor = System.Drawing.Color.DarkOrange;
            bt_go_current.FillSelectedColor = System.Drawing.Color.DarkOrange;
            bt_go_current.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_go_current.Location = new System.Drawing.Point(184, 16);
            bt_go_current.MinimumSize = new System.Drawing.Size(1, 1);
            bt_go_current.Name = "bt_go_current";
            bt_go_current.RectColor = System.Drawing.Color.DarkOrange;
            bt_go_current.RectHoverColor = System.Drawing.Color.DarkOrange;
            bt_go_current.RectPressColor = System.Drawing.Color.DarkOrange;
            bt_go_current.RectSelectedColor = System.Drawing.Color.DarkOrange;
            bt_go_current.Size = new System.Drawing.Size(158, 52);
            bt_go_current.Style = Sunny.UI.UIStyle.Custom;
            bt_go_current.StyleCustomMode = true;
            bt_go_current.TabIndex = 21;
            bt_go_current.Text = "uiButton1";
            bt_go_current.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            bt_go_current.UseDoubleClick = true;
            bt_go_current.Click += bt_go_current_Click;
            // 
            // lb_pool_info
            // 
            lb_pool_info.AutoSize = true;
            lb_pool_info.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_pool_info.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_pool_info.Location = new System.Drawing.Point(562, 48);
            lb_pool_info.Name = "lb_pool_info";
            lb_pool_info.Size = new System.Drawing.Size(106, 24);
            lb_pool_info.TabIndex = 22;
            lb_pool_info.Text = "uiLabel1";
            // 
            // RoomView
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1445, 845);
            Controls.Add(lb_pool_info);
            Controls.Add(bt_go_current);
            Controls.Add(lb_bet_info);
            Controls.Add(bt_next);
            Controls.Add(bt_pre);
            Controls.Add(lb_prize);
            Controls.Add(L1);
            Controls.Add(Box);
            Name = "RoomView";
            Text = "PlazaForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Sunny.UI.UIButton bt_next;
        private Sunny.UI.UITableLayoutPanel Box;
        private Sunny.UI.UILine L1;
        private Sunny.UI.UIFlowLayoutPanel lb_prize;
        private Sunny.UI.UIButton bt_pre;
        private Sunny.UI.UILabel lb_bet_info;
        private RoundButton bt_go_current;
        private Sunny.UI.UILabel lb_pool_info;
    }
}