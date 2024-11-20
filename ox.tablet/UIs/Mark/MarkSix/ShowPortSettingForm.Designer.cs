using Sunny.UI;
using System.Windows.Forms;
namespace OX.Tablet.UIs.MarkSix
{
    partial class ShowPortSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowPortSettingForm));
            panel = new Panel();
            bankerSetting1 = new AgentSetting();
            bt_close = new UIButton();
            darkLabel1 = new UILabel();
            panel.SuspendLayout();
            SuspendLayout();
            // 
            // panel
            // 
            panel.Controls.Add(bankerSetting1);
            panel.Controls.Add(bt_close);
            resources.ApplyResources(panel, "panel");
            panel.Name = "panel";
            panel.Paint += panel_Paint;
            // 
            // bankerSetting1
            // 
            bankerSetting1.BackColor = System.Drawing.Color.Gray;
            bankerSetting1.BorderStyle = BorderStyle.FixedSingle;
            resources.ApplyResources(bankerSetting1, "bankerSetting1");
            bankerSetting1.Name = "bankerSetting1";
            // 
            // bt_close
            // 
            resources.ApplyResources(bt_close, "bt_close");
            bt_close.Name = "bt_close";
            bt_close.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_close.Click += bt_close_Click;
            // 
            // darkLabel1
            // 
            resources.ApplyResources(darkLabel1, "darkLabel1");
            darkLabel1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            darkLabel1.Name = "darkLabel1";
            // 
            // ShowPortSettingForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            ControlBox = false;
            Controls.Add(panel);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ShowPortSettingForm";
            ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 1247, 878);
            FormClosing += ClaimForm_FormClosing;
            Load += ClaimForm_Load;
            panel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private UILabel darkLabel1;
        private UIButton bt_close;
        private AgentSetting bankerSetting1;
    }
}