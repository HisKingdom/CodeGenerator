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

            //反射程序集的方式生成相应代码 
            string className = "Order";
            var metaTableInfoList = MetaTableInfo.GetMetaTableInfoListForAssembly(className);

            //得到主键类型
            var propertyType = metaTableInfoList.FirstOrDefault(m => m.Name == "Id").PropertyType;
            // server端生成
            CodeGeneratorHelper.SetAppServiceIntercafeClass(className, propertyType);
            CodeGeneratorHelper.SetAppServiceClass(className, propertyType);
            CodeGeneratorHelper.SetCreateOrEditInputClass(className, metaTableInfoList);
            CodeGeneratorHelper.SetGetForEditOutputClass(className);
            CodeGeneratorHelper.SetGetInputClass(className);
            CodeGeneratorHelper.SetListDtoClass(className, metaTableInfoList);
            CodeGeneratorHelper.SetCreateOrEditInputClass(className, metaTableInfoList);
            CodeGeneratorHelper.SetExportingIntercafeClass(className);
            CodeGeneratorHelper.SetExportingClass(className, metaTableInfoList);
            //CodeGeneratorHelper.SetConstsClass(className);
            CodeGeneratorHelper.SetAppPermissions(className);
            ////client
            CodeGeneratorHelper.SetControllerClass(className, propertyType);
            CodeGeneratorHelper.SetCreateOrEditHtmlTemplate(className, metaTableInfoList);
            CodeGeneratorHelper.SetCreateOrEditJs(className);
            CodeGeneratorHelper.SetCreateOrEditViewModelClass(className);
            CodeGeneratorHelper.SetIndexHtmlTemplate(className, metaTableInfoList);
            CodeGeneratorHelper.SetIndexJsTemplate(className, metaTableInfoList);
        }



    }
}
