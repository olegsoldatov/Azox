@ModelType IEnumerable(Of Azox.Page)
@Code
	ViewBag.Title = "Страницы"
End Code

<h1>@ViewBag.Title</h1>
<hr />

@If Not IsNothing(TempData("Message")) Then
	@<div class="alert alert-dark alert-dismissible fade show" role="alert">
		@TempData("Message")
		<button type="button" class="close" data-dismiss="alert" aria-label="Закрыть">
			<span aria-hidden="true">&times;</span>
		</button>
	</div>
End If

<div class="row btn-toolbar">
	<div class="col-md-9">
		<span class="badge badge-dark">Все (@ViewBag.Count)</span>
	</div>
	<div class="col-md-3">
		@Using Html.BeginForm("index", Nothing, FormMethod.Get)
			@<label for="searchString" class="sr-only">Поиск</label>
			@<div class="input-group">
				<input type="search" name="searchString" id="searchString" class="form-control" placeholder="Поиск" value="@Request.QueryString("searchString")" />
				<span class="input-group-btn">
					<button class="btn btn-default" id="searchButton" title="Искать"><span class="fa fa-search"></span></button>
				</span>
			</div>
		End Using
	</div>
</div>

<div class="table-module">
	<div class="table-wrapper">
		<table class="table table-condensed table-hover tablesorter">
			<thead>
				<tr>
					<th>
						@Html.DisplayNameFor(Function(model) model.Title)
					</th>
					<th>
						@Html.DisplayNameFor(Function(model) model.Heading)
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
							@Html.DisplayFor(Function(model) item.Heading)
						</td>
						<td class="text-right">
							<a href="@Url.Action(item.ActionName, item.ControllerName, New With {.area = ""})" title="Посмотреть" target="_blank"><span class="fa fa-eye"></span></a>
						</td>
					</tr>
				Next
			</tbody>
		</table>

		<nav>
			@Html.Pagination(New With {.class = "pagination"})
		</nav>
	</div>
</div>
