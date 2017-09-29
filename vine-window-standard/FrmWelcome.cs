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

namespace vine_window_standard
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]

    public partial class FrmWelcome : Form
    {
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
            Hide();
            new FrmMain().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Hide();
            FrmMain form = new FrmMain();
            MyApp.HOME_URL = this.txtUrl.Text;
            form.loadUrl(this.txtUrl.Text);
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
    }
}
