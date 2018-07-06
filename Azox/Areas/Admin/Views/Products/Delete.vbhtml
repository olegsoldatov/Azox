@ModelType Product
@Code
	ViewBag.Title = "Удаление продукта"
End Code

<h1>@ViewBag.Title</h1>
<hr />

<p class="lead">Вы действительно хотите удалить продукт &laquo;@Model.Title&raquo;?</p>

@Using Html.BeginForm
	@Html.AntiForgeryToken
	@<div class="form-actions no-color">
		<button class="btn btn-danger">Удалить</button>
	</div>
End Using
