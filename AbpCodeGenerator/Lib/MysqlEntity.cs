using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbpCodeGenerator.Lib
{
    public class MysqlEntity
    {


        public MysqlEntity()
        {
            Fields = new List<Field>();
        }

        public MysqlEntity(string name)
            : this()
        {
            EntityName = name;
        }

        public string EntityName { get; set; }
        public List<Field> Fields { get; set; }

        /// <summary>
        /// 得到指定db下所有table的信息
        /// </summary>
        /// <param name="dbName">数据库名</param>
        /// <returns></returns>
        public static List<MysqlEntity> GetMysqlEntities(string dbName)
        {
            var entityList = new List<MysqlEntity>();
            string dbConn = Configuration.MysqlConnection;
            var conn = new MySqlConnection(dbConn);
            try
            {
                conn.Open();
                var cmd = string.Format(@"SELECT `information_schema`.`COLUMNS`.`TABLE_SCHEMA`
                                                    ,`information_schema`.`COLUMNS`.`TABLE_NAME`
                                                    ,`information_schema`.`COLUMNS`.`COLUMN_NAME`
                                                    ,`information_schema`.`COLUMNS`.`DATA_TYPE`
                                                    ,`information_schema`.`COLUMNS`.`COLUMN_COMMENT`
                                                FROM `information_schema`.`COLUMNS`
                                                WHERE `information_schema`.`COLUMNS`.`TABLE_SCHEMA` = '{0}' ", dbName);
                var mySqlCommand = new MySqlCommand(cmd, conn);
                using (var reader = mySqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var db = reader["TABLE_SCHEMA"].ToString();
                        var table = reader["TABLE_NAME"].ToString();
                        var column = reader["COLUMN_NAME"].ToString();
                        var type = reader["DATA_TYPE"].ToString();
                        var comment = reader["COLUMN_COMMENT"].ToString();

                        var entity = entityList.FirstOrDefault(x => x.EntityName == table);
                        if (entity == null)
                        {
                            entity = new MysqlEntity(table);
                            entity.Fields.Add(new Field
                            {
                                Name = column,
                                Type = GetCLRType(type),
                                Comment = comment
                            });

                            entityList.Add(entity);
                        }
                        else
                        {
                            entity.Fields.Add(new Field
                            {
                                Name = column,
                                Type = GetCLRType(type),
                                Comment = comment
                            });
                        }
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return entityList;
        }

        public static MysqlEntity GetMysqlEntityByTableName(string tableName)
        {
            var entityList = GetMysqlEntities(Configuration.DbName);
            var entity = entityList.FirstOrDefault(x => x.EntityName == tableName);
            return entity;
        }

        public static string GetCLRType(string dbType)
        {
            switch (dbType)
            {
                case "tinyint":
                case "smallint":
                case "mediumint":
                case "int":
                case "integer":
                    return "int";
                case "bigint":
                    return "long";
                case "double":
                    return "double";
                case "float":
                    return "float";
                case "decimal":
                    return "decimal";
                case "numeric":
                case "real":
                    return "decimal";
                case "bit":
                    return "bool";
                case "date":
                case "time":
                case "year":
                case "datetime":
                case "timestamp":
                    return "DateTime";
                case "tinyblob":
                case "blob":
                case "mediumblob":
                case "longblog":
                case "binary":
                case "varbinary":
                    return "byte[]";
                case "char":
                case "varchar":
                case "tinytext":
                case "text":
                case "mediumtext":
                case "longtext":
                    return "string";
                case "point":
                case "linestring":
                case "polygon":
                case "geometry":
                case "multipoint":
                case "multilinestring":
                case "multipolygon":
                case "geometrycollection":
                case "enum":
                case "set":
                default:
                    return dbType;
            }
        }

    }

    public class Field
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; }
    }
}
