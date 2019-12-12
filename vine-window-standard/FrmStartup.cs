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
using System.Diagnostics;

namespace vine_window_standard
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]

    public partial class FrmStartup : Form
    {
        private static Int32 ieMinVersion = 9999;//11001;
        private bool appUpdateReset = false;
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        private bool appIncUpdate = false;
        private int updateUID = 0;

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
                //fixWebBrowserVersion("vine-window-standard.vshost.exe", ieMinVersion);
                string appName = System.IO.Path.GetFileName(Application.ExecutablePath).ToLower();
                fixWebBrowserVersion(appName, ieMinVersion);

                txtUrl.Visible = MyApp.debug;
                btnStart.Visible = MyApp.debug;

                this.FormBorderStyle = FormBorderStyle.None;
                //txtUrl.Text = String.Format("http://{0:G}", Computer.getIPAddress());
                txtUrl.Text = String.Format("http://{0:G}:8080", Computer.getIPAddress());
                //MyApp.HOME_URL = txtUrl.Text;
                //txtUrl.Text = "https://c1.diteng.site";
                Size size = new Size();
                size = PrimaryScreen.DESKTOP;
                if (MyApp.debug)
                {
                    this.AcceptButton = btnStart;
                }

                if (size.Width < 1360)
                {
                    MessageBoxButtons messButton = MessageBoxButtons.OK;
                    DialogResult dr = MessageBox.Show("您的屏幕分辨率低于1360，无法获得理想的体验！", "环境检测", messButton);
                    //System.Environment.Exit(0);
                    //return;
                }

                //timer1.Enabled = true;
                timer1.Enabled = !MyApp.debug;
            }
            catch (System.Security.SecurityException ee)
            {
                MessageBox.Show(ee.Message);
                //MessageBoxButtons messButton = MessageBoxButtons.OK;
                //DialogResult dr = MessageBox.Show("您当前没有操作注册表的权限，地藤无法正常运行!", "环境检测", messButton);
                //System.Environment.Exit(0);
            }
        }

        private static void fixWebBrowserVersion(String appName, Int32 ieMinVer)
        {
            try { 
                String section = "Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION";
                RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;
                RegistryKey hkie = hklm.OpenSubKey(section, false);

                Object val = hkie.GetValue(appName);
                Int32 curVer = val != null ? (Int32)val : 0;
                if (curVer < ieMinVer)
                {
                    MessageBoxButtons messButton = MessageBoxButtons.OK;
                    DialogResult dr = MessageBox.Show("您的当前IE版本不符合要求，无法获得理想的体验，请咨询客服协助安装!", " 环境检测", messButton);
                    //System.Environment.Exit(0);
                    //return;
                }
                hkie.Close();
                hklm.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            ThreadStart thread = () =>
            {
                string formCode = String.Format("install.client?appCode={0:G}&curVersion={1:G}&UpdateCode={2:G}", MyApp.AppCode, myApp.getCurrentVersion(), MyApp.UpdateCode);
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
                appIncUpdate = (bool)json["appIncUpdate"];
                if (appIncUpdate)
                {
                    btnOk.Text = "自动更新";
                }
                updateUID = (int)json["updateUID"];
                llDialog.Left = (this.Width - llDialog.Width) / 2;
                llDialog.Visible = true; 
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
            if (appIncUpdate && (updateUID != 0))
            {
                try
                {
                    lblReadme.Text += "正在执行更新，这可能需要几分钟，请稍候！" + "\n";
                    lblReadme.Text += "正在下载更新文件..." + "\n";
                    //获取版本UID
                    //1.自动下载增量更新
                    HttpDldFile hdf = new HttpDldFile();
                    //install.download? uid
                    //DownloadFile("https://www.diteng.site/forms/install.download?uid=99", "vine-windows-standard-1.0.1.2.exe");
                    string url = "install.download?uid=" + updateUID.ToString();
                    string fileName = MyApp.AppCode + "-" + myApp.getCurrentVersion() + ".zip";
                    hdf.DownloadFile(MyApp.getInstance().getFormUrl(url), fileName);
                    //2.解压
                    //解压目录unZipDir，压缩包subPath1
                    lblReadme.Text += "正在解压更新文件..." + "\n";
                    string unZipDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\vine-windows-standard\\Update\\" + MyApp.AppCode + "-" + myApp.getCurrentVersion();
                    string subPath1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\vine-windows-standard\\Update\\" + fileName;
                    ZipHelper.UnZip(subPath1, unZipDir);
                    XmlHelper xh = new XmlHelper();
                    xh.updatePath("1", unZipDir);
                    //3.启动更新exe
                    Process.Start("UpSoft.exe");
                    Application.Exit();
                }catch
                {
                    ;
                }

            }
            else
            {
                string url = String.Format("install.update?appCode={0:G}&curVersion={1:G}", MyApp.AppCode, myApp.getCurrentVersion());
                //打开指定的浏览器
                System.Diagnostics.Process.Start(MyApp.getInstance().getFormUrl(url));
                //Application.Exit();
            }
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
