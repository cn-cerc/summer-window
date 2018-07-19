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
        private Label firstTitle;
        private List<ContextMenuStrip> titles = new List<ContextMenuStrip>();
        public ToolStripItemClickedEventHandler ItemClick;
        public int OwenIndex = 0;

        public TitleControl(Control parent, Label firstTitle)
        {
            this.parent = parent;
            this.firstTitle = firstTitle;
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
            item.BackColor = Color.FromArgb(0, 0, 0, 0);
            item.Parent = this.parent;
            item.Visible = true;
            item.Top = last.Top;
            item.Left = last.Left + last.Width;
            item.Height = last.Height;
            item.Width = last.Width;
            item.BackColor = this.BackColor;
            AddItem(item);

            Label label = new Label();
            label.ImageList = firstTitle.ImageList;
            label.ImageIndex = 1;
            label.ForeColor = Color.Black;
            label.Parent = item;
            label.Dock = DockStyle.Fill;
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label.Text = String.Format("Sheet{0:G}", items.Count);
            label.Visible = true;
            label.Click += GoClick;

            //关闭按钮
            Label button = new Label();
            button.BackColor = Color.Transparent;
            button.Parent = label;
            button.Top = 11;
            button.Left = 130;
            button.Width = 8;
            button.Height = 8;
            button.Tag = item;
            button.Click += CloseClick;
            button.Image = vine_window_standard.Properties.Resources.Close_8_8;
            button.MouseMove += CloseMouseHover;
            button.MouseLeave += CloseMouseLeave;

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
                            //items[i].BackColor = this.backColor;
                            foreach(var obj in items[i].Controls)
                            {
                                if(obj is Label)
                                {
                                    Label label = (Label)obj;
                                    label.ImageIndex = 0;
                                    label.ForeColor = Color.White;
                                    foreach(var obj1 in label.Controls)
                                    {
                                        Label label1 = (Label)obj1;
                                        label.ForeColor = Color.White;
                                    }
                                }
                            }
                        }
                    }
                    //items[value].BackColor = Color.Blue;
                    foreach (var obj in items[value].Controls)
                    {
                        if (obj is Label)
                        {
                            Label label = (Label)obj;
                            label.ImageIndex = 1;
                            label.ForeColor = Color.Black;
                            foreach (var obj1 in label.Controls)
                            {
                                Label label1 = (Label)obj1;
                                label.ForeColor = Color.Black;
                            }
                        }
                    }
                    index = value;
                }
            }
        }

        internal void AddTitle(ContextMenuStrip item)
        {
            item.ItemClicked += ItemClick;
            titles.Add(item);
        }

        public ContextMenuStrip gettitle(int index)
        {
            if (titles.Count > 0)
                return titles[index];
            else
                return null;
        }

        public void setMenu(ContextMenuStrip item, int index)
        {
            item.ItemClicked += ItemClick;
            this.titles[index] = item;
        }

        private void CloseMouseHover(object sender, MouseEventArgs e)
        {
            Label label = (Label)sender;
            label.BackColor = Color.LavenderBlush;
        }

        private void CloseMouseLeave(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.BackColor = Color.Transparent;
        }

    }
}
