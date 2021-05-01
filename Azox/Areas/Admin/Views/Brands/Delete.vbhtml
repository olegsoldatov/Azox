@ModelType Brand
@Code
	ViewBag.Title = "Удаление бренда"
End Code

<header>
	<h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
	<p class="lead">Вы уверены, что хотите удалить бренд &laquo;@Model.Title&raquo;?</p>
	@Using Html.BeginForm
		@Html.AntiForgeryToken
		@<div class="form-actions no-color">
			<button class="btn btn-danger">Удалить</button>
		</div>
	End Using
</article>
