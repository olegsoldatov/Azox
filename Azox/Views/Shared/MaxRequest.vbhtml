@ModelType HandleErrorInfo
@Code
	ViewBag.Title = "500 - Превышен максимальный размер файла"
End Code

<div class="container pt-5">
	<h1 class="text-danger">Ошибка сервера</h1>
	<h2 class="text-danger">@ViewBag.Title.</h2>
	<p class="lead">@Model.Exception.Message</p>
</div>