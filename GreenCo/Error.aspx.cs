// Decompiled with JetBrains decompiler
// Type: GreenCo.Error
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace GreenCo
{
  public class Error : Page
  {
    protected Panel ContentWrapper;
    protected Literal litMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      string str = this.Request.Params["message"];
      if (string.IsNullOrEmpty(str))
        str = "There was an error processing that request. Please go back and try again. Contact admin if issue persists";
      this.litMessage.Text = str;
    }
  }
}
