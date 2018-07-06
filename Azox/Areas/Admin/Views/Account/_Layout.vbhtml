<!-- Дизайн и разработка Soldata (http://soldata.ru/) -->
<!DOCTYPE html>
<html lang="ru">
<head>
	<title>@ViewBag.Title</title>
	<meta charset="utf-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=edge">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<meta name="description" content="Форма авторизации" />
	<link href="~/favicon.png" rel="icon" type="image/png" />
	@Styles.Render("~/Content/bootstrap", "~/Content/signin")
	<!-- HTML5 shim for IE8 support of HTML5 elements and media queries -->
	<!--[if lt IE 9]>
	  <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
	<![endif]-->
</head>
<body class="signin">
	<div class="container">
		@RenderBody()
	</div>
	@Scripts.Render("~/bundles/jquery")
	@Scripts.Render("~/bundles/bootstrap")
	@RenderSection("scripts", required:=False)
</body>
</html>
