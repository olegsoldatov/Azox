@ModelType IPage
@Code
    ViewBag.Title = Model.Title
    ViewBag.Description = Model.Description
    ViewBag.EditUrl = Url.Action("edit", "pages", New With {.area = "admin", Model.Id})
End code

<div class="container">
    <article>
        <h1>@ViewBag.Title</h1>
        <p class="lead">
            @Model.Description
        </p>
        <div class="row">
            <div class="col-lg-6">
                @Html.Raw(Model.Content)
                <p>
                    Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent luptatum zzril delenit augue duis dolore te feugait nulla facilisi.
                </p>
            </div>

            <div class="col-lg-6">
                @Html.Partial("_ContactForm", New ContactFormViewModel)
            </div>
        </div>
    </article>
</div>

<div>
    <script type="text/javascript" charset="utf-8" async src="https://api-maps.yandex.ru/services/constructor/1.0/js/?um=constructor%3A0b8977857a3a1155fb088a7d403f8beb8e75096fe57970c7b7cb182ed2301a39&amp;width=100%25&amp;height=400&amp;lang=ru_RU&amp;scroll=true"></script>
</div>

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
