using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
        private delegate void HttpOnResponse(WebClient client, String resp);
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

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
            lblFirstTitle.ImageList = ilTitle;
            lblFirstTitle.ImageIndex = 0;
            plTitle.BackColor = Color.FromArgb(255, 56, 154, 218);
            plSystem.BackColor = plTitle.BackColor;
            pageControl = new PageControl(this, this.plBody);
            pageControl.AddItem(webBrowser1);
            titles = new TitleControl(this.plTitle, lblFirstTitle);
            titles.GoClick = goPageClick;
            titles.CloseClick = closePageClick;
            lblFirstTitle.Click += goPageClick;
            lblFirstTitle.Text = MyApp.APP_NAME;
            titles.AddItem(btnPage);
            
            titles.ItemClick = mnuTitle_ItemClicked;
            titles.AddTitle(mnuTitle);

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
                    //btnMax.Visible = false;
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
            {
                this.Left = 0;
                this.Top = 0;
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            }
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

            var control = (Control)sender;
            mnuTitle = titles.gettitle(titles.index);
            if (mnuTitle.Items.Count > 1)
                mnuTitle.Items[1].Visible = titles.Index > 0;
            mnuTitle.Show(control, new Point(4, control.Height - 5));
            if (pageControl.browser.DocumentTitle != pageControl.browser.Url.ToString())
                titles.setTitle(pageControl.Index, pageControl.browser.DocumentTitle);
            else
                titles.setTitle(pageControl.Index, "打印报表");
        }

        private void newPageClick(object sender, EventArgs e)
        {
            createWindow(MyApp.getInstance().getFormUrl("WebDefault"));
        }

        private void createWindow(String url)
        {
            int owenIndex = titles.Index;
            Control button = titles.AddItem();
            ContextMenuStrip newtitle = new ContextMenuStrip();
            TitleInit(newtitle);
            titles.AddTitle(newtitle);
            titles.OwenIndex = owenIndex;
            button.Click += goPageClick;
            btnNew.Left = button.Left + button.Width;
            pageControl.addItem();
            pageControl.browser.NewWindow += this.webBrowser1_NewWindow;
            pageControl.browser.DocumentCompleted += this.webBrowser1_DocumentCompleted;
            pageControl.browser.Url = new Uri(url);
            
        }

        private void closePageClick(object sender, EventArgs e)
        {
            Control item = (Control)((Control)sender).Tag;
            int index = titles.IndexOf(item);
            pageControl.Delete(index);
            titles.Remove(index);
            pageControl.Index = index - 1;
            titles.Index = index - 1;

            Control last = titles.getItem(titles.Count - 1);
            btnNew.Left = last.Left + last.Width + 10;
            registerTitle();
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
            if (browser.DocumentTitle != browser.Url.ToString())
                titles.setTitle(index, browser.DocumentTitle);
            else
                titles.setTitle(index, "打印报表");
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
            WebBrowser wb = pageControl.Items[pageControl.Index];
            if (wb.CanGoBack == true)
                wb.GoBack();
            else
            {
                if (titles.Index != 0)
                {
                    int index = titles.Index;
                    int owenIndex = titles.OwenIndex;
                    if (owenIndex >= titles.Count - 1)
                        owenIndex = titles.index - 1;
                    titles.Remove(index);
                    pageControl.Delete(index);
                    pageControl.Index = owenIndex;
                    titles.Index = owenIndex;

                    Control last = titles.getItem(titles.Count - 1);
                    btnNew.Left = last.Left + last.Width + 10;
                }
            }
        }

        private void mnuTitle_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int it = mnuTitle.Items.IndexOf(e.ClickedItem);
            switch (it)
            {
                case 0: //转到首页
                    {
                        string url = MyApp.getInstance().getFormUrl("WebDefault");
                        pageControl.browser.Url = new Uri(url);
                        break;
                    }
                case 1: //close
                    {
                        if (titles.Index == 0)
                            break;
                        int index = titles.Index;
                        titles.Remove(index);
                        pageControl.Delete(index);
                        pageControl.Index = index - 1;
                        titles.Index = index - 1;

                        Control last = titles.getItem(titles.Count - 1);
                        btnNew.Left = last.Left + last.Width + 10;
                        break;
                    }
                default:
                    {
                        string url = (string)mnuTitle.Items[it].Tag;
                        if (url != "") 
                            createWindow(MyApp.getInstance().getFormUrl(url));
                        break;
                    }
            }
        }

        private void btnSetup_Click(object sender, EventArgs e)
        {
            var control = (Button)sender;
            mnuSetup.Items[0].Visible = false; //暂关闭
            mnuSetup.Show(control, new Point(-control.Width-56, control.Height + 1));
        }

        private void mnuSetup_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int it = mnuSetup.Items.IndexOf(e.ClickedItem);
            switch (it)
            {
                case 0: //设置
                    {
                        break;
                    }
                case 1: //退出
                    {
                        Application.Exit();
                        break;
                    }
                case 2: //刷新
                    {
                        WebBrowser wb = pageControl.Items[pageControl.Index];
                        wb.Document.ExecCommand("Refresh", false, null);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public String send(String classCode, String req)
        {
            JavaScriptProxy proxy = new JavaScriptProxy(this);
            JObject result = proxy.execute(classCode, req);
            return result.ToString();
        }

        public void TitleInit(ContextMenuStrip title)
        {
            title.Items.Clear();
            title.Items.Add("转到首页");
            title.Items.Add("关闭");
        }

        protected override CreateParams CreateParams
        {
            // 允许点击任务栏图标正常最小化或还原窗体
            get
            {
                const int WS_MINIMIZEBOX = 0x00020000;
                CreateParams cp = base.CreateParams;
                cp.Style = cp.Style | WS_MINIMIZEBOX;   
                return cp;
            }
        }

        public void SetTitle(ContextMenuStrip title)
        {
            titles.setMenu(title, titles.index);
        }

        public void heartbeatCheck(bool status, int time)
        {
            this.heartbeat.Interval = time * 60000;
            this.heartbeat.Enabled = status;
        }
        
        private void heartbeat_Tick(object sender, EventArgs e)
        {
            ThreadStart thread = () =>
            {
                string formCode = String.Format("WebDefault.heartbeatCheck?CLIENTID={0}&device={1}&sid={2}", Computer.getClientID(), "pc", MyApp.getInstance().getToken());
                string url = MyApp.getInstance().getFormUrl(formCode);
                var client = new WebClient();
                client.Encoding = System.Text.Encoding.GetEncoding("utf-8");
                string resp = client.DownloadString(url);

                HttpOnResponse httpResp = httpOnResponse;
                this.Invoke(httpResp, client, resp);
            };
            new Thread(thread).Start();
        }

        private void httpOnResponse(WebClient client, string resp)
        {
            ;
        }

        public void RefreshHost(string sid, string host)
        {
            int index = titles.Index;
            WebBrowser wb = pageControl.Items[index];
            string newUrl = host + wb.Url.AbsolutePath + String.Format("?CLIENTID={0}&device={1}&sid={2}", Computer.getClientID(), "pc", sid);
            createWindow(newUrl);

            titles.Remove(index);
            pageControl.Delete(index);

            Control last = titles.getItem(titles.Count - 1);
            btnNew.Left = last.Left + last.Width + 10;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;//按快捷键  
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:    //按下的是F5
                            {
                                //模拟按下Alt键
                                keybd_event(HotKey.vbKeyAlt, 0, 0, 0);
                                //模拟按下F5键
                                keybd_event(HotKey.vbKeyF5, 0, 0, 0);
                                //松开按键Alt
                                keybd_event(HotKey.vbKeyAlt, 0, 2, 0);
                                //松开按键F5
                                keybd_event(HotKey.vbKeyF5, 0, 2, 0);
                            }
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        private void FrmMain_Activated(object sender, EventArgs e)
        {
            HotKey.RegisterHotKey(Handle, 100, 0, Keys.F5);
        }

        private void FrmMain_Leave(object sender, EventArgs e)
        {
            HotKey.UnregisterHotKey(Handle, 100);
        }

        private void registerTitle()
        {
            for (int i = 0; i < pageControl.Items.Count; i++)
            {
                WebBrowser browser = (WebBrowser)pageControl.Items[i];
                if (browser.DocumentTitle != browser.Url.ToString())
                    titles.setTitle(i, browser.DocumentTitle);
                else
                    titles.setTitle(i, "打印报表");
            }
        }
    }
}
