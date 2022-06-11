@ModelType Azox.Page
@Code
    ViewBag.Title = Model.Heading
    Dim returnUrl = Request.Url.PathAndQuery
End Code

@Section Toolbar
    <button class="btn" form="model-form">
        <span class="fa fa-save"></span>
        <span>Сохранить</span>
    </button>
End Section

<header>
    <h1>@ViewBag.Title</h1>
</header>

<article>
    <div class="row">
        <div class="col-lg-9">
            @Using Html.BeginForm("edit", Nothing, New With {Model.Id, returnUrl}, FormMethod.Post, New With {.id = "model-form"})
                @Html.AntiForgeryToken
                @Html.EditorForModel
            End Using
        </div>
        <div class="col-lg-3">
            <nav class="nav flex-column">
                @Html.ActionLink("О компании", "about", "pages", Nothing, New With {.class = "nav-link active"})
                @Html.ActionLink("Контакты", "contacts", "pages", Nothing, New With {.class = "nav-link"})
                @Html.ActionLink("Доставка", "delivery", "pages", Nothing, New With {.class = "nav-link"})
                @Html.ActionLink("Условия использования", "terms", "pages", Nothing, New With {.class = "nav-link"})
                @Html.ActionLink("Политика конфиденциальности", "policy", "pages", Nothing, New With {.class = "nav-link"})
            </nav>
        </div>
    </div>
</article>

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
