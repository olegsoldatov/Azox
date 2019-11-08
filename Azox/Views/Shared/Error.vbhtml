@ModelType HandleErrorInfo
@Code
	ViewBag.Title = "При обработке запроса произошла ошибка"
End Code

<div class="container pt-5">
	<h1 class="text-danger">Ошибка сервера</h1>
	<h2 class="text-danger">@ViewBag.Title.</h2>
	<p class="lead">@Model.Exception.Message</p>
</div>