<!DOCTYPE html>
<html lang="ru">
<head>
    <title>@ViewBag.Title</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link href="~/favicon.svg" rel="icon" type="image/svg" />
    <link href="~/Content/font-awesome.min.css" rel="stylesheet" />
    @Styles.Render("~/Content/account")
</head>
<body class="text-center">
    <main class="account-form w-100 m-auto">
        <img class="mb-4" src="~/Images/full-logo.svg" alt="Логотип" height="40" />
        @RenderBody()
    </main>
    @Scripts.Render("~/bundles/jquery", "~/Scripts/bootstrap.bundle.js")
    @RenderSection("scripts", required:=False)
</body>
</html>
<!-- Дизайн и разработка Софт Бизнес https://soft.business/ -->
