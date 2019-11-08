@ModelType IEnumerable(Of Warehouse)
@Code
	ViewBag.Title = "Управление складами"
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
			<a href="#" class="dropdown-item" onclick="deleteCheckedItems('Вы действительно хотите удалить выбранные склады?', '@Url.Action("DeleteChecked")', $('#contentForm [type=checkbox]:checked')); return false;">Удалить</a>
		</div>
	</div>
End Section

<header>
	<h1 class="heading">@ViewBag.Title <sup>@CInt(ViewBag.TotalCount).ToString("склад", "склада", "складов")</sup></h1>
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
							@Html.DisplayNameFor(Function(model) model.Company)
						</th>
						<th>
							@Html.DisplayNameFor(Function(model) model.City)
						</th>
						<th>
							@Html.DisplayNameFor(Function(model) model.DeliveryDays)
						</th>
						<th>
							@Html.DisplayNameFor(Function(model) model.Products)
						</th>
						<th class="text-right" width="100">
							@Html.DisplayNameFor(Function(model) model.Order)
						</th>
						<th width="64"></th>
					</tr>
				</thead>
				<tbody>
					@For Each item In Model
						@<tr>
							<td>
								<input type="checkbox" name="id" value="@item.Id" />
							</td>
							<td>
								@Html.ActionLink(item.Name, "edit", New With {.id = item.Id, .returnUrl = Request.Url.PathAndQuery}, New With {.title = "Изменить"})
								<div>
									<small class="text-muted">@item.Slug</small>
								</div>
							</td>
							<td>
								@Html.DisplayFor(Function(model) item.Company)
							</td>
							<td>
								@Html.DisplayFor(Function(model) item.City)
							</td>
							<td>
								@Html.DisplayFor(Function(model) item.DeliveryDays)
							</td>
							<td>
								@If item.Products.Any Then
									@Html.ActionLink(item.Products.Count, "index", "watches", New With {.warehouseId = item.Id}, Nothing)
								Else
									@<text>0</text>
								End If
							</td>
							<td class="text-right">
								@item.Order
							</td>
							<td class="text-right">
								@Html.DisplayFor(Function(model) item.IsPublished)
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
