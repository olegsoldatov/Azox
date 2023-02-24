@ModelType Azox.Page
@Code
    ViewBag.Title = If(Model.Seo.Title, Model.Heading)
    ViewBag.Description = Model.Seo.Description
    ViewBag.EditUrl = Url.Action("edit", "pages", New With {.area = "admin", Model.Id})
End Code

<div class="container">
    <header>
        <h1 class="h1">@Model.Heading</h1>
    </header>

	<article>
		@Html.Raw(Model.Content)
	</article>
</div>

