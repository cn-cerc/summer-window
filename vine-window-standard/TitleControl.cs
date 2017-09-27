using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vine_window_standard
{
    class TitleControl
    {
        private List<Control> items = new List<Control>();
        private Control parent;
        private Color backColor;
        public EventHandler CloseClick;
        public EventHandler GoClick;
        public int index = -1;

        public TitleControl(Control parent)
        {
            this.parent = parent;
        }

        internal int IndexOf(Control item)
        {
            return items.IndexOf(item);
        }

        internal void AddItem(Control item)
        {
            item.BackColor = backColor;
            item.Click += GoClick;
            items.Add(item);
            Index = items.Count - 1;
        }

        internal Control AddItem()
        {
            Control last = items[items.Count - 1];

            Control item = new Panel();
            item.Parent = this.parent;
            item.Visible = true;
            item.Top = last.Top;
            item.Left = last.Left + last.Width + 10;
            item.Height = last.Height;
            item.Width = last.Width;
            item.BackColor = this.BackColor;
            AddItem(item);

            Label label = new Label();
            label.Parent = item;
            label.Dock = DockStyle.Fill;
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label.Text = String.Format("Sheet{0:G}", items.Count);
            label.Visible = true;
            label.Click += GoClick;

            //关闭按钮
            Label button = new Label();
            button.Parent = item;
            button.Text = "X";
            button.Width = 10;
            button.Dock = DockStyle.Right;
            button.Tag = item;
            button.Click += CloseClick;

            return item;
        }

        public int Count
        {
            get { return items.Count; }
        }

        internal void Remove(int v)
        {
            Control button = items[items.Count - 1];
            items.Remove(button);
            button.Dispose();
        }

        public Control getItem(int index)
        {
            return items[index];
        }

        internal void setTitle(int index, string text)
        {
            Control item = items[index];
            if (item.Controls.Count > 0)
                item.Controls[0].Text = text;
            else
                item.Text = text;
        }

        public Color BackColor
        {
            get { return backColor; }
            set { this.backColor = value; }
        }

        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                if (index != value)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        if(i != value)
                        {
                            items[i].BackColor = this.backColor;
                        }
                    }
                    items[value].BackColor = Color.Blue;
                    index = value;
                }
            }
        }
    }
}
