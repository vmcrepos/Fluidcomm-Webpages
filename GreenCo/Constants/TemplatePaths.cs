// Decompiled with JetBrains decompiler
// Type: GreenCo.Constants.TemplatePaths
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.IO;
using System.Web;

#nullable disable
namespace GreenCo.Constants
{
  public static class TemplatePaths
  {
    private static string[] allowedExtensions = new string[2]
    {
      ".xls",
      ".xlsx"
    };
    private const string TemplatesFolderRelativePath = "~/Templates";
    private const string ReadingFileName = "ReadingReportTemplate";
    private const string ReadingSummaryFileName = "SummaryReportTemplate";
    private const string AutoReportFileName = "AutoReportTemplate";
    private const string TouchScreenFileName = "AutoReportTouchScreenTemplate";
    public const string ReadingTemplate = "~/Templates/ReadingReportTemplate.xls";
    public const string SummaryReadingTemplate = "~/Templates/SummaryReportTemplate.xls";
    public const string AutoReportTemplate = "~/Templates/AutoReportTemplate.xls";
    public const string AutoReportTouchScreenTemplate = "~/Templates/AutoReportTouchScreenTemplate.xls";

    public static string GetExistingTemplateServerPath(
      Templates template,
      string company,
      HttpServerUtility server)
    {
      string str1;
      switch (template)
      {
        case Templates.Reading:
          str1 = "ReadingReportTemplate";
          break;
        case Templates.ReadingSummary:
          str1 = "SummaryReportTemplate";
          break;
        case Templates.AutoReport:
          str1 = "AutoReportTemplate";
          break;
        case Templates.TouchScreenAutoReport:
          str1 = "AutoReportTouchScreenTemplate";
          break;
        default:
          throw new ArgumentException("No template name found");
      }
      foreach (string allowedExtension in TemplatePaths.allowedExtensions)
      {
        string str2 = "~/Templates/" + str1;
        if (!string.IsNullOrEmpty(company))
          str2 = str2 + "_" + company;
        string path1 = str2 + allowedExtension;
        string path2 = server.MapPath(path1);
        if (File.Exists(path2))
          return path2;
      }
      return (string) null;
    }

    public static string GetRelativePathForTemplate(
      Templates template,
      string company,
      string fileExtension)
    {
      string str1;
      switch (template)
      {
        case Templates.Reading:
          str1 = "ReadingReportTemplate";
          break;
        case Templates.ReadingSummary:
          str1 = "SummaryReportTemplate";
          break;
        case Templates.AutoReport:
          str1 = "AutoReportTemplate";
          break;
        case Templates.TouchScreenAutoReport:
          str1 = "AutoReportTouchScreenTemplate";
          break;
        default:
          throw new ArgumentException("Template or file path not found for given template:" + template.ToString());
      }
      string str2 = "~/Templates/" + str1;
      if (!string.IsNullOrEmpty(company))
        str2 = str2 + "_" + company;
      return str2 + fileExtension;
    }

    public static string GetFileNameForTemplate(
      Templates template,
      string company,
      string fileExtension)
    {
      string str1;
      switch (template)
      {
        case Templates.Reading:
          str1 = "ReadingReportTemplate";
          break;
        case Templates.ReadingSummary:
          str1 = "SummaryReportTemplate";
          break;
        case Templates.AutoReport:
          str1 = "AutoReportTemplate";
          break;
        case Templates.TouchScreenAutoReport:
          str1 = "AutoReportTouchScreenTemplate";
          break;
        default:
          throw new ArgumentException("No template found for given template type");
      }
      string str2 = str1;
      if (!string.IsNullOrEmpty(company))
        str2 = str2 + "_" + company;
      return str2 + fileExtension;
    }

    public static string ReadingTemplateForCompany(string company)
    {
      return "~/Templates/ReadingReportTemplate_" + company + ".xls";
    }

    public static string SummaryReadingTemplateForCompany(string company)
    {
      return "~/Templates/SummaryReportTemplate_" + company + ".xls";
    }

    public static string AutoReportTemplateForCompany(string company)
    {
      return "~/Templates/AutoReportTemplate_" + company + ".xls";
    }

    public static string AutoReportTouchScreenTemplateForCompany(string company)
    {
      return "~/Templates/AutoReportTouchScreenTemplate_" + company + ".xls";
    }

    public static string GetTouchScreenTemplatePathAndNameOnlyForCompany(string company)
    {
      return "~/Templates/AutoReportTouchScreenTemplate_" + company;
    }

    public static string GetExistingTouchScreenTemplateForCompanyWithExtension(
      HttpServerUtility server,
      string company)
    {
      string companyWithExtension = (string) null;
      foreach (string allowedExtension in TemplatePaths.allowedExtensions)
      {
        string path = server.MapPath(TemplatePaths.GetTouchScreenTemplatePathAndNameOnlyForCompany(company) + allowedExtension);
        if (File.Exists(path))
        {
          companyWithExtension = path;
          break;
        }
      }
      return companyWithExtension;
    }

    public static string[] GetAllTemplatesFilePaths(HttpServerUtility server)
    {
      return Directory.GetFiles(server.MapPath("~/Templates"));
    }
  }
}
