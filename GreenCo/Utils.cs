// Decompiled with JetBrains decompiler
// Type: GreenCo.Utils
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using GreenCo.Constants;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

#nullable disable
namespace GreenCo
{
  public static class Utils
  {
    public static Color RedColor = Color.Red;
    public static Color YellowColor = Color.Yellow;
    public static Color GreenColor = Color.Green;
    public static Color GreyColor = Color.FromArgb(180, 180, 180);
    public static int GaugeDimension = 228;
    public static int GaugeDimensionMini = 114;
    public static string[] IsoLabels = new string[6]
    {
      "ISO4",
      "ISO6",
      "ISO14",
      "ISO21",
      "RH",
      "Temp"
    };
    public static string MasterDateString = "yyyy-MMM-dd";
    public static string MasterTimeString = "HH:mm:ss";
    public static string MasterDateTimeQueryString = "yyyy-MMM-dd HH:mm:ss.fffffff";
    public static string MasterDateTimeDisplayString = "yyy-MMM-dd HH:mm:ss";

    public static string AppUserName
    {
      get
      {
        return ConfigurationManager.AppSettings["appusername"] != null ? ConfigurationManager.AppSettings["appusername"].ToString() : "";
      }
    }

    public static string AppPassword
    {
      get
      {
        return ConfigurationManager.AppSettings["apppassword"] != null ? ConfigurationManager.AppSettings["apppassword"].ToString() : "";
      }
    }

    public static SqlConnection GetConn()
    {
      SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
      conn.Open();
      return conn;
    }

    public static string MakeErrorList(List<string> errors, string errorPrefix)
    {
      if (errors.Count == 0)
        return "";
      string str = errorPrefix + ":<ul>";
      foreach (string error in errors)
        str = str + "<li>" + error + "</li>";
      return str + "</ul>";
    }

    public static string CleanText(string text)
    {
      string str = text;
      int num = -1;
      for (int index = 0; num != index; index = str.Length)
      {
        num = str.Length;
        str = str.Replace("'", "").Replace("\"", "").Replace("--", "").Replace(";", "").Replace("/*", "").Replace("*/", "").Replace("xp_", "");
      }
      return str;
    }

    public static void Log(string message)
    {
      string cmdText = "INSERT INTO Log (comment, date) VALUES (@comment, @date)";
      using (SqlConnection conn = Utils.GetConn())
      {
        using (SqlCommand sqlCommand = new SqlCommand(cmdText, conn))
        {
          sqlCommand.Parameters.AddWithValue("@comment", (object) message);
          sqlCommand.Parameters.AddWithValue("@date", (object) DateTime.Now);
          sqlCommand.ExecuteNonQuery();
        }
      }
    }

    public static GreencoGauge GetGauge(bool mini)
    {
      return Utils.GetGauge(mini, 0.0M, 10.0M, new List<ColorRange>()
      {
        new ColorRange(9.0M, 10.0M, Color.Red),
        new ColorRange(6.0M, 9.0M, Color.Yellow),
        new ColorRange(0.0M, 6.0M, Color.Green)
      });
    }

    public static GreencoGauge GetGauge2(bool mini)
    {
      return Utils.GetGauge(mini, 0.0M, 10000.0M, new List<ColorRange>()
      {
        new ColorRange(7500.0M, 10000.0M, Color.Red),
        new ColorRange(5000.0M, 7500.0M, Color.Yellow),
        new ColorRange(0.0M, 5000.0M, Color.Green)
      });
    }

    public static GreencoGauge GetGauge3(bool mini)
    {
      return Utils.GetGauge(mini, 0.0M, 3000.0M, new List<ColorRange>()
      {
        new ColorRange(1500.0M, 3000.0M, Color.Red),
        new ColorRange(1000.0M, 1500.0M, Color.Yellow),
        new ColorRange(0.0M, 1000.0M, Color.Green)
      });
    }

    public static GreencoGauge GetGauge(
      bool mini,
      Decimal min,
      Decimal max,
      List<ColorRange> ranges)
    {
      int num = Utils.GaugeDimension;
      if (mini)
        num = Utils.GaugeDimensionMini;
      GreencoGauge gauge = new GreencoGauge();
      gauge.Scale.Min = min;
      gauge.Scale.Max = max;
      gauge.Width = new Unit((double) num, UnitType.Pixel);
      gauge.Height = new Unit((double) num, UnitType.Pixel);
      if (mini)
      {
        gauge.Scale.MajorTicks.Size = new float?(10f);
        gauge.Scale.MinorTicks.Size = new float?(5f);
        gauge.Scale.Labels.Font = "8px Arial,sans-serif";
      }
      foreach (ColorRange range in ranges)
        gauge.Scale.Ranges.Add((GaugeRange) range);
      return gauge;
    }

    public static Panel GetGroupErrorPanel(string title, bool mini)
    {
      string str = "";
      if (mini)
        str = "Mini";
      Panel groupErrorPanel = new Panel();
      groupErrorPanel.CssClass = "pnlGaugeGroup" + str;
      Panel child1 = new Panel();
      child1.CssClass = "pnlGaugeGroupTitle" + str;
      Label child2 = new Label();
      child2.CssClass = "lblGaugeGroupTitle" + str;
      child2.Text = title;
      child1.Controls.Add((Control) child2);
      groupErrorPanel.Controls.Add((Control) child1);
      GreencoGauge gauge1 = Utils.GetGauge(mini);
      gauge1.Pointer.Value = new Decimal?(0.0M);
      GreencoGauge gauge2 = Utils.GetGauge(mini);
      gauge2.Pointer.Value = new Decimal?(0.0M);
      GreencoGauge gauge3 = Utils.GetGauge(mini);
      gauge3.Pointer.Value = new Decimal?(0.0M);
      groupErrorPanel.Controls.Add((Control) Utils.GetGaugePanel(gauge1, "", mini));
      groupErrorPanel.Controls.Add((Control) Utils.GetGaugePanel(gauge2, "", mini));
      groupErrorPanel.Controls.Add((Control) Utils.GetGaugePanel(gauge3, "", mini));
      Panel panel = new Panel();
      panel.CssClass = "pnlErrorOverlay";
      if (mini)
        panel.CssClass = "pnlErrorOverlayMini";
      panel.Controls.Add((Control) new Label()
      {
        Text = "NO READINGS IN DATABASE"
      });
      return groupErrorPanel;
    }

