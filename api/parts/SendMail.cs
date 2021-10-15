using System;
using System.Collections.Generic;
using DotNetNuke.Services.Mail;
using ToSic.Sxc.Services; // platformLogService, mailService

public class SendMail : Custom.Hybrid.Code12
{
  public void sendMails(Dictionary<string,object> contactFormRequest) {
    var settings = new {
      MailFrom = Settings.MailFrom,
      OwnerMail = Settings.OwnerMail,
      OwnerMailCC = "",
      OwnerMailTemplateFile = Settings.OwnerMailTemplateFile,
      CustomerMailCC = "",
      CustomerMailTemplateFile = Settings.CustomerMailTemplateFile
    };

    var customerMail = contactFormRequest["Mail"].ToString();

    try {
      Send(
        settings.OwnerMailTemplateFile, contactFormRequest, settings.MailFrom, settings.OwnerMail, settings.OwnerMailCC, customerMail
      );
    } catch(Exception ex) {
      throw new Exception("OwnerSend mail failed: " + ex.Message);
    }

    try {
      Send(
        settings.CustomerMailTemplateFile, contactFormRequest, settings.MailFrom, customerMail, Content.CustomerMailCC, settings.OwnerMail
      );
    } catch(Exception ex) {
      throw new Exception("OwnerSend mail failed: " + ex.Message);
    }
  }

  public bool Send(
    string emailTemplateFilename,
    Dictionary<string,object> contactFormRequest,
    string from,
    string to,
    string cc,
    string reply)
  {
    var mailEngine = CreateInstance("../../live/email-templates/" + emailTemplateFilename);
    var subject = mailEngine.Subject();
    var mailBody = mailEngine.Message(contactFormRequest).ToString();

    // Send Mail
    // uses the DNN command: http://www.dnnsoftware.com/dnn-api/html/886d0ac8-45e8-6472-455a-a7adced60ada.htm
    // var sendMailResult = Mail.SendMail(from, to,	cc,	"", reply, MailPriority.Normal,
    //   subject, MailFormat.Html, System.Text.Encoding.UTF8, mailBody, new string[0], "", "", "", "", false);

    // Send Mail
    // Note that if an error occurs, this will bubble up, the caller will convert it to format for the client
    // Log.Add("sending...");

    var mailService = GetService<IMailService>();
    mailService.Send(from: from, to: to, cc: cc, replyTo: replyTo, subject: subject, body: mailBody);

    // Log to DNN - just as a last resort in case something is lost, to track down why
    var logInfo = new DotNetNuke.Services.Log.EventLog.LogInfo
    {
        LogTypeKey = DotNetNuke.Services.Log.EventLog.EventLogController.EventLogType.ADMIN_ALERT.ToString()
    };
    logInfo.AddProperty("MailFrom", from);
    logInfo.AddProperty("MailTo", to);
    logInfo.AddProperty("MailCC", cc);
    logInfo.AddProperty("MailReply", reply);
    logInfo.AddProperty("MailSubject", subject);
    // logInfo.AddProperty("SSL", DotNetNuke.Entities.Host.Host.EnableSMTPSSL.ToString());
    // logInfo.AddProperty("Result", sendMailResult);
    // DotNetNuke.Services.Log.EventLog.EventLogController.Instance.AddLog(logInfo);

    // return sendMailResult == "";
    return true;
  }
}