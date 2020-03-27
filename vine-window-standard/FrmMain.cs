using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static vine_window_standard.MyWebBrowser;

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

        public bool isFastPrint;
        public string defaultPrinter;
        private int Factor = 100;
        private int iActulaWidth = 1366;
        private bool isChangeFactor = false;
        string printing = "";
        //
        int defaultTabWidth = 130;
        int tabWidth = 0;
        int MaxLeft = 192;

        //书签列
        List<BookMark> BookReamrkList = new List<BookMark>();

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
            //lblFirstTitle.ImageList = ilTitle;
            lblFirstTitle.Image = vine_window_standard.Properties.Resources.title_light;
            //plTitle.BackColor = Color.FromArgb(255, 56, 154, 218);
            plSystem.BackColor = plTitle.BackColor;
            plBookmark.BackColor = plTitle.BackColor;
            pageControl = new PageControl(this, this.plBody);
            pageControl.AddItem(webBrowser1);
            titles = new TitleControl(this.plTitle, lblFirstTitle);
            titles.GoClick = goPageClick;
            titles.CloseClick = closePageClick;
            titles.MouseClick = mouseClick;
            lblFirstTitle.Click += goPageClick;
            lblFirstTitle.Text = MyApp.APP_NAME;
            lblFirstTitle.MouseClick += mouseClick;
            titles.AddItem(btnPage);
            
            //titles.ItemClick = mnuTitle_ItemClicked;
            //titles.AddTitle(mnuTitle);

            //btnNew.Click += this.newPageClick;

            //判断是否最大化
            XmlHelper xh = new XmlHelper();
            if (xh.ReadIsMaxForm() == "1")
            {
                this.Left = 0;
                this.Top = 0;
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            }

            SetBookRemark();
            widthCalculate();
             ////获取放大缩小比例
             //XmlHelper xh = new XmlHelper();
             Factor = int.Parse(xh.ReadZoom());

            tabWidth = defaultTabWidth;

            HttpDldFile hd = new HttpDldFile();
            hd.DeleteDir(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\vine-windows-standard\\Report");
        }

        private void fixWindowSize()
        {
            iActulaWidth = Screen.PrimaryScreen.Bounds.Width;
            this.MinimumSize = new Size(1000, 800);
            this.MaximumSize = new Size(iActulaWidth, Screen.PrimaryScreen.Bounds.Height);
            switch (iActulaWidth)
            {
                case 1360:
                case 1366:
                    //this.MaximumSize = new Size(iActulaWidth, Screen.PrimaryScreen.WorkingArea.Height);
                    this.Top = 0;
                    this.Left = 0;
                    this.Width = iActulaWidth;
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
                    //btnMax.Visible = false;
                    break;
                default:
                    //this.MaximumSize = new Size(1366, Screen.PrimaryScreen.WorkingArea.Height);
                    iActulaWidth = 1366;
                    this.Top = 0;
                    this.Left = 0;
                    this.Width = 1366;
                    this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
                    if (Screen.PrimaryScreen.WorkingArea.Width > 1366)
                        this.Width = 1366;
                    else
                        this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    if (Screen.PrimaryScreen.WorkingArea.Height > 800)
                        this.Height = 800;
                    else
                        this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
                    break;
            }
        }

        private void remaxForm()
        {
            if (this.Width == Screen.PrimaryScreen.WorkingArea.Width)
            {
                fixWindowSize();
                XmlHelper xh = new XmlHelper();
                xh.WriteIsMaxForm("0");
                //ConfigHelper.UpdateAppConfig("IsmaxForm", "0");
                widthCalculate(true);
                btnMax.BackgroundImage = vine_window_standard.Properties.Resources.Full;
                MarkList();
            }
            else
            {
                //最大化
                this.Left = 0;
                this.Top = 0;
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                XmlHelper xh = new XmlHelper();
                xh.WriteIsMaxForm("1");
                //ConfigHelper.UpdateAppConfig("IsmaxForm", "1");
                widthCalculate(true);
                btnMax.BackgroundImage = vine_window_standard.Properties.Resources.Narrow;
                MarkList();
            }
        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            WebBrowser wb = (WebBrowser)sender;
            string url = wb.Document.ActiveElement.GetAttribute("href");
            //String target = wb.Document.ActiveElement.GetAttribute("target");
            //buttons[pageControl.Index].Text = target;
            if (isFastPrint)
            {
                Print(defaultPrinter, url);
            }
            else
            {
                if ((url == "") && (wb.StatusText != ""))
                    url = wb.StatusText;
                createWindow(url);
            }
        }

        private void goPageClick(object sender, EventArgs e)
        {
            cleanPrintItem();
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
            WebBrowser wb = pageControl.browser;
            //mnuTitle = titles.gettitle(titles.index);
            //if (mnuTitle.Items.Count > 1)
            //    mnuTitle.Items[1].Visible = titles.Index > 0;
            //mnuTitle.Show(control, new Point(4, control.Height - 5));
            titles.setTitle(pageControl.Index, wb.DocumentTitle);
            if (wb.DocumentTitle.IndexOf("http") > -1)
                titles.setTitle(pageControl.Index, "打印报表");
        }

        private void newPageClick(object sender, EventArgs e)
        {
            if (pageControl.Index == 0)
            {
                WebBrowser wb = pageControl.Items[pageControl.Index];
                wb.Navigate(String.Format("{0:G}?CLIENTID={1:G}", MyApp.getInstance().getFormUrl("WebDefault"), Computer.getClientID()));
            }
            else { 
                pageControl.Index = 0;
                titles.Index = 0;
            }
            //createWindow(String.Format("{0:G}?CLIENTID={1:G}", MyApp.getInstance().getFormUrl("WebDefault"), Computer.getClientID()));
        }

        private void createWindow(String url)
        {
            cleanPrintItem();
            int owenIndex = titles.Index;
            titles.isHide = false;
            Control button = titles.AddItem();
            ContextMenuStrip newtitle = new ContextMenuStrip();
            TitleInit(newtitle);
            titles.AddTitle(newtitle);
            titles.OwenIndex = owenIndex;
            button.Click += goPageClick;
            //btnNew.Left = button.Left + button.Width + 10;
            pageControl.isHide = false;
            pageControl.addItem();
            pageControl.browser.NewWindow += this.webBrowser1_NewWindow;
            pageControl.browser.DocumentCompleted += this.webBrowser1_DocumentCompleted;
            //Zoom(pageControl.browser, 0);
            pageControl.browser.Url = new Uri(url);
            widthCalculate(true);
        }
        
        private void closePageClick(object sender, EventArgs e)
        {
            cleanPrintItem();
            Control item = (Control)((Control)sender).Tag;
            int index = titles.IndexOf(item);
            pageControl.Delete(index);
            titles.Remove(index);
            pageControl.Index = index - 1;
            titles.Index = index - 1;

            Control last = titles.getItem(titles.Count - 1);
            //btnNew.Left = last.Left + last.Width + 10;
            widthCalculate(false);
            registerTitle();
        }

        private void btnSystemButtonClick(object sender, EventArgs e)
        {
            if (sender == btnExit) //退出系统
            {
                if (MessageBox.Show("您将退出地藤系统,是否继续？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
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
            if (browser.DocumentTitle.IndexOf("http") > -1)
            {
                titles.setTitle(index, "打印报表");
                if (timer1.Enabled)
                    timer1.Stop();
            }
            if (browser.ReadyState == WebBrowserReadyState.Complete)
                Zoom(browser, 0);
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
                    //btnNew.Left = last.Left + last.Width + 10;
                    registerTitle();
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
                        if (pageControl.Index != 0)
                        { 
                            pageControl.Index = 0;
                            titles.Index = 0;
                        }
                        //string url = MyApp.getInstance().getFormUrl("WebDefault");
                        //string url = String.Format("{0:G}?CLIENTID={1:G}", MyApp.getInstance().getFormUrl("WebDefault"), Computer.getClientID());
                        //pageControl.browser.Url = new Uri(url);
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
                        //btnNew.Left = last.Left + last.Width + 10;
                        registerTitle();
                        widthCalculate(false);
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
            mnuSetup.Show(control, new Point(control.Width-39, control.Height + 1));
        }

        private void mnuSetup_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            isChangeFactor = false;
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
                case 3:
                    {
                        cleanPrintItem();
                        //WebBrowser wb = pageControl.Items[0];
                        //string url = String.Format("{0:G}?CLIENTID={1:G}", MyApp.getInstance().getFormUrl("WebDefault"), Computer.getClientID());
                        //wb.Url = new Uri(url);
                        //关闭其他页面
                        for (int i = pageControl.Count-1; i>0 ;i--)
                        {
                            closeTitle(i);
                        }
                        //转到首页
                        pageControl.Index = 0;
                        titles.Index = 0;
                        widthCalculate(false);
                        break;
                    }
                case 5:
                    {
                        WebBrowser wb = pageControl.Items[pageControl.Index];
                        wb.Focus();
                        Zoom(wb, (100 - Factor));
                        break;
                    }
                case 6:
                    {
                        WebBrowser wb = pageControl.Items[pageControl.Index];
                        Zoom(wb, 10);
                        isChangeFactor = true;
                        break;
                    }
                case 7:
                    {
                        WebBrowser wb = pageControl.Items[pageControl.Index];
                        Zoom(wb, -10);
                        isChangeFactor = true;
                        break;
                    }
                case 8:
                    {
                        WebBrowser wb = pageControl.Items[pageControl.Index];
                        XmlHelper xh = new XmlHelper();
                        String title = wb.DocumentTitle;
                        if (xh.WriteXML(wb.Url.ToString(), title))
                        { 
                            //
                            float dpiX;
                            Graphics graphics = this.CreateGraphics();
                            dpiX = graphics.DpiX;
                            BookMark br1 = new BookMark();
                            BookReamrkList.Add(br1);
                            br1.Title = wb.DocumentTitle;
                            br1.BookUrl = wb.Url.ToString();
                            br1.DpiX = dpiX;
                            br1.AddButton(plBookmark, BookReamrkList.Count-1, MaxLeft);

                            if (title.Length > 6)
                                br1.btPage.Text = BookMark.getStr(title, 12, "");
                            else
                                br1.btPage.Text = title;
                            br1.btPage.Width = br1.btPage.Text.Length * 16 + 1;
                            br1.btPage.Click += butbk_Click;
                            br1.btPage.ContextMenuStrip = brmum;
                            //br1.btImage.ImageIndex = 3;
                            //br1.btImage.ImageList = this.ilTop;
                            MaxLeft = MaxLeft + br1.btImage.Width + br1.btPage.Width;
                            if (br1.btImage.Left > plBookmark.Width - plSystem.Width)
                                addMarkList(br1.Title, (int)br1.btPage.Tag);
                            //SetBookRemark();
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        public void Zoom(WebBrowser brower, int Addfactor)
        {
            Control item = titles.getItem(titles.index);
            string cTitle = "";
            if (item.Controls.Count > 0)
                cTitle = item.Controls[0].Text;
            else
                cTitle = item.Text;
            if (cTitle !=  "打印报表")
            {
                Factor = Factor + Addfactor;
                object pvaIn = Factor;
                try
                {
                    if (Factor != 100)
                        ((IWebBrowser2)brower.ActiveXInstance).ExecWB(OLECMDID.OLECMDID_OPTICAL_ZOOM,
                            OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER,
                            100, IntPtr.Zero);
                    ((IWebBrowser2)brower.ActiveXInstance).ExecWB(OLECMDID.OLECMDID_OPTICAL_ZOOM,
                        OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER,
                        ref pvaIn, IntPtr.Zero);
                    brower.Document.Window.ScrollTo(0, 0);
                    toolStripMenuItem1.Text = "重置  " + Factor.ToString() + "%";
                    XmlHelper xh = new XmlHelper();
                    xh.WriteZoom(Factor.ToString());
                    //ConfigHelper.UpdateAppConfig("ZOOM", Factor.ToString());
                }
                catch (Exception)
                {
                    //throw;
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
            //title.Items.Clear();
            //title.Items.Add("转到首页");
            //title.Items.Add("关闭");
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

            pageControl.Index = 0;
            titles.Index = 0;
            Label label = (Label)titles.getItem(0).Controls[0];
            label.Image = global::vine_window_standard.Properties.Resources.title_light;
            Control last = titles.getItem(titles.Count - 1);
            //btnNew.Left = last.Left + last.Width + 10;
        }

        protected override void WndProc(ref Message m)
        {
            StringBuilder title = new StringBuilder();
            title.Length = 100;

            const int WM_HOTKEY = 0x0312;//按快捷键  
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:    //按下的是F5
                            {
                                //Alt+F5
                                SendKeys.Send("^{F5}");
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
            int conCount = pageControl.Items.Count;
            //重置标题
            for (int i = 0; i < conCount; i++)
            {
                WebBrowser browser = (WebBrowser)pageControl.Items[i];
                titles.setTitle(i, browser.DocumentTitle);
                if (browser.DocumentTitle.IndexOf("http") > -1)
                    titles.setTitle(i, "打印报表");
            }
        }
        
        private void closeTitle(int index)
        {
            titles.Remove(index);
            if (titles.Count > 0)
            {
                titles.Index = titles.Count - 1;
                Control last = titles.getItem(titles.Count - 1);
            }
            pageControl.Delete(index);
            if (index >0)
                pageControl.Index = index-1;

            registerTitle();
        }

        public class Param
        {
            public ManualResetEvent mrEvent;
            public string strUrl;
            public int i;
        }

        List<ManualResetEvent> manualEvents = new List<ManualResetEvent>();
        List<WebBrowser> printList = new List<WebBrowser>();
        List<String> urlList = new List<String>();

        /// <summary>
        /// 打印一次
        /// </summary>
        /// <param name="Printer"></param>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public bool Print(string Printer, string strUrl)
        {
            ////https://c1.diteng.site/forms/TFrmTranBC.exportPdf?reportNum=1&tbNo=BC180919116&reportRptHead=%E7%8B%BC%E7%8E%8B%E6%B8%94%E5%85%B7&tb=BC
            isFastPrint = false;
            bool result = false;
            string keyName = @"Software\Microsoft\Internet Explorer\PageSetup\";
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName, true))
            {
                if (key != null)
                {
                    key.SetValue("footer", "");  //设置页脚为空
                    key.SetValue("header", "");  //设置页眉为空
                    key.SetValue("Print_Background", true); //设置打印背景颜色
                    key.SetValue("margin_bottom", 0);  //设置下页边距为0
                    key.SetValue("margin_left", 0);   //设置左页边距为0
                    key.SetValue("margin_right", 0);  //设置右页边距为0
                    key.SetValue("margin_top", 0);   //设置上页边距为0

                    //设置默认打印机
                    if (Printer != "")
                        Externs.SetDefaultPrinter(Printer);
                    Console.WriteLine("接收打印信息 " + DateTime.Now.ToString());
                    //下载pdf文件并打印
                    string subPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\vine-windows-standard\\Report";

                    strUrl = String.Format("{0:G}&sid={1:G}", strUrl, MyApp.getInstance().getToken());

                    Random random = new Random();
                    HttpDldFile hdf = new HttpDldFile();
                    string fileName = string.Format("{0}.pdf", DateTime.Now.ToString("yyyyMMddHHmmss")+ random.Next());
                    subPath = subPath + "\\" + fileName;
                    Console.WriteLine("开始下载 " + DateTime.Now.ToString());
                    if (hdf.DownloadPdf(strUrl, subPath))
                    {

                        Console.WriteLine("下载成功 " + DateTime.Now.ToString());
                        if (!timer1.Enabled)
                            timer1.Start();
                        var doc = new PdfDocument();
                        doc.Pages.Add();
                        doc.Pages.RemoveAt(0);
                        doc.LoadFromFile(subPath);
                        PrintDocument printDoc = doc.PrintDocument;
                        printDoc.PrinterSettings.PrinterName = Printer;
                        printDoc.PrintController = new StandardPrintController();
                        printDoc.Print();
                        result = true;
                    }
                }
            }
            return result;
        }

        private void cleanPrintList()
        {
            for(int i = 0; i< printList.Count - 1; )
            {
                if ((int)printList[i].Tag == -1)
                {
                    printList.Remove(printList[i]);
                }
                else
                    i--;
            }
        }

        /// <summary>
        /// 批次打印
        /// </summary>
        /// <param name="Printer"></param>
        /// <param name="strUrls"></param>
        /// <returns></returns>
        public bool PrintList(string Printer, string strUrls)
        {
            isFastPrint = false;
            bool result = false;
            string keyName = @"Software\Microsoft\Internet Explorer\PageSetup\";
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName, true))
            {
                if (key != null)
                {
                    key.SetValue("footer", "");  //设置页脚为空
                    key.SetValue("header", "");  //设置页眉为空
                    key.SetValue("Print_Background", true); //设置打印背景颜色
                    key.SetValue("margin_bottom", 0);  //设置下页边距为0
                    key.SetValue("margin_left", 0);   //设置左页边距为0
                    key.SetValue("margin_right", 0);  //设置右页边距为0
                    key.SetValue("margin_top", 0);   //设置上页边距为0

                    //设置默认打印机
                    if (Printer != "")
                        Externs.SetDefaultPrinter(Printer);
                    
                    string subPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\vine-windows-standard\\Report";
                    string fileParh = "";
                    string strUrl = "";
                    string[] urlArray = strUrls.Split('|');
                    int i = printList.Count;
                    Random random = new Random();
                    foreach (string Url in urlArray)
                    {
                        strUrl = MyApp.getInstance().getFormUrl(String.Format("{0:G}&sid={1:G}", Url, MyApp.getInstance().getToken()));

                        HttpDldFile hdf = new HttpDldFile();
                        string fileName = string.Format("{0}.pdf", DateTime.Now.ToString("yyyyMMddHHmmss")+ random.Next());
                        fileParh = subPath + "\\" + fileName;
                        if (hdf.DownloadPdf(strUrl, fileParh))
                        {
                            if (!timer1.Enabled)
                                timer1.Start();
                            var doc = new PdfDocument();
                            doc.Pages.Add();
                            doc.Pages.RemoveAt(0);
                            doc.LoadFromFile(fileParh);
                            PrintDocument printDoc = doc.PrintDocument;
                            printDoc.PrinterSettings.PrinterName = Printer;
                            printDoc.PrintController = new StandardPrintController();
                            printDoc.Print();
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        public void widthCalculate(bool forAdd = true)
        {
            //调整页签标题宽度
            if (forAdd)
            {
                if (this.Width < (titles.Count) * (tabWidth) + 133 || tabWidth < defaultTabWidth)
                {
                    tabWidth = (this.Width - 133) / (titles.Count);
                }
            }
            else
            {
                if (this.Width < (titles.Count) * (tabWidth) + 133 || tabWidth < defaultTabWidth)
                {
                    if (titles.Count > 0)
                        tabWidth = (this.Width - 133) / (titles.Count);
                }
            }

            if (tabWidth > defaultTabWidth)
                tabWidth = defaultTabWidth;

            if (tabWidth != defaultTabWidth)
                titles.setWidch(tabWidth);
            else if (tabWidth != titles.getItem(0).Width)
                titles.setWidch(tabWidth);
        }

        public void cleanPrintItem()
        {
            if (printing == "")
                for (int i = 0; i < titles.Count; i++)
                {
                    Control item = titles.getItem(i);
                    if (!item.Visible)
                    {
                        titles.Remove(i);
                        pageControl.Delete(i);
                    }
                }
        }

        public void SetBookRemark()
        {
            //显示书签栏
            float dpiX;
            Graphics graphics = this.CreateGraphics();
            dpiX = graphics.DpiX;

            XmlHelper xh = new XmlHelper();
            xh.ReadXML();
            BookReamrkList = xh.ReadXML();
            int LastLeft = btnMange.Left+ btnMange.Width;
            if (BookReamrkList.Count > 0)
            {
                plBookmark.Controls.Clear();
                btnNew.Parent = plBookmark;
                btnNew1.Parent = plBookmark;
                btnSetup1.Parent = plBookmark;
                btnSetup.Parent = plBookmark;
                btnMange.Parent = plBookmark;
                btnMange1.Parent = plBookmark;
                if (lbMore.Visible)
                    lbMore.Visible = false;
                //plSystem.Parent = plBookmark;
                //plBookmark.Visible = true;
                //plBookmark.Height = 30;
                //plHead.Height = 60;
                for (int i=0;i<BookReamrkList.Count;i++)
                {
                    //BookReamrk br1 in BookReamrkList
                    BookMark br1 = BookReamrkList[i];
                    br1.DpiX = dpiX;
                    br1.AddButton(plBookmark, i, LastLeft);
                    br1.btPage.Click += butbk_Click;
                    br1.btPage.ContextMenuStrip = brmum;
                    //br1.btImage.ImageIndex = 3;
                    //br1.btImage.ImageList = this.ilTop;
                    if (LastLeft > plBookmark.Width - plSystem.Width)
                        addMarkList(br1.Title, i);
                    LastLeft = LastLeft + br1.btImage.Width + br1.btPage.Width;
                }
                MaxLeft = LastLeft;
            }
            else
            {
                xh.xmlInit();
                SetBookRemark();
            }
        }

        private void butbk_Click(object sender, EventArgs e)
        {
            //书签点击事件
            Button bt1 = (Button)sender;
            BookMark br1 = BookReamrkList[(int)bt1.Tag];
            string bookUrl = br1.BookUrl;
            int strform = bookUrl.IndexOf("forms");
            if (strform > 0)
            {
                string head = bookUrl.Substring(0, strform-1);
                if (head != MyApp.HOME_URL) { 
                    string name = bookUrl.Substring(strform, bookUrl.Length - strform);
                    bookUrl = String.Format("{0:G}/{1:G}", MyApp.HOME_URL, name);
                }
            }
            if (pageControl.Index ==0)
                createWindow(bookUrl);
            else
            { 
                WebBrowser wb = pageControl.Items[pageControl.Index];
                wb.Navigate(bookUrl);
            }
        }

        private void delTool_Click(object sender, EventArgs e)
        {
            //删除书签
            Button bt1 = (Button)((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl;
            int id = (int)bt1.Tag;
            BookMark br1 = BookReamrkList[id];
            int pLeft = br1.btImage.Left;
            XmlHelper xh = new XmlHelper();
            xh.DelectNode(br1.BookUrl, br1.Title);
            BookReamrkList.Remove(br1);
            br1.btPage.Dispose();
            br1.btImage.Dispose();
            for (int i = 0; i < BookReamrkList.Count; i++)
            {
                BookMark br2 = BookReamrkList[i];
                if (i >= id)
                { 
                    br2.btImage.Left = pLeft;
                    pLeft = pLeft + br2.btImage.Width;
                    br2.btPage.Left = pLeft;
                    br2.btPage.Tag = i;
                    pLeft = pLeft + br2.btPage.Width;
                }
                else
                {
                    br2.btPage.Tag = i;
                }
            }
            MaxLeft = pLeft;
            //SetBookRemark();
        }

        private void mnuSetup_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
            {
                if (isChangeFactor)
                    e.Cancel = true; 
            }
        }

        private void mnuSetup_MouseLeave(object sender, EventArgs e)
        {
            mnuSetup.Visible = false;
        }

        private void btnMange_Click(object sender, EventArgs e)
        {
            //管理
            if (MyApp.getInstance().getToken() == "")
            {
                MessageBoxButtons messButton = MessageBoxButtons.OK;
                DialogResult dr = MessageBox.Show("请登录后继续！", "系统提示", messButton);
            }
            else
            {
                FrmManage frmManage = new FrmManage();
                frmManage.ShowDialog();
                if ((frmManage.StrValue != "") && (frmManage.StrValue != "upload"))
                    SetBookRemark();
            }
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        int timeEnter = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            IntPtr maindHwnd = FindWindow(null, "打印");
            IntPtr maindHwnd2 = FindWindow(null, "Acrobat Reader"); 
            IntPtr selectedWindow = GetForegroundWindow();
            if ((maindHwnd != IntPtr.Zero) || (maindHwnd2 != IntPtr.Zero))
            {
                if ((maindHwnd == selectedWindow) || (maindHwnd2 == selectedWindow)) {
                    //SetParent(this.Handle, maindHwnd);
                    if (timeEnter != 0) {
                        SendKeys.SendWait("{ENTER}");
                        timeEnter = 0;
                    }
                    timeEnter++;
                }
            }
        }

        private void mouseClick(object sender, MouseEventArgs e)
        {
            //鼠标滚轮按下关闭页签
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                closePageClick(sender, e);
            }
        }

        public void openPDFRead(string strUrl)
        {
            //预加载PDF阅读器，提高其他页面打印速度
            try
            { 
                WebBrowser browser = new WebBrowser();
                browser.Navigate(MyApp.getInstance().getFormUrl(strUrl));
            }
             catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
        }

        private void newTool_Click(object sender, EventArgs e)
        {
            //新标签页打开书签
            Button bt1 = (Button)((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl;
            BookMark br1 = BookReamrkList[(int)bt1.Tag];
            string bookUrl = br1.BookUrl;
            int strform = bookUrl.IndexOf("forms");
            if (strform > 0)
            {
                string head = bookUrl.Substring(0, strform - 1);
                if (head != MyApp.HOME_URL)
                {
                    string name = bookUrl.Substring(strform, bookUrl.Length - strform);
                    bookUrl = String.Format("{0:G}/{1:G}", MyApp.HOME_URL, name);
                }
            }
            createWindow(bookUrl);
        }

        private void lbMore_Click(object sender, EventArgs e)
        {
            var control = (Label)sender;
            munBook.Show(control, new Point(control.Width - 20, control.Height + 1));
        }

        private void addMarkList(string name, int it)
        {
            if (!lbMore.Visible)
                lbMore.Visible = true;
            ToolStripItem item = new ToolStripMenuItem();
            item.Name = name;
            item.Text = name;
            item.Tag = it;
            item.Click += new EventHandler(munBook_ItemClick);
            item.MouseDown += new MouseEventHandler(item_MouseDown);
            munBook.Items.Add(item);
        }

        private bool isRightMouse = false;
        private void munBook_ItemClick(object sender, EventArgs e)
        {
            if (!isRightMouse) { 
                ToolStripItem item = (ToolStripItem)sender;
                //书签点击事件
                BookMark br1 = BookReamrkList[(int)item.Tag];
                string bookUrl = br1.BookUrl;
                int strform = bookUrl.IndexOf("forms");
                if (strform > 0)
                {
                    string head = bookUrl.Substring(0, strform - 1);
                    if (head != MyApp.HOME_URL)
                    {
                        string name = bookUrl.Substring(strform, bookUrl.Length - strform);
                        bookUrl = String.Format("{0:G}/{1:G}", MyApp.HOME_URL, name);
                    }
                }
                if (pageControl.Index == 0)
                    createWindow(bookUrl);
                else
                {
                    WebBrowser wb = pageControl.Items[pageControl.Index];
                    wb.Navigate(bookUrl);
                }
            }
        }

        private void item_MouseDown(object sender, MouseEventArgs e)
        {
            isRightMouse = false;
            //书签右键
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                isRightMouse = true;
                ToolStripItem item = (ToolStripItem)sender;
                ContextMenuStrip bt1 = (sender as ToolStripMenuItem).Owner as ContextMenuStrip;
                int it = bt1.Items.IndexOf(item);
                numMore.Tag = item.Tag;
                //var control = (Label)sender;
                //munBook.Show(control, new Point(control.Width - 20, control.Height + 1));e.Location.Y + 1 + 
                numMore.Show(bt1, new Point(e.Location.X + 1, 20 * it));
            }
        }

        private void NewPagetool_Click(object sender, EventArgs e)
        {
            //新标签页打开书签
            ContextMenuStrip bt1 = (sender as ToolStripMenuItem).Owner as ContextMenuStrip;
            BookMark br1 = BookReamrkList[(int)bt1.Tag];
            string bookUrl = br1.BookUrl;
            int strform = bookUrl.IndexOf("forms");
            if (strform > 0)
            {
                string head = bookUrl.Substring(0, strform - 1);
                if (head != MyApp.HOME_URL)
                {
                    string name = bookUrl.Substring(strform, bookUrl.Length - strform);
                    bookUrl = String.Format("{0:G}/{1:G}", MyApp.HOME_URL, name);
                }
            }
            createWindow(bookUrl);
            isRightMouse = false;
            munBook.Close();
        }

        private void delItemTool_Click(object sender, EventArgs e)
        {
            //删除书签
            ContextMenuStrip bt1 = (sender as ToolStripMenuItem).Owner as ContextMenuStrip;
            int id = (int)bt1.Tag;
            BookMark br1 = BookReamrkList[id];
            int pLeft = br1.btImage.Left;
            XmlHelper xh = new XmlHelper();
            xh.DelectNode(br1.BookUrl, br1.Title);
            BookReamrkList.Remove(br1);
            br1.btPage.Dispose();
            br1.btImage.Dispose();
            for (int i = 0; i < BookReamrkList.Count; i++)
            {
                BookMark br2 = BookReamrkList[i];
                if (i >= id)
                {
                    br2.btImage.Left = pLeft;
                    pLeft = pLeft + br2.btImage.Width;
                    br2.btPage.Left = pLeft;
                    br2.btPage.Tag = i;
                    pLeft = pLeft + br2.btPage.Width;
                }
                else
                {
                    br2.btPage.Tag = i;
                }
            }
            MaxLeft = pLeft;
            munBook.Items.Remove(sender as ToolStripMenuItem);
            if (munBook.Items.Count <= 1)
                lbMore.Visible = false;
            isRightMouse = false;
            munBook.Close();
        }

        private void munBook_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.AppFocusChange)
            {
                if (isRightMouse)
                    e.Cancel = true;
            }
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
            {
                if (isRightMouse)
                    e.Cancel = true;
            }
        }

        private void numMore_MouseEnter(object sender, EventArgs e)
        {
            isRightMouse = true;
        }

        private void numMore_MouseLeave(object sender, EventArgs e)
        {
            isRightMouse = false;
            numMore.Close();
        }

        private void MarkList()
        {
            munBook.Items.Clear();
            munBook.Items.Add(toolStripSeparator3);
            foreach (Control c in plBookmark.Controls)
            {
                if (c is Button)
                {
                    if (c.Left > plBookmark.Width - plSystem.Width)
                        addMarkList(c.Text, (int)c.Tag);
                }
            }
            lbMore.Visible = munBook.Items.Count > 1;
        }

        private void numMore_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            isRightMouse = false;
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
            {
                munBook.Close();
            }
        }

        private void munBook_MouseLeave(object sender, EventArgs e)
        {
            if (!isRightMouse)
                munBook.Close();
        }
    }
}