    public static Panel GetGroupPanel(
      string title,
      GreencoGauge r1,
      GreencoGauge r2,
      GreencoGauge r3,
      string c1,
      string c2,
      string c3,
      DateTime lastreading,
      bool mini)
    {
      string str = "";
      if (mini)
        str = "Mini";
      Panel groupPanel = new Panel();
      groupPanel.CssClass = "pnlGaugeGroup" + str;
      Panel child1 = new Panel();
      child1.CssClass = "pnlGaugeGroupTitle" + str;
      Label child2 = new Label();
      child2.CssClass = "lblGaugeGroupTitle" + str;
      child2.Text = title;
      child1.Controls.Add((Control) child2);
      groupPanel.Controls.Add((Control) child1);
      groupPanel.Controls.Add((Control) Utils.GetGaugePanel(r1, c1, mini));
      groupPanel.Controls.Add((Control) Utils.GetGaugePanel(r2, c2, mini));
      groupPanel.Controls.Add((Control) Utils.GetGaugePanel(r3, c3, mini));
      Panel child3 = new Panel();
      child3.CssClass = "clear";
      groupPanel.Controls.Add((Control) child3);
      Panel child4 = new Panel();
      child4.CssClass = "pnlDate" + str;
      child4.Controls.Add((Control) new Label()
      {
        Text = ("Last reading: " + lastreading.ToString("dd-MMM-yy") + " (" + Utils.HowLongAgo(lastreading) + ")")
      });
      groupPanel.Controls.Add((Control) child4);
      return groupPanel;
    }

    public static Panel GetDemoGroupPanel(
      string title,
      GreencoGauge r1,
      GreencoGauge r2,
      GreencoGauge r3,
      string c1,
      string c2,
      string c3,
      DateTime lastreading,
      bool mini,
      string TextAtBottom)
    {
      string str = "";
      if (mini)
        str = "Mini";
      Panel demoGroupPanel = new Panel();
      demoGroupPanel.CssClass = "pnlGaugeGroup" + str;
      Panel child1 = new Panel();
      child1.CssClass = "pnlGaugeGroupTitle" + str;
      Label child2 = new Label();
      child2.CssClass = "lblGaugeGroupTitle" + str;
      child2.Text = title;
      child1.Controls.Add((Control) child2);
      demoGroupPanel.Controls.Add((Control) child1);
      demoGroupPanel.Controls.Add((Control) Utils.GetGaugePanel(r1, c1, mini));
      demoGroupPanel.Controls.Add((Control) Utils.GetGaugePanel(r2, c2, mini));
      demoGroupPanel.Controls.Add((Control) Utils.GetGaugePanel(r3, c3, mini));
      Panel child3 = new Panel();
      child3.CssClass = "clear";
      demoGroupPanel.Controls.Add((Control) child3);
      Panel child4 = new Panel();
      child4.CssClass = "pnlDate" + str;
      child4.Controls.Add((Control) new Label()
      {
        Text = TextAtBottom
      });
      demoGroupPanel.Controls.Add((Control) child4);
      return demoGroupPanel;
    }

    public static Panel GetGaugePanel(GreencoGauge gauge, string text, bool mini)
    {
      string str = "";
      if (mini)
        str = "Mini";
      Panel gaugePanel = new Panel();
      if (gauge == null || text == null)
      {
        gaugePanel.CssClass = "pnlGaugeError" + str;
        return gaugePanel;
      }
      gaugePanel.CssClass = "pnlGaugeWrapper" + str;
      Panel child1 = new Panel();
      child1.CssClass = "pnlGaugeTitle" + str;
      Label child2 = new Label();
      child2.CssClass = "lblGaugeTitle" + str;
      child2.Text = text;
      child1.Controls.Add((Control) child2);
      gaugePanel.Controls.Add((Control) child1);
      Panel child3 = new Panel();
      child3.CssClass = gauge.FlareCssClass + str;
      child1.Controls.Add((Control) child3);
      Panel child4 = new Panel();
      child4.CssClass = "pnlGaugePanel" + str;
      child3.Controls.Add((Control) child4);
      Panel child5 = new Panel();
      child5.CssClass = "pnlGauge" + str;
      child5.Controls.Add((Control) gauge);
      child4.Controls.Add((Control) child5);
      return gaugePanel;
    }

    public static Dictionary<int, string> GetCompanyList()
    {
      Dictionary<int, string> companyList = new Dictionary<int, string>();
      string cmdText = "SELECT CompanyID, CompanyName FROM Company";
      using (SqlConnection conn = Utils.GetConn())
      {
        using (SqlCommand sqlCommand = new SqlCommand(cmdText, conn))
        {
          using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
          {
            while (sqlDataReader.Read())
              companyList.Add(sqlDataReader.GetInt32(0), sqlDataReader.GetString(1));
          }
        }
      }
      return companyList;
    }

    public static bool UnitHasAsset(DataRow row, string asset)
    {
      foreach (Sensor sensor in (List<Sensor>) row["Sensors"])
      {
        if (sensor.SensorCategory1 == asset)
          return true;
      }
      return false;
    }

    public static int GetCurrentUserCompanyId(MembershipUser user)
    {
      string cmdText = "SELECT Setting FROM UserSettings WHERE UserId='" + user.ProviderUserKey?.ToString() + "' AND Property='Company'";
      object obj = (object) null;
      using (SqlConnection conn = Utils.GetConn())
      {
        using (SqlCommand sqlCommand = new SqlCommand(cmdText, conn))
          obj = sqlCommand.ExecuteScalar();
      }
      int result;
      return obj == null || !int.TryParse(obj.ToString(), out result) ? -1 : result;
    }

    public static List<string> GetUnitUsers(int unitid)
    {
      List<string> unitUsers = new List<string>();
      bool flag;
      if (Roles.IsUserInRole("Admin"))
      {
        flag = true;
      }
      else
      {
        if (!Roles.IsUserInRole("CompanyAdmin"))
          return (List<string>) null;
        flag = false;
      }
      int num = -1;
      if (!flag)
        num = Utils.GetCurrentUserCompanyId(Membership.GetUser());
      string str1 = "SELECT au.UserName, comp.CompanyName FROM aspnet_users au" + " INNER JOIN UserSettings us ON us.UserID = au.UserId AND Property = 'Company'" + " INNER JOIN Company comp ON comp.CompanyID = CAST(us.Setting AS int)";
      if (!flag)
        str1 = str1 + " AND comp.CompanyID=" + num.ToString();
      string str2 = str1 + " INNER JOIN aspnet_UsersInRoles uir ON uir.UserId=au.UserId" + " INNER JOIN aspnet_Roles r ON r.RoleId=uir.RoleId";
      string cmdText = (!flag ? str2 + " AND r.RoleName IN ('CompanyAdmin', 'CompanyUser')" : str2 + " AND r.RoleName IN ('CompanyAdmin', 'Admin')") + " INNER JOIN UnitUser uu ON uu.UserId=au.UserId AND UnitID=" + unitid.ToString();
      using (SqlConnection conn = Utils.GetConn())
      {
        using (SqlCommand sqlCommand = new SqlCommand(cmdText, conn))
        {
          using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
          {
            while (sqlDataReader.Read())
            {
              if (!unitUsers.Contains(sqlDataReader.GetString(0)))
                unitUsers.Add(sqlDataReader.GetString(0));
            }
          }
        }
      }
      return unitUsers;
    }

