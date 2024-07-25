// Decompiled with JetBrains decompiler
// Type: GreenCo.AlarmHelper
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;

#nullable disable
namespace GreenCo
{
  [Serializable]
  public class AlarmHelper
  {
    public bool AlarmStrength { get; set; }

    public string AlarmText { get; set; }

    public DateTime Date { get; set; }

    public int Config { get; set; }

    public AlarmHelper(int config, string alarmText, DateTime date, bool alarmStrength)
    {
      this.AlarmStrength = alarmStrength;
      this.AlarmText = alarmText;
      this.Date = date;
      this.Config = config;
    }
  }
}
