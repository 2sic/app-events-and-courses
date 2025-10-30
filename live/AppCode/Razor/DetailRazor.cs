using ToSic.Razor.Blade;
using AppCode.Data;
using System.Collections.Generic;
using System.Linq;

namespace AppCode.Razor
{
  /// <summary>
  /// Base Class for Razor Views which have a typed App but don't use the Model or use the typed MyModel.
  /// </summary>
  public abstract class DetailRazor : AppRazor
  {
    /// <summary>
    /// Get JsonLd for a EventPosting
    /// </summary>
    public object GetJsonLdForEventDetail(EventDate eventDate)
    {
      var AppSet = App.Settings;
      var AppRes = App.Resources;

      var jsonDescription = Text.First(eventDate.Event.ShortDescription, eventDate.Event.Description);

      return new Dictionary<string, object> {
        { "@context", "https://schema.org"},
        { "@type", "Event" },
        { "name", eventDate.Event.Title },
        { "startDate", eventDate.Start.ToString("s") },
        { "endDate", eventDate.End.ToString("s") },
        { "eventAttendanceMode", "https://schema.org/OfflineEventAttendanceMode" },
        { "eventStatus", eventDate.IsFullyBooked ? "https://schema.org/EventPostponed" : eventDate.IsCancelled ? "https://schema.org/EventCancelled" : "https://schema.org/EventScheduled"},
        { "location", new Dictionary<string, object> {
          { "@type", "Place" },
          // { "name", "" },
          { "address", new Dictionary<string, object> {
            { "@type", "PostalAddress" },
            { "streetAddress", eventDate.Location },
          }}
        }}, 
        { "image", eventDate.Event.Image},
        { "description", jsonDescription},
        { "offers", new Dictionary<string, object> {
            { "@type", "Offer" },
            { "url", MyContext.Site.Url + "?" + MyPage.Parameters },
            // { "price", eventDate.Fee },
            // { "priceCurrency", "" },
            // { "availability", "" },
            { "validFrom", eventDate.Start.ToString("s") },
        }},        
        { "performer", new Dictionary<string, object> {
            { "@type", "PerformingGroup" },
            { "name", eventDate.Person },
        }},
        { "organizer", new Dictionary<string, object> {
            { "@type", "Organization" },
            { "name", AppSet.Organization },
            { "url", MyContext.Site.Url },
        }},
      };
    }

    public void AddMetaTags(Event item, string title, string titleSuffix)
    {

      var metaImageUrl = item.IsNotEmpty("Image")
          ? Link.Image(item.Image, type: "full")
          : "";

      // Try to replace the term "PostTitle" in the page title with the item title, otherwise prefix the existing title
      Kit.Page.SetTitle(Text.Has(item.MetaTitle) ? item.MetaTitle + titleSuffix + " " : title);

      Kit.Page.SetDescription(Text.First(item.MetaDescription, item.ShortDescription));

      // Add open graph meta information
      var itemTitle = item.Title;
      Kit.Page.AddOpenGraph("og:type", "article");
      Kit.Page.AddOpenGraph("og:title", itemTitle);
      Kit.Page.AddOpenGraph("og:url", Link.To(parameters: "details=" + item.UrlKey));
      Kit.Page.AddOpenGraph("og:description", item.ShortDescription);
      Kit.Page.AddOpenGraph("og:image", metaImageUrl);
      Kit.Page.AddOpenGraph("og:image:height", "1200");
      Kit.Page.AddOpenGraph("og:image:width", "630");

      // // Add twitter meta information
      // Kit.Page.AddMeta("twitter:card", "summary_large_image");
      // Kit.Page.AddMeta("twitter:title", title);
      // Kit.Page.AddMeta("twitter:description", item.ShortDescription);
      // Kit.Page.AddMeta("twitter:image", metaImageUrl);
    }    

  }
}
