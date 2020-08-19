@ModelType Article
@Code
	ViewBag.Title = If(Model.Title, Model.Name)
	ViewBag.Description = Model.Description
	ViewBag.Keywords = Model.Keywords
	ViewBag.EditUrl = Url.Action("edit", "articles", New With {.area = "admin", .id = Model.Id, .returnUrl = Request.Url.PathAndQuery})
End Code

<article>
	<div class="container">
		<h1 class="h1">@Model.Name</h1>
		@Html.Raw(Model.Content)
	</div>
</article>

