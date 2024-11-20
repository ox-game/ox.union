namespace OX.Tablet.UIs.MarkSix
{
    partial class BuryBetView
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
            pn_pairs = new System.Windows.Forms.FlowLayoutPanel();
            bt_refresh = new Sunny.UI.UIButton();
            bt_do_bury = new Sunny.UI.UIButton();
            L2 = new Sunny.UI.UILine();
            tb_plainCode = new Sunny.UI.UINumPadTextBox();
            lb_plainCode = new Sunny.UI.UILabel();
            lb_cipherCode = new Sunny.UI.UILabel();
            tb_cipherCode = new Sunny.UI.UINumPadTextBox();
            tb_balance = new Sunny.UI.UITextBox();
            lb_balance = new Sunny.UI.UILabel();
            lb_amount = new Sunny.UI.UILabel();
            tb_amount = new Sunny.UI.UINumPadTextBox();
            SuspendLayout();
            // 
            // L1
            // 
            L1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            L1.BackColor = System.Drawing.Color.Transparent;
            L1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            L1.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            L1.Location = new System.Drawing.Point(12, 257);
            L1.MinimumSize = new System.Drawing.Size(1, 1);
            L1.Name = "L1";
            L1.Size = new System.Drawing.Size(1206, 26);
            L1.TabIndex = 15;
            // 
            // pn_pairs
            // 
            pn_pairs.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pn_pairs.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            pn_pairs.Location = new System.Drawing.Point(12, 314);
            pn_pairs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pn_pairs.MinimumSize = new System.Drawing.Size(1, 1);
            pn_pairs.Name = "pn_pairs";
            pn_pairs.Padding = new System.Windows.Forms.Padding(2, 10, 2, 2);
            pn_pairs.RightToLeft = System.Windows.Forms.RightToLeft.No;
            pn_pairs.Size = new System.Drawing.Size(1419, 512);
            pn_pairs.TabIndex = 14;
            pn_pairs.Text = "uiFlowLayoutPanel1";
            // 
            // bt_refresh
            // 
            bt_refresh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            bt_refresh.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_refresh.Location = new System.Drawing.Point(1235, 243);
            bt_refresh.MinimumSize = new System.Drawing.Size(1, 1);
            bt_refresh.Name = "bt_refresh";
            bt_refresh.Size = new System.Drawing.Size(196, 52);
            bt_refresh.TabIndex = 4;
            bt_refresh.Text = "uiButton1";
            bt_refresh.Click += bt_refresh_Click;
            // 
            // bt_do_bury
            // 
            bt_do_bury.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_do_bury.Location = new System.Drawing.Point(1051, 112);
            bt_do_bury.MinimumSize = new System.Drawing.Size(1, 1);
            bt_do_bury.Name = "bt_do_bury";
            bt_do_bury.Radius = 50;
            bt_do_bury.Size = new System.Drawing.Size(196, 52);
            bt_do_bury.TabIndex = 16;
            bt_do_bury.Text = "uiButton1";
            bt_do_bury.Click += bt_do_bury_Click;
            // 
            // L2
            // 
            L2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            L2.BackColor = System.Drawing.Color.Transparent;
            L2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            L2.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            L2.Location = new System.Drawing.Point(12, 21);
            L2.MinimumSize = new System.Drawing.Size(1, 1);
            L2.Name = "L2";
            L2.Size = new System.Drawing.Size(1419, 26);
            L2.TabIndex = 17;
            // 
            // tb_plainCode
            // 
            tb_plainCode.FillColor = System.Drawing.Color.White;
            tb_plainCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_plainCode.Location = new System.Drawing.Point(209, 76);
            tb_plainCode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_plainCode.Maximum = 255D;
            tb_plainCode.Minimum = 0D;
            tb_plainCode.MinimumSize = new System.Drawing.Size(63, 0);
            tb_plainCode.Name = "tb_plainCode";
            tb_plainCode.NumPadType = Sunny.UI.NumPadType.Integer;
            tb_plainCode.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            tb_plainCode.Size = new System.Drawing.Size(206, 44);
            tb_plainCode.SymbolSize = 24;
            tb_plainCode.TabIndex = 18;
            tb_plainCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_plainCode.Watermark = "";
            // 
            // lb_plainCode
            // 
            lb_plainCode.AutoSize = true;
            lb_plainCode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_plainCode.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_plainCode.Location = new System.Drawing.Point(30, 79);
            lb_plainCode.Name = "lb_plainCode";
            lb_plainCode.Size = new System.Drawing.Size(62, 31);
            lb_plainCode.TabIndex = 21;
            lb_plainCode.Text = "住址";
            lb_plainCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_cipherCode
            // 
            lb_cipherCode.AutoSize = true;
            lb_cipherCode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_cipherCode.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_cipherCode.Location = new System.Drawing.Point(30, 165);
            lb_cipherCode.Name = "lb_cipherCode";
            lb_cipherCode.Size = new System.Drawing.Size(62, 31);
            lb_cipherCode.TabIndex = 23;
            lb_cipherCode.Text = "住址";
            lb_cipherCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_cipherCode
            // 
            tb_cipherCode.FillColor = System.Drawing.Color.White;
            tb_cipherCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_cipherCode.Location = new System.Drawing.Point(209, 162);
            tb_cipherCode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_cipherCode.Maximum = 255D;
            tb_cipherCode.Minimum = 0D;
            tb_cipherCode.MinimumSize = new System.Drawing.Size(63, 0);
            tb_cipherCode.Name = "tb_cipherCode";
            tb_cipherCode.NumPadType = Sunny.UI.NumPadType.Integer;
            tb_cipherCode.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            tb_cipherCode.Size = new System.Drawing.Size(206, 44);
            tb_cipherCode.SymbolSize = 24;
            tb_cipherCode.TabIndex = 22;
            tb_cipherCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_cipherCode.Watermark = "";
            // 
            // tb_balance
            // 
            tb_balance.Cursor = System.Windows.Forms.Cursors.IBeam;
            tb_balance.EnterAsTab = true;
            tb_balance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_balance.Location = new System.Drawing.Point(669, 76);
            tb_balance.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_balance.MinimumSize = new System.Drawing.Size(1, 16);
            tb_balance.Name = "tb_balance";
            tb_balance.Padding = new System.Windows.Forms.Padding(5);
            tb_balance.ReadOnly = true;
            tb_balance.ShowText = false;
            tb_balance.Size = new System.Drawing.Size(291, 43);
            tb_balance.TabIndex = 24;
            tb_balance.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_balance.Watermark = "";
            // 
            // lb_balance
            // 
            lb_balance.AutoSize = true;
            lb_balance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_balance.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_balance.Location = new System.Drawing.Point(480, 81);
            lb_balance.Name = "lb_balance";
            lb_balance.Size = new System.Drawing.Size(62, 31);
            lb_balance.TabIndex = 25;
            lb_balance.Text = "姓名";
            lb_balance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_amount
            // 
            lb_amount.AutoSize = true;
            lb_amount.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_amount.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_amount.Location = new System.Drawing.Point(475, 165);
            lb_amount.Name = "lb_amount";
            lb_amount.Size = new System.Drawing.Size(62, 31);
            lb_amount.TabIndex = 27;
            lb_amount.Text = "住址";
            lb_amount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_amount
            // 
            tb_amount.FillColor = System.Drawing.Color.White;
            tb_amount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_amount.Location = new System.Drawing.Point(669, 162);
            tb_amount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_amount.Maximum = 100000D;
            tb_amount.Minimum = 0D;
            tb_amount.MinimumSize = new System.Drawing.Size(63, 0);
            tb_amount.Name = "tb_amount";
            tb_amount.NumPadType = Sunny.UI.NumPadType.Integer;
            tb_amount.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            tb_amount.Size = new System.Drawing.Size(291, 44);
            tb_amount.SymbolSize = 24;
            tb_amount.TabIndex = 26;
            tb_amount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_amount.Watermark = "";
            // 
            // BuryBetView
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1445, 845);
            Controls.Add(lb_amount);
            Controls.Add(tb_amount);
            Controls.Add(tb_balance);
            Controls.Add(lb_balance);
            Controls.Add(lb_cipherCode);
            Controls.Add(tb_cipherCode);
            Controls.Add(lb_plainCode);
            Controls.Add(tb_plainCode);
            Controls.Add(L2);
            Controls.Add(bt_do_bury);
            Controls.Add(bt_refresh);
            Controls.Add(L1);
            Controls.Add(pn_pairs);
            Name = "BuryBetView";
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
        private System.Windows.Forms. FlowLayoutPanel pn_pairs;
        private Sunny.UI.UIButton bt_refresh;
        private Sunny.UI.UIButton bt_do_bury;
        private Sunny.UI.UILine L2;
        private Sunny.UI.UINumPadTextBox tb_plainCode;
        private Sunny.UI.UILabel lb_plainCode;
        private Sunny.UI.UILabel lb_cipherCode;
        private Sunny.UI.UINumPadTextBox tb_cipherCode;
        private Sunny.UI.UITextBox tb_balance;
        private Sunny.UI.UILabel lb_balance;
        private Sunny.UI.UILabel lb_amount;
        private Sunny.UI.UINumPadTextBox tb_amount;
    }
}