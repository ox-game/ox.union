using Sunny.UI;
namespace OX.Tablet
{
    partial class NewDirectSaleOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewDirectSaleOrder));
            lb_AssetName = new UILabel();
            lb_amount = new UILabel();
            btnOK = new UIButton();
            btnCancel = new UIButton();
            tb_amount = new UINumPadTextBox();
            lb_address = new UILabel();
            lb_balance = new UILabel();
            cb_delivery_expire = new UIComboBox();
            lb_delivery_expire = new UILabel();
            lb_time = new UILabel();
            uiLine1 = new UILine();
            SuspendLayout();
            // 
            // lb_AssetName
            // 
            resources.ApplyResources(lb_AssetName, "lb_AssetName");
            lb_AssetName.ForeColor = System.Drawing.Color.Black;
            lb_AssetName.Name = "lb_AssetName";
            // 
            // lb_amount
            // 
            resources.ApplyResources(lb_amount, "lb_amount");
            lb_amount.ForeColor = System.Drawing.Color.Black;
            lb_amount.Name = "lb_amount";
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
            // tb_amount
            // 
            tb_amount.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(tb_amount, "tb_amount");
            tb_amount.Maximum = 100000000D;
            tb_amount.Minimum = 10D;
            tb_amount.Name = "tb_amount";
            tb_amount.NumPadType = NumPadType.Integer;
            tb_amount.SymbolSize = 24;
            tb_amount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            tb_amount.Watermark = "";
            // 
            // lb_address
            // 
            resources.ApplyResources(lb_address, "lb_address");
            lb_address.ForeColor = System.Drawing.Color.Black;
            lb_address.Name = "lb_address";
            // 
            // lb_balance
            // 
            resources.ApplyResources(lb_balance, "lb_balance");
            lb_balance.ForeColor = System.Drawing.Color.Black;
            lb_balance.Name = "lb_balance";
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
            // lb_time
            // 
            resources.ApplyResources(lb_time, "lb_time");
            lb_time.ForeColor = System.Drawing.Color.Black;
            lb_time.Name = "lb_time";
            // 
            // uiLine1
            // 
            uiLine1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(uiLine1, "uiLine1");
            uiLine1.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            uiLine1.Name = "uiLine1";
            // 
            // NewDirectSaleOrder
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            Controls.Add(uiLine1);
            Controls.Add(lb_time);
            Controls.Add(lb_delivery_expire);
            Controls.Add(cb_delivery_expire);
            Controls.Add(lb_balance);
            Controls.Add(lb_address);
            Controls.Add(tb_amount);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(lb_amount);
            Controls.Add(lb_AssetName);
            Name = "NewDirectSaleOrder";
            ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 528, 290);
            FormClosing += ClaimForm_FormClosing;
            Load += ClaimForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private UILabel lb_AssetName;
        private UILabel lb_amount;
        private UIButton btnOK;
        private UIButton btnCancel;
        private UINumPadTextBox tb_amount;
        private System.Windows.Forms.RichTextBox rtb_remarks;
        private UILabel lb_address;
        private UILabel lb_balance;
        private UIComboBox cb_delivery_expire;
        private UILabel lb_delivery_expire;
        private UILabel lb_time;
        private UILine uiLine1;
    }
}