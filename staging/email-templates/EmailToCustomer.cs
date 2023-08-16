using System.Collections.Generic;
public class EmailToCustomer: Custom.Hybrid.CodeTyped
{
  // This generates the e-mail subject
  public string Subject() {
    return App.Resources.String("MailCustomerSubject");
  }

  // This generates the e-mail body
  public string Message(Dictionary<string,object> request)
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
      <body>" + App.Resources.String("MailCustomerBody") +
      @"</body>
    </html>";
  }
}