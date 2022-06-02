using ToSic.Razor.Blade;
using System.Linq;
using System;

public class DetailsHelpers: Custom.Hybrid.Code12
{
  // The PageCss - cached for re-use
  internal dynamic PageCss { get { return _pageCss ?? (_pageCss = GetService<Connect.Koi.ICss>()); } }
  private dynamic _pageCss;

  // Show Title
  public string Title(dynamic item, dynamic eventDate) {
    return item.Title + (eventDate != null && Text.Has(eventDate.TitleAddition) ? " " + Resources.TitleAdditionPrefix + " " + eventDate.TitleAddition + " " + Resources.TitleAdditionSuffix : "");
  }

  // Shows Event Details boxes
  public dynamic DetailsBox(string label, string copy) {
    if(!Text.Has(copy)) {
      return null;
    }
    return Tag.Div(Tag.H6(label), copy).Class("col-12 col-md-6 mb-3 app-events6-infocontainer");
  }

  // Shows a back to list button
  public dynamic BackToListButton() {
    var classes = PageCss.Is("bs3") ? "btn-default" : "btn-outline-primary";
    return Tag.A(Resources.LabelBackToList).Class("btn " + classes).Href(Link.To());
  }
}