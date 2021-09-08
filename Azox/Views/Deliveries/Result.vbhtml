@ModelType Soldata.Azox.Deliveries.Delivery
@Code
	ViewBag.Title = "Результат"
End Code

<div class="container">
	<header>
		<h1 class="h1">@ViewBag.Title</h1>
	</header>

	<p>
		@Model.Cost.ToString("C")
	</p>

	@Html.ActionLink("Доставка", "create")
</div>
