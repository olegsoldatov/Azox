Imports System.Threading.Tasks

Namespace Controllers
	Public Class AboutController
		Inherits Controller

		Private ReadOnly pageManager As New PathableEntityManager(Of Page)

		<HttpGet>
		Public Async Function Index() As Task(Of ActionResult)
			Dim model = Await pageManager.FindByAbsolutePathAsync(Request.Url.AbsolutePath)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function

		Protected Overrides Sub Dispose(disposing As Boolean)
			If disposing Then
				pageManager.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace