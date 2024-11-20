namespace OX.Tablet.UIs.MarkSix
{
    partial class CommonPage
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
            bt_lock = new Sunny.UI.UIButton();
            bt_exit = new Sunny.UI.UIButton();
            bt_rebuild = new Sunny.UI.UIButton();
            bt_switch = new Sunny.UI.UIButton();
            bt_collapse = new Sunny.UI.UIButton();
            SuspendLayout();
            // 
            // L1
            // 
            L1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            L1.BackColor = System.Drawing.Color.Transparent;
            L1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            L1.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            L1.Location = new System.Drawing.Point(12, 370);
            L1.MinimumSize = new System.Drawing.Size(1, 1);
            L1.Name = "L1";
            L1.Size = new System.Drawing.Size(1421, 26);
            L1.TabIndex = 15;
            // 
            // bt_lock
            // 
            bt_lock.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_lock.Location = new System.Drawing.Point(115, 96);
            bt_lock.MinimumSize = new System.Drawing.Size(1, 1);
            bt_lock.Name = "bt_lock";
            bt_lock.Radius = 50;
            bt_lock.Size = new System.Drawing.Size(196, 52);
            bt_lock.TabIndex = 16;
            bt_lock.Text = "uiButton1";
            bt_lock.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_lock.Click += bt_lock_Click;
            // 
            // bt_exit
            // 
            bt_exit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_exit.Location = new System.Drawing.Point(115, 212);
            bt_exit.MinimumSize = new System.Drawing.Size(1, 1);
            bt_exit.Name = "bt_exit";
            bt_exit.Radius = 50;
            bt_exit.Size = new System.Drawing.Size(196, 52);
            bt_exit.TabIndex = 17;
            bt_exit.Text = "uiButton1";
            bt_exit.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_exit.Click += bt_exit_Click;
            // 
            // bt_rebuild
            // 
            bt_rebuild.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_rebuild.Location = new System.Drawing.Point(587, 96);
            bt_rebuild.MinimumSize = new System.Drawing.Size(1, 1);
            bt_rebuild.Name = "bt_rebuild";
            bt_rebuild.Radius = 50;
            bt_rebuild.Size = new System.Drawing.Size(196, 52);
            bt_rebuild.TabIndex = 18;
            bt_rebuild.Text = "uiButton1";
            bt_rebuild.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_rebuild.Click += bt_rebuild_Click;
            // 
            // bt_switch
            // 
            bt_switch.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_switch.Location = new System.Drawing.Point(587, 212);
            bt_switch.MinimumSize = new System.Drawing.Size(1, 1);
            bt_switch.Name = "bt_switch";
            bt_switch.Radius = 50;
            bt_switch.Size = new System.Drawing.Size(196, 52);
            bt_switch.TabIndex = 19;
            bt_switch.Text = "uiButton1";
            bt_switch.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_switch.Click += bt_switch_Click;
            // 
            // bt_collapse
            // 
            bt_collapse.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_collapse.Location = new System.Drawing.Point(265, 460);
            bt_collapse.MinimumSize = new System.Drawing.Size(1, 1);
            bt_collapse.Name = "bt_collapse";
            bt_collapse.Size = new System.Drawing.Size(294, 112);
            bt_collapse.TabIndex = 22;
            bt_collapse.Text = "uiButton1";
            bt_collapse.Click += bt_collapse_Click;
            // 
            // CommonPage
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1445, 845);
            Controls.Add(bt_collapse);
            Controls.Add(bt_lock);
            Controls.Add(bt_exit);
            Controls.Add(bt_rebuild);
            Controls.Add(bt_switch);
            Controls.Add(L1);
            Name = "CommonPage";
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
        private Sunny.UI.UILine L1;
        private Sunny.UI.UIButton bt_lock;
        private Sunny.UI.UIButton bt_exit;
        private Sunny.UI.UIButton bt_rebuild;
        private Sunny.UI.UIButton bt_switch;
        private Sunny.UI.UIButton bt_collapse;
    }
}