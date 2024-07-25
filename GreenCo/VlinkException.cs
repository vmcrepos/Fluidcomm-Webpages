// Decompiled with JetBrains decompiler
// Type: GreenCo.VlinkException
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;

#nullable disable
namespace GreenCo
{
  public class VlinkException : Exception
  {
    public VlinkException()
    {
    }

    public VlinkException(string message)
      : base(message)
    {
    }

    public VlinkException(string message, Exception inner)
      : base(message, inner)
    {
    }
  }
}
