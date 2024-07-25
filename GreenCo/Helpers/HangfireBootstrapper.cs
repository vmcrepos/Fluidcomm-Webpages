// Decompiled with JetBrains decompiler
// Type: GreenCo.Helpers.HangfireBootstrapper
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using Hangfire;
using System.Web.Hosting;

#nullable disable
namespace GreenCo.Helpers
{
  public class HangfireBootstrapper : IRegisteredObject
  {
    public static readonly HangfireBootstrapper Instance = new HangfireBootstrapper();
    private readonly object _lockObject = new object();
    private bool _started;
    private BackgroundJobServer _backgroundJobServer;

    private HangfireBootstrapper()
    {
    }

    public void Start()
    {
      lock (this._lockObject)
      {
        if (this._started)
          return;
        this._started = true;
        HostingEnvironment.RegisterObject((IRegisteredObject) this);
        GlobalConfiguration.Configuration.UseSqlServerStorage("LocalSqlServer");
        this._backgroundJobServer = new BackgroundJobServer();
      }
    }

    public void Stop()
    {
      lock (this._lockObject)
      {
        if (this._backgroundJobServer != null)
          this._backgroundJobServer.Dispose();
        HostingEnvironment.UnregisterObject((IRegisteredObject) this);
      }
    }

    void IRegisteredObject.Stop(bool immediate) => this.Stop();
  }
}
