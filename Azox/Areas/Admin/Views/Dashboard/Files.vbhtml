@Code
	ViewBag.Title = "Файлы"
End Code

<header>
	<h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
	@FileManager.Render()
</article>
