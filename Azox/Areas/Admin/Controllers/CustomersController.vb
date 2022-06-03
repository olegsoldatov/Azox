Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks
Imports Soldata.Web.Extensions

Namespace Areas.Admin.Controllers
	Public Class CustomersController
		Inherits AdminController

		Private ReadOnly Property CustomerManager As New CustomerManager(New ApplicationDbContext)

		Public Async Function Index(filter As FilterViewModel, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 50) As Task(Of ActionResult)
			Dim entities = CustomerManager.Customers.AsNoTracking

			' Фильтр.
			ViewBag.Filter = filter

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchText) Then
				Dim s = filter.SearchText.ToLower.Replace("ё", "е")
				entities = entities.Where(Function(x) x.Name.ToLower.Replace("ё", "е").Contains(s) Or x.Phone.Contains(s) Or x.Email.ToLower.Contains(s))
			End If

			' Сортировка.
			entities = entities.OrderByDescending(Function(x) x.LastUpdateDate).ThenBy(Function(x) x.Name)

			Pagination(Await entities.CountAsync, pageIndex, pageSize)

			Return View(Await entities.Skip(pageIndex * pageSize).Take(pageSize).AsNoTracking.ToListAsync)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Index(id As Guid(), Optional delete As Boolean = False) As Task(Of ActionResult)
			If Not IsNothing(id) Then
				Dim entities = CustomerManager.Customers.Where(Function(x) id.Contains(x.Id))

				If delete Then
					Await CustomerManager.DeleteRangeAsync(Await entities.ToListAsync)
					Alert(String.Format("Удалено: {0}.", id.Length.ToString("клиент", "клиента", "клиентов")))
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
		Public Async Function Create(model As Customer) As Task(Of ActionResult)
			If ModelState.IsValid Then
				Await CustomerManager.CreateAsync(model)
				Alert("Клиент добавлен.")
				Return RedirectToAction("index")
			End If
			Return View(model)
		End Function

		Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await CustomerManager.FindByIdAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Edit(model As Customer, returnUrl As String) As Task(Of ActionResult)
			If ModelState.IsValid Then
				Await CustomerManager.UpdateAsync(model)
				Alert("Клиент изменен.")
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
			Dim model = Await CustomerManager.FindByIdAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function

		<HttpPost>
		<ActionName("Delete")>
		<ValidateAntiForgeryToken>
		Public Async Function DeleteConfirmed(id As Guid) As Task(Of ActionResult)
			Dim entity = Await CustomerManager.FindByIdAsync(id)
			Await CustomerManager.DeleteAsync(entity)
			Alert("Клиент удален.")
			Return RedirectToAction("index")
		End Function

		Protected Overrides Sub Dispose(disposing As Boolean)
			If disposing And CustomerManager IsNot Nothing Then
				_CustomerManager.Dispose()
			End If
			_CustomerManager = Nothing
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace