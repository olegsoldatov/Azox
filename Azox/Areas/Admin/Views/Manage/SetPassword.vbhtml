@ModelType SetPasswordViewModel
@Code
    ViewBag.Title = "Создать пароль"
End Code

<h2>@ViewBag.Title.</h2>
<p class="text-info">
    У вас нет локального имени пользователя и пароля для этого сайта. Добавьте локальную
    учетную запись, чтобы входить без использования внешнего входа.
</p>

@Using Html.BeginForm("SetPassword", "Manage", FormMethod.Post, New With { .class = "form-horizontal", .role = "form" })
    @Html.AntiForgeryToken()
    @<text>
    <h4>Создать локальное имя входа</h4>
    <hr />
    @Html.ValidationSummary("", New With { .class = "text-danger" })
    <div class="form-group">
        @Html.LabelFor(Function(m) m.NewPassword, New With {.class = "col-md-2 control-label"})
        <div class="col-md-10">
            @Html.PasswordFor(Function(m) m.NewPassword, New With {.class = "form-control"})
        </div>
    </div>
    <div class="form-group">
        @Html.LabelFor(Function(m) m.ConfirmPassword, New With { .class = "col-md-2 control-label" })
        <div class="col-md-10">
            @Html.PasswordFor(Function(m) m.ConfirmPassword, New With { .class = "form-control" })
        </div>
    </div>
    <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
            <input type="submit" value="Задать пароль" class="btn btn-default" />
        </div>
    </div>
    </text>
End Using
@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section