@ModelType Azox.Page
@Code
	ViewBag.Title = If(Model.Title, Model.Name)
	ViewBag.Description = If(Model.Description, "Информация о компании.")
	ViewBag.Heading = If(Model.Heading, If(Model.Title, Model.Name))
	ViewBag.EditUrl = Url.Action("edit", "pages", New With {.area = "admin", .id = Model.Id, .returnUrl = Request.Url.PathAndQuery})
End code

<div class="container">
	<h1>@ViewBag.Title</h1>

	<div class="row">
		<div class="col-lg-6">
			@Html.Raw(Model.Content)

			<div itemscope itemtype="http://schema.org/Organization">
				<p class="lead">
					ООО &laquo;<span itemprop="name">Просклад</span>&raquo;
				</p>
				<p itemprop="address" itemscope itemtype="http://schema.org/PostalAddress">
					<span itemprop="streetAddress">ул. Театральная, д. 23, кв. 26</span><br />
					г. <span itemprop="addressLocality">Кстово</span><br />
					<span itemprop="addressRegion">Нижегородская обл.</span><br />
					<span itemprop="postalCode">607650</span>
				</p>
				<p>
					Телефон: <a href="tel:+79307004382" itemprop="telephone">+7 930-700-43-82</a>
				</p>
				<p>
					Электронная почта: <a href="mailto:sklad_gd@bk.ru" itemprop="email">sklad_gd@bk.ru</a>
				</p>
				<p>
					Социальные сети:
				</p>
			</div>
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
