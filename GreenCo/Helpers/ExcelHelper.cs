// Decompiled with JetBrains decompiler
// Type: GreenCo.ExcelHelper
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using NPOI.SS.UserModel;
using System;

#nullable disable
namespace GreenCo
{
  public static class ExcelHelper
  {
    public static void SetCellValue(
      ICell cell,
      object v,
      IDataFormat dataFormatCustom,
      string dateformat)
    {
      switch (v)
      {
        case DateTime dateTime:
          cell.SetCellValue(dateTime);
          cell.CellStyle.DataFormat = dataFormatCustom.GetFormat(dateformat);
          break;
        case int num1:
          cell.SetCellValue((double) num1);
          break;
        case double num2:
          cell.SetCellValue(num2);
          break;
        case string _:
          cell.SetCellValue((string) v);
          break;
      }
    }
  }
}
