@ModelType Brand
@Code
	ViewBag.Title = "Добавление бренда"
End Code

@Section Toolbar
	<button class="btn" form="model-form">
		<span class="fa fa-save"></span>
		<span>Сохранить</span>
	</button>
End Section

<header>
	<h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
	@Using Html.BeginForm(Nothing, Nothing, FormMethod.Post, New With {.id = "model-form", .enctype = "multipart/form-data"})
		@Html.AntiForgeryToken
		@Html.EditorForModel
    End Using
</article>

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section
