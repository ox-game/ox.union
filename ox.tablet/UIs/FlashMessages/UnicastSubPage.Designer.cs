namespace OX.Tablet.FlashMessages
{
    partial class UnicastSubPage
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
            CastPanel = new Sunny.UI.UIPanel();
            lb_contact = new Sunny.UI.UIListBox();
            bt_add_unicast = new Sunny.UI.UIButton();
            tb_name = new Sunny.UI.UITextBox();
            SuspendLayout();
            // 
            // CastPanel
            // 
            CastPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            CastPanel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            CastPanel.Location = new System.Drawing.Point(3, 1);
            CastPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            CastPanel.MinimumSize = new System.Drawing.Size(1, 1);
            CastPanel.Name = "CastPanel";
            CastPanel.Size = new System.Drawing.Size(1027, 842);
            CastPanel.TabIndex = 0;
            CastPanel.Text = null;
            CastPanel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_contact
            // 
            lb_contact.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            lb_contact.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_contact.HoverColor = System.Drawing.Color.FromArgb(155, 200, 255);
            lb_contact.ItemSelectForeColor = System.Drawing.Color.White;
            lb_contact.Location = new System.Drawing.Point(1038, 87);
            lb_contact.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            lb_contact.MinimumSize = new System.Drawing.Size(1, 1);
            lb_contact.Name = "lb_contact";
            lb_contact.Padding = new System.Windows.Forms.Padding(2);
            lb_contact.ShowText = false;
            lb_contact.Size = new System.Drawing.Size(401, 756);
            lb_contact.TabIndex = 0;
            lb_contact.Text = "uiListBox1";
            lb_contact.SelectedIndexChanged += lb_contact_SelectedIndexChanged;
            // 
            // bt_add_unicast
            // 
            bt_add_unicast.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            bt_add_unicast.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_add_unicast.Location = new System.Drawing.Point(1278, 18);
            bt_add_unicast.MinimumSize = new System.Drawing.Size(1, 1);
            bt_add_unicast.Name = "bt_add_unicast";
            bt_add_unicast.Size = new System.Drawing.Size(155, 52);
            bt_add_unicast.TabIndex = 17;
            bt_add_unicast.Text = "uiButton1";
            bt_add_unicast.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_add_unicast.Click += bt_add_unicast_Click;
            // 
            // tb_name
            // 
            tb_name.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            tb_name.DoubleValue = 8000D;
            tb_name.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_name.IntValue = 8000;
            tb_name.Location = new System.Drawing.Point(1038, 21);
            tb_name.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_name.Maximum = 1000000D;
            tb_name.Minimum = 8000D;
            tb_name.MinimumSize = new System.Drawing.Size(1, 16);
            tb_name.Name = "tb_name";
            tb_name.Padding = new System.Windows.Forms.Padding(5);
            tb_name.ShowText = false;
            tb_name.Size = new System.Drawing.Size(221, 44);
            tb_name.TabIndex = 0;
            tb_name.Text = "8000";
            tb_name.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_name.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            tb_name.Watermark = "";
            tb_name.TextChanged += tb_name_TextChanged;
            // 
            // UnicastSubPage
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1445, 845);
            Controls.Add(tb_name);
            Controls.Add(bt_add_unicast);
            Controls.Add(lb_contact);
            Controls.Add(CastPanel);
            Name = "UnicastSubPage";
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
        private Sunny.UI.UIPanel CastPanel;
        private Sunny.UI.UIListBox lb_contact;
        private Sunny.UI.UIButton bt_add_unicast;
        private Sunny.UI.UITextBox tb_name;
    }
}