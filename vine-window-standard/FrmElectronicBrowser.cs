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
using System.Text.RegularExpressions;
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
            this.WindowState = FormWindowState.Maximized;
            createTabPage();
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
                items[i].getBrowser().NewWindow += new System.ComponentModel.CancelEventHandler(this.webBrowser1_NewWindow);
                items[i].getBrowser().DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
                items[i].getbtRead().Click += new System.EventHandler(this.btRead_Click);
                items[i].btPage.Click += new System.EventHandler(this.btPage_Click);
            }
            tabControl1.SelectedIndex = 0;
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
            if (url == "about:blank")
                e.Cancel = true;
            else if (url == "")
                e.Cancel = true;
            else
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
            if (tabControl1.SelectedIndex == 1)
            {
                //天猫
                items[1].lbMessage.Text = "正在读取";
                WebBrowser wb = items[1].getBrowser();
                StreamReader sr = new StreamReader(wb.DocumentStream, Encoding.GetEncoding(("gbk")));
                TmallDecode decode = new TmallDecode();
                string context = decode.getContect(sr);
                //decode.writeToFile("d:\\tmail.txt", context);
                string spm = decode.getSpm(wb.Document.Url.ToString());
                //List<string> urls = decode.getUrlList(context, spm);
                List<string> urls = decode.getUrlList(wb, spm);
                sr.Close();

                WebBrowser wbtmail = new WebBrowser();
                wbtmail.ScriptErrorsSuppressed = true;
                wbtmail.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wbtmall_DocumentCompleted);
                wbtmail.NewWindow += new System.ComponentModel.CancelEventHandler(this.wbtmall_NewWindow);
                for (int i = 0; i < urls.Count; i++)
                {
                    loading = true;
                    //Console.WriteLine(urls[i]);
                    //POST
                    wbtmail.Navigate(urls[i]);
                    while (loading)
                    {
                        Application.DoEvents(); // 等待本次加载完毕才执行下次循环. 
                    }
                }
                items[1].lbMessage.Text = "读取完成";
            }
            else if (tabControl1.SelectedIndex == 0)
            {
                WebBrowser wbtaobao = new WebBrowser();
                wbtaobao.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wbtaobao_DocumentCompleted);
                wbtaobao.Navigate(browserUrl);
            }
        }

        private void wbtmall_NewWindow(object sender, CancelEventArgs e)
        {            
            WebBrowser wb = (WebBrowser)sender;
            string url = wb.Document.ActiveElement.GetAttribute("href");
            if (url == "about:blank")
                e.Cancel = true;
            else if (url == "")
                e.Cancel = true;
            else
                wb.Navigate(url);
        }

        private void wbtmall_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser wb = (WebBrowser)sender;
            //判断加载完毕
            if (e.Url == wb.Document.Url)
            {
                try
                {
                    StringBuilder sb = getTmallContext(wb.DocumentStream);
                    string decument = sb.ToString();
                    if (decument != "")
                    {
                        decument = decument.Substring(decument.IndexOf("{"), decument.Length - decument.IndexOf("{"));
                        ThreadStart thread = () =>
                        {
                            string formCode = String.Format("FrmElectronicOrder.appendTmall?CLIENTID={0}&device={1}&sid={2}", Computer.getClientID(), "pc", MyApp.getInstance().getToken());
                            string url = MyApp.getInstance().getFormUrl(formCode);
                            WebClient client = new WebClient();

                            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                            System.Collections.Specialized.NameValueCollection VarPost = new System.Collections.Specialized.NameValueCollection();
                            VarPost.Add("htmlText", Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(decument)));
                            byte[] responseData = client.UploadValues(url, "POST", VarPost);
                            string resp = Encoding.UTF8.GetString(responseData);

                            //
                            HttpOnResponse httpResp = TmallhttpOnResponse;
                            this.Invoke(httpResp, client, resp);
                        };
                        new Thread(thread).Start();
                    }
                }
                catch (System.Security.SecurityException ee)
                {
                    MessageBox.Show(e.ToString());
                }
                loading = false;
            }
        }

        private void wbtaobao_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser wb = (WebBrowser)sender;
            //正在读取
            items[1].lbMessage.Text = "正在读取";
            StringBuilder sb = getTaobaoContext(wb.DocumentStream);
            ThreadStart thread = () =>
            {
                string formCode = String.Format("FrmElectronicOrder.appendTaobao?CLIENTID={0}&device={1}&sid={2}", Computer.getClientID(), "pc", MyApp.getInstance().getToken());
                string url = MyApp.getInstance().getFormUrl(formCode);
                WebClient client = new WebClient();

                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                System.Collections.Specialized.NameValueCollection VarPost = new System.Collections.Specialized.NameValueCollection();
                VarPost.Add("htmlText", Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(sb.ToString())));
                byte[] responseData = client.UploadValues(url, "POST", VarPost);
                string resp = Encoding.UTF8.GetString(responseData);

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
        public void TmallhttpOnResponse(WebClient client, string resp)
        {
            try
            {
                var json = JObject.Parse(resp);
                bool result = (bool)json["result"];
                string message = (string)json["message"];
                if (!result)
                    items[1].lbMessage.Text = message;
                else
                    items[1].lbMessage.Text = "读取成功 ";
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
        private StringBuilder getTmallContext(Stream stream)
        {
            StringBuilder sb = new StringBuilder();
            Boolean start = false;
            StreamReader sr = new StreamReader(stream, Encoding.GetEncoding(("gbk")));
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
