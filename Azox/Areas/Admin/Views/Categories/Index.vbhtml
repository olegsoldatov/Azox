@ModelType IEnumerable(Of CategoryAdminItem)
@Code
	ViewBag.Title = "Все категории"
End Code

@Section Toolbar
	<a href="@Url.Action("create")" class="btn">
		<span class="fa fa-plus"></span>
		<span>Добавить</span>
	</a>
	<a href="@Url.Action("uploadCache")" class="btn" title="@String.Format("Доступно памяти: {0} МБ ({1}%)", Cache.EffectivePrivateBytesLimit / 1048576, Cache.EffectivePercentagePhysicalMemoryLimit)">
		<span class="fa fa-microchip"></span>
		<span>Обновить кэш</span>
	</a>
	<div class="btn-group">
		<button class="btn dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
			<span class="fa fa-ellipsis-h"></span>
			<span>Действия</span>
		</button>
		<div class="dropdown-menu">
			<a href="#delete-modal" class="dropdown-item" data-toggle="modal">Удалить</a>
		</div>
	</div>
End Section

<header>
	<h1 class="heading">@ViewBag.Title <sup>@CInt(ViewBag.Count).ToString("категория", "категории", "категорий")</sup></h1>
	@Html.Partial("_Filter", ViewBag.Filter)
	@Html.Pagination(New With {.class = "pagination"})
</header>

<article>
	@If Model.Any Then
		@Using Html.BeginForm(New With {.returnUrl = If(Request.QueryString("ReturnUrl"), Request.Url.PathAndQuery)})
			@Html.AntiForgeryToken
			@Html.Partial("_Delete")
			@<div class="table-responsive">
				<table class="table table-hover">
					<thead>
						<tr>
							<th width="40">
								<input type="checkbox" />
							</th>
							<th>
								@Html.DisplayNameFor(Function(model) model.Title)
							</th>
							<th class="text-right">
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
									@Html.ActionLink(item.Title, "edit", New With {.id = item.Id}, New With {.title = "Изменить"})
									<small class="d-block text-muted">@Html.DisplayFor(Function(m) item.Name)</small>
									@If item.Draft Then
										@<small class="d-block"><b>@Html.DisplayNameFor(Function(model) model.Draft)</b></small>
									End If
								</td>
								<td class="text-right">
									@Html.ActionLink(item.Products, "index", "products", New With {.categoryId = item.Id}, Nothing)
								</td>
								<td class="text-right">
									@Html.DisplayFor(Function(model) item.Order)
								</td>
								<td class="text-right">
									<a href="@Url.Action("details", New With {.id = item.Id})" title="Посмотреть" target="_blank"><span class="fa fa-external-link"></span></a>
									<a href="@Url.Action("delete", New With {.id = item.Id})" title="Удалить"><span class="fa fa-remove"></span></a>
								</td>
							</tr>
						Next
					</tbody>
				</table>
			</div>
		End Using
	Else
		@<p class="lead text-center">Список пуст.</p>
	End If

	@Html.Pagination(New With {.class = "pagination"})
</article>

