// Decompiled with JetBrains decompiler
// Type: GreenCo.Global
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using GreenCo.Helpers;
using Hangfire;
using Hangfire.Common;
using System;
using System.Configuration;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

#nullable disable
namespace GreenCo
{
  public class Global : HttpApplication
  {
    protected void Application_Start(object sender, EventArgs e)
    {
      HangfireBootstrapper.Instance.Start();
      this.ConfirmJobRunning();
    }

    protected void Session_Start(object sender, EventArgs e)
    {
    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
    }

    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {
    }

    protected void Application_Error(object sender, EventArgs e)
    {
    }

    protected void Session_End(object sender, EventArgs e)
    {
    }

    protected void Application_End(object sender, EventArgs e)
    {
      HangfireBootstrapper.Instance.Stop();
    }

    private void ConfirmJobRunning()
    {
      int result = 1;
      if (ConfigurationManager.AppSettings["RefreshInterval"] != null)
        int.TryParse(ConfigurationManager.AppSettings["RefreshInterval"], out result);
      int interval = Math.Max(Math.Min(result, 60), 1);
      RecurringJobManager manager = new RecurringJobManager();
      manager.RemoveIfExists("vlinkconstantupdate");
      manager.RemoveIfExists("vlinkdailyupdate");
      // ISSUE: method reference
      manager.AddOrUpdate("vlinkconstantupdate", Job.FromExpression(Expression.Lambda<Action>((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Global.VLinkConstantUpdate)), Array.Empty<Expression>()))), Cron.MinuteInterval(interval));
      // ISSUE: method reference
      manager.AddOrUpdate("vlinkdailyupdate", Job.FromExpression(Expression.Lambda<Action>((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Global.VLinkDailyUpdate)), Array.Empty<Expression>()))), Cron.Daily());
    }

    public static void VLinkConstantUpdate()
    {
      try
      {
        DateTime now = DateTime.Now;
        Utils.UpdateAllReadings(now.AddMinutes(-2.0), now, false, false);
        if (ConfigurationManager.AppSettings["debug"] == null || !(ConfigurationManager.AppSettings["debug"].ToString().ToLower() == "true"))
          return;
        Utils.Log("Constant Update performed, elapsed time " + (DateTime.Now - now).TotalMilliseconds.ToString() + " ms");
      }
      catch (Exception ex)
      {
        Utils.Log("VlinkConstantUpdate: " + ex.Message);
      }
    }

    public static void VLinkDailyUpdate()
    {
      try
      {
        DateTime now = DateTime.Now;
        Utils.UpdateAllReadings(now.AddHours(-25.0), now, true, true);
        Utils.Log("Daily Update performed, elapsed time " + (DateTime.Now - now).TotalMilliseconds.ToString() + " ms");
      }
      catch (Exception ex)
      {
        Utils.Log("VlinkDailyUpdate Exception: " + ex.Message);
      }
    }
  }
}
