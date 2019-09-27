using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpSoft
{
    public partial class FrmUpSoft : Form
    {
        string mypath = System.Environment.CurrentDirectory;
        public FrmUpSoft()
        {
            InitializeComponent();
            //label1.Left = 92;
            //label1.Text = "正在升级...";
            tStart.Enabled = true;
        }

        public bool UpdateSoft()
        {
            //label1.Text = "正在升级...";
            XmlHelper xh = new XmlHelper();
            string folder = xh.ReadIsInstall();
            if (folder != "")
            {
                if (CopyDir(folder, mypath))
                {
                    xh.updatePath("0", "");
                    return true;
                }
                else
                    return false;
            }else
                return false;
        }
        private bool CopyDir(string srcPath, string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加
                if (aimPath[aimPath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                {
                    aimPath += System.IO.Path.DirectorySeparatorChar;
                }
                // 判断目标目录是否存在如果不存在则新建
                if (!System.IO.Directory.Exists(aimPath))
                {
                    System.IO.Directory.CreateDirectory(aimPath);
                }
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                // string[] fileList = Directory.GetFiles（srcPath）；
                string[] fileList = System.IO.Directory.GetFileSystemEntries(srcPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    if (System.IO.Directory.Exists(file))
                    {
                        CopyDir(file, aimPath + System.IO.Path.GetFileName(file));
                    }
                    // 否则直接Copy文件
                    else
                    {
                        System.IO.File.Copy(file, aimPath + System.IO.Path.GetFileName(file), true);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw;
            }
        }

        private void tStart_Tick(object sender, EventArgs e)
        {
            tStart.Enabled = false;
            if (UpdateSoft())
            {
                label1.Left = 101;
                label1.Text = "升级完成！点击“确定”退出并重新启动地藤管家！";
                //MessageBox.Show("升级完成！点击确定后将重新启动地藤管家！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                label1.Left = 155;
                label1.Text = "当前版本已是最新版本！";
                //MessageBox.Show("当前版本已是最新版本，不需要升级！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            butOK.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("vine-window-standard.exe");
            }
            catch
            {
                //throw;
            }
            Application.Exit();
        }
    }
}
