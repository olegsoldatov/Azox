@ModelType IEnumerable(Of Customer)
@Code
	ViewBag.Title = "Управление клиентами"
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
			<button class="dropdown-item" data-toggle="modal" data-target="#deleteModal">Удалить</button>
		</div>
	</div>
End Section

<header>
	<h1 class="heading">@ViewBag.Title <sup>@CInt(ViewBag.Count).ToString("клиент", "клиента", "клиентов")</sup></h1>
	@Html.Partial("_Filter", ViewBag.Filter)
	@Html.Pagination(New With {.class = "pagination"})
</header>

<article>
	@If Model.Any Then
		@<form id="contentForm" method="post" action="@Url.Action("index")">
			@Html.AntiForgeryToken
			@Html.Partial("_Delete")
			<div class="table-responsive">
				<table class="table table-hover">
					<thead>
						<tr>
							<th width="40">
								<input type="checkbox" data-toggle="check-all" aria-controls="id" />
							</th>
							<th>
								@Html.DisplayNameFor(Function(model) model.Name)
							</th>
							<th>
								@Html.DisplayNameFor(Function(model) model.Phone)
							</th>
							<th>
								@Html.DisplayNameFor(Function(model) model.Email)
							</th>
							<th>
								@Html.DisplayNameFor(Function(model) model.Comment)
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
									@Html.ActionLink(item.Name, "edit", New With {item.Id}, New With {.title = "Изменить"})
								</td>
								<td>
									<a href="tel:+@item.Phone" title="Позвонить">+@item.Phone</a>
								</td>
								<td>
									@Html.DisplayFor(Function(model) item.Email)
								</td>
								<td>
									@item.Comment
								</td>
								<td class="text-right">
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
