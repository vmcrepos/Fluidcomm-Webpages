// Decompiled with JetBrains decompiler
// Type: GreenCo.VLinkWebReference.GetAggregatePacketsRequest
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

#nullable disable
namespace GreenCo.VLinkWebReference
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  [MessageContract(WrapperName = "GetAggregatePackets", WrapperNamespace = "http://vmcnet.com/", IsWrapped = true)]
  public class GetAggregatePacketsRequest
  {
    [MessageBodyMember(Namespace = "http://vmcnet.com/", Order = 0)]
    public DataSet unitid;
    [MessageBodyMember(Namespace = "http://vmcnet.com/", Order = 1)]
    public DataSet datatype;
    [MessageBodyMember(Namespace = "http://vmcnet.com/", Order = 2)]
    public int function;
    [MessageBodyMember(Namespace = "http://vmcnet.com/", Order = 3)]
    [XmlElement(IsNullable = true)]
    public DateTime? start;
    [MessageBodyMember(Namespace = "http://vmcnet.com/", Order = 4)]
    [XmlElement(IsNullable = true)]
    public DateTime? end;

    public GetAggregatePacketsRequest()
    {
    }

    public GetAggregatePacketsRequest(
      DataSet unitid,
      DataSet datatype,
      int function,
      DateTime? start,
      DateTime? end)
    {
      this.unitid = unitid;
      this.datatype = datatype;
      this.function = function;
      this.start = start;
      this.end = end;
    }
  }
}
