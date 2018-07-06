@ModelType ExternalLoginConfirmationViewModel
@Code
    ViewBag.Title = "Регистрация"
End Code

<h2>@ViewBag.Title.</h2>
<h3>Связывание вашей учетной записи @ViewBag.LoginProvider.</h3>

@Using Html.BeginForm("ExternalLoginConfirmation", "Account", New With { .ReturnUrl = ViewBag.ReturnUrl }, FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
    @Html.AntiForgeryToken()

    @<text>
    <h4>Форма связи</h4>
    <hr />
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    <p class="text-info">
        Вы успешно прошли проверку подлинности через <strong>@ViewBag.LoginProvider</strong>
            Введите имя пользователя на этом сайте ниже и нажмите кнопку "Зарегистрировать", чтобы завершить
            вход.
    </p>
    <div class="form-group">
        @Html.LabelFor(Function(m) m.Email, New With {.class = "col-md-2 control-label"})
        <div class="col-md-10">
            @Html.TextBoxFor(Function(m) m.Email, New With {.class = "form-control"})
            @Html.ValidationMessageFor(Function(m) m.Email, "", New With {.class = "text-danger"})
        </div>
    </div>
    <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
            <input type="submit" class="btn btn-default" value="Регистрация" />
        </div>
    </div>
    </text>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
