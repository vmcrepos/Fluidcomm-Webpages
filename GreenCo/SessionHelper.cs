// Decompiled with JetBrains decompiler
// Type: GreenCo.SessionHelper
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System.Collections.Generic;

#nullable disable
namespace GreenCo
{
  public static class SessionHelper
  {
    public static Dictionary<string, int> GetReadingsFilters()
    {
      return new Dictionary<string, int>()
      {
        {
          "CompanyIndex",
          0
        },
        {
          "LocationIndex",
          0
        },
        {
          "AssetIndex",
          0
        },
        {
          "FluidTypeIndex",
          0
        },
        {
          "SensorIndex",
          0
        },
        {
          "DataTypeIndex",
          0
        }
      };
    }
  }
}
