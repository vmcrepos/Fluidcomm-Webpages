// Decompiled with JetBrains decompiler
// Type: GreenCo.Repositories.UnitUserRepo
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.Collections.Generic;
using System.Data.SqlClient;

#nullable disable
namespace GreenCo.Repositories
{
  public static class UnitUserRepo
  {
    public static void DeleteUnitUsersForUnitId(int unitId)
    {
      string cmdText = "Delete FROM UnitUser WHERE UnitId = @unitId";
      using (SqlConnection conn = Utils.GetConn())
      {
        using (SqlCommand sqlCommand = new SqlCommand(cmdText, conn))
        {
          sqlCommand.Parameters.AddWithValue("@unitId", (object) unitId);
          sqlCommand.ExecuteNonQuery();
        }
      }
    }

    public static int RemoveUsersFromUnit(int unitId, List<string> userIds)
    {
      string str = "";
      foreach (string userId in userIds)
        str = str + "DELETE FROM UnitUser Where UserId = N'" + Utils.CleanText(userId) + "' AND UnitID = " + unitId.ToString() + ";";
      string cmdText = str + " SELECT COUNT(UserId) FROM UnitUser WHERE UnitID=" + unitId.ToString();
      if (string.IsNullOrEmpty(cmdText))
        return -1;
      try
      {
        using (SqlConnection conn = Utils.GetConn())
        {
          using (SqlCommand sqlCommand = new SqlCommand(cmdText, conn))
          {
            object obj = sqlCommand.ExecuteScalar();
            return obj != null && obj != DBNull.Value && obj is int num ? num : -1;
          }
        }
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public static int AddUsersToUnit(int unitId, List<string> userIds)
    {
      string str = "";
      foreach (string userId in userIds)
        str = str + "INSERT INTO UnitUser (UserId, UnitID) VALUES (N'" + Utils.CleanText(userId) + "', " + unitId.ToString() + ") ";
      string cmdText = str + "; SELECT COUNT(UserId) FROM UnitUser WHERE UnitID=" + unitId.ToString();
      if (string.IsNullOrEmpty(cmdText))
        return -1;
      try
      {
        using (SqlConnection conn = Utils.GetConn())
        {
          using (SqlCommand sqlCommand = new SqlCommand(cmdText, conn))
          {
            object obj = sqlCommand.ExecuteScalar();
            return obj != null && obj != DBNull.Value && obj is int num ? num : 0;
          }
        }
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}
