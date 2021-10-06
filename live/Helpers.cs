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
  }

  public dynamic Label(string label, string forControl) {
    return Tag.Label(label).For(forControl);
  }

  public dynamic Textbox(string label, string id, bool required = false, string type = "text", string value = "", bool disabled = false) {
    return Tag.Div(Label(label, id), Tag.Div(Tag.Input()));
  }

  // generate a paging-link number
  public string LinkToPageNumber(int pageNumber, string eventP){
    string url = (@eventP + "/page/" + pageNumber);
    return url.ToLower();
  }

}