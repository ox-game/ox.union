namespace OX.Tablet.UIs.MarkSix
{
    partial class MarkSixAwardSubPage
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
            AwardResult = new Mark.MarkSix.AwardResult();
            bt_copy_image = new Sunny.UI.UIButton();
            SuspendLayout();
            // 
            // AwardResult
            // 
            AwardResult.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            AwardResult.Location = new System.Drawing.Point(12, 23);
            AwardResult.Name = "AwardResult";
            AwardResult.Size = new System.Drawing.Size(587, 665);
            AwardResult.TabIndex = 0;
            // 
            // bt_copy_image
            // 
            bt_copy_image.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            bt_copy_image.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_copy_image.Location = new System.Drawing.Point(179, 717);
            bt_copy_image.MinimumSize = new System.Drawing.Size(1, 1);
            bt_copy_image.Name = "bt_copy_image";
            bt_copy_image.Size = new System.Drawing.Size(215, 52);
            bt_copy_image.TabIndex = 18;
            bt_copy_image.Text = "uiButton1";
            bt_copy_image.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_copy_image.Click += bt_copy_image_Click;
            // 
            // MarkSixAwardSubPage
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(611, 845);
            Controls.Add(bt_copy_image);
            Controls.Add(AwardResult);
            Name = "MarkSixAwardSubPage";
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
        private Mark.MarkSix.AwardResult AwardResult;
        private Sunny.UI.UIButton bt_copy_image;
    }
}