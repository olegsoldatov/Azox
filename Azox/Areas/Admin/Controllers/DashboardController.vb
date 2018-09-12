Namespace Areas.Admin.Controllers
	<Authorize>
	Public Class DashboardController
		Inherits Controller

		Public Function Index() As ActionResult
			Return View()
		End Function

		<AllowAnonymous>
		Public Function Bar(editUrl As String) As ActionResult
			Return PartialView(New DashboardBarViewModel With {.EditUrl = editUrl})
		End Function
	End Class
End Namespace