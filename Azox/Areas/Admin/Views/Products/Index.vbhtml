@ModelType IEnumerable(Of ProductAdminItem)
@Code
	ViewBag.Title = "Управление товарами"
End Code

@Section Toolbar
	<a href="@Url.Action("create")" class="btn">
		<span class="fa fa-plus"></span>
		<span>Добавить</span>
	</a>
	<div class="btn-group">
		<button class="btn dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
			<span class="fa fa-ellipsis-h"></span>
			<span>Действия</span>
		</button>
		<div class="dropdown-menu">
			<a href="#" class="dropdown-item" data-toggle="modal" data-target="#changeModal">Изменить</a>
			<a href="#" class="dropdown-item" data-toggle="modal" data-target="#deleteModal">Удалить</a>
		</div>
	</div>
	<a href="@Url.Action("clearCache")" class="btn" title="@String.Format("Доступно памяти: {0} МБ ({1}%)", Cache.EffectivePrivateBytesLimit / 1048576, Cache.EffectivePercentagePhysicalMemoryLimit)">
		<span class="fa fa-microchip"></span>
		<span>Обновить кэш</span>
	</a>
End Section

<header>
	<h1 class="heading">@ViewBag.Title <sup>@CInt(ViewBag.Count).ToString("товар", "товара", "товаров")</sup></h1>
	@Html.Partial("_Filter", ViewBag.Filter)
	@Html.Pagination(New With {.class = "pagination"})
</header>

<article>
	@If Model.Any Then
		@Using Html.BeginForm(Nothing, Nothing, New With {.returnUrl = Request.Url.PathAndQuery}, FormMethod.Post, New With {.id = "contentForm"})
			@Html.AntiForgeryToken
			@*@Html.Partial("_Change", New ProductChangeViewModel)*@
			@Html.Partial("_Delete")
			@<table class="table table-hover">
				<thead>
					<tr>
						<th width="40">
							<input type="checkbox" data-toggle="check-all" aria-controls="id" />
						</th>
						<th>
							@Html.DisplayNameFor(Function(model) model.Title)
						</th>
						<th>
							@Html.DisplayNameFor(Function(model) model.Brand)
						</th>
						<th>
							@Html.DisplayNameFor(Function(model) model.Category)
						</th>
						<th class="text-right text-nowrap" width="120">
							@Html.DisplayNameFor(Function(model) model.Offers)
						</th>
						<th width="100"></th>
					</tr>
				</thead>
				<tbody>
					@For Each item In Model
						@<tr>
							<td>
								<input type="checkbox" name="id" value="@item.Id" />
							</td>
							<td>
								@Html.ActionLink(item.Title, "edit", New With {.id = item.Id}, New With {.title = item.Title})
								<small class="d-block text-muted">@item.Sku</small>
								@If item.Draft Then
									@<small class="d-block"><b>@Html.DisplayNameFor(Function(model) model.Draft)</b></small>
								End If
							</td>
							<td>
								@If IsNothing(item.BrandId) OrElse item.BrandId.Equals(Guid.Empty) Then
									@<span class="text-muted">@item.Brand</span>
								Else
									@Html.ActionLink(item.Brand, "index", "brands", New With {.searchText = item.Brand}, New With {.title = item.Brand})
								End If
							</td>
							<td>
								@If IsNothing(item.CategoryId) OrElse item.CategoryId.Equals(Guid.Empty) Then
									@<span class="text-muted">@item.Category</span>
								Else
									@Html.ActionLink(item.Category, "index", "categories", New With {.searchText = item.Category}, New With {.title = item.Category})
								End If
							</td>
							<td class="text-right">
								@item.Offers
							</td>
							<td class="text-right">
								<a href="@Url.Action("product", "catalog", New With {.area = "", .id = item.Id})" title="Посмотреть" target="_blank"><span class="fa fa-external-link"></span></a>
								<a href="@Url.Action("delete", New With {.id = item.Id})" title="Удалить"><span class="fa fa-remove"></span></a>
							</td>
						</tr>
					Next
				</tbody>
			</table>
		End Using
	Else
		@<p class="lead text-center">Список пуст.</p>
	End If

	@Html.Pagination(New With {.class = "pagination"})
</article>
