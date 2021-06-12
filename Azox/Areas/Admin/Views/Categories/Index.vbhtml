@ModelType IEnumerable(Of Category)
@Code
	ViewBag.Title = "Управление категориями"
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
			<button class="dropdown-item" data-toggle="modal" data-target="#changeModal">Изменить</button>
			<button class="dropdown-item" data-toggle="modal" data-target="#deleteModal">Удалить</button>
		</div>
	</div>
End Section

<header>
	<h1 class="heading">@ViewBag.Title <sup>@CInt(ViewBag.TotalCount).ToString("категория", "категории", "категорий")</sup></h1>
	@Html.Partial("_Filter", ViewBag.Filter)
	@Html.Pagination(New With {.class = "pagination"})
</header>

<article>
	@If Model.Any Then
		@<form id="contentForm" method="post" action="@Url.Action("index")">
			@Html.AntiForgeryToken
			@Html.Partial("_Delete")
			@Html.Partial("_Change", New CategoryChangeViewModel)
			<div class="table-responsive">
				<table class="table table-hover">
					<thead>
						<tr>
							<th width="40">
								<input type="checkbox" data-toggle="check-all" aria-controls="id" />
							</th>
							<th>
								@Html.DisplayNameFor(Function(model) model.Title)
							</th>
							<th class="text-right" width="100">
								@Html.DisplayNameFor(Function(model) model.Products)
							</th>
							<th class="text-right" width="100">
								@Html.DisplayNameFor(Function(model) model.Order)
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
									@Html.ActionLink(item.Title, "edit", New With {item.Id}, New With {.title = "Изменить"})
									<small class="d-block text-muted">@item.Name</small>
									@If item.Draft Then
										@<small class="d-block"><strong>@Html.DisplayNameFor(Function(model) model.Draft)</strong></small>
									End If
								</td>
								<td class="text-right">
									@Html.ActionLink(item.Products.Count, "index", "products", New With {.categoryId = item.Id}, Nothing)
								</td>
								<td class="text-right">
									@item.Order
								</td>
								<td class="text-right">
									<a href="@Url.Action("details", "categories", New With {.area = "", item.Id})" title="Посмотреть" target="_blank"><span class="fa fa-external-link"></span></a>
									<a href="@Url.Action("delete", New With {item.Id})" title="Удалить"><span class="fa fa-remove"></span></a>
								</td>
							</tr>
						Next
					</tbody>
				</table>
			</div>
		</form>
	Else
		@<p class="lead text-center">Список пуст.</p>
	End If

	@Html.Pagination(New With {.class = "pagination"})
</article>
