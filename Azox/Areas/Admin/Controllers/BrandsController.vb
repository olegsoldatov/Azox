Imports System.Data.Entity
Imports System.Threading.Tasks
Imports System.Net
Imports Soldata.Imaging
Imports Soldata.Web.Extensions

Namespace Areas.Admin.Controllers
	<Authorize>
	Public Class BrandsController
		Inherits Controller

		Private ReadOnly manager As New CatalogManager

		Public Async Function Index(filter As FilterViewModel, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 50) As Task(Of ActionResult)
			ViewBag.Filter = filter

			Dim entities = manager.Brands

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchText) Then
				Dim s = filter.SearchText.ToLower.Replace("ё", "е")
				entities = entities.Where(Function(x) x.Title.ToLower.Replace("ё", "е").Contains(s) Or x.Name.ToLower.Replace("ё", "е").Contains(s))
			End If

			' Количество и пагинация.
			Dim count = Await entities.AsNoTracking.CountAsync
			ViewBag.Count = count
			ViewBag.PageIndex = pageIndex
			ViewBag.PageSize = pageSize
			ViewBag.PageCount = CInt(Math.Ceiling(count / pageSize))

			' Сортировка.
			entities = entities.OrderByDescending(Function(x) x.LastUpdateDate).ThenBy(Function(x) x.Title)

			Return View(Await entities.Skip(pageIndex * pageSize).Take(pageSize).AsNoTracking.ToListAsync)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Index(id As Guid(), Optional delete As Boolean = False) As Task(Of ActionResult)
			If Not IsNothing(id) Then
				Dim entities = manager.Brands.Where(Function(x) id.Contains(x.Id))

				If delete Then
					Await manager.DeleteBrandRangeAsync(Await entities.ToListAsync)
					TempData("Message") = String.Format("Удалено: {0}", id.Length.ToString("бренд", "бренда", "брендов"))
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
		Public Async Function Create(brand As Brand, imageFile As HttpPostedFileWrapper) As Task(Of ActionResult)
			If Await manager.ExistsBrandAsync(brand) Then
				ModelState.AddModelError("Name", "Такой бренд уже существует.")
			End If
			If ModelState.IsValid Then
				Await manager.CreateBrandAsync(brand)
				TempData("Message") = "Бренд добавлен."
				Return RedirectToAction("index")
			End If
			Return View(brand)
		End Function

		Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim brand = Await manager.FindBrandAsync(id)
			If IsNothing(brand) Then
				Return HttpNotFound()
			End If
			Return View(brand)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Edit(model As Brand, imageFile As HttpPostedFileWrapper, returnUrl As String) As Task(Of ActionResult)
			If Await manager.ExistsBrandAsync(model, model.Id) Then
				ModelState.AddModelError("Name", "Такой бренд уже существует.")
			End If
			If ModelState.IsValid Then
				Await manager.UpdateBrandAsync(model)
				TempData("Message") = "Бренд изменен."
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
			Dim brand = Await manager.FindBrandAsync(id)
			If IsNothing(brand) Then
				Return HttpNotFound()
			End If
			Return View(brand)
		End Function

		<HttpPost>
		<ActionName("Delete")>
		<ValidateAntiForgeryToken>
		Public Async Function DeleteConfirmed(id As Guid) As Task(Of ActionResult)
			Dim brand = Await manager.FindBrandAsync(id)
			Await manager.DeleteBrandAsync(brand)
			TempData("Message") = "Бренд удален."
			Return RedirectToAction("index")
		End Function

		Protected Overrides Sub Dispose(disposing As Boolean)
			If disposing Then
				manager.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace