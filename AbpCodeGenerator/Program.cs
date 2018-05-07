using System;
using System.IO;
using System.Text;
using AbpCodeGenerator.Lib;

namespace AbpCodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string className = "Test";
            string Primary_Key_Inside_Tag_Here = "<long>";
            CodeGeneratorHelper.SetAppServiceIntercafeClass(className, Primary_Key_Inside_Tag_Here);
            CodeGeneratorHelper.SetAppServiceClass(className, Primary_Key_Inside_Tag_Here);

        }


      
    }
}
