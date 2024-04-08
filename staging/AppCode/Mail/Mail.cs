using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using ToSic.Razor.Blade;
using AppCode.Data;

namespace AppCode.Mail
{
    public class Mail: Custom.Hybrid.CodeTyped
    {

        public void SendMails(Dictionary<string, object> contactFormRequest)
        {

            var appSettings = As<AppSettings>(App.Settings);
            var settings = new
            {
                MailFrom = appSettings.MailFrom,
                OwnerMail = appSettings.OwnerMail,
                OwnerMailCC = "",
                OwnerMailTemplateFile = appSettings.OwnerMailTemplateFile,
                CustomerMailCC = "",
                CustomerMailTemplateFile = appSettings.CustomerMailTemplateFile
            };

            var customerMail = contactFormRequest["Mail"].ToString();

            try
            {
                Send(
                  settings.OwnerMailTemplateFile, contactFormRequest, settings.MailFrom, settings.OwnerMail, settings.OwnerMailCC, customerMail
                );
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                throw new Exception("OwnerSend mail failed: " + ex.Message);
            }

            try
            {
                Send(
                  settings.CustomerMailTemplateFile, contactFormRequest, settings.MailFrom, customerMail, settings.CustomerMailCC, settings.OwnerMail
                );
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                throw new Exception("OwnerSend mail failed: " + ex.Message);
            }
        }

        public bool Send(string emailTemplateFilename, Dictionary<string, object> valuesWithMailLabels, string from, string to, string cc, string replyTo)
        {
            // Log what's happening in case we run into problems
            var wrapLog = Log.Call("template:" + emailTemplateFilename + ", from:" + from + ", to:" + to + ", cc:" + cc + ", reply:" + replyTo);

            Log.Add("Get MailEngine");
            var mailEngine = GetService<IMailTemplate>(typeName: "AppCode.MailTemplates." + emailTemplateFilename.Replace(".cs", ""));
            var mailBody = mailEngine.Message(valuesWithMailLabels).ToString();
            var subject = mailEngine.Subject();

            // Send Mail
            // Note that if an error occurs, this will bubble up, the caller will convert it to format for the client
            Log.Add("sending...");
            Kit.Mail.Send(from: from, to: to, cc: cc, replyTo: replyTo, subject: subject, body: mailBody, attachments: new List<ToSic.Sxc.Adam.IFile>());

            // Log to Platform - just as a last resort in case something is lost, to track down why
            var message = new StringBuilder()
              .AppendLine("Send Mail")
              .AppendLine("From:    " + from)
              .AppendLine("To:      " + to)
              .AppendLine("CC:      " + cc)
              .AppendLine("Reply:   " + replyTo)
              .AppendLine("Subject: " + subject)
              .ToString();


            Kit.SystemLog.Add("SendMail", message);
            wrapLog("ok");

            return true;
        }
    }
}
