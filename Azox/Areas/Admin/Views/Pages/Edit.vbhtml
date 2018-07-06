@ModelType Azox.Page
@Code
	ViewBag.Title = "Изменение страницы"
	Dim returnUrl = If(Request.QueryString("returnUrl"), Url.Action("index"))
End Code

@Section Toolbar
	<button type="submit" class="btn" form="modelForm"><span class="fa fa-save">&nbsp;&nbsp;</span>Сохранить</button>
	<a href="@Url.Action(Model.ActionName, Model.ControllerName, New With {.area = ""})" class="btn" target="_blank"><span class="fa fa-eye">&nbsp;&nbsp;</span>Посмотреть</a>
End Section

<h1>@ViewBag.Title</h1>
<hr />

@Using Html.BeginForm("edit", Nothing, FormMethod.Post, New With {.id = "modelForm"})
	@Html.AntiForgeryToken
	@Html.ValidationSummary(True, "", New With {.class = "text-danger"})
	@Html.Hidden("returnUrl", returnUrl)
	@Html.HiddenFor(Function(model) model.Id)
	@Html.EditorForModel
	@<div class="form-group">
		<button type="submit" class="btn btn-primary">Сохранить</button>
		<a href="@returnUrl" class="btn btn-link">Отменить</a>
	</div>
End Using

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section


