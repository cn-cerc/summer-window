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
    public partial class FrmTaobaoBrowser : Form
    {
        private delegate void HttpOnResponse(WebClient client, String resp);
        public FrmTaobaoBrowser()
        {
            InitializeComponent();

            webBrowser1.Navigate("https://www.taobao.com");
        }

        private void btnReadTaobao_Click(object sender, EventArgs e)
        {
            StringBuilder sb = getTaobaoContext(this.webBrowser1.DocumentStream);

            ThreadStart thread = () =>
            {
                string formCode = String.Format("FrmSysTaobaoOrder?sid=%s", MyApp.getInstance().getToken());
                string url = MyApp.getInstance().getFormUrl(formCode);
                WebClient client = new WebClient();

                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                string postString = string.Format("htmlType={0}&htmlText={1}", "taobao", sb.ToString());
                byte[] postData = Encoding.UTF8.GetBytes(postString);
                byte[] responseData = client.UploadData(url, "POST", postData);
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
                MessageBox.Show("读取成功 ", "提示", MessageBoxButtons.OKCancel);
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
                webBrowser1.Navigate(tbUrl.Text);
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
    }
}
