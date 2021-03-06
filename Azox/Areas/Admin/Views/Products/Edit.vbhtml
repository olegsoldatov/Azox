﻿@ModelType Product
@Code
	ViewBag.Title = "Изменение товара"
End Code

@Section Toolbar
	<button class="btn" form="model-form">
		<span class="fa fa-save"></span>
		<span>Сохранить</span>
	</button>
	<a class="btn" href="@Url.Action("product", "catalog", New With {.area = "", .id = Model.Id})" target="_blank">
		<span class="fa fa-external-link"></span>
		<span>Посмотреть</span>
	</a>
End Section

<header>
	<h1 class="heading">@ViewBag.Title</h1>
	@Html.Partial("_Alert")
</header>

<article>
	@Using Html.BeginForm(Nothing, Nothing, New With {.returnUrl = If(Request.QueryString("ReturnUrl"), Request.UrlReferrer.PathAndQuery)}, FormMethod.Post, New With {.id = "model-form"})
		@Html.AntiForgeryToken
		@Html.HiddenFor(Function(model) model.Id)
		@Html.HiddenFor(Function(model) model.BrandName)
		@Html.EditorForModel
	End Using
</article>

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section

