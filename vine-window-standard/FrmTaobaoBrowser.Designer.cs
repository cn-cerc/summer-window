namespace vine_window_standard
{
    partial class FrmTaobaoBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTaobaoBrowser));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.btnReadTaobao = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbUrl);
            this.panel1.Controls.Add(this.btnReadTaobao);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 21);
            this.panel1.TabIndex = 0;
            // 
            // tbUrl
            // 
            this.tbUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbUrl.Location = new System.Drawing.Point(0, 0);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(709, 21);
            this.tbUrl.TabIndex = 1;
            this.tbUrl.Text = "https://www.taobao.com";
            this.tbUrl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbUrl_KeyPress);
            // 
            // btnReadTaobao
            // 
            this.btnReadTaobao.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnReadTaobao.FlatAppearance.BorderSize = 0;
            this.btnReadTaobao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReadTaobao.Image = global::vine_window_standard.Properties.Resources.按钮1_2x;
            this.btnReadTaobao.Location = new System.Drawing.Point(709, 0);
            this.btnReadTaobao.Name = "btnReadTaobao";
            this.btnReadTaobao.Size = new System.Drawing.Size(75, 21);
            this.btnReadTaobao.TabIndex = 0;
            this.btnReadTaobao.Text = "读取";
            this.btnReadTaobao.UseVisualStyleBackColor = true;
            this.btnReadTaobao.Click += new System.EventHandler(this.btnReadTaobao_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 21);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(784, 400);
            this.webBrowser1.TabIndex = 1;
            // 
            // FrmTaobaoBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 421);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmTaobaoBrowser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "电商浏览器";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnReadTaobao;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TextBox tbUrl;
    }
}