namespace OX.Tablet.UIs.MarkSix
{
    partial class MessagePage
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
            bt_refresh = new Sunny.UI.UIButton();
            L1 = new Sunny.UI.UILine();
            pn_pairs = new Sunny.UI.UIFlowLayoutPanel();
            SuspendLayout();
            // 
            // bt_refresh
            // 
            bt_refresh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            bt_refresh.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_refresh.Location = new System.Drawing.Point(1232, 9);
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
            L1.Location = new System.Drawing.Point(17, 23);
            L1.MinimumSize = new System.Drawing.Size(1, 1);
            L1.Name = "L1";
            L1.Size = new System.Drawing.Size(1200, 26);
            L1.TabIndex = 17;
            L1.Text = "uiLine1";
            // 
            // pn_pairs
            // 
            pn_pairs.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pn_pairs.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            pn_pairs.Location = new System.Drawing.Point(13, 84);
            pn_pairs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pn_pairs.MinimumSize = new System.Drawing.Size(1, 1);
            pn_pairs.Name = "pn_pairs";
            pn_pairs.Padding = new System.Windows.Forms.Padding(2, 10, 2, 2);
            pn_pairs.RectColor = System.Drawing.Color.Transparent;
            pn_pairs.RightToLeft = System.Windows.Forms.RightToLeft.No;
            pn_pairs.ScrollBarHandleWidth = 100;
            pn_pairs.ShowText = false;
            pn_pairs.Size = new System.Drawing.Size(1418, 745);
            pn_pairs.TabIndex = 18;
            pn_pairs.Text = "uiFlowLayoutPanel1";
            pn_pairs.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            pn_pairs.SizeChanged += pn_pairs_SizeChanged;
            // 
            // MessagePage
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1445, 845);
            Controls.Add(pn_pairs);
            Controls.Add(bt_refresh);
            Controls.Add(L1);
            Name = "MessagePage";
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
        private Sunny.UI.UIButton bt_refresh;
        private Sunny.UI.UILine L1;
        private Sunny.UI.UIFlowLayoutPanel pn_pairs;
    }
}