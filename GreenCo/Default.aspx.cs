// Decompiled with JetBrains decompiler
// Type: GreenCo.Default
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace GreenCo
{
  public class Default : Page
  {
    protected System.Web.UI.ScriptManager scriptMan;
    protected Panel pnlPlaceholder;

    private DataTable CurrentUnits
    {
      get
      {
        if (this.Session[nameof (CurrentUnits)] == null)
          this.Session[nameof (CurrentUnits)] = (object) Utils.RefreshUnits(false);
        return (DataTable) this.Session[nameof (CurrentUnits)];
      }
      set => this.Session[nameof (CurrentUnits)] = (object) value;
    }

    protected void Page_Load(object sender, EventArgs e) => this.Response.Redirect("Readings.aspx");

    private void UpdateView()
    {
      this.pnlPlaceholder.Controls.Clear();
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("SensorCount", typeof (int));
      dataTable.Columns.Add("LatestReading", typeof (DateTime));
      dataTable.Columns.Add("ISO-4", typeof (Channel));
      dataTable.Columns.Add("ISO-6", typeof (Channel));
      dataTable.Columns.Add("ISO-14", typeof (Channel));
      dataTable.Columns.Add("ISO-21", typeof (Channel));
      foreach (DataRow row1 in (InternalDataCollectionBase) this.CurrentUnits.Rows)
      {
        DataRow dataRow = (DataRow) null;
        foreach (DataRow row2 in (InternalDataCollectionBase) dataTable.Rows)
        {
          foreach (Sensor sensor in (List<Sensor>) row2["Sensors"])
          {
            if (sensor.SensorCategory1 == (string) row1["AssetName"])
              dataRow = row2;
          }
        }
        if (dataRow == null)
        {
          DataRow row3 = dataTable.NewRow();
          row3["SensorCount"] = (object) 0;
          row3["ISO-4"] = (object) new Channel("ISO-4", -1, (short) -1, Decimal.MaxValue, Decimal.MinValue);
          row3["ISO-6"] = (object) new Channel("ISO-6", -1, (short) -1, Decimal.MaxValue, Decimal.MinValue);
          row3["ISO-14"] = (object) new Channel("ISO-14", -1, (short) -1, Decimal.MaxValue, Decimal.MinValue);
          row3["ISO-21"] = (object) new Channel("ISO-21", -1, (short) -1, Decimal.MaxValue, Decimal.MinValue);
          row3["LatestReading"] = (object) DateTime.MinValue;
          dataTable.Rows.Add(row3);
        }
      }
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        GreencoGauge[] greencoGaugeArray = new GreencoGauge[3];
        string[] strArray = new string[3]
        {
          "ISO-4",
          "ISO-6",
          "ISO-14"
        };
        greencoGaugeArray[0] = ((Channel) row["ISO-4"]).GetGauge(false);
        greencoGaugeArray[1] = ((Channel) row["ISO-6"]).GetGauge(false);
        greencoGaugeArray[2] = ((Channel) row["ISO-14"]).GetGauge(false);
        DateTime lastreading = (DateTime) row["LatestReading"];
        this.pnlPlaceholder.Controls.Add((Control) Utils.GetGroupPanel((string) row["AssetName"], greencoGaugeArray[0], greencoGaugeArray[1], greencoGaugeArray[2], strArray[0], strArray[1], strArray[2], lastreading, false));
      }
    }

    private void Helper(
      DataRow asset,
      List<Sensor> sensors,
      DataTable recentReading,
      string columnName)
    {
      Channel channel1 = (Channel) asset[columnName];
      foreach (DataRow row in (InternalDataCollectionBase) recentReading.Rows)
      {
        if (recentReading.Columns.Contains(columnName) && row[columnName] != DBNull.Value && row[columnName] != null && row[columnName] is Decimal)
        {
          Decimal? currentValue = channel1.CurrentValue;
          if (!currentValue.HasValue)
          {
            channel1.CurrentValue = new Decimal?((Decimal) row[columnName]);
          }
          else
          {
            Channel channel2 = channel1;
            Decimal val1 = (Decimal) row[columnName];
            currentValue = channel1.CurrentValue;
            Decimal val2 = currentValue.Value;
            Decimal? nullable = new Decimal?(Math.Max(val1, val2));
            channel2.CurrentValue = nullable;
          }
        }
      }
      foreach (Sensor sensor in sensors)
      {
        foreach (Channel channel3 in sensor.Channels)
        {
          if (!(channel3.Name != columnName))
          {
            channel1.Min = Math.Min(channel1.Min, channel3.Min);
            channel1.Max = Math.Max(channel1.Max, channel3.Max);
            foreach (SensorRange range1 in channel3.Ranges)
            {
              for (int index = 0; index < 10; ++index)
              {
                bool flag = false;
                foreach (SensorRange range2 in channel1.Ranges)
                {
                  if ((int) range2.LimitType == (int) range1.LimitType)
                  {
                    flag = true;
                    range2.LimitValue = Math.Min(range2.LimitValue, range1.LimitValue);
                  }
                }
                if (!flag)
                  channel1.Ranges.Add(new SensorRange(range1.LimitType, range1.LimitValue));
              }
              for (int index = 10; index < 20; ++index)
              {
                bool flag = false;
                foreach (SensorRange range3 in channel1.Ranges)
                {
                  if ((int) range3.LimitType == (int) range1.LimitType)
                  {
                    flag = true;
                    range3.LimitValue = Math.Max(range3.LimitValue, range1.LimitValue);
                  }
                }
                if (!flag)
                  channel1.Ranges.Add(new SensorRange(range1.LimitType, range1.LimitValue));
              }
            }
          }
        }
      }
    }
  }
}
