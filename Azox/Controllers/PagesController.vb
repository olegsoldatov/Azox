Imports System.Threading.Tasks
Imports System.Net

Namespace Controllers
	Public Class PagesController
		Inherits ManagerController(Of EntityManager(Of Page), Page)

		Public Sub New()
			MyBase.New(New EntityManager(Of Page))
		End Sub

		Public Async Function Details(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await Manager.FindByIdAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			ElseIf Not String.IsNullOrEmpty(model.AbsolutePath) Then
				Return Redirect(model.AbsolutePath)
			End If
			Return View("Page", model)
		End Function
	End Class
End Namespace