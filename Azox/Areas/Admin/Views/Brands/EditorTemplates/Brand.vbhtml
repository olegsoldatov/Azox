@ModelType Brand
<div class="row">
    <div class="col-lg-9">
        <div class="form-group">
            @Html.LabelFor(Function(model) model.Title, htmlAttributes:=New With {.class = "control-label required"})
            @Html.ValidationMessageFor(Function(model) model.Title, "", New With {.class = "text-danger"})
            @Html.EditorFor(Function(model) model.Title, New With {.htmlAttributes = New With {.class = "form-control"}})
        </div>
    </div>
    <div class="col-lg-3">
        @*<div class="form-group">
                @Html.LabelFor(Function(model) model.Order, htmlAttributes:=New With {.class = "control-label"})
                @Html.ValidationMessageFor(Function(model) model.Order, "", New With {.class = "text-danger"})
                @Html.EditorFor(Function(model) model.Order, New With {.htmlAttributes = New With {.class = "form-control text-right"}})
            </div>*@

        <div class="form-group">
            @Html.LabelFor(Function(model) model.ImageId, htmlAttributes:=New With {.class = "control-label"})
            @Html.ValidationMessageFor(Function(model) model.ImageId, "", New With {.class = "text-danger"})
            @Html.EditorFor(Function(model) model.ImageId, "ImageFile")
        </div>

        <div class="form-group form-check">
            @Html.CheckBoxFor(Function(model) model.IsPublished, New With {.class = "form-check-input"})
            @Html.LabelFor(Function(model) model.IsPublished, New With {.class = "form-check-label"})
        </div>
    </div>
</div>
