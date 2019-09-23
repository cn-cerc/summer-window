using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vine_window_standard
{
    public partial class UCBtnLabel : UserControl
    {
        private string _labText = "导航";
        /// <summary>
        /// 按钮文字
        /// </summary>
        [Description("文字"), Category("自定义")]
        public string LabText
        {
            get { return _labText; }
            set
            {
                _labText = value;
                label1.Text = value;
                label1.Refresh();
            }
        }
        /// <summary>
         /// 图片
         /// </summary>
        [Description("图片"), Category("自定义")]
        public Image Image
        {
            get
            {
                return this.imageList1.Images[0];
            }
            set
            {
                this.imageList1.Images.Clear();
                this.imageList1.Images.Add(value);
                this.button1.ImageIndex = 0;
            }
        }
        public UCBtnLabel()
        {
            InitializeComponent();
        }
    }
}
