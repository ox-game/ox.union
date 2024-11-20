namespace OX.Tablet.UIs.MarkSix
{
    partial class MarketPlacePage
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
            L1 = new Sunny.UI.UILine();
            pn_pairs = new Sunny.UI.UIFlowLayoutPanel();
            bt_refresh = new Sunny.UI.UIButton();
            uiLine1 = new Sunny.UI.UILine();
            rtb_remarks = new System.Windows.Forms.RichTextBox();
            rtb_contact = new System.Windows.Forms.RichTextBox();
            lb_contact = new Sunny.UI.UILabel();
            lb_mark = new Sunny.UI.UILabel();
            btn_publish = new Sunny.UI.UIButton();
            lb_asset = new Sunny.UI.UILabel();
            cb_asset = new Sunny.UI.UIComboBox();
            SuspendLayout();
            // 
            // L1
            // 
            L1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            L1.BackColor = System.Drawing.Color.Transparent;
            L1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            L1.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            L1.Location = new System.Drawing.Point(353, 26);
            L1.MinimumSize = new System.Drawing.Size(1, 1);
            L1.Name = "L1";
            L1.Size = new System.Drawing.Size(865, 26);
            L1.TabIndex = 15;
            L1.Text = "uiLine1";
            // 
            // pn_pairs
            // 
            pn_pairs.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pn_pairs.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            pn_pairs.Location = new System.Drawing.Point(353, 81);
            pn_pairs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pn_pairs.MinimumSize = new System.Drawing.Size(1, 1);
            pn_pairs.Name = "pn_pairs";
            pn_pairs.Padding = new System.Windows.Forms.Padding(2, 10, 2, 2);
            pn_pairs.RectColor = System.Drawing.Color.Transparent;
            pn_pairs.RightToLeft = System.Windows.Forms.RightToLeft.No;
            pn_pairs.ScrollBarHandleWidth = 100;
            pn_pairs.ShowText = false;
            pn_pairs.Size = new System.Drawing.Size(1078, 745);
            pn_pairs.TabIndex = 14;
            pn_pairs.Text = "uiFlowLayoutPanel1";
            pn_pairs.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_refresh
            // 
            bt_refresh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            bt_refresh.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_refresh.Location = new System.Drawing.Point(1235, 12);
            bt_refresh.MinimumSize = new System.Drawing.Size(1, 1);
            bt_refresh.Name = "bt_refresh";
            bt_refresh.Size = new System.Drawing.Size(196, 52);
            bt_refresh.TabIndex = 4;
            bt_refresh.Text = "uiButton1";
            bt_refresh.Click += bt_refresh_Click;
            // 
            // uiLine1
            // 
            uiLine1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            uiLine1.BackColor = System.Drawing.Color.Transparent;
            uiLine1.Direction = Sunny.UI.UILine.LineDirection.Vertical;
            uiLine1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiLine1.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            uiLine1.Location = new System.Drawing.Point(336, 59);
            uiLine1.MinimumSize = new System.Drawing.Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new System.Drawing.Size(10, 774);
            uiLine1.TabIndex = 16;
            uiLine1.Text = "uiLine1";
            // 
            // rtb_remarks
            // 
            rtb_remarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            rtb_remarks.Location = new System.Drawing.Point(12, 480);
            rtb_remarks.MaxLength = 60;
            rtb_remarks.Name = "rtb_remarks";
            rtb_remarks.Size = new System.Drawing.Size(318, 203);
            rtb_remarks.TabIndex = 38;
            rtb_remarks.Text = "";
            // 
            // rtb_contact
            // 
            rtb_contact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            rtb_contact.Location = new System.Drawing.Point(12, 234);
            rtb_contact.MaxLength = 30;
            rtb_contact.Name = "rtb_contact";
            rtb_contact.Size = new System.Drawing.Size(318, 130);
            rtb_contact.TabIndex = 37;
            rtb_contact.Text = "";
            // 
            // lb_contact
            // 
            lb_contact.AutoSize = true;
            lb_contact.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_contact.ForeColor = System.Drawing.Color.Black;
            lb_contact.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            lb_contact.Location = new System.Drawing.Point(12, 194);
            lb_contact.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lb_contact.Name = "lb_contact";
            lb_contact.Size = new System.Drawing.Size(130, 24);
            lb_contact.TabIndex = 39;
            lb_contact.Text = "Available:";
            // 
            // lb_mark
            // 
            lb_mark.AutoSize = true;
            lb_mark.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_mark.ForeColor = System.Drawing.Color.Black;
            lb_mark.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            lb_mark.Location = new System.Drawing.Point(14, 439);
            lb_mark.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lb_mark.Name = "lb_mark";
            lb_mark.Size = new System.Drawing.Size(130, 24);
            lb_mark.TabIndex = 40;
            lb_mark.Text = "Available:";
            // 
            // btn_publish
            // 
            btn_publish.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_publish.Location = new System.Drawing.Point(68, 714);
            btn_publish.MinimumSize = new System.Drawing.Size(1, 1);
            btn_publish.Name = "btn_publish";
            btn_publish.Radius = 50;
            btn_publish.Size = new System.Drawing.Size(196, 52);
            btn_publish.TabIndex = 41;
            btn_publish.Text = "uiButton1";
            // 
            // lb_asset
            // 
            lb_asset.AutoSize = true;
            lb_asset.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_asset.ForeColor = System.Drawing.Color.Black;
            lb_asset.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            lb_asset.Location = new System.Drawing.Point(12, 59);
            lb_asset.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lb_asset.Name = "lb_asset";
            lb_asset.Size = new System.Drawing.Size(130, 24);
            lb_asset.TabIndex = 42;
            lb_asset.Text = "Available:";
            // 
            // cb_asset
            // 
            cb_asset.DataSource = null;
            cb_asset.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            cb_asset.FillColor = System.Drawing.Color.White;
            cb_asset.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cb_asset.ItemHoverColor = System.Drawing.Color.FromArgb(155, 200, 255);
            cb_asset.ItemSelectForeColor = System.Drawing.Color.FromArgb(235, 243, 255);
            cb_asset.Location = new System.Drawing.Point(14, 103);
            cb_asset.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cb_asset.MinimumSize = new System.Drawing.Size(63, 0);
            cb_asset.Name = "cb_asset";
            cb_asset.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            cb_asset.Size = new System.Drawing.Size(315, 44);
            cb_asset.SymbolSize = 24;
            cb_asset.TabIndex = 43;
            cb_asset.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            cb_asset.Watermark = "";
            // 
            // MarketPlacePage
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1445, 845);
            Controls.Add(cb_asset);
            Controls.Add(lb_asset);
            Controls.Add(btn_publish);
            Controls.Add(lb_mark);
            Controls.Add(lb_contact);
            Controls.Add(rtb_remarks);
            Controls.Add(rtb_contact);
            Controls.Add(uiLine1);
            Controls.Add(bt_refresh);
            Controls.Add(L1);
            Controls.Add(pn_pairs);
            Name = "MarketPlacePage";
            Text = "PlazaForm";
            Initialize += OrderHistory_Initialize;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Sunny.UI.UIButton uiButton1;
        private Sunny.UI.UIButton uiButton2;
        private Sunny.UI.UIButton uiButton3;
        private Sunny.UI.UIButton uiButton4;
        private Sunny.UI.UIButton uiButton5;
        private Sunny.UI.UIButton uiButton6;
        private Sunny.UI.UILine L1;
        private Sunny.UI.UIFlowLayoutPanel pn_pairs;
        private Sunny.UI.UIButton bt_refresh;
        private Sunny.UI.UILine uiLine1;
        private System.Windows.Forms.RichTextBox rtb_remarks;
        private System.Windows.Forms.RichTextBox rtb_contact;
        private Sunny.UI.UILabel lb_contact;
        private Sunny.UI.UILabel lb_mark;
        private Sunny.UI.UIButton btn_publish;
        private Sunny.UI.UILabel lb_asset;
        private Sunny.UI.UIComboBox cb_asset;
    }
}