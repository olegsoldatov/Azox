@Code
    Dim title = If(ViewBag.Title, Settings.SiteName.Value)
    Dim description = If(ViewBag.Description, Settings.SiteName.Description)
    Dim imageUrl = If(ViewBag.ImageUrl, Url.Action("default.jpg", "images", New With {.area = ""}, Request.Url.Scheme))
    Dim canonicalUrl = If(ViewBag.CanonicalUrl, Url.Action(Nothing, Nothing, Nothing, Request.Url.Scheme).ToLower)
End Code
<!-- Дизайн и разработка: Софт Бизнес https://soft.business/ -->
<!DOCTYPE html>
<html lang="ru">
<head prefix="og: http://ogp.me/ns#">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="format-detection" content="telephone=no" />
    <title>@title</title>
    <meta property="og:url" content="@Request.Url.AbsoluteUri" />
    <meta property="og:type" content="website" />
    <meta property="og:locale" content="ru_RU" />
    <meta property="og:title" content="@title" />
    <meta property="og:description" name="description" content="@description" />
    <meta property="og:image" content="@imageUrl" />
    <link href="~/favicon.png" rel="icon" type="image/png" />
    <link href="@canonicalUrl" rel="canonical" />
    @Styles.Render("~/Content/css")
    @RenderSection("Head", required:=False)
</head>
<body>
    @Html.Partial("_Navbar")

    <main>
        @RenderBody()
    </main>

    <div class="container">
        <footer class="d-flex flex-wrap justify-content-between align-items-center py-3 my-4 border-top">
            <div class="col-md-4 d-flex align-items-center">
                <span class="mb-3 mb-md-0 text-muted">&copy; @Settings.Author.Value, @Now.Year</span>
            </div>
        </footer>
    </div>

    <a href="#" class="back-to-top" aria-disabled="true" data-toggle="toTop" title="Наверх">
        <div class="fa fa-arrow-up" aria-label="Наверх"></div>
    </a>

    @Scripts.Render("~/bundles/jquery", "~/Scripts/bootstrap.bundle.js")
    @Scripts.Render("~/bundles/scripts")
    @RenderSection("Scripts", required:=False)
</body>
</html>
