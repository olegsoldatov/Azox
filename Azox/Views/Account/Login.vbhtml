@ModelType LoginViewModel
@Code
    Layout = "~/Views/Shared/_Account.vbhtml"
    ViewBag.Title = "Авторизация"
End Code

@Using Html.BeginForm("login", "account", New With {.ReturnUrl = ViewBag.ReturnUrl}, FormMethod.Post, Nothing)
    @Html.AntiForgeryToken
    @<text>
        <h1 class="h3 mb-3 fw-normal">@ViewBag.Title</h1>
        @Html.ValidationSummary("", New With {.class = "text-danger"})
        <div class="form-floating">
            @Html.EditorFor(Function(m) m.Email, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = "name@example.com"}})
            @Html.LabelFor(Function(m) m.Email)
        </div>
        <div class="form-floating">
            @Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control", .placeholder = "Пароль"})
            @Html.LabelFor(Function(m) m.Password)
        </div>
        <div class="checkbox mb-3">
            <label>
                @Html.CheckBoxFor(Function(m) m.RememberMe)
                @Html.DisplayNameFor(Function(m) m.RememberMe)
            </label>
        </div>
        <button class="w-100 btn btn-lg btn-primary" type="submit">Войти</button>
        <p class="mt-5 mb-3">
            @Html.ActionLink("Забыли пароль?", "forgotPassword", New With {.loginUrl = Request.Url.PathAndQuery})
        </p>
    </text>
End Using

@*<p class="help-block text-center">
        @Html.ActionLink("Зарегистрируйтесь", "register"), если у вас нет учетной записи.
    </p>*@

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
