@ModelType Category
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
			<div class="form-group col-md-4">
				@Html.LabelFor(Function(model) model.ParentId, htmlAttributes:=New With {.class = "control-label"})
				@Html.ValidationMessageFor(Function(model) model.ParentId, "", New With {.class = "text-danger"})
				@Html.DropDownListFor(Function(model) model.ParentId, Nothing, "", New With {.class = "form-control"})
			</div>
		</div>
	</div>
	<div class="col-md-3">
		<div class="form-group">
			@Html.LabelFor(Function(model) model.ImageFile, htmlAttributes:=New With {.class = "control-label"})
			@Html.ValidationMessageFor(Function(model) model.ImageFile, "", New With {.class = "text-danger"})
			@Html.EditorFor(Function(model) model.ImageFile, New With {.accept = "image/*"})
			@Html.EditorFor(Function(model) model.ImageId, "Thumbnail", New With {.class = "img-fluid", .alt = "Миниатюра"})
		</div>

		<div class="form-group">
			@Html.LabelFor(Function(model) model.Order, htmlAttributes:=New With {.class = "control-label"})
			@Html.ValidationMessageFor(Function(model) model.Order, "", New With {.class = "text-danger"})
			@Html.EditorFor(Function(model) model.Order, New With {.htmlAttributes = New With {.class = "form-control"}})
		</div>
	</div>
</div>
<div class="form-group">
	<button class="btn btn-primary">Сохранить</button>
</div>


