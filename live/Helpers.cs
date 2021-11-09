using ToSic.Razor.Blade;
using System.Linq;
using System;

public class Helpers: Custom.Hybrid.Code12
{
  // Show Title
  public string Title(dynamic item, dynamic eventDate) {
    // TODO: @2hm - this variable doesn't do much, don't use it
    var resources = Resources;
    return item.Title + (Text.Has(eventDate.TitleAddition) ? resources.TitleAdditionPrefix + " " + eventDate.TitleAddition + " " + resources.TitleAdditionSuffix : "");
  }

  // Shows Event Details boxes
  public dynamic DetailsBox(string label, string copy) {
    if(!Text.Has(copy)) {
      return null;
    }
    return Tag.Div(copy, Tag.H6(label)).Class("ol-12 col-md-6 mb-3 app-events6-infocontainer");
  }

  // Shows a back to list button
  public dynamic BackToListButton() {
    return Tag.A(Resources.LabelBackToList).Class("btn btn-outline-primary").Href(Link.To());
  }
}