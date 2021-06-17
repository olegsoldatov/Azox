Namespace Mvc
    Public MustInherit Class BaseController
        Inherits Controller

        ''' <summary>
        ''' Передает в представление данные для элемента пагинации.
        ''' </summary>
        ''' <param name="totalCount">Количество элементов в списке.</param>
        ''' <param name="pageIndex">Индекс страницы.</param>
        ''' <param name="pageSize">Размер списка на странице.</param>
        Protected Friend Sub Pagination(totalCount As Integer, pageIndex As Integer, pageSize As Integer)
            ViewBag.TotalCount = totalCount
            ViewBag.PageIndex = pageIndex
            ViewBag.PageSize = pageSize
        End Sub


        ''' <summary>
        ''' Передает в представление содержание для краткого всплывающего сообщения.
        ''' </summary>
        ''' <param name="message">Текст сообщения.</param>
        Protected Friend Sub Toast(message As String)
            TempData("Message") = message
        End Sub
    End Class
End Namespace
