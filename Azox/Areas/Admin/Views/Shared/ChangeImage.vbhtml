@ModelType ChangeImageViewModel
@Code
	ViewBag.Title = "Изображение"
End Code

@Section Toolbar
	<a class="button" href="#" onclick="$('#ModelForm').submit(); return false;"><span class="fa fa-save">&nbsp;&nbsp;</span>Сохранить</a>
End Section

<h1>@ViewBag.Title</h1>
<hr />

@Using Html.BeginForm("changeimage", Nothing, FormMethod.Post, New With {.enctype = "multipart/form-data", .role = "form", .id = "ModelForm"})
	@Html.AntiForgeryToken
	@Html.HiddenFor(Function(model) model.Id)
	@<div class="form-group">
		@Html.LabelFor(Function(model) model.ImageFile, htmlAttributes:=New With {.class = "control-label"})
		@Html.ValidationMessageFor(Function(model) model.ImageFile, "", New With {.class = "text-danger"})
		<input type="file" name="imageFile" id="imageFile" accept="image/*" />
		<small class="help-block">Размер файла не более 4 мегабайт.</small>
	</div>

	@<div class="form-group">
		<button class="btn btn-default">Сохранить</button>
		@Html.ActionLink("Отменить", "edit", New With {.id = Model.Id}, New With {.class = "btn btn-link"})
	</div>
End Using

