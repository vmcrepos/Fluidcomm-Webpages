// Decompiled with JetBrains decompiler
// Type: GreenCo.ColorRange
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.Drawing;
using Telerik.Web.UI;

#nullable disable
namespace GreenCo
{
  [Serializable]
  public class ColorRange : GaugeRange
  {
    public ColorRange(Decimal start, Decimal end, Color shading)
    {
      this.Color = shading;
      this.From = start;
      this.To = end;
    }
  }
}
