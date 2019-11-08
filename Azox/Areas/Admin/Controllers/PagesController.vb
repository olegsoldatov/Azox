Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks

Namespace Areas.Admin.Controllers
	<Authorize>
	Public Class PagesController
		Inherits Controller

		Private ReadOnly db As New ApplicationDbContext

		Public Async Function Index(filter As PageFilterViewModel, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 50) As Task(Of ActionResult)
			Dim entities = db.Pages.AsQueryable

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchText) Then
				Dim s = filter.SearchText.Trim.ToLower.Replace("ё", "е")
				entities = entities.Where(Function(x) x.Name.ToLower.Replace("ё", "е").Contains(s))
			End If

			' Сортировка.
			entities = entities.OrderBy(Function(x) x.Name)

			' Фильтр.
			ViewBag.Filter = filter

			' Количество и пагинация.
			ViewBag.TotalCount = Await entities.CountAsync
			ViewBag.PageIndex = pageIndex
			ViewBag.PageCount = CInt(Math.Ceiling(ViewBag.TotalCount / pageSize))

			Return View(Await entities.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync)
		End Function

		Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await db.Pages.FindAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Edit(model As Page, returnUrl As String) As Task(Of ActionResult)
			If ModelState.IsValid Then
				db.Entry(model).State = EntityState.Modified
				Await db.SaveChangesAsync
				TempData("Message") = "Страница изменена."
				Return RedirectToLocal(returnUrl)
			End If
			Return View(model)
		End Function

		Private Function RedirectToLocal(returnUrl As String) As ActionResult
			If Url.IsLocalUrl(returnUrl) Then
				Return Redirect(returnUrl)
			End If
			Return RedirectToAction("index")
		End Function

		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				db.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace