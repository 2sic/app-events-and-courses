using ToSic.Razor.Blade;
using System.Linq;
using System;
using ToSic.Sxc.Data;

public class DetailsHelpers: Custom.Hybrid.CodeTyped
{
  // Show Title
  public string Title(ITypedItem item, ITypedItem eventDate) {
    return item.String("Title") + (eventDate != null && Text.Has(eventDate.String("TitleAddition")) ? " " + App.Resources.String("TitleAdditionPrefix") + " " + eventDate.String("TitleAddition") + " " + App.Resources.String("TitleAdditionSuffix") : "");
  }

  // Shows Event Details boxes
  public IHtmlTag DetailsBox(string label, string copy) {
    if (!Text.Has(copy)) return null;
    return Tag.Div(Tag.H6(label), copy).Class("col-12 col-md-6 mb-3 app-events6-infocontainer");
  }

  // Shows a back to list button
  public IHtmlTag BackToListButton() {
    var classes = Kit.Css.Is("bs3") ? "btn-default" : "btn-outline-primary";
    return Tag.A(App.Resources.String("LabelBackToList")).Class("btn " + classes).Href(Link.To());
  }
}