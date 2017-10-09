using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace vine_window_standard
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]

    public partial class FrmStartup : Form
    {
        private bool appUpdateReset = false;
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        private extern static bool ReleaseCapture();
        [DllImport("user32.dll")]
        private extern static int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private delegate void HttpOnResponse(WebClient client, String resp);

        public FrmStartup()
        {
            InitializeComponent();

            txtUrl.Visible = MyApp.debug;
            btnStart.Visible = MyApp.debug;

            this.FormBorderStyle = FormBorderStyle.None;
            txtUrl.Text = String.Format("http://{0:G}", Computer.getIPAddress());

            if (Screen.PrimaryScreen.Bounds.Width < 1360)
            {
                MessageBoxButtons messButton = MessageBoxButtons.OK;
                DialogResult dr = MessageBox.Show("您的屏幕分辨率低于1360，地藤无法正常运行!", "环境检测", messButton);
                System.Environment.Exit(0);
                return;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            ThreadStart thread = () =>
            {
                string formCode = String.Format("install.client?appcode={0:G}", MyApp.AppCode);
                string url = MyApp.getInstance().getFormUrl(formCode);
                var client = new WebClient();
                client.Encoding = System.Text.Encoding.GetEncoding("utf-8");
                string resp = client.DownloadString(url);

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
                MyApp myApp = MyApp.getInstance();
                myApp.loadConfig(json);

                var oldVersion = myApp.getCurrentVersion();
                if (oldVersion == myApp.AppVersion)
                {
                    startMainForm();
                    return;
                }
                JArray readme = (JArray)json["appUpdateReadme"];
                string str = "";
                foreach (var line in readme)
                    str += (string)line + "\n";
                lblReadme.Text = str;
                appUpdateReset = (bool)json["appUpdateReset"];
                if (appUpdateReset)
                {
                    btnCancel.Text = "退出";
                }
                llDialog.Visible = true; ;
            }
            catch (Exception e1)
            {
                lblTitle.Text = "启动出现错误!";
                lblReadme.Text = e1.Message;
                btnOk.Visible = false;
                appUpdateReset = true;
                btnCancel.Text = "稍后再试";
                llDialog.Visible = true;
            }
        }

        private void startMainForm()
        {
            Hide();
            new FrmMain().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyApp.HOME_URL = txtUrl.Text;          
            timer1.Enabled = false;
            startMainForm();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //模拟点击窗体的Title
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();//释放窗体的鼠标焦点
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //打开指定的浏览器
            string url = String.Format("install?appcode={0:G}&operate={1:G}", MyApp.AppCode, "update");
            System.Diagnostics.Process.Start(MyApp.getInstance().getFormUrl(url));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (appUpdateReset)
            {
                Application.Exit();
            }else
            {
                startMainForm();
            }
        }
    }
}
