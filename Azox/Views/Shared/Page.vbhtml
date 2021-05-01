@ModelType IPage
@Code
	ViewBag.Title = Model.Title
	ViewBag.Description = Model.Description
	ViewBag.Keywords = Model.Keywords
	ViewBag.EditUrl = Url.Action("edit", "pages", New With {.area = "admin", Model.Id, .returnUrl = Request.Url.PathAndQuery})
End Code

<div class="container">
	<header>
		<h1 class="h1">@ViewBag.Title</h1>
	</header>

	<article>
		@Html.Raw(Model.Content)
	</article>
</div>