    public static Dictionary<string, string> GetValidUsers()
    {
      bool flag;
      if (Roles.IsUserInRole("Admin"))
      {
        flag = true;
      }
      else
      {
        if (!Roles.IsUserInRole("CompanyAdmin"))
          return (Dictionary<string, string>) null;
        flag = false;
      }
      int num = -1;
      if (!flag)
        num = Utils.GetCurrentUserCompanyId(Membership.GetUser());
      Dictionary<string, string> validUsers = new Dictionary<string, string>();
      string str1 = "SELECT au.UserName, comp.CompanyName FROM aspnet_users au" + " INNER JOIN UserSettings us ON us.UserID = au.UserId AND Property = 'Company'" + " INNER JOIN Company comp ON comp.CompanyID = CAST(us.Setting AS int)";
      if (!flag)
        str1 = str1 + " AND comp.CompanyID=" + num.ToString();
      string str2 = str1 + " INNER JOIN aspnet_UsersInRoles uir ON uir.UserId=au.UserId" + " INNER JOIN aspnet_Roles r ON r.RoleId=uir.RoleId";
      string cmdText = !flag ? str2 + " AND r.RoleName IN ('CompanyAdmin', 'CompanyUser')" : str2 + " AND r.RoleName IN ('CompanyAdmin', 'Admin')";
      using (SqlConnection conn = Utils.GetConn())
      {
        using (SqlCommand sqlCommand = new SqlCommand(cmdText, conn))
        {
          using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
          {
            while (sqlDataReader.Read())
            {
              if (!validUsers.ContainsKey(sqlDataReader.GetString(0)))
                validUsers.Add(sqlDataReader.GetString(0), sqlDataReader.GetString(1));
            }
          }
        }
      }
      return validUsers;
    }

    public static Dictionary<string, string> GetValidUsersForSelectList()
    {
      bool flag;
      if (Roles.IsUserInRole("Admin"))
      {
        flag = true;
      }
      else
      {
        if (!Roles.IsUserInRole("CompanyAdmin"))
          return (Dictionary<string, string>) null;
        flag = false;
      }
      int num = -1;
      if (!flag)
        num = Utils.GetCurrentUserCompanyId(Membership.GetUser());
      Dictionary<string, string> usersForSelectList = new Dictionary<string, string>();
      string str1 = "SELECT au.UserName, au.UserID FROM aspnet_users au" + " INNER JOIN UserSettings us ON us.UserID = au.UserId AND Property = 'Company'" + " INNER JOIN Company comp ON comp.CompanyID = CAST(us.Setting AS int)";
      if (!flag)
        str1 = str1 + " AND comp.CompanyID=" + num.ToString();
      string str2 = str1 + " INNER JOIN aspnet_UsersInRoles uir ON uir.UserId=au.UserId" + " INNER JOIN aspnet_Roles r ON r.RoleId=uir.RoleId";
      string cmdText = !flag ? str2 + " AND r.RoleName IN ('CompanyAdmin', 'CompanyUser')" : str2 + " AND r.RoleName IN ('CompanyAdmin', 'Admin')";
      using (SqlConnection conn = Utils.GetConn())
      {
        using (SqlCommand sqlCommand = new SqlCommand(cmdText, conn))
        {
          using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
          {
            while (sqlDataReader.Read())
            {
              if (!usersForSelectList.ContainsKey(sqlDataReader.GetString(0)))
                usersForSelectList.Add(sqlDataReader.GetString(0), sqlDataReader.GetGuid(1).ToString());
            }
          }
        }
      }
      return usersForSelectList;
    }

    public static DataTable RefreshUnits(bool admin) => Utils.RefreshUnits(admin, false);

