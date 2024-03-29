﻿@Modeltype IEnumerable(Of Setting)
@Code
    ViewBag.Title = "Настройки"
End Code

<header>
    <h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
    <div class="table-responsive">
        <table class="table table-hover">
            <thead>
                <tr>
                    <th>@Html.DisplayNameFor(Function(model) model.Name)</th>
                    <th>@Html.DisplayNameFor(Function(model) model.Value)</th>
                    <th>@Html.DisplayNameFor(Function(model) model.Description)</th>
                </tr>
            </thead>
            <tbody>
                @For Each item In Model
                    @<tr>
                        <td>@Html.DisplayFor(Function(model) item.Name)</td>
                        <td>@Html.DisplayFor(Function(model) item.Value)</td>
                        <td>@Html.DisplayFor(Function(model) item.Description)</td>
                    </tr>
                Next
            </tbody>
        </table>
    </div>
</article>
