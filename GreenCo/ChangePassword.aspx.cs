// Decompiled with JetBrains decompiler
// Type: GreenCo.ChangePassword
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace GreenCo
{
  public class ChangePassword : Page
  {
    protected Label lblTitle;
    protected Label lblEmail;
    protected TextBox txtEmail;
    protected Label lblName;
    protected TextBox txtExisting;
    protected RequiredFieldValidator vldNameRequired;
    protected Label lblPassword1;
    protected TextBox txtPassword1;
    protected Label lblPassword2;
    protected TextBox txtPassword2;
    protected RequiredFieldValidator vldPasswordRequired1;
    protected RequiredFieldValidator vldPasswordRequired2;
    protected CompareValidator vldPasswordCompare;
    protected Label lblCompany;
    protected Button btnUserOK;
    protected Button btnUserCancel;
    protected ValidationSummary ValidationSummary1;
    protected Label lblError;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (Membership.GetUser() == null)
        this.Response.Redirect("Login.aspx", true);
      if (this.IsPostBack)
        return;
      this.txtEmail.Text = Membership.GetUser().Email;
    }

    protected void btnUserCancel_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("Readings.aspx", true);
    }

    protected void btnUserOK_Click(object sender, EventArgs e)
    {
      if (Membership.GetUser() == null)
        this.Response.Redirect("Login.aspx", true);
      MembershipUser user = Membership.GetUser();
      try
      {
        if (Utils.CleanText(this.txtEmail.Text).Length > 6)
          user.Email = Utils.CleanText(this.txtEmail.Text);
        if (user.ChangePassword(Utils.CleanText(this.txtExisting.Text), Utils.CleanText(this.txtPassword1.Text)))
        {
          this.txtEmail.Enabled = false;
          this.txtExisting.Enabled = false;
          this.txtPassword1.Enabled = false;
          this.txtPassword2.Enabled = false;
          this.btnUserOK.Enabled = false;
          this.lblError.Text = "Your password has been changed";
          this.btnUserCancel.Text = "Return";
        }
        else
          this.lblError.Text = "The system rejected the change";
      }
      catch (Exception ex)
      {
        this.lblEmail.Text = ex.Message;
      }
    }
  }
}