    public static DataTable RefreshUnits(bool admin, bool system)
    {
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("CompanyName", typeof (string));
      dataTable.Columns.Add("UnitID", typeof (int));
      dataTable.Columns.Add("UnitCategory", typeof (string));
      dataTable.Columns.Add("Name", typeof (string));
      dataTable.Columns.Add("VLinkName", typeof (string));
      dataTable.Columns.Add("SerialNumber", typeof (string));
      dataTable.Columns.Add("Sensors", typeof (List<Sensor>));
      dataTable.Columns.Add("Users", typeof (List<string>));
      dataTable.Columns.Add("Active", typeof (bool));
      dataTable.Columns.Add("IsMobile", typeof (bool));
      DataTable units;
      try
      {
        units = VLinkUtility.GetUnits();
      }
      catch (VlinkException ex)
      {
        return dataTable;
      }
      MembershipUser user = (MembershipUser) null;
      if (!system)
      {
        try
        {
          user = Membership.GetUser();
        }
        catch (Exception ex)
        {
          return (DataTable) null;
        }
      }
      string str1 = "SELECT u.UnitID, au.UserName FROM Unit u" + " INNER JOIN UnitUser uu ON u.UnitID = uu.UnitID" + " INNER JOIN aspnet_Users au ON au.UserId = uu.UserId";
      string cmdText1;
      if (system)
        cmdText1 = str1 ?? "";
      else if (admin && Roles.IsUserInRole("Admin"))
        cmdText1 = str1 ?? "";
      else if (Roles.IsUserInRole(user.UserName, "CompanyAdmin"))
      {
        int currentUserCompanyId = Utils.GetCurrentUserCompanyId(user);
        if (currentUserCompanyId == -1)
          return (DataTable) null;
        cmdText1 = str1 + " INNER JOIN UserSettings us ON us.UserID=au.UserId AND us.Property='Company' AND us.Setting='" + currentUserCompanyId.ToString() + "'";
      }
      else
        cmdText1 = str1 + " AND au.LoweredUserName='" + user.UserName.ToLower() + "'";
      string str2 = "SELECT u.UnitID, u.UnitName, u.UnitCategory, comp.CompanyName," + " s.SensorID, s.SensorName, s.SensorCategory1, s.SensorCategory2," + " c.ChannelID, c.ChannelName, c.ChannelIndex, c.Min, c.Max, r.LimitType, r.LimitValue, u.Active, s.Active, s.SensorIndex, u.IsMobile, u.WorkOrderTag, u.TechnicianIdTag, u.FluidTypeTag, u.SerialNumberTag, u.HoursMilesTag" + " FROM Unit u " + " LEFT JOIN Sensor s ON s.UnitID=u.UnitID" + " LEFT JOIN Channel c ON c.SensorID=s.SensorID" + " LEFT JOIN Range r ON r.ChannelID=c.ChannelID" + " INNER JOIN Company comp ON comp.CompanyID=u.CompanyID";
      string cmdText2 = !system ? (!Roles.IsUserInRole("Admin") ? (!Roles.IsUserInRole("CompanyAdmin") ? str2 + " INNER JOIN UnitUser uu ON uu.UnitID=u.UnitID INNER JOIN aspnet_Users au ON au.UserId=uu.UserId AND au.UserName='" + user.UserName + "'" : str2 + " INNER JOIN UserSettings us ON us.Property='Company' AND CAST(us.Setting AS int)=comp.CompanyID INNER JOIN aspnet_Users au ON au.UserId=us.UserId AND au.UserName='" + user.UserName + "'") : str2 ?? "") : str2 ?? "";
      int ordinal1 = 19;
      int ordinal2 = 20;
      int ordinal3 = 21;
      int ordinal4 = 22;
      int ordinal5 = 23;
      Dictionary<int, List<string>> dictionary = new Dictionary<int, List<string>>();
      using (SqlConnection conn = Utils.GetConn())
      {
        if (system || !admin || !Roles.IsUserInRole("Admin") && !Roles.IsUserInRole("CompanyAdmin"))
        {
          using (SqlCommand sqlCommand = new SqlCommand(cmdText1, conn))
          {
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
              while (sqlDataReader.Read())
              {
                int int32 = sqlDataReader.GetInt32(0);
                List<string> stringList;
                if (dictionary.ContainsKey(int32))
                {
                  stringList = dictionary[int32];
                }
                else
                {
                  stringList = new List<string>();
                  dictionary.Add(int32, stringList);
                }
                stringList.Add(sqlDataReader.GetString(1));
              }
            }
          }
        }
        using (SqlCommand sqlCommand = new SqlCommand(cmdText2, conn))
        {
          using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
          {
            while (sqlDataReader.Read())
            {
              if (!sqlDataReader.IsDBNull(0) && !sqlDataReader.IsDBNull(1) && !sqlDataReader.IsDBNull(2) && !sqlDataReader.IsDBNull(3))
              {
                int int32_1 = sqlDataReader.GetInt32(0);
                int index1 = -1;
                if (!units.Columns.Contains("UnitID"))
                  throw new VlinkException("Error 1701: Incorrect format of unit table");
                if (!units.Columns.Contains("Name"))
                  throw new VlinkException("Error 1702: Incorrect format of unit table");
                if (!units.Columns.Contains("SerialNumber"))
                  throw new VlinkException("Error 1703: Incorrect format of unit table");
                for (int index2 = 0; index2 < units.Rows.Count; ++index2)
                {
                  if (units.Rows[index2]["UnitID"] is int)
                  {
                    if ((int) units.Rows[index2]["UnitID"] == int32_1)
                      index1 = index2;
                  }
                  else
                  {
                    int result;
                    if (int.TryParse(units.Rows[index2]["UnitID"].ToString(), out result) && result == int32_1)
                      index1 = index2;
                  }
                }
                if (index1 != -1)
                {
                  DataRow dataRow = (DataRow) null;
                  for (int index3 = 0; index3 < dataTable.Rows.Count; ++index3)
                  {
                    if ((int) dataTable.Rows[index3]["UnitID"] == int32_1)
                    {
                      dataRow = dataTable.Rows[index3];
                      break;
                    }
                  }
                  if (dataRow == null)
                  {
                    string str3 = sqlDataReader.GetString(1);
                    string str4 = sqlDataReader.GetString(3);
                    string str5 = units.Rows[index1]["Name"].ToString();
                    string str6 = units.Rows[index1]["SerialNumber"].ToString();
                    string str7 = sqlDataReader.GetString(2);
                    bool boolean = sqlDataReader.GetBoolean(15);
                    bool flag = !sqlDataReader.IsDBNull(18) && sqlDataReader.GetBoolean(18);
                    List<Sensor> sensorList = new List<Sensor>();
                    dataRow = dataTable.Rows.Add((object) str4, (object) int32_1, (object) str7, (object) str3, (object) str5, (object) str6, (object) sensorList, null, (object) boolean, (object) flag);
                  }
                  List<Sensor> sensorList1 = (List<Sensor>) dataRow["Sensors"];
                  Sensor sensor1 = (Sensor) null;
                  if (!sqlDataReader.IsDBNull(4))
                  {
                    int int32_2 = sqlDataReader.GetInt32(4);
                    foreach (Sensor sensor2 in sensorList1)
                    {
                      if (sensor2.SensorID == int32_2)
                        sensor1 = sensor2;
                    }
                    if (sensor1 == null)
                    {
                      bool active = sqlDataReader.IsDBNull(16) || sqlDataReader.GetBoolean(16);
                      short? sensorIndex = !sqlDataReader.IsDBNull(17) ? new short?(sqlDataReader.GetInt16(17)) : new short?();
                      sensor1 = new Sensor(sqlDataReader.GetString(5), int32_2, SensorType.ParticleCount, sqlDataReader.GetString(6), sqlDataReader.GetString(7), (List<string>) null, active, sensorIndex);
                      sensorList1.Add(sensor1);
                      if (dataRow["IsMobile"] != null && dataRow["IsMobile"] != DBNull.Value && (bool) dataRow["IsMobile"])
                      {
                        int min = 0;
                        int max = 40;
                        int channelID = -1;
                        string name1 = "meta1";
                        string name2 = "meta2";
                        string name3 = "meta3";
                        string name4 = "meta4";
                        string name5 = "meta5";
                        if (!sqlDataReader.IsDBNull(ordinal1))
                          sensor1.Channels.Add(new Channel(name1, channelID, sqlDataReader.GetInt16(ordinal1), (Decimal) min, (Decimal) max));
                        if (!sqlDataReader.IsDBNull(ordinal2))
                          sensor1.Channels.Add(new Channel(name2, channelID, sqlDataReader.GetInt16(ordinal2), (Decimal) min, (Decimal) max));
                        if (!sqlDataReader.IsDBNull(ordinal3))
                          sensor1.Channels.Add(new Channel(name3, channelID, sqlDataReader.GetInt16(ordinal3), (Decimal) min, (Decimal) max));
                        if (!sqlDataReader.IsDBNull(ordinal4))
                          sensor1.Channels.Add(new Channel(name4, channelID, sqlDataReader.GetInt16(ordinal4), (Decimal) min, (Decimal) max));
                        if (!sqlDataReader.IsDBNull(ordinal5))
                          sensor1.Channels.Add(new Channel(name5, channelID, sqlDataReader.GetInt16(ordinal5), (Decimal) min, (Decimal) max));
                      }
                      if (dictionary.ContainsKey(sensor1.SensorID))
                        sensor1.Permissions = dictionary[sensor1.SensorID];
                    }
                    Channel channel1 = (Channel) null;
                    if (!sqlDataReader.IsDBNull(8))
                    {
                      int int32_3 = sqlDataReader.GetInt32(8);
                      foreach (Channel channel2 in sensor1.Channels)
                      {
                        if (channel2.ChannelID == int32_3)
                          channel1 = channel2;
                      }
                      if (channel1 == null)
                      {
                        channel1 = new Channel(sqlDataReader.GetString(9), int32_3, sqlDataReader.GetInt16(10), sqlDataReader.GetDecimal(11), sqlDataReader.GetDecimal(12));
                        sensor1.Channels.Add(channel1);
                      }
                      if (!sqlDataReader.IsDBNull(13))
                        channel1.Ranges.Add(new SensorRange(sqlDataReader.GetByte(13), sqlDataReader.GetDecimal(14)));
                    }
                  }
                }
              }
            }
          }
        }
      }
      return dataTable;
    }

