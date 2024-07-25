// Decompiled with JetBrains decompiler
// Type: GreenCo.Logout
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;

#nullable disable
namespace GreenCo
{
  public class Logout : Page
  {
    protected HtmlForm form1;

    protected void Page_Load(object sender, EventArgs e)
    {
      FormsAuthentication.SignOut();
      this.Response.Redirect(FormsAuthentication.LoginUrl);
    }
  }
}
