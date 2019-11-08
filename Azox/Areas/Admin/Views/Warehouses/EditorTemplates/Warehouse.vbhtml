@ModelType Warehouse
<div class="row">
	<div class="col-md-9">
		<div class="row">
			<div class="form-group col-md-8">
				@Html.LabelFor(Function(model) model.Name, htmlAttributes:=New With {.class = "control-label required"})
				@Html.ValidationMessageFor(Function(model) model.Name, "", New With {.class = "text-danger"})
				@Html.EditorFor(Function(model) model.Name, New With {.htmlAttributes = New With {.class = "form-control"}})
			</div>
			<div class="form-group col-md-4">
				@Html.LabelFor(Function(model) model.Slug, htmlAttributes:=New With {.class = "control-label"})
				@Html.ValidationMessageFor(Function(model) model.Slug, "", New With {.class = "text-danger"})
				@Html.EditorFor(Function(model) model.Slug, New With {.htmlAttributes = New With {.class = "form-control"}})
			</div>
		</div>
		<div class="row">
			<div class="form-group col-md-8">
				@Html.LabelFor(Function(model) model.City, htmlAttributes:=New With {.class = "control-label"})
				@Html.ValidationMessageFor(Function(model) model.City, "", New With {.class = "text-danger"})
				@Html.EditorFor(Function(model) model.City, New With {.htmlAttributes = New With {.class = "form-control"}})
			</div>
			<div class="form-group col-md-4">
				@Html.LabelFor(Function(model) model.PostalCode, htmlAttributes:=New With {.class = "control-label"})
				@Html.ValidationMessageFor(Function(model) model.PostalCode, "", New With {.class = "text-danger"})
				@Html.EditorFor(Function(model) model.PostalCode, New With {.htmlAttributes = New With {.class = "form-control"}})
			</div>
			<div class="form-group col-md-4">
				@Html.LabelFor(Function(model) model.DeliveryDays, htmlAttributes:=New With {.class = "control-label"})
				@Html.ValidationMessageFor(Function(model) model.DeliveryDays, "", New With {.class = "text-danger"})
				@Html.EditorFor(Function(model) model.DeliveryDays, New With {.htmlAttributes = New With {.class = "form-control"}})
			</div>
		</div>
	</div>
	<div class="col-md-3">
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Order, htmlAttributes:=New With {.class = "control-label"})
			@Html.ValidationMessageFor(Function(model) model.Order, "", New With {.class = "text-danger"})
			@Html.EditorFor(Function(model) model.Order, New With {.htmlAttributes = New With {.class = "form-control"}})
		</div>

		<div class="form-group form-check">
			@Html.CheckBoxFor(Function(m) m.IsPublished, New With {.class = "form-check-input"})
			@Html.LabelFor(Function(m) m.IsPublished, New With {.class = "form-check-label"})
		</div>
	</div>
</div>
<div class="form-group">
	<button class="btn btn-primary">Сохранить</button>
</div>

