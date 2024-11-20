using Sunny.UI;
namespace OX.Tablet
{
    partial class NewMutualLockSellerTx
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewMutualLockSellerTx));
            lb_AssetName = new UILabel();
            lb_balance = new UILabel();
            btnOK = new UIButton();
            btnCancel = new UIButton();
            lb_notice = new UILabel();
            lb_amount = new UILabel();
            lb_buyer_address = new UILabel();
            L1 = new UILine();
            lb_remarks = new UILabel();
            cb_delivery_expire = new UIComboBox();
            lb_delivery_expire = new UILabel();
            SuspendLayout();
            // 
            // lb_AssetName
            // 
            resources.ApplyResources(lb_AssetName, "lb_AssetName");
            lb_AssetName.ForeColor = System.Drawing.Color.Black;
            lb_AssetName.Name = "lb_AssetName";
            // 
            // lb_balance
            // 
            resources.ApplyResources(lb_balance, "lb_balance");
            lb_balance.ForeColor = System.Drawing.Color.Black;
            lb_balance.Name = "lb_balance";
            // 
            // btnOK
            // 
            resources.ApplyResources(btnOK, "btnOK");
            btnOK.Name = "btnOK";
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            resources.ApplyResources(btnCancel, "btnCancel");
            btnCancel.Name = "btnCancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // lb_notice
            // 
            resources.ApplyResources(lb_notice, "lb_notice");
            lb_notice.ForeColor = System.Drawing.Color.Black;
            lb_notice.Name = "lb_notice";
            // 
            // lb_amount
            // 
            resources.ApplyResources(lb_amount, "lb_amount");
            lb_amount.ForeColor = System.Drawing.Color.Black;
            lb_amount.Name = "lb_amount";
            // 
            // lb_buyer_address
            // 
            resources.ApplyResources(lb_buyer_address, "lb_buyer_address");
            lb_buyer_address.ForeColor = System.Drawing.Color.Black;
            lb_buyer_address.Name = "lb_buyer_address";
            // 
            // L1
            // 
            resources.ApplyResources(L1, "L1");
            L1.BackColor = System.Drawing.Color.Transparent;
            L1.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            L1.Name = "L1";
            // 
            // lb_remarks
            // 
            resources.ApplyResources(lb_remarks, "lb_remarks");
            lb_remarks.ForeColor = System.Drawing.Color.Black;
            lb_remarks.Name = "lb_remarks";
            // 
            // cb_delivery_expire
            // 
            cb_delivery_expire.DataSource = null;
            cb_delivery_expire.DropDownStyle = UIDropDownStyle.DropDownList;
            cb_delivery_expire.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(cb_delivery_expire, "cb_delivery_expire");
            cb_delivery_expire.ItemHoverColor = System.Drawing.Color.FromArgb(155, 200, 255);
            cb_delivery_expire.ItemSelectForeColor = System.Drawing.Color.FromArgb(235, 243, 255);
            cb_delivery_expire.Name = "cb_delivery_expire";
            cb_delivery_expire.SymbolSize = 24;
            cb_delivery_expire.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            cb_delivery_expire.Watermark = "";
            // 
            // lb_delivery_expire
            // 
            resources.ApplyResources(lb_delivery_expire, "lb_delivery_expire");
            lb_delivery_expire.ForeColor = System.Drawing.Color.Black;
            lb_delivery_expire.Name = "lb_delivery_expire";
            // 
            // NewMutualLockSellerTx
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            Controls.Add(lb_delivery_expire);
            Controls.Add(cb_delivery_expire);
            Controls.Add(lb_remarks);
            Controls.Add(L1);
            Controls.Add(lb_buyer_address);
            Controls.Add(lb_amount);
            Controls.Add(lb_notice);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(lb_balance);
            Controls.Add(lb_AssetName);
            Name = "NewMutualLockSellerTx";
            ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 528, 290);
            FormClosing += ClaimForm_FormClosing;
            Load += ClaimForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private UILabel lb_AssetName;
        private UILabel lb_balance;
        private UIButton btnOK;
        private UIButton btnCancel;
        private UILabel lb_notice;
        private UILabel lb_amount;
        private UILabel lb_buyer_address;
        private UILine L1;
        private UILabel lb_remarks;
        private UIComboBox cb_delivery_expire;
        private UILabel lb_delivery_expire;
    }
}