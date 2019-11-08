@ModelType Azox.Page
@Code
	ViewBag.Title = "Изменение страницы"
End Code

@Section Toolbar
	<button class="btn" form="model-form">
		<span class="fa fa-save"></span>
		<span>Сохранить</span>
	</button>
	<a href="@Url.Action(Model.ActionName, Model.ControllerName, New With {.area = ""})" class="btn" target="_blank">
		<span class="fa fa-eye"></span>
		<span>Посмотреть</span>
	</a>
End Section

<header>
	<h1>@ViewBag.Title</h1>
	@Html.Partial("_Alert")
</header>

<article>
	@Using Html.BeginForm("edit", Nothing, FormMethod.Post, New With {.id = "model-form"})
		@Html.AntiForgeryToken
		@Html.Hidden("ReturnUrl", Request.QueryString("ReturnUrl"))
		@Html.HiddenFor(Function(model) model.Id)
		@Html.HiddenFor(Function(model) model.ActionName)
		@Html.HiddenFor(Function(model) model.ControllerName)
		@Html.EditorForModel
		@<div class="form-group">
			<button class="btn btn-primary">Сохранить</button>
		</div>
	End Using
</article>

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section
