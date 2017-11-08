using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class TmallDecode
{
    public TmallDecode()
    {
    }

    public string getSpm(String url)
    {
        string defUrl = url;
        int first = defUrl.IndexOf("spm=");
        int last = defUrl.Length;
        if (defUrl.IndexOf("&") > 0)
        {
            last = defUrl.IndexOf("&");
        }
        return defUrl.Substring(first, last - first);
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
            urlList.Add(string.Format("https://trade.tmall.com/detail/orderDetail.htm?spm={0}&bizOrderId={1}", spm, e));
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
