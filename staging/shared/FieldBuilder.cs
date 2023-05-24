using ToSic.Razor.Blade;

public class FieldBuilder : Custom.Hybrid.Code14
{
    /* 
      this file is for creating different fields e.g. input, textarea, file, dropdown and showing them in the template

      Example: 
      Shows a required input of type text with a label infront of it

      var FieldBuilder = CreateInstance("shared/FieldBuilders.cs");
      @FieldBuilder.Text("Subject", true)

      Shows a required input of type text with a label infront of it

      var FieldBuilder = CreateInstance("shared/FieldBuilders.cs");
      FieldBuilder.LabelInPlaceholder = true
      @FieldBuilder.EMail("Email", true)

      Shows a required input of type text with a placeholder
    */

    // handles the visibility of a label or a placeholder
    public bool LabelInPlaceholder = false;

    // returns form validation class 
    private string FormValidationClass()
    {
        return "app-events6-form-fields ";
    }

    #region Koi based class selection
    internal string FormClasses()
    {
        return FormValidationClass()
          + (LabelInPlaceholder ? "" : "row ")
          + (Kit.Css.Is("bs3") ? "form-group" : "mb-3");
    }

    // Choose CSS classes based on the framework
    // if you customize this, you probably know what css framework you want,
    // in which case you can skip framework detection and just write the classes

    // Choose CSS classes for the labels
    internal string LabelClasses(bool required)
    {
        return "control-label "
          + (required ? "app-events6-field-required " : "")
          + (Kit.Css.Is("bs3") ? "col col-xs-12 col-sm-3" : "col-12 col-sm-3");
    }

    #endregion

    // Add a placeholder text to the inputs
    internal string PhLabel(string key, bool required)
    {
        return LabelInPlaceholder ? Resources.Get("Label" + key) + (required ? "*" : "") : "";
    }

    // Sets a RazorBlade Input/TextArea to required and adds the message which is different for each field type
    internal void SetRequired(dynamic content, bool required, string message)
    {
        if (!required) return;
        content.Attr("data-pristine-required-message", message).Required();
    }

    // returns a Label
    public dynamic Label(string label, string forControl, bool required = false)
    {
        return Tag.Label(label).Class("col-sm-3" + (required ? " app-events6-field-required " : "")).Attr("for", forControl);
    }

    // returns an input with common attributes and a possible placeholder
    public dynamic Text(string idString, bool required, bool disabled = false, string value = "")
    {
        var content = Tag.Input().Type("text").Id(idString).Placeholder(PhLabel(idString, required)).Class("form-control");
        if (value != null) { content.Attr("value", value); }
        SetRequired(content, required, Resources.LabelRequired);
        if (disabled) { content = content.Disabled(); }
        return Field(idString, required, content);
    }

    // returns an input of type email with common attributes and a possible placeholder
    public dynamic EMail(string idString, bool required)
    {
        var content = Tag.Input().Type("email").Id(idString).Placeholder(PhLabel(idString, required)).Class("form-control");
        SetRequired(content, required, Resources.LabelValidEmail);
        return Field(idString, required, content);
    }

    // returns a textarea with common attributes and a possible placeholder
    public dynamic Multiline(string idString, bool required, bool disabled = false, string value = "")
    {
        var content = Tag.Textarea().Id(idString).Placeholder(PhLabel(idString, required)).Class("form-control");
        if (value != null) { content.Add(value); }
        SetRequired(content, required, Resources.LabelRequired);
        if (disabled) { content = content.Disabled(); }
        return Field(idString, required, content);
    }

    // returns a select and options with common attributes
    public dynamic DropDown(string idString, bool required, string[] values)
    {
        var content = Tag.Select().Id(idString).Class("form-control");
        SetRequired(content, required, Resources.LabelRequired);
        content.Add(Tag.Option(Resources.LabelPleaseSelect).Attr("value", ""));
        foreach (var value in values)
        {
            content.Add(Tag.Option(value));
        }
        return Field(idString, required, content);
    }

    // returns a Radio with common attributes
    public dynamic Radio(string idString, bool required, string[] values)
    {
        var content = Tag.Div();
        foreach (var value in values)
        {
            var radioId = idString + value.ToLower().Replace(" ", "");
            var wrapper = Tag.Div().Class(Kit.Css.Is("bs3") ? "radio" : "form-check");
            var radio = Tag.Input().Attr("type", "radio").Id(radioId).Name(idString).Value(value);
            SetRequired(radio, required, Resources.LabelRequired);

            if (Kit.Css.Is("bs3"))
            {
                var radioLabel = Tag.Label(radio + value).For(radioId);
                wrapper.Add(radioLabel);
            }
            else
            {
                radio.Class("form-check-input");
                var radioLabel = Tag.Label(value).Class("form-check-label").For(radioId);
                wrapper.Add(radio + radioLabel);
            }
            content.Add(wrapper);
        }

        return Field(idString, required, content);
    }

    // returns a checkbox with common attributes
    public dynamic Checkbox(string idString, bool required)
    {
        var checkbox = Tag.Input().Attr("type", "checkbox").Id(idString).Class("form-check-input");
        SetRequired(checkbox, required, Resources.LabelRequired);
        if (Kit.Css.Is("bs3"))
        {
            return FieldCheckboxBs3(idString, required, checkbox);
        }
        else
        {
            return FieldCheckbox(idString, required, checkbox);
        }
    }

    // shows a wrapping div with choosen content
    public dynamic Field(string idString, bool required, dynamic contents)
    {
        var inputWrapperClasses = Kit.Css.Is("bs3") ? "col col-xs-12 col-sm-9" : "col-12  col-sm-9";
        var labelTranslated = Resources.Get("Label" + idString);
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

        return field.Add(Tag.Div(contents).Class(!LabelInPlaceholder ? inputWrapperClasses : ""));
    }

    public IHtmlTag FieldCheckbox(string idString, bool required, dynamic contents)
    {
        var labelTranslated = Kit.Scrub.Only(Resources.Get(idString + "Label"), "p");
        var label = ToSic.Razor.Blade.Text.First(labelTranslated, idString) + (required ? "*" : "");
        var field = Tag.Div().Class(FormValidationClass() + "mb-3 form-check");

        field.Add(contents);

        return field.Add(Tag.Label(label).Class("form-check-label").For(idString));
    }

    public IHtmlTag FieldCheckboxBs3(string idString, bool required, dynamic contents)
    {
        var labelTranslated = Kit.Scrub.Only(Resources.Get(idString + "Label"), "p");
        var label = ToSic.Razor.Blade.Text.First(labelTranslated, idString) + (required ? "*" : "");
        var field = Tag.Div().Class(FormValidationClass() + "form-group");

        var labelWithInput = Tag.Label(contents + label);
        var checkboxDiv = Tag.Div().Class("checkbox").Add(labelWithInput);

        return field.Add(checkboxDiv);
    }

}