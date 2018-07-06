@ModelType Service
@Code
	ViewBag.Title = "Изменение услуги"
End Code

@Section Toolbar
	<a class="button" href="#" onclick="$('#ModelForm').submit(); return false;"><span class="fa fa-save">&nbsp;&nbsp;</span>Сохранить</a>
	<a class="button" href="@Url.Action("details", New With {.id = Model.Id, .area = ""})" target="_blank"><span class="fa fa-eye">&nbsp;&nbsp;</span>Посмотреть</a>
	<a class="button" href="@Url.Action("delete", New With {.id = Model.Id})"><span class="fa fa-trash">&nbsp;&nbsp;</span>Удалить</a>
End Section

<h1>@ViewBag.Title</h1>
<hr />

@If Not IsNothing(TempData("Message")) Then
	@<div class="alert alert-info alert-dismissible" role="alert">
		<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
		@TempData("Message")
	</div>
End If

@Using Html.BeginForm("edit", Nothing, FormMethod.Post, New With {.enctype = "multipart/form-data", .id = "ModelForm"})
	@Html.AntiForgeryToken()
	@Html.ValidationSummary(True, "", New With {.class = "text-danger"})
	@Html.HiddenFor(Function(model) model.Id)
	@Html.HiddenFor(Function(model) model.CreationDate)
	@Html.EditorForModel
	@<div class="form-group">
		<button type="submit" class="btn btn-primary">Сохранить</button>
		@Html.ActionLink("Отменить", "index", Nothing, New With {.class = "btn btn-link"})
	</div>
End Using

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section
