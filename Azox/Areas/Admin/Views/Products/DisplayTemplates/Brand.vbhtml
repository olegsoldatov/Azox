@ModelType String
@Code
	If IsNothing(ViewBag.BrandId) OrElse ViewBag.BrandId.Equals(Guid.Empty) Then
		@<span class="text-muted">
			@Model
		</span>
	Else
		@Html.ActionLink(Model, "index", "brands", New With {.area = "admin", .searchText = Model}, New With {.title = Model})
	End If
End Code
