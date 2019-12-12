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
            this.ilMenu = new System.Windows.Forms.ImageList(this.components);
            this.ilTitle = new System.Windows.Forms.ImageList(this.components);
            this.mnuTitle = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.转到首页ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.缩小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加入书签ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heartbeat = new System.Windows.Forms.Timer(this.components);
            this.timerPrint = new System.Windows.Forms.Timer(this.components);
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.brmum = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newTool = new System.Windows.Forms.ToolStripMenuItem();
            this.delTool = new System.Windows.Forms.ToolStripMenuItem();
            this.ilLB = new System.Windows.Forms.ImageList(this.components);
            this.ilTop = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.munBook = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.numMore = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewPagetool = new System.Windows.Forms.ToolStripMenuItem();
            this.delItemTool = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx1 = new vine_window_standard.PanelEx();
            this.plBody = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.plHead = new System.Windows.Forms.Panel();
            this.plSystem = new System.Windows.Forms.Panel();
            this.lbMore = new System.Windows.Forms.Label();
            this.btnMax = new System.Windows.Forms.Button();
            this.btnMin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.plTitle = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnPage = new System.Windows.Forms.Panel();
            this.lblFirstTitle = new System.Windows.Forms.Label();
            this.plBookmark = new System.Windows.Forms.Panel();
            this.btnMange = new System.Windows.Forms.Button();
            this.btnMange1 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSetup = new System.Windows.Forms.Button();
            this.btnSetup1 = new System.Windows.Forms.Label();
            this.btnNew1 = new System.Windows.Forms.Label();
            this.mnuTitle.SuspendLayout();
            this.mnuSetup.SuspendLayout();
            this.brmum.SuspendLayout();
            this.munBook.SuspendLayout();
            this.numMore.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.plBody.SuspendLayout();
            this.plHead.SuspendLayout();
            this.plSystem.SuspendLayout();
            this.panel1.SuspendLayout();
            this.plTitle.SuspendLayout();
            this.btnPage.SuspendLayout();
            this.plBookmark.SuspendLayout();
            this.SuspendLayout();
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
            // ilTitle
            // 
            this.ilTitle.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTitle.ImageStream")));
            this.ilTitle.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTitle.Images.SetKeyName(0, "title_normal.png");
            this.ilTitle.Images.SetKeyName(1, "title_light.png");
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
            this.刷新ToolStripMenuItem,
            this.toolStripSeparator2,
            this.toolStripSeparator1,
            this.toolStripMenuItem1,
            this.toolStripMenuItem3,
            this.缩小ToolStripMenuItem,
            this.加入书签ToolStripMenuItem});
            this.mnuSetup.Name = "mnuSetup";
            this.mnuSetup.Size = new System.Drawing.Size(141, 186);
            this.mnuSetup.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.mnuSetup_Closing);
            this.mnuSetup.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnuSetup_ItemClicked);
            this.mnuSetup.MouseLeave += new System.EventHandler(this.mnuSetup_MouseLeave);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.刷新ToolStripMenuItem.Text = "刷新";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(140, 22);
            this.toolStripSeparator2.Text = "全部关闭";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(137, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.toolStripMenuItem1.Text = "重置  100%";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(140, 22);
            this.toolStripMenuItem3.Text = "放大";
            // 
            // 缩小ToolStripMenuItem
            // 
            this.缩小ToolStripMenuItem.Name = "缩小ToolStripMenuItem";
            this.缩小ToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.缩小ToolStripMenuItem.Text = "缩小";
            // 
            // 加入书签ToolStripMenuItem
            // 
            this.加入书签ToolStripMenuItem.Name = "加入书签ToolStripMenuItem";
            this.加入书签ToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.加入书签ToolStripMenuItem.Text = "添加书签";
            // 
            // heartbeat
            // 
            this.heartbeat.Tick += new System.EventHandler(this.heartbeat_Tick);
            // 
            // timerPrint
            // 
            this.timerPrint.Interval = 1000;
            // 
            // brmum
            // 
            this.brmum.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTool,
            this.delTool});
            this.brmum.Name = "brmum";
            this.brmum.ShowImageMargin = false;
            this.brmum.Size = new System.Drawing.Size(148, 48);
            // 
            // newTool
            // 
            this.newTool.Name = "newTool";
            this.newTool.Size = new System.Drawing.Size(147, 22);
            this.newTool.Text = "在新标签页中打开";
            this.newTool.Click += new System.EventHandler(this.newTool_Click);
            // 
            // delTool
            // 
            this.delTool.Name = "delTool";
            this.delTool.Size = new System.Drawing.Size(147, 22);
            this.delTool.Text = "删除";
            this.delTool.Click += new System.EventHandler(this.delTool_Click);
            // 
            // ilLB
            // 
            this.ilLB.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilLB.ImageStream")));
            this.ilLB.TransparentColor = System.Drawing.Color.Transparent;
            this.ilLB.Images.SetKeyName(0, "Close.png");
            this.ilLB.Images.SetKeyName(1, "Full.png");
            this.ilLB.Images.SetKeyName(2, "Narrow.png");
            this.ilLB.Images.SetKeyName(3, "Minimize.png");
            // 
            // ilTop
            // 
            this.ilTop.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTop.ImageStream")));
            this.ilTop.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTop.Images.SetKeyName(0, "Navigation.png");
            this.ilTop.Images.SetKeyName(1, "Setting.png");
            this.ilTop.Images.SetKeyName(2, "Manage.png");
            this.ilTop.Images.SetKeyName(3, "RemarkBook.png");
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // munBook
            // 
            this.munBook.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator3});
            this.munBook.Name = "munBook";
            this.munBook.ShowImageMargin = false;
            this.munBook.Size = new System.Drawing.Size(36, 10);
            this.munBook.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.munBook_Closing);
            this.munBook.MouseLeave += new System.EventHandler(this.munBook_MouseLeave);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(32, 6);
            // 
            // numMore
            // 
            this.numMore.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewPagetool,
            this.delItemTool});
            this.numMore.Name = "brmum";
            this.numMore.ShowImageMargin = false;
            this.numMore.Size = new System.Drawing.Size(148, 48);
            this.numMore.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.numMore_Closed);
            this.numMore.MouseEnter += new System.EventHandler(this.numMore_MouseEnter);
            this.numMore.MouseLeave += new System.EventHandler(this.numMore_MouseLeave);
            // 
            // NewPagetool
            // 
            this.NewPagetool.Name = "NewPagetool";
            this.NewPagetool.Size = new System.Drawing.Size(147, 22);
            this.NewPagetool.Text = "在新标签页中打开";
            this.NewPagetool.Click += new System.EventHandler(this.NewPagetool_Click);
            // 
            // delItemTool
            // 
            this.delItemTool.Name = "delItemTool";
            this.delItemTool.Size = new System.Drawing.Size(147, 22);
            this.delItemTool.Text = "删除";
            this.delItemTool.Click += new System.EventHandler(this.delItemTool_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.BorderColor = System.Drawing.Color.DarkGray;
            this.panelEx1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEx1.BorderWidth = 1;
            this.panelEx1.Controls.Add(this.plBody);
            this.panelEx1.Controls.Add(this.plHead);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1008, 421);
            this.panelEx1.TabIndex = 8;
            // 
            // plBody
            // 
            this.plBody.Controls.Add(this.webBrowser1);
            this.plBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plBody.Location = new System.Drawing.Point(0, 75);
            this.plBody.Name = "plBody";
            this.plBody.Size = new System.Drawing.Size(1006, 344);
            this.plBody.TabIndex = 1;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1006, 344);
            this.webBrowser1.TabIndex = 3;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.webBrowser1.NewWindow += new System.ComponentModel.CancelEventHandler(this.webBrowser1_NewWindow);
            // 
            // plHead
            // 
            this.plHead.BackColor = System.Drawing.Color.Transparent;
            this.plHead.Controls.Add(this.plSystem);
            this.plHead.Controls.Add(this.panel1);
            this.plHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.plHead.ForeColor = System.Drawing.Color.Transparent;
            this.plHead.Location = new System.Drawing.Point(0, 0);
            this.plHead.Name = "plHead";
            this.plHead.Size = new System.Drawing.Size(1006, 75);
            this.plHead.TabIndex = 0;
            // 
            // plSystem
            // 
            this.plSystem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.plSystem.BackColor = System.Drawing.Color.WhiteSmoke;
            this.plSystem.BackgroundImage = global::vine_window_standard.Properties.Resources.Top_03;
            this.plSystem.Controls.Add(this.lbMore);
            this.plSystem.Controls.Add(this.btnMax);
            this.plSystem.Controls.Add(this.btnMin);
            this.plSystem.Controls.Add(this.btnExit);
            this.plSystem.Location = new System.Drawing.Point(893, 0);
            this.plSystem.Margin = new System.Windows.Forms.Padding(0);
            this.plSystem.Name = "plSystem";
            this.plSystem.Size = new System.Drawing.Size(114, 34);
            this.plSystem.TabIndex = 3;
            // 
            // lbMore
            // 
            this.lbMore.BackColor = System.Drawing.Color.Transparent;
            this.lbMore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbMore.Image = global::vine_window_standard.Properties.Resources.Arrow;
            this.lbMore.Location = new System.Drawing.Point(0, 0);
            this.lbMore.Margin = new System.Windows.Forms.Padding(0);
            this.lbMore.Name = "lbMore";
            this.lbMore.Size = new System.Drawing.Size(21, 30);
            this.lbMore.TabIndex = 20;
            this.lbMore.Visible = false;
            this.lbMore.Click += new System.EventHandler(this.lbMore_Click);
            // 
            // btnMax
            // 
            this.btnMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMax.BackColor = System.Drawing.Color.Transparent;
            this.btnMax.BackgroundImage = global::vine_window_standard.Properties.Resources.Full;
            this.btnMax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMax.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnMax.FlatAppearance.BorderSize = 0;
            this.btnMax.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMax.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMax.ImageIndex = 1;
            this.btnMax.Location = new System.Drawing.Point(54, 3);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(24, 24);
            this.btnMax.TabIndex = 7;
            this.btnMax.UseVisualStyleBackColor = false;
            this.btnMax.Click += new System.EventHandler(this.btnSystemButtonClick);
            // 
            // btnMin
            // 
            this.btnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMin.BackColor = System.Drawing.Color.Transparent;
            this.btnMin.BackgroundImage = global::vine_window_standard.Properties.Resources.Minimize;
            this.btnMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.ImageIndex = 3;
            this.btnMin.Location = new System.Drawing.Point(24, 3);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(24, 24);
            this.btnMin.TabIndex = 6;
            this.btnMin.UseVisualStyleBackColor = false;
            this.btnMin.Click += new System.EventHandler(this.btnSystemButtonClick);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImage = global::vine_window_standard.Properties.Resources.Close;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ImageIndex = 0;
            this.btnExit.Location = new System.Drawing.Point(86, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(24, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnSystemButtonClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.plTitle);
            this.panel1.Controls.Add(this.plBookmark);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1006, 75);
            this.panel1.TabIndex = 8;
            // 
            // plTitle
            // 
            this.plTitle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.plTitle.BackgroundImage = global::vine_window_standard.Properties.Resources.Top_02;
            this.plTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.plTitle.Controls.Add(this.btnBack);
            this.plTitle.Controls.Add(this.btnPage);
            this.plTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTitle.Location = new System.Drawing.Point(0, 34);
            this.plTitle.Name = "plTitle";
            this.plTitle.Size = new System.Drawing.Size(1006, 37);
            this.plTitle.TabIndex = 3;
            this.plTitle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.plTitle_MouseDoubleClick);
            this.plTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.plTitle_MouseMove);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.ForeColor = System.Drawing.Color.Transparent;
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.Location = new System.Drawing.Point(6, 9);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(22, 22);
            this.btnBack.TabIndex = 5;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnPage
            // 
            this.btnPage.BackColor = System.Drawing.Color.Transparent;
            this.btnPage.Controls.Add(this.lblFirstTitle);
            this.btnPage.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPage.ForeColor = System.Drawing.Color.Transparent;
            this.btnPage.Location = new System.Drawing.Point(45, 5);
            this.btnPage.Name = "btnPage";
            this.btnPage.Size = new System.Drawing.Size(130, 38);
            this.btnPage.TabIndex = 4;
            // 
            // lblFirstTitle
            // 
            this.lblFirstTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblFirstTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFirstTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblFirstTitle.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFirstTitle.ForeColor = System.Drawing.Color.Black;
            this.lblFirstTitle.Image = ((System.Drawing.Image)(resources.GetObject("lblFirstTitle.Image")));
            this.lblFirstTitle.Location = new System.Drawing.Point(0, 0);
            this.lblFirstTitle.Name = "lblFirstTitle";
            this.lblFirstTitle.Size = new System.Drawing.Size(130, 38);
            this.lblFirstTitle.TabIndex = 2;
            this.lblFirstTitle.Text = "label1";
            this.lblFirstTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // plBookmark
            // 
            this.plBookmark.BackgroundImage = global::vine_window_standard.Properties.Resources.Top_01;
            this.plBookmark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.plBookmark.Controls.Add(this.btnMange);
            this.plBookmark.Controls.Add(this.btnMange1);
            this.plBookmark.Controls.Add(this.btnNew);
            this.plBookmark.Controls.Add(this.btnSetup);
            this.plBookmark.Controls.Add(this.btnSetup1);
            this.plBookmark.Controls.Add(this.btnNew1);
            this.plBookmark.Dock = System.Windows.Forms.DockStyle.Top;
            this.plBookmark.Location = new System.Drawing.Point(0, 0);
            this.plBookmark.Name = "plBookmark";
            this.plBookmark.Size = new System.Drawing.Size(1006, 34);
            this.plBookmark.TabIndex = 4;
            this.plBookmark.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.plTitle_MouseDoubleClick);
            this.plBookmark.MouseMove += new System.Windows.Forms.MouseEventHandler(this.plTitle_MouseMove);
            // 
            // btnMange
            // 
            this.btnMange.BackColor = System.Drawing.Color.Transparent;
            this.btnMange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMange.FlatAppearance.BorderSize = 0;
            this.btnMange.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMange.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMange.ForeColor = System.Drawing.Color.Silver;
            this.btnMange.ImageIndex = 0;
            this.btnMange.Location = new System.Drawing.Point(160, 6);
            this.btnMange.Name = "btnMange";
            this.btnMange.Size = new System.Drawing.Size(39, 24);
            this.btnMange.TabIndex = 16;
            this.btnMange.Text = "管理";
            this.btnMange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMange.UseVisualStyleBackColor = false;
            this.btnMange.Click += new System.EventHandler(this.btnMange_Click);
            // 
            // btnMange1
            // 
            this.btnMange1.BackColor = System.Drawing.Color.Transparent;
            this.btnMange1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMange1.Image = global::vine_window_standard.Properties.Resources.Manage;
            this.btnMange1.Location = new System.Drawing.Point(135, 6);
            this.btnMange1.Margin = new System.Windows.Forms.Padding(0);
            this.btnMange1.Name = "btnMange1";
            this.btnMange1.Size = new System.Drawing.Size(24, 24);
            this.btnMange1.TabIndex = 19;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.Transparent;
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.ForeColor = System.Drawing.Color.Silver;
            this.btnNew.ImageIndex = 0;
            this.btnNew.Location = new System.Drawing.Point(30, 6);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(39, 24);
            this.btnNew.TabIndex = 12;
            this.btnNew.Text = "导航";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.newPageClick);
            // 
            // btnSetup
            // 
            this.btnSetup.BackColor = System.Drawing.Color.Transparent;
            this.btnSetup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSetup.FlatAppearance.BorderSize = 0;
            this.btnSetup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSetup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetup.ForeColor = System.Drawing.Color.Silver;
            this.btnSetup.ImageIndex = 0;
            this.btnSetup.Location = new System.Drawing.Point(93, 6);
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.Size = new System.Drawing.Size(39, 24);
            this.btnSetup.TabIndex = 14;
            this.btnSetup.Text = "设置";
            this.btnSetup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSetup.UseVisualStyleBackColor = false;
            this.btnSetup.Click += new System.EventHandler(this.btnSetup_Click);
            // 
            // btnSetup1
            // 
            this.btnSetup1.BackColor = System.Drawing.Color.Transparent;
            this.btnSetup1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetup1.Image = global::vine_window_standard.Properties.Resources.Setting;
            this.btnSetup1.Location = new System.Drawing.Point(71, 6);
            this.btnSetup1.Margin = new System.Windows.Forms.Padding(0);
            this.btnSetup1.Name = "btnSetup1";
            this.btnSetup1.Size = new System.Drawing.Size(24, 24);
            this.btnSetup1.TabIndex = 18;
            // 
            // btnNew1
            // 
            this.btnNew1.BackColor = System.Drawing.Color.Transparent;
            this.btnNew1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew1.Image = global::vine_window_standard.Properties.Resources.Navigation;
            this.btnNew1.Location = new System.Drawing.Point(7, 6);
            this.btnNew1.Margin = new System.Windows.Forms.Padding(0);
            this.btnNew1.Name = "btnNew1";
            this.btnNew1.Size = new System.Drawing.Size(24, 24);
            this.btnNew1.TabIndex = 17;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 421);
            this.Controls.Add(this.panelEx1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地藤管家";
            this.Activated += new System.EventHandler(this.FrmMain_Activated);
            this.Leave += new System.EventHandler(this.FrmMain_Leave);
            this.mnuTitle.ResumeLayout(false);
            this.mnuSetup.ResumeLayout(false);
            this.brmum.ResumeLayout(false);
            this.munBook.ResumeLayout(false);
            this.numMore.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.plBody.ResumeLayout(false);
            this.plHead.ResumeLayout(false);
            this.plSystem.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.plTitle.ResumeLayout(false);
            this.btnPage.ResumeLayout(false);
            this.plBookmark.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel plHead;
        private System.Windows.Forms.Panel plBody;
        private System.Windows.Forms.Panel plTitle;
        private System.Windows.Forms.Panel btnPage;
        private System.Windows.Forms.Label lblFirstTitle;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ContextMenuStrip mnuTitle;
        private System.Windows.Forms.ToolStripMenuItem 转到首页ToolStripMenuItem;
        private System.Windows.Forms.ImageList ilTitle;
        private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem;
        private System.Windows.Forms.ImageList ilMenu;
        private System.Windows.Forms.ContextMenuStrip mnuSetup;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.Timer heartbeat;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
        private System.Windows.Forms.Timer timerPrint;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 缩小ToolStripMenuItem;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Panel plBookmark;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.ToolStripMenuItem 加入书签ToolStripMenuItem;
        private System.Windows.Forms.Panel plSystem;
        private System.Windows.Forms.Button btnMax;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ContextMenuStrip brmum;
        private System.Windows.Forms.ToolStripMenuItem delTool;
        private System.Windows.Forms.ToolStripMenuItem toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private PanelEx panelEx1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList ilLB;
        private System.Windows.Forms.ImageList ilTop;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSetup;
        private System.Windows.Forms.Button btnMange;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label btnSetup1;
        private System.Windows.Forms.Label btnNew1;
        private System.Windows.Forms.Label btnMange1;
        private System.Windows.Forms.ToolStripMenuItem newTool;
        private System.Windows.Forms.Label lbMore;
        private System.Windows.Forms.ContextMenuStrip munBook;
        private System.Windows.Forms.ContextMenuStrip numMore;
        private System.Windows.Forms.ToolStripMenuItem NewPagetool;
        private System.Windows.Forms.ToolStripMenuItem delItemTool;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

