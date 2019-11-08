@ModelType Brand
<div class="form-group">
	@Html.LabelFor(Function(model) model.Name, htmlAttributes:=New With {.class = "control-label required"})
	@Html.ValidationMessageFor(Function(model) model.Name, "", New With {.class = "text-danger"})
	@Html.EditorFor(Function(model) model.Name, New With {.htmlAttributes = New With {.class = "form-control"}})
</div>
<div class="form-group">
	@Html.LabelFor(Function(model) model.Slug, htmlAttributes:=New With {.class = "control-label required"})
	@Html.ValidationMessageFor(Function(model) model.Slug, "", New With {.class = "text-danger"})
	@Html.EditorFor(Function(model) model.Slug, New With {.htmlAttributes = New With {.class = "form-control"}})
</div>
