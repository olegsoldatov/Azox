@Imports Microsoft.AspNet.Identity
@ModelType DashboardBarViewModel

@If Request.IsAuthenticated Then
	@Styles.Render("~/Content/dashboard/bar")
	@<div class="dashboard-bar">
		<div class="dashboard-bar__left-menu">
			<a title="Панель управления" class="dashboard-bar__button" href="~/admin" target="_blank">
				<span class="fas fa-tachometer-alt">&nbsp;</span>
			</a>
			@If Not String.IsNullOrEmpty(Model.EditUrl) Then
				@<a class="dashboard-bar__button" href="@Model.EditUrl">
					<span class="fas fa-edit">&nbsp;</span>
					Изменить
				</a>
			End If
		</div>

		<div class="dashboard-bar__right-menu">
			@Using Html.BeginForm("LogOff", "Account", FormMethod.Post, New With {.id = "logoutForm"})
				@Html.AntiForgeryToken
				@<a title="Управление" class="dashboard-bar__button" href="~/admin/manage"><span class="fas fa-user">&nbsp;</span>&nbsp;@User.Identity.GetUserName()</a>
				@<a title="Выход" class="dashboard-bar__button" href="javascript:document.getElementById('logoutForm').submit()"><span class="fas fa-sign-out-alt"></span></a>
			End Using
		</div>
	</div>
End If

