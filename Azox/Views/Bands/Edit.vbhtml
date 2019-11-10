﻿@ModelType Brand
@Code
	ViewBag.Title = "Изменение бренда"
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

	@Using Html.BeginForm("edit", Nothing, FormMethod.Post, New With {.enctype = "multipart/form-data", .id = "model-form"})
		@Html.AntiForgeryToken
		@Html.Hidden("ReturnUrl", Request.QueryString("ReturnUrl"))
		@Html.HiddenFor(Function(model) model.Id)
		@<div class="row">
			<div class="col-md-9">
				@Html.EditorForModel
			</div>
			<div class="col-md-3">
				<div class="form-group">
					@Html.LabelFor(Function(model) model.ImageFile, htmlAttributes:=New With {.class = "control-label"})
					@Html.ValidationMessageFor(Function(model) model.ImageFile, "", New With {.class = "text-danger"})
					@Html.EditorFor(Function(model) model.ImageFile, New With {.accept = "image/*"})
					@Html.EditorFor(Function(model) model.ImageId, "Thumbnail", New With {.class = "img-fluid", .alt = "Миниатюра"})
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