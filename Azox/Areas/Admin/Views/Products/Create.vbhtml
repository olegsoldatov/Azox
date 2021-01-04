@ModelType Product
@Code
	ViewBag.Title = "Добавление товара"
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
	@Using Html.BeginForm(Nothing, Nothing, New With {.returnUrl = If(Request.QueryString("ReturnUrl"), Request.UrlReferrer.PathAndQuery)}, FormMethod.Post, New With {.id = "model-form"})
		@Html.AntiForgeryToken
		@Html.EditorForModel
	End Using
</article>

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section

