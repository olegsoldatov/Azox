<!DOCTYPE html>
<html lang="ru">
<head>
	<title>@ViewBag.Title</title>
	<meta charset="utf-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=edge">
	<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
	<link href="~/favicon.png" rel="icon" type="image/png" />
	@Styles.Render("~/Content/account")
</head>
<body>
	@RenderBody()
	@Scripts.Render("~/bundles/jquery")
	@Scripts.Render("~/bundles/bootstrap")
	@RenderSection("scripts", required:=False)
</body>
</html>
<!-- Дизайн и разработка Софт Бизнес http://soft.business -->
