@ModelType Product
@Code
	ViewBag.Title = Model.Name
	ViewBag.Description = ""
	ViewBag.EditUrl = Url.Action("edit", New With {.id = Model.Id, .returnUrl = Request.Url.AbsolutePath})
End Code

<div class="container">
	<div class="row mt-5">
		<div class="col-md-6">
			<picture>
				@If Model.Pictures.Any Then
					@Html.DisplayFor(Function(model) model.Pictures.First.ImageId, "Medium", New With {.htmlAttributes = New With {.alt = Model.Pictures.First.Name, .class = "img-fluid"}})
				Else
					@<img src="http://placehold.it/544x306" alt="..." class="img-fluid" />
				End If
			</picture>
		</div>
		<duv class="col-md-6">
			<h1>@ViewBag.Title</h1>
			<p class="sku lead text-muted">
				@Html.DisplayNameFor(Function(model) model.Sku):
				<span>
					@Html.DisplayFor(Function(model) model.Sku)
				</span>
			</p>
			<p class="description">
				@Html.DisplayFor(Function(model) model.Description)
			</p>
		</duv>
	</div>

</div>
