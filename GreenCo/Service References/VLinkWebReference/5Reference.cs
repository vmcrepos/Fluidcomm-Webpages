// Decompiled with JetBrains decompiler
// Type: GreenCo.VLinkWebReference.GetAlarmsResponse
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

#nullable disable
namespace GreenCo.VLinkWebReference
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  [MessageContract(WrapperName = "GetAlarmsResponse", WrapperNamespace = "http://vmcnet.com/", IsWrapped = true)]
  public class GetAlarmsResponse
  {
    [MessageBodyMember(Namespace = "http://vmcnet.com/", Order = 0)]
    public string GetAlarmsResult;

    public GetAlarmsResponse()
    {
    }

    public GetAlarmsResponse(string GetAlarmsResult) => this.GetAlarmsResult = GetAlarmsResult;
  }
}
