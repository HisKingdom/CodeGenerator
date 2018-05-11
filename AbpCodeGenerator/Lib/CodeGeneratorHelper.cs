using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace AbpCodeGenerator.Lib
{
    public class CodeGeneratorHelper
    {

        #region client

        /// <summary>
        /// 生成ControllerClass
        /// </summary>
        /// <param name="className"></param>
        public static void SetControllerClass(string className, string primary_Key_Here)
        {
            string appServiceIntercafeClassDirectory = Configuration.RootDirectory + @"\Client\Mvc\ControllerClass\MainTemplate.txt";
            var templateContent = Read(appServiceIntercafeClassDirectory);

            templateContent = templateContent.Replace("{{Namespace_Here}}", Configuration.Namespace_Here)
                                             .Replace("{{Namespace_Relative_Full_Here}}", className)
                                             .Replace("{{Entity_Name_Plural_Here}}", className)
                                             .Replace("{{Entity_Name_Here}}", className)
                                             .Replace("{{Permission_Name_Here}}", $"Pages_Administration_{className}")
                                             .Replace("{{App_Area_Name_Here}}", Configuration.App_Area_Name)
                                             .Replace("{{Primary_Key_Here}}", primary_Key_Here)
                                             .Replace("{{Project_Name_Here}}", Configuration.Controller_Base_Class)
                                             .Replace("{{entity_Name_Plural_Here}}", GetFirstToLowerStr(className))
                                             ;
            Write(Configuration.Web_Mvc_Directory + "Areas\\Admin\\Controllers\\", className + "Controller.cs", templateContent);
        }


        /// <summary>
        /// 生成CreateOrEditHtmlTemplate
        /// </summary>
        /// <param name="className"></param>
        public static void SetCreateOrEditHtmlTemplate(string className, List<MetaTableInfo> metaTableInfoList)
        {
            string appServiceIntercafeClassDirectory = Configuration.RootDirectory + @"\Client\Mvc\CreateOrEditHtmlTemplate\MainTemplate.txt";
            var templateContent = Read(appServiceIntercafeClassDirectory);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < metaTableInfoList.Count; i++)
            {
                sb.AppendLine("<div class=\"form-group m-form__group row\">");
                if (i % 2 == 0)
                {
                    sb.AppendLine($"<label class=\"col-xl-1 col-lg-1 col-form-label\">{metaTableInfoList[i].Annotation}</label>");
                    sb.AppendLine(" <div class=\"col-xl-5 col-lg-5\">");
                    if (metaTableInfoList[i].PropertyType == "string")
                    {
                        sb.AppendLine("  <input class=\"form-control@(Model." + className + "." + metaTableInfoList[i].Name + ".IsNullOrEmpty() ? \"\" : \" edited\")\"");
                    }
                    else
                    {
                        sb.AppendLine("  <input class=\"form-control\"");
                    }
                    sb.AppendLine("type=\"text\" name=\"" + metaTableInfoList[i].Name + "\"");
                    sb.AppendLine("value=\"@Model." + className + "." + metaTableInfoList[i].Name + "\" />");
                    sb.AppendLine("</div>");

                    if (i + 1 < metaTableInfoList.Count)
                    {
                        sb.AppendLine($"<label class=\"col-xl-1 col-lg-1 col-form-label\">{metaTableInfoList[i + 1].Annotation}</label>");
                        sb.AppendLine(" <div class=\"col-xl-5 col-lg-5\">");
                        if (metaTableInfoList[i + 1].PropertyType == "string")
                        {
                            // <input type="datetime"  class="form-control date-picker">
                            sb.AppendLine("  <input class=\"form-control@(Model." + className + "." + metaTableInfoList[i + 1].Name + ".IsNullOrEmpty() ? \"\" : \" edited\")\"");
                        }
                        else
                        {
                            sb.AppendLine("  <input class=\"form-control\"");
                        }
                        sb.AppendLine("type=\"text\" name=\"" + metaTableInfoList[i + 1].Name + "\"");
                        sb.AppendLine("value=\"@Model." + className + "." + metaTableInfoList[i + 1].Name + "\" />");
                        sb.AppendLine("</div>");
                    }
                }


                sb.AppendLine("</div> ");
            }

            var property_Looped_Template_Here = sb.ToString();

            templateContent = templateContent.Replace("{{Namespace_Here}}", Configuration.Namespace_Here)
                                             .Replace("{{Namespace_Relative_Full_Here}}", className)
                                             .Replace("{{Entity_Name_Plural_Here}}", className)
                                             .Replace("{{Entity_Name_Here}}", className)
                                             .Replace("{{App_Area_Name_Here}}", Configuration.App_Area_Name)
                                             .Replace("{{Property_Looped_Template_Here}}", property_Looped_Template_Here)
                                             .Replace("{{entity_Name_Plural_Here}}", GetFirstToLowerStr(className))
                                             ;
            Write(Configuration.Web_Mvc_Directory + "Areas\\Admin\\Views\\" + className + "\\", "_CreateOrEditModal.cshtml", templateContent);
        }


        /// <summary>
        /// 生成CreateOrEditJs
        /// </summary>
        /// <param name="className"></param>
        public static void SetCreateOrEditJs(string className)
        {
            string appServiceIntercafeClassDirectory = Configuration.RootDirectory + @"\Client\Mvc\CreateOrEditJs\MainTemplate.txt";
            var templateContent = Read(appServiceIntercafeClassDirectory);

            templateContent = templateContent.Replace("{{Namespace_Here}}", Configuration.Namespace_Here)
                                             .Replace("{{Namespace_Relative_Full_Here}}", className)
                                             .Replace("{{Entity_Name_Plural_Here}}", className)
                                             .Replace("{{Entity_Name_Here}}", className)
                                             .Replace("{{entity_Name_Here}}", GetFirstToLowerStr(className))
                                             .Replace("{{entity_Name_Plural_Here}}", GetFirstToLowerStr(className))
                                             ;
            Write(Configuration.Web_Mvc_Directory + "\\wwwroot\\view-resources\\Areas\\Admin\\Views\\" + className + "\\", "_CreateOrEditModal.js", templateContent);
        }



        /// <summary>
        /// 生成CreateOrEditViewModelClass
        /// </summary>
        /// <param name="className"></param>
        public static void SetCreateOrEditViewModelClass(string className)
        {
            string appServiceIntercafeClassDirectory = Configuration.RootDirectory + @"\Client\Mvc\CreateOrEditViewModelClass\MainTemplate.txt";
            var templateContent = Read(appServiceIntercafeClassDirectory);
            templateContent = templateContent.Replace("{{Namespace_Here}}", Configuration.Namespace_Here)
                                             .Replace("{{Namespace_Relative_Full_Here}}", className)
                                             .Replace("{{Entity_Name_Plural_Here}}", className)
                                             .Replace("{{Entity_Name_Here}}", className)
                                             .Replace("{{App_Area_Name_Here}}", Configuration.App_Area_Name)
                                             ;
            Write(Configuration.Web_Mvc_Directory + "Areas\\Admin\\Models\\" + className + "s\\", "CreateOrEdit" + className + "ModalViewModel.cs", templateContent);
        }


        /// <summary>
        /// 生成IndexHtmlTemplate
        /// </summary>
        /// <param name="className"></param>
        public static void SetIndexHtmlTemplate(string className, List<MetaTableInfo> metaTableInfoList)
        {
            string appServiceIntercafeClassDirectory = Configuration.RootDirectory + @"\Client\Mvc\IndexHtmlTemplate\MainTemplate.txt";
            var templateContent = Read(appServiceIntercafeClassDirectory);

            StringBuilder sb = new StringBuilder();

            foreach (var item in metaTableInfoList)
            {
                sb.AppendLine(" <th>" + item.Annotation + "</th>");
            }
            var property_Looped_Template_Here = sb.ToString();

            templateContent = templateContent.Replace("{{Namespace_Here}}", Configuration.Namespace_Here)
                                             .Replace("{{Entity_Name_Plural_Here}}", className)
                                             .Replace("{{Entity_Name_Here}}", className)
                                             .Replace("{{App_Area_Name_Here}}", Configuration.App_Area_Name)
                                             .Replace("{{Property_Looped_Template_Here}}", property_Looped_Template_Here)
                                             .Replace("{{Permission_Name_Here}}", $"Pages_Administration_{className}")
                                             ;
            Write(Configuration.Web_Mvc_Directory + "Areas\\Admin\\Views\\" + className + "\\", "Index.cshtml", templateContent);
        }


        /// <summary>
        /// 生成IndexJsTemplate
        /// </summary>
        /// <param name="className"></param>
        public static void SetIndexJsTemplate(string className, List<MetaTableInfo> metaTableInfoList)
        {
            string appServiceIntercafeClassDirectory = Configuration.RootDirectory + @"\Client\Mvc\IndexJsTemplate\MainTemplate.txt";
            var templateContent = Read(appServiceIntercafeClassDirectory);

            StringBuilder sb = new StringBuilder();
            var i = 1;
            foreach (var item in metaTableInfoList)
            {
                sb.AppendLine(", {");
                sb.AppendLine("targets: " + i + ",");
                sb.AppendLine("data: \"" + GetFirstToLowerStr(item.Name) + "\"");
                sb.AppendLine("}");
                i++;
            }
            var property_Looped_Template_Here = sb.ToString();
            templateContent = templateContent
                                             .Replace("{{Entity_Name_Plural_Here}}", className)
                                             .Replace("{{Entity_Name_Here}}", className)
                                             .Replace("{{entity_Name_Here}}", GetFirstToLowerStr(className))
                                             .Replace("{{entity_Name_Plural_Here}}", GetFirstToLowerStr(className))
                                             .Replace("{{App_Area_Name_Here}}", Configuration.App_Area_Name)
                                             .Replace("{{Property_Looped_Template_Here}}", property_Looped_Template_Here)
                                             .Replace("{{Permission_Value_Here}}", "Pages.Administration." + className + "")
                                             ;
            Write(Configuration.Web_Mvc_Directory + "\\wwwroot\\view-resources\\Areas\\Admin\\Views\\" + className + "\\", "Index.js", templateContent);
        }

        #endregion


        #region Server
        /// <summary>
        /// 生成接口信息
        /// </summary>
        /// <param name="className"></param>
        /// <param name="primary_Key_Inside_Tag_Here">主键类型</param>
        public static void SetAppServiceIntercafeClass(string className, string primary_Key_Inside_Tag_Here)
        {
            string appServiceIntercafeClassDirectory = Configuration.RootDirectory + @"\Server\AppServiceIntercafeClass\MainTemplate.txt";
            var templateContent = Read(appServiceIntercafeClassDirectory);

            templateContent = templateContent.Replace("{{Namespace_Here}}", Configuration.Namespace_Here)
                                             .Replace("{{Namespace_Relative_Full_Here}}", className)
                                             .Replace("{{Entity_Name_Plural_Here}}", className)
                                             .Replace("{{Entity_Name_Here}}", className)
                                             .Replace("{{Primary_Key_Inside_Tag_Here}}", primary_Key_Inside_Tag_Here)
                                             ;
            Write(Configuration.Application_Directory + className + "s\\", "I" + className + "AppService.cs", templateContent);
        }

        /// <summary>
        /// 生成接口实现类信息
        /// </summary>
        /// <param name="className"></param>
        /// <param name="Primary_Key_Inside_Tag_Here">主键类型</param>
        public static void SetAppServiceClass(string className, string Primary_Key_Inside_Tag_Here)
        {
            string appServiceIntercafeClassDirectory = Configuration.RootDirectory + @"\Server\AppServiceClass\MainTemplate.txt";
            var templateContent = Read(appServiceIntercafeClassDirectory);
            var Primary_Key_With_Comma_Here = Primary_Key_Inside_Tag_Here;
            if (Primary_Key_Inside_Tag_Here != "int")
            {
                Primary_Key_With_Comma_Here = "," + Primary_Key_Inside_Tag_Here;
            }
            templateContent = templateContent.Replace("{{Namespace_Here}}", Configuration.Namespace_Here)
                                             .Replace("{{Namespace_Relative_Full_Here}}", className)
                                             .Replace("{{Entity_Name_Plural_Here}}", className)
                                             .Replace("{{Entity_Name_Here}}", className)
                                             .Replace("{{Primary_Key_Inside_Tag_Here}}", Primary_Key_Inside_Tag_Here)
                                             .Replace("{{entity_Name_Here}}", GetFirstToLowerStr(className))
                                             .Replace("{{Permission_Name_Here}}", $"Pages_Administration_{className}")
                                             .Replace("{{Project_Name_Here}}", Configuration.Application_AppServiceBase)
                                             .Replace("{{Primary_Key_With_Comma_Here}}", Primary_Key_With_Comma_Here)
                                             ;
            Write(Configuration.Application_Directory + className + "s\\", className + "AppService.cs", templateContent);
        }

        /// <summary>
        /// 生成Exporting接口信息
        /// </summary>
        /// <param name="className"></param>
        /// <param name="primary_Key_Inside_Tag_Here">主键类型</param>
        public static void SetExportingIntercafeClass(string className)
        {
            string appServiceIntercafeClassDirectory = Configuration.RootDirectory + @"\Server\ExportingIntercafeClass\MainTemplate.txt";
            var templateContent = Read(appServiceIntercafeClassDirectory);

            templateContent = templateContent.Replace("{{Namespace_Here}}", Configuration.Namespace_Here)
                                             .Replace("{{Namespace_Relative_Full_Here}}", className)
                                             .Replace("{{Entity_Name_Plural_Here}}", className)
                                             .Replace("{{Entity_Name_Here}}", className)
                                             .Replace("{{entity_Name_Here}}", GetFirstToLowerStr(className))
                                             ;
            Write(Configuration.Application_Directory + className + "s\\Exporting\\", "I" + className + "ListExcelExporter.cs", templateContent);
        }

        /// <summary>
        /// 生成ExportingClass
        /// </summary>
        /// <param name="className"></param>
        /// <param name="Primary_Key_Inside_Tag_Here">主键类型</param>
        public static void SetExportingClass(string className, List<MetaTableInfo> metaTableInfoList)
        {
            string appServiceIntercafeClassDirectory = Configuration.RootDirectory + @"\Server\ExportingClass\MainTemplate.txt";
            var templateContent = Read(appServiceIntercafeClassDirectory);
            StringBuilder excel_Header = new StringBuilder();
            StringBuilder excel_Objects = new StringBuilder();

            for (int i = 0; i < metaTableInfoList.Count; i++)
            {
                if (i == 0)
                {
                    excel_Header.AppendLine($"\"{metaTableInfoList[i].Annotation }\",");
                    excel_Objects.AppendLine($"_ => _.{metaTableInfoList[i].Name },");
                }
                else
                {
                    var comma = string.Empty;
                    if (i + 1 < metaTableInfoList.Count)
                    {
                        comma = ",";
                    }
                    //空格是为了排版 强迫症
                    excel_Header.AppendLine($"                        \"{metaTableInfoList[i].Annotation }\"" + comma);
                    excel_Objects.AppendLine($"                        _ => _.{metaTableInfoList[i].Name }" + comma);

                }
            }

            templateContent = templateContent.Replace("{{Namespace_Here}}", Configuration.Namespace_Here)
                                             .Replace("{{Namespace_Relative_Full_Here}}", className)
                                             .Replace("{{Entity_Name_Here}}", className)
                                             .Replace("{{entity_Name_Here}}", GetFirstToLowerStr(className))
                                             .Replace("{{Permission_Name_Here}}", $"Pages_Administration_{className}")
                                             .Replace("{{Excel_Header}}", excel_Header.ToString())
                                             .Replace("{{Excel_Objects}}", excel_Objects.ToString())
                                             ;
            Write(Configuration.Application_Directory + className + "s\\Exporting\\", className + "ListExcelExporter.cs", templateContent);
        }

        #region Dtos

        /// <summary>
        /// 生成GetInputClass
        /// </summary>
        /// <param name="className"></param>
        public static void SetGetInputClass(string className)
        {
            string appServiceIntercafeClassDirectory = Configuration.RootDirectory + @"\Server\Dtos\GetInputClass\MainTemplate.txt";
            var templateContent = Read(appServiceIntercafeClassDirectory);

            templateContent = templateContent.Replace("{{Namespace_Here}}", Configuration.Namespace_Here)
                                             .Replace("{{Namespace_Relative_Full_Here}}", className)
                                             .Replace("{{Entity_Name_Here}}", className)
                                             ;
            Write(Configuration.Application_Directory + className + "s\\Dtos\\", "Get" + className + "Input.cs", templateContent);
        }


        /// <summary>
        /// 生成GetForEditOutputClass
        /// </summary>
        /// <param name="className"></param>
        public static void SetGetForEditOutputClass(string className)
        {
            string appServiceIntercafeClassDirectory = Configuration.RootDirectory + @"\Server\Dtos\GetForEditOutputClass\MainTemplate.txt";
            var templateContent = Read(appServiceIntercafeClassDirectory);

            templateContent = templateContent.Replace("{{Namespace_Here}}", Configuration.Namespace_Here)
                                             .Replace("{{Namespace_Relative_Full_Here}}", className)
                                             .Replace("{{Entity_Name_Here}}", className)
                                             ;
            Write(Configuration.Application_Directory + className + "s\\Dtos\\", "Get" + className + "ForEditOutput.cs", templateContent);
        }


        /// <summary>
        /// 生成ListDtoClass
        /// </summary>
        /// <param name="className"></param>
        /// <param name="metaTableInfoList"></param>
        public static void SetListDtoClass(string className, List<MetaTableInfo> metaTableInfoList)
        {
            string appServiceIntercafeClassDirectory = Configuration.RootDirectory + @"\Server\Dtos\ListDtoClass\MainTemplate.txt";
            var templateContent = Read(appServiceIntercafeClassDirectory);
            StringBuilder sb = new StringBuilder();

            foreach (var item in metaTableInfoList)
            {
                sb.AppendLine("/// <summary>");
                sb.AppendLine("/// " + item.Annotation);
                sb.AppendLine("/// </summary>");
                sb.AppendLine("public " + item.PropertyType + " " + item.Name + " { get; set; }");
                sb.AppendLine("     ");
            }
            var property_Looped_Template_Here = sb.ToString();
            templateContent = templateContent.Replace("{{Namespace_Here}}", Configuration.Namespace_Here)
                                             .Replace("{{Namespace_Relative_Full_Here}}", className)
                                             .Replace("{{Entity_Name_Here}}", className)
                                             .Replace("{{Property_Looped_Template_Here}}", property_Looped_Template_Here)
                                             ;
            Write(Configuration.Application_Directory + className + "s\\Dtos\\", className + "ListDto.cs", templateContent);
        }


        /// <summary>
        /// 生成CreateOrEditInput
        /// </summary>
        /// <param name="className"></param>
        /// <param name="metaTableInfoList"></param>
        public static void SetCreateOrEditInputClass(string className, List<MetaTableInfo> metaTableInfoList)
        {
            string appServiceIntercafeClassDirectory = Configuration.RootDirectory + @"\Server\Dtos\CreateOrEditInputClass\MainTemplate.txt";
            var templateContent = Read(appServiceIntercafeClassDirectory);
            StringBuilder sb = new StringBuilder();

            foreach (var item in metaTableInfoList)
            {
                sb.AppendLine("/// <summary>");
                sb.AppendLine("/// " + item.Annotation);
                sb.AppendLine("/// </summary>");
                sb.AppendLine("public " + item.PropertyType + (item.Name == "Id" ? "? " : " ") + item.Name + " { get; set; }");
                sb.AppendLine("     ");
            }
            var property_Looped_Template_Here = sb.ToString();
            templateContent = templateContent.Replace("{{Namespace_Here}}", Configuration.Namespace_Here)
                                             .Replace("{{Namespace_Relative_Full_Here}}", className)
                                             .Replace("{{Entity_Name_Here}}", className)
                                             .Replace("{{Property_Looped_Template_Here}}", property_Looped_Template_Here)
                                             ;
            Write(Configuration.Application_Directory + className + "s\\Dtos\\", "CreateOrEdit" + className + "Input.cs", templateContent);
        }


        /// <summary>
        /// 生成ConstsClass
        /// </summary>
        /// <param name="className"></param>
        public static void SetConstsClass(string className)
        {
            string appServiceIntercafeClassDirectory = Configuration.RootDirectory + @"\Server\ConstsClass\MainTemplate.txt";
            var templateContent = Read(appServiceIntercafeClassDirectory);

            templateContent = templateContent.Replace("{{entity_Name_Here}}", GetFirstToLowerStr(className))
                                             .Replace("{{Entity_Name_Here}}", className)
                                             ;
            Write(Configuration.Application_Directory + className + "s\\", className + "ConstsClass.txt", templateContent);
        }

        /// <summary>
        /// 生成AppPermissions
        /// </summary>
        /// <param name="className"></param>
        public static void SetAppPermissions(string className)
        {
            StringBuilder sbAppPermissions_Here = new StringBuilder();
            sbAppPermissions_Here.AppendLine($"#region {className}");
            sbAppPermissions_Here.AppendLine($"public const string Pages_Administration_{className} = \"Pages.Administration.{className}\";");
            sbAppPermissions_Here.AppendLine($"public const string Pages_Administration_{className}_Create = \"Pages.Administration.{className}.Create\";");
            sbAppPermissions_Here.AppendLine($"public const string Pages_Administration_{className}_Edit = \"Pages.Administration.{className}.Edit\";");
            sbAppPermissions_Here.AppendLine($"public const string Pages_Administration_{className}_Delete = \"Pages.Administration.{className}.Delete\";");
            sbAppPermissions_Here.AppendLine(" #endregion");
            sbAppPermissions_Here.AppendLine("                         ");
            sbAppPermissions_Here.AppendLine(" //{{AppPermissions_Here}}");

            var appPermissionsTemplateContent = Read(Configuration.AppPermissions_Path);
            if (!appPermissionsTemplateContent.Contains($"Pages_Administration_{className}"))
            {
                appPermissionsTemplateContent = appPermissionsTemplateContent.Replace("//{{AppPermissions_Here}}", sbAppPermissions_Here.ToString());
                Write(Configuration.AppPermissions_Path, appPermissionsTemplateContent);
            }
        }

        /// <summary>
        /// 生成AppPermissions
        /// </summary>
        /// <param name="className"></param>
        public static void SetAppAuthorizationProvider(string className)
        {
            StringBuilder sbAppAuthorizationProvider_Here = new StringBuilder();
            sbAppAuthorizationProvider_Here.AppendLine($"#region {className}");
            sbAppAuthorizationProvider_Here.AppendLine($" var {className} = administration.CreateChildPermission(AppPermissions.Pages_Administration_{className}, L(\"{ className }\"));");
            sbAppAuthorizationProvider_Here.AppendLine($"{className}.CreateChildPermission(AppPermissions.Pages_Administration_{ className }_Create, L(\"CreatingNew{className}\"));");
            sbAppAuthorizationProvider_Here.AppendLine($"{className}.CreateChildPermission(AppPermissions.Pages_Administration_{className}_Edit, L(\"Editing{className}\"));");
            sbAppAuthorizationProvider_Here.AppendLine($"{className}.CreateChildPermission(AppPermissions.Pages_Administration_{className}_Delete, L(\"Deleting{className}\"));");
            sbAppAuthorizationProvider_Here.AppendLine(" #endregion");
            sbAppAuthorizationProvider_Here.AppendLine("                         ");
            sbAppAuthorizationProvider_Here.AppendLine(" //{{AppAuthorizationProvider_Here}}");

            var appAuthorizationProviderTemplateContent = Read(Configuration.AppAuthorizationProvider_Path);
            if (!appAuthorizationProviderTemplateContent.Contains($"AppPermissions.Pages_Administration_{className}"))
            {
                appAuthorizationProviderTemplateContent = appAuthorizationProviderTemplateContent.Replace("//{{AppAuthorizationProvider_Here}}", sbAppAuthorizationProvider_Here.ToString());
                Write(Configuration.AppAuthorizationProvider_Path, appAuthorizationProviderTemplateContent);
            }
        }

        /// <summary>
        /// 生成本地化语言 xml 文档
        /// </summary>
        /// <param name="className"></param>
        public static void SetZh_CN_LocalizationDictionary_Here(string className, string classAnnotation)
        {
            //zh_CN_LocalizationDictionary_Here

            StringBuilder sbzh_CN_LocalizationDictionary_Here = new StringBuilder();
            sbzh_CN_LocalizationDictionary_Here.AppendLine($"<!-- {classAnnotation}-->");
            sbzh_CN_LocalizationDictionary_Here.AppendLine($"<text name=\"{className }\">{classAnnotation}</text>");
            sbzh_CN_LocalizationDictionary_Here.AppendLine($"<text name=\"CreatingNew{ className}\">创建{classAnnotation}</text>");
            sbzh_CN_LocalizationDictionary_Here.AppendLine($"<text name=\"Editing{className}\">编辑{classAnnotation}</text>");
            sbzh_CN_LocalizationDictionary_Here.AppendLine($"<text name=\"Deleting{className}\">删除{classAnnotation}</text>");
            sbzh_CN_LocalizationDictionary_Here.AppendLine("<!--zh_CN_LocalizationDictionary_Here-->");

            var zh_CN_LocalizationDictionaryTemplateContent = Read(Configuration.Zh_CN_LocalizationDictionary_Path);
            if (!zh_CN_LocalizationDictionaryTemplateContent.Contains($"<text name=\"{className }\">"))
            {
                zh_CN_LocalizationDictionaryTemplateContent = zh_CN_LocalizationDictionaryTemplateContent.Replace("<!--zh_CN_LocalizationDictionary_Here-->", sbzh_CN_LocalizationDictionary_Here.ToString());
                Write(Configuration.Zh_CN_LocalizationDictionary_Path, zh_CN_LocalizationDictionaryTemplateContent);
            }
        }

        #endregion

        #endregion





        #region 文件读取
        public static string Read(string path)
        {
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                StringBuilder sb = new StringBuilder();

                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line.ToString());
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">文件保存路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="templateContent">模板内容</param>
        public static void Write(string filePath, string fileName, string templateContent)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            using (FileStream fs = new FileStream(filePath + fileName, FileMode.Create))
            {
                //获得字节数组
                byte[] data = Encoding.Default.GetBytes(templateContent);
                //开始写入
                fs.Write(data, 0, data.Length);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">文件保存路径</param>
        /// <param name="templateContent">模板内容</param>
        public static void Write(string filePath, string templateContent)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                //获得字节数组
                byte[] data = Encoding.Default.GetBytes(templateContent);
                //开始写入
                fs.Write(data, 0, data.Length);
            }

        }
        #endregion

        #region 首字母小写
        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetFirstToLowerStr(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Length > 1)
                {
                    return char.ToLower(str[0]) + str.Substring(1);
                }
                return char.ToLower(str[0]).ToString();
            }
            return null;
        }
        #endregion
    }
}
