namespace OX.Tablet.UIs.MarkSix
{
    partial class OutGoldPage
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
            lb_amount = new Sunny.UI.UILabel();
            tb_balance = new Sunny.UI.UITextBox();
            lb_usdt_balance = new Sunny.UI.UILabel();
            rbg_ratio = new Sunny.UI.UIRadioButtonGroup();
            st_state = new Sunny.UI.UISwitch();
            btn_do_sale = new Sunny.UI.UIButton();
            lb_pool_address = new Sunny.UI.UILabel();
            lb_pool_balance = new Sunny.UI.UILabel();
            tb_amount = new Sunny.UI.UINumPadTextBox();
            uiTrackBar1 = new Sunny.UI.UITrackBar();
            uiRuler1 = new Sunny.UI.UIRuler();
            SuspendLayout();
            // 
            // lb_amount
            // 
            lb_amount.AutoSize = true;
            lb_amount.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_amount.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_amount.Location = new System.Drawing.Point(49, 154);
            lb_amount.Name = "lb_amount";
            lb_amount.Size = new System.Drawing.Size(62, 31);
            lb_amount.TabIndex = 19;
            lb_amount.Text = "住址";
            lb_amount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_balance
            // 
            tb_balance.Cursor = System.Windows.Forms.Cursors.IBeam;
            tb_balance.EnterAsTab = true;
            tb_balance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_balance.Location = new System.Drawing.Point(841, 148);
            tb_balance.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_balance.MinimumSize = new System.Drawing.Size(1, 16);
            tb_balance.Name = "tb_balance";
            tb_balance.Padding = new System.Windows.Forms.Padding(5);
            tb_balance.ReadOnly = true;
            tb_balance.ShowText = false;
            tb_balance.Size = new System.Drawing.Size(256, 43);
            tb_balance.TabIndex = 16;
            tb_balance.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_balance.Watermark = "";
            // 
            // lb_usdt_balance
            // 
            lb_usdt_balance.AutoSize = true;
            lb_usdt_balance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_usdt_balance.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_usdt_balance.Location = new System.Drawing.Point(609, 152);
            lb_usdt_balance.Name = "lb_usdt_balance";
            lb_usdt_balance.Size = new System.Drawing.Size(62, 31);
            lb_usdt_balance.TabIndex = 17;
            lb_usdt_balance.Text = "姓名";
            lb_usdt_balance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rbg_ratio
            // 
            rbg_ratio.ColumnCount = 5;
            rbg_ratio.ColumnInterval = 50;
            rbg_ratio.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            rbg_ratio.Items.AddRange(new object[] { "0", "1%", "2%", "3%", "4%", "5%", "6%", "7%", "8%", "9%", "10%", "11%", "12%", "13%", "14%" });
            rbg_ratio.ItemSize = new System.Drawing.Size(150, 30);
            rbg_ratio.Location = new System.Drawing.Point(52, 314);
            rbg_ratio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            rbg_ratio.MinimumSize = new System.Drawing.Size(1, 1);
            rbg_ratio.Name = "rbg_ratio";
            rbg_ratio.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            rbg_ratio.RowInterval = 30;
            rbg_ratio.Size = new System.Drawing.Size(1045, 211);
            rbg_ratio.TabIndex = 20;
            rbg_ratio.Text = "uiRadioButtonGroup1";
            rbg_ratio.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            rbg_ratio.ValueChanged += rbg_ratio_ValueChanged;
            // 
            // st_state
            // 
            st_state.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            st_state.Location = new System.Drawing.Point(52, 559);
            st_state.MinimumSize = new System.Drawing.Size(1, 1);
            st_state.Name = "st_state";
            st_state.Size = new System.Drawing.Size(161, 44);
            st_state.SwitchShape = Sunny.UI.UISwitch.UISwitchShape.Square;
            st_state.TabIndex = 23;
            st_state.Text = "uiSwitch1";
            // 
            // btn_do_sale
            // 
            btn_do_sale.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_do_sale.Location = new System.Drawing.Point(49, 653);
            btn_do_sale.MinimumSize = new System.Drawing.Size(1, 1);
            btn_do_sale.Name = "btn_do_sale";
            btn_do_sale.Size = new System.Drawing.Size(161, 52);
            btn_do_sale.TabIndex = 24;
            btn_do_sale.Text = "uiButton1";
            btn_do_sale.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_do_sale.Click += btn_do_sale_Click;
            // 
            // lb_pool_address
            // 
            lb_pool_address.AutoSize = true;
            lb_pool_address.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_pool_address.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_pool_address.Location = new System.Drawing.Point(52, 24);
            lb_pool_address.Name = "lb_pool_address";
            lb_pool_address.Size = new System.Drawing.Size(62, 31);
            lb_pool_address.TabIndex = 25;
            lb_pool_address.Text = "住址";
            lb_pool_address.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_pool_balance
            // 
            lb_pool_balance.AutoSize = true;
            lb_pool_balance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_pool_balance.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_pool_balance.Location = new System.Drawing.Point(52, 80);
            lb_pool_balance.Name = "lb_pool_balance";
            lb_pool_balance.Size = new System.Drawing.Size(62, 31);
            lb_pool_balance.TabIndex = 26;
            lb_pool_balance.Text = "住址";
            lb_pool_balance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_amount
            // 
            tb_amount.FillColor = System.Drawing.Color.White;
            tb_amount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_amount.Location = new System.Drawing.Point(272, 147);
            tb_amount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_amount.Maximum = 10000000D;
            tb_amount.Minimum = 0D;
            tb_amount.MinimumSize = new System.Drawing.Size(63, 0);
            tb_amount.Name = "tb_amount";
            tb_amount.NumPadType = Sunny.UI.NumPadType.Integer;
            tb_amount.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            tb_amount.Size = new System.Drawing.Size(276, 44);
            tb_amount.SymbolSize = 24;
            tb_amount.TabIndex = 28;
            tb_amount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_amount.Watermark = "";
            // 
            // uiTrackBar1
            // 
            uiTrackBar1.BarSize = 50;
            uiTrackBar1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiTrackBar1.Location = new System.Drawing.Point(52, 199);
            uiTrackBar1.MinimumSize = new System.Drawing.Size(1, 1);
            uiTrackBar1.Name = "uiTrackBar1";
            uiTrackBar1.Size = new System.Drawing.Size(1045, 44);
            uiTrackBar1.TabIndex = 31;
            uiTrackBar1.Text = "uiTrackBar1";
            uiTrackBar1.ValueChanged += uiTrackBar1_ValueChanged;
            // 
            // uiRuler1
            // 
            uiRuler1.BackColor = System.Drawing.Color.Transparent;
            uiRuler1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiRuler1.Location = new System.Drawing.Point(49, 243);
            uiRuler1.MinimumSize = new System.Drawing.Size(1, 1);
            uiRuler1.Name = "uiRuler1";
            uiRuler1.Size = new System.Drawing.Size(1045, 44);
            uiRuler1.TabIndex = 30;
            uiRuler1.Text = "uiRuler1";
            // 
            // OutGoldPage
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1445, 845);
            Controls.Add(uiTrackBar1);
            Controls.Add(uiRuler1);
            Controls.Add(tb_amount);
            Controls.Add(lb_pool_balance);
            Controls.Add(lb_pool_address);
            Controls.Add(btn_do_sale);
            Controls.Add(st_state);
            Controls.Add(rbg_ratio);
            Controls.Add(lb_amount);
            Controls.Add(tb_balance);
            Controls.Add(lb_usdt_balance);
            Name = "OutGoldPage";
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
        private Sunny.UI.UILabel lb_amount;
        private Sunny.UI.UITextBox tb_balance;
        private Sunny.UI.UILabel lb_usdt_balance;
        private Sunny.UI.UIRadioButtonGroup rbg_ratio;
        private Sunny.UI.UISwitch st_state;
        private Sunny.UI.UIButton btn_do_sale;
        private Sunny.UI.UILabel lb_pool_address;
        private Sunny.UI.UILabel lb_pool_balance;
        private Sunny.UI.UINumPadTextBox tb_amount;
        private Sunny.UI.UITrackBar uiTrackBar1;
        private Sunny.UI.UIRuler uiRuler1;
    }
}