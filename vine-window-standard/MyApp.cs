using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace vine_window_standard
{
    class MyApp
    {
        private static string currentVersion = "1.1.1";
        public static string AppCode = "vine-windows-standard";
        private static MyApp instance;
        public static String HOME_URL = "https://m.knowall.cn";
       // public static String HOME_URL = "http://192.168.31.247";
        public static String FORMS = "form";
        public static String SERVICES = "services";
        internal static string APP_NAME = "地藤标准版";
        public static bool debug = false;

        public string AppVersion { get; private set; } = "0.0.0";

        public static MyApp getInstance()
        {
            if(instance == null)
            {
                instance = new MyApp();
            }
            return instance;
        }

        public String getFormUrl(String formCode)
        {
            return String.Format("{0:G}/{1:G}/{2:G}", HOME_URL, FORMS, formCode);
        }

        internal void loadConfig(JObject json)
        {
            if (json.Property("appVersion") != null)
            {
                AppVersion = (string)json["appVersion"];
            }
        }

        internal String getCurrentVersion()
        {
            return currentVersion;
        }
    }
}
