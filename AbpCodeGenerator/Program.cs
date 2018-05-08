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


            string className = "User";

            var metaTableInfoList = MetaTableInfo.GetMetaTableInfoList(className);
            ////得到主键类型
            //var propertyType = metaTableInfoList.FirstOrDefault(m => m.Name == "Id").PropertyType;

            //CodeGeneratorHelper.SetAppServiceIntercafeClass(className, propertyType);
            //CodeGeneratorHelper.SetAppServiceClass(className, propertyType);
            //CodeGeneratorHelper.SetCreateOrEditInputClass(className, metaTableInfoList);
        }



    }
}
