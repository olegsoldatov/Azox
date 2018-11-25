@ModelType Product
@Code
	ViewBag.Title = "Изменение продукта"
End Code

@Section Toolbar
	<button class="btn" form="model-form">
		<span class="fa fa-save"></span>
		<span>Сохранить</span>
	</button>
	<a class="btn" href="@Url.Action("details", New With {.area = "", .id = Model.Id})">
		<span class="fa fa-eye"></span>
		<span>Посмотреть</span>
	</a>
End Section

<header>
	<h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
	@Html.Partial("_Alert")

	@Using Html.BeginForm("edit", Nothing, FormMethod.Post, New With {.id = "model-form"})
		@Html.AntiForgeryToken
		@Html.Hidden("ReturnUrl", Request.QueryString("ReturnUrl"))
		@Html.HiddenFor(Function(model) model.Id)
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

			</div>
		</div>
		@<div class="form-group">
			<button class="btn btn-primary">Сохранить</button>
		</div>
	End Using
</article>

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section
