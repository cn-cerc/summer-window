using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace vine_window_standard 
{
    class SetMenuList
    {
        internal string execute(FrmMain owner, JObject json, JObject result)
        {
            JArray data = (JArray)json["data"];
            ContextMenuStrip mtitle = new ContextMenuStrip();
            foreach (var item in data)
            {
                TitleInit(mtitle);
                ToolStripMenuItem mi = new ToolStripMenuItem();
                mi.Text = ((JObject)item)["name"].ToString();
                mi.Tag = ((JObject)item)["href"].ToString();
                mtitle.Items.Add(mi);
            }
            owner.SetTitle(mtitle);
            return "";
        }

        public void TitleInit(ContextMenuStrip title)
        {
            title.Items.Clear();
            title.Items.Add("转到首页");
            title.Items.Add("关闭");
        }
    }
}
