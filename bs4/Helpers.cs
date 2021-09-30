using ToSic.Razor.Blade;
using System.Linq;
using System;

public class Helpers: Custom.Hybrid.Code12
{
  // Show Title
  public string Title(dynamic item, dynamic eventDate) {
    var resources = Resources;
    return item.Title + Text.Has(eventDate.TitleAddition) ? resources.TitleAdditionPrefix + " " + eventDate.TitleAddition + " " + resources.TitleAdditionSuffix : "";
  }

  // Shows Event Details boxes
  public dynamic DetailsBox(string label, string copy) {
    if(Text.Has(copy)) {
      return Tag.Div(copy, Tag.H6(label)).Class("ol-12 col-md-6 mb-3 app-events6-infocontainer");
    }
  }

  // Shows a back to list button
  public dynamic BackToListButton() {
    // <a class="btn btn-outline-primary" href='@Tags.SafeUrl(Link.To())'>@Html.Raw(App.Resources.LabelBackToList)</a>
  }

//   @* This generates the e-mail subject *@
// @functions {
//   public string Subject() {
//     return Resources.MailCustomerSubject;
//   }
// }

// @* This generates the e-mail body *@
// @helper Message(Dictionary<string,object> request)
// {
// <!doctype html>
// <html>
//   <head>
//     <meta name="viewport" content="width=device-width">
//     <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
//     <style type="text/css">
//       body { font-family: Helvetica, sans-serif; }
//     </style>
//   </head>
//   <body>
//     @(new HtmlString(Resources.MailCustomerBody))
//   </body>
// </html>
// }

// @* This generates the e-mail subject *@
// @functions {
//   public string Subject() {
//     return App.Resources.MailOwnerSubject;
//   }
// }

// @* This generates the e-mail body *@
// @helper Message(Dictionary<string,object> request)
// {
// <!doctype html>
// <html>
//   <head>
//     <meta name="viewport" content="width=device-width">
//     <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
//     <style type="text/css">
//         body { font-family: Helvetica, sans-serif; }
//     </style>
//   </head>
//   <body>
//     @(new HtmlString(App.Resources.MailOwnerIntroduction))
//     @foreach (var item in request) {
//         <div><strong>@item.Key</strong>: @item.Value</div>
//     }
//   </body>
// </html>
// }


// @helper Label(string label, string forControl) {
//   <label class="col-sm-3" for="@forControl">@label</label>
// }

// @helper Textbox(string label, string id, bool required = false, string type = "text", string value = "", bool disabled = false) {
//   <div class="form-group row @(required ? "form-required" : "")">
//     @Label(label, id)
//     <div class="col-sm-9">
//         <input value="@value" type="@type" @((required) ? "required" : "") class="form-control" id="@id" name="@id" @((disabled) ? "disabled" : "")>
//     </div>
//   </div>
// }

// @{
//   var paging = AsList(Data["Paging"]).FirstOrDefault();
//   var eventPage = PageData["eventPage"];
// }

// <div class="app-events-paging mt-4">
//   @if (!(paging.PageNumber <= 1)) {
//     <a class="app-events-prev-pager" href="@LinkToPageNumber((int)(paging.PageNumber) - 1, eventPage)">&lsaquo;</a>
//   }
//   @if (@paging.PageSize >= 0) {
//     for(int i = 1; i <= @paging.PageCount; i++) {
//       if (i == paging.PageNumber) {
//         <a class="app-events-paging-active">@i</a>
//       } else {
//         <a href="@LinkToPageNumber(i, eventPage)">@i</a>
//       }
//     }
//   }
//   @if (!(paging.PageNumber >= paging.PageCount)) {
//     <a class="app-events6-next-pager" href="@LinkToPageNumber((int)(paging.PageNumber) + 1, eventPage)">&rsaquo;</a>
//   }
// </div>

// @functions {
//   // generate a paging-link number
//   public string LinkToPageNumber(int pageNumber, String eventP){
//     string url = (@eventP + "/page/" + pageNumber);
//     return url.ToLower();
//   }
// }

}