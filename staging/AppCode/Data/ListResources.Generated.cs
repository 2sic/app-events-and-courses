// DO NOT MODIFY THIS FILE - IT IS AUTO-GENERATED
// See also: https://go.2sxc.org/copilot-data
// To extend it, create a "ListResources.cs" with this contents:
/*
namespace AppCode.Data
{
  public partial class ListResources
  {
    // Add your own properties and methods here
  }
}
*/

// Generator:   CSharpDataModelsGenerator v17.07.00
// App/Edition: Events, Courses and Registration/staging
// User:        2sic Web-Developer
// When:        2024-05-02 13:03:09Z
namespace AppCode.Data
{
  // This is a generated class for ListResources (scope: System.Configuration)
  // To extend/modify it, see instructions above.

  /// <summary>
  /// ListResources data. <br/>
  /// Generated 2024-05-02 13:03:09Z. Re-generate whenever you change the ContentType. <br/>
  /// <br/>
  /// Default properties such as `.Title` or `.Id` are provided in the base class. <br/>
  /// Most properties have a simple access, such as `.Admin`. <br/>
  /// For other properties or uses, use methods such as
  /// .IsNotEmpty("FieldName"), .String("FieldName"), .Children(...), .Picture(...), .Html(...).
  /// </summary>
  /// <remarks>
  /// This Content-Type is NOT in the default scope, so you may not see it in the Admin UI. It's in the scope System.Configuration.
  /// </remarks>
  public partial class ListResources: AutoGenerated.ZAutoGenListResources
  {  }
}

namespace AppCode.Data.AutoGenerated
{
  /// <summary>
  /// Auto-Generated base class for System.Configuration.ListResources in separate namespace and special name to avoid accidental use.
  /// </summary>
  public abstract class ZAutoGenListResources: Custom.Data.CustomItem
  {
    /// <summary>
    /// Admin as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("Admin", scrubHtml: true) etc.
    /// </summary>
    public string Admin => _item.String("Admin", fallback: "");

    /// <summary>
    /// BtnEventDates as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("BtnEventDates", scrubHtml: true) etc.
    /// </summary>
    public string BtnEventDates => _item.String("BtnEventDates", fallback: "");

    /// <summary>
    /// BtnEvents as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("BtnEvents", scrubHtml: true) etc.
    /// </summary>
    public string BtnEvents => _item.String("BtnEvents", fallback: "");

    /// <summary>
    /// BtnNewEvent as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("BtnNewEvent", scrubHtml: true) etc.
    /// </summary>
    public string BtnNewEvent => _item.String("BtnNewEvent", fallback: "");

    /// <summary>
    /// BtnNewEventDate as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("BtnNewEventDate", scrubHtml: true) etc.
    /// </summary>
    public string BtnNewEventDate => _item.String("BtnNewEventDate", fallback: "");

    /// <summary>
    /// BtnRegistrations as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("BtnRegistrations", scrubHtml: true) etc.
    /// </summary>
    public string BtnRegistrations => _item.String("BtnRegistrations", fallback: "");

    /// <summary>
    /// HeadlineEventsWithoutDate as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("HeadlineEventsWithoutDate", scrubHtml: true) etc.
    /// </summary>
    public string HeadlineEventsWithoutDate => _item.String("HeadlineEventsWithoutDate", fallback: "");
  }
}