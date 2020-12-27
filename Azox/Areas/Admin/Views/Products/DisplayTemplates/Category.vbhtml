@ModelType String
@Code
	If IsNothing(ViewBag.CategoryId) OrElse ViewBag.CategoryId.Equals(Guid.Empty) Then
		@<span class="text-muted">
			@Model
		</span>
	Else
		@Html.ActionLink(Model, "index", "categories", New With {.area = "admin", .searchText = Model}, New With {.title = Model})
	End If
End Code
