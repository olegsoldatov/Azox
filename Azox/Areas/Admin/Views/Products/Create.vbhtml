@ModelType Product
@Code
	ViewBag.Title = "Добавление продукта"
End Code

@Section Toolbar
	<button class="btn" form="model-form">
		<span class="fas fa-save"></span>
		<span>Сохранить</span>
	</button>
End Section

<h1>@ViewBag.Title</h1>
<hr />

@Using Html.BeginForm("create", Nothing, FormMethod.Post, New With {.id = "model-form"})
	@Html.AntiForgeryToken
	@<div class="row">
		<div class="col-md-9">
			@Html.EditorForModel
		</div>
		<div class="col-md-3">
			<div class="form-group">
				@Html.LabelFor(Function(model) model.Sku, htmlAttributes:=New With {.class = "control-label"})
				@Html.ValidationMessageFor(Function(model) model.Sku, "", New With {.class = "text-danger"})
				@Html.EditorFor(Function(model) model.Sku, New With {.htmlAttributes = New With {.class = "form-control"}})
			</div>

			<div class="form-group">
				<div class="checkbox">
					<label>
						@Html.EditorFor(Function(model) model.Draft)
						@Html.DisplayNameFor(Function(model) model.Draft)
					</label>
				</div>
			</div>

			<div class="form-group">
				<button class="btn btn-primary">Сохранить</button>
			</div>
		</div>
	</div>
End Using

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section
