﻿@ModelType Category
@Code
	ViewBag.Title = "Удаление категории"
End Code

<header>
	<h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
	<p class="lead">Вы уверены, что хотите удалить категорию &laquo;@Model.Title&raquo;?</p>
	@Using Html.BeginForm
		@Html.AntiForgeryToken
		@<div class="form-actions no-color">
			<button class="btn btn-danger">Удалить</button>
		</div>
	End Using
</article>
