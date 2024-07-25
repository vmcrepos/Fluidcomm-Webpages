// Decompiled with JetBrains decompiler
// Type: GreenCo.VLinkWebReference.VLinkSoapChannel
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using System.ServiceModel.Channels;

#nullable disable
namespace GreenCo.VLinkWebReference
{
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  public interface VLinkSoapChannel : 
    VLinkSoap,
    IClientChannel,
    IContextChannel,
    IChannel,
    ICommunicationObject,
    IExtensibleObject<IContextChannel>,
    IDisposable
  {
  }
}
