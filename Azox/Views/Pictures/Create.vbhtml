@ModelType PictureCreateViewModel

@Code
	ViewBag.Title = "Добавление изображения"
	Layout = "~/Views/Shared/_Dashboard.vbhtml"
End Code

@Section Toolbar
	<button class="btn" form="model-form">
		<span class="fa fa-upload"></span>
		<span>Загрузить</span>
	</button>
End Section

<header>
	<h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
	<div class="row">
		<div class="col-lg-4">
			@Using Html.BeginForm("create", Nothing, FormMethod.Post, New With {.enctype = "multipart/form-data", .id = "model-form"})
				@Html.AntiForgeryToken
				@Html.Hidden("ReturnUrl", Request.QueryString("ReturnUrl"))
				@Html.HiddenFor(Function(m) m.ProductId)

				@<div class="photo-uploads" id="photoUploads">
					<div class="form-group">
						<label class="control-label required">Файл изображения</label>
						@Html.ValidationMessageFor(Function(m) m.ImageFile, "", New With {.class = "text-danger"})
						<div>
							<input type="file" name="imageFile" accept="image/*" />
							<button type="button" class="btn btn-secondary btn-sm fa fa-plus" id="uploadPlus"></button>
						</div>
					</div>
					<div class="form-group">
						<input type="file" name="imageFile" accept="image/*" />
					</div>
					<div class="form-group">
						<input type="file" name="imageFile" accept="image/*" />
					</div>
				</div>
				@<div class="form-group">
					<small Class="form-text text-muted">@String.Format("Размер файла не более {0} МБ.", ViewBag.Length / 1024)</small>
				</div>

				@<div class="form-group">
					<button class="btn btn-primary">Загрузить</button>
				</div>
			End Using
		</div>
	</div>
</article>

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")

	<script>
		$("#uploadPlus").click(function () {
			$('<div class="form-group"><input type="file" name="imageFile" accept="image/*" /></div>').appendTo("#photoUploads");
		});
	</script>
End Section


