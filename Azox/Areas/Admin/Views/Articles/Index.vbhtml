@ModelType IEnumerable(Of Article)
@Code
	ViewBag.Title = "Статьи"
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

<div class="btn-toolbar">
	@Using Html.BeginForm("index", "articles", FormMethod.Get)
		@<label for="searchString" class="sr-only">Поиск</label>
		@<div class="input-group">
			<input type="search" name="searchString" id="searchString" class="form-control" placeholder="Поиск" value="@Request.QueryString("searchString")" />
			<span class="input-group-btn">
				<button class="btn btn-default" id="searchButton" title="Искать"><span class="fa fa-search"></span></button>
			</span>
		</div>
	End Using
</div>

<table class="table table-condensed table-hover tablesorter">
	<thead>
		<tr>
			<th>
				@Html.DisplayNameFor(Function(model) model.Title)
			</th>
			<th>
				@Html.DisplayNameFor(Function(model) model.Slug)
			</th>
			<th class="text-right"></th>
		</tr>
	</thead>
	<tbody>
		@For Each item In Model
			@<tr>
				<td>
					@Html.ActionLink(item.Title, "edit", New With {.id = item.Id}, New With {.title = "Изменить"})
				</td>
			 	<td>
			 		@Html.DisplayFor(Function(m) item.Slug)
				</td>
				<td class="text-right">
					<a href="@Url.Action("details", New With {.id = item.Id, .area = ""})" title="Посмотреть" target="_blank"><span class="fa fa-eye"></span></a> |
					<a href="@Url.Action("delete", New With {.id = item.Id})" title="Удалить"><span class="fa fa-trash"></span></a>
				</td>
			</tr>
		Next
	</tbody>
</table>

<nav>
	@Html.Pagination(New With {.class = "pagination"})
</nav>

@Section Scripts
	<script>
		$(function () {
			$(".tablesorter").tablesorter({
				sortList: [[0, 0]],
				headers: { 2: { sorter: false } }
			});
		});
	</script>
End Section