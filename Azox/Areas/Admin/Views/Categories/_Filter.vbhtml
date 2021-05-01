@ModelType FilterViewModel
@Using Html.BeginForm(Nothing, Nothing, FormMethod.Get, New With {.data_toggle = "filter"})
	@<div class="form-group">
		<div class="input-group">
			@Html.TextBoxFor(Function(model) model.SearchText, New With {.class = "form-control form-control-sm", .placeholder = "Поиск"})
			<div class="input-group-append">
				<button class="btn btn-outline-secondary btn-sm" type="submit" title="Искать"><span class="fa fa-search"></span></button>
				<a href="@Request.Url.AbsolutePath" class="btn btn-outline-secondary btn-sm" title="Сбросить"><span class="fa fa-times"></span></a>
			</div>
		</div>
	</div>
End Using