    public static DataTable GetNewUnits()
    {
      DataTable newUnits = new DataTable();
      newUnits.Columns.Add("UnitID", typeof (int));
      newUnits.Columns.Add("VLinkName", typeof (string));
      newUnits.Columns.Add("SerialNumber", typeof (string));
      DataTable units = VLinkUtility.GetUnits();
      string cmdText = "SELECT u.UnitID FROM Unit u";
      List<int> intList = new List<int>();
      using (SqlConnection conn = Utils.GetConn())
      {
        using (SqlCommand sqlCommand = new SqlCommand(cmdText, conn))
        {
          using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
          {
            while (sqlDataReader.Read())
              intList.Add(sqlDataReader.GetInt32(0));
          }
        }
      }
      for (int index = 0; index < units.Rows.Count; ++index)
      {
        int num = -1;
        if (units.Rows[index]["UnitID"] is int)
        {
          num = (int) units.Rows[index]["UnitID"];
        }
        else
        {
          int result;
          if (int.TryParse(units.Rows[index]["UnitID"].ToString(), out result))
            num = result;
        }
        if (num != -1 && !intList.Contains(num))
          newUnits.Rows.Add((object) num, (object) units.Rows[index]["Name"].ToString(), (object) units.Rows[index]["SerialNumber"].ToString());
      }
      return newUnits;
    }

