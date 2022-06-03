@ModelType Product

@Html.HiddenFor(Function(model) model.BrandName)
@Html.HiddenFor(Function(model) model.CategoryName)

<ul class="nav nav-tabs mt-3">
    <li class="nav-item">
        <a class="nav-link active" id="home-tab" data-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true">Товар</a>
    </li>
    <li class="nav-item">
        <a class="nav-link" id="price-tab" data-toggle="tab" href="#price" role="tab" aria-controls="price" aria-selected="false">Цены и остатки</a>
    </li>
    @*<li class="nav-item">
            <a class="nav-link" id="feature-tab" data-toggle="tab" href="#feature" role="tab" aria-controls="feature" aria-selected="false">Характеристики</a>
        </li>*@
</ul>

<div class="tab-content">
    <div class="tab-pane show active" id="home" role="tabpanel" aria-labelledby="home-tab">
        <div class="row">
            <div class="col-lg-9">
                <div class="form-group">
                    @Html.LabelFor(Function(model) model.Title, htmlAttributes:=New With {.class = "control-label required"})
                    @Html.ValidationMessageFor(Function(model) model.Title, "", New With {.class = "text-danger"})
                    @Html.EditorFor(Function(model) model.Title, New With {.htmlAttributes = New With {.class = "form-control"}})
                </div>

                <div class="row">
                    <div class="form-group col-lg-4">
                        @Html.LabelFor(Function(model) model.BrandId, htmlAttributes:=New With {.class = "control-label"})
                        @Html.ValidationMessageFor(Function(model) model.BrandId, "", New With {.class = "text-danger"})
                        @Html.DropDownList("BrandId", Nothing, "", htmlAttributes:=New With {.class = "form-control"})
                    </div>

                    <div class="form-group col-lg-4">
                        @Html.LabelFor(Function(model) model.ModelName, htmlAttributes:=New With {.class = "control-label"})
                        @Html.ValidationMessageFor(Function(model) model.ModelName, "", New With {.class = "text-danger"})
                        @Html.EditorFor(Function(model) model.ModelName, New With {.htmlAttributes = New With {.class = "form-control"}})
                    </div>

                    <div class="form-group col-lg-4">
                        @Html.LabelFor(Function(model) model.Sku, htmlAttributes:=New With {.class = "control-label"})
                        @Html.ValidationMessageFor(Function(model) model.Sku, "", New With {.class = "text-danger"})
                        @Html.EditorFor(Function(model) model.Sku, New With {.htmlAttributes = New With {.class = "form-control"}})
                    </div>
                </div>

                <div class="form-group">
                    @Html.LabelFor(Function(model) model.Content, htmlAttributes:=New With {.class = "control-label"})
                    @Html.ValidationMessageFor(Function(model) model.Content, "", New With {.class = "text-danger"})
                    @Html.EditorFor(Function(model) model.Content, New With {.htmlAttributes = New With {.class = "form-control"}})
                </div>
            </div>
            <div class="col-lg-3">
                <div class="form-group">
                    @Html.LabelFor(Function(model) model.CategoryId, htmlAttributes:=New With {.class = "control-label"})
                    @Html.ValidationMessageFor(Function(model) model.CategoryId, "", New With {.class = "text-danger"})
                    @Html.DropDownListFor(Function(model) model.CategoryId, Nothing, "", htmlAttributes:=New With {.class = "form-control"})
                </div>

                <div class="form-group">
                    @Html.LabelFor(Function(model) model.Order, htmlAttributes:=New With {.class = "control-label"})
                    @Html.ValidationMessageFor(Function(model) model.Order, "", New With {.class = "text-danger"})
                    @Html.EditorFor(Function(model) model.Order, New With {.htmlAttributes = New With {.class = "form-control text-right"}})
                </div>

                <div class="form-group">
                    @Html.LabelFor(Function(model) model.Vendor, htmlAttributes:=New With {.class = "control-label"})
                    @Html.ValidationMessageFor(Function(model) model.Vendor, "", New With {.class = "text-danger"})
                    @Html.EditorFor(Function(model) model.Vendor, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})
                </div>

                <div class="form-group">
                    @Html.LabelFor(Function(model) model.ImageId, htmlAttributes:=New With {.class = "control-label"})
                    @Html.ValidationMessageFor(Function(model) model.ImageId, "", New With {.class = "text-danger"})
                    @Html.Editor("ImageId", "ImageFile")
                </div>

                <div class="form-group">
                    <div class="checkbox">
                        <label>
                            @Html.EditorFor(Function(model) model.IsPopular)
                            @Html.DisplayNameFor(Function(model) model.IsPopular)
                        </label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="tab-pane" id="price" role="tabpanel" aria-labelledby="price-tab">
        <div class="row">
            <div class="form-group col-md-3">
                @Html.LabelFor(Function(model) model.Price, htmlAttributes:=New With {.class = "control-label required"})
                @Html.ValidationMessageFor(Function(model) model.Price, "", New With {.class = "text-danger"})
                @Html.EditorFor(Function(model) model.Price, New With {.htmlAttributes = New With {.class = "form-control text-right"}})
            </div>
            @*<div class="form-group col-md-3">
                    @Html.LabelFor(Function(model) model.OldPrice, htmlAttributes:=New With {.class = "control-label"})
                    @Html.ValidationMessageFor(Function(model) model.OldPrice, "", New With {.class = "text-danger"})
                    @Html.EditorFor(Function(model) model.OldPrice, New With {.htmlAttributes = New With {.class = "form-control text-right"}})
                </div>
                <div class="form-group col-md-1">
                    @Html.LabelFor(Function(model) model.AvailableQuantity, htmlAttributes:=New With {.class = "control-label required"})
                    @Html.ValidationMessageFor(Function(model) model.AvailableQuantity, "", New With {.class = "text-danger"})
                    @Html.EditorFor(Function(model) model.AvailableQuantity, New With {.htmlAttributes = New With {.class = "form-control text-right"}})
                </div>*@
        </div>
    </div>
    @*<div class="tab-pane" id="feature" role="tabpanel" aria-labelledby="feature-tab">
            ...
        </div>*@
</div>
