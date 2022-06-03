Namespace Controllers
    Public Class ErrorController
        Inherits Controller

        Public Function NotFound() As ActionResult
            Dim message = "Видимо на сайте, с которого вы сейчас перешли, оказалась битая ссылка."
            If IsNothing(Request.UrlReferrer) Then
                message = "Похоже, что адрес содержит опечатку или закладка, сохраненная в вашем браузере, уже устарела."
            ElseIf Request.UrlReferrer.Host = Request.Url.Host Then
                message = "Возможно на нашем сайте есть битая ссылка. Сообщение об этом уже отправлено нашему администратору. Ошибка будет устранена в ближайшее время."
            End If
            ViewBag.Message = message
            Response.StatusCode = 404
            Return View()
        End Function
    End Class
End Namespace