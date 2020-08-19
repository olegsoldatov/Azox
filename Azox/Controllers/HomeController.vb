Namespace Controllers
	Public Class HomeController
		Inherits Controller

		<HttpGet>
		Public Function Index() As ActionResult
			Return View()
		End Function
	End Class
End Namespace
