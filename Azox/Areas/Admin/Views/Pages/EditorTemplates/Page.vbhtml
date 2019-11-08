@ModelType Azox.Page
<div class="row">
	<div class="col-md-9">
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Name, htmlAttributes:=New With {.class = "control-label required"})
			@Html.ValidationMessageFor(Function(model) model.Name, "", New With {.class = "text-danger"})
			@Html.EditorFor(Function(model) model.Name, New With {.htmlAttributes = New With {.class = "form-control"}})
		</div>

		<div class="form-group">
			@Html.LabelFor(Function(model) model.Content, htmlAttributes:=New With {.class = "control-label"})
			@Html.ValidationMessageFor(Function(model) model.Content, "", New With {.class = "text-danger"})
			@Html.EditorFor(Function(model) model.Content, New With {.htmlAttributes = New With {.class = "form-control"}})
		</div>

		<div class="row">
			<div class="form-group col-md-6">
				@Html.LabelFor(Function(model) model.Title, htmlAttributes:=New With {.class = "control-label"})
				@Html.ValidationMessageFor(Function(model) model.Title, "", New With {.class = "text-danger"})
				@Html.EditorFor(Function(model) model.Title, New With {.htmlAttributes = New With {.class = "form-control"}})
			</div>

			<div class="form-group col-md-6">
				@Html.LabelFor(Function(model) model.Heading, htmlAttributes:=New With {.class = "control-label"})
				@Html.ValidationMessageFor(Function(model) model.Heading, "", New With {.class = "text-danger"})
				@Html.EditorFor(Function(model) model.Heading, New With {.htmlAttributes = New With {.class = "form-control"}})
			</div>
		</div>

		<div class="form-group">
			@Html.LabelFor(Function(model) model.Description, htmlAttributes:=New With {.class = "control-label"})
			@Html.ValidationMessageFor(Function(model) model.Description, "", New With {.class = "text-danger"})
			@Html.EditorFor(Function(model) model.Description, New With {.htmlAttributes = New With {.class = "form-control"}})
		</div>
	</div>
</div>
