@ModelType IEnumerable(Of Product)
@Code
	ViewBag.Title = "Продукция"
	ViewBag.Description = "Каталог продукции."
End Code

<div class="container">
	<h1>@ViewBag.Title</h1>

	<div class="row">
		<div class="col-md-3">
			<div>
				<form>
					@Html.Partial("_Filter", ViewBag.Filter)
				</form>
			</div>
		</div>

		<div class="col-md-9">
			@If Model.Any Then
				@<text>...</text>
			Else
				@<p class="lead text-center">Список пуст.</p>
			End If
		</div>
	</div>
</div>
