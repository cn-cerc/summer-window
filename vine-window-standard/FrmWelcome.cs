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

    public partial class FrmWelcome : Form
    {
        private bool appUpdateReset = false;
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        [DllImport("user32.dll")]
        private extern static bool ReleaseCapture();
        [DllImport("user32.dll")]
        private extern static int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public FrmWelcome()
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

            string url = MyApp.getInstance().getFormUrl(String.Format("install.client?appcode=%s", "windows-standard"));
            var client = new WebClient();
            client.Encoding = System.Text.Encoding.GetEncoding("utf-8");
            string resp = client.DownloadString(url);

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
            Hide();
            FrmMain form = new FrmMain();
            MyApp.HOME_URL = this.txtUrl.Text;
            form.loadUrl(String.Format("{0:G}?CLIENTID={1:G}", MyApp.HOME_URL, Computer.getClientID()));
            form.Show();
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
            System.Diagnostics.Process.Start(MyApp.getInstance().getFormUrl("install"));
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
