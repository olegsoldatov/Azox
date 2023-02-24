@ModelType ForgotPasswordViewModel
@Code
    Layout = "~/Views/Shared/_Account.vbhtml"
    ViewBag.Title = "Забыли пароль?"
    Dim loginUrl = If(Request.QueryString("LoginUrl"), Url.Action("login"))
End Code

@Using Html.BeginForm("forgotPassword", "account", New With {loginUrl}, FormMethod.Post, Nothing)
    @Html.AntiForgeryToken
    @<text>
        <h1 class="h3 mb-3 fw-normal">@ViewBag.Title</h1>
        <p class="form-text">Укажите адрес электронной почты, на&nbsp;который будет отправлена ссылка для восстановления.</p>
        @Html.ValidationSummary("", New With {.class = "text-danger"})
        <div class="form-floating mb-3">
            @Html.EditorFor(Function(m) m.Email, New With {.htmlAttributes = New With {.class = "form-control rounded", .placeholder = "name@example.com"}})
            @Html.LabelFor(Function(m) m.Email, New With {.class = "form-label"})
        </div>
        <button class="w-100 btn btn-lg btn-primary" type="submit">Отправить ссылку</button>
        <p class="mt-5 mb-3">
            <a href="@loginUrl"><span class="fa fa-lock">&nbsp;&nbsp;</span>Авторизация</a>
        </p>
    </text>
End Using

@section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
