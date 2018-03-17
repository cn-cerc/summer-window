using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vine_window_standard
{
    class StartVine
    {
        public string execute(FrmMain owner, JObject json, JObject result)
        {
            if (!MyApp.debug)
            {
                string host = (String)json.GetValue("host");
                if (!host.StartsWith("http://") && !host.StartsWith("https://"))
                {
                    host = "https://" + host;
                }
                string sid = (String)json.GetValue("sid");
                if ((host != null) && (host != "") && (host != MyApp.HOME_URL))
                {
                    MyApp.HOME_URL = host;
                    owner.RefreshHost(sid, host);
                }
            }   
            return "OK";
        }
    }
}
