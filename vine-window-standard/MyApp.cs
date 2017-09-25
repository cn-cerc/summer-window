using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vine_window_standard
{
    class MyApp
    {
        private static MyApp instance;
        public static String HOME_URL = "https://m.knowall.cn";
        public static String FORMS = "form";
        public static String SERVICES = "services";

        public static MyApp getInstance()
        {
            if(instance == null)
            {
                instance = new MyApp();
            }
            return instance;
        }

        public static String getFormUrl(String formCode)
        {
            return String.Format("{0:G}/{1:G}/{2:G}", HOME_URL, FORMS, formCode);
        }
    }
}
