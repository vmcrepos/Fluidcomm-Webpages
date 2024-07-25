// Decompiled with JetBrains decompiler
// Type: GreenCo.VLinkWebReference.VLinkSoap
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System.CodeDom.Compiler;
using System.ServiceModel;

#nullable disable
namespace GreenCo.VLinkWebReference
{
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  [ServiceContract(Namespace = "http://vmcnet.com/", ConfigurationName = "VLinkWebReference.VLinkSoap")]
  public interface VLinkSoap
  {
    [OperationContract(Action = "http://vmcnet.com/Login", ReplyAction = "*")]
    [XmlSerializerFormat(SupportFaults = true)]
    int Login(string user, string password);

    [OperationContract(Action = "http://vmcnet.com/Logout", ReplyAction = "*")]
    [XmlSerializerFormat(SupportFaults = true)]
    int Logout();

    [OperationContract(Action = "http://vmcnet.com/GetUnits", ReplyAction = "*")]
    [XmlSerializerFormat(SupportFaults = true)]
    string GetUnits();

    [OperationContract(Action = "http://vmcnet.com/GetPackets", ReplyAction = "*")]
    [XmlSerializerFormat(SupportFaults = true)]
    GetPacketsResponse GetPackets(GetPacketsRequest request);

    [OperationContract(Action = "http://vmcnet.com/GetAggregatePackets", ReplyAction = "*")]
    [XmlSerializerFormat(SupportFaults = true)]
    GetAggregatePacketsResponse GetAggregatePackets(GetAggregatePacketsRequest request);

    [OperationContract(Action = "http://vmcnet.com/GetProperty", ReplyAction = "*")]
    [XmlSerializerFormat(SupportFaults = true)]
    string GetProperty(int unitid, int propertyid);

    [OperationContract(Action = "http://vmcnet.com/SetProperty", ReplyAction = "*")]
    [XmlSerializerFormat(SupportFaults = true)]
    string SetProperty(int unitid, int propertyid, string value);

    [OperationContract(Action = "http://vmcnet.com/GetAlarms", ReplyAction = "*")]
    [XmlSerializerFormat(SupportFaults = true)]
    GetAlarmsResponse GetAlarms(GetAlarmsRequest request);

    [OperationContract(Action = "http://vmcnet.com/AcknowledgeAlarm", ReplyAction = "*")]
    [XmlSerializerFormat(SupportFaults = true)]
    string AcknowledgeAlarm(int unitid, int id, string name);

    [OperationContract(Action = "http://vmcnet.com/GetAlarmConfiguration", ReplyAction = "*")]
    [XmlSerializerFormat(SupportFaults = true)]
    string GetAlarmConfiguration(int unitid);

    [OperationContract(Action = "http://vmcnet.com/DeleteAlarmConfiguration", ReplyAction = "*")]
    [XmlSerializerFormat(SupportFaults = true)]
    string DeleteAlarmConfiguration(int unitid, int id, int alarmtype);

    [OperationContract(Action = "http://vmcnet.com/ConfigureAlarm", ReplyAction = "*")]
    [XmlSerializerFormat(SupportFaults = true)]
    string ConfigureAlarm(
      int id,
      int unitid,
      int alarmtype,
      int sensorid,
      string LowLimit,
      string HighLimit,
      string OnAction,
      string OffAction,
      string Text,
      int linked);

    [OperationContract(Action = "http://vmcnet.com/GetErrors", ReplyAction = "*")]
    [XmlSerializerFormat(SupportFaults = true)]
    GetErrorsResponse GetErrors(GetErrorsRequest request);

    [OperationContract(Action = "http://vmcnet.com/AcknowledgeError", ReplyAction = "*")]
    [XmlSerializerFormat(SupportFaults = true)]
    string AcknowledgeError(int unitid, int id, string name);
  }
}
