// Decompiled with JetBrains decompiler
// Type: GreenCo.UpdateData
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace GreenCo
{
  public class UpdateData : Page
  {
    protected System.Web.UI.ScriptManager scriptMan;
    protected TextBox txtDate1;
    protected TextBox txtDate2;
    protected Button btnProcess;
    protected Label lblResult;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      DateTime result1;
      DateTime result2;
      if (!DateTime.TryParse(this.txtDate1.Text, out result1) || !DateTime.TryParse(this.txtDate2.Text, out result2))
        return;
      result2 = result2.AddDays(1.0);
      int num = Utils.UpdateAllReadings(result1, result2, true, false);
      this.lblResult.Visible = true;
      this.lblResult.Text = num.ToString() + " new records were found and added";
    }

    protected void btnConstant_Click(object sender, EventArgs e) => Global.VLinkConstantUpdate();

    protected void btnDaily_Click(object sender, EventArgs e) => Global.VLinkDailyUpdate();
  }
}
