@ModelType FilterViewModel
<form method="get">
	<div class="form-group">
		<div class="input-group">
			@Html.TextBoxFor(Function(model) model.SearchText, New With {.class = "form-control form-control-sm", .placeholder = "Поиск"})
			<div class="input-group-append">
				<button class="btn btn-outline-secondary btn-sm" type="submit" title="Искать"><span class="fa fa-search"></span></button>
				<a href="@Url.Action("index")" class="btn btn-outline-secondary btn-sm" title="Сбросить"><span class="fa fa-times"></span></a>
			</div>
		</div>
	</div>
</form>
