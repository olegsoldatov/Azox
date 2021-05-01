@ModelType Customer

<div class="row">
	<div class="col-lg-3">
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Name, htmlAttributes:=New With {.class = "control-label required"})
			@Html.ValidationMessageFor(Function(model) model.Name, "", New With {.class = "text-danger"})
			@Html.EditorFor(Function(model) model.Name, New With {.htmlAttributes = New With {.class = "form-control"}})
		</div>
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Phone, htmlAttributes:=New With {.class = "control-label required"})
			@Html.ValidationMessageFor(Function(model) model.Phone, "", New With {.class = "text-danger"})
			@Html.EditorFor(Function(model) model.Phone, New With {.htmlAttributes = New With {.class = "form-control", .data_inputmask = "'mask': '79999999999'"}})
		</div>
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Email, htmlAttributes:=New With {.class = "control-label"})
			@Html.ValidationMessageFor(Function(model) model.Email, "", New With {.class = "text-danger"})
			@Html.EditorFor(Function(model) model.Email, New With {.htmlAttributes = New With {.class = "form-control"}})
		</div>
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Comment, htmlAttributes:=New With {.class = "control-label"})
			@Html.ValidationMessageFor(Function(model) model.Comment, "", New With {.class = "text-danger"})
			@Html.EditorFor(Function(model) model.Comment, New With {.htmlAttributes = New With {.class = "form-control"}})
		</div>
	</div>
</div>
