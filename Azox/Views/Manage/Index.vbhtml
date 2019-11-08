@Code
	ViewBag.Title = "Управление"
End Code

<h1>@ViewBag.Title</h1>

<p>@Html.ActionLink("Смена пароля", "ChangePassword")</p>

<p class="text-success">@ViewBag.StatusMessage</p>
