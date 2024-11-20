namespace OX.Tablet.FlashMessages
{
    partial class SetSemanticSubPage
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
            tb_msg = new Sunny.UI.UIRichTextBox();
            tb_name = new Sunny.UI.UITextBox();
            lb_name = new Sunny.UI.UILabel();
            SuspendLayout();
            // 
            // bt_refresh
            // 
            bt_refresh.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_refresh.Location = new System.Drawing.Point(32, 203);
            bt_refresh.MinimumSize = new System.Drawing.Size(1, 1);
            bt_refresh.Name = "bt_refresh";
            bt_refresh.Size = new System.Drawing.Size(287, 52);
            bt_refresh.TabIndex = 16;
            bt_refresh.Text = "uiButton1";
            bt_refresh.Click += bt_refresh_Click;
            // 
            // tb_msg
            // 
            tb_msg.FillColor = System.Drawing.Color.White;
            tb_msg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_msg.Location = new System.Drawing.Point(32, 315);
            tb_msg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_msg.MinimumSize = new System.Drawing.Size(1, 1);
            tb_msg.Name = "tb_msg";
            tb_msg.Padding = new System.Windows.Forms.Padding(2);
            tb_msg.ReadOnly = true;
            tb_msg.ShowText = false;
            tb_msg.Size = new System.Drawing.Size(1057, 221);
            tb_msg.TabIndex = 17;
            tb_msg.Text = "uiRichTextBox1";
            tb_msg.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_name
            // 
            tb_name.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tb_name.Location = new System.Drawing.Point(32, 105);
            tb_name.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tb_name.MinimumSize = new System.Drawing.Size(1, 16);
            tb_name.Name = "tb_name";
            tb_name.Padding = new System.Windows.Forms.Padding(5);
            tb_name.ShowText = false;
            tb_name.Size = new System.Drawing.Size(287, 44);
            tb_name.TabIndex = 18;
            tb_name.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_name.Watermark = "";
            tb_name.TextChanged += tb_name_TextChanged;
            // 
            // lb_name
            // 
            lb_name.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_name.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_name.Location = new System.Drawing.Point(32, 39);
            lb_name.Name = "lb_name";
            lb_name.Size = new System.Drawing.Size(150, 34);
            lb_name.TabIndex = 19;
            lb_name.Text = "uiLabel1";
            // 
            // SetSemanticSubPage
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1445, 845);
            Controls.Add(lb_name);
            Controls.Add(tb_name);
            Controls.Add(tb_msg);
            Controls.Add(bt_refresh);
            Name = "SetSemanticSubPage";
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
        private Sunny.UI.UIRichTextBox tb_msg;
        private Sunny.UI.UITextBox tb_name;
        private Sunny.UI.UILabel lb_name;
    }
}