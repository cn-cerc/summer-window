using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp.WinForms.Internals;
using CefSharp;


namespace project
{
    public partial class MainForm : Form
    {
        CefSharp.WinForms.ChromiumWebBrowser browser = null;
        public MainForm()
        {
            InitializeComponent();
            NewTab("http://sm.knowall.cn/form/TFrmWelcome");
        }

        private void NewTab(string startUrl)
        {
            var page = new TabPage("New Tab");
            page.Padding = new Padding(0, 0, 0, 0);
            browser = new CefSharp.WinForms.ChromiumWebBrowser(startUrl);
            browser.Dock = DockStyle.Fill;
            page.Controls.Add(browser);
            myTabControl.Controls.Add(page);
            myTabControl.SelectedTab = page;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //初始化Tab选项卡添加按钮
            this.myTabControl.SetPageAddBtn();
        }

        private void newTabContextMenuItem_Click(object sender, EventArgs e)
        {
            NewTab("http://www.mimrc.com/");
        }

        private void closeTabContextMenuItem_Click(object sender, EventArgs e)
        {
            this.myTabControl.TabPages.Remove(myTabControl.SelectedTab);
        }

        private void 前进ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            browser.GetBrowser().GoForward();
        }

        private void 后退ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            browser.GetBrowser().GoBack();
        }
    }
}
