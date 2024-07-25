// Decompiled with JetBrains decompiler
// Type: GreenCo.Startup1
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using Hangfire;
using Hangfire.Dashboard;
using Owin;
using System.Collections.Generic;

#nullable disable
namespace GreenCo
{
  public class Startup1
  {
    public void Configuration(IAppBuilder app)
    {
      DashboardOptions options = new DashboardOptions()
      {
        Authorization = (IEnumerable<IDashboardAuthorizationFilter>) new LocalRequestsOnlyAuthorizationFilter[1]
        {
          new LocalRequestsOnlyAuthorizationFilter()
        }
      };
      app.UseHangfireDashboard("/hangfire", options);
    }
  }
}
