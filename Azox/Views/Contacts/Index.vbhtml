@ModelType ContactsPage
@Code
    ViewBag.Title = If(Model.Seo.Title, Model.Heading)
    ViewBag.Description = Model.Seo.Description
    ViewBag.EditUrl = Url.Action("edit", "pages", New With {.area = "admin", Model.Id})
End code

<div class="container">
    <header>
        <h1 class="h1">@Model.Heading</h1>
    </header>

	<div class="row">
		<div class="col-lg-6">
			@Html.Raw(Model.Content)
		</div>

		<div class="col-lg-6">
			@Html.Partial("_ContactForm", New ContactFormViewModel)
		</div>
	</div>
</div>

<div>
	<script type="text/javascript" charset="utf-8" async src="https://api-maps.yandex.ru/services/constructor/1.0/js/?um=constructor%3A0b8977857a3a1155fb088a7d403f8beb8e75096fe57970c7b7cb182ed2301a39&amp;width=100%25&amp;height=400&amp;lang=ru_RU&amp;scroll=true"></script>
</div>

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section
