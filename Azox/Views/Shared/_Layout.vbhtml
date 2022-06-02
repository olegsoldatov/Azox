@Code
	Dim imageUrl = If(ViewBag.ImageUrl, Url.Action("default.jpg", "images", New With {.area = ""}, Request.Url.Scheme))
	Dim controllerName = Request.RequestContext.RouteData.Values("controller")
	Dim actionName = Request.RequestContext.RouteData.Values("action")
End Code
<!DOCTYPE html>
<html lang="ru">
<head prefix="og: http://ogp.me/ns#">
	<title>@ViewBag.Title</title>
	<meta charset="utf-8">
	<meta http-equiv="X-UA-Compatible" content="IE=edge">
	<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
	<meta property="og:url" content="@Request.Url.AbsoluteUri" />
	<meta property="og:title" name="title" content="@ViewBag.Title" />
	<meta property="og:image" content="@imageUrl" />
	<meta property="og:description" name="description" content="@ViewBag.Description" />
	<meta property="og:type" content="website" />
	<meta property="og:locale" content="ru_RU" />
	<link href="~/favicon.png" rel="icon" type="image/png" />
	<link href="@Url.Action(Nothing, Nothing, Nothing, Request.Url.Scheme)" rel="canonical" />
	@Styles.Render("~/Content/css")
	@RenderSection("Head", required:=False)
</head>
<body>
	<header>
		<nav class="navbar navbar-expand-lg sticky-top navbar-light bg-light">
			<div class="container">
				<a class="navbar-brand" href="~/">Azox</a>
				<button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
					<span class="navbar-toggler-icon"></span>
				</button>

				<div class="collapse navbar-collapse" id="navbarSupportedContent">
					<ul class="navbar-nav mr-auto">
						<li class="nav-item dropdown">
							<a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
								Каталог
							</a>
                            <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                                <a class="dropdown-item" href="~/products">Товары</a>
                                <a class="dropdown-item" href="~/categories">Категории</a>
                                <div class="dropdown-divider"></div>
                                <a class="dropdown-item" href="~/brands">Бренды</a>
                            </div>
						</li>
						<li class="nav-item">
							<a class="nav-link" href="~/about">О компании</a>
						</li>
						<li class="nav-item">
							<a class="nav-link" href="~/contacts">Контакты</a>
						</li>
					</ul>
					<form class="form-inline my-2 my-lg-0">
						<input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search">
						<button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
					</form>
				</div>
			</div>
		</nav>
	</header>

	<main>
		@RenderBody()
	</main>

	<footer>
		<hr />
		<div class="container">
			&copy; @Now.Year, Софт Бизнес
		</div>
	</footer>

	<a href="#" class="back-to-top" aria-disabled="true" data-toggle="toTop" title="Наверх">
		<div class="fa fa-arrow-up" aria-label="Наверх"></div>
	</a>

	@*@Html.Action("bar", "dashboard", New With {.area = "admin", .editUrl = ViewBag.EditUrl})*@

	@Scripts.Render("~/bundles/jquery")
	@Scripts.Render("~/bundles/bootstrap")
	@Scripts.Render("~/bundles/scripts")
	@RenderSection("Scripts", required:=False)
</body>
</html>
<!-- Дизайн и разработка Софт Бизнес https://soft.business -->
