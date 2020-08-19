@ModelType IEnumerable(Of Product)
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
End Section

<header>
	<h1 class="heading">@ViewBag.Title <sup>@CInt(ViewBag.TotalCount).ToString("товар", "товара", "товаров")</sup></h1>
	@Html.Partial("_Alert")
	@Html.Partial("_Filter", ViewBag.Filter)
	@Html.Pagination(New With {.class = "pagination"})
</header>

<article>
	@If Model.Any Then
		@<form id="contentForm" method="post">
			@Html.AntiForgeryToken
			@Html.Hidden("ReturnUrl", Request.Url.PathAndQuery)
			@*@Html.Partial("_Change", New ProductChangeViewModel)
				@Html.Partial("_Delete")*@
			<table class="table table-hover">
				<thead>
					<tr>
						<th width="40">
							<input type="checkbox" data-toggle="check-all" aria-controls="id" />
						</th>
						<th>
							@Html.DisplayNameFor(Function(model) model.Title)
						</th>
						<th>
							@Html.DisplayNameFor(Function(model) model.Vendor)
						</th>
						<th>
							@Html.DisplayNameFor(Function(model) model.Category)
						</th>
						<th>
							@Html.DisplayNameFor(Function(model) model.Warehouse)
						</th>
						<th>
							@Html.DisplayNameFor(Function(model) model.Availability)
						</th>
						<th>
							@Html.DisplayNameFor(Function(model) model.Price)
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
								@Html.ActionLink(item.Title, "edit", New With {.id = item.Id, .returnUrl = Request.Url.PathAndQuery}, New With {.title = item.Title})
								<div>
									<small class="text-muted">@Html.DisplayFor(Function(model) item.Sku)</small>
								</div>
								@If Not item.IsPublished Then
									@<div>
										<small><b>Скрыт</b></small>
									</div>
								End If
							</td>
							<td>
								@Html.DisplayFor(Function(model) item.Vendor)
							</td>
							<td>
								...
							</td>
							<td>
								...
							</td>
							<td>
								@Html.DisplayFor(Function(model) item.Availability)
							</td>
							<td>
								@Html.DisplayFor(Function(model) item.Price)
								@If item.OldPrice > Decimal.Zero AndAlso Not item.OldPrice = item.Price Then
									@<div>
										<small class="text-muted"><s>@Html.DisplayFor(Function(model) item.OldPrice)</s></small>
									</div>
								End If
							</td>
							<td class="text-right">
								<a href="@Url.Action("product", "catalog", New With {.area = "", .id = item.Id})" title="Посмотреть" target="_blank"><span class="fa fa-external-link"></span></a>
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
