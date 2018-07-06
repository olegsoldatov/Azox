'Imports System.Data.Entity
'Imports System.Net
'Imports System.Threading.Tasks

'Namespace Areas.Admin.Controllers
'	<Authorize>
'	Public Class NodesController
'		Inherits EntityBaseController

'		Private Manager As New NodeManager

'		Protected Overrides Sub Dispose(disposing As Boolean)
'			If disposing Then
'				If Manager IsNot Nothing Then
'					Manager.Dispose()
'					Manager = Nothing
'				End If
'			End If
'			MyBase.Dispose(disposing)
'		End Sub

'		Public Async Function Index(searchString As String, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 20) As Task(Of ActionResult)
'			Dim items = Manager.Entities.AsNoTracking

'			'Фильтр поиска.
'			If Not String.IsNullOrEmpty(searchString) Then
'				items = items.Where(Function(m) m.Title.Contains(searchString) Or m.Url.Contains(searchString) Or m.Heading.Contains(searchString) Or m.Description.Contains(searchString) Or m.Keywords.Contains(searchString))
'			End If

'			'Пагинация.
'			ViewBag.PageIndex = pageIndex
'			ViewBag.PageCount = CInt(Math.Ceiling(Await items.CountAsync / pageSize))

'			Return View(Await items.OrderBy(Function(m) m.Order).ThenBy(Function(m) m.Title).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync)
'		End Function

'		Public Async Function Create() As Task(Of ActionResult)
'			ViewBag.Parents = New SelectList(NodeManager.GetViewList(Await Manager.Entities.ToListAsync), "Id", "Title")
'			Return View(New Node With {.ParentId = Manager.GetRooNodeAsync.Result.Id})
'		End Function

'		<HttpPost>
'		<ValidateAntiForgeryToken>
'		Public Async Function Create(model As Node) As Task(Of ActionResult)
'			If ModelState.IsValid Then
'				Dim result = Await Manager.CreateAsync(model)
'				If result.Succeeded Then
'					TempData("Message") = "Узел добавлен."
'					Return RedirectToAction("index")
'				End If
'				AddErrors(result)
'			End If
'			ViewBag.Parents = New SelectList(NodeManager.GetViewList(Await Manager.Entities.ToListAsync), "Id", "Title")
'			Return View(model)
'		End Function

'		Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
'			If IsNothing(id) Then
'				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
'			End If
'			Dim model = Await Manager.FindByIdAsync(id)
'			If IsNothing(model) Then
'				Return HttpNotFound()
'			End If
'			ViewBag.Parents = New SelectList(NodeManager.GetViewList(Manager.GetUpLevelNodes(model)), "Id", "Title")
'			Return View(model)
'		End Function

'		<HttpPost>
'		<ValidateAntiForgeryToken>
'		Public Async Function Edit(model As Node) As Task(Of ActionResult)
'			If ModelState.IsValid Then
'				Dim result = Await Manager.UpdateAsync(model)
'				If result.Succeeded Then
'					TempData("Message") = "Узел изменен."
'					Return RedirectToAction("index")
'				End If
'				AddErrors(result)
'			End If
'			ViewBag.Parents = New SelectList(NodeManager.GetViewList(Manager.GetUpLevelNodes(model)), "Id", "Title")
'			Return View(model)
'		End Function

'		Public Async Function Delete(id As Guid?) As Task(Of ActionResult)
'			If IsNothing(id) Then
'				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
'			End If
'			Dim model = Await Manager.FindByIdAsync(id)
'			If IsNothing(model) Then
'				Return HttpNotFound()
'			End If
'			Return View(model)
'		End Function

'		<HttpPost>
'		<ActionName("Delete")>
'		<ValidateAntiForgeryToken>
'		Public Async Function DeleteConfirmed(id As Guid) As Task(Of ActionResult)
'			Dim model = Await Manager.FindByIdAsync(id)
'			Dim result = Await Manager.DeleteAsync(model)
'			If result.Succeeded Then
'				TempData("Message") = "Узел удален."
'				Return RedirectToAction("index")
'			End If
'			AddErrors(result)
'			Return View(model)
'		End Function
'	End Class
'End Namespace