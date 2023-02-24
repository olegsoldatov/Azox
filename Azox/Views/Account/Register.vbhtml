@ModelType RegisterViewModel
@Code
    Layout = "~/Views/Shared/_Account.vbhtml"
    ViewBag.Title = "Регистрация"
End Code

@Using Html.BeginForm("register", "account", FormMethod.Post, New With {.role = "form"})
    @Html.AntiForgeryToken()
    @<text>
        <h1 class="h3 mb-3 fw-normal">@ViewBag.Title</h1>
        <p class="form-text">Создайте новую учетную запись.</p>
        @Html.ValidationSummary("", New With {.class = "text-danger"})
        <div class="form-floating mb-3">
            @Html.TextBoxFor(Function(m) m.Email, New With {.class = "form-control rounded", .placeholder = "Электронный адрес"})
            @Html.LabelFor(Function(m) m.Email, New With {.class = "form-label"})
        </div>
        <div class="form-floating mb-3">
            @Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control rounded", .placeholder = "Пароль"})
            @Html.LabelFor(Function(m) m.Password, New With {.class = "form-label"})
        </div>
        <div class="form-floating mb-3">
            @Html.PasswordFor(Function(m) m.ConfirmPassword, New With {.class = "form-control rounded", .placeholder = "Подтверждение пароля"})
            @Html.LabelFor(Function(m) m.ConfirmPassword, New With {.class = "form-label"})
        </div>
        <button class="w-100 btn btn-lg btn-primary" type="submit">Регистрация</button>
    </text>
End Using

@section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
