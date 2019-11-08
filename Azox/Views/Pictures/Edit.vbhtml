@ModelType Picture
@Code
	ViewBag.Title = "Изменение изображения"
	Layout = "~/Views/Shared/_Dashboard.vbhtml"
End Code

@Section Toolbar
	<button class="btn" form="model-form">
		<span class="fa fa-save"></span>
		<span>Сохранить</span>
	</button>
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
		@Html.HiddenFor(Function(model) model.ProductId)
		@<div class="row">
			<div class="col-md-9">
				<div class="form-group">
					@Html.LabelFor(Function(model) model.Name, htmlAttributes:=New With {.class = "control-label required"})
					@Html.ValidationMessageFor(Function(model) model.Name, "", New With {.class = "text-danger"})
					@Html.EditorFor(Function(model) model.Name, New With {.htmlAttributes = New With {.class = "form-control"}})
				</div>
			</div>
			<div class="col-md-3">
				<div class="form-group">
					@Html.LabelFor(Function(model) model.ImageId, htmlAttributes:=New With {.class = "control-label"})
					@Html.ValidationMessageFor(Function(model) model.ImageId, "", New With {.class = "text-danger"})
					@Html.EditorFor(Function(model) model.ImageId, "Thumbnail", New With {.htmlAttributes = New With {.class = "form-control"}, .id = Model.Id})
				</div>

				<div class="form-group">
					@Html.LabelFor(Function(model) model.Order, htmlAttributes:=New With {.class = "control-label"})
					@Html.ValidationMessageFor(Function(model) model.Order, "", New With {.class = "text-danger"})
					@Html.EditorFor(Function(model) model.Order, New With {.htmlAttributes = New With {.class = "form-control"}})
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

