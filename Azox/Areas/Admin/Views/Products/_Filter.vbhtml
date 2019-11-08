@ModelType ProductFilterViewModel
<form method="get">
	<div class="form-group">
		<div class="input-group">
			@Html.TextBoxFor(Function(model) model.SearchString, New With {.class = "form-control form-control-sm", .placeholder = "Поиск"})
			<div class="input-group-append">
				<button class="btn btn-outline-secondary btn-sm" type="submit" title="Искать"><span class="fa fa-search"></span></button>
				<a href="@Url.Action("index")" class="btn btn-outline-secondary btn-sm" title="Сбросить"><span class="fa fa-times"></span></a>
			</div>
		</div>
	</div>
	<div class="row">
		<div class="col-md-10">
			<div class="row">
				<div class="form-group col-md-4">
					@Html.DropDownListFor(Function(model) model.CategoryId, Nothing, "Все категории", New With {.class = "form-control form-control-sm"})
				</div>
				<div class="form-group col-md-4">
					@Html.DropDownListFor(Function(model) model.BrandId, Nothing, "Все бренды", New With {.class = "form-control form-control-sm"})
				</div>
				<div class="form-group col-md-4">
					@Html.DropDownListFor(Function(model) model.WarehouseId, Nothing, "Все склады", New With {.class = "form-control form-control-sm"})
				</div>
			</div>
		</div>
		<div class="form-group col-md-1">
			<a href="@Url.Action("index")" class="btn btn-outline-secondary btn-sm btn-block">Сбросить</a>
		</div>
		<div class="form-group col-md-1">
			<button class="btn btn-secondary btn-sm btn-block" type="submit">Применить</button>
		</div>
	</div>
</form>
