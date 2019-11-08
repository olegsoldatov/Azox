Imports System.Web.Configuration

Namespace Controllers
	Public Class ErrorController
		Inherits Controller

		Public Function NotFound() As ActionResult
			If IsNothing(Request.UrlReferrer) Then
				ViewBag.Message = "Похоже, что адрес содержит опечатку или закладка, сохраненная в вашем браузере, уже устарела."
			ElseIf Request.UrlReferrer.Host = Request.Url.Host Then
				ViewBag.Message = "Возможно на нашем сайте есть битая ссылка."
			Else
				ViewBag.Message = "Возможно на сайте, с которого вы перешли, есть битая ссылка."
			End If

			Response.StatusCode = 404
			Return View()
		End Function

		Public Function MaxRequest() As ActionResult
			Return View(New HandleErrorInfo(New HttpException(500, String.Format("Размер загружаемого файла не должен быть больше {0} МБ.", CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength / 1024)), RouteData.Values("controller"), RouteData.Values("action")))
		End Function
	End Class
End Namespace
