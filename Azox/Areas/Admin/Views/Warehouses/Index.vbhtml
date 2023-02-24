@ModelType IEnumerable(Of Warehouse)
@Code
    ViewBag.Title = "Магазины / Склады"
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
    <h1 class="heading">@ViewBag.Title <sup>@CInt(ViewBag.TotalCount).ToString("магазин / склад", "магазина / склада", "магазинов / складов")</sup></h1>
    @Html.Partial("_Filter", ViewBag.Filter)
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
                                @Html.DisplayNameFor(Function(model) model.Title)
                            </th>
                            <th>
                                @Html.DisplayNameFor(Function(model) model.Company)
                            </th>
                            <th>
                                @Html.DisplayNameFor(Function(model) model.MarginGroup)
                            </th>
                            <th class="text-right" width="100">
                                @Html.DisplayNameFor(Function(model) model.Margin), %
                            </th>
                            @*<th class="text-right" width="100">
                                    @Html.DisplayNameFor(Function(model) model.Offers)
                                </th>*@
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
                                    @Html.ActionLink(item.Title, "edit", New With {.id = item.Id, .returnUrl = Request.Url.PathAndQuery}, New With {.title = "Изменить"})
                                    <div>
                                        <small class="text-muted">@Html.DisplayFor(Function(model) item.Name)</small>
                                    </div>
                                    @Html.DisplayFor(Function(model) item.IsPublished)
                                </td>
                                <td>
                                    @Html.DisplayFor(Function(model) item.Company)
                                </td>
                                <td>
                                    @If item.MarginGroup IsNot Nothing Then
                                        @Html.ActionLink(item.MarginGroup.Title, "index", "margingroups", New With {.searchText = item.MarginGroup.Title}, Nothing)
                                    End If
                                </td>
                                <td Class="text-right">
                                    @Html.DisplayFor(Function(model) item.Margin)
                                </td>
                                @*<td class="text-right">
                                        @Html.DisplayFor(Function(model) item.Offers.Count)
                                    </td>*@
                                <td class="text-right">
                                    <a href="@Url.Action("delete", New With {.id = item.Id, .returnUrl = Request.Url.PathAndQuery})" title="Удалить"><span class="fa fa-remove"></span></a>
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
