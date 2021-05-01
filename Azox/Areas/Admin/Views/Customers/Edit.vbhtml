@ModelType Customer
@Code
	ViewBag.Title = "Изменение клиента"
End Code

@Section Toolbar
	<button class="btn" form="modelForm">
		<span class="fa fa-save"></span>
		<span>Сохранить</span>
	</button>
	<a class="btn" href="@Url.Action("delete", New With {Model.Id})">
		<span class="fa fa-remove"></span>
		<span>Удалить</span>
	</a>
End Section

<header>
	<h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
	@Using Html.BeginForm(Nothing, Nothing, New With {.returnUrl = Request.QueryString("ReturnUrl")}, FormMethod.Post, New With {.id = "modelForm", .enctype = "multipart/form-data"})
		@Html.AntiForgeryToken
		@Html.HiddenFor(Function(model) model.Id)
		@Html.EditorForModel
	End Using
</article>

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section
