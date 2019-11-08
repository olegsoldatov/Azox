@ModelType Product
@Code
	ViewBag.Title = "Изменение продукта"
	Layout = "~/Views/Shared/_Dashboard.vbhtml"
End Code

@Section Toolbar
	<button class="btn" form="model-form">
		<span class="fa fa-save"></span>
		<span>Сохранить</span>
	</button>
	<a class="btn" href="@Url.Action("details", New With {.area = "", .id = Model.Id})">
		<span class="fa fa-eye"></span>
		<span>Посмотреть</span>
	</a>
End Section

<header>
	<h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
	@Html.Partial("_Alert")

	@Using Html.BeginForm("edit", Nothing, FormMethod.Post, New With {.enctype = "multipart/form-data", .id = "model-form"})
		@Html.AntiForgeryToken
		@Html.Hidden("ReturnUrl", Request.QueryString("ReturnUrl"))
		@Html.HiddenFor(Function(model) model.Id)
		@Html.EditorForModel
		@<div class="form-group">
			<button class="btn btn-primary">Сохранить</button>
		</div>
	End Using
</article>

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section
