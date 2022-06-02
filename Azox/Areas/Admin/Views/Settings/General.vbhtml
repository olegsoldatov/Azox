@ModelType GeneralSetting
@Code
    ViewBag.Title = "Общие параметры"
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
    @Using Html.BeginForm(Nothing, Nothing, FormMethod.Post, New With {.id = "model-form"})
        @Html.AntiForgeryToken
        @<div class="row">
             <div class="col-lg-4">
                 <div class="form-group">
                     @Html.LabelFor(Function(model) model.Title, htmlAttributes:=New With {.class = "control-label required"})
                     @Html.ValidationMessageFor(Function(model) model.Title, "", New With {.class = "text-danger"})
                     @Html.EditorFor(Function(model) model.Title, New With {.htmlAttributes = New With {.class = "form-control"}})
                 </div>

                 <div class="form-group">
                     @Html.LabelFor(Function(model) model.Description, htmlAttributes:=New With {.class = "control-label"})
                     @Html.ValidationMessageFor(Function(model) model.Description, "", New With {.class = "text-danger"})
                     @Html.EditorFor(Function(model) model.Description, New With {.htmlAttributes = New With {.class = "form-control"}})
                 </div>
             </div>
        </div>
    End Using
</article>

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
