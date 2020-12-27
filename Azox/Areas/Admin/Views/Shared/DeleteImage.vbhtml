@ModelType Guid
@Code
	ViewBag.Title = "Удаление изображения"
End Code

@Section Toolbar
	<a class="btn" href="@Request.QueryString("ReturnUrl")">
		<span class="fa fa-arrow-circle-left"></span>
		<span>Назад</span>
	</a>
End Section

<header>
	<h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
	<p class="lead">Вы уверены, что хотите удалить это изображение?</p>
	@Using Html.BeginForm(New With {.returnUrl = Request.QueryString("ReturnUrl")})
		@Html.AntiForgeryToken
		@<div class="form-group">
			<button class="btn btn-danger">Удалить</button>
		</div>
	End Using
</article>
