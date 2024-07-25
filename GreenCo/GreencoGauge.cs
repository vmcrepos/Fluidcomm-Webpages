// Decompiled with JetBrains decompiler
// Type: GreenCo.GreencoGauge
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.Web.UI;
using Telerik.Web.UI;

#nullable disable
namespace GreenCo
{
  public class GreencoGauge : RadRadialGauge
  {
    public string FlareCssClass
    {
      get
      {
        if (!this.Pointer.Value.HasValue)
          return "pnlGaugeNone";
        foreach (GaugeRange range in (StateManagedCollection) this.Scale.Ranges)
        {
          Decimal? nullable1 = this.Pointer.Value;
          Decimal from = range.From;
          if (nullable1.GetValueOrDefault() > from & nullable1.HasValue)
          {
            Decimal? nullable2 = this.Pointer.Value;
            Decimal to = range.To;
            if (nullable2.GetValueOrDefault() <= to & nullable2.HasValue)
            {
              if (range.Color == Utils.YellowColor)
                return "pnlGaugeYellow";
              if (range.Color == Utils.GreyColor)
                return "pnlGaugeGrey";
              if (range.Color == Utils.RedColor)
                return "pnlGaugeRed";
            }
          }
        }
        return "pnlGaugeNone";
      }
    }
  }
}