    public static bool CheckForZero(DataRow row, int DataTypeIndex)
    {
      try
      {
        switch (DataTypeIndex)
        {
          case 1:
            return !((Decimal) row["ISO-4"] != 0.0M) && !((Decimal) row["ISO-6"] != 0.0M) && !((Decimal) row["ISO-14"] != 0.0M) && !((Decimal) row["ISO-21"] != 0.0M);
          case 2:
            return !((Decimal) row["RH"] != 0.0M) && !((Decimal) row["Temp"] != 0.0M);
          default:
            return false;
        }
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    private static void SetUnitActiveFlag(DataRow unit, bool active)
    {
      unit["Active"] = (object) active;
      string cmdText = "UPDATE Unit SET Active=@active WHERE UnitID=" + unit["UnitID"]?.ToString();
      using (SqlConnection conn = Utils.GetConn())
      {
        using (SqlCommand sqlCommand = new SqlCommand(cmdText, conn))
        {
          sqlCommand.Parameters.AddWithValue("@active", (object) active);
          sqlCommand.ExecuteNonQuery();
        }
      }
    }

    public static int UpdateAllReadings(
      DateTime start,
      DateTime end,
      bool allSensors,
      bool updateFlags)
    {
      DataTable dataTable1 = Utils.RefreshUnits(true, true);
      DataTable dataTable2 = new DataTable();
      dataTable2.Columns.Add("sensorid", typeof (int));
      dataTable2.Columns.Add("date", typeof (DateTime));
      dataTable2.Columns.Add("alt", typeof (Decimal));
      dataTable2.Columns.Add("meta1", typeof (string));
      dataTable2.Columns.Add("meta2", typeof (string));
      dataTable2.Columns.Add("meta3", typeof (string));
      dataTable2.Columns.Add("meta4", typeof (string));
      dataTable2.Columns.Add("meta5", typeof (string));
      dataTable2.Columns.Add("ISO4", typeof (int));
      dataTable2.Columns.Add("ISO6", typeof (int));
      dataTable2.Columns.Add("ISO14", typeof (int));
      dataTable2.Columns.Add("ISO21", typeof (int));
      dataTable2.Columns.Add("RH", typeof (int));
      dataTable2.Columns.Add("Temp", typeof (int));
      DataTable dataTable3 = new DataTable();
      dataTable3.Columns.Add("sensorid", typeof (int));
      dataTable3.Columns.Add("configid", typeof (int));
      dataTable3.Columns.Add("begindate", typeof (DateTime));
      dataTable3.Columns.Add("enddate", typeof (DateTime));
      dataTable3.Columns.Add("alarmstring", typeof (string));
      dataTable3.Columns.Add("alarmvalue", typeof (double));
      foreach (DataRow row1 in (InternalDataCollectionBase) dataTable1.Rows)
      {
        if (((IEnumerable<Sensor>) row1["Sensors"]).Count<Sensor>((System.Func<Sensor, bool>) (x => x.Active)) > 0 && (allSensors || (bool) row1["Active"]))
        {
          int unitID = (int) row1["UnitID"];
          DataTable dataTable4 = (DataTable) null;
          DataTable packets;
          try
          {
            packets = VLinkUtility.GetPackets(unitID, new DateTime?(start), new DateTime?(end));
          }
          catch
          {
            continue;
          }
          try
          {
            dataTable4 = VLinkUtility.GetAlarms(unitID, new DateTime?(start), new DateTime?(end), true);
          }
          catch
          {
          }
          if (packets.Rows.Count < 1 && dataTable4.Rows.Count < 1)
          {
            if (updateFlags && (bool) row1["Active"])
            {
              string cmdText = "SELECT max(d.date) from sensor s INNER JOIN data d ON s.sensorid=d.sensorid WHERE s.unitid=" + unitID.ToString();
              object obj = (object) null;
              using (SqlConnection conn = Utils.GetConn())
              {
                using (SqlCommand sqlCommand = new SqlCommand(cmdText, conn))
                  obj = sqlCommand.ExecuteScalar();
              }
              if (obj == null || obj == DBNull.Value || !(obj is DateTime dateTime) || (dateTime - DateTime.Now).TotalDays > 30.0)
              {
                Utils.SetUnitActiveFlag(row1, false);
                Utils.Log("Unit " + unitID.ToString() + " has been disabled due to no activity over the past 30 days");
              }
            }
          }
          else
          {
            if (updateFlags && !(bool) row1["Active"])
              Utils.SetUnitActiveFlag(row1, true);
            List<long> longList1 = new List<long>();
            List<long> longList2 = new List<long>();
            string cmdText1 = "SELECT date FROM Data d" + " INNER JOIN Sensor s ON d.sensorid=s.sensorid AND s.unitid=" + unitID.ToString() + " WHERE Date > '" + start.ToString(Utils.MasterDateTimeQueryString) + "' AND Date <= '" + end.ToString(Utils.MasterDateTimeQueryString) + "'";
            string cmdText2 = "SELECT BeginDate FROM Alarm a" + " INNER JOIN Sensor s ON a.sensorid=s.sensorid AND s.unitid=" + unitID.ToString() + " WHERE beginDate > '" + start.ToString(Utils.MasterDateTimeQueryString) + "' AND beginDate <= '" + end.ToString(Utils.MasterDateTimeQueryString) + "'";
            using (SqlConnection conn = Utils.GetConn())
            {
              using (SqlCommand sqlCommand = new SqlCommand(cmdText1, conn))
              {
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                  while (sqlDataReader.Read())
                    longList1.Add(sqlDataReader.GetDateTime(0).Ticks);
                }
              }
              using (SqlCommand sqlCommand = new SqlCommand(cmdText2, conn))
              {
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                  while (sqlDataReader.Read())
                    longList2.Add(sqlDataReader.GetDateTime(0).Ticks);
                }
              }
            }
            foreach (DataRow row2 in (InternalDataCollectionBase) packets.Rows)
            {
              int result1 = -1;
              DateTime result2;
              if (row2["DataType"] != null && row2["DataType"] != DBNull.Value && int.TryParse(row2["DataType"].ToString(), out result1) && row2["PacketDate"] != null && row2["PacketDate"] != DBNull.Value && DateTime.TryParse(row2["PacketDate"].ToString(), out result2) && !longList1.Contains(result2.Ticks))
              {
                DataRow dataRow = (DataRow) null;
                Channel channel1 = (Channel) null;
                Sensor sensor1 = (Sensor) null;
                foreach (Sensor sensor2 in (List<Sensor>) row1["Sensors"])
                {
                  foreach (Channel channel2 in sensor2.Channels)
                  {
                    if ((int) channel2.ChannelIndex == result1)
                    {
                      channel1 = channel2;
                      break;
                    }
                  }
                  if (channel1 != null)
                  {
                    sensor1 = sensor2;
                    dataRow = row1;
                    break;
                  }
                }
                if (dataRow != null)
                {
                  DataRow row3 = (DataRow) null;
                  foreach (DataRow row4 in (InternalDataCollectionBase) dataTable2.Rows)
                  {
                    if ((DateTime) row4["Date"] == result2 && (int) row4["sensorid"] == sensor1.SensorID)
                    {
                      row3 = row4;
                      break;
                    }
                  }
                  if (row3 == null)
                  {
                    row3 = dataTable2.NewRow();
                    dataTable2.Rows.Add(row3);
                    row3["sensorid"] = (object) sensor1.SensorID;
                    row3["Date"] = (object) result2;
                  }
                  switch (channel1.Name)
                  {
                    case "ISO14":
                    case "ISO21":
                    case "ISO4":
                    case "ISO6":
                    case "RH":
                    case "Temp":
                      short result3;
                      if (short.TryParse(row2["DataValue1"].ToString(), out result3))
                      {
                        row3[channel1.Name] = (object) (int) result3;
                        continue;
                      }
                      continue;
                    case "alt":
                      Decimal result4;
                      if (Decimal.TryParse(row2["DataValue1"].ToString(), out result4))
                      {
                        row3[channel1.Name] = (object) result4;
                        continue;
                      }
                      continue;
                    case "meta1":
                    case "meta2":
                    case "meta3":
                    case "meta4":
                    case "meta5":
                      row3[channel1.Name] = (object) row2["DataString"].ToString();
                      continue;
                    default:
                      continue;
                  }
                }
              }
            }
            DataTable dataTable5 = (DataTable) null;
            if (dataTable4.Rows.Count > 0)
              dataTable5 = VLinkUtility.GetAlarmConfig((int) row1["UnitID"]);
            foreach (DataRow row5 in (InternalDataCollectionBase) dataTable4.Rows)
            {
              if (row5["ConfigID"] != null)
              {
                int num1 = (int) row5["ConfigID"];
                DataRow dataRow = (DataRow) null;
                foreach (DataRow row6 in (InternalDataCollectionBase) dataTable5.Rows)
                {
                  if (row6["id"] != null && (int) row6["id"] == num1)
                    dataRow = row6;
                }
                if (dataRow != null)
                {
                  int num2 = int.Parse(dataRow["AlarmType"].ToString());
                  int num3 = (int) dataRow["SensorID"];
                  DateTime dateTime = (DateTime) row5["BeginDate"];
                  if (!longList2.Contains(dateTime.Ticks))
                  {
                    dataTable3.NewRow();
                    dataTable3.Rows.Add((object) num3, (object) num1, row5["BeginDate"], row5["EndDate"], row5["AlarmString"], row5["AlarmValue"], (object) num2);
                  }
                }
              }
            }
          }
        }
      }
      string cmdText3 = "";
      string cmdText4 = "";
      foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
      {
        cmdText3 += "INSERT INTO Data (sensorid, date, alt, meta1, meta2, meta3, meta4, meta5, ISO4, ISO6, ISO14, ISO21, RH, Temp)";
        cmdText3 = cmdText3 + " VALUES (" + row["sensorid"]?.ToString() + ", '" + ((DateTime) row["date"]).ToString(Utils.MasterDateTimeQueryString) + "', ";
        cmdText3 = cmdText3 + Utils.qHelpValue(row["alt"]) + ", " + Utils.qHelpString(row["meta1"]) + ", " + Utils.qHelpString(row["meta2"]) + ", " + Utils.qHelpString(row["meta3"]) + ", " + Utils.qHelpString(row["meta4"]) + ", " + Utils.qHelpString(row["meta5"]) + ", ";
        cmdText3 = cmdText3 + Utils.qHelpValue(row["ISO4"]) + ", " + Utils.qHelpValue(row["ISO6"]) + ", " + Utils.qHelpValue(row["ISO14"]) + ", " + Utils.qHelpValue(row["ISO21"]) + ", ";
        cmdText3 = cmdText3 + Utils.qHelpValue(row["RH"]) + ", " + Utils.qHelpValue(row["Temp"]) + "); ";
      }
      foreach (DataRow row in (InternalDataCollectionBase) dataTable3.Rows)
      {
        cmdText4 += "INSERT INTO Alarm (sensorid, configid, begindate, enddate, alarmstring, alarmvalue, alarmtype)";
        cmdText4 = cmdText4 + " VALUES (" + row["sensorid"]?.ToString() + ", " + row["configid"]?.ToString() + ", '" + ((DateTime) row["begindate"]).ToString(Utils.MasterDateTimeQueryString) + "'";
        cmdText4 = cmdText4 + ", " + Utils.qHelpDate((object) (DateTime) row["enddate"]) + ", " + Utils.qHelpString(row["alarmstring"]) + ", " + Utils.qHelpValue(row["alarmvalue"]) + ", " + row["AlarmType"].ToString() + "); ";
      }
      using (SqlConnection conn = Utils.GetConn())
      {
        using (SqlCommand sqlCommand = new SqlCommand(cmdText3, conn))
        {
          if (cmdText3 != "")
            sqlCommand.ExecuteNonQuery();
        }
        using (SqlCommand sqlCommand = new SqlCommand(cmdText4, conn))
        {
          if (cmdText4 != "")
            sqlCommand.ExecuteNonQuery();
        }
      }
      return dataTable2.Rows.Count;
    }

    public static string qHelpString(object o)
    {
      return o == DBNull.Value ? "NULL" : "'" + o.ToString() + "'";
    }

    public static string qHelpValue(object o) => o == DBNull.Value ? "NULL" : o.ToString();

    public static string qHelpDate(object o)
    {
      return o == DBNull.Value || !(o is DateTime dateTime) ? "NULL" : "'" + dateTime.ToString(Utils.MasterDateTimeDisplayString) + "'";
    }

