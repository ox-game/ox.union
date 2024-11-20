using Sunny.UI;
namespace OX.Tablet
{
    partial class SingleClaimOXC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SingleClaimOXC));
            lb_Available = new UILabel();
            lb_Unavailable = new UILabel();
            tb_Available = new UITextBox();
            tb_Unavailable = new UITextBox();
            btnOK = new UIButton();
            btnCancel = new UIButton();
            SuspendLayout();
            // 
            // lb_Available
            // 
            resources.ApplyResources(lb_Available, "lb_Available");
            lb_Available.ForeColor = System.Drawing.Color.Black;
            lb_Available.Name = "lb_Available";
            // 
            // lb_Unavailable
            // 
            resources.ApplyResources(lb_Unavailable, "lb_Unavailable");
            lb_Unavailable.ForeColor = System.Drawing.Color.Black;
            lb_Unavailable.Name = "lb_Unavailable";
            // 
            // tb_Available
            // 
            tb_Available.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            resources.ApplyResources(tb_Available, "tb_Available");
            tb_Available.ForeColor = System.Drawing.Color.Black;
            tb_Available.Name = "tb_Available";
            tb_Available.ReadOnly = true;
            tb_Available.ShowText = false;
            tb_Available.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_Available.Watermark = "";
            // 
            // tb_Unavailable
            // 
            tb_Unavailable.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            resources.ApplyResources(tb_Unavailable, "tb_Unavailable");
            tb_Unavailable.ForeColor = System.Drawing.Color.Black;
            tb_Unavailable.Name = "tb_Unavailable";
            tb_Unavailable.ReadOnly = true;
            tb_Unavailable.ShowText = false;
            tb_Unavailable.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_Unavailable.Watermark = "";
            // 
            // btnOK
            // 
            resources.ApplyResources(btnOK, "btnOK");
            btnOK.Name = "btnOK";
            btnOK.Click += btnOK_Click_1;
            // 
            // btnCancel
            // 
            resources.ApplyResources(btnCancel, "btnCancel");
            btnCancel.Name = "btnCancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // SingleClaimOXC
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(tb_Unavailable);
            Controls.Add(tb_Available);
            Controls.Add(lb_Unavailable);
            Controls.Add(lb_Available);
            Name = "SingleClaimOXC";
            ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 528, 290);
            FormClosing += ClaimForm_FormClosing;
            Load += ClaimForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private UILabel lb_Available;
        private UILabel lb_Unavailable;
        private UITextBox tb_Available;
        private UITextBox tb_Unavailable;
        private UIButton btnOK;
        private UIButton btnCancel;
    }
}