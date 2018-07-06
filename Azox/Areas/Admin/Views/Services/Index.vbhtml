@ModelType IEnumerable(Of Service)
@Code
	ViewBag.Title = "Услуги"
End Code

@Section Toolbar
	<a class="button" href="@Url.Action("create")"><span class="fa fa-plus">&nbsp;&nbsp;</span>Добавить</a>
End Section

<h1>@ViewBag.Title</h1>
<hr />

@If Not IsNothing(TempData("Message")) Then
	@<div class="alert alert-info alert-dismissible" role="alert">
		<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
		@TempData("Message")
	</div>
End If

<div class="row btn-toolbar">
	<div class="col-md-3 col-md-offset-9">
		@Using Html.BeginForm("index", "services", FormMethod.Get)
			@<label for="searchString" class="sr-only">Поиск</label>
			@<div class="input-group">
				<input type="search" name="searchString" id="searchString" class="form-control" placeholder="Поиск" value="@Request.QueryString("searchString")" />
				<span class="input-group-btn">
					<button class="btn btn-default" id="searchButton"><span class="fa fa-search"></span></button>
				</span>
			</div>
		End Using
	</div>
</div>

@If Model.Any Then
	@<div>
		<div class="btn-group btn-group-sm">
			<button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
				Действия <span class="caret"></span>
			</button>
			<ul class="dropdown-menu" data-wrap="table-actions">
				<li><a href="@Url.Action("index")" data-url="@Url.Action("DeleteChecked")">Удалить</a></li>
			</ul>
		</div>
	</div>
	@<table class="table table-hover tablesorter">
		<thead>
			<tr>
				<th>
					<input type="checkbox" />
				</th>
				<th>
					@Html.DisplayNameFor(Function(model) model.Title)
				</th>
				<th class="text-right">
					@Html.DisplayNameFor(Function(model) model.Order)
				</th>
				<th></th>
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
					</td>
					<td class="text-right">
						@Html.DisplayFor(Function(m) item.Order)
					</td>
					<td class="text-right text-nowrap">
						@Html.DisplayFor(Function(modelItem) item.Draft) |
						<a href="@Url.Action("details", New With {.id = item.Id, .area = ""})" title="Посмотреть" target="_blank"><span class="fa fa-eye"></span></a> |
						<a href="@Url.Action("delete", New With {.id = item.Id})" title="Удалить"><span class="fa fa-trash"></span></a>
					</td>
				</tr>
			Next
		</tbody>
	</table>
Else
	@<p class="lead text-center">Список пуст.</p>
End If

<nav>
	@Html.Pagination(New With {.class = "pagination"})
</nav>

@Section Scripts
	<script>
		$(function () {
			$(".tablesorter").tablesorter({
				headers: { 0: { sorter: false }, 3: { sorter: false } }
			});
		});
	</script>
End Section