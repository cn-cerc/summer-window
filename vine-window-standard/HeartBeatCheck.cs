using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vine_window_standard
{
    class HeartBeatCheck
    {
        public string execute(FrmMain owner, JObject json, JObject result)
        {
            String token = (String)json.GetValue("token");
            Boolean status = (Boolean)json.GetValue("status");
            int time = (int)json.GetValue("time");
            MyApp.getInstance().setToken(token);
            owner.heartbeatCheck(status, time);
            return "OK";
        }
    }
}
