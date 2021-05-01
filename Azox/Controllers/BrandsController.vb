Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks

Namespace Controllers
	Public Class BrandsController
		Inherits Controller

		Private ReadOnly db As New ApplicationDbContext

		Public Async Function Index() As Task(Of ActionResult)
			Return View(Await db.Brands.Where(Function(x) Not x.Draft).ToListAsync)
		End Function

		Public Async Function Details(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await db.Brands.FindAsync(id)
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
