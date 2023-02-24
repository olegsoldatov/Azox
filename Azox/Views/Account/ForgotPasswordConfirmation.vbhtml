@Code
    Layout = "~/Views/Shared/_Account.vbhtml"
    ViewBag.Title = "Восстановление пароля"
    Dim loginUrl = If(ViewBag.LoginUrl, Url.Action("login"))
End Code

<h1 class="h3 mb-3 fw-normal">@ViewBag.Title</h1>
<p>
    Проверьте электронную почту, чтобы сбросить пароль.
</p>
<p class="mt-5 mb-3">
    <a href="@loginUrl"><span class="fa fa-lock">&nbsp;&nbsp;</span>Авторизация</a>
</p>
