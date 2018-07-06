@ModelType Node

<div class="row">
	<div class="col-md-9">
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Title, htmlAttributes:=New With {.class = "control-label"})
			@Html.EditorFor(Function(model) model.Title, New With {.htmlAttributes = New With {.class = "form-control"}})
			@Html.ValidationMessageFor(Function(model) model.Title, "", New With {.class = "text-danger"})
		</div>
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Heading, htmlAttributes:=New With {.class = "control-label"})
			@Html.EditorFor(Function(model) model.Heading, New With {.htmlAttributes = New With {.class = "form-control"}})
			@Html.ValidationMessageFor(Function(model) model.Heading, "", New With {.class = "text-danger"})
		</div>
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Description, htmlAttributes:=New With {.class = "control-label"})
			@Html.EditorFor(Function(model) model.Description, New With {.htmlAttributes = New With {.class = "form-control"}})
			@Html.ValidationMessageFor(Function(model) model.Description, "", New With {.class = "text-danger"})
		</div>
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Keywords, htmlAttributes:=New With {.class = "control-label"})
			@Html.EditorFor(Function(model) model.Keywords, New With {.htmlAttributes = New With {.class = "form-control"}})
			@Html.ValidationMessageFor(Function(model) model.Keywords, "", New With {.class = "text-danger"})
		</div>
	</div>
	<div class="col-md-3">
		@If Model.ParentId = Guid.Empty Then
			@Html.HiddenFor(Function(m) m.Url)
			@Html.HiddenFor(Function(m) m.Order)
			@Html.HiddenFor(Function(m) m.ParentId)
		Else
			@<div class="form-group">
				@Html.LabelFor(Function(model) model.Url, htmlAttributes:=New With {.class = "control-label"})
				@Html.EditorFor(Function(model) model.Url, New With {.htmlAttributes = New With {.class = "form-control"}})
				@Html.ValidationMessageFor(Function(model) model.Url, "", New With {.class = "text-danger"})
			</div>
			@<div class="form-group">
				@Html.LabelFor(Function(model) model.Order, htmlAttributes:=New With {.class = "control-label"})
				@Html.EditorFor(Function(model) model.Order, "Order")
				@Html.ValidationMessageFor(Function(model) model.Order, "", New With {.class = "text-danger"})
			</div>
			@<div class="form-group">
			 	@Html.LabelFor(Function(model) model.ParentId, htmlAttributes:=New With {.class = "control-label"})
				@Html.DropDownListFor(Function(m) m.ParentId, CType(ViewBag.Parents, SelectList), New With {.class = "form-control"})
				@Html.ValidationMessageFor(Function(model) model.ParentId, "", New With {.class = "text-danger"})
			</div>
		End If
	</div>
</div>