    public static void RefreshLatestReadings(DataTable units)
    {
      foreach (DataRow row in (InternalDataCollectionBase) units.Rows)
      {
        List<Sensor> sensorList = (List<Sensor>) row["Sensors"];
        using (SqlConnection conn = Utils.GetConn())
        {
          foreach (Sensor sensor in sensorList)
          {
            if (sensor.Active)
              sensor.RefreshLatestReading(conn);
          }
        }
      }
    }

    public static DataTable GetReadings(DataRow unit, DateTime? start, DateTime? end)
    {
      int unitID = (int) unit["UnitID"];
      DataTable packets = VLinkUtility.GetPackets(unitID, start, end);
      DataTable readings = new DataTable();
      readings.Columns.Add("id", typeof (int));
      readings.Columns.Add("UnitID", typeof (int));
      readings.Columns.Add("UnitName", typeof (string));
      readings.Columns.Add("SensorID", typeof (int));
      readings.Columns.Add("SensorName", typeof (string));
      readings.Columns.Add("Date", typeof (DateTime));
      readings.Columns.Add("Alt", typeof (Decimal));
      List<Sensor> sensorList = (List<Sensor>) unit["Sensors"];
      foreach (Sensor sensor in sensorList)
      {
        sensor.LastReadingDate = new DateTime?();
        foreach (Channel channel in sensor.Channels)
        {
          if (!readings.Columns.Contains(channel.Name))
            readings.Columns.Add(channel.Name, typeof (Decimal));
        }
      }
      int num = 0;
      foreach (DataRow row1 in (InternalDataCollectionBase) packets.Rows)
      {
        int result1 = -1;
        DateTime result2;
        if (row1["DataType"] != null && row1["DataType"] != DBNull.Value && int.TryParse(row1["DataType"].ToString(), out result1) && row1["DataValue1"] != null && row1["DataValue1"] != DBNull.Value && row1["PacketDate"] != null && row1["PacketDate"] != DBNull.Value && DateTime.TryParse(row1["PacketDate"].ToString(), out result2))
        {
          Channel channel1 = (Channel) null;
          Sensor sensor1 = (Sensor) null;
          foreach (Sensor sensor2 in sensorList)
          {
            foreach (Channel channel2 in sensor2.Channels)
            {
              if ((int) channel2.ChannelIndex == result1)
              {
                channel1 = channel2;
                break;
              }
            }
            if (channel1 != null)
            {
              sensor1 = sensor2;
              break;
            }
          }
          if (sensor1 != null)
          {
            if (sensor1.LastReadingDate.HasValue)
            {
              DateTime? lastReadingDate = sensor1.LastReadingDate;
              DateTime dateTime = result2;
              if ((lastReadingDate.HasValue ? (lastReadingDate.GetValueOrDefault() <= dateTime ? 1 : 0) : 0) == 0)
                goto label_29;
            }
            sensor1.LastReadingDate = new DateTime?(result2);
label_29:
            if (channel1 != null || sensor1 != null)
            {
              bool flag = false;
              Decimal result3;
              if (Decimal.TryParse(row1["DataValue1"].ToString(), out result3))
              {
                foreach (DataRow row2 in (InternalDataCollectionBase) readings.Rows)
                {
                  if (((DateTime) row2["Date"]).Ticks == result2.Ticks && (int) row2["SensorID"] == sensor1.SensorID)
                  {
                    if (channel1 == null)
                      row2["Alt"] = (object) result3;
                    else
                      row2[channel1.Name] = (object) result3;
                    flag = true;
                  }
                }
                if (!flag)
                {
                  DataRow row3 = readings.NewRow();
                  row3["id"] = (object) num++;
                  row3["UnitID"] = (object) unitID;
                  row3["UnitName"] = (object) unit["Name"].ToString();
                  row3["SensorID"] = (object) sensor1.SensorID;
                  row3["SensorName"] = (object) sensor1.Name;
                  row3["Date"] = (object) result2;
                  if (channel1 == null)
                    row3["Alt"] = (object) result3;
                  else
                    row3[channel1.Name] = (object) result3;
                  readings.Rows.Add(row3);
                }
              }
            }
          }
        }
      }
      return readings;
    }

    public static void PopulateAlarms(DataRow unit)
    {
      int num = (int) unit["UnitID"];
      foreach (Sensor sensor in (List<Sensor>) unit["Sensors"])
        sensor.Alarms = new List<AlarmHelper>();
    }

    public static DataTable GetAlarms(DataRow unit, bool current, DateTime? start, DateTime? end)
    {
      int num = (int) unit["UnitID"];
      DataTable alarms1 = VLinkUtility.GetAlarms(num, start, end, current);
      DataTable alarmConfig = VLinkUtility.GetAlarmConfig(num);
      DataTable alarms2 = new DataTable();
      alarms2.Columns.Add("UnitID", typeof (int));
      alarms2.Columns.Add("UnitName", typeof (string));
      alarms2.Columns.Add("SensorID", typeof (int));
      alarms2.Columns.Add("SensorName", typeof (string));
      alarms2.Columns.Add("Alarms", typeof (List<AlarmHelper>));
      foreach (DataRow row1 in (InternalDataCollectionBase) alarms1.Rows)
      {
        int result1 = -1;
        int result2 = -1;
        int result3 = -1;
        DateTime result4 = DateTime.MinValue;
        if (row1["id"] != DBNull.Value && int.TryParse(row1["id"].ToString(), out result1) && row1["UnitID"] != DBNull.Value && int.TryParse(row1["UnitID"].ToString(), out result2) && (row1["BeginDate"] == DBNull.Value || DateTime.TryParse(row1["BeginDate"].ToString(), out result4)) && (row1["ConfigID"] == DBNull.Value || int.TryParse(row1["ConfigID"].ToString(), out result3)) && !(result4 == DateTime.MinValue))
        {
          string alarmText = row1["AlarmString"].ToString();
          DataRow dataRow1 = (DataRow) null;
          DataRow dataRow2 = (DataRow) null;
          foreach (DataRow row2 in (InternalDataCollectionBase) alarmConfig.Rows)
          {
            if (row2["id"].ToString() == result3.ToString())
              dataRow2 = row2;
          }
          if (dataRow2 != null)
          {
            int result5 = -1;
            int result6 = -1;
            if (int.TryParse(dataRow2["AlarmType"].ToString(), out result5) && int.TryParse(dataRow2["SensorID"].ToString(), out result6))
            {
              AlarmHelper alarmHelper = new AlarmHelper(result3, alarmText, result4, result5 >= 1000);
              Sensor sensor1 = (Sensor) null;
              foreach (Sensor sensor2 in (List<Sensor>) unit["Sensors"])
              {
                foreach (Channel channel in sensor2.Channels)
                {
                  if ((int) channel.ChannelIndex == result6)
                    sensor1 = sensor2;
                }
              }
              if (sensor1 != null)
              {
                foreach (DataRow row3 in (InternalDataCollectionBase) alarms2.Rows)
                {
                  if ((int) row3["SensorID"] == sensor1.SensorID)
                    dataRow1 = row3;
                }
                if (dataRow1 == null)
                {
                  DataRow row4 = alarms2.NewRow();
                  row4["UnitID"] = (object) num;
                  row4["UnitName"] = (object) unit["Name"].ToString();
                  row4["SensorID"] = (object) sensor1.SensorID;
                  row4["SensorName"] = (object) sensor1.Name;
                  row4["Alarms"] = (object) new List<AlarmHelper>();
                  alarms2.Rows.Add(row4);
                  dataRow1 = row4;
                }
                ((List<AlarmHelper>) dataRow1["Alarms"]).Add(alarmHelper);
              }
            }
          }
        }
      }
      return alarms2;
    }

