@ModelType Warehouse
@Code
	ViewBag.Title = "Изменение склада"
End Code

@Section Toolbar
	<button class="btn" form="model-form">
		<span class="fa fa-save"></span>
		<span>Сохранить</span>
	</button>
End Section

<header>
	<h1 class="heading">@ViewBag.Title</h1>
	@Html.Partial("_Alert")
</header>

<article>
	@Using Html.BeginForm("edit", Nothing, FormMethod.Post, New With {.enctype = "multipart/form-data", .id = "model-form"})
		@Html.AntiForgeryToken
		@Html.Hidden("ReturnUrl", Request.QueryString("ReturnUrl"))
		@Html.HiddenFor(Function(model) model.Id)
		@Html.EditorForModel
	End Using
</article>

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section
