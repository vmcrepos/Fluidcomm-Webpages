// Decompiled with JetBrains decompiler
// Type: GreenCo.FormTest
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

#nullable disable
namespace GreenCo
{
  public class FormTest : Page
  {
    private Random rnd = new Random();
    protected System.Web.UI.ScriptManager scriptMan;
    protected Panel pnlFormWrapper;
    protected Panel pnlFormFloater;
    protected RadTextBox txtTest;
    protected RadDropDownList cmbTest;
    protected RadButton btnTest;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.IsPostBack)
        return;
      this.cmbTest.Items.Clear();
      this.cmbTest.Items.Add(new DropDownListItem("Blue", "b"));
      this.cmbTest.Items.Add(new DropDownListItem("Green", "g"));
      this.cmbTest.SelectedIndex = -1;
      this.cmbTest.DefaultMessage = "Select a color";
      this.txtTest.EmptyMessage = "Enter your name";
    }

    protected void btnTest_Click(object sender, EventArgs e)
    {
      this.btnTest.BackColor = Color.FromArgb(this.rnd.Next(256), this.rnd.Next(256), this.rnd.Next(256));
      Color backColor = this.btnTest.BackColor;
      int b = (int) backColor.B;
      backColor = this.btnTest.BackColor;
      int r = (int) backColor.R;
      int num = b + r;
      backColor = this.btnTest.BackColor;
      int g = (int) backColor.G;
      if (num + g > 500)
        this.btnTest.ForeColor = Color.Black;
      else
        this.btnTest.ForeColor = Color.White;
    }

    protected void cmbTest_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.cmbTest.SelectedItem.Value == "b")
        this.txtTest.ForeColor = Color.Blue;
      if (!(this.cmbTest.SelectedItem.Value == "g"))
        return;
      this.txtTest.ForeColor = Color.Green;
    }
  }
}
