'Namespace Controllers
'	Public Class ServicesController
'		Inherits Controller

'		Private Manager As New ServiceManager

'		Protected Overrides Sub Dispose(disposing As Boolean)
'			If disposing Then
'				If Manager IsNot Nothing Then
'					Manager.Dispose()
'					Manager = Nothing
'				End If
'			End If
'			MyBase.Dispose(disposing)
'		End Sub

'		Public Async Function Details(id As Guid?) As Task(Of ActionResult)
'			If IsNothing(id) Then
'				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
'			End If
'			Dim model = Await Manager.FindByIdAsync(id)
'			If IsNothing(model) Then
'				Return HttpNotFound()
'			End If
'			Return RedirectPermanent(Url.Action(model.Slug))
'		End Function

'		Protected Overrides Sub HandleUnknownAction(actionName As String)
'			Dim model = Manager.FindBySlug(actionName)
'			If IsNothing(model) Then
'				HttpNotFound.ExecuteResult(ControllerContext)
'				Exit Sub
'			End If
'			ViewBag.EditUrl = Url.Action("edit", New With {.area = "admin", .id = model.Id})
'			View("Details", model).ExecuteResult(ControllerContext)
'		End Sub
'	End Class
'End Namespace