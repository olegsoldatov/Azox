@ModelType ArticleCreateViewModel
@Code
	ViewBag.Title = "Добавление статьи"
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
	@Using Html.BeginForm("create", Nothing, FormMethod.Post, New With {.enctype = "multipart/form-data", .id = "model-form"})
		@Html.AntiForgeryToken
		@Html.ValidationSummary(True, "", New With {.class = "text-danger"})
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


