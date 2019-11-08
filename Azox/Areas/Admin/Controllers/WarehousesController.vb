Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks
Imports Soldata.Web.Extensions

Namespace Areas.Admin.Controllers
	<Authorize>
	Public Class WarehousesController
		Inherits Controller

		Private ReadOnly db As New ApplicationDbContext

		Public Async Function Index(filter As WarehouseFilterViewModel, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 50) As Task(Of ActionResult)
			Dim warehouses = db.Warehouses.AsQueryable

			' Количество и пагинация.
			ViewBag.TotalCount = Await ApplyFilterAndSort(warehouses, filter).Select(Function(x) x.Id).CountAsync
			ViewBag.PageIndex = pageIndex
			ViewBag.PageCount = CInt(Math.Ceiling(ViewBag.TotalCount / pageSize))

			' Модель.
			Dim model = Await ApplyFilterAndSort(warehouses, filter).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync

			' Фильтр.
			ViewBag.Filter = filter

			Return View(model)
		End Function

		Public Function Create() As ActionResult
			Return View()
		End Function

		<HttpPost, ValidateAntiForgeryToken>
		Public Async Function Create(model As Warehouse, returnUrl As String) As Task(Of ActionResult)
			If ModelState.IsValid Then
				model.Id = Guid.NewGuid()
				db.Warehouses.Add(model)
				Await db.SaveChangesAsync()
				TempData("Message") = "Склад добавлен."
				Return RedirectToLocal(returnUrl)
			End If
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
			Return View(model)
		End Function

		<HttpPost, ValidateAntiForgeryToken>
		Public Async Function Edit(model As Warehouse, returnUrl As String) As Task(Of ActionResult)
			If ModelState.IsValid Then
				db.Entry(model).State = EntityState.Modified
				Await db.SaveChangesAsync()
				TempData("Message") = "Склад изменен."
				Return RedirectToLocal(returnUrl)
			End If
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

		<HttpPost, ActionName("Delete"), ValidateAntiForgeryToken>
		Public Async Function DeleteConfirmed(id As Guid, returnUrl As String) As Task(Of ActionResult)
			Await db.Products.Where(Function(x) id = x.WarehouseId).ForEachAsync(Sub(p As Product) p.WarehouseId = Nothing)
			db.Warehouses.Remove(Await db.Warehouses.FindAsync(id))
			Await db.SaveChangesAsync()
			TempData("Message") = "Склад удален."
			Return RedirectToLocal(returnUrl)
		End Function

		<HttpPost>
		Public Async Function DeleteChecked(id As Guid()) As Task(Of ActionResult)
			Await db.Products.Where(Function(x) id.Contains(x.WarehouseId)).ForEachAsync(Sub(p As Product) p.WarehouseId = Nothing)
			Await db.Warehouses.Where(Function(x) id.Contains(x.Id)).ForEachAsync(Sub(x As Warehouse) db.Entry(x).State = EntityState.Deleted)
			Await db.SaveChangesAsync
			TempData("Message") = String.Format("Удалено: {0}", id.Length.ToString("склад", "склада", "складов"))
			Return Json(New With {.redirect = Url.Action("index")})
		End Function

		''' <summary>
		''' Применяет фильтр и сортировку к перечислению складов.
		''' </summary>
		''' <param name="warehouses">Перечисление складов, к которому применяется фильтр.</param>
		''' <param name="filter">Фильтр.</param>
		''' <returns>Отсортированное перечисление складов после применения фильтра.</returns>
		Private Function ApplyFilterAndSort(warehouses As IQueryable(Of Warehouse), filter As WarehouseFilterViewModel) As IOrderedQueryable(Of Warehouse)

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchString) Then
				Dim searchString = filter.SearchString.Trim.ToLower.Replace("ё", "е")
				warehouses = warehouses.Where(Function(x) x.Name.ToLower.Replace("ё", "е").Contains(searchString) Or x.Slug.ToLower.Contains(searchString))
			End If

			' Сортировка (по умолчанию по названию).
			warehouses = warehouses.OrderBy(Function(x) x.Name)

			Return warehouses
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