@ModelType IEnumerable(Of CategoryListItem)
@If Model.Any Then
	@<div class="row">
		@For Each item In Model.Where(Function(x) x.ParentId Is Nothing).OrderBy(Function(x) x.Order).ThenBy(Function(x) x.Title)
			@<div class="col-lg-4 mb-4">
				<div class="card">
					@Html.DisplayFor(Function(model) item.ImageId, "Medium", New With {.htmlAttributes = New With {.class = "card-img-top", .alt = item.Title}})
					<div class="card-body">
						<h5 class="card-title">@item.Title</h5>
						<ul class="list-unstyled">
							@For Each child In Model.Where(Function(x) x.ParentId IsNot Nothing AndAlso x.ParentId = item.Id).OrderBy(Function(x) x.Order).ThenBy(Function(x) x.Title).Take(4)
								@<li>
									@Html.ActionLink(child.Title, "details", "categories", New With {.id = child.Id}, Nothing)
								</li>
							Next
						</ul>
						@Html.ActionLink("Больше", "details", "categories", New With {.id = item.Id}, New With {.class = "btn btn-primary"})
					</div>
				</div>
			</div>
		Next
	</div>
End If
