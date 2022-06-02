Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks
Imports Soldata.Web.Extensions

Namespace Areas.Admin.Controllers
	Public Class PagesController
		Inherits AdminController

		Private Const existsMessage = "Такой путь уже существует."

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
				If delete Then
					Await PageManager.DeleteRangeAsync(id)
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
		Public Async Function Create(page As Page) As Task(Of ActionResult)
			If Await PageManager.ExistsAsync(page) Then
				ModelState.AddModelError(NameOf(page.AbsolutePath), existsMessage)
			End If
			If ModelState.IsValid Then
				Await PageManager.CreateAsync(page)
				Alert("Страница добавлена.")
				Return RedirectToAction("index")
			End If
			Return View(page)
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
		Public Async Function Edit(page As Page, returnUrl As String) As Task(Of ActionResult)
			If Await PageManager.ExistsAsync(page) Then
				ModelState.AddModelError(NameOf(page.AbsolutePath), existsMessage)
			End If
			If ModelState.IsValid Then
				Await PageManager.UpdateAsync(page)
				Alert("Страница изменена.")
				Return RedirectToReturnUrl(returnUrl)
			End If
			Return View(page)
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
			Dim page = Await PageManager.FindByIdAsync(id)
			Await PageManager.DeleteAsync(page)
			Alert("Страница удалена.")
			Return RedirectToReturnUrl(returnUrl)
		End Function

		<HttpGet>
		Public Function Exists(id As Guid?, absolutePath As String) As ActionResult
			'Dim b = PageManager.ExistsAsync(New Page With {.Id = id, .AbsolutePath = absolutePath})

			If PageManager.Pages.AsNoTracking().Any(Function(x) Not x.Id = id And x.AbsolutePath = absolutePath) Then
				Return Json(False, JsonRequestBehavior.AllowGet)
			End If
			Return Json(True, JsonRequestBehavior.AllowGet)
		End Function
	End Class
End Namespace