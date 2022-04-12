@ModelType IEnumerable(Of Azox.Page)
@Code
    ViewBag.Title = "Страницы"
    Dim returnUrl = Request.Url.PathAndQuery
End Code

@Section Toolbar
    <a href="@Url.Action("create")" class="btn">
        <span class="fa fa-plus-circle"></span>
        <span>Добавить</span>
    </a>
    <div class="btn-group">
        <button class="btn dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            <span class="fa fa-ellipsis-h"></span>
            <span>Действия</span>
        </button>
        <div class="dropdown-menu">
            <a href="#" class="dropdown-item" data-toggle="modal" data-target="#deleteModal">Удалить</a>
        </div>
    </div>
End Section

<header>
    @Html.Title("страница", "страницы", "страниц", New With {.class = "heading"})
    @Html.Pagination(New With {.class = "pagination"})
</header>

<article>
    @If Model.Any Then
        @Using Html.BeginForm(Nothing, Nothing, Nothing, FormMethod.Post, New With {.id = "contentForm"})
            @Html.AntiForgeryToken
            @Html.Partial("_Delete")
            @<div class="table-responsive">
                <table class="table table-hover">
                    <thead>
                        <tr>
                            <th width="40">
                                <input type="checkbox" data-toggle="check-all" aria-controls="id" />
                            </th>
                            <th>
                                @Html.DisplayNameFor(Function(model) model.Title)
                            </th>
                            <th>
                                @Html.DisplayNameFor(Function(model) model.LastUpdateDate)
                            </th>
                            <th width="100"></th>
                        </tr>
                    </thead>
                    <tbody>
                        @For Each item In Model
                            @<tr>
                                <td>
                                    <input type="checkbox" name="id" value="@item.Id" />
                                </td>
                                <td>
                                    @Html.ActionLink(item.Title, "edit", New With {item.Id, returnUrl}, New With {.title = item.Title})
                                </td>
                                <td>
                                    @Html.DisplayFor(Function(model) item.LastUpdateDate)
                                </td>
                                <td class="text-right">
                                    <a href="@item.AbsolutePath" title="Посмотреть" target="_blank"><span class="fa fa-external-link"></span></a>
                                    <a href="@Url.Action("delete", New With {item.Id, returnUrl})" title="Удалить"><span class="fa fa-remove"></span></a>
                                </td>
                            </tr>
                        Next
                    </tbody>
                </table>
            </div>
        End Using
    Else
        @<p class="lead text-center">Список пуст.</p>
    End If

    @Html.Pagination(New With {.class = "pagination"})
</article>
