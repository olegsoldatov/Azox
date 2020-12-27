@ModelType IEnumerable(Of BrandAdminItem)
@Code
	ViewBag.Title = "Все бренды"
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
			<a href="#" class="dropdown-item" data-toggle="modal" data-target="#deleteModal">Удалить</a>
		</div>
	</div>
End Section

<header>
	<h1>@ViewBag.Title <sup>@CInt(ViewBag.Count).ToString("бренд", "бренда", "брендов")</sup></h1>
	@Html.Partial("_Filter", ViewBag.Filter)
	@Html.Pagination(New With {.class = "pagination"})
</header>

<article>
	@If Model.Any Then
		@Using Html.BeginForm(Nothing, Nothing, New With {.returnUrl = Request.Url.PathAndQuery}, FormMethod.Post, New With {.id = "contentForm"})
			@Html.AntiForgeryToken
			@Html.Partial("_Delete")
			@<table class="table table-hover">
				<thead>
					<tr>
						<th width="40">
							<input type="checkbox" />
						</th>
						<th>
							@Html.DisplayNameFor(Function(model) model.Title)
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
								@Html.ActionLink(item.Title, "edit", New With {.id = item.Id}, New With {.title = "Изменить"})
								<div><small class="text-muted">@Html.DisplayFor(Function(model) item.Name)</small></div>
								@Html.DisplayFor(Function(model) item.Draft)
							</td>
							<td>
								@Html.ActionLink(item.Products, "index", "products", New With {.brandId = item.Id}, New With {.title = "Товары"})
							</td>
							<td class="text-right">
								@Html.DisplayFor(Function(model) item.Order)
							</td>
							<td class="text-right">
								<a href="@Url.Action("brands", "catalog", New With {.id = item.Name.ToLower, .area = ""})" title="Посмотреть" target="_blank"><span class="fa fa-external-link"></span></a>
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

