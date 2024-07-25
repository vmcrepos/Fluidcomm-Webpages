// Decompiled with JetBrains decompiler
// Type: GreenCo.Admin.Email
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace GreenCo.Admin
{
  public class Email : Page
  {
    private MembershipUser user;
    protected Label lblTitle;
    protected Label lblName;
    protected Label lblUserName;
    protected Label lblEmail;
    protected TextBox txtEmail;
    protected Button btnUserOK;
    protected Button btnUserCancel;
    protected Label lblError;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Session["username"] == null)
      {
        this.Error();
      }
      else
      {
        try
        {
          this.user = Membership.GetAllUsers()[this.Session["username"].ToString()];
          if (this.IsPostBack)
            return;
          this.lblUserName.Text = this.user.UserName;
          this.txtEmail.Text = this.user.Email;
        }
        catch (Exception ex)
        {
          this.Error();
        }
      }
    }

    private void Error()
    {
      this.txtEmail.Enabled = false;
      this.btnUserOK.Enabled = false;
      this.lblError.Text = "The specified user could not be found";
    }

    protected void btnUserCancel_Click(object sender, EventArgs e)
    {
    }

    protected void btnUserOK_Click(object sender, EventArgs e)
    {
      string str = "";
      if (this.txtEmail.Text.IndexOf('@') < 1)
        str = "User not added! Invalid e-mail address. Must include text before and after @ character.";
      if (str != "")
      {
        this.lblError.Text = str;
      }
      else
      {
        MembershipUser user = this.user;
      }
    }
  }
}
