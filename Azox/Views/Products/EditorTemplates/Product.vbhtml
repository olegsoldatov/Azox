@ModelType Product

<ul class="nav nav-tabs">
	<li class="nav-item">
		<a class="nav-link active" id="home-tab" data-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true">Общее</a>
	</li>
	<li class="nav-item">
		<a class="nav-link" id="price-tab" data-toggle="tab" href="#price" role="tab" aria-controls="price" aria-selected="false">Цены и наличие</a>
	</li>
	<li class="nav-item">
		<a class="nav-link" id="feature-tab" data-toggle="tab" href="#feature" role="tab" aria-controls="feature" aria-selected="false">Характеристики</a>
	</li>
	<li class="nav-item">
		<a class="nav-link" id="pictures-tab" data-toggle="tab" href="#pictures" role="tab" aria-controls="pictures" aria-selected="false">Изображения</a>
	</li>
</ul>

<div class="tab-content">
	<div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">
		<div class="row">
			<div class="col-md-9">
				<div class="row">
					<div class="form-group col-md-8">
						@Html.LabelFor(Function(model) model.Title, htmlAttributes:=New With {.class = "control-label required"})
						@Html.ValidationMessageFor(Function(model) model.Title, "", New With {.class = "text-danger"})
						@Html.EditorFor(Function(model) model.Title, New With {.htmlAttributes = New With {.class = "form-control"}})
					</div>

					<div class="form-group col-md-4">
						@Html.LabelFor(Function(model) model.Sku, htmlAttributes:=New With {.class = "control-label required"})
						@Html.ValidationMessageFor(Function(model) model.Sku, "", New With {.class = "text-danger"})
						@Html.EditorFor(Function(model) model.Sku, New With {.htmlAttributes = New With {.class = "form-control"}})
					</div>
				</div>

				<div class="form-group">
					@Html.LabelFor(Function(model) model.Content, htmlAttributes:=New With {.class = "control-label"})
					@Html.ValidationMessageFor(Function(model) model.Content, "", New With {.class = "text-danger"})
					@Html.EditorFor(Function(model) model.Content, New With {.htmlAttributes = New With {.class = "form-control"}})
				</div>

				<div class="row">
					<div class="form-group col-md-4">
						@Html.LabelFor(Function(model) model.Vendor, htmlAttributes:=New With {.class = "control-label"})
						@Html.ValidationMessageFor(Function(model) model.Vendor, "", New With {.class = "text-danger"})
						@Html.EditorFor(Function(model) model.Vendor, New With {.htmlAttributes = New With {.class = "form-control"}})
					</div>
					<div class="form-group col-md-4">
						@Html.LabelFor(Function(model) model.CategoryId, htmlAttributes:=New With {.class = "control-label"})
						@Html.ValidationMessageFor(Function(model) model.CategoryId, "", New With {.class = "text-danger"})
						@Html.DropDownList("CategoryId", Nothing, "", htmlAttributes:=New With {.class = "form-control"})
					</div>
					<div class="form-group col-md-4">
						@Html.LabelFor(Function(model) model.WarehouseId, htmlAttributes:=New With {.class = "control-label"})
						@Html.ValidationMessageFor(Function(model) model.WarehouseId, "", New With {.class = "text-danger"})
						@Html.DropDownList("WarehouseId", Nothing, "", htmlAttributes:=New With {.class = "form-control"})
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
	</div>
	<div class="tab-pane fade" id="price" role="tabpanel" aria-labelledby="price-tab">
		<div class="row">
			<div class="col-md-3">
				<div class="form-group">
					@Html.LabelFor(Function(model) model.Price, htmlAttributes:=New With {.class = "control-label required"})
					@Html.ValidationMessageFor(Function(model) model.Price, "", New With {.class = "text-danger"})
					@Html.EditorFor(Function(model) model.Price, New With {.htmlAttributes = New With {.class = "form-control", .type = "currency"}})
				</div>
				<div class="form-group">
					@Html.LabelFor(Function(model) model.OldPrice, htmlAttributes:=New With {.class = "control-label"})
					@Html.ValidationMessageFor(Function(model) model.OldPrice, "", New With {.class = "text-danger"})
					@Html.EditorFor(Function(model) model.OldPrice, New With {.htmlAttributes = New With {.class = "form-control", .type = "currency"}})
				</div>
			</div>
			<div class="col-md-3">
				<div class="form-group">
					@Html.LabelFor(Function(model) model.Availability, htmlAttributes:=New With {.class = "control-label"})
					@Html.ValidationMessageFor(Function(model) model.Availability, "", New With {.class = "text-danger"})
					@Html.EnumDropDownListFor(Function(model) model.Availability, New With {.class = "form-control"})
				</div>
				<div class="form-group">
					@Html.LabelFor(Function(model) model.AvailableQuantity, htmlAttributes:=New With {.class = "control-label"})
					@Html.ValidationMessageFor(Function(model) model.AvailableQuantity, "", New With {.class = "text-danger"})
					@Html.EditorFor(Function(model) model.AvailableQuantity, New With {.htmlAttributes = New With {.class = "form-control"}})
				</div>
			</div>
		</div>
	</div>
	<div class="tab-pane fade" id="feature" role="tabpanel" aria-labelledby="feature-tab">
		@*<div class="d-flex justify-content-end">
			<a href="@Url.Action("create", "parameters", New With {.productId = Model.Id, .returnUrl = Request.Url.PathAndQuery})" class="btn btn-primary btn-round" title="Добавить"><span class="fa fa-plus"></span></a>
		</div>*@
		@*@Html.Partial("_Parameters", Model.Parameters)*@
	</div>
	<div class="tab-pane fade" id="pictures" role="tabpanel" aria-labelledby="pictures-tab">
		@*<div class="d-flex justify-content-end">
			<a href="@Url.Action("create", "pictures", New With {.productId = Model.Id, .returnUrl = Request.Url.PathAndQuery})" class="btn btn-primary btn-round" title="Добавить"><span class="fa fa-plus"></span></a>
		</div>*@
		@*@Html.Partial("_Pictures", Model.Pictures)*@
	</div>
</div>
