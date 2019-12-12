using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vine_window_standard
{
    public class BookMark
    {
       // public Button btImage;
        public Label btImage;
        public Button btPage;
        public double scale;

        public BookMark()
        {

        }

        /// <summary>
        /// 书签路径
        /// </summary>
        private string bookUrl;
        
        public string BookUrl
        {
            get { return bookUrl; }
            set { bookUrl = value; }
        }

        /// <summary>
        /// 书签标题
        /// </summary>
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private float dpiX;

        public float DpiX
        {
            get { return dpiX; }
            set { dpiX = value; }
        }

        public void AddButton(Panel parent, int Index, int MaxLeft)
        {
            if (dpiX == 96)
                scale = 1;
            else if (dpiX == 120)
                scale = 1.25;
            else if (dpiX == 144)
                scale = 1.5;
            else if (dpiX == 192)
                scale = 2;
            btImage = new Label();
            btImage.Parent = parent;
            btImage.Height = (int)(24 * scale);
            btImage.Width = (int)(24 * scale);
            btImage.Top = (int)(6 * scale);
            btImage.Left = MaxLeft;
            MaxLeft = MaxLeft + btImage.Width - (int)(2 * scale);
            btImage.BackColor = System.Drawing.Color.Transparent;
            btImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btImage.Image = global::vine_window_standard.Properties.Resources.BookMark;
            btImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btImage.BackColor = System.Drawing.Color.Transparent;
            btImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;

            btPage = new Button();
            btPage.Parent = parent;
            btPage.Height = (int)(24 * scale);
            btPage.Top = (int)(6 * scale);
            btPage.Width = (int)(85 * scale);
            btPage.Left = MaxLeft;
            if (title.Length >= 6)
            {
                //btPage.Text = getStr(title, 12, "");
                btPage.Text = title;
                btPage.Width = (int)(85 * scale) + 1;
            }
            else
            {
                btPage.Text = title;
                btPage.Width = (int)((btPage.Text.Length * 10 + 25) * scale) + 1;
            }
            MaxLeft = MaxLeft + btPage.Width;
            btPage.Tag = Index;
            btPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btPage.FlatAppearance.BorderSize = 0;
            btPage.BackColor = System.Drawing.Color.Transparent;
            btPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            btPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            btPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            btPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btPage.ForeColor = System.Drawing.Color.Silver;
            btPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        }

        public static string getStr(string s, int l, string endStr)
        {
            string temp = s.Substring(0, (s.Length < l + 1) ? s.Length : l + 1);
            byte[] encodedBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(temp);

            string outputStr = "";
            int count = 0;

            for (int i = 0; i < temp.Length; i++)
            {
                if ((int)encodedBytes[i] == 63)
                    count += 2;
                else
                    count += 1;

                if (count <= l - endStr.Length)
                    outputStr += temp.Substring(i, 1);
                else if (count > l)
                    break;
            }

            if (count <= l)
            {
                outputStr = temp;
                endStr = "";
            }

            outputStr += endStr;

            return outputStr;
        }
    }
}
