@ModelType Azox.Page
@Code
    ViewBag.Title = "Удаление страницы"
    Dim returnUrl = Request.QueryString("ReturnUrl")
End Code

<header>
	<h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
	<p class="lead">Вы уверены, что хотите удалить страницу &laquo;@Model.Title&raquo;?</p>
	@Using Html.BeginForm(New With {returnUrl})
		@Html.AntiForgeryToken
		@<div class="form-actions no-color">
			<button class="btn btn-danger">Удалить</button>
		</div>
    End Using
</article>
