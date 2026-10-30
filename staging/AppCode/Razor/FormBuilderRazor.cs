using ToSic.Razor.Blade;

namespace AppCode.Razor
{
  /// <summary>
  /// Base Class for Razor Views which have a typed App but don't use the Model or use the typed MyModel.
  /// </summary>
  public abstract class FormBuilderRazor : AppRazor<object>
  {
    /* 
    this file is for creating different fields e.g. input, textarea, file, dropdown and showing them in the template

    Example: 
    Shows a required input of type text with a label in front of it

    Shows a required input of type text with a placeholder
  */

    // handles the visibility of a label or a placeholder
    public bool LabelInPlaceholder = false;

    /// <summary>
    /// Returns form validation class 
    /// </summary>
    private string FormValidationClass()
    {
      return "app-events6-form-fields ";
    }

    #region Koi based class selection
    internal string FormClasses()
    {
      return FormValidationClass()
        + (LabelInPlaceholder ? "" : "row ")
        + "mb-3";
    }

    // Choose CSS classes based on the framework
    // if you customize this, you probably know what css framework you want,
    // in which case you can skip framework detection and just write the classes

    /// <summary>
    /// Choose CSS classes for the labels
    /// </summary>
    internal string LabelClasses(bool required)
    {
      return "control-label "
        + (required ? "app-events6-field-required " : "")
        + "col-12 col-sm-3";
    }

    #endregion

    /// <summary>
    /// Add a placeholder text to the inputs
    /// </summary>
    internal string PhLabel(string key, bool required)
    {
      return LabelInPlaceholder ? App.Resources.String("Label" + key, scrubHtml: "p") + (required ? "*" : "") : "";
    }

    /// <summary>
    /// Sets a RazorBlade Input/TextArea to required and adds the message which is different for each field type
    /// </summary>
    internal void SetRequired(IHtmlTag item, bool required, string key = null)
    {
      if (!required) return;
      var message = key != null
        ? App.Resources.String(key)
        : (MessageRequired ?? (MessageRequired = App.Resources.String("LabelRequired")));
      item.Attr("data-pristine-required-message", message).Attr("required");
    }


    /// Cache the message after first lookup for performance as we use it quite ofter
    private string MessageRequired = null;

    /// <summary>
    /// Returns a Label
    /// </summary>
    public object Label(string label, string forControl, bool required = false)
    {
      return Tag.Label(label).Class("col-sm-3" + (required ? " app-events6-field-required " : "")).Attr("for", forControl);
    }

    /// <summary>
    /// Returns an input with common attributes and a possible placeholder
    /// </summary>
    public IHtmlTag TextField(string idString, bool required, bool disabled = false, string value = "")
    {
      var item = Tag.Input().Type("text").Id(idString).Placeholder(PhLabel(idString, required)).Class("form-control");
      if (value != null) { item.Attr("value", value); }
      SetRequired(item, required);
      if (disabled) { item = item.Disabled(); }
      return Field(idString, required, item);
    }

    /// <summary>
    /// Returns an input of type email with common attributes and a possible placeholder
    /// </summary>
    public IHtmlTag EMail(string idString, bool required)
    {
      var item = Tag.Input().Type("email").Id(idString).Placeholder(PhLabel(idString, required)).Class("form-control");
      SetRequired(item, required, "LabelValidEmail");
      return Field(idString, required, item);
    }

    /// <summary>
    /// Returns a textarea with common attributes and a possible placeholder
    /// </summary>
    public IHtmlTag Multiline(string idString, bool required, bool disabled = false, string value = "")
    {
      var item = Tag.Textarea().Id(idString).Placeholder(PhLabel(idString, required)).Class("form-control");
      if (value != null) { item.Add(value); }
      SetRequired(item, required);
      if (disabled) { item = item.Disabled(); }
      return Field(idString, required, item);
    }

    /// <summary>
    /// Returns a select and options with common attributes
    /// </summary>
    public IHtmlTag DropDown(string idString, bool required, string[] values)
    {
      var item = Tag.Select().Id(idString).Class("form-control");
      SetRequired(item, required);
      item.Add(Tag.Option(App.Resources.String("LabelPleaseSelect")).Attr("value", ""));
      foreach (var value in values)
      {
        item.Add(Tag.Option(value));
      }
      return Field(idString, required, item);
    }

    /// <summary>
    /// Returns a Radio with common attributes
    /// </summary>
    public IHtmlTag Radio(string idString, bool required, string[] values)
    {
      var item = Tag.Div();
      foreach (var value in values)
      {
        var radioId = idString + value.ToLower().Replace(" ", "");
        var wrapper = Tag.Div().Class("form-check");
        var radio = Tag.Input().Attr("type", "radio").Id(radioId).Name(idString).Value(value).Class("form-check-input");
        SetRequired(radio, required);
        var radioLabel = Tag.Label(value).Class("form-check-label").For(radioId);
        wrapper.Add(radio + radioLabel);
        item.Add(wrapper);
      }
      return Field(idString, required, item);
    }

    /// <summary>
    /// Returns a checkbox with common attributes
    /// </summary>
    public IHtmlTag Checkbox(string idString, bool required, bool isTerm = false)
    {
      var checkbox = Tag.Input().Attr("type", "checkbox").Id(idString).Name(idString).Class("form-check-input");

      if (isTerm)
        checkbox.Attr("terms", "true");

      SetRequired(checkbox, required);
      var labelTranslated = App.Resources.String("Label" + idString, scrubHtml: "p");
      var label = ToSic.Razor.Blade.Text.First(labelTranslated, idString) + (required ? "*" : "");

      return Tag.Div().Class(FormValidationClass() + "mb-3 form-check").Wrap(
          checkbox,
          Tag.Label(label).Class("form-check-label").For(idString)
      );
    }

    /// <summary>
    /// Shows a wrapping div with chosen content
    /// </summary>
    private IHtmlTag Field(string idString, bool required, IHtmlTag items)
    {
      var inputWrapperClasses = "col-12 col-sm-9";
      var labelTranslated = App.Resources.String("Label" + idString, scrubHtml: "p", required: false);
      var field = Tag.Div().Class(FormClasses());

      // If the label is _not_ in the placeholder, add the label first
      if (!LabelInPlaceholder)
      {
        field = field.Add(
          Tag.Label(ToSic.Razor.Blade.Text.First(labelTranslated, idString))
            .Class(LabelClasses(required))
            .For(idString)
        );
      }
      return field.Add(Tag.Div(items).Class(!LabelInPlaceholder ? inputWrapperClasses : ""));
    }
  }

}
