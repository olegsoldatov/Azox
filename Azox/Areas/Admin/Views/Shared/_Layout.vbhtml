@Imports Microsoft.AspNet.Identity

<!DOCTYPE html>
<html lang="ru">
<head>
	<title>@ViewBag.Title</title>
	<meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<meta name="description" content="Панель управления" />
	<meta name="format-detection" content="telephone=no" />
	<link href="~/favicon.png" rel="icon" type="image/png" />
	@Styles.Render("~/Content/dashboard")
</head>
<body>
	<div class="sidebar">
		<button title="Свернуть" data-toggle="collapse">
			<span class="fas fa-bars"></span>
		</button>
		<button title="Развернуть" data-toggle="expand">
			<span class="fas fa-bars"></span>
		</button>

		@Html.Partial("_Navigation")
	</div>

	<div class="main">
		<div class="top-bar">
			<div>
				<a href="~/" class="btn" title="Переход на сайт" target="_blank"><span class="fas fa-external-link-alt">&nbsp;&nbsp;</span>Сайт</a>
				@RenderSection("Toolbar", required:=False)
			</div>

			<div>
				<a href="~/admin/manage" class="btn" title="Управление"><span class="fas fa-user">&nbsp;&nbsp;</span>@User.Identity.GetUserName()</a>
				@Using Html.BeginForm("logoff", "account", New With {.area = "admin"}, FormMethod.Post, New With {.id = "logoutForm"})
					@Html.AntiForgeryToken()
					@<button class="btn" title="Выход"><span class="fas fa-sign-out-alt"></span></button>
				End Using
			</div>
		</div>

		<div class="content">
			@RenderBody()
		</div>

		<a href="#" class="back-to-top" aria-disabled="true" data-toggle="toTop" title="Наверх">
			<div class="fas fa-arrow-up" aria-label="Наверх"></div>
		</a>
	</div>

	@Scripts.Render("~/bundles/jquery")
	@Scripts.Render("~/bundles/bootstrap")
	@Scripts.Render("~/bundles/dashboard")
	@RenderSection("Scripts", required:=False)
</body>
</html>
<!-- Софт Бизнес (http://soft.business/) -->
