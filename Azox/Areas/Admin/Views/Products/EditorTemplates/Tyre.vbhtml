@ModelType Tyre

<ul class="nav nav-tabs mt-3">
	<li class="nav-item">
		<a class="nav-link active" id="home-tab" data-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true">Общее</a>
	</li>
	<li class="nav-item">
		<a class="nav-link" id="price-tab" data-toggle="tab" href="#price" role="tab" aria-controls="price" aria-selected="false">Цены и остатки</a>
	</li>
	<li class="nav-item">
		<a class="nav-link" id="feature-tab" data-toggle="tab" href="#feature" role="tab" aria-controls="feature" aria-selected="false">Характеристики</a>
	</li>
	<li class="nav-item">
		<a class="nav-link" id="additional-tab" data-toggle="tab" href="#additional" role="tab" aria-controls="additional" aria-selected="false">Дополнительно</a>
	</li>
</ul>

<div class="tab-content">
	<div class="tab-pane show active" id="home" role="tabpanel" aria-labelledby="home-tab">
		<div class="row">
			<div class="col-md-9">
				<div class="row">
					<div class="form-group col-md-8">
						@Html.LabelFor(Function(model) model.Name, htmlAttributes:=New With {.class = "control-label required"})
						@Html.ValidationMessageFor(Function(model) model.Name, "", New With {.class = "text-danger"})
						@Html.EditorFor(Function(model) model.Name, New With {.htmlAttributes = New With {.class = "form-control"}})
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
			</div>
			<div class="col-md-3">
				<div class="form-group">
					@Html.LabelFor(Function(model) model.BrandId, htmlAttributes:=New With {.class = "control-label"})
					@Html.ValidationMessageFor(Function(model) model.BrandId, "", New With {.class = "text-danger"})
					@Html.DropDownListFor(Function(model) model.BrandId, Nothing, "", htmlAttributes:=New With {.class = "form-control"})
				</div>
				<div class="form-group">
					@Html.LabelFor(Function(model) model.WarehouseId, htmlAttributes:=New With {.class = "control-label"})
					@Html.ValidationMessageFor(Function(model) model.WarehouseId, "", New With {.class = "text-danger"})
					@Html.DropDownListFor(Function(model) model.WarehouseId, Nothing, "", htmlAttributes:=New With {.class = "form-control"})
				</div>
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
				<div class="form-group form-check">
					@Html.CheckBoxFor(Function(m) m.IsPublished, New With {.class = "form-check-input"})
					@Html.LabelFor(Function(m) m.IsPublished, New With {.class = "form-check-label"})
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
			<div class="form-group col-md-3">
				@Html.LabelFor(Function(model) model.OldPrice, htmlAttributes:=New With {.class = "control-label"})
				@Html.ValidationMessageFor(Function(model) model.OldPrice, "", New With {.class = "text-danger"})
				@Html.EditorFor(Function(model) model.OldPrice, New With {.htmlAttributes = New With {.class = "form-control text-right"}})
			</div>
			<div class="form-group col-md-1">
				@Html.LabelFor(Function(model) model.AvailableQuantity, htmlAttributes:=New With {.class = "control-label required"})
				@Html.ValidationMessageFor(Function(model) model.AvailableQuantity, "", New With {.class = "text-danger"})
				@Html.EditorFor(Function(model) model.AvailableQuantity, New With {.htmlAttributes = New With {.class = "form-control text-right"}})
			</div>
		</div>
	</div>
	<div class="tab-pane" id="feature" role="tabpanel" aria-labelledby="feature-tab">
		<div class="row">
			<div class="form-group col-md-3">
				@Html.LabelFor(Function(model) model.Season, htmlAttributes:=New With {.class = "control-label required"})
				@Html.ValidationMessageFor(Function(model) model.Season, "", New With {.class = "text-danger"})
				@Html.EnumDropDownListFor(Function(model) model.Season, "", New With {.class = "form-control"})
			</div>
			<div class="form-group col-md-3">
				@Html.LabelFor(Function(model) model.Type, htmlAttributes:=New With {.class = "control-label required"})
				@Html.ValidationMessageFor(Function(model) model.Type, "", New With {.class = "text-danger"})
				@Html.EnumDropDownListFor(Function(model) model.Type, "", New With {.class = "form-control"})
			</div>
			<div class="form-group col-md-3">
				@Html.LabelFor(Function(model) model.Diameter, htmlAttributes:=New With {.class = "control-label required"})
				@Html.ValidationMessageFor(Function(model) model.Diameter, "", New With {.class = "text-danger"})
				@Html.EditorFor(Function(model) model.Diameter, New With {.htmlAttributes = New With {.class = "form-control"}})
			</div>
			<div class="form-group col-md-3">
				@Html.LabelFor(Function(model) model.Width, htmlAttributes:=New With {.class = "control-label required"})
				@Html.ValidationMessageFor(Function(model) model.Width, "", New With {.class = "text-danger"})
				@Html.EditorFor(Function(model) model.Width, New With {.htmlAttributes = New With {.class = "form-control"}})
			</div>
			<div class="form-group col-md-3">
				@Html.LabelFor(Function(model) model.Height, htmlAttributes:=New With {.class = "control-label required"})
				@Html.ValidationMessageFor(Function(model) model.Height, "", New With {.class = "text-danger"})
				@Html.EditorFor(Function(model) model.Height, New With {.htmlAttributes = New With {.class = "form-control"}})
			</div>
			<div class="form-group col-md-3">
				@Html.LabelFor(Function(model) model.SpeedIndex, htmlAttributes:=New With {.class = "control-label"})
				@Html.ValidationMessageFor(Function(model) model.SpeedIndex, "", New With {.class = "text-danger"})
				@Html.EditorFor(Function(model) model.SpeedIndex, New With {.htmlAttributes = New With {.class = "form-control"}})
			</div>
			<div class="form-group col-md-3">
				@Html.LabelFor(Function(model) model.LoadIndex, htmlAttributes:=New With {.class = "control-label"})
				@Html.ValidationMessageFor(Function(model) model.LoadIndex, "", New With {.class = "text-danger"})
				@Html.EditorFor(Function(model) model.LoadIndex, New With {.htmlAttributes = New With {.class = "form-control"}})
			</div>
			<div class="form-group col-md-12">
				<div class="form-check form-check-inline">
					@Html.CheckBoxFor(Function(m) m.Thorn, New With {.class = "form-check-input"})
					@Html.LabelFor(Function(m) m.Thorn, New With {.class = "form-check-label"})
				</div>
				<div class="form-check form-check-inline">
					@Html.CheckBoxFor(Function(m) m.Runflat, New With {.class = "form-check-input"})
					@Html.LabelFor(Function(m) m.Runflat, New With {.class = "form-check-label"})
				</div>
				<div class="form-check form-check-inline">
					@Html.CheckBoxFor(Function(m) m.Reinforced, New With {.class = "form-check-input"})
					@Html.LabelFor(Function(m) m.Reinforced, New With {.class = "form-check-label"})
				</div>
			</div>
		</div>
	</div>
	<div class="tab-pane" id="additional" role="tabpanel" aria-labelledby="additional-tab">
		<div class="row">
			<div class="form-group col-md-9">
				@Html.LabelFor(Function(model) model.ImageUrl, htmlAttributes:=New With {.class = "control-label"})
				@Html.ValidationMessageFor(Function(model) model.ImageUrl, "", New With {.class = "text-danger"})
				@Html.EditorFor(Function(model) model.ImageUrl, New With {.htmlAttributes = New With {.class = "form-control"}})
			</div>

			<div class="form-group col-md-9">
				@Html.LabelFor(Function(model) model.ThumbnailUrl, htmlAttributes:=New With {.class = "control-label"})
				@Html.ValidationMessageFor(Function(model) model.ThumbnailUrl, "", New With {.class = "text-danger"})
				@Html.EditorFor(Function(model) model.ThumbnailUrl, New With {.htmlAttributes = New With {.class = "form-control"}})
			</div>
		</div>
	</div>
</div>
<div class="form-group">
	<button class="btn btn-primary">Сохранить</button>
</div>
