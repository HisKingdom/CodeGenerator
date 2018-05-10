using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace AbpCodeGenerator.Lib
{
    public class MetaTableInfo
    {
        /// <summary>
        /// 类的注释
        /// </summary>
        public string ClassAnnotation { get; set; }

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

        public static List<MetaTableInfo> GetMetaTableInfoListForMysql(string tableName)
        {
            var mysqlEntity = MysqlEntity.GetMysqlEntityByTableName(tableName);
            var metaTableInfoList = new List<MetaTableInfo>();
            foreach (var item in mysqlEntity.Fields)
            {
                var metaTableInfo = new MetaTableInfo
                {
                    Name = item.Name,
                    Annotation = item.Comment,
                    PropertyType = item.Type
                };
                metaTableInfoList.Add(metaTableInfo);
            }

            return metaTableInfoList;
        }

        /// <summary>
        /// 根据类名 反射得到类的信息
        /// </summary>
        /// <param name="className">类名</param>
        /// <returns></returns>
        public static List<MetaTableInfo> GetMetaTableInfoListForAssembly(string className)
        {

            var list = new List<MetaTableInfo>();
            Type[] types = Assembly.LoadFrom(Configuration.SourceAssembly).GetTypes();
            foreach (var type in types)
            {
                if (type.Name.Equals(className))
                {
                    var classAnnotation = string.Empty;
                    try
                    {
                        //获取类的注释
                        XmlElement xmlFromType = DocsByReflection.XMLFromType(type.GetTypeInfo());
                        classAnnotation = xmlFromType["summary"].InnerText.Trim();
                    }
                    catch
                    {


                    }

                    foreach (PropertyInfo properties in type.GetProperties())
                    {
                        var metaTableInfo = new MetaTableInfo();
                        try
                        {
                            XmlElement documentation = DocsByReflection.XMLFromMember(type.GetProperty(properties.Name));
                            metaTableInfo.Annotation = documentation["summary"].InnerText.Trim();

                            metaTableInfo.ClassAnnotation = classAnnotation;
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
