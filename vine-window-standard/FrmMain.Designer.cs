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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.plHead = new System.Windows.Forms.Panel();
            this.plSystem = new System.Windows.Forms.Panel();
            this.btnMin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.plBody = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.plTitle = new System.Windows.Forms.Panel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnPage = new System.Windows.Forms.Panel();
            this.lblFirstTitle = new System.Windows.Forms.Label();
            this.plHead.SuspendLayout();
            this.plSystem.SuspendLayout();
            this.plBody.SuspendLayout();
            this.plTitle.SuspendLayout();
            this.btnPage.SuspendLayout();
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
            this.plHead.Size = new System.Drawing.Size(655, 35);
            this.plHead.TabIndex = 0;
            this.plHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // plSystem
            // 
            this.plSystem.Controls.Add(this.btnMin);
            this.plSystem.Controls.Add(this.btnExit);
            this.plSystem.Dock = System.Windows.Forms.DockStyle.Right;
            this.plSystem.Location = new System.Drawing.Point(557, 0);
            this.plSystem.Name = "plSystem";
            this.plSystem.Size = new System.Drawing.Size(98, 35);
            this.plSystem.TabIndex = 2;
            this.plSystem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // btnMin
            // 
            this.btnMin.Location = new System.Drawing.Point(35, 5);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(24, 24);
            this.btnMin.TabIndex = 6;
            this.btnMin.Text = "_";
            this.btnMin.UseVisualStyleBackColor = true;
            this.btnMin.Click += new System.EventHandler(this.btnSystemButtonClick);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(65, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(24, 24);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnSystemButtonClick);
            // 
            // plBody
            // 
            this.plBody.Controls.Add(this.webBrowser1);
            this.plBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plBody.Location = new System.Drawing.Point(0, 35);
            this.plBody.Name = "plBody";
            this.plBody.Size = new System.Drawing.Size(655, 276);
            this.plBody.TabIndex = 1;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(655, 276);
            this.webBrowser1.TabIndex = 3;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.webBrowser1.NewWindow += new System.ComponentModel.CancelEventHandler(this.webBrowser1_NewWindow);
            // 
            // plTitle
            // 
            this.plTitle.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.plTitle.Controls.Add(this.btnPage);
            this.plTitle.Controls.Add(this.btnNew);
            this.plTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plTitle.Location = new System.Drawing.Point(0, 0);
            this.plTitle.Name = "plTitle";
            this.plTitle.Size = new System.Drawing.Size(557, 35);
            this.plTitle.TabIndex = 3;
            this.plTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(129, 5);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(24, 24);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "+";
            this.btnNew.UseVisualStyleBackColor = true;
            // 
            // btnPage
            // 
            this.btnPage.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnPage.Controls.Add(this.lblFirstTitle);
            this.btnPage.Location = new System.Drawing.Point(4, 5);
            this.btnPage.Name = "btnPage";
            this.btnPage.Size = new System.Drawing.Size(119, 24);
            this.btnPage.TabIndex = 4;
            // 
            // lblFirstTitle
            // 
            this.lblFirstTitle.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblFirstTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFirstTitle.Location = new System.Drawing.Point(0, 0);
            this.lblFirstTitle.Name = "lblFirstTitle";
            this.lblFirstTitle.Size = new System.Drawing.Size(119, 24);
            this.lblFirstTitle.TabIndex = 2;
            this.lblFirstTitle.Text = "label1";
            this.lblFirstTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 311);
            this.Controls.Add(this.plBody);
            this.Controls.Add(this.plHead);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地藤标准版";
            this.plHead.ResumeLayout(false);
            this.plSystem.ResumeLayout(false);
            this.plBody.ResumeLayout(false);
            this.plTitle.ResumeLayout(false);
            this.btnPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel plHead;
        private System.Windows.Forms.Panel plBody;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Panel plSystem;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel plTitle;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Panel btnPage;
        private System.Windows.Forms.Label lblFirstTitle;
    }
}

