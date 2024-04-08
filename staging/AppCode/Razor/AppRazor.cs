using AppCode.Data;
using ToSic.Razor.Blade;
using ToSic.Sxc.Data;

namespace AppCode.Razor
{
  public abstract partial class AppRazor<TModel> : Custom.Hybrid.RazorTyped<TModel>
  {
    /// <summary>
    /// Show Title
    /// </summary>
    public string Title(ITypedItem item, ITypedItem eventDate)
    {
      return item.String("Title") + (eventDate != null && Text.Has(eventDate.String("TitleAddition")) ? " " + App.Resources.String("TitleAdditionPrefix") + " " + eventDate.String("TitleAddition") + " " + App.Resources.String("TitleAdditionSuffix") : "");
    }

    /// <summary>
    /// Shows Event Details boxes
    /// </summary>
    public IHtmlTag DetailsBox(string label, string copy)
    {
      if (!Text.Has(copy)) return null;
      return Tag.Div(Tag.H6(label), copy).Class("col-12 col-md-6 mb-3 app-events6-infocontainer");
    }

    /// <summary>
    /// Shows a back to list button
    /// </summary>
    public IHtmlTag BackToListButton()
    {
      var classes = Kit.Css.Is("bs3") ? "btn-default" : "btn-outline-primary";
      return Tag.A(App.Resources.String("LabelBackToList")).Class("btn " + classes).Href(Link.To());
    }
  }
}
