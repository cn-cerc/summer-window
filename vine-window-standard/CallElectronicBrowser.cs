using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vine_window_standard
{
    class CallElectronicBrowser
    {
        public string execute(FrmMain owner, JObject json, JObject result)
        {
            String token = (String)json.GetValue("token");
            MyApp.getInstance().setToken(token);
            new FrmElectronicBrowser().Show();
            return "OK";
        }
    }
}
