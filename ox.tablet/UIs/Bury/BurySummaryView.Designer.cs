namespace OX.Tablet.UIs.MarkSix
{
    partial class BurySummaryView
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
            bt_refresh = new Sunny.UI.UIButton();
            L1 = new Sunny.UI.UILine();
            SuspendLayout();
            // 
            // Box
            // 
            Box.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            Box.AutoSize = true;
            Box.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            Box.ColumnCount = 2;
            Box.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.61867F));
            Box.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.38133F));
            Box.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 469F));
            Box.Location = new System.Drawing.Point(12, 90);
            Box.Name = "Box";
            Box.RowCount = 2;
            Box.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            Box.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            Box.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 242F));
            Box.Size = new System.Drawing.Size(1421, 743);
            Box.TabIndex = 0;
            Box.TagString = null;
            // 
            // bt_refresh
            // 
            bt_refresh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            bt_refresh.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_refresh.Location = new System.Drawing.Point(1237, 22);
            bt_refresh.MinimumSize = new System.Drawing.Size(1, 1);
            bt_refresh.Name = "bt_refresh";
            bt_refresh.Size = new System.Drawing.Size(196, 52);
            bt_refresh.TabIndex = 16;
            bt_refresh.Text = "uiButton1";
            bt_refresh.Click += bt_refresh_Click;
            // 
            // L1
            // 
            L1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            L1.BackColor = System.Drawing.Color.Transparent;
            L1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            L1.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            L1.Location = new System.Drawing.Point(14, 36);
            L1.MinimumSize = new System.Drawing.Size(1, 1);
            L1.Name = "L1";
            L1.Size = new System.Drawing.Size(1206, 26);
            L1.TabIndex = 17;
            // 
            // BurySummaryView
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1445, 845);
            Controls.Add(bt_refresh);
            Controls.Add(L1);
            Controls.Add(Box);
            Name = "BurySummaryView";
            Text = "PlazaForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Sunny.UI.UITableLayoutPanel Box;
        private Sunny.UI.UIButton bt_refresh;
        private Sunny.UI.UILine L1;
    }
}