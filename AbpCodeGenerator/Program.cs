using System;
using System.IO;
using System.Text;
using System.Linq;
using AbpCodeGenerator.Lib;
using Microsoft.Extensions.Configuration;

namespace AbpCodeGenerator
{
    class Program
    {

        static void Main(string[] args)
        {


            //mysql
            //string tableName = "abpusers";
            //var metaTableInfoList = MetaTableInfo.GetMetaTableInfoListForMysql(tableName);

            //程序集
            string className = "Usert";
            var metaTableInfoList = MetaTableInfo.GetMetaTableInfoListForAssembly(className);

            //得到主键类型
            var propertyType = metaTableInfoList.FirstOrDefault(m => m.Name == "Id").PropertyType;
            // 生成接口信息相关代码
            CodeGeneratorHelper.SetAppServiceIntercafeClass(className, propertyType);
            //CodeGeneratorHelper.SetAppServiceClass(className, propertyType);
            CodeGeneratorHelper.SetCreateOrEditInputClass(className, metaTableInfoList);
        }



    }
}
