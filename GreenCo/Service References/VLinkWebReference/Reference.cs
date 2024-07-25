// Decompiled with JetBrains decompiler
// Type: GreenCo.VLinkWebReference.GetPacketsRequest
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

#nullable disable
namespace GreenCo.VLinkWebReference
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  [MessageContract(WrapperName = "GetPackets", WrapperNamespace = "http://vmcnet.com/", IsWrapped = true)]
  public class GetPacketsRequest
  {
    [MessageBodyMember(Namespace = "http://vmcnet.com/", Order = 0)]
    public int unitid;
    [MessageBodyMember(Namespace = "http://vmcnet.com/", Order = 1)]
    [XmlElement(IsNullable = true)]
    public DateTime? start;
    [MessageBodyMember(Namespace = "http://vmcnet.com/", Order = 2)]
    [XmlElement(IsNullable = true)]
    public DateTime? end;

    public GetPacketsRequest()
    {
    }

    public GetPacketsRequest(int unitid, DateTime? start, DateTime? end)
    {
      this.unitid = unitid;
      this.start = start;
      this.end = end;
    }
  }
}
