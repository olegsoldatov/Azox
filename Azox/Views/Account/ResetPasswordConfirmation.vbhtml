@Code
    ViewBag.Title = "Подтверждение сброса пароля"
End Code

<hgroup class="title">
    <h1>@ViewBag.Title.</h1>
</hgroup>
<div>
    <p>
        Ваш пароль сброшен. @Html.ActionLink("Щелкните здесь для входа", "Login", "Account", routeValues:=Nothing, htmlAttributes:=New With {Key .id = "loginLink"})
    </p>
</div>
