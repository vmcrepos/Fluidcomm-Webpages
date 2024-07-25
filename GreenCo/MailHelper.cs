// Decompiled with JetBrains decompiler
// Type: GreenCo.MailHelper
// Assembly: GreenCo, Version=0.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F63B31C9-6FC1-4AED-A804-2C1309A8A158
// Assembly location: C:\Users\gregg\Desktop\Fluidcomm-Webpages\bin\GreenCo.dll

using System.Net.Mail;

#nullable disable
namespace GreenCo
{
  public static class MailHelper
  {
    public static void SendMailMessage(
      string from,
      string to,
      string bcc,
      string cc,
      string subject,
      string body)
    {
      MailMessage message = new MailMessage();
      message.From = new MailAddress(from);
      message.To.Add(new MailAddress(to));
      if (bcc != null && bcc != string.Empty)
        message.Bcc.Add(new MailAddress(bcc));
      if (cc != null && cc != string.Empty)
        message.CC.Add(new MailAddress(cc));
      message.Subject = subject;
      message.Body = body;
      message.IsBodyHtml = true;
      message.Priority = MailPriority.Normal;
      new SmtpClient().Send(message);
    }
  }
}
