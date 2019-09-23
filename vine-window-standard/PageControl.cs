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
        public bool isHide = false;
        private List<bool> itemVis = new List<bool>();

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
                    if (i == value)
                    {
                        items[i].Visible = true;
                    }
                }
                for (int i = 0; i < items.Count; i++)
                {
                    if (i != value)
                    {
                        items[i].Visible = false;
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
            if (isHide)
                browser.Visible = false;
            else
                foreach (WebBrowser item in items)
                    item.Visible = false;
            items.Add(browser);
            itemVis.Add(isHide);

            //browser.IsWebBrowserContextMenuEnabled = false;
            browser.ObjectForScripting = owner;
            browser.ScriptErrorsSuppressed = true;
            if (!isHide)
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
