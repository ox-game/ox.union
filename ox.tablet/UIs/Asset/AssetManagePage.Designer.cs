namespace OX.Tablet
{
    partial class AssetManagePage
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
            tbc_sections = new Sunny.UI.UITabControl();
            SuspendLayout();
            // 
            // tbc_sections
            // 
            tbc_sections.Dock = System.Windows.Forms.DockStyle.Fill;
            tbc_sections.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            tbc_sections.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tbc_sections.ItemSize = new System.Drawing.Size(200, 40);
            tbc_sections.Location = new System.Drawing.Point(0, 0);
            tbc_sections.MainPage = "";
            tbc_sections.Name = "tbc_sections";
            tbc_sections.SelectedIndex = 0;
            tbc_sections.Size = new System.Drawing.Size(1531, 985);
            tbc_sections.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            tbc_sections.TabIndex = 1;
            tbc_sections.TabUnSelectedForeColor = System.Drawing.Color.FromArgb(240, 240, 240);
            tbc_sections.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // DirectSale
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tbc_sections);
            Name = "DirectSale";
            Size = new System.Drawing.Size(1531, 985);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UITabControl tbc_sections;
    }
}