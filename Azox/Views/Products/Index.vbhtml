@Code
	ViewBag.Title = "Продукция"
	ViewBag.Description = "Каталог продукции."
End Code

<div class="row">
	<div class="col-md-3">
		<div>
			<form>
				@Html.Partial("_Filter", ViewBag.Filter)
			</form>
		</div>
	</div>

	<div class="col-md-9">
		<h1>@ViewBag.Title</h1>

		@Html.Action("List")
	</div>
</div>
