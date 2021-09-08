@ModelType DeliveryViewModel
@Code
	ViewBag.Title = "Доставка"
End Code

<div class="container">
	<header>
		<h1 class="h1">@ViewBag.Title</h1>
	</header>

	@Using Html.BeginForm
		@Html.AntiForgeryToken

		@<div class="row">
			<div class="col-lg-8">
				@Html.EditorForModel
			</div>
		</div>

		@<div class="form-group">
			<button class="btn btn-primary">Продолжить</button>
		</div>
	End Using
</div>

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section

