@ModelType Brand
@Code
    ViewBag.Title = "Изменение бренда"
    Dim returnUrl = Request.QueryString("ReturnUrl")
End Code

@Section Toolbar
	<button class="btn" form="ModelForm">
		<span class="fa fa-save"></span>
		<span>Сохранить</span>
	</button>
End Section

<header>
	<h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
    @Using Html.BeginForm(Nothing, Nothing, New With {returnUrl}, FormMethod.Post, New With {.id = "ModelForm", .enctype = "multipart/form-data"})
        @Html.AntiForgeryToken
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
        @Html.HiddenFor(Function(model) model.Id)
        @Html.EditorForModel
    End Using
</article>

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section

