<!DOCTYPE html>
<html lang="ru" itemtype="http://schema.org/WebPage" itemscope>
<head prefix="og: http://ogp.me/ns#">
	<title>@ViewBag.Title</title>
	<meta charset="utf-8">
	<meta http-equiv="X-UA-Compatible" content="IE=edge">
	<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
	<meta name="title" content="@ViewBag.Title" property="og:title" itemprop="name" />
	<meta name="description" content="@ViewBag.Description" property="og:description" itemprop="description" />
	<meta name="author" content="Soldata" itemprop="author" />
	<meta content="@Request.Url.AbsoluteUri" property="og:url" itemprop="url" />
	<meta content="@ViewBag.ImageUrl" property="og:image" itemprop="image" />
	<meta content="website" property="og:type" />
	<meta content="ru_RU" property="og:locale" />
	@RenderSection("Meta", required:=False)
	<link href="~/favicon.png" rel="icon" type="image/png" />
	@RenderSection("Links", required:=False)
	@Styles.Render("~/Content/css")
	@RenderSection("Styles", required:=False)
</head>
<body>
	<header>
		<nav class="navbar navbar-expand-lg navbar-light bg-light">
			<div class="container">
				@Html.ActionLink("Azox", "Index", "Home", New With {.area = ""}, New With {.class = "navbar-brand"})
				<button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
					<span class="navbar-toggler-icon"></span>
				</button>
				<div class="collapse navbar-collapse" id="navbarSupportedContent">
					@Html.Partial("_Navigation")
				</div>
			</div>
		</nav>

		@RenderSection("Hero", required:=False)
	</header>

	<main>
		@RenderBody()
	</main>

	<footer class="page-footer">
		<div class="container">
			<p>&copy; Soldata, @DateTime.Now.Year</p>
		</div>
	</footer>

	<a href="#" class="back-to-top" aria-disabled="true" data-toggle="toTop" title="Наверх">
		<div class="fa fa-arrow-up" aria-label="Наверх"></div>
	</a>

	@Html.Action("Bar", "Dashboard", New With {.area = "Admin", .editUrl = ViewBag.EditUrl})

	@Scripts.Render("~/bundles/jquery")
	@Scripts.Render("~/bundles/bootstrap")
	@Scripts.Render("~/bundles/scripts")
	@RenderSection("Scripts", required:=False)
</body>
</html>
<!-- Дизайн и разработка Софт Бизнес http://soft.business -->
