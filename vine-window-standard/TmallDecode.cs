using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

public class TmallDecode
{
    public TmallDecode()
    {
    }

    public string getSpm(String url)
    {
        string defUrl = url;
        string spm = "";
        int first = defUrl.IndexOf("spm=");
        int last = defUrl.Length;
        if (defUrl.IndexOf("&") > -1)
        {
            last = defUrl.IndexOf("&");
        }
        if (first > -1 && last - first > -1)
            spm = defUrl.Substring(first, last - first);
        return spm;
    }

    public List<string> getUrlList(string stb, string spm)
    {
        List<string> urlList = new List<string>();
        List<string> ordList = new List<string>();
        stb = stb.Replace(@"\", ""); 
        var json = JObject.Parse(stb);
        string s = json["mainOrders"].ToString();
        var jsons = JArray.Parse(s);
        foreach (var ss in jsons)
        {
            ordList.Add((string)((JObject)ss)["id"]);
        }
        foreach (string e in ordList)
        {
            urlList.Add(string.Format("https://trade.tmall.com/detail/orderDetail.htm?{0}&bizOrderId={1}", spm, e));
        }
        return urlList;
    }

    public List<string> getUrlList(WebBrowser wb, string spm)
    {
        List<string> urlList = new List<string>();
        List<string> ordList = new List<string>();
        //WebBrowser wb = items[1].getBrowser();
        HtmlElement script = wb.Document.CreateElement("script");
        script.SetAttribute("type", "text/javascript");
        script.SetAttribute("text", "function _func(){return document.getElementById('sold_container').outerHTML}");
        wb.Document.Body.AppendChild(script);

        object sr = wb.Document.InvokeScript("_func");
        if (sr != null)
        {
            Regex reg = new Regex(@"tradeID=[0-9]*&");
            foreach (var item in reg.Matches(sr.ToString()))
            {
                string OrderId = item.ToString();
                if (OrderId.Length > 7)
                    OrderId = OrderId.Substring(8, OrderId.Length - 9);
                if (OrderId != ""  && ordList.IndexOf(OrderId) == -1)
                {
                    ordList.Add(OrderId);
                }
            }
            if (ordList.Count > 0)
            {
                foreach (string ordId in ordList)
                {
                    urlList.Add(string.Format("https://trade.tmall.com/detail/orderDetail.htm?{0}&bizOrderId={1}", spm, ordId));
                }
            }
        }
        return urlList;
    }

    public string getContect(StreamReader sr)
    {
        StringBuilder sb = new StringBuilder();
        Boolean start = false;
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            String str = line.Trim();
            if (str.StartsWith("var data = JSON.parse("))
            {
                start = true;
            }
            if (start)
            {
                if (str.StartsWith("</script>"))
                {
                    break;
                }
                sb.Append(line.Trim());
            }
        }
        string json = sb.ToString();
        if (json != "")
            json = json.Substring(json.IndexOf("{"), json.Length - json.IndexOf("{") - 3);
        return json;
    }

    public void writeToFile(string fileName, string dataText)
    {
        FileStream fs = new FileStream(fileName, FileMode.Create);
        //获得字节数组
        byte[] data = System.Text.Encoding.Default.GetBytes(dataText);
        //开始写入
        fs.Write(data, 0, data.Length);
        //清空缓冲区、关闭流
        fs.Flush();
        fs.Close();
    }

    public string readFromFile(string fileName)
    {
        try
        {
            StreamReader sr = new StreamReader(fileName, Encoding.Default); 
            return sr.ReadToEnd();
        }
        catch (IOException e)
        {
            return e.ToString();
        }
    }

}
