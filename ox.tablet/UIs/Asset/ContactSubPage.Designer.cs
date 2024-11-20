namespace OX.Tablet.UIs.MarkSix
{
    partial class ContactSubPage
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
            pn_contacts = new Sunny.UI.UIFlowLayoutPanel();
            SuspendLayout();
            // 
            // pn_contacts
            // 
            pn_contacts.Dock = System.Windows.Forms.DockStyle.Fill;
            pn_contacts.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            pn_contacts.Location = new System.Drawing.Point(0, 0);
            pn_contacts.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pn_contacts.MinimumSize = new System.Drawing.Size(1, 1);
            pn_contacts.Name = "pn_contacts";
            pn_contacts.Padding = new System.Windows.Forms.Padding(2, 10, 2, 2);
            pn_contacts.RectColor = System.Drawing.Color.Transparent;
            pn_contacts.RightToLeft = System.Windows.Forms.RightToLeft.No;
            pn_contacts.ScrollBarHandleWidth = 100;
            pn_contacts.ShowText = false;
            pn_contacts.Size = new System.Drawing.Size(1445, 845);
            pn_contacts.TabIndex = 14;
            pn_contacts.Text = "uiFlowLayoutPanel1";
            pn_contacts.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ContactSubPage
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1445, 845);
            Controls.Add(pn_contacts);
            Name = "ContactSubPage";
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
        private Sunny.UI.UIFlowLayoutPanel pn_contacts;
    }
}