using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace AbpCodeGenerator.Lib
{
    public class MetaTableInfo
    {
    

        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 属性类型
        /// </summary>
        public string PropertyType { get; set; }

        /// <summary>
        /// 属性注释
        /// </summary>
        public string Annotation { get; set; }

        public static List<MetaTableInfo> GetMetaTableInfoList(string className)
        {

            var list = new List<MetaTableInfo>();
            Type[] types = Assembly.LoadFrom(Configuration.SourceAssembly).GetTypes();
            foreach (var type in types)
            {
                if (type.Name.Equals(className))
                {
                    foreach (PropertyInfo properties in type.GetProperties())
                    {
                        var metaTableInfo = new MetaTableInfo();
                        try
                        {
                            XmlElement documentation = DocsByReflection.XMLFromMember(type.GetProperty(properties.Name));
                            metaTableInfo.Annotation = documentation["summary"].InnerText.Trim();
                        }
                        catch
                        {
                            metaTableInfo.Annotation = "";
                        }
                        metaTableInfo.Name = properties.Name;
                        if (properties.PropertyType == typeof(int))
                        {
                            metaTableInfo.PropertyType = "int";
                        }
                        else if (properties.PropertyType == typeof(int?))
                        {
                            metaTableInfo.PropertyType = "int?";
                        }
                        else if (properties.PropertyType == typeof(long))
                        {
                            metaTableInfo.PropertyType = "long";
                        }
                        else if (properties.PropertyType == typeof(long?))
                        {
                            metaTableInfo.PropertyType = "long?";
                        }
                        else if (properties.PropertyType == typeof(DateTime?))
                        {
                            metaTableInfo.PropertyType = "DateTime?";
                        }
                        else if (properties.PropertyType == typeof(decimal))
                        {
                            metaTableInfo.PropertyType = "decimal";
                        }
                        else if (properties.PropertyType == typeof(decimal?))
                        {
                            metaTableInfo.PropertyType = "decimal?";
                        }
                        else if (properties.PropertyType == typeof(string))
                        {
                            metaTableInfo.PropertyType = "string";
                        }
                        else if (properties.PropertyType == typeof(bool))
                        {
                            metaTableInfo.PropertyType = "bool";
                        }
                        else
                        {
                            metaTableInfo.PropertyType = properties.PropertyType.ToString().Split('.').Last().Replace("]", "");
                        }
                        list.Add(metaTableInfo);
                    }
                }
            }

            return list;
        }

   
    }
}
