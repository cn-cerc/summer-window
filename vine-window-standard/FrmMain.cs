using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vine_window_standard
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FrmMain : Form
    {
        PageControl pageControl;
        TitleControl titles;

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        [DllImport("user32.dll")]
        private extern static bool ReleaseCapture();
        [DllImport("user32.dll")]
        private extern static int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        internal void loadUrl(string url)
        {
            webBrowser1.Navigate(url);
        }

        public FrmMain()
        {
            InitializeComponent();

            webBrowser1.Url = new Uri(String.Format("{0:G}?CLIENTID={1:G}", MyApp.HOME_URL, Computer.getClientID()));

            //调整初始化窗口
            this.FormBorderStyle = FormBorderStyle.None;
            fixWindowSize();

            //初始化
            pageControl = new PageControl(this, this.plBody);
            pageControl.AddItem(webBrowser1);
            titles = new TitleControl(this.plTitle);
            titles.BackColor = SystemColors.GradientActiveCaption;
            titles.GoClick = goPageClick;
            titles.CloseClick = closePageClick;
            lblFirstTitle.Click += goPageClick;
            lblFirstTitle.Text = MyApp.APP_NAME;
            titles.AddItem(btnPage);

            btnNew.Click += this.newPageClick;
        }

        private void fixWindowSize()
        {
            int iActulaWidth = Screen.PrimaryScreen.Bounds.Width;
            this.MinimumSize = new Size(1000, 800);
            switch (iActulaWidth)
            {
                case 1360:
                case 1366:
                    this.MaximumSize = new Size(iActulaWidth, Screen.PrimaryScreen.WorkingArea.Height);
                    this.Top = 0;
                    this.Left = 0;
                    this.Width = iActulaWidth;
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    btnMax.Visible = false;
                    break;
                default:
                    this.MaximumSize = new Size(1366, Screen.PrimaryScreen.WorkingArea.Height);
                    this.Top = 0;
                    this.Width = 1366;
                    this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
                    if (Screen.PrimaryScreen.WorkingArea.Height > 800)
                        this.Height = 800;
                    else
                        this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    break;
            }
        }

        private void remaxForm()
        {
            if (Screen.PrimaryScreen.WorkingArea.Height > 800)
            {
                if (this.Height == 800)
                {
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    this.Top = 0;
                }
                else
                {
                    this.Height = 800;
                    this.Top = (Screen.PrimaryScreen.WorkingArea.Height - 800) / 2;
                }
            }
            else
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            WebBrowser wb = (WebBrowser)sender;
            string url = wb.Document.ActiveElement.GetAttribute("href");

            //String target = wb.Document.ActiveElement.GetAttribute("target");
            //buttons[pageControl.Index].Text = target;
            createWindow(url);
        }

        private void goPageClick(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                Label label = (Label)sender;
                titles.Index = titles.IndexOf(label.Parent);
            }
            else
            {
                titles.Index = titles.IndexOf((Control)sender);
            }
            pageControl.Index = titles.Index;
        }

        private void newPageClick(object sender, EventArgs e)
        {
            createWindow(MyApp.getInstance().getFormUrl("WebDefault"));
        }

        private void createWindow(String url)
        {
            Control button = titles.AddItem();
            button.Click += goPageClick;
            btnNew.Left = button.Left + button.Width + 10;
            pageControl.addItem();
            pageControl.browser.NewWindow += this.webBrowser1_NewWindow;
            pageControl.browser.DocumentCompleted += this.webBrowser1_DocumentCompleted;
            pageControl.browser.Url = new Uri(url);
        }

        private void closePageClick(object sender, EventArgs e)
        {
            Control item = (Control)((Control)sender).Tag;
            int index = titles.IndexOf(item);
            titles.Remove(index);
            pageControl.Delete(index);
            pageControl.Index = index - 1;
            titles.Index = index - 1;

            Control last = titles.getItem(titles.Count - 1);
            btnNew.Left = last.Left + last.Width + 10;
        }

        private void btnSystemButtonClick(object sender, EventArgs e)
        {
            if (sender == btnExit) //退出系统
            {
                Application.Exit();
            }
            else if (sender == btnMin) //最小化
            {
                this.WindowState = FormWindowState.Minimized;
            }
            else if (sender == btnMax) //最大化
            {
                remaxForm();
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = (WebBrowser)sender;
            var index = pageControl.Items.IndexOf(browser);
            titles.setTitle(index, browser.DocumentTitle);
        }

        private void plTitle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            remaxForm();
        }

        private void plTitle_MouseMove(object sender, MouseEventArgs e)
        {
            //模拟点击窗体的Title
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();//释放窗体的鼠标焦点
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }
    }
}
