@ModelType Category
@Code
	ViewBag.Title = "Изменение категории"
End Code

@Section Toolbar
	<button class="btn" form="model-form">
		<span class="fa fa-save"></span>
		<span>Сохранить</span>
	</button>
	<a class="btn" href="@Url.Action("details", New With {.area = "", .id = Model.Id})" target="_blank">
		<span class="fa fa-external-link"></span>
		<span>Посмотреть</span>
	</a>
	<a class="btn" href="@Url.Action("delete", New With {.id = Model.Id, .returnUrl = Url.Action("index")})">
		<span class="fa fa-remove"></span>
		<span>Удалить</span>
	</a>
End Section

<header>
	<h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
	@Using Html.BeginForm("edit", Nothing, New With {.returnUrl = If(Request.QueryString("ReturnUrl"), Request.UrlReferrer.PathAndQuery)}, FormMethod.Post, New With {.enctype = "multipart/form-data", .id = "model-form"})
		@Html.AntiForgeryToken
		@Html.HiddenFor(Function(model) model.Id)
		@Html.HiddenFor(Function(model) model.Path)
		@Html.EditorForModel
	End Using
</article>

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section

