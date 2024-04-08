using System.Collections.Generic;
using AppCode.Mail;
using AppCode.Data;
namespace AppCode.MailTemplates
{
  public class EmailToCustomer : Custom.Hybrid.CodeTyped, IMailTemplate
  {
    // This generates the e-mail subject
    public string Subject()
    {
      var appRes = As<AppResources>(App.Resources);

       return Kit.Scrub.Only(appRes.MailOwnerSubject, "p");
    }

    // This generates the e-mail body
    public string Message(Dictionary<string, object> request)
    {
      var appRes = As<AppResources>(App.Resources);
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
      <body>" + appRes.MailCustomerBody +
        @"</body>
    </html>";
    }
  }
}
