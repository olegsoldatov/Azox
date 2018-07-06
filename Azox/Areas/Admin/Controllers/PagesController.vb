Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks

Namespace Areas.Admin.Controllers
	<Authorize>
	Public Class PagesController
		Inherits DbController

		Public Async Function Index(searchString As String, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 20) As Task(Of ActionResult)
			Dim entities = Db.Pages.AsQueryable

			' Поиск.
			If Not String.IsNullOrEmpty(searchString) Then
				entities = entities.Where(Function(m) m.Title.Contains(searchString) Or m.Heading.Contains(searchString))
			End If

			' Сортировка.
			entities = entities.OrderBy(Function(x) x.Title)

			' Количество.
			ViewBag.Count = Await entities.CountAsync

			' Пагинация.
			ViewBag.PageIndex = pageIndex
			ViewBag.PageSize = pageSize
			ViewBag.PageCount = CInt(Math.Ceiling(Await entities.CountAsync / pageSize))

			Return View(Await entities.OrderBy(Function(m) m.Title).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync)
		End Function

		Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await Db.Pages.FindAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function

		<HttpPost, ValidateAntiForgeryToken>
		Public Async Function Edit(model As Page, returnUrl As String) As Task(Of ActionResult)
			If ModelState.IsValid Then
				Db.Entry(model).State = EntityState.Modified
				Await Db.SaveChangesAsync
				TempData("Message") = "Изменено."
				Return RedirectToLocal(returnUrl)
			End If
			Return View(model)
		End Function
	End Class
End Namespace