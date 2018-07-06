Imports System.Net
Imports System.Threading.Tasks

Namespace Controllers
	Public Class ProductsController
		Inherits DbController

		Public Function Index() As ActionResult
			Return View()
		End Function

		Public Async Function Details(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await Db.Products.FindAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function

		<ChildActionOnly>
		Public Function List(filter As ProductFilterViewModel, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 20) As ActionResult
			ViewBag.Filter = filter
			Return PartialView("_List", New List(Of Product))
		End Function
	End Class
End Namespace