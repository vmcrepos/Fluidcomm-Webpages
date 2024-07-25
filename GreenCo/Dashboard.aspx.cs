// Decompiled with JetBrains decompiler
// Type: GreenCo.Dashboard
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace GreenCo
{
  public class Dashboard : Page
  {
    protected System.Web.UI.ScriptManager scriptman;
    protected Panel pnlLogo;
    protected Image imgLogo;
    protected Panel pnlCustomerTitle;
    protected Label lblCustomerTitle;
    protected Panel pnlOptionContainer;
    protected DropDownList cmbFacility;
    protected Panel pnlDemo;
    protected Panel pnlDemoInside;
    protected Panel pnlDemoTitle;
    protected Label lblDemoTitle;
    protected Panel pnlDemo1;
    protected Panel pnlDemo1T;
    protected Label lblDemo1;
    protected Panel pnlDemo1F;
    protected Panel pnlDemo1I;
    protected Image imgDemo1;
    protected Panel pnlDemo2;
    protected Panel pnlDemo2T;
    protected Label lblDemo2;
    protected Panel pnlDemo2F;
    protected Panel pnlDemo2I;
    protected Image imgDemo2;
    protected Panel pnlDemo3;
    protected Panel pnlDemo3T;
    protected Label lblDemo3;
    protected Panel pnlDemo3F;
    protected Panel pnlDemo3I;
    protected Image imgDemo3;
    protected Panel pnlDemoDate;
    protected Label lblDate;
    protected Panel pnlTest;
    protected Panel pnlTest2;
    protected Panel pnlTest2A;
    protected Panel pnlTest3;

    protected void Page_Load(object sender, EventArgs e)
    {
      GreencoGauge gauge1 = Utils.GetGauge(false);
      gauge1.Pointer.Value = new Decimal?(2.5M);
      GreencoGauge gauge2 = Utils.GetGauge(false);
      gauge2.Pointer.Value = new Decimal?(3.1M);
      GreencoGauge gauge3 = Utils.GetGauge(false);
      gauge3.Pointer.Value = new Decimal?(6.8M);
      this.pnlTest.Controls.Add((Control) Utils.GetDemoGroupPanel("Current Leachate Levels", gauge1, gauge2, gauge3, "Cell 1 ", "Cell 2", "Cell 3", DateTime.Now.AddSeconds(-31.0), false, "Last reading 4 minutes ago"));
      GreencoGauge gauge2_1 = Utils.GetGauge2(true);
      gauge2_1.Pointer.Value = new Decimal?(1562.0M);
      GreencoGauge gauge2_2 = Utils.GetGauge2(true);
      gauge2_2.Pointer.Value = new Decimal?(7255.0M);
      GreencoGauge gauge2_3 = Utils.GetGauge2(true);
      gauge2_3.Pointer.Value = new Decimal?(3059.0M);
      GreencoGauge gauge3_1 = Utils.GetGauge3(true);
      gauge3_1.Pointer.Value = new Decimal?(1562.0M);
      GreencoGauge gauge3_2 = Utils.GetGauge3(true);
      gauge3_2.Pointer.Value = new Decimal?(856.0M);
      GreencoGauge gauge3_3 = Utils.GetGauge3(true);
      gauge3_3.Pointer.Value = new Decimal?(84.0M);
      this.pnlTest2A.Controls.Add((Control) Utils.GetDemoGroupPanel("Total Hours", gauge2_1, gauge2_2, gauge2_3, "Cell 1", "Cell 2", "Cell 3", DateTime.Now.AddMinutes(-16.0), true, ""));
      this.pnlTest2A.Controls.Add((Control) Utils.GetDemoGroupPanel("Hours Since Last Service", gauge3_1, gauge3_2, gauge3_3, "Cell 1", "Cell 2", "Cell 3", DateTime.Now.AddDays(-3.0), true, ""));
      List<SensorRange> ranges = new List<SensorRange>();
      ranges.Add(new SensorRange((byte) 10, 19.0M));
      ranges.Add(new SensorRange((byte) 11, 16.0M));
      ranges.Add(new SensorRange((byte) 2, 6.0M));
      ranges.Add(new SensorRange((byte) 3, 16.0M));
      this.pnlTest3.Controls.Add((Control) new Sensor("ISO PARTICLE READINGS", 0, SensorType.ParticleCount, "No Asset", "No Product", (List<string>) null)
      {
        Channels = {
          new Channel("ISO-4", 0, (short) 0, 0.0M, 24.0M, ranges)
          {
            CurrentValue = new Decimal?(120M)
          },
          new Channel("ISO-6", 0, (short) 0, 0.0M, 24.0M, ranges)
          {
            CurrentValue = new Decimal?(200M)
          },
          new Channel("ISO-14", 0, (short) 0, 0.0M, 24.0M, ranges)
          {
            CurrentValue = new Decimal?(50M)
          }
        },
        LastReadingDate = new DateTime?(DateTime.Now)
      }.GenerateFullPanel(true));
    }
  }
}
