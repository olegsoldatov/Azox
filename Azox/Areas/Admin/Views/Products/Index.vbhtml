@ModelType IEnumerable(Of Product)
@Code
	ViewBag.Title = "Продукция"
End Code

@Section Toolbar
	<a class="btn" href="@Url.Action("Create")"><span class="fa fa-plus">&nbsp;&nbsp;</span>Добавить</a>
End Section

<h1>@ViewBag.Title</h1>
<hr />

@Html.Partial("_Alert")

<div class="table-module">
	<div class="table-wrapper">
		@If Model.Any Then
			@<table class="table table-hover tablesorter">
				<thead>
					<tr>
						<th>
							@Html.DisplayNameFor(Function(model) model.Title)
						</th>
						<th class="text-right"></th>
					</tr>
				</thead>
				<tbody>
					@For Each item In Model
						@<tr>
							<td>
								@Html.ActionLink(item.Title, "Edit", New With {.id = item.Id, .returnUrl = Request.Url.PathAndQuery}, New With {.title = "Изменить"})
							</td>
							<td class="text-right">
								@Html.DisplayFor(Function(modelItem) item.Draft) |
								<a href="@Url.Action("Details", "Products", New With {.area = "", .id = item.Id})" title="Посмотреть" target="_blank"><span class="fa fa-eye"></span></a> |
								<a href="@Url.Action("Delete", New With {.id = item.Id})" title="Удалить"><span class="fa fa-trash"></span></a>
							</td>
						</tr>
					Next
				</tbody>
			</table>
		Else
			@<p class="lead text-center">Список пуст.</p>
		End If
	</div>
</div>

@Html.Pagination(New With {.class = "pagination"})

@Section Scripts
	<script>
		$(function () {
			$(".tablesorter").tablesorter({
				headers: { 1: { sorter: false } }
			});
		});
	</script>
End Section