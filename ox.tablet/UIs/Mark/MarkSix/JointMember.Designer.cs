namespace OX.Tablet.UIs.MarkSix
{
    partial class JointMember
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
            bt_member_recharge = new Sunny.UI.UIButton();
            pn_ports = new Sunny.UI.UIFlowLayoutPanel();
            L1 = new Sunny.UI.UILine();
            Box = new Sunny.UI.UISplitContainer();
            lb_deposit = new Sunny.UI.UILabel();
            bt_recharge = new Sunny.UI.UIButton();
            st_auto_cut = new Sunny.UI.UISwitch();
            bt_show_unionsetting = new Sunny.UI.UIButton();
            lb_due_member = new Sunny.UI.UILabel();
            lb_MemberId = new Sunny.UI.UILabel();
            slb_memberType = new Sunny.UI.UISymbolLabel();
            Box.BeginInit();
            Box.Panel1.SuspendLayout();
            Box.SuspendLayout();
            SuspendLayout();
            // 
            // bt_member_recharge
            // 
            bt_member_recharge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            bt_member_recharge.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_member_recharge.Location = new System.Drawing.Point(365, 172);
            bt_member_recharge.MinimumSize = new System.Drawing.Size(1, 1);
            bt_member_recharge.Name = "bt_member_recharge";
            bt_member_recharge.Size = new System.Drawing.Size(149, 52);
            bt_member_recharge.TabIndex = 9;
            bt_member_recharge.Text = "uiButton1";
            bt_member_recharge.Click += bt_new_banker_Click;
            // 
            // pn_ports
            // 
            pn_ports.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pn_ports.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            pn_ports.Location = new System.Drawing.Point(11, 272);
            pn_ports.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pn_ports.MinimumSize = new System.Drawing.Size(1, 1);
            pn_ports.Name = "pn_ports";
            pn_ports.Padding = new System.Windows.Forms.Padding(2, 10, 2, 2);
            pn_ports.RectColor = System.Drawing.Color.Transparent;
            pn_ports.RightToLeft = System.Windows.Forms.RightToLeft.No;
            pn_ports.ScrollBarHandleWidth = 100;
            pn_ports.ShowText = false;
            pn_ports.Size = new System.Drawing.Size(504, 778);
            pn_ports.TabIndex = 10;
            pn_ports.Text = "uiFlowLayoutPanel1";
            pn_ports.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1
            // 
            L1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            L1.BackColor = System.Drawing.Color.Transparent;
            L1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            L1.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            L1.Location = new System.Drawing.Point(11, 224);
            L1.MinimumSize = new System.Drawing.Size(1, 1);
            L1.Name = "L1";
            L1.Size = new System.Drawing.Size(503, 47);
            L1.TabIndex = 11;
            L1.Text = "uiLine1";
            // 
            // Box
            // 
            Box.Dock = System.Windows.Forms.DockStyle.Fill;
            Box.Location = new System.Drawing.Point(0, 0);
            Box.MinimumSize = new System.Drawing.Size(20, 20);
            Box.Name = "Box";
            // 
            // Box.Panel1
            // 
            Box.Panel1.Controls.Add(lb_deposit);
            Box.Panel1.Controls.Add(bt_recharge);
            Box.Panel1.Controls.Add(st_auto_cut);
            Box.Panel1.Controls.Add(pn_ports);
            Box.Panel1.Controls.Add(L1);
            Box.Panel1.Controls.Add(bt_show_unionsetting);
            Box.Panel1.Controls.Add(lb_due_member);
            Box.Panel1.Controls.Add(lb_MemberId);
            Box.Panel1.Controls.Add(bt_member_recharge);
            Box.Panel1.Controls.Add(slb_memberType);
            Box.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            Box.Panel1.Paint += Box_Panel1_Paint;
            // 
            // Box.Panel2
            // 
            Box.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            Box.Size = new System.Drawing.Size(1411, 1064);
            Box.SplitterDistance = 528;
            Box.SplitterWidth = 30;
            Box.TabIndex = 14;
            // 
            // lb_deposit
            // 
            lb_deposit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lb_deposit.AutoSize = true;
            lb_deposit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_deposit.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_deposit.Location = new System.Drawing.Point(321, 119);
            lb_deposit.Name = "lb_deposit";
            lb_deposit.Size = new System.Drawing.Size(0, 24);
            lb_deposit.TabIndex = 44;
            lb_deposit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_recharge
            // 
            bt_recharge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            bt_recharge.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_recharge.Location = new System.Drawing.Point(365, 105);
            bt_recharge.MinimumSize = new System.Drawing.Size(1, 1);
            bt_recharge.Name = "bt_recharge";
            bt_recharge.Size = new System.Drawing.Size(149, 52);
            bt_recharge.TabIndex = 43;
            bt_recharge.Text = "uiButton1";
            bt_recharge.Click += bt_recharge_Click;
            // 
            // st_auto_cut
            // 
            st_auto_cut.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            st_auto_cut.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            st_auto_cut.Location = new System.Drawing.Point(25, 177);
            st_auto_cut.MinimumSize = new System.Drawing.Size(1, 1);
            st_auto_cut.Name = "st_auto_cut";
            st_auto_cut.Size = new System.Drawing.Size(157, 44);
            st_auto_cut.SwitchShape = Sunny.UI.UISwitch.UISwitchShape.Square;
            st_auto_cut.TabIndex = 41;
            st_auto_cut.Text = "uiSwitch1";
            st_auto_cut.ValueChanged += st_auto_cut_ValueChanged;
            // 
            // bt_show_unionsetting
            // 
            bt_show_unionsetting.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            bt_show_unionsetting.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_show_unionsetting.Location = new System.Drawing.Point(200, 172);
            bt_show_unionsetting.MinimumSize = new System.Drawing.Size(1, 1);
            bt_show_unionsetting.Name = "bt_show_unionsetting";
            bt_show_unionsetting.Size = new System.Drawing.Size(149, 52);
            bt_show_unionsetting.TabIndex = 17;
            bt_show_unionsetting.Text = "uiButton1";
            bt_show_unionsetting.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_show_unionsetting.Click += bt_Agent_setting_Click;
            // 
            // lb_due_member
            // 
            lb_due_member.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lb_due_member.AutoSize = true;
            lb_due_member.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_due_member.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_due_member.Location = new System.Drawing.Point(485, 55);
            lb_due_member.Name = "lb_due_member";
            lb_due_member.RightToLeft = System.Windows.Forms.RightToLeft.No;
            lb_due_member.Size = new System.Drawing.Size(0, 24);
            lb_due_member.TabIndex = 40;
            // 
            // lb_MemberId
            // 
            lb_MemberId.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lb_MemberId.AutoSize = true;
            lb_MemberId.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_MemberId.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_MemberId.Location = new System.Drawing.Point(485, 9);
            lb_MemberId.Name = "lb_MemberId";
            lb_MemberId.Size = new System.Drawing.Size(0, 33);
            lb_MemberId.TabIndex = 39;
            // 
            // slb_memberType
            // 
            slb_memberType.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            slb_memberType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            slb_memberType.Location = new System.Drawing.Point(177, 55);
            slb_memberType.MinimumSize = new System.Drawing.Size(1, 1);
            slb_memberType.Name = "slb_memberType";
            slb_memberType.Size = new System.Drawing.Size(134, 28);
            slb_memberType.TabIndex = 37;
            slb_memberType.Text = "slb_port";
            // 
            // JointMember
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1411, 1064);
            Controls.Add(Box);
            Name = "JointMember";
            RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            Text = "PlazaForm";
            Box.Panel1.ResumeLayout(false);
            Box.Panel1.PerformLayout();
            Box.EndInit();
            Box.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Sunny.UI.UIButton bt_member_recharge;
        private Sunny.UI.UIFlowLayoutPanel pn_ports;
        private Sunny.UI.UILine L1;
        private Sunny.UI.UISplitContainer Box;
        private Sunny.UI.UIButton bt_show_unionsetting;
        private Sunny.UI.UILabel lb_due_member;
        private Sunny.UI.UILabel lb_MemberId;
        private Sunny.UI.UISymbolLabel slb_memberType;
        private Sunny.UI.UISwitch st_auto_cut;
        private Sunny.UI.UIButton bt_recharge;
        private Sunny.UI.UILabel lb_deposit;
    }
}