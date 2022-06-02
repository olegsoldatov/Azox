@ModelType Product
@Code
    ViewBag.Title = "Изменение товара"
    Dim returnUrl = Request.QueryString("ReturnUrl")
End Code

@Section Toolbar
    <button class="btn" form="model-form">
        <span class="fa fa-save"></span>
        <span>Сохранить</span>
    </button>
    <a class="btn" href="@Url.Action("product", New With {Model.Id, .area = ""})" target="_blank">
        <span class="fa fa-eye"></span>
        <span>Посмотреть</span>
    </a>
    <a class="btn" href="@Url.Action("delete", New With {Model.Id, returnUrl})">
        <span class="fa fa-remove"></span>
        <span>Удалить</span>
    </a>
End Section

<header>
    <h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
    @Using Html.BeginForm(Nothing, Nothing, New With {returnUrl}, FormMethod.Post, New With {.id = "model-form", .enctype = "multipart/form-data"})
        @Html.AntiForgeryToken
        @Html.ValidationSummary(True)
        @Html.HiddenFor(Function(model) model.Id)
        @Html.EditorForModel
    End Using
</article>

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section

