// Decompiled with JetBrains decompiler
// Type: GreenCo.Greenco
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.Reflection;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace GreenCo
{
  public class Greenco : MasterPage
  {
    protected ContentPlaceHolder head;
    protected HtmlForm form1;
    protected Image imgHeader;
    protected HtmlAnchor linkHome;
    protected HtmlAnchor linkAutoReports;
    protected HtmlAnchor linkConfigure;
    protected HtmlAnchor linkTroubleShoot;
    protected Label lblWelcome;
    protected HtmlAnchor linkMyAccount;
    protected HtmlAnchor linkLogout;
    protected HtmlAnchor linkAdmin;
    protected ContentPlaceHolder ContentPlaceHolder1;
    protected Label lblVersion;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblVersion.Text = "Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
      if (Membership.GetUser() == null)
        this.Response.Redirect("Login.aspx");
      this.lblWelcome.Text = "Welcome " + Membership.GetUser().UserName;
      if (!Roles.IsUserInRole("Admin"))
        this.linkAdmin.Visible = false;
      if (!Roles.IsUserInRole("Admin") && !Roles.IsUserInRole("CompanyAdmin"))
        this.linkConfigure.Visible = false;
      this.linkHome.HRef = this.Page.ResolveUrl("~/Readings.aspx");
      this.linkAutoReports.HRef = this.Page.ResolveUrl("~/Reports.aspx");
      this.linkConfigure.HRef = this.Page.ResolveUrl("~/Configure.aspx");
      this.linkTroubleShoot.HRef = this.Page.ResolveUrl("~/Troubleshoot.aspx");
      this.linkAdmin.HRef = this.Page.ResolveUrl("~/Admin/Admin.aspx");
      this.linkLogout.HRef = this.Page.ResolveUrl("~/Logout.aspx");
      this.linkMyAccount.HRef = this.Page.ResolveUrl("~/MyAccount.aspx");
      string lower = this.Request.RawUrl.ToLower();
      string str = "navbar-item is-active";
      if (lower.Contains("readings.aspx"))
        this.linkHome.Attributes["class"] = str;
      else if (lower.Contains("reports.aspx"))
        this.linkAutoReports.Attributes["class"] = str;
      else if (lower.Contains("configure.aspx") || lower.Contains("configureunit.aspx"))
        this.linkConfigure.Attributes["class"] = str;
      else if (lower.Contains("troubleshoot.aspx"))
      {
        this.linkTroubleShoot.Attributes["class"] = str;
      }
      else
      {
        if (!lower.Contains("admin.aspx"))
          return;
        this.linkAdmin.Attributes["class"] = str;
      }
    }

    protected void btnSettings_Click(object sender, EventArgs e)
    {
    }
  }
}
