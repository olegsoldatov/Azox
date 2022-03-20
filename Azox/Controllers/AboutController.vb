Imports System.Threading.Tasks

Namespace Controllers
	Public Class AboutController
		Inherits Controller

		Public ReadOnly Property PageManager As PageManager

		Public Sub New(pageManager As PageManager)
			Me.PageManager = pageManager
		End Sub

		<HttpGet>
		Public Async Function Index() As Task(Of ActionResult)
			Dim model = Await PageManager.FindByAbsolutePathAsync(Request.Url.AbsolutePath)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function
	End Class
End Namespace