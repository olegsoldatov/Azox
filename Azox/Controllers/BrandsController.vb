Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks

Namespace Controllers
	Public Class BrandsController
		Inherits Controller

		Public ReadOnly Property BrandManager As BrandManager

		Public Sub New(brandManager As BrandManager)
			Me.BrandManager = brandManager
		End Sub

		Public Async Function Index() As Task(Of ActionResult)
			Return View(Await BrandManager.Published.ToListAsync)
		End Function

		Public Async Function Details(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await BrandManager.FindByIdAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function
	End Class
End Namespace
