namespace project
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newTabContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeTabContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.前进ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.后退ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myTabControl = new project.MyTabControl();
            this.tabContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabContextMenu
            // 
            this.tabContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTabContextMenuItem,
            this.closeTabContextMenuItem,
            this.前进ToolStripMenuItem,
            this.后退ToolStripMenuItem});
            this.tabContextMenu.Name = "tabContextMenu";
            this.tabContextMenu.Size = new System.Drawing.Size(137, 92);
            // 
            // newTabContextMenuItem
            // 
            this.newTabContextMenuItem.Name = "newTabContextMenuItem";
            this.newTabContextMenuItem.Size = new System.Drawing.Size(136, 22);
            this.newTabContextMenuItem.Text = "新建标签页";
            this.newTabContextMenuItem.Click += new System.EventHandler(this.newTabContextMenuItem_Click);
            // 
            // closeTabContextMenuItem
            // 
            this.closeTabContextMenuItem.Name = "closeTabContextMenuItem";
            this.closeTabContextMenuItem.Size = new System.Drawing.Size(136, 22);
            this.closeTabContextMenuItem.Text = "关闭标签页";
            this.closeTabContextMenuItem.Click += new System.EventHandler(this.closeTabContextMenuItem_Click);
            // 
            // 前进ToolStripMenuItem
            // 
            this.前进ToolStripMenuItem.Name = "前进ToolStripMenuItem";
            this.前进ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.前进ToolStripMenuItem.Text = "前进";
            this.前进ToolStripMenuItem.Click += new System.EventHandler(this.前进ToolStripMenuItem_Click);
            // 
            // 后退ToolStripMenuItem
            // 
            this.后退ToolStripMenuItem.Name = "后退ToolStripMenuItem";
            this.后退ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.后退ToolStripMenuItem.Text = "后退";
            this.后退ToolStripMenuItem.Click += new System.EventHandler(this.后退ToolStripMenuItem_Click);
            // 
            // myTabControl
            // 
            this.myTabControl.ContextMenuStrip = this.tabContextMenu;
            this.myTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.myTabControl.Location = new System.Drawing.Point(0, 0);
            this.myTabControl.Name = "myTabControl";
            this.myTabControl.NewPageCaptain = "新标签页";
            this.myTabControl.Padding = new System.Drawing.Point(15, 5);
            this.myTabControl.PgIndex = 1;
            this.myTabControl.SelectedIndex = 0;
            this.myTabControl.Size = new System.Drawing.Size(1344, 730);
            this.myTabControl.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 730);
            this.ContextMenuStrip = this.tabContextMenu;
            this.Controls.Add(this.myTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "地藤软件";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyTabControl myTabControl;
        private System.Windows.Forms.ContextMenuStrip tabContextMenu;
        private System.Windows.Forms.ToolStripMenuItem newTabContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeTabContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 前进ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 后退ToolStripMenuItem;
    }
}

