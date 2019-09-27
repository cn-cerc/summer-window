using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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

        public JObject execute(String classCode, String req)
        {
            JObject result = new JObject();
            result.Add("result", true);
            var json = JObject.Parse(req);
            if (classCode == "SetMenuList")
            {
                SetMenuList obj = new SetMenuList();
                obj.execute(owner, json, result);
            }
            else if (classCode == "CallTaobaoBrowser")
            {
                //CallTaobaoBrowser obj = new CallTaobaoBrowser();
                CallElectronicBrowser obj = new CallElectronicBrowser();
                obj.execute(owner, json, result);
            }
            else if (classCode == "GetVersionName")
            {
                result.Add("data", MyApp.getInstance().getCurrentVersion());
            }
            else if (classCode == "HeartbeatCheck")
            {
                HeartBeatCheck hbc = new HeartBeatCheck();
                hbc.execute(owner, json, result);
            }
            else if (classCode == "GetClientID")
            {
                result.Add("data", Computer.getClientID());
            }
            else if (classCode == "cleanCache")
            {
                ;
            }
            else if (classCode == "openIE")
            {
                string url = (String)json.GetValue("url");
                if (url != "")
                    System.Diagnostics.Process.Start(url);
            }
            else if (classCode == "startVine")
            {
                StartVine sv = new StartVine();
                sv.execute(owner, json, result);
            }
            else if (classCode == "fastPrint")
            {
                owner.isFastPrint = true;
                string url = (String)json.GetValue("url");
                string printer = (String)json.GetValue("printer");
                if (url != "")
                {
                    if (!owner.Print(printer, MyApp.getInstance().getFormUrl(url)))
                        result.GetValue("result").Replace(false);
                }
            }
            else if (classCode == "printList")
            {
                owner.isFastPrint = true;
                string urls = (String)json.GetValue("url");
                string printer = (String)json.GetValue("printer");
                if (urls != "")
                {
                    if (!owner.PrintList(printer, urls))
                        result.GetValue("result").Replace(false);
                }
            }
            else if (classCode == "getPrinter")
            {
                PrintDocument prtdoc = new PrintDocument();
                string NameList = "";
                foreach (string strprinter in PrinterSettings.InstalledPrinters)
                {
                    if (NameList == "")
                        NameList = strprinter;
                    else
                        NameList = NameList + "," + strprinter;
                }
                result.Add("data", NameList);
            }
            else if (classCode == "openPDFRead")
            {
                string url = (String)json.GetValue("url");
                owner.openPDFRead(url);
            }
            else
            {
                result.GetValue("result").Replace(false);
                result.Add("message", "classCode not find.");
            }
            return result;
        }
    }
}
