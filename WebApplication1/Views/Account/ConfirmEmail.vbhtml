@Code
    ViewBag.Title = "Подтверждение почты"
End Code

<h2>@ViewBag.Title.</h2>
<div>
    <p>
        Благодарим за подтверждение вашей почты. @Html.ActionLink("Щелкните здесь для входа", "Login", "Account", routeValues:=Nothing, htmlAttributes:=New With {Key .id = "loginLink"})
    </p>
</div>
