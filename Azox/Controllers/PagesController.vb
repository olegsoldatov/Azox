Imports System.Threading.Tasks
Imports System.Net

Namespace Controllers
	Public Class PagesController
		Inherits Controller

		Private ReadOnly pageManager As New PageManager(New ApplicationDbContext)

		Public Async Function Details(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await pageManager.FindByIdAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			ElseIf Not String.IsNullOrEmpty(model.AbsolutePath) Then
				Return Redirect(model.AbsolutePath)
			End If
			Return View("Page", model)
		End Function

		Protected Overrides Sub Dispose(disposing As Boolean)
			If disposing Then
				pageManager.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace