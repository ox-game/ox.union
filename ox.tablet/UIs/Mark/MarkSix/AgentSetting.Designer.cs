using Sunny.UI;
namespace OX.Tablet.UIs.MarkSix
{
    partial class AgentSetting
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
            lb_name = new UILabel();
            lb_odds = new UILabel();
            pfull = new System.Windows.Forms.FlowLayoutPanel();
            SuspendLayout();
            // 
            // lb_name
            // 
            lb_name.AutoSize = true;
            lb_name.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_name.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_name.Location = new System.Drawing.Point(37, 6);
            lb_name.Name = "lb_name";
            lb_name.Size = new System.Drawing.Size(130, 24);
            lb_name.TabIndex = 0;
            lb_name.Text = "darkLabel1";
            // 
            // lb_odds
            // 
            lb_odds.AutoSize = true;
            lb_odds.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_odds.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_odds.Location = new System.Drawing.Point(234, 6);
            lb_odds.Name = "lb_odds";
            lb_odds.Size = new System.Drawing.Size(130, 24);
            lb_odds.TabIndex = 1;
            lb_odds.Text = "darkLabel2";
            // 
            // pfull
            // 
            pfull.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pfull.Location = new System.Drawing.Point(6, 51);
            pfull.Name = "pfull";
            pfull.Size = new System.Drawing.Size(423, 657);
            pfull.TabIndex = 3;
            // 
            // AgentSetting
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(43, 43, 43);
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            Controls.Add(pfull);
            Controls.Add(lb_odds);
            Controls.Add(lb_name);
            Name = "AgentSetting";
            Size = new System.Drawing.Size(435, 711);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private UILabel lb_name;
        private UILabel lb_odds;
        private System.Windows.Forms.FlowLayoutPanel pfull;
    }
}
