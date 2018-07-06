@ModelType Azox.Page

@Html.HiddenFor(Function(m) m.ActionName)
@Html.HiddenFor(Function(m) m.ControllerName)

<div class="row">
	<div class="col-md-9">
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Title, htmlAttributes:=New With {.class = "control-label required"})
			@Html.ValidationMessageFor(Function(model) model.Title, "", New With {.class = "text-danger"})
			@Html.EditorFor(Function(model) model.Title, New With {.htmlAttributes = New With {.class = "form-control"}})
		</div>

		<div class="form-group">
			@Html.LabelFor(Function(model) model.Heading, htmlAttributes:=New With {.class = "control-label"})
			@Html.ValidationMessageFor(Function(model) model.Heading, "", New With {.class = "text-danger"})
			@Html.EditorFor(Function(model) model.Heading, New With {.htmlAttributes = New With {.class = "form-control"}})
			<small class="form-text text-muted">Если оставить пустым, то загловок будет, как название.</small>
		</div>

		<div class="form-group">
			@Html.LabelFor(Function(model) model.Content, htmlAttributes:=New With {.class = "control-label"})
			@Html.ValidationMessageFor(Function(model) model.Content, "", New With {.class = "text-danger"})
			@Html.EditorFor(Function(model) model.Content, New With {.htmlAttributes = New With {.class = "form-control"}})
		</div>
	</div>
	<div class="col-md-3">
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Description, htmlAttributes:=New With {.class = "control-label"})
			@Html.ValidationMessageFor(Function(model) model.Description, "", New With {.class = "text-danger"})
			@Html.EditorFor(Function(model) model.Description, New With {.htmlAttributes = New With {.class = "form-control"}})
		</div>

		<div class="form-group">
			@Html.LabelFor(Function(model) model.Keywords, htmlAttributes:=New With {.class = "control-label"})
			@Html.ValidationMessageFor(Function(model) model.Keywords, "", New With {.class = "text-danger"})
			@Html.EditorFor(Function(model) model.Keywords, New With {.htmlAttributes = New With {.class = "form-control"}})
		</div>
	</div>
</div>

