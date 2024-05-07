// DO NOT MODIFY THIS FILE - IT IS AUTO-GENERATED
// See also: https://go.2sxc.org/copilot-data
// To extend it, create a "AppSettings.cs" with this contents:
/*
namespace AppCode.Data
{
  public partial class AppSettings
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
  // This is a generated class for AppSettings (scope: System.App)
  // To extend/modify it, see instructions above.

  /// <summary>
  /// AppSettings data. <br/>
  /// Generated 2024-05-02 13:03:09Z. Re-generate whenever you change the ContentType. <br/>
  /// <br/>
  /// Default properties such as `.Title` or `.Id` are provided in the base class. <br/>
  /// Most properties have a simple access, such as `.CustomerMailTemplateFile`. <br/>
  /// For other properties or uses, use methods such as
  /// .IsNotEmpty("FieldName"), .String("FieldName"), .Children(...), .Picture(...), .Html(...).
  /// </summary>
  /// <remarks>
  /// This Content-Type is NOT in the default scope, so you may not see it in the Admin UI. It's in the scope System.App.
  /// </remarks>
  public partial class AppSettings: AutoGenerated.ZAutoGenAppSettings
  {  }
}

namespace AppCode.Data.AutoGenerated
{
  /// <summary>
  /// Auto-Generated base class for System.App.AppSettings in separate namespace and special name to avoid accidental use.
  /// </summary>
  public abstract class ZAutoGenAppSettings: Custom.Data.CustomItem
  {
    /// <summary>
    /// CustomerMailTemplateFile as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("CustomerMailTemplateFile", scrubHtml: true) etc.
    /// </summary>
    public string CustomerMailTemplateFile => _item.String("CustomerMailTemplateFile", fallback: "");

    /// <summary>
    /// MailFrom as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("MailFrom", scrubHtml: true) etc.
    /// </summary>
    public string MailFrom => _item.String("MailFrom", fallback: "");

    /// <summary>
    /// OwnerMail as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("OwnerMail", scrubHtml: true) etc.
    /// </summary>
    public string OwnerMail => _item.String("OwnerMail", fallback: "");

    /// <summary>
    /// OwnerMailTemplateFile as string. <br/>
    /// For advanced manipulation like scrubHtml, use .String("OwnerMailTemplateFile", scrubHtml: true) etc.
    /// </summary>
    public string OwnerMailTemplateFile => _item.String("OwnerMailTemplateFile", fallback: "");

    /// <summary>
    /// Recaptcha as bool. <br/>
    /// To get nullable use .Get("Recaptcha") as bool?;
    /// </summary>
    public bool Recaptcha => _item.Bool("Recaptcha");

    /// <summary>
    /// RegistrationEnabled as bool. <br/>
    /// To get nullable use .Get("RegistrationEnabled") as bool?;
    /// </summary>
    public bool RegistrationEnabled => _item.Bool("RegistrationEnabled");
  }
}