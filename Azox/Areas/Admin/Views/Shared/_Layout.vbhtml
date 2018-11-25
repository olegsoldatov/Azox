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
	@RenderSection("Head", required:=False)
</head>
<body>
	<header class="sidebar" id="sidebar">
		<button class="sidebar-toggle" data-toggle="collapse" aria-expanded="false" data-target="#sidebar">
			<span class="fas fa-bars"></span>
		</button>
		@Html.Partial("_Navigation")
	</header>

	<main class="main" id="main">
		<footer class="toolbar" id="toolbar" tabindex="-1">
			<div>
				@RenderSection("Toolbar", required:=False)
			</div>

			<div>
				<a href="~/" class="btn" title="Переход на сайт" target="_blank">
					<span class="fa fa-globe"></span>
					<span>Сайт</span>
				</a>
				<a href="~/admin/manage" class="btn" title="Управление учетной записью">
					<span class="fas fa-user"></span>
					<span>@User.Identity.Name</span>
				</a>
				@Using Html.BeginForm("logoff", "account", New With {.area = "admin"}, FormMethod.Post, New With {.id = "logoutForm"})
					@Html.AntiForgeryToken()
					@<button class="btn" title="Выход"><span class="fas fa-sign-out-alt"></span></button>
				End Using
			</div>
		</footer>

		<section class="content" id="content" tabindex="-1">
			@RenderBody()
		</section>
	</main>

	<a href="#" class="back-to-top" aria-disabled="true" data-toggle="toTop" title="Наверх">
		<div class="fas fa-arrow-up" aria-label="Наверх"></div>
	</a>

	@Scripts.Render("~/bundles/jquery")
	@Scripts.Render("~/bundles/bootstrap")
	@Scripts.Render("~/bundles/dashboard")
	@RenderSection("Scripts", required:=False)
</body>
</html>
<!-- Дизайн и разработка Софт Бизнес http://soft.business -->
