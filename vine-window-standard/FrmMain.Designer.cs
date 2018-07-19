namespace vine_window_standard
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.plHead = new System.Windows.Forms.Panel();
            this.plTitle = new System.Windows.Forms.Panel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.ilMenu = new System.Windows.Forms.ImageList(this.components);
            this.btnPage = new System.Windows.Forms.Panel();
            this.lblFirstTitle = new System.Windows.Forms.Label();
            this.ilTitle = new System.Windows.Forms.ImageList(this.components);
            this.plSystem = new System.Windows.Forms.Panel();
            this.btnSetup = new System.Windows.Forms.Button();
            this.btnMax = new System.Windows.Forms.Button();
            this.btnMin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.plBody = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.mnuTitle = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.转到首页ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heartbeat = new System.Windows.Forms.Timer(this.components);
            this.plHead.SuspendLayout();
            this.plTitle.SuspendLayout();
            this.btnPage.SuspendLayout();
            this.plSystem.SuspendLayout();
            this.plBody.SuspendLayout();
            this.mnuTitle.SuspendLayout();
            this.mnuSetup.SuspendLayout();
            this.SuspendLayout();
            // 
            // plHead
            // 
            this.plHead.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.plHead.Controls.Add(this.plTitle);
            this.plHead.Controls.Add(this.plSystem);
            this.plHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.plHead.Location = new System.Drawing.Point(0, 0);
            this.plHead.Name = "plHead";
            this.plHead.Size = new System.Drawing.Size(655, 34);
            this.plHead.TabIndex = 0;
            // 
            // plTitle
            // 
            this.plTitle.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.plTitle.Controls.Add(this.btnNew);
            this.plTitle.Controls.Add(this.btnBack);
            this.plTitle.Controls.Add(this.btnPage);
            this.plTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plTitle.Location = new System.Drawing.Point(0, 0);
            this.plTitle.Name = "plTitle";
            this.plTitle.Size = new System.Drawing.Size(536, 34);
            this.plTitle.TabIndex = 3;
            this.plTitle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.plTitle_MouseDoubleClick);
            this.plTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.plTitle_MouseMove);
            // 
            // btnNew
            // 
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Image = global::vine_window_standard.Properties.Resources.首页_2_2x;
            this.btnNew.Location = new System.Drawing.Point(195, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(39, 32);
            this.btnNew.TabIndex = 6;
            this.btnNew.UseVisualStyleBackColor = false;
            // 
            // btnBack
            // 
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.ImageIndex = 4;
            this.btnBack.ImageList = this.ilMenu;
            this.btnBack.Location = new System.Drawing.Point(10, 7);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(24, 24);
            this.btnBack.TabIndex = 5;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // ilMenu
            // 
            this.ilMenu.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilMenu.ImageStream")));
            this.ilMenu.TransparentColor = System.Drawing.Color.Transparent;
            this.ilMenu.Images.SetKeyName(0, "缩小@2x.png");
            this.ilMenu.Images.SetKeyName(1, "放大@2x.png");
            this.ilMenu.Images.SetKeyName(2, "右删除@2x.png");
            this.ilMenu.Images.SetKeyName(3, "lightning.png");
            this.ilMenu.Images.SetKeyName(4, "返回@2x.png");
            this.ilMenu.Images.SetKeyName(5, "Close_8_8.png");
            // 
            // btnPage
            // 
            this.btnPage.BackColor = System.Drawing.Color.Transparent;
            this.btnPage.Controls.Add(this.lblFirstTitle);
            this.btnPage.ForeColor = System.Drawing.Color.Transparent;
            this.btnPage.Location = new System.Drawing.Point(40, 5);
            this.btnPage.Name = "btnPage";
            this.btnPage.Size = new System.Drawing.Size(152, 34);
            this.btnPage.TabIndex = 4;
            // 
            // lblFirstTitle
            // 
            this.lblFirstTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblFirstTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFirstTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblFirstTitle.ForeColor = System.Drawing.Color.Black;
            this.lblFirstTitle.ImageIndex = 1;
            this.lblFirstTitle.ImageList = this.ilTitle;
            this.lblFirstTitle.Location = new System.Drawing.Point(0, 0);
            this.lblFirstTitle.Name = "lblFirstTitle";
            this.lblFirstTitle.Size = new System.Drawing.Size(152, 34);
            this.lblFirstTitle.TabIndex = 2;
            this.lblFirstTitle.Text = "label1";
            this.lblFirstTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ilTitle
            // 
            this.ilTitle.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTitle.ImageStream")));
            this.ilTitle.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTitle.Images.SetKeyName(0, "没选择@1.png");
            this.ilTitle.Images.SetKeyName(1, "选择@2x.png");
            this.ilTitle.Images.SetKeyName(2, "没选择@2x.png");
            // 
            // plSystem
            // 
            this.plSystem.Controls.Add(this.btnSetup);
            this.plSystem.Controls.Add(this.btnMax);
            this.plSystem.Controls.Add(this.btnMin);
            this.plSystem.Controls.Add(this.btnExit);
            this.plSystem.Dock = System.Windows.Forms.DockStyle.Right;
            this.plSystem.Location = new System.Drawing.Point(536, 0);
            this.plSystem.Name = "plSystem";
            this.plSystem.Size = new System.Drawing.Size(119, 34);
            this.plSystem.TabIndex = 2;
            this.plSystem.MouseMove += new System.Windows.Forms.MouseEventHandler(this.plTitle_MouseMove);
            // 
            // btnSetup
            // 
            this.btnSetup.FlatAppearance.BorderSize = 0;
            this.btnSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetup.ImageIndex = 3;
            this.btnSetup.ImageList = this.ilMenu;
            this.btnSetup.Location = new System.Drawing.Point(6, 5);
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.Size = new System.Drawing.Size(24, 24);
            this.btnSetup.TabIndex = 8;
            this.btnSetup.UseVisualStyleBackColor = true;
            this.btnSetup.Click += new System.EventHandler(this.btnSetup_Click);
            // 
            // btnMax
            // 
            this.btnMax.FlatAppearance.BorderSize = 0;
            this.btnMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMax.ImageIndex = 1;
            this.btnMax.ImageList = this.ilMenu;
            this.btnMax.Location = new System.Drawing.Point(63, 5);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(24, 24);
            this.btnMax.TabIndex = 7;
            this.btnMax.UseVisualStyleBackColor = true;
            this.btnMax.Click += new System.EventHandler(this.btnSystemButtonClick);
            // 
            // btnMin
            // 
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.ImageIndex = 0;
            this.btnMin.ImageList = this.ilMenu;
            this.btnMin.Location = new System.Drawing.Point(35, 5);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(24, 24);
            this.btnMin.TabIndex = 6;
            this.btnMin.UseVisualStyleBackColor = true;
            this.btnMin.Click += new System.EventHandler(this.btnSystemButtonClick);
            // 
            // btnExit
            // 
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ImageIndex = 2;
            this.btnExit.ImageList = this.ilMenu;
            this.btnExit.Location = new System.Drawing.Point(89, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(24, 24);
            this.btnExit.TabIndex = 5;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnSystemButtonClick);
            // 
            // plBody
            // 
            this.plBody.Controls.Add(this.webBrowser1);
            this.plBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plBody.Location = new System.Drawing.Point(0, 34);
            this.plBody.Name = "plBody";
            this.plBody.Size = new System.Drawing.Size(655, 277);
            this.plBody.TabIndex = 1;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(655, 277);
            this.webBrowser1.TabIndex = 3;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.webBrowser1.NewWindow += new System.ComponentModel.CancelEventHandler(this.webBrowser1_NewWindow);
            // 
            // mnuTitle
            // 
            this.mnuTitle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.转到首页ToolStripMenuItem,
            this.关闭ToolStripMenuItem});
            this.mnuTitle.Name = "mnuTitle";
            this.mnuTitle.Size = new System.Drawing.Size(125, 48);
            this.mnuTitle.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnuTitle_ItemClicked);
            // 
            // 转到首页ToolStripMenuItem
            // 
            this.转到首页ToolStripMenuItem.Name = "转到首页ToolStripMenuItem";
            this.转到首页ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.转到首页ToolStripMenuItem.Text = "转到首页";
            // 
            // 关闭ToolStripMenuItem
            // 
            this.关闭ToolStripMenuItem.Name = "关闭ToolStripMenuItem";
            this.关闭ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.关闭ToolStripMenuItem.Text = "关闭";
            // 
            // mnuSetup
            // 
            this.mnuSetup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.退出ToolStripMenuItem,
            this.刷新ToolStripMenuItem});
            this.mnuSetup.Name = "mnuSetup";
            this.mnuSetup.Size = new System.Drawing.Size(101, 70);
            this.mnuSetup.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnuSetup_ItemClicked);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.刷新ToolStripMenuItem.Text = "刷新";
            // 
            // heartbeat
            // 
            this.heartbeat.Tick += new System.EventHandler(this.heartbeat_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 311);
            this.Controls.Add(this.plBody);
            this.Controls.Add(this.plHead);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地藤标准版";
            this.Activated += new System.EventHandler(this.FrmMain_Activated);
            this.Leave += new System.EventHandler(this.FrmMain_Leave);
            this.plHead.ResumeLayout(false);
            this.plTitle.ResumeLayout(false);
            this.btnPage.ResumeLayout(false);
            this.plSystem.ResumeLayout(false);
            this.plBody.ResumeLayout(false);
            this.mnuTitle.ResumeLayout(false);
            this.mnuSetup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel plHead;
        private System.Windows.Forms.Panel plBody;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Panel plSystem;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel plTitle;
        private System.Windows.Forms.Panel btnPage;
        private System.Windows.Forms.Label lblFirstTitle;
        private System.Windows.Forms.Button btnMax;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ContextMenuStrip mnuTitle;
        private System.Windows.Forms.ToolStripMenuItem 转到首页ToolStripMenuItem;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.ImageList ilTitle;
        private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem;
        private System.Windows.Forms.Button btnSetup;
        private System.Windows.Forms.ImageList ilMenu;
        private System.Windows.Forms.ContextMenuStrip mnuSetup;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.Timer heartbeat;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
    }
}

