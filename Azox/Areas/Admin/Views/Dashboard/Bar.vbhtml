@Imports Microsoft.AspNet.Identity
@ModelType DashboardBarViewModel

@If Request.IsAuthenticated Then
	@<div class="dashboard-bar" role="toolbar">
		@Styles.Render("~/Content/dashboard/bar")
		<div class="dashboard-bar__left-menu">
			<a title="Панель управления" class="dashboard-bar__button" href="~/admin" target="_blank" aria-label="Панель управления">
				<span class="fa fa-tachometer" aria-hidden="true">&nbsp;</span>
			</a>
			@If Not String.IsNullOrEmpty(Model.EditUrl) Then
				@<a class="dashboard-bar__button" href="@Model.EditUrl">
					<span class="fa fa-edit">&nbsp;</span>
					<span class="dashboard-bar__button__text">Изменить</span>
				</a>
			End If
		</div>

		<div class="dashboard-bar__right-menu">
			@Using Html.BeginForm("logoff", "account", New With {.area = ""}, FormMethod.Post, New With {.id = "logoutForm"})
				@Html.AntiForgeryToken
				@<a title="Управление" class="dashboard-bar__button" href="~/manage">
					<span class="fa fa-user" aria-hidden="true">&nbsp;</span>
					<span class="dashboard-bar__button__text">@User.Identity.GetUserName</span>
				</a>
				@<a title="Выход" class="dashboard-bar__button" href="javascript:document.getElementById('logoutForm').submit()"><span class="fa fa-sign-out" aria-hidden="true"></span></a>
			End Using
		</div>
	</div>
End If

