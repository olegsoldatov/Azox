@ModelType ResetPasswordViewModel
@Code
    Layout = "~/Views/Shared/_Account.vbhtml"
    ViewBag.Title = "Сброс пароля"
End Code


@Using Html.BeginForm("resetPassword", "account", FormMethod.Post, New With {.role = "form"})
    @Html.AntiForgeryToken()
    @Html.HiddenFor(Function(m) m.Code)
    @<text>
        <h1 class="h3 mb-3 fw-normal">@ViewBag.Title</h1>
        @Html.ValidationSummary("", New With {.class = "text-danger"})
        <div class="form-floating mb-3">
            @Html.EditorFor(Function(m) m.Email, New With {.htmlAttributes = New With {.class = "form-control rounded", .placeholder = "name@example.com"}})
            @Html.LabelFor(Function(m) m.Email, New With {.class = "form-label"})
        </div>
        <div class="form-floating mb-3">
            @Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control rounded", .placeholder = "Новый пароль"})
            @Html.LabelFor(Function(m) m.Password, New With {.class = "form-label"})
        </div>
        <div class="form-floating mb-3">
            @Html.PasswordFor(Function(m) m.ConfirmPassword, New With {.class = "form-control rounded", .placeholder = "Подтверждение пароля"})
            @Html.LabelFor(Function(m) m.ConfirmPassword, New With {.class = "form-label"})
        </div>
        <button class="w-100 btn btn-lg btn-primary" type="submit">Сбросить</button>
    </text>
End Using

@section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
