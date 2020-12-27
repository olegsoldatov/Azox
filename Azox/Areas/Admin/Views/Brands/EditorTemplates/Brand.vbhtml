@ModelType Brand

<div class="row">
	<div class="col-lg-4">
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Name, htmlAttributes:=New With {.class = "control-label required"})
			@Html.ValidationMessageFor(Function(model) model.Name, "", New With {.class = "text-danger"})
			@Html.EditorFor(Function(model) model.Name, New With {.htmlAttributes = New With {.class = "form-control"}})
			<small class="form-text text-muted">Используется для идентификации и навигации.</small>
		</div>

		<div class="form-group">
			@Html.LabelFor(Function(model) model.Title, htmlAttributes:=New With {.class = "control-label required"})
			@Html.ValidationMessageFor(Function(model) model.Title, "", New With {.class = "text-danger"})
			@Html.EditorFor(Function(model) model.Title, New With {.htmlAttributes = New With {.class = "form-control"}})
		</div>

		<div class="form-group">
			@Html.LabelFor(Function(model) model.Order, htmlAttributes:=New With {.class = "control-label"})
			@Html.ValidationMessageFor(Function(model) model.Order, "", New With {.class = "text-danger"})
			@Html.Editor("Order", New With {.htmlAttributes = New With {.class = "form-control text-right"}})
		</div>

		<div class="form-group form-check">
			@Html.CheckBoxFor(Function(model) model.Draft, New With {.class = "form-check-input"})
			@Html.LabelFor(Function(model) model.Draft, New With {.class = "form-check-label"})
		</div>
	</div>
</div>
