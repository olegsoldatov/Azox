@ModelType ArticleEditViewModel
@Code
	ViewBag.Title = "Изменение статьи"
End Code

@Section Toolbar
	<button class="btn" form="model-form">
		<span class="fa fa-save"></span>
		<span>Сохранить</span>
	</button>
	<a class="btn" href="@Url.Action("details", "articles", New With {.id = Model.Id, .area = ""})" target="_blank">
		<span class="fa fa-external-link"></span>
		<span>Посмотреть</span>
	</a>
	<a class="btn" href="@Url.Action("delete", New With {.id = Model.Id})">
		<span class="fa fa-remove"></span>
		<span>Удалить</span>
	</a>
End Section

<header>
	<h1 class="heading">@ViewBag.Title</h1>
	@Html.Partial("_Alert")
</header>

<article>
	@Using Html.BeginForm("edit", Nothing, FormMethod.Post, New With {.enctype = "multipart/form-data", .id = "model-form"})
		@Html.AntiForgeryToken
		@Html.ValidationSummary(True, "", New With {.class = "text-danger"})
		@Html.Hidden("ReturnUrl", Request.QueryString("ReturnUrl"))
		@Html.HiddenFor(Function(model) model.Id)
		@<div class="row">
			<div class="col-lg-9">
				<div class="form-group">
					@Html.LabelFor(Function(model) model.Name, htmlAttributes:=New With {.class = "control-label required"})
					@Html.ValidationMessageFor(Function(model) model.Name, "", New With {.class = "text-danger"})
					@Html.EditorFor(Function(model) model.Name, New With {.htmlAttributes = New With {.class = "form-control"}})
				</div>

				<div class="form-group">
					@Html.LabelFor(Function(model) model.Content, htmlAttributes:=New With {.class = "control-label"})
					@Html.ValidationMessageFor(Function(model) model.Content, "", New With {.class = "text-danger"})
					@Html.EditorFor(Function(model) model.Content, New With {.htmlAttributes = New With {.class = "form-control"}})
				</div>

				<fieldset>
					<legend>SEO</legend>
					<div class="form-group">
						@Html.LabelFor(Function(model) model.Title, htmlAttributes:=New With {.class = "control-label"})
						@Html.ValidationMessageFor(Function(model) model.Title, "", New With {.class = "text-danger"})
						@Html.EditorFor(Function(model) model.Title, New With {.htmlAttributes = New With {.class = "form-control"}})
					</div>

					<div class="form-group">
						@Html.LabelFor(Function(model) model.Description, htmlAttributes:=New With {.class = "control-label"})
						@Html.ValidationMessageFor(Function(model) model.Description, "", New With {.class = "text-danger"})
						@Html.EditorFor(Function(model) model.Description, New With {.htmlAttributes = New With {.class = "form-control"}})
					</div>

					<div class="form-group">
						@Html.LabelFor(Function(model) model.Keywords, htmlAttributes:=New With {.class = "control-label"})
						@Html.ValidationMessageFor(Function(model) model.Keywords, "", New With {.class = "text-danger"})
						@Html.EditorFor(Function(model) model.Keywords, New With {.htmlAttributes = New With {.class = "form-control"}})
					</div>
				</fieldset>
			</div>
			<div class="col-lg-3">
				<div class="form-group">
					@Html.LabelFor(Function(model) model.Slug, htmlAttributes:=New With {.class = "control-label"})
					@Html.ValidationMessageFor(Function(model) model.Slug, "", New With {.class = "text-danger"})
					@Html.EditorFor(Function(model) model.Slug, New With {.htmlAttributes = New With {.class = "form-control"}})
				</div>
			</div>
		</div>
	End Using
</article>

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section

