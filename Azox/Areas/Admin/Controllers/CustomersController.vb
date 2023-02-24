Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks
Imports Soldata.Web.Extensions

Namespace Areas.Admin.Controllers
	Public Class CustomersController
		Inherits AdminController

		Private ReadOnly CustomerManager As CustomerManager

		Public Sub New(customerManager As CustomerManager)
			Me.CustomerManager = customerManager
		End Sub

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
                Dim customers = Await CustomerManager.Customers.Where(Function(x) id.Contains(x.Id)).ToListAsync
                If delete Then
                    Await CustomerManager.DeleteRangeAsync(customers)
                    Alert(String.Format("Удалено: {0}.", customers.Count.ToString("клиент", "клиента", "клиентов")))
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
        Public Async Function Create(customer As Customer) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Await CustomerManager.CreateAsync(customer)
                Alert("Клиент добавлен.")
                Return RedirectToAction("index")
            End If
            Return View(customer)
        End Function

        Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
            Dim customer = Await CustomerManager.FindByIdAsync(id)
            If IsNothing(customer) Then
                Return HttpNotFound()
            End If
            Return View(customer)
        End Function

		<HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Edit(customer As Customer, returnUrl As String) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Await CustomerManager.UpdateAsync(customer)
                Alert("Клиент изменен.")
                Return RedirectToReturnUrl(returnUrl)
            End If
            Return View(customer)
        End Function

        Public Async Function Delete(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
            Dim customer = Await CustomerManager.FindByIdAsync(id)
            If IsNothing(customer) Then
                Return HttpNotFound()
            End If
            Return View(customer)
        End Function

		<HttpPost>
		<ActionName("Delete")>
        <ValidateAntiForgeryToken>
        Public Async Function DeleteConfirmed(id As Guid, returnUrl As String) As Task(Of ActionResult)
            Dim customer = Await CustomerManager.FindByIdAsync(id)
            Await CustomerManager.DeleteAsync(customer)
            Alert("Клиент удален.")
            Return RedirectToReturnUrl(returnUrl)
        End Function
    End Class
End Namespace