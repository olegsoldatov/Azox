@ModelType IEnumerable(Of Picture)

@If Model.Any Then
	@<div class="row">
		@For Each item In Model.OrderBy(Function(model) model.Order).ThenBy(Function(model) model.Name)
			@<div class="col-md-6 col-lg-4">
				<div class="img-thumbnail mt-1 mb-3">
					<a href="@Url.Action("edit", "pictures", New With {.id = item.Id, .returnUrl = Request.Url.PathAndQuery})" title="Изменить">
						@Html.DisplayFor(Function(m) item.ImageId, "Thumbnail", New With {.htmlAttributes = New With {.class = "img-fluid", .alt = item.Name}})
					</a>
					<div class="text-right">
						<a href="@Url.Action("delete", "pictures", New With {.id = item.Id, .returnUrl = Request.Url.PathAndQuery})" title="Удалить" data-toggle="tooltip" data-placement="left"><span class="fa fa-remove"></span></a>
					</div>
				</div>
			</div>
		Next
	</div>
Else
	@<div class="lead">Список пуст.</div>
End If

