@ModelType IEnumerable(Of Azox.Page)
@Code
    ViewBag.Title = "Управление страницами"
    Dim returnUrl = Request.Url.AbsolutePath
End Code

<header>
	@Html.Title("страница", "страницы", "страниц", New With {.class = "heading"})
</header>

<article>
    <div class="table-responsive">
        <table class="table table-hover">
            <thead>
                <tr>
                    <th>
                        @Html.DisplayNameFor(Function(model) model.Title)
                    </th>
                    <th width="100"></th>
                </tr>
            </thead>
            <tbody>
                @For Each item In Model
                    @<tr>
                        <td>
                            @Html.ActionLink(item.Title, "edit", New With {item.Id, returnUrl}, New With {.title = item.Title})
                        </td>
                        <td class="text-right">
                            <a href="@item.AbsolutePath" title="Посмотреть" target="_blank"><span class="fa fa-external-link"></span></a>
                        </td>
                    </tr>
                Next
            </tbody>
        </table>
    </div>
</article>
