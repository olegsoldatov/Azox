@ModelType Article
@Code
	ViewBag.Title = If(IsNothing(SiteMap.CurrentNode), Model.Title, If(SiteMap.CurrentNode.Item("Heading"), SiteMap.CurrentNode.Title))
	If Not IsNothing(SiteMap.CurrentNode) Then
		ViewBag.Description = SiteMap.CurrentNode.Description
		ViewBag.Keywords = SiteMap.CurrentNode.Item("Keywords")
	End If
End Code

<header class="page-header">
	<h1 class="heading">@ViewBag.Title</h1>

	<nav>
		<div class="container">
			B
		</div>
	</nav>
</header>

<div class="container">
	@Html.Raw(Model.Content)
</div>

