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

  public bool Send(string emailTemplateFilename, Dictionary<string,object> contactFormRequest, string from, string to, string cc, string replyTo)
  {
    var mailEngine = CreateInstance("../../live/email-templates/" + emailTemplateFilename);
    var subject = mailEngine.Subject();
    var mailBody = mailEngine.Message(contactFormRequest).ToString();

    var mailService = GetService<IMailService>();
    mailService.Send(from: from, to: to, cc: cc, replyTo: replyTo, subject: subject, body: mailBody, attachments: new List<ToSic.Sxc.Adam.IFile>());

    // Log to DNN - just as a last resort in case something is lost, to track down why
    var logInfo = new DotNetNuke.Services.Log.EventLog.LogInfo
    {
        LogTypeKey = DotNetNuke.Services.Log.EventLog.EventLogController.EventLogType.ADMIN_ALERT.ToString()
    };
    logInfo.AddProperty("MailFrom", from);
    logInfo.AddProperty("MailTo", to);
    logInfo.AddProperty("MailCC", cc);
    logInfo.AddProperty("MailReply", replyTo);
    logInfo.AddProperty("MailSubject", subject);

    return true;
  }
}