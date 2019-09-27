using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UpSoft
{
    class XmlHelper
    {
        //C:\Users\用户名称\AppData\Local\vine-windows-standard 文件夹
        string subPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\vine-windows-standard";
        //执行档路径
        string mypath = System.Environment.CurrentDirectory;
        string XMLUpdate = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\vine-windows-standard\\Update.xml";
        public XmlHelper()
        {
            if (!Directory.Exists(subPath))
            {
                Directory.CreateDirectory(subPath);
            }
            XmlDocument xmlDoc = new XmlDocument();
            if (!File.Exists(XMLUpdate))
                CreateUpdateXML(XMLUpdate);
        }

        public void CreateUpdateXML(string filePath)
        {
            //保存的XML的地址
            string XMLPath = filePath;
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode node;
            node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(node);

            //创建根节点
            XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "vine-windows-standard", null);
            xmlDoc.AppendChild(root);
            XmlNode xnXwsp = xmlDoc.SelectSingleNode("vine-windows-standard");

            XmlElement root1 = xmlDoc.CreateElement("path");
            root1.SetAttribute("install", "0");
            root1.InnerText = mypath;
            xnXwsp.AppendChild(root1);
            xmlDoc.Save(XMLPath);
        }
        public string ReadIsInstall()
        {
            XmlDocument xmlDoc = new XmlDocument();
            string install = "0";
            string folder = "";
            //如果文件存在      
            if (File.Exists(XMLUpdate))
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;//忽略文档里面的注释
                XmlReader reader = XmlReader.Create(@XMLUpdate, settings);
                xmlDoc.Load(reader);

                XmlNode xn = xmlDoc.SelectSingleNode("vine-windows-standard/path");

                XmlElement xe = (XmlElement)xn;
                install = xe.GetAttribute("install").ToString();
                folder = xe.GetAttribute("folder").ToString();
                reader.Close();
            }
            if (install != "1")
            {
                folder = "";
            }
            return folder;
        }
        public void updatePath(string install, string folder)
        {
            //更新执行档目录
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XMLUpdate);

            XmlNode root = xmlDoc.SelectSingleNode("vine-windows-standard/path");
            if (root == null)
            {
                XmlElement root2 = xmlDoc.CreateElement("path");
                root2.SetAttribute("install", install);
                root2.SetAttribute("folder", folder);
                root2.InnerText = mypath;
                xmlDoc.AppendChild(root2);

            }
            else
            {
                XmlElement xe = (XmlElement)root;
                xe.SetAttribute("install", install);
                xe.SetAttribute("folder", folder);
            }

            xmlDoc.Save(XMLUpdate);
        }
    }
}
