@ModelType Brand
@Code
	ViewBag.Title = Model.Title
	ViewBag.Description = Model.Description
	ViewBag.EditUrl = Url.Action("edit", "brands", New With {.area = "admin", .id = Model.Id, .returnUrl = Request.Url.PathAndQuery})
End Code

<header class="container">
	<h1 class="h1">@Model.Title</h1>
</header>

<article class="container">
	...
</article>