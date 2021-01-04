@ModelType Product
@Code
	ViewBag.Title = "Удаление товара"
	Dim returnUrl = If(Request.QueryString("ReturnUrl"), Request.UrlReferrer.PathAndQuery)
End Code

<header>
	<h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
	<p class="lead">Вы уверены, что хотите удалить товар &laquo;@Model.Title&raquo;?</p>
	@Using Html.BeginForm(New With {.returnUrl = If(Request.QueryString("ReturnUrl"), Request.UrlReferrer.PathAndQuery)})
		@Html.AntiForgeryToken
		@<div class="form-group">
			<a class="btn btn-outline-secondary" href="@returnUrl">Отменить</a>
			<button class="btn btn-danger">Удалить</button>
		</div>
	End Using
</article>
