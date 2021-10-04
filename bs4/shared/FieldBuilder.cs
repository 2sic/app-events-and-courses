using ToSic.Razor.Blade;

public class FieldBuilders: Custom.Hybrid.Code12
{
  public dynamic Label(string label, string forControl) {
    return Tag.Label().Class("col-sm-3").Attr("for", forControl);
  }

  public dynamic Textbox(string label, string id, bool required = false, string type = "text", string value = "", bool disabled = false) {
    var input = Tag.Input().Class("form-control").Value(value).Type(type).Id(id).Name(id);
    if (required) {
      input = input.Required();
    }
    var textbox = Tag.Div(Label(label, id), Tag.Div(input).Class("col-sm-9")).Class("form-group row");
    if (disabled) {
      textbox = textbox.Disabled();
    }
    return textbox;
  }
}