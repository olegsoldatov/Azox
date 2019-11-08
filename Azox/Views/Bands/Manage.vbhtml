@ModelType IEnumerable(Of Brand)
@Code
	ViewBag.Title = "Бренды"
	Layout = "~/Views/Shared/_Dashboard.vbhtml"
End Code

@Section Toolbar
	<a href="@Url.Action("create")" class="btn">
		<span class="fa fa-plus"></span>
		<span>Добавить</span>
	</a>
End Section

<header>
	<h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
	@Html.Partial("_Alert")

	@Html.Partial("_Filter", ViewBag.Filter)

	@If Model.Any Then

		@<table class="table table-hover">
			<thead>
				<tr>
					<th>
						@Html.DisplayNameFor(Function(model) model.Name)
					</th>
					<th>
						@Html.DisplayNameFor(Function(model) model.Slug)
					</th>
					<th>
						@Html.DisplayNameFor(Function(model) model.Products)
					</th>
					<th width="84"></th>
				</tr>
			</thead>
			<tbody>
				@For Each item In Model
					@<tr>
						<td>
							@Html.ActionLink(item.Name, "edit", New With {.id = item.Id, .returnUrl = Request.Url.AbsolutePath}, New With {.title = "Изменить"})
						</td>
						<td>
							@Html.DisplayFor(Function(model) item.Slug)
						</td>
						<td>
							@If item.Products.Any Then
								@Html.ActionLink(item.Products.Count, "manage", "products", New With {.brandId = item.Id}, New With {.title = "Продукция этого бренда"})
							Else
								@Html.DisplayFor(Function(model) item.Products.Count)
							End If
						</td>
						<td class="text-right">
							<a href="@Url.Action("edit", New With {.id = item.Id, .returnUrl = Request.Url.PathAndQuery})" title="Изменить"><span class="fa fa-pencil"></span></a>
							<a href="@Url.Action("details", New With {.id = item.Id})" title="Посмотреть" target="_blank"><span class="fa fa-eye"></span></a>
							<a href="@Url.Action("delete", New With {.id = item.Id, .returnUrl = Request.Url.PathAndQuery})" title="Удалить"><span class="fa fa-trash"></span></a>
						</td>
					</tr>
				Next
			</tbody>
		</table>
	Else
		@<p class="lead text-center">Список пуст.</p>
	End If

	@Html.Pagination(New With {.class = "pagination"})
</article>
