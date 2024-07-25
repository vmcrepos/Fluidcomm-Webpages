// Decompiled with JetBrains decompiler
// Type: GreenCo.Configure
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using GreenCo.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

#nullable disable
namespace GreenCo
{
  public class Configure : Page
  {
    private string company;
    private bool IsFluidTraxAdmin;
    protected System.Web.UI.ScriptManager scriptman;
    protected Panel manageReadingTemplates;
    protected Panel pnlButtons;
    protected RadButton btnDefaultTemplateDownload;
    protected RadButton btnCurrentTemplateDownload;
    protected RadAsyncUpload uplTemplate;
    protected RadButton btnUpload;
    protected Label lblResult;
    protected Panel pnlButtons4;
    protected RadButton btnDefaultSummaryTemplateDownload;
    protected RadButton btnCurrentSummaryTemplateDownload;
    protected RadAsyncUpload uplSummaryReportTemplate;
    protected RadButton btnSummaryReportUpload;
    protected Label lblSummaryUploadResult;
    protected Panel pnlButtons2;
    protected RadButton btnDefaultAutoReportTemplateDownload;
    protected RadButton btnCurrentAutoReportTemplateDownload;
    protected RadAsyncUpload uplAutoReportTemplate;
    protected RadButton btnAutoReportUpload;
    protected Label lblAutoReportResult;
    protected Panel pnlButtons3;
    protected RadButton btnTouchScreenDefaultDownload;
    protected RadButton btnTouchScreenCurrentDownload;
    protected RadAsyncUpload uplTouchScreenTemplate;
    protected RadButton btnTouchScreenUpload;
    protected Label lblTouchScreenResult;
    protected Literal litError;
    protected GridView gridUnit;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (Roles.IsUserInRole("Admin"))
        this.IsFluidTraxAdmin = true;
      else if (Roles.IsUserInRole("CompanyAdmin"))
        this.IsFluidTraxAdmin = false;
      else
        this.ShowError("User does not have appropriate permission for this page.");
      int currentUserCompanyId = Utils.GetCurrentUserCompanyId(Membership.GetUser());
      if (currentUserCompanyId == -1)
      {
        this.Response.Redirect("~/Defualt.aspx", false);
      }
      else
      {
        this.company = Utils.GetCompanyList()[currentUserCompanyId];
        this.lblResult.Text = "";
        if (this.IsPostBack)
          return;
        if (Roles.IsUserInRole("Admin"))
        {
          DataTable newUnits = Utils.GetNewUnits();
          if (newUnits.Rows.Count > 0)
          {
            foreach (DataRow row in (InternalDataCollectionBase) newUnits.Rows)
            {
              int num1 = (int) row["UnitID"];
              string str = "NEW_VLINK_UNIT: " + Utils.CleanText(row["VLinkName"].ToString());
              string cmdText = "INSERT INTO Unit (UnitID, UnitName, UnitCategory, CompanyID, Active, IsMobile) VALUES(@unitId, @unitName, @unitCategory, @companyId, @active, @isMobile); SELECT UnitID FROM Unit WHERE UnitID =@unitId";
              using (SqlConnection conn = Utils.GetConn())
              {
                using (SqlCommand sqlCommand = new SqlCommand(cmdText, conn))
                {
                  sqlCommand.Parameters.AddWithValue("@unitId", (object) num1);
                  sqlCommand.Parameters.AddWithValue("@unitName", (object) str);
                  sqlCommand.Parameters.AddWithValue("@unitCategory", (object) "Unknown");
                  sqlCommand.Parameters.AddWithValue("@companyId", (object) 0);
                  sqlCommand.Parameters.AddWithValue("@active", (object) true);
                  sqlCommand.Parameters.AddWithValue("@isMobile", (object) false);
                  object obj = sqlCommand.ExecuteScalar();
                  if (obj == DBNull.Value || !(obj is int num2) || num2 != num1)
                  {
                    this.litError.Text = "Database error: could not save and find new unit" + num1.ToString();
                    return;
                  }
                  int num3 = (int) obj;
                }
              }
            }
          }
        }
        this.gridUnit.DataSource = (object) Utils.RefreshUnits(this.IsFluidTraxAdmin);
        this.gridUnit.DataBind();
        this.DisableCurrentCompanyTemplateIfMissing(new List<string>()
        {
          TemplatePaths.GetExistingTemplateServerPath(Templates.Reading, this.company, this.Server),
          TemplatePaths.GetExistingTemplateServerPath(Templates.AutoReport, this.company, this.Server),
          TemplatePaths.GetExistingTemplateServerPath(Templates.TouchScreenAutoReport, this.company, this.Server),
          TemplatePaths.GetExistingTemplateServerPath(Templates.ReadingSummary, this.company, this.Server)
        }, new RadButton[4]
        {
          this.btnCurrentTemplateDownload,
          this.btnCurrentAutoReportTemplateDownload,
          this.btnTouchScreenCurrentDownload,
          this.btnCurrentSummaryTemplateDownload
        });
      }
    }

