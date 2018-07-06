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
	<nav class="navbar navbar-expand-md navbar-dark bg-dark">
		<a class="navbar-brand" href="#">Azox</a>
		<button class="navbar-toggler" aria-expanded="false" aria-controls="navbarsExampleDefault" aria-label="Toggle navigation" type="button" data-target="#navbarsExampleDefault" data-toggle="collapse">
			<span class="navbar-toggler-icon"></span>
		</button>
		<div class="collapse navbar-collapse" id="navbarsExampleDefault">
			@Html.Partial("_Navigation")

			<form class="form-inline my-2 my-lg-0">
				<input class="form-control mr-sm-2" aria-label="Search" type="text" placeholder="Search">
				<button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
			</form>
		</div>
	</nav>

	<main>
		@RenderSection("Hero", required:=False)

		<div class="container">
			@RenderBody()
			<hr />
		</div>
	</main>

	<footer class="container">
		<p>&copy; Soldata, @Date.Now.Year</p>
	</footer>

	<a href="#" class="back-to-top" aria-disabled="true" data-toggle="toTop" title="Наверх">
		<div class="fa fa-arrow-up" aria-label="Наверх"></div>
	</a>

	@Html.Action("bar", "dashboard", New With {.area = "admin", .editUrl = ViewBag.EditUrl})

	@Scripts.Render("~/bundles/jquery")
	@Scripts.Render("~/bundles/popper")
	@Scripts.Render("~/bundles/bootstrap")
	@RenderSection("Scripts", required:=False)
</body>
</html>
<!-- Софт Бизнес (http://soft.business/) -->
