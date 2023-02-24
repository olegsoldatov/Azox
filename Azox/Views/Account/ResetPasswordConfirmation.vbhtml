@Code
    Layout = "~/Views/Shared/_Account.vbhtml"
    ViewBag.Title = "Сброс пароля"
End Code

<h1 class="h3 mb-3 fw-normal">@ViewBag.Title</h1>
<p>
    Ваш пароль сброшен.
</p>
<p class="mt-5 mb-3">
    <a href="@Url.Action("login", "account")"><span class="fa fa-lock">&nbsp;&nbsp;</span>Авторизация</a>
</p>
