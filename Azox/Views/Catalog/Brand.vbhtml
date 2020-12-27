@ModelType Brand
@Code
	ViewBag.Title = Model.Title
	ViewBag.Description = ""
	ViewBag.EditUrl = Url.Action("edit", "brands", New With {.area = "admin", .id = Model.Id, .returnUrl = Request.Url.PathAndQuery})
	ViewBag.Canonical = Url.Action("brand", Nothing, New With {.name = Model.Name}, Request.Url.Scheme)
End Code

<header class="container">
	<h1 class="h1">@Model.Title</h1>
</header>

<article class="container">
	...
</article>