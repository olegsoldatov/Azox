@ModelType IEnumerable(Of Offer)
@If Model.Any Then
	@<div class="btn-toolbar">
		@Html.ActionLink("Добавить", "createoffer", New With {.id = ViewBag.ProductId}, New With {.class = "btn btn-default btn-sm"})
	</div>
	@<table class="table table-hover table-condensed">
		<thead>
			<tr>
				<th>Название</th>
				<th>Цена</th>
				<th width="72"></th>
			</tr>
		</thead>
		<tbody>
			@For Each item In Model.OrderByDescending(Function(x) x.Price)
				@<tr>
					<td>@Html.ActionLink(item.Name, "editoffer", New With {.id = item.Id})</td>
					<td>@item.Price.ToString("C")</td>
					<td class="text-right">
						<a href="@Url.Action("deleteoffer", New With {.id = item.Id})" title="Удалить"><span class="fa fa-trash"></span></a>
					</td>
				</tr>
			Next
		</tbody>
	</table>
Else
	@<p class="lead text-center">Список пуст. @Html.ActionLink("Добавьте цену", "createoffer", New With {.id = ViewBag.ProductId}, Nothing).</p>
End If
