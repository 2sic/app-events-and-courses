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

  }
}
