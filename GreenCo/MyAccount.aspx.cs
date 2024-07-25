// Decompiled with JetBrains decompiler
// Type: GreenCo.MyAccount
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
  public class MyAccount : Page
  {
    protected System.Web.UI.ScriptManager scriptMan;
    protected Label lblTitle;
    protected Label lblPassword1;
    protected TextBox txtPassword1;
    protected RequiredFieldValidator vldPasswordRequired1;
    protected Label lblPassword2;
    protected TextBox txtPassword2;
    protected RequiredFieldValidator vldPasswordRequired2;
    protected Label lblPassword3;
    protected TextBox txtConfirmPassword;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected CompareValidator ComparePasswordValidator;
    protected Button btnSave;
    protected ValidationSummary ValidationSummary1;
    protected Literal litError;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnSavePassword_Click(object sender, EventArgs e)
    {
      MembershipUser user = Membership.GetUser();
      if (user == null)
        return;
      if (user.ChangePassword(this.txtPassword1.Text, this.txtPassword2.Text))
        this.litError.Text = "Password successfully updated";
      else
        this.litError.Text = "Password update failed. Please ensure you are using the correct current password.";
    }

    protected void btnPasswordCancel_Click(object sender, EventArgs e)
    {
    }
  }
}
