namespace OX.WebPort
{
    partial class ShellForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShellForm));
            bt_start = new Button();
            bt_stop = new Button();
            lb_status = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            lb_index = new Label();
            lb_nodes = new Label();
            bt_rebuild = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // bt_start
            // 
            bt_start.Location = new Point(552, 10);
            bt_start.Name = "bt_start";
            bt_start.Size = new Size(112, 34);
            bt_start.TabIndex = 1;
            bt_start.Text = "Start";
            bt_start.UseVisualStyleBackColor = true;
            bt_start.Click += bt_start_Click;
            // 
            // bt_stop
            // 
            bt_stop.Location = new Point(552, 59);
            bt_stop.Name = "bt_stop";
            bt_stop.Size = new Size(112, 34);
            bt_stop.TabIndex = 2;
            bt_stop.Text = "Stop";
            bt_stop.UseVisualStyleBackColor = true;
            bt_stop.Click += bt_stop_Click;
            // 
            // lb_status
            // 
            lb_status.AutoSize = true;
            lb_status.Location = new Point(21, 105);
            lb_status.Name = "lb_status";
            lb_status.Size = new Size(63, 24);
            lb_status.TabIndex = 3;
            lb_status.Text = "label1";
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // lb_index
            // 
            lb_index.AutoSize = true;
            lb_index.Location = new Point(21, 61);
            lb_index.Name = "lb_index";
            lb_index.Size = new Size(63, 24);
            lb_index.TabIndex = 4;
            lb_index.Text = "label1";
            // 
            // lb_nodes
            // 
            lb_nodes.AutoSize = true;
            lb_nodes.Location = new Point(21, 20);
            lb_nodes.Name = "lb_nodes";
            lb_nodes.Size = new Size(63, 24);
            lb_nodes.TabIndex = 5;
            lb_nodes.Text = "label1";
            // 
            // bt_rebuild
            // 
            bt_rebuild.Location = new Point(552, 105);
            bt_rebuild.Name = "bt_rebuild";
            bt_rebuild.Size = new Size(112, 34);
            bt_rebuild.TabIndex = 6;
            bt_rebuild.Text = "Rebuild";
            bt_rebuild.UseVisualStyleBackColor = true;
            bt_rebuild.Click += bt_rebuild_Click;
            // 
            // button1
            // 
            button1.Location = new Point(378, 12);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 7;
            button1.Text = "test";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ShellForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(699, 151);
            ControlBox = false;
            Controls.Add(button1);
            Controls.Add(bt_rebuild);
            Controls.Add(lb_nodes);
            Controls.Add(lb_index);
            Controls.Add(lb_status);
            Controls.Add(bt_stop);
            Controls.Add(bt_start);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "ShellForm";
            Text = "MainForm";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bt_start;
        private Button bt_stop;
        private Label lb_status;
        private System.Windows.Forms.Timer timer1;
        private Label lb_index;
        private Label lb_nodes;
        private Button bt_rebuild;
        private Button button1;
    }
}