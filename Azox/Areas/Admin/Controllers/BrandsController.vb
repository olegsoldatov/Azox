Imports System.Data.Entity
Imports System.Threading.Tasks
Imports System.Net
Imports Soldata.Web.Extensions
Imports System.Drawing

Namespace Areas.Admin.Controllers
	<Authorize>
	Public Class BrandsController
		Inherits BaseController

		Private ReadOnly manager As New BrandManager
		Private ReadOnly imageService As New ImageService With {.SmallConfiguration = New ImageConfiguration With {.Width = 320, .Height = 320, .Background = Color.White}}

		Public Async Function Index(filter As FilterViewModel, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 50) As Task(Of ActionResult)
			ViewBag.Filter = filter

			Dim entities = manager.Brands

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchText) Then
				Dim s = filter.SearchText.ToLower.Replace("ё", "е")
				entities = entities.Where(Function(x) x.Title.ToLower.Replace("ё", "е").Contains(s) Or x.Name.ToLower.Replace("ё", "е").Contains(s))
			End If

			' Сортировка.
			entities = entities.OrderByDescending(Function(x) x.LastUpdateDate).ThenBy(Function(x) x.Title)

			Pagination(Await entities.CountAsync)

			Return View(Await entities.Skip(pageIndex * pageSize).Take(pageSize).AsNoTracking().ToListAsync())
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Index(id As Guid(), Optional delete As Boolean = False) As Task(Of ActionResult)
			If Not IsNothing(id) Then
				Dim entities = Await manager.Brands.Where(Function(x) id.Contains(x.Id)).ToListAsync()

				If delete Then
					Await imageService.DeleteAsync(entities)
					Await manager.DeleteAsync(entities)
					TempData("Message") = String.Format("Удалено: {0}.", id.Length.ToString("бренд", "бренда", "брендов"))
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
		Public Async Function Create(model As Brand, imageFile As HttpPostedFileWrapper) As Task(Of ActionResult)
			If Await manager.NameExistsAsync(model) Then
				ModelState.AddModelError("Name", "Такое имя уже существует.")
			End If
			If Not IsNothing(imageFile) AndAlso Not imageFile.ContentType.Contains("image") Then
				ModelState.AddModelError("ImageId", "Файл не является изображением.")
			End If
			If ModelState.IsValid Then
				If Not IsNothing(imageFile) Then
					Await imageService.UploadAsync(model, imageFile.InputStream, imageFile.ContentType)
				End If
				Await manager.AddAsync(model)
				TempData("Message") = "Бренд добавлен."
				Return RedirectToAction("index")
			End If
			Return View(model)
		End Function

		Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim brand = Await manager.GetByIdAsync(id)
			If IsNothing(brand) Then
				Return HttpNotFound()
			End If
			Return View(brand)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Edit(model As Brand, imageFile As HttpPostedFileWrapper, returnUrl As String) As Task(Of ActionResult)
			If Await manager.NameExistsAsync(model) Then
				ModelState.AddModelError("Name", "Такое имя уже существует.")
			End If
			If Not IsNothing(imageFile) AndAlso Not imageFile.ContentType.Contains("image") Then
				ModelState.AddModelError("ImageId", "Файл не является изображением.")
			End If
			If ModelState.IsValid Then
				If Not IsNothing(imageFile) Then
					Await imageService.UploadAsync(model, imageFile.InputStream, imageFile.ContentType)
				End If
				Await manager.UpdateAsync(model)
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
			Dim model = Await manager.GetByIdAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function

		<HttpPost>
		<ActionName("Delete")>
		<ValidateAntiForgeryToken>
		Public Async Function DeleteConfirmed(id As Guid) As Task(Of ActionResult)
			Dim entity = Await manager.GetByIdAsync(id)
			If Not IsNothing(entity.ImageId) Then
				Await imageService.DeleteAsync(entity)
			End If
			Await manager.DeleteAsync(entity)
			TempData("Message") = "Бренд удален."
			Return RedirectToAction("index")
		End Function

		<HttpGet>
		Public Function Exists(id As Guid?, name As String) As ActionResult
			If manager.Brands.AsNoTracking().Any(Function(x) Not x.Id = id And x.Name = name) Then
				Return Json(False, JsonRequestBehavior.AllowGet)
			End If
			Return Json(True, JsonRequestBehavior.AllowGet)
		End Function

		Protected Overrides Sub Dispose(disposing As Boolean)
			If disposing Then
				manager.Dispose()
				imageService.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace