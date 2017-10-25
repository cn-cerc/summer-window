using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace vine_window_standard
{
    class CallTaobaoBrowser
    {
        public string execute(FrmMain owner, JObject json, JObject result)
        {
            String token = (String)json.GetValue("token");
            MyApp.getInstance().setToken(token);
            new FrmTaobaoBrowser().Show();
            return "OK";
        }
    }
}
