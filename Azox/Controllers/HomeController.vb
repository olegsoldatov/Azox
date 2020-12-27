Imports System.Threading.Tasks

Namespace Controllers
	Public Class HomeController
		Inherits Controller

		Private ReadOnly manager As New CatalogManager

		<OutputCache(CacheProfile:="Index")>
		Public Async Function Index() As Task(Of ActionResult)
			Return View(Await manager.GetDocumentAsync)
		End Function

		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				manager.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace
