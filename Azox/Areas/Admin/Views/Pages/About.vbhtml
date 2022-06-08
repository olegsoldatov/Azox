﻿@ModelType AboutPage
@Code
    ViewBag.Title = ViewBag.Heading
End Code

@Section Toolbar
    <button class="btn" form="model-form">
        <span class="fa fa-save"></span>
        <span>Сохранить</span>
    </button>
End Section

<header>
    <h1>@ViewBag.Heading</h1>
</header>

<article>
    @Using Html.BeginForm(Nothing, Nothing, FormMethod.Post, New With {.id = "model-form"})
        @Html.AntiForgeryToken
        @Html.HiddenFor(Function(model) model.Id)
        @<text>
            <ul class="nav nav-tabs">
                <li class="nav-item">
                    <a class="nav-link active" id="home-tab" data-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true">Страница</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" id="seo-tab" data-toggle="tab" href="#seo" role="tab" aria-controls="seo" aria-selected="false">SEO</a>
                </li>
            </ul>
            <div class="tab-content">
                <div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">
                    <div class="form-group">
                        @Html.LabelFor(Function(model) model.Heading, New With {.class = "control-label required"})
                        @Html.ValidationMessageFor(Function(model) model.Heading, "", New With {.class = "text-danger"})
                        @Html.EditorFor(Function(model) model.Heading, New With {.htmlAttributes = New With {.class = "form-control"}})
                    </div>
                    <div class="form-group">
                        @Html.LabelFor(Function(model) model.Content, htmlAttributes:=New With {.class = "control-label"})
                        @Html.ValidationMessageFor(Function(model) model.Content, "", New With {.class = "text-danger"})
                        @Html.EditorFor(Function(model) model.Content, New With {.htmlAttributes = New With {.class = "form-control"}})
                    </div>
                </div>
                <div class="tab-pane fade" id="seo" role="tabpanel" aria-labelledby="seo-tab">
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
        </text>
    End Using
</article>

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section