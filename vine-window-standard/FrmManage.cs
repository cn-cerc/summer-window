using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vine_window_standard
{
    public partial class FrmManage : Form
    {
        public FrmManage()
        {
            InitializeComponent();
        }
        private string strValue = "";

        public string StrValue
        {
            get { return strValue; }
            set { strValue = value; }
        }

        private void ucBtnExt1_BtnClick(object sender, EventArgs e)
        {
            DoCheck();
            if (StrValue != "")
                this.DialogResult = DialogResult.OK;
        }

        private void ucBtnExt2_BtnClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private bool DoCheck()
        {
            if (radioButton1.Checked)
            {
                doBookmark("upload");
                //StrValue = "upload";
                return true;
            }
            else if (radioButton2.Checked)
            {
                doBookmark("download");
                //StrValue = "download";
                return true;
            }
            else if (radioButton3.Checked)
            {
                XmlHelper xh = new XmlHelper();
                xh.CleanNode();
                //xh.xmlInit();
                StrValue = "clean";
                MessageBoxButtons messButton = MessageBoxButtons.OK;
                DialogResult dr = MessageBox.Show("执行成功！", "系统提示", messButton);
                return true;
            }
            else
                return false;
        }

        List<ManualResetEvent> manualEvents = new List<ManualResetEvent>();

        private void doBookmark(string type)
        {
            Thread thread = new Thread(new ThreadStart(ThreadMethod));
            thread.Start();
            thread.Join();
        }

        private void ThreadMethod()
        {
            string type = "";
            if (radioButton1.Checked)
                type = "upload";
            if (radioButton2.Checked)
                type = "download";
            
            XmlHelper xh = new XmlHelper();
            string bookmakr = xh.getXMLString();
            string formCode = String.Format("FrmSendPrint.doBookmark?CLIENTID={0}&device={1}&sid={2}&bookmark={3}&type={4}", Computer.getClientID(), "pc", MyApp.getInstance().getToken(), bookmakr, type);
            string url = MyApp.getInstance().getFormUrl(formCode);
            var client = new WebClient();
            client.Encoding = System.Text.Encoding.GetEncoding("utf-8");
            string resp = client.DownloadString(url);
            
            var json = JObject.Parse(resp);
            string value = (string)json["value"];
            bool doReset = (bool)json["result"];
            if (doReset) {
                if (type == "download")
                {
                    if ((value != null) && (value != ""))
                    {
                        
                        XmlHelper xm = new XmlHelper();
                        xm.UpdateFile(value);
                    }
                    StrValue = "download";
                }else
                    StrValue = "upload";
                MessageBoxButtons messButton = MessageBoxButtons.OK;
                DialogResult dr = MessageBox.Show("执行成功！", "系统提示", messButton);
            }
            else
            {
                string message = (string)json["message"];
                MessageBoxButtons messButton = MessageBoxButtons.OK;
                DialogResult dr = MessageBox.Show(message, "系统提示", messButton);
            }
        }
    }
}
