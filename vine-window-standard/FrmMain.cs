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
        private static Int32 ieMinVersion = 11001;
        PageControl pageControl;
        TitleControl titles;

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        [DllImport("user32.dll")]
        private extern static bool ReleaseCapture();
        [DllImport("user32.dll")]
        private extern static int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public FrmMain()
        {
            InitializeComponent();
            this.Width = 1360;
            this.Height = 768;

            //让WebBrowser以ie9模式运行，用于支持html5

            fixWebBrowserVersion("vine-window-standard.vshost.exe", ieMinVersion);
            String appName = System.IO.Path.GetFileName(Application.ExecutablePath).ToLower();
            fixWebBrowserVersion(appName, ieMinVersion);

            webBrowser1.Url = new Uri(String.Format("{0:G}?CLIENTID={1:G}", MyApp.HOME_URL, "jasonpc"));

            //调整初始化窗口
            fixWindowSize();
            this.FormBorderStyle = FormBorderStyle.None;

            //初始化
            pageControl = new PageControl(this, this.plBody);
            pageControl.AddItem(webBrowser1);
            titles = new TitleControl(this.plTitle);
            titles.BackColor = SystemColors.GradientActiveCaption;
            titles.GoClick = goPageClick;
            titles.CloseClick = closePageClick;
            lblFirstTitle.Click += goPageClick;
            titles.AddItem(btnPage);

            btnNew.Click += this.newPageClick;
        }

        private void fixWindowSize()
        {
            int iActulaWidth = Screen.PrimaryScreen.Bounds.Width;
            if (iActulaWidth < 1360)
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("您的屏幕分辨率过低，地藤无法正常运行，要退出吗?", "环境检测", messButton);

                if (dr == DialogResult.OK)//如果点击“确定”按钮
                {
                    System.Environment.Exit(0);
                }
            }
            else
            {
                if (iActulaWidth >= 1360 && iActulaWidth <= 1366)
                {
                    this.Top = 0;
                    this.Left = (iActulaWidth - 1360) / 2;
                    this.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    this.MaximumSize = new Size(1366, Screen.PrimaryScreen.Bounds.Height);
                }
            }
        }

        private static void fixWebBrowserVersion(String appName, Int32 ieMinVer)
        {
            String section = "Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION";
            RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;
            RegistryKey hkie = hklm.OpenSubKey(section, true);

            Object val = hkie.GetValue(appName);
            Int32 curVer = val != null ? (Int32)val : 0;
            if (curVer <= ieMinVer)
            {
                hkie.SetValue(appName, ieMinVer);
            }
            hkie.Close();
            hklm.Close();
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
            createWindow(MyApp.getFormUrl("WebDefault"));
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

        private void remaxForm()
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
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
    }
}
