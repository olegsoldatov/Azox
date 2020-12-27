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
			Dim entities = manager.Brands

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchText) Then
				Dim s = filter.SearchText.ToLower.Replace("ё", "е")
				entities = entities.Where(Function(x) x.Name.ToLower.Replace("ё", "е").Contains(s) Or x.Title.ToLower.Replace("ё", "е").Contains(s))
			End If

			' Сортировка.
			entities = entities.OrderBy(Function(x) x.Title)

			' Количество и пагинация.
			Dim count = Await entities.CountAsync
			ViewBag.Count = count
			ViewBag.PageIndex = pageIndex
			ViewBag.PageCount = CInt(Math.Ceiling(count / pageSize))

			' Фильтр.
			ViewBag.Filter = filter

			Return View(Await entities _
				.Select(Function(x) New BrandAdminItem With {
					.Id = x.Id,
					.Name = x.Name,
					.Title = x.Title,
					.ImageId = x.ImageId,
					.Products = x.Products.Count,
					.Order = x.Order,
					.Draft = x.Draft}) _
				.Skip(pageIndex * pageSize) _
				.Take(pageSize) _
				.ToListAsync)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Index(id As Guid(), returnUrl As String, Optional delete As Boolean = False) As Task(Of ActionResult)
			If Not IsNothing(id) AndAlso id.Any Then
				Dim brands = manager.Brands.Where(Function(x) id.Contains(x.Id))
				If delete Then
					Await manager.DeleteBrandRangeAsync(Await brands.ToListAsync)
					TempData("Message") = String.Format("Удалено: {0}", id.Length.ToString("бренд", "бренда", "брендов"))
				End If
			End If
			Return Redirect(returnUrl)
		End Function

		<HttpGet>
		Public Function Create() As ActionResult
			Return View()
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Create(brand As Brand, returnUrl As String) As Task(Of ActionResult)
			If Await manager.ExistsBrandAsync(brand) Then
				ModelState.AddModelError("Name", "Такой бренд уже существует.")
			End If
			If ModelState.IsValid Then
				Await manager.CreateBrandAsync(brand)
				TempData("Message") = "Бренд добавлен."
				Return Redirect(returnUrl)
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
		Public Async Function Edit(brand As Brand, returnUrl As String) As Task(Of ActionResult)
			If Await manager.ExistsBrandAsync(brand, brand.Id) Then
				ModelState.AddModelError("Name", "Такой бренд уже существует.")
			End If
			If ModelState.IsValid Then
				Await manager.UpdateBrandAsync(brand)
				TempData("Message") = "Бренд изменен."
				Return Redirect(returnUrl)
			End If
			Return View(brand)
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
		<ValidateAntiForgeryToken>
		Public Async Function Delete(id As Guid, returnUrl As String) As Task(Of ActionResult)
			Dim brand = Await manager.FindBrandAsync(id)
			Await manager.DeleteBrandAsync(brand)
			TempData("Message") = "Бренд удален."
			Return Redirect(returnUrl)
		End Function

#Region "Image"

		<HttpGet>
		Public Function CreateImage(id As Guid?) As ActionResult
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Return View(id)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function CreateImage(id As Guid, imageFile As HttpPostedFileWrapper, returnUrl As String) As Task(Of ActionResult)
			If IsNothing(imageFile) Then
				ModelState.AddModelError("ImageFile", "Не выбран файл.")
			ElseIf Not imageFile.ContentType.Contains("image") Then
				ModelState.AddModelError("ImageFile", "Файл не является изображением.")
			End If

			If ModelState.IsValid Then
				Dim image As New Image With {
					.Id = Guid.NewGuid,
					.ContentType = imageFile.ContentType,
					.Original = ImageUtility.Generate(imageFile.InputStream, imageFile.ContentType),
					.Thumbnail = ImageUtility.Generate(imageFile.InputStream, imageFile.ContentType, 200, 200, StretchMode.Uniform, Drawing.Color.White),
					.Small = ImageUtility.Generate(imageFile.InputStream, imageFile.ContentType, 200, 200, StretchMode.Uniform, Drawing.Color.White),
					.Medium = ImageUtility.Generate(imageFile.InputStream, imageFile.ContentType, 400, 300, StretchMode.Uniform, Drawing.Color.White)}

				Await manager.ImageManager.CreateAsync(image)

				Dim brand = Await manager.FindBrandAsync(id)
				brand.ImageId = image.Id
				Await manager.UpdateBrandAsync(brand)
				TempData("Message") = "Изображение добавлено."
				Return Redirect(returnUrl)
			End If

			Return View(id)
		End Function

		<HttpGet>
		Public Function DeleteImage(id As Guid?) As ActionResult
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Return View(id)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function DeleteImage(id As Guid, returnUrl As String) As Task(Of ActionResult)
			Dim brand = Await manager.FindBrandAsync(id)
			Dim image = Await manager.ImageManager.FindByIdAsync(brand.ImageId.GetValueOrDefault)
			If image IsNot Nothing Then
				Await manager.ImageManager.DeleteAsync(image)
			End If
			brand.ImageId = Nothing
			Await manager.UpdateBrandAsync(brand)
			TempData("Message") = "Изображение удалено."
			Return Redirect(returnUrl)
		End Function

#End Region

		Protected Overrides Sub Dispose(disposing As Boolean)
			If disposing Then
				manager.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace