@Imports System.Web.Configuration
@ModelType Guid
@Code
	ViewBag.Title = "Добавление изображения"
End Code

@Section Toolbar
	<button class="btn" form="model-form">
		<span class="fa fa-save"></span>
		<span>Сохранить</span>
	</button>
	<a class="btn" href="@Request.QueryString("ReturnUrl")">
		<span class="fa fa-arrow-circle-left"></span>
		<span>Назад</span>
	</a>
End Section

<header>
	<h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
	@Using Html.BeginForm(Nothing, Nothing, New With {.returnUrl = Request.QueryString("ReturnUrl")}, FormMethod.Post, New With {.enctype = "multipart/form-data", .id = "model-form"})
		@Html.AntiForgeryToken
		@Html.Hidden("Id", Model)
		@<div class="form-group">
			@Html.Label("Изображение", New With {.class = "control-label required"})
			@Html.ValidationMessage("ImageFile", "", New With {.class = "text-danger"})
			@Html.TextBox("ImageFile", Nothing, New With {.type = "file", .accept = "image/*"})
			<p><small>@String.Format("Размер файла не более {0} МБ.", CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength / 1024)</small></p>
		</div>
	End Using
</article>

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section
