@ModelType IEnumerable(Of Product)
@Code
	ViewBag.Title = "Продукция"
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

	@If Model.Any Then
		@<table class="table table-hover">
			<thead>
				<tr>
					<th>
						@Html.DisplayNameFor(Function(model) model.Title)
					</th>
					<th>
						@Html.DisplayNameFor(Function(model) model.Sku)
					</th>
					<th width="84"></th>
				</tr>
			</thead>
			<tbody>
				@For Each item In Model
					@<tr>
						<td>
							@Html.ActionLink(item.Title, "edit", New With {.id = item.Id, .returnUrl = Request.Url.PathAndQuery}, New With {.title = "Изменить"})
						</td>
						<td>
							@Html.DisplayFor(Function(modelItem) item.Sku)
						</td>
						<td class="text-right">
							<a href="@Url.Action("edit", New With {.id = item.Id, .returnUrl = Request.Url.PathAndQuery})" title="Изменить"><span class="fa fa-pencil"></span></a>
							<a href="@Url.Action("details", New With {.id = item.Id, .returnUrl = Request.Url.PathAndQuery})" title="Посмотреть" target="_blank"><span class="fa fa-eye"></span></a>
							<a href="@Url.Action("delete", New With {.id = item.Id})" title="Удалить"><span class="fa fa-trash"></span></a>
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
