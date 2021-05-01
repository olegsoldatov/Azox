Namespace Areas.Admin.Controllers
	<Authorize>
	Public Class DashboardController
		Inherits Controller

		<HttpGet>
		Public Function Index() As ActionResult
			Return View()
		End Function

		<HttpGet>
		Public Function Files() As ActionResult
			Return View()
		End Function

		<AllowAnonymous>
		<HttpGet>
		Public Function Bar(editUrl As String) As ActionResult
			Return PartialView(New DashboardBarViewModel With {.EditUrl = editUrl})
		End Function

		<HttpGet>
		Public Function UploadCache() As ActionResult
			'CacheConfig.Clear()
			TempData("Message") = "Кэш обновлен."
			Return Redirect(Request.UrlReferrer.PathAndQuery)
		End Function
	End Class
End Namespace