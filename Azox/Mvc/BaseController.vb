Public MustInherit Class BaseController
    Inherits Controller

    ''' <summary>
    ''' Передает в представление данные для элемента пагинации.
    ''' </summary>
    ''' <param name="count">Количество элементов в списке.</param>
    ''' <param name="pageIndex">Текущий индекс страницы.</param>
    ''' <param name="pageSize">Количество элементов списка на странице.</param>
    Protected Friend Sub Pagination(count As Integer, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 50)
        ViewBag.Count = count
        ViewBag.PageIndex = pageIndex
        ViewBag.PageSize = pageSize
        ViewBag.PageCount = CInt(Math.Ceiling(count / pageSize))
    End Sub


    ''' <summary>
    ''' Передает в представление содержание для краткого всплывающего сообщения.
    ''' </summary>
    ''' <param name="message">Текст сообщения.</param>
    Protected Friend Sub Toast(message As String)
        TempData("Message") = message
    End Sub
End Class
