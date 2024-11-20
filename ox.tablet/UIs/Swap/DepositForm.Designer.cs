using Sunny.UI;
namespace OX.Tablet
{
    partial class DepositForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DepositForm));
            btnCancel = new UIButton();
            lb_step1 = new UIMarkLabel();
            lb_step2 = new UIMarkLabel();
            L1 = new UILine();
            lb_wifi_ssid = new UILabel();
            lb_wifi_pwd = new UILabel();
            img_wifi = new System.Windows.Forms.PictureBox();
            img_wallet = new System.Windows.Forms.PictureBox();
            bt_copy_url = new UIButton();
            ((System.ComponentModel.ISupportInitialize)img_wifi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)img_wallet).BeginInit();
            SuspendLayout();
            // 
            // btnCancel
            // 
            resources.ApplyResources(btnCancel, "btnCancel");
            btnCancel.Name = "btnCancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // lb_step1
            // 
            resources.ApplyResources(lb_step1, "lb_step1");
            lb_step1.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_step1.Name = "lb_step1";
            // 
            // lb_step2
            // 
            resources.ApplyResources(lb_step2, "lb_step2");
            lb_step2.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            lb_step2.Name = "lb_step2";
            // 
            // L1
            // 
            resources.ApplyResources(L1, "L1");
            L1.BackColor = System.Drawing.Color.Transparent;
            L1.Direction = UILine.LineDirection.Vertical;
            L1.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            L1.Name = "L1";
            // 
            // lb_wifi_ssid
            // 
            resources.ApplyResources(lb_wifi_ssid, "lb_wifi_ssid");
            lb_wifi_ssid.ForeColor = System.Drawing.Color.Black;
            lb_wifi_ssid.Name = "lb_wifi_ssid";
            // 
            // lb_wifi_pwd
            // 
            resources.ApplyResources(lb_wifi_pwd, "lb_wifi_pwd");
            lb_wifi_pwd.ForeColor = System.Drawing.Color.Black;
            lb_wifi_pwd.Name = "lb_wifi_pwd";
            // 
            // img_wifi
            // 
            resources.ApplyResources(img_wifi, "img_wifi");
            img_wifi.Name = "img_wifi";
            img_wifi.TabStop = false;
            // 
            // img_wallet
            // 
            resources.ApplyResources(img_wallet, "img_wallet");
            img_wallet.Name = "img_wallet";
            img_wallet.TabStop = false;
            // 
            // bt_copy_url
            // 
            resources.ApplyResources(bt_copy_url, "bt_copy_url");
            bt_copy_url.Name = "bt_copy_url";
            bt_copy_url.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bt_copy_url.Click += bt_copy_url_Click;
            // 
            // DepositForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            ControlBox = false;
            Controls.Add(bt_copy_url);
            Controls.Add(img_wallet);
            Controls.Add(img_wifi);
            Controls.Add(lb_wifi_pwd);
            Controls.Add(lb_wifi_ssid);
            Controls.Add(L1);
            Controls.Add(lb_step2);
            Controls.Add(lb_step1);
            Controls.Add(btnCancel);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DepositForm";
            ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 528, 290);
            FormClosing += ClaimForm_FormClosing;
            Load += ClaimForm_Load;
            ((System.ComponentModel.ISupportInitialize)img_wifi).EndInit();
            ((System.ComponentModel.ISupportInitialize)img_wallet).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private UIButton btnCancel;
        private UIMarkLabel lb_step1;
        private UIMarkLabel lb_step2;
        private UILine L1;
        private UILabel lb_wifi_ssid;
        private UILabel lb_wifi_pwd;
        private System.Windows.Forms.PictureBox img_wifi;
        private System.Windows.Forms.PictureBox img_wallet;
        private UIButton bt_copy_url;
    }
}