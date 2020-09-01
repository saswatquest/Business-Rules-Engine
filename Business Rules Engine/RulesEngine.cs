using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Business_Rules_Engine
{
    public class RulesEngine
    {
        public static ExpandoObject Loadconfig(string configpath)
        {
            //dynamic root = new ExpandoObject();
            ExpandoObject root = new ExpandoObject();
            Dictionary<string, string> connectdata = new Dictionary<string, string>();

            root = GetConstantsData(configpath);

            var config = root.FirstOrDefault(x => x.Key == "Rules").Value;

            if (config == null)
            {
                throw new Exception("Invalid configuraton file...");
            }

            return (ExpandoObject)config;
        }

        public static Dictionary<string, string> LoadDatabyNode(ExpandoObject config, string Node)
        {
            Dictionary<string, string> connectordata = new Dictionary<string, string>();

            ExpandoObject result3 = (ExpandoObject)(config.FirstOrDefault(x => x.Key == Node).Value);

            connectordata = result3.ToDictionary(x => x.Key, x => x.Value.ToString());

            return connectordata;
        }

        public static string LoadDatabyKey(ExpandoObject config, string Key)
        {
            string value = null;

            if (Key != null)
            {
                value = (string)(config.FirstOrDefault(x => x.Key == Key).Value);
            }

            return value;
        }


        private static dynamic GetConstantsData(string xmlFileName)
        {
            ExpandoObject root = new ExpandoObject();
            if (string.IsNullOrEmpty(xmlFileName))
            {
                return root;
            }
            // Load an XML document.
            var xDoc = XDocument.Load(new StreamReader(xmlFileName));

            // Convert the XML document in to a dynamic C# object.
            Parse(root, xDoc.Elements().First());
            return root;
        }

        private static void AddProperty(dynamic parent, string name, object value)
        {
            try
            {
                if (parent is List<dynamic>)
                {
                    (parent as List<dynamic>).Add(value);
                }
                else
                {
                    (parent as IDictionary<string, object>)[name] = value;
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        private static void Parse(dynamic parent, XElement node)
        {
            try
            {
                if (node.HasElements)
                {
                    if (node.Elements(node.Elements().First().Name.LocalName).Count() > 1)
                    {
                        //list
                        var item = new ExpandoObject();
                        var list = new List<dynamic>();
                        foreach (var element in node.Elements())
                        {
                            Parse(list, element);
                        }
                        AddProperty(item, node.Elements().First().Name.LocalName, list);
                        AddProperty(parent, node.Name.ToString(), item);
                    }
                    else
                    {
                        var item = new ExpandoObject();
                        foreach (var attribute in node.Attributes())
                        {
                            AddProperty(item, attribute.Name.ToString(), attribute.Value.Trim());
                        }
                        //element
                        foreach (var element in node.Elements())
                        {
                            Parse(item, element);
                        }
                        AddProperty(parent, node.Name.ToString(), item);
                    }
                }
                else
                {
                    AddProperty(parent, node.Name.ToString(), node.Value.Trim());
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }


    }
}
