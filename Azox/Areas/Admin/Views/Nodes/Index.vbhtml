@ModelType IEnumerable(Of Node)
@Code
	ViewBag.Title = "Узлы"
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
	<div class="col-md-12">
		@Using Html.BeginForm("index", "nodes", FormMethod.Get)
			@<label for="searchString" class="sr-only">Поиск узла</label>
			@<div class="input-group">
				<input type="search" name="searchString" id="searchString" class="form-control" placeholder="Поиск узла" value="@Request.QueryString("searchString")" />
				<span class="input-group-btn">
					<button class="btn btn-default" id="searchButton"><span class="fa fa-search"></span></button>
				</span>
			</div>
		End Using
	</div>
</div>

<table class="table table-hover tablesorter">
	<thead>
		<tr>
			<th>
				@Html.DisplayNameFor(Function(model) model.Title)
			</th>
			<th>
				@Html.DisplayNameFor(Function(model) model.Url)
			</th>
			<th>
				@Html.DisplayNameFor(Function(model) model.Heading)
			</th>
			<th>
				@Html.DisplayNameFor(Function(model) model.Description)
			</th>
			<th>
				@Html.DisplayNameFor(Function(model) model.Keywords)
			</th>
			<th class="text-right"></th>
		</tr>
	</thead>
	<tbody>
		@For Each item In Model
			@<tr>
				<td>
					@Html.ActionLink(item.Title, "edit", New With {.id = item.Id}, New With {.title = "Изменить"})
					@If item.ParentId = Guid.Empty Then
							@<span class="text-muted" title="Домашняя страница">&mdash; <i class="fa fa-home"></i></span>
					End If
				</td>
				<td class="text-nowrap" title="@item.Url">
					@Html.DisplayFor(Function(m) item.Url)
				</td>
			 	<td>
			 		@Html.DisplayFor(Function(m) item.Heading)
				</td>
				<td>
					@Html.DisplayFor(Function(m) item.Description)
				</td>
				<td>
					@Html.DisplayFor(Function(m) item.Keywords)
				</td>
				<td class="text-right text-nowrap">
					@If Not String.IsNullOrEmpty(item.Url) Then
						@<a href = "@Url.Content(item.Url)" title="Посмотреть" target="_blank"><span class="fa fa-eye"></span></a> @<text>|</text>
					End If
					@If item.ParentId = Guid.Empty Then
						@<span class="fa fa-trash text-muted" title="Заблокировано"></span>
					Else
						@<a href="@Url.Action("delete", New With {.id = item.Id})" title="Удалить"><span class="fa fa-trash"></span></a>
					End If
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
				headers: { 5: { sorter: false } }
			});
		});
	</script>
End Section