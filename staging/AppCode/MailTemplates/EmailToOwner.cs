using System.Collections.Generic;
using System.Web;
using AppCode.Mail;

namespace AppCode.MailTemplates
{
  public class EmailToOwner : AppCode.Services.ServiceBase, IMailTemplate
  {
    // This generates the e-mail subject
    public string Subject() => App.Resources.String("MailOwnerSubject", scrubHtml: "p");

    // This generates the e-mail body
    public string Message(Dictionary<string, object> request)
    {
      var message =
      @"<!doctype html>
    <html>
      <head>
        <meta name='viewport' content='width=device-width'>
        <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
        <style type='text/css'>
            body { font-family: Helvetica, sans-serif; }
        </style>
      </head>
      <body>" + App.Resources.MailOwnerIntroduction;

      foreach (var item in request)
      {
        message += "<div><strong>" + item.Key + "</strong>: " + HttpUtility.HtmlEncode(item.Value) + "</div>";
      }

      message +=
        @"</body>
    </html>";

      return message;
    }
  }
}
