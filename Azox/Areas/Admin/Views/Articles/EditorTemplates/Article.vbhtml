@ModelType Article

<div class="row">
	<div class="col-md-9">
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Title, htmlAttributes:=New With {.class = "control-label"})
			@Html.EditorFor(Function(model) model.Title, New With {.htmlAttributes = New With {.class = "form-control"}})
			@Html.ValidationMessageFor(Function(model) model.Title, "", New With {.class = "text-danger"})
		</div>

		<div class="form-group">
			@Html.LabelFor(Function(model) model.Content, htmlAttributes:=New With {.class = "control-label"})
			@Html.EditorFor(Function(model) model.Content, New With {.htmlAttributes = New With {.class = "form-control"}})
			@Html.ValidationMessageFor(Function(model) model.Content, "", New With {.class = "text-danger"})
		</div>
	</div>
	<div class="col-md-3">
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Slug, htmlAttributes:=New With {.class = "control-label"})
			@Html.EditorFor(Function(model) model.Slug, New With {.htmlAttributes = New With {.class = "form-control"}})
			@Html.ValidationMessageFor(Function(model) model.Slug, "", New With {.class = "text-danger"})
		</div>
	</div>
</div>

