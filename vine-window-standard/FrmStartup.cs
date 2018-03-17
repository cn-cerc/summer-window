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
using System.IO;

namespace vine_window_standard
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]

    public partial class FrmStartup : Form
    {
        private static Int32 ieMinVersion = 9999;//11001;
        private bool appUpdateReset = false;
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        MyApp myApp = MyApp.getInstance();

        [DllImport("user32.dll")]
        private extern static bool ReleaseCapture();
        [DllImport("user32.dll")]
        private extern static int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private delegate void HttpOnResponse(WebClient client, String resp);

        public FrmStartup()
        {
            InitializeComponent();

            try
            {
                //让WebBrowser以ie9模式运行，用于支持html5
                fixWebBrowserVersion("vine-window-standard.vshost.exe", ieMinVersion);
                String appName = System.IO.Path.GetFileName(Application.ExecutablePath).ToLower();
                fixWebBrowserVersion(appName, ieMinVersion);

                txtUrl.Visible = MyApp.debug;
                btnStart.Visible = MyApp.debug;

                this.FormBorderStyle = FormBorderStyle.None;
                txtUrl.Text = String.Format("http://{0:G}", Computer.getIPAddress());
                if (MyApp.debug)
                {
                    this.AcceptButton = btnStart;
                }

                if (Screen.PrimaryScreen.Bounds.Width < 1360)
                {
                    MessageBoxButtons messButton = MessageBoxButtons.OK;
                    DialogResult dr = MessageBox.Show("您的屏幕分辨率低于1360，地藤无法正常运行!", "环境检测", messButton);
                    System.Environment.Exit(0);
                    return;
                }
                if (checkAdobeReader() == false)
                {
                    MessageBoxButtons messButton = MessageBoxButtons.OK;
                    DialogResult dr = MessageBox.Show("尚未安裝Adobe Acrobat Reader软件, 请质询客服协助安装", " 环境检测", messButton);
                    System.Environment.Exit(0);
                    return;
                }

                timer1.Enabled = !MyApp.debug;
            }
            catch (System.Security.SecurityException ee)
            {
                MessageBoxButtons messButton = MessageBoxButtons.OK;
                DialogResult dr = MessageBox.Show("您当前没有操作注册表的权限，地藤无法正常运行!", "环境检测", messButton);
                System.Environment.Exit(0);
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            ThreadStart thread = () =>
            {
                string formCode = String.Format("install.client?appCode={0:G}&curVersion={1:G}", MyApp.AppCode, myApp.getCurrentVersion());
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
                llDialog.Left = (this.Width - llDialog.Width) / 2;
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
            string url = String.Format("install.update?appCode={0:G}&curVersion={1:G}", MyApp.AppCode, myApp.getCurrentVersion());
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

        private bool checkAdobeReader()
        {
            var adobePath = Registry.GetValue(@"HKEY_CLASSES_ROOT\Software\Adobe\Acrobat\Exe", string.Empty, string.Empty);
            if (adobePath != null)
                return true;
            else
                return false;
        }
    }
}
