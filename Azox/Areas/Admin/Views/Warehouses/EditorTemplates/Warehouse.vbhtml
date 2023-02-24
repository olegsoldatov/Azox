@ModelType Warehouse
<div class="row">
	<div class="col-lg-9">
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Name, htmlAttributes:=New With {.class = "control-label required"})
			@Html.ValidationMessageFor(Function(model) model.Name, "", New With {.class = "text-danger"})
			@If IsNothing(Model) Then
				@Html.EditorFor(Function(model) model.Name, New With {.htmlAttributes = New With {.class = "form-control"}})
			Else
				@Html.EditorFor(Function(model) model.Name, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})
			End If
		</div>
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Title, htmlAttributes:=New With {.class = "control-label required"})
			@Html.ValidationMessageFor(Function(model) model.Title, "", New With {.class = "text-danger"})
			@Html.EditorFor(Function(model) model.Title, New With {.htmlAttributes = New With {.class = "form-control"}})
		</div>
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Company, htmlAttributes:=New With {.class = "control-label required"})
			@Html.ValidationMessageFor(Function(model) model.Company, "", New With {.class = "text-danger"})
			@Html.EditorFor(Function(model) model.Company, New With {.htmlAttributes = New With {.class = "form-control"}})
		</div>
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Margin, htmlAttributes:=New With {.class = "control-label required"})
			@Html.ValidationMessageFor(Function(model) model.Margin, "", New With {.class = "text-danger"})
			@Html.Editor("Margin", New With {.htmlAttributes = New With {.class = "form-control text-right", .type = "number"}})
		</div>
		<div class="form-group">
			@Html.LabelFor(Function(model) model.MarginGroupId, htmlAttributes:=New With {.class = "control-label"})
			@Html.ValidationMessageFor(Function(model) model.MarginGroupId, "", New With {.class = "text-danger"})
			@Html.DropDownList("MarginGroupId", Nothing, "", New With {.class = "form-control"})
		</div>
	</div>
    <div class="col-lg-3">
        <div class="form-group">
            @Html.LabelFor(Function(model) model.Order, htmlAttributes:=New With {.class = "control-label"})
            @Html.ValidationMessageFor(Function(model) model.Order, "", New With {.class = "text-danger"})
            @Html.EditorFor(Function(model) model.Order, New With {.htmlAttributes = New With {.class = "form-control text-right"}})
        </div>

        <div class="form-group form-check">
            @Html.CheckBoxFor(Function(model) model.IsPublished, New With {.class = "form-check-input"})
            @Html.LabelFor(Function(model) model.IsPublished, New With {.class = "form-check-label"})
        </div>
    </div>
</div>

