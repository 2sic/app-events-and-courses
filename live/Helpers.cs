using ToSic.Razor.Blade;
using System.Linq;
using System;

public class Helpers: Custom.Hybrid.Code12
{
  // Show Title
  public string Title(dynamic item, dynamic eventDate) {
    var resources = Resources;
    return item.Title + (Text.Has(eventDate.TitleAddition) ? resources.TitleAdditionPrefix + " " + eventDate.TitleAddition + " " + resources.TitleAdditionSuffix : "");
  }

  // Shows Event Details boxes
  public dynamic DetailsBox(string label, string copy) {
    if(Text.Has(copy)) {
      return Tag.Div(copy, Tag.H6(label)).Class("ol-12 col-md-6 mb-3 app-events6-infocontainer");
    }
    return null;
  }

  // Shows a back to list button
  public dynamic BackToListButton() {
    return Tag.A(Resources.LabelBackToList).Class("btn btn-outline-primary").Href(Link.To());
    // <a class="btn btn-outline-primary" href='@Tags.SafeUrl(Link.To())'>@Html.Raw(App.Resources.LabelBackToList)</a>
  }

  public dynamic Label(string label, string forControl) {
    return Tag.Label(label).For(forControl);
    // <label class="col-sm-3" for="@forControl">@label</label>
  }

  public dynamic Textbox(string label, string id, bool required = false, string type = "text", string value = "", bool disabled = false) {
    return Tag.Div(Label(label, id), Tag.Div(Tag.Input()));
    // <div class="form-group row @(required ? "form-required" : "")">
    //   @Label(label, id)
    //   <div class="col-sm-9">
    //       <input value="@value" type="@type" @((required) ? "required" : "") class="form-control" id="@id" name="@id" @((disabled) ? "disabled" : "")>
    //   </div>
    // </div>
  }

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

  // generate a paging-link number
  public string LinkToPageNumber(int pageNumber, string eventP){
    string url = (@eventP + "/page/" + pageNumber);
    return url.ToLower();
  }

}