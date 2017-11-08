using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class TmailDecode
{
	public TmailDecode()
	{
	}

    public string getSpm(String url)
    {
        string defUrl = url;
        int first = defUrl.IndexOf("spm=");
        return  defUrl.Substring(first, defUrl.IndexOf("&") - first);
    }

    public List<string>  getUrlList(string stb, string spm)
    {
        List<string> urlList = new List<string>();
        //int sta = stb.IndexOf("mainBizOrderIds");
        //int end = stb.IndexOf("rateGift");
        //stb = stb.Substring(sta+20, end- sta - 25);
        //string[] sArray = stb.Split(new char[1] { '-' });
        //foreach (string e in sArray)
        //{
        //    urlList.Add(string.Format("https://trade.tmall.com/detail/orderDetail.htm?{0}&bizOrderId={1}", spm, e));
        //}
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
        return sb.ToString();
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
}
