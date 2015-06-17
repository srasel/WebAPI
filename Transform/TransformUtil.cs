using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using Newtonsoft.Json;

namespace WebAPI.Transform
{
    public class TransformUtil
    {
        public static string JsonToXML(string json)
        {
            XmlDocument doc = JsonConvert.DeserializeXmlNode(json, "root");
            //XDocument.Parse(doc.InnerXml);
            return doc.InnerXml;
        }
    }
}