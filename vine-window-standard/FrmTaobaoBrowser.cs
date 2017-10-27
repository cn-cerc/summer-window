using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vine_window_standard
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FrmTaobaoBrowser : Form
    {
        private delegate void HttpOnResponse(WebClient client, String resp);
        public FrmTaobaoBrowser()
        {
            InitializeComponent();
            fixWindowSize();
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Navigate("https://www.taobao.com");
            this.lbMessage.Text = "";
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

        private void btnReadTaobao_Click(object sender, EventArgs e)
        {
            lbMessage.Text = "正在读取";
            StringBuilder sb = getTaobaoContext(this.webBrowser1.DocumentStream);
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
                    lbMessage.Text = message;
                else
                    lbMessage.Text = "读取成功 ";
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

        private void tbUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                this.webBrowser1.Navigate(tbUrl.Text);
        }

        public void writeToFile(string fileName, string dataText)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            //获得字节数组
            byte[] data = System.Text.Encoding.Default.GetBytes(dataText);
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            WebBrowser wb = (WebBrowser)sender;
            string url = wb.Document.ActiveElement.GetAttribute("href");
            this.webBrowser1.Navigate(url);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            tbUrl.Text = this.webBrowser1.Url.ToString();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Refresh();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (this.webBrowser1.CanGoBack)
                this.webBrowser1.GoBack();
        }
    }
}
