// Decompiled with JetBrains decompiler
// Type: GreenCo.VLinkUtility
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using GreenCo.VLinkWebReference;
using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;

#nullable disable
namespace GreenCo
{
  public static class VLinkUtility
  {
    private static readonly string PasswordHash = "SaLmoNArms";
    private static readonly string SaltKey = "t27nw@xp";
    private static readonly string VIKey = "^dC2)wH4#UsKqZ0&";
    private static VLinkSoapClient sc = new VLinkSoapClient();

    private static string Encrypt(string plainText)
    {
      byte[] bytes1 = Encoding.UTF8.GetBytes(plainText);
      byte[] bytes2 = new Rfc2898DeriveBytes(VLinkUtility.PasswordHash, Encoding.ASCII.GetBytes(VLinkUtility.SaltKey)).GetBytes(32);
      RijndaelManaged rijndaelManaged = new RijndaelManaged();
      rijndaelManaged.Mode = CipherMode.CBC;
      rijndaelManaged.Padding = PaddingMode.Zeros;
      ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(bytes2, Encoding.ASCII.GetBytes(VLinkUtility.VIKey));
      byte[] array;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, encryptor, CryptoStreamMode.Write))
        {
          cryptoStream.Write(bytes1, 0, bytes1.Length);
          cryptoStream.FlushFinalBlock();
          array = memoryStream.ToArray();
          cryptoStream.Close();
        }
      }
      return Convert.ToBase64String(array);
    }

    public static int Connect(string user, string pass)
    {
      VLinkUtility.sc = new VLinkSoapClient();
      string user1 = VLinkUtility.Encrypt(user);
      string password = VLinkUtility.Encrypt(pass);
      return VLinkUtility.sc.Login(user1, password);
    }

    public static void CheckResult(string strWorking)
    {
      switch (strWorking)
      {
        case "-1":
          throw new VlinkException("Login failed with the username/password provided");
        case "-2":
          throw new VlinkException("Login could not be established with the username/password provided");
        case "-3":
          throw new VlinkException("That unit does not exist");
        case "-4":
          throw new VlinkException("Database query failed");
        case "-5":
          throw new VlinkException("No units available for this user");
        case "-6":
          throw new VlinkException("Invalid item index");
        case "-7":
          throw new VlinkException("Invalid value");
      }
    }

    public static DataTable GetAlarms(int unitID, DateTime? start, DateTime? end, bool current)
    {
      string alarms;
      try
      {
        alarms = VLinkUtility.sc.GetAlarms(unitID, current, start, end);
        if (alarms == "-2")
        {
          VLinkUtility.Connect(Utils.AppUserName, Utils.AppPassword);
          alarms = VLinkUtility.sc.GetAlarms(unitID, false, start, end);
        }
        VLinkUtility.CheckResult(alarms);
      }
      catch (CommunicationException ex)
      {
        throw new VlinkException("A communication exception has occurred. " + ex.Message);
      }
      catch (TimeoutException ex)
      {
        throw new VlinkException("A timeout occurred. " + ex.Message);
      }
      StringReader reader = new StringReader(alarms);
      DataSet dataSet = new DataSet();
      int num = (int) dataSet.ReadXml((TextReader) reader);
      if (dataSet.Tables.Count == 0)
        dataSet.Tables.Add(new DataTable());
      return dataSet.Tables[0];
    }

    public static DataTable GetPackets(int unitID, DateTime? start, DateTime? end)
    {
      string packets;
      try
      {
        packets = VLinkUtility.sc.GetPackets(unitID, start, end);
        if (packets == "-2")
        {
          VLinkUtility.Connect(Utils.AppUserName, Utils.AppPassword);
          packets = VLinkUtility.sc.GetPackets(unitID, start, end);
        }
        VLinkUtility.CheckResult(packets);
      }
      catch (CommunicationException ex)
      {
        throw new VlinkException("A communication exception has occurred. " + ex.Message);
      }
      catch (TimeoutException ex)
      {
        throw new VlinkException("A timeout occurred. " + ex.Message);
      }
      StringReader reader = new StringReader(packets);
      DataSet dataSet = new DataSet();
      int num = (int) dataSet.ReadXml((TextReader) reader);
      if (dataSet.Tables.Count == 0)
        dataSet.Tables.Add(new DataTable());
      return dataSet.Tables[0];
    }

    public static DataTable GetAggregatePackets(
      DataSet unitID,
      DataSet dataType,
      int function,
      DateTime? start,
      DateTime? end)
    {
      string aggregatePackets;
      try
      {
        aggregatePackets = VLinkUtility.sc.GetAggregatePackets(unitID, dataType, function, start, end);
        if (aggregatePackets == "-2")
        {
          VLinkUtility.Connect(Utils.AppUserName, Utils.AppPassword);
          aggregatePackets = VLinkUtility.sc.GetAggregatePackets(unitID, dataType, function, start, end);
        }
        VLinkUtility.CheckResult(aggregatePackets);
      }
      catch (CommunicationException ex)
      {
        throw new VlinkException("A communication exception has occurred. " + ex.Message);
      }
      catch (TimeoutException ex)
      {
        throw new VlinkException("A timeout occurred. " + ex.Message);
      }
      Utils.Log("GetAggregatePackets returned string of " + aggregatePackets.Length.ToString() + " characters for unit id " + unitID?.ToString());
      StringReader reader = new StringReader(aggregatePackets);
      DataSet dataSet = new DataSet();
      int num = (int) dataSet.ReadXml((TextReader) reader);
      if (dataSet.Tables.Count == 0)
        dataSet.Tables.Add(new DataTable());
      return dataSet.Tables[0];
    }

    public static DataTable GetUnits()
    {
      string units;
      try
      {
        units = VLinkUtility.sc.GetUnits();
        if (units == "-2")
        {
          VLinkUtility.Connect(Utils.AppUserName, Utils.AppPassword);
          units = VLinkUtility.sc.GetUnits();
        }
        VLinkUtility.CheckResult(units);
      }
      catch (CommunicationException ex)
      {
        throw new VlinkException("A communication exception has occurred. " + ex.Message);
      }
      catch (TimeoutException ex)
      {
        throw new VlinkException("A timeout occurred. " + ex.Message);
      }
      StringReader reader = new StringReader(units);
      DataSet dataSet = new DataSet();
      int num = (int) dataSet.ReadXml((TextReader) reader);
      if (dataSet.Tables.Count == 0)
        dataSet.Tables.Add(new DataTable());
      return dataSet.Tables[0];
    }

    public static DataTable GetAlarmConfig() => VLinkUtility.GetAlarmConfig(-1);

    public static DataTable GetAlarmConfig(int unitId)
    {
      string alarmConfiguration;
      try
      {
        alarmConfiguration = VLinkUtility.sc.GetAlarmConfiguration(unitId);
        if (alarmConfiguration == "-2")
        {
          VLinkUtility.Connect(Utils.AppUserName, Utils.AppPassword);
          alarmConfiguration = VLinkUtility.sc.GetAlarmConfiguration(-1);
        }
        VLinkUtility.CheckResult(alarmConfiguration);
      }
      catch (CommunicationException ex)
      {
        throw new VlinkException("A communication exception has occurred. " + ex.Message);
      }
      catch (TimeoutException ex)
      {
        throw new VlinkException("A timeout occurred. " + ex.Message);
      }
      StringReader reader = new StringReader(alarmConfiguration);
      DataSet dataSet = new DataSet();
      int num = (int) dataSet.ReadXml((TextReader) reader);
      if (dataSet.Tables.Count == 0)
        dataSet.Tables.Add(new DataTable());
      return dataSet.Tables[0];
    }

    private static string GetRowValue(DataRow row, string column)
    {
      return row[column] != null && row[column] != DBNull.Value ? row[column].ToString() : (string) null;
    }

    public struct CUnit
    {
      public string UnitID;
      public string Name;
    }
  }
}
