using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void WelcomeForm_Shown(object sender, EventArgs e)
        {
            // 3000 毫秒，即3秒
            this.timer1 = new Timer();
            this.timer1.Interval = 2000;
            // 设置运行
            this.timer1.Enabled = true;
            this.timer1.Tick += timer1_Tick;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 隐藏本窗体
            this.Hide();
            // 停止timer
            this.timer1.Enabled = false;
            // 创建新窗体并显示。
            MainForm mf = new MainForm();
            mf.Show();
        }
    }
}
