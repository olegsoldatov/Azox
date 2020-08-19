@ModelType Product
@Code
	ViewBag.Title = "Добавление товара"
End Code

@Section Toolbar
	<button class="btn" form="modelForm">
		<span class="fa fa-save"></span>
		<span>Сохранить</span>
	</button>
End Section

<header>
	<h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
	@Using Html.BeginForm("create", Nothing, FormMethod.Post, New With {.id = "modelForm"})
		@Html.AntiForgeryToken
		@Html.ValidationSummary(False, "", New With {.class = "text-danger"})
		@Html.Hidden("ReturnUrl", Request.QueryString("ReturnUrl"))
		@Html.EditorForModel
		@<div class="form-group">
			<button class="btn btn-primary">Сохранить</button>
		</div>
	End Using
</article>

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section
