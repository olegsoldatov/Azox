Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks
Imports Azox.Mvc
Imports Soldata.Web.Extensions

Namespace Areas.Admin.Controllers
	<Authorize>
	Public Class PagesController
		Inherits BaseController

		Public ReadOnly Property PageManager As PageManager

		Public Sub New(pageManager As PageManager)
			Me.PageManager = pageManager
		End Sub

		Public Async Function Index() As Task(Of ActionResult)
			Dim pages = Await PageManager.GetListAsync
			Return View(pages)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Index(id As Guid(), Optional delete As Boolean = False) As Task(Of ActionResult)
			If Not IsNothing(id) Then
				Dim entities = Await PageManager.Pages.Where(Function(x) id.Contains(x.Id)).ToListAsync

				If delete Then
					Await PageManager.DeleteRangeAsync(entities)
					Alert(String.Format("Удалено: {0}.", id.Length.ToString("страница", "страницы", "страниц")))
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
				Await PageManager.CreateAsync(model)
				Alert("Бренд добавлен.")
				Return RedirectToAction("index")
			End If
			Return View(model)
		End Function

		Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await PageManager.FindByIdAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Edit(model As Page, returnUrl As String) As Task(Of ActionResult)
			If ModelState.IsValid Then
				Await PageManager.UpdateAsync(model)
				Alert("Страница изменена.")
				Return RedirectToReturnUrl(returnUrl)
			End If
			Return View(model)
		End Function

		Public Async Function Delete(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await PageManager.FindByIdAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function

		<HttpPost>
		<ActionName("Delete")>
		<ValidateAntiForgeryToken>
		Public Async Function DeleteConfirmed(id As Guid, returnUrl As String) As Task(Of ActionResult)
			Dim entity = Await PageManager.FindByIdAsync(id)
			Await PageManager.DeleteAsync(entity)
			Alert("Страница удалена.")
			Return RedirectToReturnUrl(returnUrl)
		End Function

		<HttpGet>
		Public Function Exists(id As Guid?, absolutePath As String) As ActionResult
			If PageManager.Pages.AsNoTracking().Any(Function(x) Not x.Id = id And x.AbsolutePath = absolutePath) Then
				Return Json(False, JsonRequestBehavior.AllowGet)
			End If
			Return Json(True, JsonRequestBehavior.AllowGet)
		End Function
	End Class
End Namespace