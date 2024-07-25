// Decompiled with JetBrains decompiler
// Type: GreenCo.VLinkWebReference.VLinkSoapClient
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

#nullable disable
namespace GreenCo.VLinkWebReference
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  public class VLinkSoapClient : ClientBase<VLinkSoap>, VLinkSoap
  {
    public VLinkSoapClient()
    {
    }

    public VLinkSoapClient(string endpointConfigurationName)
      : base(endpointConfigurationName)
    {
    }

    public VLinkSoapClient(string endpointConfigurationName, string remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public VLinkSoapClient(string endpointConfigurationName, EndpointAddress remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public VLinkSoapClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    public int Login(string user, string password) => this.Channel.Login(user, password);

    public int Logout() => this.Channel.Logout();

    public string GetUnits() => this.Channel.GetUnits();

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    GetPacketsResponse VLinkSoap.GetPackets(GetPacketsRequest request)
    {
      return this.Channel.GetPackets(request);
    }

    public string GetPackets(int unitid, DateTime? start, DateTime? end)
    {
      return ((VLinkSoap) this).GetPackets(new GetPacketsRequest()
      {
        unitid = unitid,
        start = start,
        end = end
      }).GetPacketsResult;
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    GetAggregatePacketsResponse VLinkSoap.GetAggregatePackets(GetAggregatePacketsRequest request)
    {
      return this.Channel.GetAggregatePackets(request);
    }

    public string GetAggregatePackets(
      DataSet unitid,
      DataSet datatype,
      int function,
      DateTime? start,
      DateTime? end)
    {
      return ((VLinkSoap) this).GetAggregatePackets(new GetAggregatePacketsRequest()
      {
        unitid = unitid,
        datatype = datatype,
        function = function,
        start = start,
        end = end
      }).GetAggregatePacketsResult;
    }

    public string GetProperty(int unitid, int propertyid)
    {
      return this.Channel.GetProperty(unitid, propertyid);
    }

    public string SetProperty(int unitid, int propertyid, string value)
    {
      return this.Channel.SetProperty(unitid, propertyid, value);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    GetAlarmsResponse VLinkSoap.GetAlarms(GetAlarmsRequest request)
    {
      return this.Channel.GetAlarms(request);
    }

    public string GetAlarms(int unitid, bool NewAlarms, DateTime? start, DateTime? end)
    {
      return ((VLinkSoap) this).GetAlarms(new GetAlarmsRequest()
      {
        unitid = unitid,
        NewAlarms = NewAlarms,
        start = start,
        end = end
      }).GetAlarmsResult;
    }

    public string AcknowledgeAlarm(int unitid, int id, string name)
    {
      return this.Channel.AcknowledgeAlarm(unitid, id, name);
    }

    public string GetAlarmConfiguration(int unitid) => this.Channel.GetAlarmConfiguration(unitid);

    public string DeleteAlarmConfiguration(int unitid, int id, int alarmtype)
    {
      return this.Channel.DeleteAlarmConfiguration(unitid, id, alarmtype);
    }

    public string ConfigureAlarm(
      int id,
      int unitid,
      int alarmtype,
      int sensorid,
      string LowLimit,
      string HighLimit,
      string OnAction,
      string OffAction,
      string Text,
      int linked)
    {
      return this.Channel.ConfigureAlarm(id, unitid, alarmtype, sensorid, LowLimit, HighLimit, OnAction, OffAction, Text, linked);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    GetErrorsResponse VLinkSoap.GetErrors(GetErrorsRequest request)
    {
      return this.Channel.GetErrors(request);
    }

    public string GetErrors(int unitid, bool NewErrors, DateTime? start, DateTime? end)
    {
      return ((VLinkSoap) this).GetErrors(new GetErrorsRequest()
      {
        unitid = unitid,
        NewErrors = NewErrors,
        start = start,
        end = end
      }).GetErrorsResult;
    }

    public string AcknowledgeError(int unitid, int id, string name)
    {
      return this.Channel.AcknowledgeError(unitid, id, name);
    }
  }
}
