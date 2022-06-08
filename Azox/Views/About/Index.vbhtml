@ModelType AboutPage
@Code
    ViewBag.Title = If(Model.Title, Model.Heading)
    ViewBag.Description = Model.Description
End Code

<div class="container">
	<header>
		<h1 class="h1">@Model.Heading</h1>
	</header>

	<article>
		@Html.Raw(Model.Content)
	</article>
</div>

