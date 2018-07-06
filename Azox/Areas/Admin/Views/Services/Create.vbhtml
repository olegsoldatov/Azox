@ModelType Service
@Code
	ViewBag.Title = "Добавление услуги"
End Code

@Section Toolbar
	<a class="button" href="#" onclick="$('#ModelForm').submit(); return false;"><span class="fa fa-save">&nbsp;&nbsp;</span>Сохранить</a>
End Section

<h1>@ViewBag.Title</h1>
<hr />

@Using Html.BeginForm("create", Nothing, FormMethod.Post, New With {.enctype = "multipart/form-data", .id = "ModelForm"})
	@Html.AntiForgeryToken()
	@Html.ValidationSummary(True, "", New With {.class = "text-danger"})
	@Html.EditorForModel
	@<div class="form-group">
		<button type="submit" class="btn btn-primary">Сохранить</button>
		@Html.ActionLink("Отменить", "index", Nothing, New With {.class = "btn btn-link"})
	</div>
End Using

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section

