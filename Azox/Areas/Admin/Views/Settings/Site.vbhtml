@ModelType ValueTuple(Of SiteName, Author)
@Code
    ViewBag.Title = "Сайт"
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
    @Using Html.BeginForm(Nothing, Nothing, Nothing, FormMethod.Post, New With {.id = "model-form"})
        @Html.AntiForgeryToken
        @<div class="row">
            <div class="col-lg-3">
                <fieldset>
                    <legend>Имя сайта</legend>
                    @Html.HiddenFor(Function(model) model.Item1.Id)
                    <div class="form-group">
                        @Html.LabelFor(Function(model) model.Item1.Value, htmlAttributes:=New With {.class = "control-label"})
                        @Html.ValidationMessageFor(Function(model) model.Item1.Value, "", New With {.class = "text-danger"})
                        @Html.EditorFor(Function(model) model.Item1.Value, New With {.htmlAttributes = New With {.class = "form-control"}})
                    </div>
                    <div class="form-group">
                        @Html.LabelFor(Function(model) model.Item1.Description, htmlAttributes:=New With {.class = "control-label"})
                        @Html.ValidationMessageFor(Function(model) model.Item1.Description, "", New With {.class = "text-danger"})
                        @Html.EditorFor(Function(model) model.Item1.Description, New With {.htmlAttributes = New With {.class = "form-control"}})
                    </div>
                </fieldset>
                <fieldset>
                    <legend>Автор</legend>
                    @Html.HiddenFor(Function(model) model.Item2.Id)
                    <div class="form-group">
                        @Html.LabelFor(Function(model) model.Item2.Value, htmlAttributes:=New With {.class = "control-label"})
                        @Html.ValidationMessageFor(Function(model) model.Item2.Value, "", New With {.class = "text-danger"})
                        @Html.EditorFor(Function(model) model.Item2.Value, New With {.htmlAttributes = New With {.class = "form-control"}})
                    </div>
                </fieldset>
            </div>
        </div>
    End Using
</article>


@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section