// Decompiled with JetBrains decompiler
// Type: GreenCo.AutoCompleteObject
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

#nullable disable
namespace GreenCo
{
  public class AutoCompleteObject
  {
    public string DisplayMemberPath { get; set; }

    public string TextSearchPath { get; set; }

    public AutoCompleteObject(string display)
    {
      this.DisplayMemberPath = display;
      this.TextSearchPath = (string) null;
    }
  }
}
