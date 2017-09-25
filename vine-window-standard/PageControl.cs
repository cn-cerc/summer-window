using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vine_window_standard
{
    class PageControl
    {
        private Form owner;
        private Control parent;
        private List<WebBrowser> items = new List<WebBrowser>();
        private int index = -1;

        public PageControl(Form owner, Control parent)
        {
            this.owner = owner;
            this.parent = parent;
        }

        public Form Owner
        {
            get { return this.owner; }
        }

        public List<WebBrowser> Items { get { return items; } }

        public int Index
        {
            get { return index; }
            internal set
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (i != value)
                    {
                        items[i].Visible = false;
                    }
                }
                for (int i = 0; i < items.Count; i++)
                {
                    if (i == value)
                    {
                        items[i].Visible = true;
                    }
                }
                this.index = value;
            }
        }
        public int Count
        {
            get { return items.Count; }
        }

        public WebBrowser browser
        {
            get { return index < 0 ? null : items[index]; }
        }

        internal void AddItem(WebBrowser browser)
        {
            foreach (WebBrowser item in items)
                item.Visible = false;
            items.Add(browser);

            browser.IsWebBrowserContextMenuEnabled = false;
            browser.ObjectForScripting = owner;
            browser.ScriptErrorsSuppressed = true;

            this.Index = items.Count - 1;
        }

        internal void addItem()
        {
            WebBrowser browser = new WebBrowser();
            browser.Parent = this.parent;
            browser.Dock = DockStyle.Fill;
            AddItem(browser);
        }

        internal void Delete(int index)
        {
            WebBrowser browser = items[index];
            items.Remove(browser);
            browser.Dispose();
        }
    }
}
