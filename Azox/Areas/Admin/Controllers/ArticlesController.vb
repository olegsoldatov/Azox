'Namespace Areas.Admin.Controllers
'	<Authorize>
'	Public Class ArticlesController
'		Inherits BaseController

'		Async Function Index(searchString As String, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 20) As Task(Of ActionResult)
'			Dim entities = Db.Articles.AsNoTracking

'			'Фильтр поиска.
'			If Not String.IsNullOrEmpty(searchString) Then
'				entities = entities.Where(Function(x) x.Title.Contains(searchString))
'			End If

'			'Пагинация.
'			ViewBag.PageIndex = pageIndex
'			ViewBag.PageCount = CInt(Math.Ceiling(Await entities.CountAsync / pageSize))

'			Return View(Await entities.OrderByDescending(Function(m) m.LastUpdateDate).ThenBy(Function(m) m.Title).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync)
'		End Function

'		Function Create() As ActionResult
'			Return View()
'		End Function

'		<HttpPost>
'		<ValidateAntiForgeryToken>
'		Async Function Create(model As Article) As Task(Of ActionResult)
'			If ModelState.IsValid Then
'				Dim result = Await Manager.CreateAsync(model)
'				If result.Succeeded Then
'					TempData("Message") = "Добавлено."
'					Return RedirectToAction("index")
'				End If
'				AddErrors(result)
'			End If
'			Return View(model)
'		End Function

'		Async Function Edit(id As Guid?) As Task(Of ActionResult)
'			If IsNothing(id) Then
'				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
'			End If
'			Dim model = Await Db.Articles.FindAsync(id)
'			If IsNothing(model) Then
'				Return HttpNotFound()
'			End If
'			Return View(model)
'		End Function

'		<HttpPost>
'		<ValidateAntiForgeryToken>
'		Public Async Function Edit(model As Article) As Task(Of ActionResult)
'			If ModelState.IsValid Then
'				Dim result = Await Manager.UpdateAsync(model)
'				If result.Succeeded Then
'					TempData("Message") = "Изменено."
'					Return RedirectToAction("index")
'				End If
'			End If
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
'				TempData("Message") = "Удалено."
'				Return RedirectToAction("index")
'			End If
'			Return View(model)
'		End Function
'	End Class
'End Namespace