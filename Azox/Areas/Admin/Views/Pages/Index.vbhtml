@ModelType IEnumerable(Of Azox.Page)
@Code
	ViewBag.Title = "Управление страницами"
End Code

<header>
	<h1 class="heading">@ViewBag.Title <sup>@CInt(ViewBag.TotalCount).ToString("страница", "страницы", "страниц")</sup></h1>
	@Html.Partial("_Alert")
	@Html.Partial("_Filter", ViewBag.Filter)
	@Html.Pagination(New With {.class = "pagination"})
</header>

<article>
	@If Model.Any Then
		@<table class="table table-hover">
			<thead>
				<tr>
					<th>
						@Html.DisplayNameFor(Function(model) model.Name)
					</th>
					<th width="44"></th>
				</tr>
			</thead>
			<tbody>
				@For Each item In Model
					@<tr>
						<td>
							@Html.ActionLink(item.Name, "edit", New With {.id = item.Id, .returnUrl = Request.Url.PathAndQuery}, New With {.title = "Изменить"})
						</td>
						<td class="text-right">
							<a href="@Url.Action(item.ActionName, item.ControllerName, New With {.area = ""})" title="Посмотреть" target="_blank"><span class="fa fa-eye"></span></a>
						</td>
					</tr>
				Next
			</tbody>
		</table>

		@Html.Pagination(New With {.class = "pagination"})
	Else
		@<p class="lead text-center">Список пуст.</p>
	End If
</article>
