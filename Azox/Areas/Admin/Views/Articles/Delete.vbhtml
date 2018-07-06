@ModelType Article
@Code
	ViewBag.Title = "Удаление статьи"
End Code

<h1>@ViewBag.Title</h1>
<hr />

<p class="lead">Вы действительно хотите удалить статью &laquo;@Model.Title&raquo;?</p>

@Using Html.BeginForm()
	@Html.AntiForgeryToken()
	@Html.ValidationSummary(False, "", New With {.class = "text-danger"})
	@<div class="form-actions no-color">
		<button class="btn btn-default">Удалить</button>
	 	<a href="@Request.UrlReferrer.AbsolutePath" class="btn btn-link">Отмена</a>
	</div>
End Using
