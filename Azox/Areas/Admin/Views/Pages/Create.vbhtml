@ModelType Azox.Page
@Code
    ViewBag.Title = "Добавление страницы"
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
    @Using Html.BeginForm(Nothing, Nothing, Nothing, FormMethod.Post, New With {.id = "model-form"})
        @Html.AntiForgeryToken
        @Html.EditorForModel
    End Using
</article>

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section
