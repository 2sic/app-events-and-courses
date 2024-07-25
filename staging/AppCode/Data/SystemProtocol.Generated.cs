// DO NOT MODIFY THIS FILE - IT IS AUTO-GENERATED
// See also: https://go.2sxc.org/copilot-data
// To extend it, create a "SystemProtocol.cs" with this contents:
/*
namespace AppCode.Data
{
  public partial class SystemProtocol
  {
    // Add your own properties and methods here
  }
}
*/

// Generator:   CSharpDataModelsGenerator v17.99.00
// App/Edition: Events, Courses and Registration/staging
// User:        2sic Web-Developer
// When:        2024-06-18 09:15:56Z
using System;

namespace AppCode.Data
{
  // This is a generated class for SystemProtocol 
  // To extend/modify it, see instructions above.

  /// <summary>
  /// SystemProtocol data. <br/>
  /// Generated 2024-06-18 09:15:56Z. Re-generate whenever you change the ContentType. <br/>
  /// <br/>
  /// Default properties such as `.Title` or `.Id` are provided in the base class. <br/>
  /// Most properties have a simple access, such as `.ModuleId`. <br/>
  /// For other properties or uses, use methods such as
  /// .IsNotEmpty("FieldName"), .String("FieldName"), .Children(...), .Picture(...), .Html(...).
  /// </summary>
  public partial class SystemProtocol: AutoGenerated.ZAutoGenSystemProtocol
  {  }
}

namespace AppCode.Data.AutoGenerated
{
  /// <summary>
  /// Auto-Generated base class for Default.SystemProtocol in separate namespace and special name to avoid accidental use.
  /// </summary>
  public abstract class ZAutoGenSystemProtocol: Custom.Data.CustomItem
  {
    /// <summary>
    /// ModuleId as int. <br/>
    /// To get other types use methods such as .Decimal("ModuleId")
    /// </summary>
    public int ModuleId => _item.Int("ModuleId");

    /// <summary>
    /// RawData as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("RawData", scrubHtml: true) etc.
    /// </summary>
    public string RawData => _item.String("RawData", fallback: "");

    /// <summary>
    /// SenderIP as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("SenderIP", scrubHtml: true) etc.
    /// </summary>
    public string SenderIP => _item.String("SenderIP", fallback: "");

    /// <summary>
    /// Timestamp as DateTime.
    /// </summary>
    public DateTime Timestamp => _item.DateTime("Timestamp");

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
  }
}