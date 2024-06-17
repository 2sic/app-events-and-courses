// DO NOT MODIFY THIS FILE - IT IS AUTO-GENERATED
// See also: https://go.2sxc.org/copilot-data
// To extend it, create a "Event.cs" with this contents:
/*
namespace AppCode.Data
{
  public partial class Event
  {
    // Add your own properties and methods here
  }
}
*/

// Generator:   CSharpDataModelsGenerator v17.99.00
// App/Edition: Events, Courses and Registration/staging
// User:        2sic Web-Developer
// When:        2024-06-17 14:29:51Z
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ToSic.Sxc.Adam;
using ToSic.Sxc.Data;

namespace AppCode.Data
{
  // This is a generated class for Event 
  // To extend/modify it, see instructions above.

  /// <summary>
  /// Event data. <br/>
  /// Generated 2024-06-17 14:29:51Z. Re-generate whenever you change the ContentType. <br/>
  /// <br/>
  /// Default properties such as `.Title` or `.Id` are provided in the base class. <br/>
  /// Most properties have a simple access, such as `.BodyContentBlocks`. <br/>
  /// For other properties or uses, use methods such as
  /// .IsNotEmpty("FieldName"), .String("FieldName"), .Children(...), .Picture(...), .Html(...).
  /// </summary>
  public partial class Event: AutoGenerated.ZAutoGenEvent
  {  }
}

namespace AppCode.Data.AutoGenerated
{
  /// <summary>
  /// Auto-Generated base class for Default.Event in separate namespace and special name to avoid accidental use.
  /// </summary>
  public abstract class ZAutoGenEvent: Custom.Data.CustomItem
  {
    /// <summary>
    /// BodyContentBlocks as single item of ITypedItem.
    /// </summary>
    /// <remarks>
    /// Generated to only return 1 child because field settings had Multi-Value=false. 
    /// </remarks>
    /// <returns>
    /// A single item OR null if nothing found, so you can use ?? to provide alternate objects.
    /// </returns>
    public ITypedItem BodyContentBlocks => _bodyContentBlocks ??= _item.Child("BodyContentBlocks");
    private ITypedItem _bodyContentBlocks;

    /// <summary>
    /// Category as single item of Categories.
    /// </summary>
    /// <remarks>
    /// Generated to only return 1 child because field settings had Multi-Value=false. The type Categories was specified in the field settings.
    /// </remarks>
    /// <returns>
    /// A single item OR null if nothing found, so you can use ?? to provide alternate objects.
    /// </returns>
    public Categories Category => _category ??= _item.Child<Categories>("Category");
    private Categories _category;

    /// <summary>
    /// Description as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("Description", scrubHtml: true) etc.
    /// </summary>
    public string Description => _item.String("Description", fallback: "");

    /// <summary>
    /// Document as link (url). <br/>
    /// To get the underlying value like 'file:72' use String("Document")
    /// </summary>
    public string Document => _item.Url("Document");

    /// <summary>
    /// Get the file object for Document - or null if it's empty or not referencing a file.
    /// </summary>

    [JsonIgnore]
    public IFile DocumentFile => _item.File("Document");

    /// <summary>
    /// Get the folder object for Document.
    /// </summary>

    [JsonIgnore]
    public IFolder DocumentFolder => _item.Folder("Document");

    /// <summary>
    /// EventId as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("EventId", scrubHtml: true) etc.
    /// </summary>
    public string EventId => _item.String("EventId", fallback: "");

    /// <summary>
    /// Fee as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("Fee", scrubHtml: true) etc.
    /// </summary>
    public string Fee => _item.String("Fee", fallback: "");

    /// <summary>
    /// Image as link (url). <br/>
    /// To get the underlying value like 'file:72' use String("Image")
    /// </summary>
    public string Image => _item.Url("Image");

    /// <summary>
    /// Get the file object for Image - or null if it's empty or not referencing a file.
    /// </summary>

    [JsonIgnore]
    public IFile ImageFile => _item.File("Image");

    /// <summary>
    /// Get the folder object for Image.
    /// </summary>

    [JsonIgnore]
    public IFolder ImageFolder => _item.Folder("Image");

    /// <summary>
    /// Location as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("Location", scrubHtml: true) etc.
    /// </summary>
    public string Location => _item.String("Location", fallback: "");

    /// <summary>
    /// Person as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("Person", scrubHtml: true) etc.
    /// </summary>
    public string Person => _item.String("Person", fallback: "");

    /// <summary>
    /// ShortDescription as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("ShortDescription", scrubHtml: true) etc.
    /// </summary>
    public string ShortDescription => _item.String("ShortDescription", fallback: "");

    /// <summary>
    /// ShowInLanguage as bool. <br/>
    /// To get nullable use .Get("ShowInLanguage") as bool?;
    /// </summary>
    public bool ShowInLanguage => _item.Bool("ShowInLanguage");

    /// <summary>
    /// Title as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("Title", scrubHtml: true) etc.
    /// </summary>
    /// <remarks>
    /// This hides base property Title.
    /// To access original, convert using AsItem(...) or cast to ITypedItem.
    /// Consider renaming this field in the underlying content-type.
    /// </remarks>
    public new string Title => _item.String("Title", fallback: "");

    /// <summary>
    /// UrlKey as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("UrlKey", scrubHtml: true) etc.
    /// </summary>
    public string UrlKey => _item.String("UrlKey", fallback: "");
  }
}