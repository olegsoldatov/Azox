@ModelType Category
<div class="row">
	<div class="col-lg-9">
		<div class="row">
			<div class="form-group col-lg-8">
				@Html.LabelFor(Function(model) model.Title, htmlAttributes:=New With {.class = "control-label required"})
				@Html.EditorFor(Function(model) model.Title, New With {.htmlAttributes = New With {.class = "form-control"}})
				@Html.ValidationMessageFor(Function(model) model.Title, "", New With {.class = "text-danger"})
			</div>

			<div class="form-group col-lg-4">
				@Html.LabelFor(Function(model) model.ParentId, htmlAttributes:=New With {.class = "control-label"})
				@Html.DropDownList("ParentId", Nothing, "", New With {.class = "form-control"})
				@Html.ValidationMessageFor(Function(model) model.ParentId, "", New With {.class = "text-danger"})
			</div>
		</div>

		<div class="form-group">
			@Html.LabelFor(Function(model) model.Content, htmlAttributes:=New With {.class = "control-label"})
			@Html.EditorFor(Function(model) model.Content, New With {.htmlAttributes = New With {.class = "form-control"}})
			@Html.ValidationMessageFor(Function(model) model.Content, "", New With {.class = "text-danger"})
		</div>

		<div class="row">
			<div class="form-group col-lg-6">
				@Html.LabelFor(Function(model) model.Slug, htmlAttributes:=New With {.class = "control-label"})
				@Html.EditorFor(Function(model) model.Slug, New With {.htmlAttributes = New With {.class = "form-control"}})
				@Html.ValidationMessageFor(Function(model) model.Slug, "", New With {.class = "text-danger"})
			</div>

			<div class="form-group col-lg-6">
				@Html.LabelFor(Function(model) model.TypePrefix, htmlAttributes:=New With {.class = "control-label"})
				@Html.EditorFor(Function(model) model.TypePrefix, New With {.htmlAttributes = New With {.class = "form-control"}})
				@Html.ValidationMessageFor(Function(model) model.TypePrefix, "", New With {.class = "text-danger"})
			</div>
		</div>
	</div>
	<div class="col-lg-3">
		<div class="form-group">
			@Html.LabelFor(Function(model) model.ImageId, htmlAttributes:=New With {.class = "control-label"})
			@Html.Editor("ImageId", "ImageFile")
			@Html.ValidationMessageFor(Function(model) model.ImageId, "", New With {.class = "text-danger"})
		</div>

		<div class="form-group">
			@Html.LabelFor(Function(model) model.Order, htmlAttributes:=New With {.class = "control-label"})
			@Html.EditorFor(Function(model) model.Order, New With {.htmlAttributes = New With {.class = "form-control text-right"}})
			@Html.ValidationMessageFor(Function(model) model.Order, "", New With {.class = "text-danger"})
		</div>

		<div class="form-group form-check">
			@Html.CheckBoxFor(Function(m) m.Draft, New With {.class = "form-check-input"})
			@Html.LabelFor(Function(m) m.Draft, New With {.class = "form-check-label"})
		</div>
	</div>
</div>
