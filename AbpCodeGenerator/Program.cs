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


            #region 获取数据源的两种方式mysql和反射程序集
            //mysql
            //string tableName = "abpusers";//表名
            //var metaTableInfoList = MetaTableInfo.GetMetaTableInfoListForMysql(tableName);

            //反射程序集的方式生成相应代码 
            string className = "Order";//跟类名保持一致
            var metaTableInfoList = MetaTableInfo.GetMetaTableInfoListForAssembly(className);
            #endregion

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
            //CodeGeneratorHelper.SetConstsClass(className); 若使用 SetAppPermissions，SetAppAuthorizationProvider，SetZh_CN_LocalizationDictionary_Here 三个方法 就可弃用该方法  

            CodeGeneratorHelper.SetAppPermissions(className);
            CodeGeneratorHelper.SetAppAuthorizationProvider(className);
            CodeGeneratorHelper.SetZh_CN_LocalizationDictionary_Here(className, metaTableInfoList[0].ClassAnnotation);

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
