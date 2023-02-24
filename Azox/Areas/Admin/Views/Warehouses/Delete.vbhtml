﻿@ModelType Warehouse
@Code
    ViewBag.Title = "Удаление магазина / склада"
    Dim returnUrl = If(Request.QueryString("ReturnUrl"), Url.Action("index"))
End Code

<header>
    <h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
    <p class="lead">Вы уверены, что хотите удалить магазин / склад &laquo;@Model.Name&raquo;?</p>
    @Using Html.BeginForm(New With {returnUrl})
        @Html.AntiForgeryToken
        @<div class="form-actions no-color">
            <button class="btn btn-danger">Удалить</button>
            <a href="@returnUrl" class="btn btn-outline-secondary">Отменить</a>
        </div>
    End Using
</article>