    private void DisableCurrentCompanyTemplateIfMissing(
      List<string> templateServerPaths,
      RadButton[] buttons)
    {
      for (int index = 0; index < templateServerPaths.Count; ++index)
      {
        string templateServerPath = templateServerPaths[index];
        if (index < buttons.Length)
        {
          buttons[index].Enabled = File.Exists(templateServerPath);
          buttons[index].ToolTip = File.Exists(templateServerPath) ? "Download the customized template for your company" : "No template has been uploaded for your company";
        }
      }
    }

    private void ShowError(string message)
    {
      this.Response.Redirect("~/Error.aspx?message=" + message);
    }

    protected void btnConfigure_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("ConfigureUnit.aspx?unit_id=" + this.gridUnit.DataKeys[((GridViewRow) ((Control) sender).NamingContainer).RowIndex]["UnitID"].ToString());
    }

    protected void gridUnit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
        return;
      List<Sensor> sensorList = (List<Sensor>) this.gridUnit.DataKeys[e.Row.RowIndex]["Sensors"];
      string str = "";
      List<string> stringList = new List<string>();
      foreach (Sensor sensor in sensorList)
      {
        if (str != "")
          str += ", ";
        str += sensor.Name;
        if (!stringList.Contains(sensor.SensorCategory1) && sensor.Active)
          stringList.Add(sensor.SensorCategory1);
      }
      e.Row.Cells[1].Text = string.Join(", ", stringList.ToArray());
      e.Row.Cells[9].Text = str;
      object obj = this.gridUnit.DataKeys[e.Row.RowIndex]["IsMobile"];
      bool flag = obj != null && (bool) obj;
      e.Row.Cells[3].Text = flag ? "mobile" : "fixed";
    }

    protected void chkActive_CheckedChanged(object sender, EventArgs e)
    {
      int num = (int) this.gridUnit.DataKeys[((GridViewRow) ((Control) sender).NamingContainer).RowIndex]["UnitID"];
      bool flag = ((CheckBox) sender).Checked;
      string cmdText = "UPDATE Unit SET Active=@active WHERE UnitID=@unitId; SELECT UnitID FROM Unit WHERE UnitID=@unitId";
      using (SqlConnection conn = Utils.GetConn())
      {
        using (SqlCommand sqlCommand = new SqlCommand(cmdText, conn))
        {
          sqlCommand.Parameters.AddWithValue("@active", (object) flag);
          sqlCommand.Parameters.AddWithValue("@unitId", (object) num);
          object obj = sqlCommand.ExecuteScalar();
          if (obj != DBNull.Value && obj is int)
            return;
          this.litError.Text = "Database error: could not find unit with ID" + num.ToString();
        }
      }
    }

    protected void btnDefaultTemplateDownload_Click(object sender, CommandEventArgs e)
    {
      this.DefaultTemplateDownloadHandler(e);
    }

    protected void btnDefaultAutoReportTemplateDownload_Click(object sender, CommandEventArgs e)
    {
      this.DefaultTemplateDownloadHandler(e);
    }

    private void DefaultTemplateDownloadHandler(CommandEventArgs e)
    {
      if (e.CommandArgument is string commandArgument)
      {
        switch (commandArgument)
        {
          case "ReadingReport":
            string templateServerPath1 = TemplatePaths.GetExistingTemplateServerPath(Templates.Reading, (string) null, this.Server);
            FileInfo file1 = new FileInfo(templateServerPath1);
            this.TriggerDownload(TemplatePaths.GetFileNameForTemplate(Templates.Reading, (string) null, file1.Extension), templateServerPath1, file1);
            return;
          case "SummaryReport":
            string templateServerPath2 = TemplatePaths.GetExistingTemplateServerPath(Templates.ReadingSummary, (string) null, this.Server);
            FileInfo file2 = new FileInfo(templateServerPath2);
            this.TriggerDownload(TemplatePaths.GetFileNameForTemplate(Templates.ReadingSummary, (string) null, file2.Extension), templateServerPath2, file2);
            return;
          case "AutoReport":
            string templateServerPath3 = TemplatePaths.GetExistingTemplateServerPath(Templates.AutoReport, (string) null, this.Server);
            FileInfo file3 = new FileInfo(templateServerPath3);
            this.TriggerDownload(TemplatePaths.GetFileNameForTemplate(Templates.AutoReport, (string) null, file3.Extension), templateServerPath3, file3);
            return;
          case "TouchScreen":
            string templateServerPath4 = TemplatePaths.GetExistingTemplateServerPath(Templates.TouchScreenAutoReport, (string) null, this.Server);
            FileInfo file4 = new FileInfo(templateServerPath4);
            this.TriggerDownload(TemplatePaths.GetFileNameForTemplate(Templates.TouchScreenAutoReport, (string) null, file4.Extension), templateServerPath4, file4);
            return;
        }
      }
      throw new ArgumentException("No file found for default template download");
    }

    protected void btnCurrentTemplateDownload_Click(object sender, CommandEventArgs e)
    {
      this.CurrentTemplateDownloadHandler(e);
    }

    protected void btnCurrentAutoReportTemplateDownload_Click(object sender, CommandEventArgs e)
    {
      this.CurrentTemplateDownloadHandler(e);
    }

    private void CurrentTemplateDownloadHandler(CommandEventArgs e)
    {
      if (!(e.CommandArgument is string commandArgument))
        return;
      switch (commandArgument)
      {
        case "ReadingReport":
          string templateServerPath1 = TemplatePaths.GetExistingTemplateServerPath(Templates.Reading, this.company, this.Server);
          FileInfo file1 = new FileInfo(templateServerPath1);
          this.TriggerDownload(TemplatePaths.GetFileNameForTemplate(Templates.Reading, this.company, file1.Extension), templateServerPath1, file1);
          break;
        case "SummaryReport":
          string templateServerPath2 = TemplatePaths.GetExistingTemplateServerPath(Templates.ReadingSummary, this.company, this.Server);
          FileInfo file2 = new FileInfo(templateServerPath2);
          this.TriggerDownload(TemplatePaths.GetFileNameForTemplate(Templates.ReadingSummary, this.company, file2.Extension), templateServerPath2, file2);
          break;
        case "AutoReport":
          string templateServerPath3 = TemplatePaths.GetExistingTemplateServerPath(Templates.AutoReport, this.company, this.Server);
          FileInfo file3 = new FileInfo(templateServerPath3);
          this.TriggerDownload(TemplatePaths.GetFileNameForTemplate(Templates.AutoReport, this.company, file3.Extension), templateServerPath3, file3);
          break;
        case "TouchScreen":
          string templateServerPath4 = TemplatePaths.GetExistingTemplateServerPath(Templates.TouchScreenAutoReport, this.company, this.Server);
          FileInfo file4 = new FileInfo(templateServerPath4);
          this.TriggerDownload(TemplatePaths.GetFileNameForTemplate(Templates.TouchScreenAutoReport, this.company, file4.Extension), templateServerPath4, file4);
          break;
      }
    }

    protected void btnUpload_Click(object sender, CommandEventArgs e) => this.UploadClickHandler(e);

    protected void btnAutoReportUpload_Click(object sender, CommandEventArgs e)
    {
      this.UploadClickHandler(e);
    }

    private void UploadClickHandler(CommandEventArgs e)
    {
      if (!(e.CommandArgument is string commandArgument))
        return;
      switch (commandArgument)
      {
        case "ReadingReport":
          string templateServerPath1 = TemplatePaths.GetExistingTemplateServerPath(Templates.Reading, this.company, this.Server);
          if (!string.IsNullOrEmpty(templateServerPath1))
            File.Delete(templateServerPath1);
          this.uplTemplate.UploadedFiles[0].SaveAs(this.Server.MapPath(TemplatePaths.GetRelativePathForTemplate(Templates.Reading, this.company, this.uplTemplate.UploadedFiles[0].GetExtension())), true);
          this.btnCurrentTemplateDownload.Enabled = true;
          this.btnCurrentTemplateDownload.ToolTip = "Download the customized template for your company";
          this.lblResult.Text = "File uploaded successfully";
          break;
        case "SummaryReport":
          string templateServerPath2 = TemplatePaths.GetExistingTemplateServerPath(Templates.ReadingSummary, this.company, this.Server);
          if (!string.IsNullOrEmpty(templateServerPath2))
            File.Delete(templateServerPath2);
          this.uplSummaryReportTemplate.UploadedFiles[0].SaveAs(this.Server.MapPath(TemplatePaths.GetRelativePathForTemplate(Templates.ReadingSummary, this.company, this.uplSummaryReportTemplate.UploadedFiles[0].GetExtension())), true);
          this.btnCurrentSummaryTemplateDownload.Enabled = true;
          this.btnCurrentSummaryTemplateDownload.ToolTip = "Download the customized template for your company";
          this.lblSummaryUploadResult.Text = "File uploaded successfully";
          break;
        case "AutoReport":
          string templateServerPath3 = TemplatePaths.GetExistingTemplateServerPath(Templates.AutoReport, this.company, this.Server);
          if (!string.IsNullOrEmpty(templateServerPath3))
            File.Delete(templateServerPath3);
          this.uplAutoReportTemplate.UploadedFiles[0].SaveAs(this.Server.MapPath(TemplatePaths.GetRelativePathForTemplate(Templates.AutoReport, this.company, this.uplAutoReportTemplate.UploadedFiles[0].GetExtension())), true);
          this.btnCurrentAutoReportTemplateDownload.Enabled = true;
          this.btnCurrentAutoReportTemplateDownload.ToolTip = "Download your company template";
          this.lblAutoReportResult.Text = "File uploaded successfully";
          break;
        case "TouchScreen":
          string templateServerPath4 = TemplatePaths.GetExistingTemplateServerPath(Templates.TouchScreenAutoReport, this.company, this.Server);
          if (!string.IsNullOrEmpty(templateServerPath4))
            File.Delete(templateServerPath4);
          this.uplTouchScreenTemplate.UploadedFiles[0].SaveAs(this.Server.MapPath(TemplatePaths.GetRelativePathForTemplate(Templates.TouchScreenAutoReport, this.company, this.uplTouchScreenTemplate.UploadedFiles[0].GetExtension())), true);
          this.btnTouchScreenCurrentDownload.Enabled = true;
          this.btnTouchScreenCurrentDownload.ToolTip = "Download your company template";
          this.lblTouchScreenResult.Text = "File uploaded successfully";
          break;
      }
    }

    private void TriggerDownload(string fileName, string filepath, FileInfo file)
    {
      if (file.Exists)
      {
        this.Response.Clear();
        this.Response.ClearHeaders();
        this.Response.ClearContent();
        this.Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
        this.Response.AddHeader("Content-Type", "application/Excel");
        this.Response.ContentType = "application/vnd.xls";
        this.Response.AddHeader("Content-Length", file.Length.ToString());
        this.Response.TransmitFile(filepath);
        this.Response.End();
      }
      else
        this.Response.Write("This file does not exist");
    }
  }
}
