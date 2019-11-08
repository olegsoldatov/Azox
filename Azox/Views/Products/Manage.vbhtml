@ModelType IEnumerable(Of Product)
@Code
	ViewBag.Title = "Продукция"
	Layout = "~/Views/Shared/_Dashboard.vbhtml"
End Code

@Section Toolbar
	<a href="@Url.Action("create")" class="btn">
		<span class="fa fa-plus"></span>
		<span>Добавить</span>
	</a>
	<a href="@Url.Action("upload")" class="btn">
		<span class="fa fa-download"></span>
		<span>Импорт</span>
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
						@Html.DisplayNameFor(Function(model) model.Sku)
					</th>
					<th>
						@Html.DisplayNameFor(Function(model) model.Category)
					</th>
					<th>
						@Html.DisplayNameFor(Function(model) model.Brand)
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
							@Html.ActionLink(item.Name, "edit", New With {.id = item.Id, .returnUrl = Request.Url.PathAndQuery}, New With {.title = "Изменить"})
						</td>
						<td>
							@Html.DisplayFor(Function(modelItem) item.Sku)
						</td>
						<td>
							@Html.DisplayFor(Function(modelItem) item.Category.Name)
						</td>
						<td>
							@Html.DisplayFor(Function(modelItem) item.Brand.Name)
						</td>
						<td>
							@Html.DisplayFor(Function(modelItem) item.Price)
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
