@* This generates the e-mail subject *@
@functions {
  public string Subject() {
    return App.Resources.MailOwnerSubject;
  }
}

@* This generates the e-mail body *@
@helper Message(Dictionary<string,object> request)
{
  <!doctype html>
  <html>
    <head>
      <meta name="viewport" content="width=device-width">
      <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
      <style type="text/css">
          body { font-family: Helvetica, sans-serif; }
      </style>
    </head>
    <body>
      @(new HtmlString(App.Resources.MailOwnerIntroduction))
      @foreach (var item in request) {
          <div><strong>@item.Key</strong>: @item.Value</div>
      }
    </body>
  </html>
}