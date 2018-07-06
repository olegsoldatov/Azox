@ModelType LoginViewModel
@Code
	Layout = "~/Areas/Admin/Views/Account/_Layout.vbhtml"
	ViewBag.Title = "Авторизация"
End Code

@Using Html.BeginForm("login", "account", New With {.ReturnUrl = ViewBag.ReturnUrl}, FormMethod.Post, New With {.class = "form-signin", .role = "form"})
	@Html.AntiForgeryToken()
	@<text>
		<h2 class="form-signin-heading">@ViewBag.Title</h2>
		@Html.ValidationSummary(False)
		@Html.LabelFor(Function(m) m.Email, New With {.class = "sr-only"})
		@Html.EditorFor(Function(m) m.Email, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = "Электронный адрес", .autofocus = ""}})
		@Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control", .placeholder = "Пароль"})
		<div class="checkbox">
			<label>
				@Html.CheckBoxFor(Function(m) m.RememberMe)
				@Html.DisplayNameFor(Function(m) m.RememberMe)
			</label>
		</div>
		<div class="form-group">
			<button class="btn btn-lg btn-primary btn-block" type="submit">Войти</button>
		</div>
		@*<div class="form-group">
				<a href="~/admin/account/forgotpassword">Забыли пароль?</a>
			</div>*@
	</text>
End Using

@*<p class="help-block text-center">
		@Html.ActionLink("Зарегистрируйтесь", "register"), если у вас нет учетной записи.
	</p>*@

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section
