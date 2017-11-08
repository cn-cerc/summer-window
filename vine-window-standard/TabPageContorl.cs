using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vine_window_standard
{
    class TabPageContorl
    {
        private TabPage tabPage;
        private WebBrowser browser;
        private string url;
        private Panel panel1;
        private Button btRead;
        public Label lbMessage;
        public string defaultUrl;
        public Button btPage;

        public TabPageContorl(string openUrl, string Title)
        {
            this.url = openUrl;
            this.defaultUrl = openUrl;
            tabPage = new System.Windows.Forms.TabPage();
            tabPage.Text = Title;
        }

        public void AddItem()
        {
            browser = new WebBrowser();
            browser.ScriptErrorsSuppressed = true;
            browser.Dock = System.Windows.Forms.DockStyle.Fill;
            browser.Visible = true;
            tabPage.Controls.Add(browser);
            panel1 = new Panel();
            panel1.Height = 65;
            panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel1.Parent = tabPage;
            btRead = new Button();
            btRead.Parent = panel1;
            btRead.Text = "读取";
            lbMessage = new Label();
            lbMessage.Parent = panel1;
            lbMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            lbMessage.Text = "";
        }

        public TabPage getPage()
        {
            return tabPage;
        }

        public WebBrowser getBrowser()
        {
            return browser;
        }

        public string getUrl()
        {
            return url;
        }

        public Button getbtRead()
        {
            return btRead;
        }

        public void AddButton(Panel parent, int Index)
        {
            btPage = new Button();
            btPage.Parent = parent;
            btPage.Height = 23;
            btPage.Top = 23 * (Index + 1);
            btPage.Text = tabPage.Text;
            btPage.Tag = Index;
        }
    }
}
