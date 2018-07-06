Namespace Controllers
    Public Class ErrorController
        Inherits Controller

        Function NotFound() As ActionResult
            If IsNothing(Request.UrlReferrer) Then
                ViewBag.Message = "Похоже, что адрес содержит опечатку или закладка, сохраненная в вашем браузере, уже устарела."
            ElseIf Request.UrlReferrer.Host = Request.Url.Host Then
                ViewBag.Message = "Видимо у нас на сайте оказалась битая ссылка. Сообщение об этом уже отправлено нашему администратору. Ошибка будет устранена в ближайшее время."
            Else
                ViewBag.Message = "Видимо на сайте, с которого вы сейчас перешли, оказалась битая ссылка. Мы попытаемся связаться с владельцем того сайта, чтобы он исправил ссылку."
            End If

            Response.StatusCode = 404
            Return View()
        End Function
    End Class
End Namespace
