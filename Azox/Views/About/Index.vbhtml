@ModelType AboutSetting
@Code
    ViewBag.Description = Model.Description
End Code

<div class="container">
	<header>
		<h1 class="h1">@ViewBag.Title</h1>
	</header>

	<article>
		@Html.Raw(Model.Content)
	</article>
</div>

