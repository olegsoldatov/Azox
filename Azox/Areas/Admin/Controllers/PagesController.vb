Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks
Imports Azox.Mvc

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

		<HttpGet>
		Public Function Exists(id As Guid?, absolutePath As String) As ActionResult
			If PageManager.Pages.AsNoTracking().Any(Function(x) Not x.Id = id And x.AbsolutePath = absolutePath) Then
				Return Json(False, JsonRequestBehavior.AllowGet)
			End If
			Return Json(True, JsonRequestBehavior.AllowGet)
		End Function
	End Class
End Namespace