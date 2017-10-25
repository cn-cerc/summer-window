using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vine_window_standard
{
    public class JavaScriptProxy
    {
        private FrmMain owner;

        public JavaScriptProxy(FrmMain owner)
        {
            this.owner = owner;
        }

        public string execute(string req)
        {
            var json = JObject.Parse(req);
            String classCode = (string)json["classCode"];
            if (classCode == "SetMenuList")
            {
                SetMenuList obj = new SetMenuList();
                return obj.execute(owner, json);
            }
            else if (classCode == "CallTaobaoBrowser")
            {
                CallTaobaoBrowser obj = new CallTaobaoBrowser();
                return obj.execute(owner, json);
            }
            else
            {
                return "classCode not find.";
            }
            return "";
        }
    }
}
