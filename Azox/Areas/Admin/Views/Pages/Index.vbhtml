@Code
    ViewBag.Title = "Страницы"
End Code

<header>
    <h1>@ViewBag.Title</h1>
</header>

<article>
    <div class="row">
        <div class="col-lg-3">
            <nav class="nav flex-column">
                @Html.ActionLink("О компании", "about", "pages", Nothing, New With {.class = "nav-link"})
                @Html.ActionLink("Контакты", "contacts", "pages", Nothing, New With {.class = "nav-link"})
                @Html.ActionLink("Доставка", "delivery", "pages", Nothing, New With {.class = "nav-link"})
                @Html.ActionLink("Условия использования", "terms", "pages", Nothing, New With {.class = "nav-link"})
                @Html.ActionLink("Политика конфиденциальности", "policy", "pages", Nothing, New With {.class = "nav-link"})
            </nav>
        </div>
    </div>
</article>