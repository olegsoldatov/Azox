@ModelType IEnumerable(Of ProductAdminViewModel)
@Code
	ViewBag.Title = "Управление продуктами"
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
			<a href="#" class="dropdown-item" onclick="deleteCheckedItems('Вы действительно хотите удалить выбранные продукты?', '@Url.Action("DeleteChecked")', $('#contentForm [type=checkbox]:checked')); return false;">Удалить</a>
		</div>
	</div>
End Section

<header>
	<h1 class="heading">@ViewBag.Title <sup>@CInt(ViewBag.TotalCount).ToString("продукт", "продукта", "продуктов")</sup></h1>
	@Html.Partial("_Alert")
	@Html.Partial("_Filter", ViewBag.Filter)
	@Html.Pagination(New With {.class = "pagination"})
</header>

<article>
	@If Model.Any Then
		@<form id="contentForm">
			<table class="table table-hover">
				<thead>
					<tr>
						<th width="40">
							<input type="checkbox" />
						</th>
						<th>
							@Html.DisplayNameFor(Function(model) model.Name)
						</th>
						<th>
							@Html.DisplayNameFor(Function(model) model.CategoryName)
						</th>
						<th>
							@Html.DisplayNameFor(Function(model) model.BrandName)
						</th>
						<th>
							@Html.DisplayNameFor(Function(model) model.WarehouseName)
						</th>
						<th>
							@Html.DisplayNameFor(Function(model) model.Price)
						</th>
						<th width="84"></th>
					</tr>
				</thead>
				<tbody>
					@For Each item In Model
						@<tr>
							<td>
								<input type="checkbox" name="id" value="@item.Id" />
							</td>
							<td>
								@Html.ActionLink(item.Name, "edit", New With {.id = item.Id, .returnUrl = Request.Url.PathAndQuery}, New With {.title = item.Name})
								<div>
									<small class="text-muted">@Html.DisplayFor(Function(modelItem) item.Sku)</small>
								</div>
							</td>
							<td>
								@Html.DisplayFor(Function(modelItem) item.CategoryName)
							</td>
							<td>
								@Html.DisplayFor(Function(modelItem) item.BrandName)
							</td>
							<td>
								@Html.DisplayFor(Function(modelItem) item.WarehouseName)
							</td>
							<td>
								@Html.DisplayFor(Function(modelItem) item.Price)
								@If item.OldPrice > Decimal.Zero AndAlso Not item.OldPrice = item.Price Then
									@<div>
										<small class="text-muted"><s>@Html.DisplayFor(Function(modelItem) item.OldPrice)</s></small>
									</div>
								End If
							</td>
							<td class="text-right">
								@Html.DisplayFor(Function(model) item.IsPublished)
								<a href="@Url.Action("details", New With {.area = "", .id = item.Id})" title="Посмотреть" target="_blank"><span class="fa fa-eye"></span></a>
								<a href="@Url.Action("delete", New With {.id = item.Id, .returnUrl = Request.Url.PathAndQuery})" title="Удалить"><span class="fa fa-trash"></span></a>
							</td>
						</tr>
					Next
				</tbody>
			</table>
		</form>
	Else
		@<p class="lead text-center">Список пуст.</p>
	End If

	@Html.Pagination(New With {.class = "pagination"})
</article>
