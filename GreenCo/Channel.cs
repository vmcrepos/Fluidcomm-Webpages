// Decompiled with JetBrains decompiler
// Type: GreenCo.Channel
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
  [Serializable]
  public class Channel
  {
    public string Name { get; set; }

    public int ChannelID { get; set; }

    public Decimal Min { get; set; }

    public Decimal Max { get; set; }

    public short ChannelIndex { get; set; }

    public List<SensorRange> Ranges { get; set; }

    public DateTime? LastReadingDate { get; set; }

    public Decimal? CurrentValue { get; set; }

    public Channel(string name, int channelID, short channelIndex, Decimal min, Decimal max)
    {
      this.Name = name;
      this.ChannelID = channelID;
      this.ChannelIndex = channelIndex;
      this.Min = min;
      this.Max = max;
      this.Ranges = new List<SensorRange>();
      this.LastReadingDate = new DateTime?();
      this.CurrentValue = new Decimal?();
    }

    public Channel(
      string name,
      int channelID,
      short channelIndex,
      Decimal min,
      Decimal max,
      List<SensorRange> ranges)
    {
      this.Name = name;
      this.ChannelID = channelID;
      this.ChannelIndex = channelIndex;
      this.Min = min;
      this.Max = max;
      this.LastReadingDate = new DateTime?();
      this.Ranges = ranges;
    }

    public GreencoGauge GetGauge(bool mini)
    {
      GreencoGauge gauge = Utils.GetGauge(mini, this.Min, this.Max, this.GetRanges());
      if (this.CurrentValue.HasValue)
        gauge.Pointer.Value = this.CurrentValue;
      return gauge;
    }

    public DateTime convertDate(int pseudoDate) => DateTime.Now.AddHours(-2.5);

    public Panel GeneratePanel(
      int SensorID,
      int ReadingTypeIndex,
      Decimal conversionFactor,
      string precision)
    {
      return this.GeneratePanel(SensorID, ReadingTypeIndex, conversionFactor, precision, false);
    }

    public Panel GeneratePanel(
      int SensorID,
      int ReadingTypeIndex,
      Decimal conversionFactor,
      string precision,
      bool includeSubtitle)
    {
      Panel panel = new Panel();
      panel.CssClass = "pnlBoxWrapper";
      panel.Attributes.Add("onclick", "CallServer('Readings" + ReadingTypeIndex.ToString() + "_" + SensorID.ToString() + "')");
      Panel child1 = new Panel();
      child1.CssClass = "pnlBoxInteriorTitle";
      panel.Controls.Add((Control) child1);
      Label child2 = new Label();
      child2.CssClass = "lblBoxInteriorTitle";
      if (includeSubtitle)
        child2.Text = this.Name;
      child1.Controls.Add((Control) child2);
      Panel child3 = new Panel();
      child3.CssClass = this.FlareCssClass;
      panel.Controls.Add((Control) child3);
      Panel child4 = new Panel();
      child4.CssClass = "pnlBoxInteriorValue";
      child3.Controls.Add((Control) child4);
      string str = "";
      Decimal? currentValue = this.CurrentValue;
      if (currentValue.HasValue && conversionFactor <= 0.0M)
      {
        currentValue = this.CurrentValue;
        str = Utils.GetIsoCodeDisplayString((object) currentValue.Value);
      }
      else
      {
        currentValue = this.CurrentValue;
        if (currentValue.HasValue)
        {
          currentValue = this.CurrentValue;
          str = (currentValue.Value * conversionFactor).ToString(precision);
        }
      }
      Label child5 = new Label();
      child5.CssClass = "lblBoxInteriorValue";
      if (str.Length > 2)
        child5.CssClass = "lblBoxInteriorValueSmallText";
      if (str.Length > 4)
        child5.CssClass = "lblBoxInteriorValueTinyText";
      child5.Text = str;
      child4.Controls.Add((Control) child5);
      return panel;
    }

    public string FlareCssClass
    {
      get
      {
        if (!this.CurrentValue.HasValue)
          return "pnlBoxNone";
        int isoCode = Utils.GetIsoCode((object) this.CurrentValue.Value);
        foreach (ColorRange range in this.GetRanges())
        {
          if ((Decimal) isoCode > range.From && (Decimal) isoCode <= range.To)
          {
            if (range.Color == Utils.YellowColor)
              return "pnlBoxYellow";
            if (range.Color == Utils.GreyColor)
              return "pnlBoxGrey";
            if (range.Color == Utils.RedColor)
              return "pnlBoxRed";
            if (range.Color == Utils.GreenColor)
              return "pnlBoxGreen";
          }
        }
        return "pnlBoxNone";
      }
    }

    public List<ColorRange> GetRanges()
    {
      List<ColorRange> ranges = new List<ColorRange>();
      Decimal start = this.Min;
      Decimal end = this.Max;
      foreach (SensorRange range in this.Ranges)
      {
        if (range.LimitType == (byte) 0 && !(range.LimitValue <= start))
        {
          if (range.LimitValue > end)
            range.LimitValue = end;
          ranges.Add(new ColorRange(start, range.LimitValue, Utils.RedColor));
          start = range.LimitValue;
          break;
        }
      }
      foreach (SensorRange range in this.Ranges)
      {
        if (range.LimitType == (byte) 10 && !(range.LimitValue >= end))
        {
          if (range.LimitValue < start)
            range.LimitValue = start;
          ranges.Add(new ColorRange(range.LimitValue, end, Utils.RedColor));
          end = range.LimitValue;
          break;
        }
      }
      foreach (SensorRange range in this.Ranges)
      {
        if (range.LimitType == (byte) 1 && !(range.LimitValue <= start))
        {
          if (range.LimitValue > end)
            range.LimitValue = end;
          ranges.Add(new ColorRange(start, range.LimitValue, Utils.YellowColor));
          start = range.LimitValue;
          break;
        }
      }
      foreach (SensorRange range in this.Ranges)
      {
        if (range.LimitType == (byte) 11 && !(range.LimitValue >= end))
        {
          if (range.LimitValue < start)
            range.LimitValue = start;
          ranges.Add(new ColorRange(range.LimitValue, end, Utils.YellowColor));
          end = range.LimitValue;
          break;
        }
      }
      foreach (SensorRange range in this.Ranges)
      {
        if (range.LimitType == (byte) 2 && !(range.LimitValue <= start))
        {
          if (range.LimitValue > end)
            range.LimitValue = end;
          ranges.Add(new ColorRange(start, range.LimitValue, Utils.GreyColor));
          start = range.LimitValue;
          break;
        }
      }
      foreach (SensorRange range in this.Ranges)
      {
        if (range.LimitType == (byte) 12 && !(range.LimitValue >= end))
        {
          if (range.LimitValue < start)
            range.LimitValue = start;
          ranges.Add(new ColorRange(range.LimitValue, end, Utils.GreyColor));
          end = range.LimitValue;
          break;
        }
      }
      foreach (SensorRange range in this.Ranges)
      {
        if (range.LimitType == (byte) 3 && !(range.LimitValue <= start))
        {
          if (range.LimitValue > end)
            range.LimitValue = end;
          ranges.Add(new ColorRange(start, range.LimitValue, Utils.GreenColor));
          start = range.LimitValue;
          break;
        }
      }
      foreach (SensorRange range in this.Ranges)
      {
        if (range.LimitType == (byte) 13 && !(range.LimitValue >= end))
        {
          if (range.LimitValue < start)
            range.LimitValue = start;
          ranges.Add(new ColorRange(range.LimitValue, end, Utils.GreenColor));
          Decimal limitValue = range.LimitValue;
          break;
        }
      }
      return ranges;
    }
  }
}
