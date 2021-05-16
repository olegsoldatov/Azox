Imports System.Threading.Tasks

Namespace Controllers
	Public Class AboutController
		Inherits ManagerController(Of PathableEntityManager(Of Page), Page)

		Public Sub New()
			MyBase.New(New PathableEntityManager(Of Page))
		End Sub

		<HttpGet>
		Public Async Function Index() As Task(Of ActionResult)
			Dim model = Await Manager.FindByAbsolutePathAsync(Request.Url.AbsolutePath)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function
	End Class
End Namespace