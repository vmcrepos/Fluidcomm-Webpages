// Decompiled with JetBrains decompiler
// Type: GreenCo.Sensor
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace GreenCo
{
  [Serializable]
  public class Sensor
  {
    public string Name { get; set; }

    public int SensorID { get; set; }

    public string SensorCategory1 { get; set; }

    public string SensorCategory2 { get; set; }

    public List<Channel> Channels { get; set; }

    public DateTime? LastReadingDate { get; set; }

    public List<AlarmHelper> Alarms { get; set; }

    public SensorType SType { get; set; }

    public List<string> Permissions { get; set; }

    public short? SensorIndex { get; set; }

    public bool Active { get; set; }

    public Channel AltChannel
    {
      get
      {
        foreach (Channel channel in this.Channels)
        {
          if (channel.Name.ToLower() == "alt")
            return channel;
        }
        return (Channel) null;
      }
    }

    public Channel[] ParticleCountChannels
    {
      get
      {
        Channel[] channelArray = new Channel[4];
        foreach (Channel channel in this.Channels)
        {
          if (channel.Name == "ISO4")
            channelArray[0] = channel;
          else if (channel.Name == "ISO6")
            channelArray[1] = channel;
          else if (channel.Name == "ISO14")
            channelArray[2] = channel;
          else if (channel.Name == "ISO21")
            channelArray[3] = channel;
        }
        return channelArray[0] == null || channelArray[1] == null || channelArray[2] == null || channelArray[3] == null ? (Channel[]) null : channelArray;
      }
    }

    public Channel[] PhysicalChannels
    {
      get
      {
        Channel[] channelArray = new Channel[2];
        foreach (Channel channel in this.Channels)
        {
          if (channel.Name == "RH")
            channelArray[0] = channel;
          else if (channel.Name == "Temp")
            channelArray[1] = channel;
        }
        return channelArray[0] == null || channelArray[1] == null ? (Channel[]) null : channelArray;
      }
    }

    public string Heading
    {
      get
      {
        return this.SensorCategory1 == "" || this.SensorCategory1 == "Unknown" ? this.Name : this.Name + " (" + this.SensorCategory1 + ")";
      }
    }

    public Sensor(
      string name,
      int sensorID,
      SensorType sensorType,
      string sensorCategory1,
      string sensorCategory2,
      List<string> permissions)
    {
      this.Name = name;
      this.SensorID = sensorID;
      this.Channels = new List<Channel>();
      this.Alarms = new List<AlarmHelper>();
      this.SType = sensorType;
      this.SensorCategory1 = sensorCategory1;
      this.SensorCategory2 = sensorCategory2;
      if (permissions == null)
        this.Permissions = new List<string>();
      else
        this.Permissions = permissions;
    }

    public Sensor(
      string name,
      int sensorID,
      SensorType sensorType,
      string sensorCategory1,
      string sensorCategory2,
      List<string> permissions,
      bool active,
      short? sensorIndex)
    {
      this.Name = name;
      this.SensorID = sensorID;
      this.Channels = new List<Channel>();
      this.Alarms = new List<AlarmHelper>();
      this.SType = sensorType;
      this.SensorCategory1 = sensorCategory1;
      this.SensorCategory2 = sensorCategory2;
      this.Permissions = permissions != null ? permissions : new List<string>();
      this.Active = active;
      this.SensorIndex = sensorIndex;
    }

    public void RefreshLatestReading(SqlConnection conn)
    {
      this.LastReadingDate = new DateTime?();
      using (SqlCommand sqlCommand = new SqlCommand("SELECT top 1 date, alt, meta1, meta2, meta3, meta4, iso4, iso6, iso14, iso21, RH, Temp FROM data WHERE sensorid=" + this.SensorID.ToString() + " ORDER BY Date DESC", conn))
      {
        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
        {
          if (sqlDataReader.Read())
          {
            if (this.AltChannel != null)
            {
              Decimal result;
              this.AltChannel.CurrentValue = sqlDataReader.IsDBNull(1) || !Decimal.TryParse(sqlDataReader.GetValue(1).ToString(), out result) ? new Decimal?() : new Decimal?(result);
            }
            if (this.ParticleCountChannels != null)
            {
              this.ParticleCountChannels[0].CurrentValue = sqlDataReader.IsDBNull(6) ? new Decimal?() : new Decimal?((Decimal) sqlDataReader.GetInt16(6));
              this.ParticleCountChannels[1].CurrentValue = sqlDataReader.IsDBNull(7) ? new Decimal?() : new Decimal?((Decimal) sqlDataReader.GetInt16(7));
              this.ParticleCountChannels[2].CurrentValue = sqlDataReader.IsDBNull(8) ? new Decimal?() : new Decimal?((Decimal) sqlDataReader.GetInt16(8));
              this.ParticleCountChannels[3].CurrentValue = sqlDataReader.IsDBNull(9) ? new Decimal?() : new Decimal?((Decimal) sqlDataReader.GetInt16(9));
            }
            if (this.PhysicalChannels != null)
            {
              this.PhysicalChannels[0].CurrentValue = sqlDataReader.IsDBNull(10) ? new Decimal?() : new Decimal?((Decimal) sqlDataReader.GetInt16(10));
              this.PhysicalChannels[1].CurrentValue = sqlDataReader.IsDBNull(11) ? new Decimal?() : new Decimal?((Decimal) sqlDataReader.GetInt16(11));
            }
            if (!sqlDataReader.IsDBNull(0))
              this.LastReadingDate = new DateTime?(sqlDataReader.GetDateTime(0));
          }
        }
      }
      conn.Close();
    }

    public Panel GenerateAlarm()
    {
      bool flag = false;
      foreach (AlarmHelper alarm in this.Alarms)
      {
        if (alarm.AlarmStrength)
          flag = true;
      }
      Panel alarm1 = new Panel();
      if (flag)
        alarm1.CssClass = "pnlAlarmFlareRed";
      else if (this.Alarms.Count == 0)
        alarm1.CssClass = "pnlAlarmFlareNone";
      else
        alarm1.CssClass = "pnlAlarmFlareYellow";
      alarm1.Attributes.Add("onclick", "CallServer('Alarm" + this.SensorID.ToString() + "')");
      Panel child1 = new Panel();
      child1.CssClass = "pnlAlarmBox";
      alarm1.Controls.Add((Control) child1);
      Panel child2 = new Panel();
      child2.CssClass = "pnlAlarmTitle";
      child1.Controls.Add((Control) child2);
      Label child3 = new Label();
      child3.CssClass = "lblAlarmTitle";
      child3.Text = "ALERTS";
      child2.Controls.Add((Control) child3);
      Panel child4 = new Panel();
      child4.CssClass = "pnlAlarmInteriorWrapper";
      child1.Controls.Add((Control) child4);
      Panel child5 = new Panel();
      child5.CssClass = "pnlAlarmInteriorValue";
      child4.Controls.Add((Control) child5);
      Label child6 = new Label();
      child6.CssClass = "lblAlarmInteriorValue";
      child6.Text = this.Alarms.Count.ToString();
      child5.Controls.Add((Control) child6);
      return alarm1;
    }

    public Panel GenerateFullPanel(bool center)
    {
      Panel panel = new Panel();
      Panel child1 = new Panel();
      if (center)
      {
        panel.CssClass = "pnlBoxGroupFloater";
        child1.CssClass = "pnlBoxGroupWrapperCenter";
        panel.Controls.Add((Control) child1);
      }
      else
      {
        child1 = panel;
        panel.CssClass = "pnlBoxGroupWrapper";
      }
      Panel child2 = new Panel();
      child2.CssClass = "pnlBoxGroupTitle";
      panel.Controls.Add((Control) child2);
      child2.Controls.Add((Control) new Label()
      {
        Text = this.Heading
      });
      panel.Controls.Add((Control) this.GenerateAlarm());
      return child1;
    }
  }
}