    public static string HowLongAgo(DateTime date)
    {
      TimeSpan timeSpan1 = DateTime.Now - date;
      if (Math.Round(timeSpan1.TotalSeconds, 0) == 1.0)
        return "1 second ago";
      TimeSpan timeSpan2 = DateTime.Now - date;
      if (timeSpan2.TotalSeconds < 60.0)
      {
        timeSpan2 = DateTime.Now - date;
        return timeSpan2.TotalSeconds.ToString("0") + " seconds ago";
      }
      if (Math.Round(timeSpan1.TotalMinutes, 0) == 1.0)
        return "1 minute ago";
      timeSpan2 = DateTime.Now - date;
      if (timeSpan2.TotalMinutes < 60.0)
      {
        timeSpan2 = DateTime.Now - date;
        return timeSpan2.TotalMinutes.ToString("0") + " minutes ago";
      }
      if (Math.Round(timeSpan1.TotalHours, 0) == 1.0)
        return "1 hour ago";
      timeSpan2 = DateTime.Now - date;
      if (timeSpan2.TotalHours < 48.0)
      {
        timeSpan2 = DateTime.Now - date;
        return timeSpan2.TotalHours.ToString("0") + " hours ago";
      }
      timeSpan2 = DateTime.Now - date;
      return timeSpan2.TotalDays.ToString("0") + " days ago";
    }

    public static string GetIsoCodeDisplayString(object pseudoReading)
    {
      result = int.MinValue;
      switch (pseudoReading)
      {
        case int result:
label_8:
          return result == int.MinValue ? "N/A" : ((int) ((Decimal) result / 10.0M)).ToString();
        case double num1:
          result = (int) num1;
          goto label_8;
        case Decimal num2:
          result = (int) num2;
          goto label_8;
        case string _:
          int.TryParse((string) pseudoReading, out result);
          goto label_8;
        case null:
          return "N/A";
        default:
          if (pseudoReading == DBNull.Value)
            return "N/A";
          int.TryParse(pseudoReading.ToString(), out result);
          goto label_8;
      }
    }

    public static Decimal GetIsoCodeDecimal(object pseudoReading)
    {
      result = int.MinValue;
      switch (pseudoReading)
      {
        case int result:
label_8:
          return result == int.MinValue ? Decimal.MinValue : Math.Max(4.0M, (Decimal) result / 10.0M);
        case double num1:
          result = (int) num1;
          goto label_8;
        case Decimal num2:
          result = (int) num2;
          goto label_8;
        case string _:
          int.TryParse((string) pseudoReading, out result);
          goto label_8;
        case null:
          return Decimal.MinValue;
        default:
          if (pseudoReading == DBNull.Value)
            return Decimal.MinValue;
          int.TryParse(pseudoReading.ToString(), out result);
          goto label_8;
      }
    }

    public static int GetIsoCode(object pseudoReading)
    {
      result = int.MinValue;
      switch (pseudoReading)
      {
        case int result:
label_8:
          return result == int.MinValue ? int.MinValue : Math.Max(4, result / 10);
        case double num1:
          result = (int) num1;
          goto label_8;
        case Decimal num2:
          result = (int) num2;
          goto label_8;
        case string _:
          int.TryParse((string) pseudoReading, out result);
          goto label_8;
        case null:
          return int.MinValue;
        default:
          if (pseudoReading == DBNull.Value)
            return int.MinValue;
          int.TryParse(pseudoReading.ToString(), out result);
          goto label_8;
      }
    }

    public static object GetIsoCodeTableObject(object pseudoReading)
    {
      int isoCode = Utils.GetIsoCode(pseudoReading);
      return isoCode == int.MinValue ? (object) DBNull.Value : (object) isoCode;
    }

    public static double GetReading(object pseudoReading)
    {
      result = double.MinValue;
      switch (pseudoReading)
      {
        case double result:
label_8:
          return result == double.MinValue ? double.MinValue : Math.Pow(2.0, result / 10.0 - 1.0) * 0.01;
        case int num1:
          result = (double) num1;
          goto label_8;
        case Decimal num2:
          result = (double) num2;
          goto label_8;
        case string _:
          double.TryParse((string) pseudoReading, out result);
          goto label_8;
        case null:
          return double.MinValue;
        default:
          if (pseudoReading == DBNull.Value)
            return double.MinValue;
          double.TryParse(pseudoReading.ToString(), out result);
          goto label_8;
      }
    }

    public static Decimal GetAverage(List<Decimal> ListOfValues, bool RoundDown)
    {
      Decimal num = 0M;
      foreach (Decimal listOfValue in ListOfValues)
        num += listOfValue;
      return RoundDown ? Math.Floor(num / (Decimal) ListOfValues.Count) : num / (Decimal) ListOfValues.Count;
    }

    public static object GetReadingTableObject(object pseudoReading)
    {
      double reading = Utils.GetReading(pseudoReading);
      return reading == double.MinValue ? (object) DBNull.Value : (object) reading;
    }

    public static object GetReadingTableTempObject(object tableCell)
    {
      if (tableCell == DBNull.Value)
        return (object) DBNull.Value;
      if (tableCell is Decimal num)
        return (object) (num * ScaleConstants.TEMPERATURE_FACTOR);
      Decimal result;
      return Decimal.TryParse(tableCell.ToString(), out result) ? (object) (result * ScaleConstants.TEMPERATURE_FACTOR) : (object) DBNull.Value;
    }

    public static object GetReadingTableRHObject(object tableCell)
    {
      if (tableCell == DBNull.Value)
        return (object) DBNull.Value;
      if (tableCell is Decimal num)
        return (object) (num * ScaleConstants.RH_FACTOR);
      Decimal result;
      return Decimal.TryParse(tableCell.ToString(), out result) ? (object) (result * ScaleConstants.RH_FACTOR) : (object) DBNull.Value;
    }

    public static int ConvertFlowRate(object input)
    {
      if (input is int input1)
        return Utils.ConvertFlowRate(input1);
      int result;
      if (int.TryParse(input.ToString(), out result))
        return Utils.ConvertFlowRate(result);
      if (input == null || input == DBNull.Value)
        return 0;
      throw new ArgumentException("Cannot parse flow rate");
    }

    public static int ConvertFlowRate(int input) => input * 2;
  }
}
