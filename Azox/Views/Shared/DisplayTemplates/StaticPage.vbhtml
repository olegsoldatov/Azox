@ModelType StaticPage

<section>
	<article>
		<h1 itemprop="name">@Html.Raw(If(Model.Heading, Model.Title))</h1>
		@Html.Raw(Model.Content)
	</article>
</section>
