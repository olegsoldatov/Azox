@ModelType Picture
@Code
	ViewBag.Title = "Удаление изображения"
	Layout = "~/Views/Shared/_Dashboard.vbhtml"
End Code

<header>
	<h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
	<p class="lead">Вы уверены, что хотите удалить изображение &laquo;@Model.Name&raquo;?</p>
	@Using Html.BeginForm
		@Html.AntiForgeryToken
		@Html.Hidden("ReturnUrl", Request.QueryString("ReturnUrl"))
		@<div class="form-actions no-color">
			<button class="btn btn-danger">Удалить</button>
		</div>
	End Using
</article>
