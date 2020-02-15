using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace 五子棋.AI
{
    public class DNAFile
    {
        private string _file = "_DNAFile.xml";
        private XmlDocument _xml = new XmlDocument();
        private XmlNode _root;

        public DNAFile()
        {
            _root = _xml.CreateElement("root");
            _xml.AppendChild(_root);
        }
        public bool Load(string file = "")
        {
            if(!string.IsNullOrEmpty(file))
            {
                _file = file;
            }
            if(!File.Exists(_file))
            {
                return false;
            }
            _xml.Load(_file);
            _root = _xml.FirstChild;
            return true;
        }

        public void Save(string file = "")
        {
            if (!string.IsNullOrEmpty(file))
            {
                _file = file;
            }
            _xml.Save(_file);
        }

        public List<KeyValuePair<string, float>> GetAllDatas()
        {
            List<KeyValuePair<string, float>> list = new List<KeyValuePair<string, float>>();
            if(_root != null)
            {
                for(int i = 0; i < _root.ChildNodes.Count; i ++)
                {
                    KeyValuePair<string, float> pair = new KeyValuePair<string, float>(_root.ChildNodes[i].Name, GetFloat(_root.ChildNodes[i].Name, 1f));
                    list.Add(pair);
                }
            }
            return list;
        }

        public int GetInt(string key, int defaultValue = 0)
        {
            XmlElement element = GetElement(key);
            if (element != null)
            {
                Int32.TryParse(element.InnerText, out defaultValue);
            }
            return defaultValue;
        }
        public float GetFloat(string key, float defaultValue = 0f)
        {
            XmlElement element = GetElement(key);
            if (element != null)
            {
                float.TryParse(element.InnerText, out defaultValue);
            }
            return defaultValue;
        }
        public string GetString(string key, string defaultValue = "")
        {
            XmlElement element = GetElement(key);
            if(element != null)
            {
                defaultValue = element.InnerText;
            }
            return defaultValue;
        }
        private XmlElement GetElement(string key)
        {
            if(_root != null)
            {
                for(int i = 0; i < _root.ChildNodes.Count; i ++)
                {
                    if(_root.ChildNodes[i].Name == key)
                    {
                        return _root.ChildNodes[i] as XmlElement;
                    }
                }
            }
            return null;
        }
        public void SetInt(string key, int v)
        {
            XmlElement node = GetElement(key);
            if(node == null)
            {
                node = _xml.CreateElement(key);
                _root.AppendChild(node);
            }
            node.InnerText = v.ToString();
            
        }
        public void SetFloat(string key, float v)
        {
            XmlElement node = GetElement(key);
            if (node == null)
            {
                node = _xml.CreateElement(key);
                _root.AppendChild(node);
            }
            node.InnerText = v.ToString();
            
        }
        public void SetString(string key, string v)
        {
            if(v == null)
            {
                return;
            }
            XmlElement node = GetElement(key);
            if (node == null)
            {
                node = _xml.CreateElement(key);
                _root.AppendChild(node);
            }
            node.InnerText = v.ToString();
        }


    }
}
