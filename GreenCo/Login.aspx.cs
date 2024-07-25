// Decompiled with JetBrains decompiler
// Type: GreenCo.Login
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace GreenCo
{
  public class Login : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager scriptMan;
    protected HiddenField hidDateOffset;
    protected Panel pnlHeader;
    protected Panel pnlHeaderSub;
    protected Image imgLogo;
    protected System.Web.UI.WebControls.Login login;
    protected Label lblLoginError;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.PopulateMenu();
      this.login.Focus();
      int result;
      if (!this.IsPostBack || !int.TryParse(this.hidDateOffset.Value, out result))
        return;
      this.Session["TimeOffset"] = (object) (result * -1);
    }

    private void PopulateMenu()
    {
      this.login.UserNameLabelText = "Username";
      this.login.PasswordLabelText = "Password";
      this.login.TitleText = "Enter your FluidTrax login and password";
      this.login.PasswordRequiredErrorMessage = "A password is required";
      this.login.UserNameRequiredErrorMessage = "A username is required";
    }

    protected void login_OnLoggedIn(object sender, EventArgs e)
    {
    }

    protected void login_OnLoggingIn(object sender, EventArgs e)
    {
    }
  }
}
