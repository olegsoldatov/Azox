Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks
Imports Soldata.Web.Extensions

Namespace Areas.Admin.Controllers
	<Authorize>
	Public Class WarehousesController
		Inherits BaseController

		Private ReadOnly db As New ApplicationDbContext

		Public Async Function Index(filter As WarehouseFilterViewModel, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 50) As Task(Of ActionResult)
			Dim entities = db.Warehouses _
				.Include(Function(x) x.MarginGroup) _
				.AsNoTracking

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchText) Then
				Dim s = filter.SearchText.ToLower.Replace("ё", "е")
				entities = entities.Where(Function(x) x.Name.ToLower.Replace("ё", "е").Contains(s) Or x.Title.ToLower.Replace("ё", "е").Contains(s))
			End If

			' Компания.
			If Not String.IsNullOrEmpty(filter.Company) Then
				Dim c = filter.Company.ToLower.Replace("ё", "е")
				entities = entities.Where(Function(x) x.Company.ToLower.Replace("ё", "е") = c)
			End If

			' Группа наценок.
			If Not IsNothing(filter.MarginGroupId) AndAlso Not filter.MarginGroupId.Equals(Guid.Empty) Then
				entities = entities.Where(Function(x) x.MarginGroupId = filter.MarginGroupId)
			End If

			' Сортировка.
			entities = entities.OrderBy(Function(x) x.Title) _
				.ThenBy(Function(x) x.Name) _
				.ThenBy(Function(x) x.Order)

			' Фильтр.
			ViewBag.Company = New SelectList(db.Warehouses.AsNoTracking.Select(Function(x) x.Company).GroupBy(Function(x) x).Select(Function(x) x.Key).OrderBy(Function(x) x))
			ViewBag.MarginGroupId = New SelectList(db.MarginGroups.OrderBy(Function(x) x.Title).AsNoTracking, "Id", "Title", filter.MarginGroupId)
			ViewBag.Filter = filter

			Pagination(Await entities.CountAsync, pageIndex, pageSize)

			Dim model = Await entities _
				.Skip(pageIndex * pageSize) _
				.Take(pageSize) _
				.ToListAsync

			Return View(model)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Index(id As Guid(), returnUrl As String, Optional delete As Boolean = False) As Task(Of ActionResult)
			If Not IsNothing(id) AndAlso id.Any Then
				Dim entities = db.Warehouses.Where(Function(x) id.Contains(x.Id))

				If delete Then
					Await entities.ForEachAsync(Sub(x As Warehouse)
													db.Entry(x).State = EntityState.Deleted
												End Sub)
					Await db.SaveChangesAsync
					TempData("Message") = String.Format("Удалено: {0}.", id.Length.ToString("склад", "склада", "складов"))
				End If
			End If

			Return Redirect(returnUrl)
		End Function

		<HttpGet>
		Public Function Create() As ActionResult
			ViewBag.Margin = 0
			ViewBag.Order = 0
			ViewBag.MarginGroupId = New SelectList(db.MarginGroups.OrderBy(Function(x) x.Title).AsNoTracking, "Id", "Title")
			Return View()
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Create(model As Warehouse, returnUrl As String) As Task(Of ActionResult)
			If ModelState.IsValid Then
				model.Id = Guid.NewGuid
				db.Warehouses.Add(model)
				Await db.SaveChangesAsync
				TempData("Message") = "Склад добавлен."
				Return RedirectToLocal(returnUrl)
			End If
			ViewBag.MarginGroupId = New SelectList(db.MarginGroups.OrderBy(Function(x) x.Title).AsNoTracking, "Id", "Title", model.MarginGroupId)
			Return View(model)
		End Function

		Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await db.Warehouses.FindAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			ViewBag.MarginGroupId = New SelectList(db.MarginGroups.OrderBy(Function(x) x.Title).AsNoTracking, "Id", "Title", model.MarginGroupId)
			Return View(model)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Edit(model As Warehouse, returnUrl As String) As Task(Of ActionResult)
			If ModelState.IsValid Then
				db.Entry(model).State = EntityState.Modified
				Await db.SaveChangesAsync
				TempData("Message") = "Склад изменен."
				Return RedirectToLocal(returnUrl)
			End If
			ViewBag.MarginGroupId = New SelectList(db.MarginGroups.OrderBy(Function(x) x.Title).AsNoTracking, "Id", "Title", model.MarginGroupId)
			Return View(model)
		End Function

		Public Async Function Delete(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await db.Warehouses.FindAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function

		<HttpPost>
		<ActionName("Delete")>
		<ValidateAntiForgeryToken>
		Public Async Function DeleteConfirmed(id As Guid, returnUrl As String) As Task(Of ActionResult)
			db.Warehouses.Remove(Await db.Warehouses.FindAsync(id))
			Await db.SaveChangesAsync
			TempData("Message") = "Склад удален."
			Return RedirectToLocal(returnUrl)
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