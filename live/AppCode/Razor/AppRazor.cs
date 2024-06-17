using AppCode.Data;
using ToSic.Razor.Blade;
using ToSic.Sxc.Data;

namespace AppCode.Razor
{
  public abstract partial class AppRazor<TModel>
  {
    /// <summary>
    /// Show Title
    /// </summary>
    public string Title(Event item, EventDate eventDate)
    {
      return item.String("Title") + (eventDate != null && Text.Has(eventDate.String("TitleAddition")) ? " " + App.Resources.String("TitleAdditionPrefix") + " " + eventDate.String("TitleAddition") + " " + App.Resources.String("TitleAdditionSuffix") : "");
    }

    /// <summary>
    /// Shows Event Details boxes
    /// </summary>
    public IHtmlTag DetailsBox(string label, string copy, string itemprop = "")
    {
      if (!Text.Has(copy)) return null;
      var copyText = Tag.Span(copy);

      if(itemprop == "location") copyText.Attr("itemprop", "address").Attr("itemscope", "").Attr("itemtype", "https://schema.org/PostalAddress");
      if(itemprop == "performer" || itemprop == "organizer") copyText.Attr("itemprop", "name");
      if(itemprop == "offers") copyText.Attr("itemprop", "price");

      var tag = Tag.Div(Tag.H6(label), copyText).Class("col-12 col-md-6 mb-3 app-events6-infocontainer");
      
      if(itemprop != "") tag.Attr("itemprop", itemprop);

      if(itemprop == "location") tag.Attr("itemscope", "").Attr("itemtype", "https://schema.org/Place");
      if(itemprop == "performer") tag.Attr("itemscope", "").Attr("itemtype", "https://schema.org/PerformingGroup");
      if(itemprop == "offers") tag.Attr("itemscope", "").Attr("itemtype", "https://schema.org/Offer");

      return tag;
    }

    /// <summary>
    /// Shows a back to list button
    /// </summary>
    public IHtmlTag BackToListButton()
    {
      // TODO: @2dg - add Koi to csproj file
      var classes = Kit.Css.Is("bs3") ? "btn-default" : "btn-outline-primary";
      return Tag.A(App.Resources.String("LabelBackToList")).Class("btn " + classes).Href(Link.To());
    }
  }
}
