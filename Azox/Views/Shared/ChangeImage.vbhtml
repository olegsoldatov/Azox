@ModelType ChangeImageViewModel
@Code
	ViewBag.Title = "Изображение"
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
	@Using Html.BeginForm("changeimage", Nothing, FormMethod.Post, New With {.enctype = "multipart/form-data", .id = "model-form"})
		@Html.AntiForgeryToken
		@Html.ValidationSummary(True, "", New With {.class = "text-danger"})
		@<div class="form-group">
			@Html.LabelFor(Function(model) model.ImageFile, htmlAttributes:=New With {.class = "control-label required"})
			@Html.ValidationMessageFor(Function(model) model.ImageFile, "", New With {.class = "text-danger"})
			<div>
				<input type="file" name="imageFile" id="imageFile" accept="image/*" />
			</div>
			<p><small>@String.Format("Размер файла не более {0} МБ.", ViewBag.Length / 1024)</small></p>
		</div>

		@<div class="form-group">
			<button class="btn btn-primary">Сохранить</button>
		</div>
	End Using
</article>

