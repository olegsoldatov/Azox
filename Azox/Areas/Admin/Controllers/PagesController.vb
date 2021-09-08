Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks
Imports Soldata.Web.Extensions
Imports Azox.Mvc

Namespace Areas.Admin.Controllers
	<Authorize>
	Public Class PagesController
		Inherits BaseController

		Private ReadOnly pageManager As New PageManager(New ApplicationDbContext)

		Public Async Function Index(filter As FilterViewModel, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 50) As Task(Of ActionResult)
			Dim entities = pageManager.Pages.AsNoTracking

			' Фильтр.
			ViewBag.Filter = filter

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchText) Then
				Dim s = filter.SearchText.ToLower.Replace("ё", "е")
				entities = entities.Where(Function(x) x.Title.ToLower.Replace("ё", "е").Contains(s))
			End If

			' Сортировка (по умолчанию по дате изменения).
			entities = entities.OrderByDescending(Function(x) x.LastUpdateDate)

			Pagination(Await entities.CountAsync, pageIndex, pageSize)

			Return View(Await entities.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Index(id As Guid(), Optional delete As Boolean = False) As Task(Of ActionResult)
			If Not IsNothing(id) Then
				Dim entities = Await pageManager.Pages.Where(Function(x) id.Contains(x.Id)).ToListAsync

				If delete Then
					Await pageManager.DeleteRangeAsync(entities)
					Toast(String.Format("Удалено: {0}.", id.Length.ToString("страница", "страницы", "страниц")))
				End If
			End If

			Return Redirect(Request.UrlReferrer.PathAndQuery)
		End Function

		<HttpGet>
		Public Function Create() As ActionResult
			Return View()
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Create(model As Page) As Task(Of ActionResult)
			If ModelState.IsValid Then
				model.Id = Guid.NewGuid
				model.LastUpdateDate = Now
				Await pageManager.CreateAsync(model)
				Toast("Страница добавлена.")
				Return RedirectToAction("index")
			End If
			Return View(model)
		End Function

		Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await pageManager.FindByIdAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Edit(model As Page, returnUrl As String) As Task(Of ActionResult)
			If ModelState.IsValid Then
				model.LastUpdateDate = Now
				Await pageManager.UpdateAsync(model)
				Toast("Страница изменена.")
				If String.IsNullOrEmpty(returnUrl) Then
					Return RedirectToAction("index")
				Else
					Return Redirect(returnUrl)
				End If
			End If
			Return View(model)
		End Function

		Public Async Function Delete(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await pageManager.FindByIdAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function

		<HttpPost>
		<ActionName("Delete")>
		<ValidateAntiForgeryToken>
		Public Async Function DeleteConfirmed(id As Guid) As Task(Of ActionResult)
			Await pageManager.DeleteAsync(Await pageManager.FindByIdAsync(id))
			Toast("Страница удалена.")
			Return RedirectToAction("index")
		End Function

		<HttpGet>
		Public Function Exists(id As Guid?, absolutePath As String) As ActionResult
			If pageManager.Pages.AsNoTracking().Any(Function(x) Not x.Id = id And x.AbsolutePath = absolutePath) Then
				Return Json(False, JsonRequestBehavior.AllowGet)
			End If
			Return Json(True, JsonRequestBehavior.AllowGet)
		End Function

		Protected Overrides Sub Dispose(disposing As Boolean)
			If disposing Then
				pageManager.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace