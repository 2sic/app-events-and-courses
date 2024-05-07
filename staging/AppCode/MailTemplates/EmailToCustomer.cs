using System.Collections.Generic;
using AppCode.Mail;
namespace AppCode.MailTemplates
{
  public class EmailToCustomer : AppCode.Services.ServiceBase, IMailTemplate
  {
    // This generates the e-mail subject
    public string Subject() => App.Resources.String("MailOwnerSubject", scrubHtml: "p");

    // This generates the e-mail body
    public string Message(Dictionary<string, object> request)
    {
      return @"
    <!doctype html>
    <html>
      <head>
        <meta name='viewport' content='width=device-width'>
        <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
        <style type='text/css'>
          body { font-family: Helvetica, sans-serif; }
        </style>
      </head>
      <body>" + App.Resources.MailCustomerBody +
        @"</body>
    </html>";
    }
  }
}
