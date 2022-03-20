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
        Protected Friend Sub Alert(message As String)
            TempData("Message") = message
        End Sub

        ''' <summary>
        ''' Перенаправляет по ссылке возврата или по имени метода действия по умолчанию.
        ''' </summary>
        ''' <param name="returnUrl">Строка, содержащая ссылку возврата.</param>
        ''' <param name="defaultActionName">Имя метода действия, по которому будет перенаправлен запрос, если ссылка возврата будет пустой.</param>
        Protected Friend Function RedirectToReturnUrl(returnUrl As String, Optional defaultActionName As String = "index") As ActionResult
            If String.IsNullOrWhiteSpace(returnUrl) Then
                Return RedirectToAction(defaultActionName)
            End If
            Return Redirect(returnUrl)
        End Function
    End Class
End Namespace
