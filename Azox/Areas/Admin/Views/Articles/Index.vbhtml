@ModelType IEnumerable(Of ArticleAdminViewModel)
@Code
	ViewBag.Title = "Все статьи"
End Code

@Section Toolbar
	<a href="@Url.Action("create")" class="btn">
		<span class="fa fa-plus"></span>
		<span>Добавить</span>
	</a>
End Section

<header>
	<h1>@ViewBag.Title <sup>@CInt(ViewBag.TotalCount).ToString("статья", "статьи", "статей")</sup></h1>
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
					<th class="64"></th>
				</tr>
			</thead>
			<tbody>
				@For Each item In Model
					@<tr>
						<td>
							@Html.ActionLink(item.Name, "edit", New With {.id = item.Id, .returnUrl = Request.Url.PathAndQuery}, New With {.title = "Изменить"})
							<div>
								<small class="form-text text-muted">@item.Slug</small>
							</div>
						</td>
						<td class="text-right">
							<a href="@Url.Action("details", "articles", New With {.id = item.Id, .area = ""})" title="Посмотреть" target="_blank"><span class="fa fa-external-link"></span></a>
							<a href="@Url.Action("delete", New With {.id = item.Id, .returnUrl = Request.Url.PathAndQuery})" title="Удалить"><span class="fa fa-remove"></span></a>
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


