Imports System.Data.Entity
Imports System.Threading.Tasks

Namespace Controllers
	Public Class AboutController
		Inherits Controller

		Private ReadOnly db As New ApplicationDbContext

		<HttpGet>
		Public Async Function Index() As Task(Of ActionResult)
			Dim model = Await db.Pages.FirstOrDefaultAsync(Function(x) x.Slug = Request.Url.AbsolutePath)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function

		Protected Overrides Sub Dispose(disposing As Boolean)
			If disposing Then
				db.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace