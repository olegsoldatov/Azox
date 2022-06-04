@ModelType AboutSetting

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
    @Using Html.BeginForm(Nothing, Nothing, Nothing, FormMethod.Post, New With {.id = "model-form"})
        @Html.AntiForgeryToken
        @<div class="form-group">
            @Html.LabelFor(Function(model) model.Content, New With {.class = "control-label"})
            @Html.ValidationMessageFor(Function(model) model.Content, "", New With {.class = "text-danger"})
            @Html.EditorFor(Function(model) model.Content)
        </div>
        @<div class="form-group">
            @Html.LabelFor(Function(model) model.Description, New With {.class = "control-label"})
            @Html.ValidationMessageFor(Function(model) model.Description, "", New With {.class = "text-danger"})
            @Html.EditorFor(Function(model) model.Description, New With {.htmlAttributes = New With {.class = "form-control"}})
            <small class="form-text text-muted">Может использоваться для мета-тэга 'description'.</small>
        </div>
    End Using
</article>
