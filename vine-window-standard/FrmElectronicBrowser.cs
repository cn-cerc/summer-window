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
    public partial class FrmElectronicBrowser : Form
    {
        private delegate void HttpOnResponse(WebClient client, String resp);
        private List<TabPageContorl> items = new List<TabPageContorl>();
        bool loading = true;    // 该变量表示网页是否正在加载. 
        public FrmElectronicBrowser()
        {
            InitializeComponent();
            fixWindowSize();

            createTabPage();
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

        private void createTabPage()
        {
            AddItems("https://www.taobao.com", "淘宝");
            AddItems("https://www.tmall.com", "天猫");
            
            for (int i = 0; i < items.Count; i++)
            {
                items[i].AddButton(panel1, i);
                this.tabControl1.Controls.Add(items[i].getPage());
                items[i].getBrowser().Navigate(items[i].getUrl());
                items[i].getBrowser().NewWindow += this.webBrowser1_NewWindow;
                items[i].getBrowser().DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
                items[i].getbtRead().Click += new System.EventHandler(this.btRead_Click);
                items[i].btPage.Click += new System.EventHandler(this.btPage_Click);
            }
            tabControl1.SelectedIndex = 1;
        }

        private void AddItems(string url, string Title)
        {
            TabPageContorl tabPage = new TabPageContorl(url, Title);
            tabPage.AddItem();
            items.Add(tabPage);
        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            WebBrowser wb = (WebBrowser)sender;
            string url = wb.Document.ActiveElement.GetAttribute("href");
            wb.Navigate(url);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser wb = (WebBrowser) sender;
            tbUrl.Text = wb.Url.ToString();
        }

        private void btRead_Click(object sender, EventArgs e)
        {
            string browserUrl = items[tabControl1.SelectedIndex].getBrowser().Document.Url.ToString();
            if (tabControl1.SelectedIndex == 0)
            {
                //淘宝
                items[0].lbMessage.Text = "正在读取";
                List<string> urls = getUrlList(items[1].getBrowser());
                WebBrowser wbtmail = new WebBrowser();
                wbtmail.ScriptErrorsSuppressed = true;
                wbtmail.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wbtmail_DocumentCompleted);
                for (int i = 0; i < urls.Count; i++)
                {
                    loading = true;
                    wbtmail.Navigate(urls[i]);
                    while (loading)
                    {
                        Application.DoEvents(); // 等待本次加载完毕才执行下次循环. 
                    }
                }
                items[0].lbMessage.Text = "读取完成";
                //POST
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                //天猫
                WebBrowser wbtaobao = new WebBrowser();
                wbtaobao.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wbtaobao_DocumentCompleted);
                wbtaobao.Navigate(browserUrl);
            }
        }

        private void wbtmail_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser wb = (WebBrowser)sender;
            //判断加载完毕
            if (e.Url == wb.Document.Url)
            {
                Console.WriteLine(wb.Document.Url);
                StringBuilder sb = new StringBuilder();
                Boolean start = false;
                StreamReader sr = new StreamReader(wb.DocumentStream, Encoding.GetEncoding(("gbk")));
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    String str = line.Trim();
                    if (str.StartsWith("var detailData"))
                    {
                        start = true;
                    }
                    if (start)
                    {
                        if (str.StartsWith("</script>"))
                        {
                            break;
                        }
                        sb.Append(line);
                    }
                }
                sr.Close();
                string sbs = sb.ToString();
                loading = false;
            }
        }

        public List<string> getUrlList(WebBrowser tmBroowser)
        {
            List<string> urlList = new List<string>();
            string defUrl = tmBroowser.Document.Url.ToString();
            int first = defUrl.IndexOf("spm=");
            string spm = defUrl.Substring(first, defUrl.Length - first);
            StringBuilder sb = new StringBuilder();
            Boolean start = false;
            StreamReader sr = new StreamReader(tmBroowser.DocumentStream, Encoding.GetEncoding(("gbk")));
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                String str = line.Trim();
                if (str.StartsWith("var data = JSON.parse("))
                {
                    start = true;
                }
                if (start)
                {
                    if (str.StartsWith("</script>"))
                    {
                        break;
                    }
                    sb.Append(line.Trim());
                }
            }
            sr.Close();
            string stb = sb.ToString();
            int sta = stb.IndexOf("mainBizOrderIds");
            int end = stb.IndexOf("rateGift");
            stb = stb.Substring(sta+20, end- sta - 25);
            string[] sArray = stb.Split(new char[1] { '-' });
            foreach (string e in sArray)
            {
                urlList.Add(string.Format("https://trade.tmall.com/detail/orderDetail.htm?{0}&bizOrderId={1}", spm, e));
            }
            return urlList;
        }

        private void wbtaobao_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser wb = (WebBrowser)sender;
            //正在读取
            items[0].lbMessage.Text = "正在读取";
            StringBuilder sb = getTaobaoContext(wb.DocumentStream);
            ThreadStart thread = () =>
            {
                string formCode = String.Format("FrmTaobaoOrder.append?CLIENTID={0}&device={1}&sid={2}", Computer.getClientID(), "pc", MyApp.getInstance().getToken());
                string url = MyApp.getInstance().getFormUrl(formCode);
                WebClient client = new WebClient();

                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                System.Collections.Specialized.NameValueCollection VarPost = new System.Collections.Specialized.NameValueCollection();
                VarPost.Add("htmlText", Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(sb.ToString())));
                byte[] responseData = client.UploadValues(url, "POST", VarPost);
                string resp = Encoding.UTF8.GetString(responseData);

                //
                HttpOnResponse httpResp = httpOnResponse;
                this.Invoke(httpResp, client, resp);
            };
            new Thread(thread).Start();
        }

        public void httpOnResponse(WebClient client, string resp)
        {
            try
            {
                var json = JObject.Parse(resp);
                bool result = (bool)json["result"];
                string message = (string)json["message"];
                if (!result)
                    items[0].lbMessage.Text = message;
                else
                    items[0].lbMessage.Text = "读取成功 ";
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        private StringBuilder getTaobaoContext(Stream stream)
        {
            StringBuilder sb = new StringBuilder();
            Boolean start = false;
            StreamReader sr = new StreamReader(stream, Encoding.GetEncoding(("gbk")));
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                String str = line.Trim();
                if (str.StartsWith("<tbody "))
                {
                    start = true;
                }
                if (start)
                {
                    if (str.StartsWith("<div id=\"complaintEmsDiv\""))
                    {
                        break;
                    }
                    sb.Append(line);
                }
            }
            sr.Close();
            return sb;
        }

        private void btDefault_Click(object sender, EventArgs e)
        {
            WebBrowser wb = items[tabControl1.SelectedIndex].getBrowser();
            wb.Navigate(items[tabControl1.SelectedIndex].defaultUrl);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbUrl.Text = items[tabControl1.SelectedIndex].getBrowser().Document.Url.ToString();
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            items[tabControl1.SelectedIndex].getBrowser().Refresh();
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            WebBrowser wb = items[tabControl1.SelectedIndex].getBrowser();
            if (wb.CanGoBack)
                wb.GoBack();
        }

        private void tbUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                items[tabControl1.SelectedIndex].getBrowser().Navigate(tbUrl.Text);
        }

        private void btPage_Click(object sender, EventArgs e)
        {
            Button tb = (Button)sender;
            tabControl1.SelectedIndex = (int)tb.Tag;
        }
    }
}
