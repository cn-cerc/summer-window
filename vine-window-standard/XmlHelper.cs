using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace vine_window_standard
{
    public class XmlHelper
    {
        //C:\Users\用户名称\AppData\Local\vine-windows-standard 文件夹
        string subPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\vine-windows-standard";
        //保存的XML的地址
        string XMLPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\vine-windows-standard\\bsii.xml";
        string XMLPathZ = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\vine-windows-standard\\Zoom.xml";

        public XmlHelper()
        {
            if (!Directory.Exists(subPath))
            {
                Directory.CreateDirectory(subPath);
            }
            XmlDocument xmlDoc = new XmlDocument();
            if (!File.Exists(XMLPath))
                CreateBRXML(XMLPath);
            if (!File.Exists(XMLPathZ))
                CreateZoomXML(XMLPathZ);
        }

        public void CreateBRXML(string filePath)
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

            XmlElement root1 = xmlDoc.CreateElement("BookReamrk");
            xnXwsp.AppendChild(root1);
            xmlDoc.Save(XMLPath);
        }
        public void CreateZoomXML(string filePath)
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

            XmlElement root2 = xmlDoc.CreateElement("Zoom");
            root2.SetAttribute("Factor", "100");
            xnXwsp.AppendChild(root2);

            XmlElement root3 = xmlDoc.CreateElement("IsMaxForm");
            root3.SetAttribute("IsMax", "0");
            xnXwsp.AppendChild(root3);
            xmlDoc.Save(XMLPath);
        }

        public bool WriteXML(string Values, string Title)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XMLPath);
            if (CheckNode(Values, Title))
                return false;


            XmlNode root = xmlDoc.SelectSingleNode("vine-windows-standard/BookReamrk");
            if (root == null)
            {
                root = xmlDoc.CreateNode(XmlNodeType.Element, "BookReamrk", null); ;
                xmlDoc.AppendChild(root);
            }
            XmlElement xelKey = xmlDoc.CreateElement("Reamrk");
            XmlAttribute xelUrle = xmlDoc.CreateAttribute("Url");
            xelUrle.InnerText = Values;
            xelKey.SetAttributeNode(xelUrle);

            XmlAttribute xelTitle = xmlDoc.CreateAttribute("Title");
            xelTitle.InnerText = Title;
            xelKey.SetAttributeNode(xelTitle);

            root.AppendChild(xelKey);

            xmlDoc.Save(XMLPath);
            return true;
        }

        public List<BookMark> ReadXML()
        {
            List<BookMark> BookReamrkList = new List<BookMark>();
            XmlDocument xmlDoc = new XmlDocument();

            //如果文件存在      
            if (File.Exists(XMLPath))
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;//忽略文档里面的注释
                XmlReader reader = XmlReader.Create(@XMLPath, settings);
                xmlDoc.Load(reader);

                XmlNode xn = xmlDoc.SelectSingleNode("vine-windows-standard/BookReamrk");
                XmlNodeList xnl = xn.ChildNodes;
                foreach (XmlNode xm1 in xnl)
                {

                    XmlElement xe = (XmlElement)xm1;
                    if ((xe.GetAttribute("Url").ToString() != "") && (xe.GetAttribute("Title").ToString() != ""))
                    {
                        BookMark bk = new BookMark();
                        bk.BookUrl = xe.GetAttribute("Url").ToString();
                        bk.Title = xe.GetAttribute("Title").ToString();
                    
                        BookReamrkList.Add(bk);
                    }
                }
                reader.Close();
            }
            return BookReamrkList;
        }

        public void CreateNode(XmlDocument xmldoc, XmlNode parentnode, string name, string value)
        {
            XmlNode node = xmldoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentnode.AppendChild(node);
        }

        public void DelectNode(string Values, string Title)
        {
            XmlDocument xmlDoc = new XmlDocument();

            //如果文件存在      
            if (File.Exists(XMLPath))
            {
                xmlDoc.Load(XMLPath);
                XmlNode xn = xmlDoc.SelectSingleNode("vine-windows-standard/BookReamrk");
                XmlNodeList xnl = xn.ChildNodes;

                foreach (XmlNode xm1 in xnl)
                {
                    XmlElement xe = (XmlElement)xm1;
                    if ((xe.GetAttribute("Url") == Values) && (xe.GetAttribute("Title") == Title))
                    {
                        xm1.RemoveAll();//删除该节点的全部内容   
                        xn.RemoveChild(xm1);
                        break;
                    }
                }
                xmlDoc.Save(XMLPath);
            }
        }

        public bool CheckNode(string Values, string Title)
        {
            bool result = false;
            XmlDocument xmlDoc = new XmlDocument();

            //如果文件存在      
            if (File.Exists(XMLPath))
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;//忽略文档里面的注释
                XmlReader reader = XmlReader.Create(@XMLPath, settings);
                xmlDoc.Load(reader);

                XmlNode xn = xmlDoc.SelectSingleNode("vine-windows-standard/BookReamrk");
                XmlNodeList xnl = xn.ChildNodes;
                foreach (XmlNode xm1 in xnl)
                {

                    XmlElement xe = (XmlElement)xm1;
                    if (xe.GetAttribute("Url").ToString() == Values)
                    {
                        result = true;
                        break;
                    }
                }
                reader.Close();
            }
            return result;
        }

        public void WriteZoom(string FC)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XMLPathZ);

            XmlNode root = xmlDoc.SelectSingleNode("vine-windows-standard/Zoom");
            if (root == null)
            {
                XmlElement root2 = xmlDoc.CreateElement("Zoom");
                root2.SetAttribute("Factor", FC);
                xmlDoc.AppendChild(root2);

            }
            else
            {
                XmlElement xe = (XmlElement)root;
                xe.SetAttribute("Factor", FC);
            }
            xmlDoc.Save(XMLPathZ);
        }

        public string ReadZoom()
        {
            List<BookMark> BookReamrkList = new List<BookMark>();
            XmlDocument xmlDoc = new XmlDocument();
            string Factor = "100";
            //如果文件存在      
            if (File.Exists(XMLPathZ))
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;//忽略文档里面的注释
                XmlReader reader = XmlReader.Create(@XMLPathZ, settings);
                xmlDoc.Load(reader);

                XmlNode xn = xmlDoc.SelectSingleNode("vine-windows-standard/Zoom");

                XmlElement xe = (XmlElement)xn;
                Factor = xe.GetAttribute("Factor").ToString();

                reader.Close();
            }
            return Factor;
        }

        public void WriteIsMaxForm(string IsMax)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XMLPathZ);

            XmlNode root = xmlDoc.SelectSingleNode("vine-windows-standard/IsMaxForm");
            if (root == null)
            {
                XmlElement root2 = xmlDoc.CreateElement("Zoom");
                root2.SetAttribute("IsMax", IsMax);
                xmlDoc.AppendChild(root2);

            }
            else
            {
                XmlElement xe = (XmlElement)root;
                xe.SetAttribute("IsMax", IsMax);
            }

            xmlDoc.Save(XMLPathZ);
        }
        public string ReadIsMaxForm()
        {
            List<BookMark> BookReamrkList = new List<BookMark>();
            XmlDocument xmlDoc = new XmlDocument();
            string IsMax = "0";
            //如果文件存在      
            if (File.Exists(XMLPathZ))
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;//忽略文档里面的注释
                XmlReader reader = XmlReader.Create(@XMLPathZ, settings);
                xmlDoc.Load(reader);

                XmlNode xn = xmlDoc.SelectSingleNode("vine-windows-standard/IsMaxForm");

                XmlElement xe = (XmlElement)xn;
                IsMax = xe.GetAttribute("IsMax").ToString();

                reader.Close();
            }
            return IsMax;
        }

        public void CleanNode()
        {
            XmlDocument xmlDoc = new XmlDocument();

            //如果文件存在      
            if (File.Exists(XMLPath))
            {
                xmlDoc.Load(XMLPath);
                XmlNode xn = xmlDoc.SelectSingleNode("vine-windows-standard/BookReamrk");
                //XmlNodeList xnl = xn.ChildNodes;
                XmlNodeList xnl = xmlDoc.SelectNodes("vine-windows-standard/BookReamrk/Reamrk");
                foreach (XmlNode xm1 in xnl)
                {
                    xm1.RemoveAll();//删除该节点的全部内容  
                    xn.RemoveChild(xm1);
                }
                xmlDoc.Save(XMLPath);
            }
        }

        public void xmlInit()
        {
            string url = "";
            url = MyApp.getInstance().getFormUrl("FrmMessages");
            WriteXML(url, "消息管理");
            url = MyApp.getInstance().getFormUrl("TFrmYGTMyAccount");
            WriteXML(url, "更改我的资料");
            url = MyApp.getInstance().getFormUrl("TFrmMyInputMode");
            WriteXML(url, "设置我的喜好");
        }

        public void UpdateFile(string UpXml)
        {
            if (File.Exists(XMLPath))
                File.Delete(XMLPath);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(UpXml);
            xmlDoc.Save(XMLPath);
        }

        public string getXMLString()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XMLPath);
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, null);
            writer.Formatting = Formatting.Indented;
            xmlDoc.Save(writer);
            StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
            stream.Position = 0;
            string xmlString = sr.ReadToEnd();
            sr.Close();
            stream.Close();
            return xmlString;
        }
    }
}
