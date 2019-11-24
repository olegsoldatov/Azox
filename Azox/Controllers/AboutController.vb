Imports System.Data.Entity
Imports System.Threading.Tasks

Namespace Controllers
	Public Class AboutController
		Inherits Controller

		Private ReadOnly Db As New ApplicationDbContext

		Public Async Function Index() As Task(Of ActionResult)
			Return View(Await Db.Pages.SingleAsync(Function(x) x.ActionName = "index" And x.ControllerName = "about"))
		End Function

		Protected Overrides Sub Dispose(disposing As Boolean)
			If disposing Then
				Db.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace