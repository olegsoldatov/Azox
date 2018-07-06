@ModelType Product
@Code
	ViewBag.Title = "Изменение продукта"
End Code

@Section Toolbar
	<button class="btn" form="ModelForm"><span class="fas fa-save">&nbsp;&nbsp;</span>Сохранить</button>
	<a class="btn" href="@Url.Action("Details", New With {.area = "", .id = Model.Id})"><span class="fa fa-eye">&nbsp;&nbsp;</span>Посмотреть</a>
	<a class="btn" href="@Url.Action("Create")"><span class="fa fa-plus">&nbsp;&nbsp;</span>Добавить</a>
End Section

<h1>@ViewBag.Title</h1>
<hr />

@Html.Partial("_Alert")

@Using Html.BeginForm("Edit", Nothing, FormMethod.Post, New With {.id = "ModelForm"})
	@Html.AntiForgeryToken
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

			<div class="form-group">
				<button class="btn btn-primary">Сохранить</button>
				<a href="@Url.Action("delete", New With {.id = Model.Id})" class="btn btn-link">Удалить</a>
			</div>
		</div>
	</div>
End Using

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section
