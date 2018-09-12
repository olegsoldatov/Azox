@Code
	ViewBag.Title = "Контакты"
	ViewBag.Description = ""
	ViewBag.Keywords = ""
End code

<div class="region container">
	<h1>@ViewBag.Title</h1>

	<div class="row">
		<div class="col-lg-6">
			<h2 class="h2">Адрес, телефон</h2>
			<p class="lead">
				Компания &laquo;Строймир&raquo;
			</p>
			<p>
				ул. Ванеева, д. 205<br />
				Нижний Новгород<br />
				603122
			</p>
			<p>
				Телефон: <a href="tel:+789202523636">8-920-252-36-36</a>
			</p>
			<p>
				Электронная почта: <a href="mailto:org-invest@yandex.ru">org-invest@yandex.ru</a>
			</p>
		</div>

		<div class="col-lg-6">
			@Html.Partial("ContactForm", New ContactFormViewModel)
		</div>
	</div>
</div>

<div>
	<script type="text/javascript" charset="utf-8" async src="https://api-maps.yandex.ru/services/constructor/1.0/js/?sid=TSCwPqnrC0vRMOIRjJ2RyA3-jpp2NF2_&amp;width=100%25&amp;height=450&amp;lang=ru_RU&amp;sourceType=constructor&amp;scroll=true"></script>
</div>

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section
