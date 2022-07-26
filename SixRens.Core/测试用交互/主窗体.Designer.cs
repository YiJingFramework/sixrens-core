namespace 测试用交互
{
    partial class 主窗体
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.占例ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.起课ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.打开占例ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存占例ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.导出占例ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插件预设ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插件包ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.占例ToolStripMenuItem,
            this.配置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(760, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 占例ToolStripMenuItem
            // 
            this.占例ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.起课ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.打开占例ToolStripMenuItem,
            this.保存占例ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.导出占例ToolStripMenuItem});
            this.占例ToolStripMenuItem.Name = "占例ToolStripMenuItem";
            this.占例ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.占例ToolStripMenuItem.Text = "占例";
            // 
            // 起课ToolStripMenuItem
            // 
            this.起课ToolStripMenuItem.Name = "起课ToolStripMenuItem";
            this.起课ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.起课ToolStripMenuItem.Text = "起课";
            this.起课ToolStripMenuItem.Click += new System.EventHandler(this.起课ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(121, 6);
            // 
            // 打开占例ToolStripMenuItem
            // 
            this.打开占例ToolStripMenuItem.Name = "打开占例ToolStripMenuItem";
            this.打开占例ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.打开占例ToolStripMenuItem.Text = "打开占例";
            // 
            // 保存占例ToolStripMenuItem
            // 
            this.保存占例ToolStripMenuItem.Name = "保存占例ToolStripMenuItem";
            this.保存占例ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.保存占例ToolStripMenuItem.Text = "保存占例";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(121, 6);
            // 
            // 导出占例ToolStripMenuItem
            // 
            this.导出占例ToolStripMenuItem.Name = "导出占例ToolStripMenuItem";
            this.导出占例ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.导出占例ToolStripMenuItem.Text = "导出占例";
            // 
            // 配置ToolStripMenuItem
            // 
            this.配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.插件预设ToolStripMenuItem,
            this.插件包ToolStripMenuItem});
            this.配置ToolStripMenuItem.Name = "配置ToolStripMenuItem";
            this.配置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.配置ToolStripMenuItem.Text = "配置";
            // 
            // 插件预设ToolStripMenuItem
            // 
            this.插件预设ToolStripMenuItem.Name = "插件预设ToolStripMenuItem";
            this.插件预设ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.插件预设ToolStripMenuItem.Text = "插件预设";
            this.插件预设ToolStripMenuItem.Click += new System.EventHandler(this.插件预设ToolStripMenuItem_Click);
            // 
            // 插件包ToolStripMenuItem
            // 
            this.插件包ToolStripMenuItem.Name = "插件包ToolStripMenuItem";
            this.插件包ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.插件包ToolStripMenuItem.Text = "插件包";
            this.插件包ToolStripMenuItem.Click += new System.EventHandler(this.插件包ToolStripMenuItem_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 28);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(365, 410);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "点击菜单栏占例起课";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(383, 28);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(365, 410);
            this.textBox2.TabIndex = 3;
            // 
            // 主窗体
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 450);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "主窗体";
            this.Text = "主窗体";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem 占例ToolStripMenuItem;
        private ToolStripMenuItem 起课ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem 打开占例ToolStripMenuItem;
        private ToolStripMenuItem 保存占例ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem 导出占例ToolStripMenuItem;
        private ToolStripMenuItem 配置ToolStripMenuItem;
        private ToolStripMenuItem 插件预设ToolStripMenuItem;
        private ToolStripMenuItem 插件包ToolStripMenuItem;
        private TextBox textBox1;
        private TextBox textBox2;
    }
}