namespace OX.Tablet
{
    partial class NoticeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoticeForm));
            bt_expand = new Sunny.UI.UIButton();
            lb_msg = new Sunny.UI.UIListBox();
            SuspendLayout();
            // 
            // bt_expand
            // 
            bt_expand.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            bt_expand.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_expand.Location = new System.Drawing.Point(156, 12);
            bt_expand.MinimumSize = new System.Drawing.Size(1, 1);
            bt_expand.Name = "bt_expand";
            bt_expand.Radius = 50;
            bt_expand.Size = new System.Drawing.Size(150, 52);
            bt_expand.TabIndex = 0;
            bt_expand.Text = "uiButton1";
            bt_expand.Click += bt_expand_Click;
            // 
            // lb_msg
            // 
            lb_msg.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lb_msg.FillColor = System.Drawing.Color.FromArgb(60, 63, 65);
            lb_msg.FillColor2 = System.Drawing.Color.FromArgb(60, 63, 65);
            lb_msg.FillColorGradient = true;
            lb_msg.FillDisableColor = System.Drawing.Color.FromArgb(60, 63, 65);
            lb_msg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_msg.ForeColor = System.Drawing.Color.White;
            lb_msg.HoverColor = System.Drawing.Color.FromArgb(155, 200, 255);
            lb_msg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            lb_msg.ItemSelectForeColor = System.Drawing.Color.White;
            lb_msg.Location = new System.Drawing.Point(13, 74);
            lb_msg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            lb_msg.MinimumSize = new System.Drawing.Size(1, 1);
            lb_msg.Name = "lb_msg";
            lb_msg.Padding = new System.Windows.Forms.Padding(2);
            lb_msg.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            lb_msg.ShowText = false;
            lb_msg.Size = new System.Drawing.Size(445, 991);
            lb_msg.TabIndex = 1;
            lb_msg.Text = "uiListBox1";
            lb_msg.MouseDown += SyncForm_MouseDown;
            lb_msg.MouseUp += NoticeForm_MouseUp;
            lb_msg.MouseMove += NoticeForm_MouseMove;
            // 
            // NoticeForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            ClientSize = new System.Drawing.Size(471, 1079);
            Controls.Add(lb_msg);
            Controls.Add(bt_expand);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            Name = "NoticeForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "O X";
            Load += SyncForm_Load;
            MouseDown += SyncForm_MouseDown;
            MouseMove += NoticeForm_MouseMove;
            MouseUp += NoticeForm_MouseUp;
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIButton bt_expand;
        private Sunny.UI.UIListBox lb_msg;
    }
}