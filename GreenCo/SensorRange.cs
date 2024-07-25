// Decompiled with JetBrains decompiler
// Type: GreenCo.SensorRange
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;

#nullable disable
namespace GreenCo
{
  [Serializable]
  public class SensorRange
  {
    public byte LimitType { get; set; }

    public Decimal LimitValue { get; set; }

    public SensorRange(byte limitType, Decimal limitValue)
    {
      this.LimitType = limitType;
      this.LimitValue = limitValue;
    }
  }
}
