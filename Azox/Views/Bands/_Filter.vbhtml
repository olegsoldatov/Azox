@ModelType BrandFilterViewModel
<form method="get">
	<div class="form-group">
		<div class="input-group">
			@Html.TextBoxFor(Function(model) model.SearchString, New With {.class = "form-control form-control-sm", .placeholder = "Поиск"})
			<div class="input-group-append" id="button-addon4">
				<button class="btn btn-outline-secondary btn-sm" type="submit" title="Искать"><span class="fa fa-search"></span></button>
				<button class="btn btn-outline-secondary btn-sm" type="reset" title="Сбросить" onclick="document.location('@Url.Action("manage")')"><span class="fa fa-times"></span></button>
			</div>
		</div>
	</div>
</form>
