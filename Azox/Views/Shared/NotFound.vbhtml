@Code
	ViewBag.Title = "404 - Страница не найдена"
End Code

<div class="container pt-5">
	<h1 class="text-danger">Ошибка сервера</h1>
	<h2 class="text-danger">@ViewBag.Title.</h2>
	<p class="lead">@ViewBag.Message</p>
</div>
