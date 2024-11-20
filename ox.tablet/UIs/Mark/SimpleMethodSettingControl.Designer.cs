using Sunny.UI;
namespace OX.Tablet.UIs.MarkSix
{
    partial class SimpleMethodSettingControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            lb_method = new UILabel();
            nd_odd = new System.Windows.Forms.NumericUpDown();
            darkLabel2 = new UILabel();
            darkLabel1 = new UILabel();
            nd_fee = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)nd_odd).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nd_fee).BeginInit();
            SuspendLayout();
            // 
            // lb_method
            // 
            lb_method.AutoSize = true;
            lb_method.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_method.Location = new System.Drawing.Point(3, 10);
            lb_method.Name = "lb_method";
            lb_method.Size = new System.Drawing.Size(106, 24);
            lb_method.TabIndex = 1;
            lb_method.Text = "darkLabel1";
            // 
            // nd_odd
            // 
            nd_odd.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            nd_odd.Location = new System.Drawing.Point(191, 8);
            nd_odd.Name = "nd_odd";
            nd_odd.Size = new System.Drawing.Size(126, 30);
            nd_odd.TabIndex = 4;
            nd_odd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // darkLabel2
            // 
            darkLabel2.AutoSize = true;
            darkLabel2.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            darkLabel2.Location = new System.Drawing.Point(324, 11);
            darkLabel2.Name = "darkLabel2";
            darkLabel2.Size = new System.Drawing.Size(26, 24);
            darkLabel2.TabIndex = 6;
            darkLabel2.Text = "%";
            // 
            // darkLabel1
            // 
            darkLabel1.AutoSize = true;
            darkLabel1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            darkLabel1.Location = new System.Drawing.Point(500, 11);
            darkLabel1.Name = "darkLabel1";
            darkLabel1.Size = new System.Drawing.Size(26, 24);
            darkLabel1.TabIndex = 8;
            darkLabel1.Text = "%";
            // 
            // nd_fee
            // 
            nd_fee.Location = new System.Drawing.Point(415, 8);
            nd_fee.Name = "nd_fee";
            nd_fee.Size = new System.Drawing.Size(78, 30);
            nd_fee.TabIndex = 7;
            nd_fee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FullMethodSettingControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(43, 43, 43);
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            Controls.Add(darkLabel1);
            Controls.Add(nd_fee);
            Controls.Add(darkLabel2);
            Controls.Add(nd_odd);
            Controls.Add(lb_method);
            Name = "FullMethodSettingControl";
            Size = new System.Drawing.Size(534, 47);
            ((System.ComponentModel.ISupportInitialize)nd_odd).EndInit();
            ((System.ComponentModel.ISupportInitialize)nd_fee).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private UILabel lb_method;
        private System.Windows.Forms.NumericUpDown nd_odd;
        private UILabel darkLabel2;
        private UILabel darkLabel1;
        private System.Windows.Forms.NumericUpDown nd_fee;
    }
}
