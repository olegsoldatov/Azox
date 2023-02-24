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
        <button type="button" class="btn dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            <span>Действия</span>
        </button>
        <div class="dropdown-menu">
            <button type="button" class="dropdown-item" data-toggle="modal" data-target="#deleteModal">Удалить</button>
        </div>
    </div>
End Section

<header>
    <h1>@ViewBag.Title <sup>@CInt(ViewBag.TotalCount).ToString("страница", "страницы", "страниц")</sup></h1>
    @Html.Pagination(New With {.class = "pagination"})
</header>

<article>
    @If Model.Any Then
        @Using Html.BeginForm(Nothing, Nothing, New With {returnUrl}, FormMethod.Post, New With {.id = "contentForm"})
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
                                @Html.DisplayNameFor(Function(model) model.Heading)
                            </th>
                            <th class="100"></th>
                        </tr>
                    </thead>
                    <tbody>
                        @For Each item In Model
                            @<tr>
                                <td>
                                    <input type="checkbox" name="id" value="@item.Id" />
                                </td>
                                <td>
                                    @Html.ActionLink(item.Heading, "edit", New With {item.Id, returnUrl}, New With {.title = "Изменить"})
                                </td>
                                <td class="text-right text-nowrap">
                                    <a class="action-link" href="@Url.Action("details", "pages", New With {item.Id, .area = ""})" title="Посмотреть" target="_blank"><span class="fa fa-eye"></span></a>
                                    <a class="action-link" href="@Url.Action("delete", New With {item.Id, returnUrl})" title="Удалить"><span class="fa fa-remove"></span></a>
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
