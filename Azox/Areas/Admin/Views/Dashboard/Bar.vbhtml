@Imports Microsoft.AspNet.Identity
@ModelType DashboardBarViewModel

@If Request.IsAuthenticated Then
	@<div class="dashboard-bar">
		@Styles.Render("~/Content/dashboard/bar")
		<div class="dashboard-bar__left-menu">
			<a title="Панель управления" class="dashboard-bar__button" href="~/admin" target="_blank">
				<span class="fa fa-tachometer">&nbsp;</span>
			</a>
			@If Not String.IsNullOrEmpty(Model.EditUrl) Then
				@<a class="dashboard-bar__button" href="@Model.EditUrl">
					<span class="fa fa-edit">&nbsp;</span>
					Изменить
				</a>
			End If
		</div>

		<div class="dashboard-bar__right-menu">
			@Using Html.BeginForm("logoff", "account", New With {.area = ""}, FormMethod.Post, New With {.id = "logoutForm"})
				@Html.AntiForgeryToken
				@<a title="Управление" class="dashboard-bar__button" href="~/manage"><span class="fa fa-user">&nbsp;</span>&nbsp;@User.Identity.GetUserName()</a>
				@<a title="Выход" class="dashboard-bar__button" href="javascript:document.getElementById('logoutForm').submit()"><span class="fa fa-sign-out"></span></a>
			End Using
		</div>
	</div>
End If

