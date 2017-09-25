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
        List<Button> buttons = new List<Button>();


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
            this.Height = 700;

            //让WebBrowser以ie9模式运行，用于支持html5

            fixWebBrowserVersion("vine-window-standard.vshost.exe", ieMinVersion);
            String appName = System.IO.Path.GetFileName(Application.ExecutablePath).ToLower();
            fixWebBrowserVersion(appName, ieMinVersion);

            webBrowser1.Url = new Uri(String.Format("https://m.knowall.cn?CLIENTID={0:G}", "jasonpc"));

            //调整初始化窗口
            fixWindowSize();
            this.FormBorderStyle = FormBorderStyle.None;

            //初始化
            pageControl = new PageControl(this, this.panel2);
            pageControl.AddItem(webBrowser1);
            buttons.Add(btnPage1);
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

            String target = wb.Document.ActiveElement.GetAttribute("target");
            createWindow(url);
        }

        private void page1_Click(object sender, EventArgs e)
        {
            pageControl.Index = buttons.IndexOf((Button)sender);
        }

        private void page2_Click(object sender, EventArgs e)
        {
            createWindow("https://m.knowall.cn");
        }

        private void createWindow(String url)
        {
            Button last = buttons[buttons.Count - 1];

            Button button = new Button();
            button.Parent = this.panel1;
            button.Visible = true;
            button.Top = last.Top;
            button.Left = last.Left + last.Width + 10;
            button.Click += page1_Click;
            button.Height = last.Height;
            button.Width = last.Width;
            buttons.Add(button);
            button.Text = String.Format("Sheet{0:G}", buttons.Count);
            btnNew.Left = button.Left + button.Width + 10;

            pageControl.addItem();
            pageControl.browser.NewWindow += this.webBrowser1_NewWindow;
            pageControl.browser.DocumentCompleted += this.webBrowser1_DocumentCompleted;
            pageControl.browser.Url = new Uri(url);
        }

        private void btnSystemButtonClick(object sender, EventArgs e)
        {
            if (sender == btnClose) //关闭当前浏览页
            {
                if (buttons.Count > 1)
                {
                    Button button = buttons[buttons.Count - 1];
                    buttons.Remove(button);
                    button.Dispose();
                    pageControl.Index = buttons.Count - 1;
                    pageControl.Delete(pageControl.Count - 1);

                    Button last = buttons[buttons.Count - 1];
                    btnNew.Left = last.Left + last.Width + 10;
                }
            }
            else if (sender == btnExit) //退出系统
            {
                Application.Exit();
            }
            else if (sender == btnMin) //最小化
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = (WebBrowser)sender;
            var index = pageControl.Items.IndexOf(browser);
            buttons[index].Text = browser.DocumentTitle;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
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
