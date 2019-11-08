Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks
Imports System.Web.Configuration
Imports Soldata.Imaging

Namespace Controllers
	<Authorize>
	Public Class BrandsController
		Inherits Controller

		Private db As New ApplicationDbContext

		<AllowAnonymous>
		Async Function Index() As Task(Of ActionResult)
			Dim brands = db.Brands.AsQueryable
			Return View(Await brands.OrderBy(Function(x) x.Name).ToListAsync)
		End Function

		<AllowAnonymous>
		Async Function Details(ByVal id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim brand As Brand = Await db.Brands.FindAsync(id)
			If IsNothing(brand) Then
				Return HttpNotFound()
			End If
			Return View(brand)
		End Function

		Async Function Manage(filter As BrandFilterViewModel, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 20) As Task(Of ActionResult)
			Dim entities = db.Brands.AsQueryable

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchString) Then
				Dim searchString = filter.SearchString.Trim
				entities = entities.Where(Function(x) x.Name.Contains(searchString))
			End If

			' Фильтр.
			ViewBag.Filter = filter

			' Сортировка.
			entities = entities.OrderBy(Function(x) x.Name)

			' Пагинация.
			ViewBag.PageIndex = pageIndex
			ViewBag.PageCount = CInt(Math.Ceiling(Await entities.CountAsync / pageSize))

			Return View(Await entities.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync)
		End Function

		Function Create() As ActionResult
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View()
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Async Function Create(brand As Brand, returnUrl As String) As Task(Of ActionResult)
			If ModelState.IsValid Then
				brand.Slug = ValidateSlug(brand.Id, If(brand.Slug, brand.Name).ToSlug(True))
				db.Brands.Add(brand)

				If brand.ImageFile IsNot Nothing AndAlso brand.ImageFile.ContentLength > 0 Then
					Dim image As New Image With {
						.ContentType = brand.ImageFile.ContentType,
						.Original = ImageUtility.Generate(brand.ImageFile.InputStream, brand.ImageFile.ContentType),
						.Thumbnail = ImageUtility.Generate(brand.ImageFile.InputStream, brand.ImageFile.ContentType, 200, 200, StretchMode.UniformToFill),
						.Small = ImageUtility.Generate(brand.ImageFile.InputStream, brand.ImageFile.ContentType, 320, 320, StretchMode.Uniform, Drawing.Color.White)
					}
					brand.ImageId = image.Id
					db.Images.Add(image)
				End If

				Await db.SaveChangesAsync()
				TempData("Message") = "Бренд добавлен."
				Return RedirectToLocal(returnUrl)
			End If
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View(brand)
		End Function

		Async Function Edit(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim brand As Brand = Await db.Brands.FindAsync(id)
			If IsNothing(brand) Then
				Return HttpNotFound()
			End If
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View(brand)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Async Function Edit(brand As Brand, returnUrl As String) As Task(Of ActionResult)
			If ModelState.IsValid Then
				brand.Slug = ValidateSlug(brand.Id, If(brand.Slug, brand.Name).ToSlug(True))
				db.Entry(brand).State = EntityState.Modified

				If brand.ImageFile IsNot Nothing AndAlso brand.ImageFile.ContentLength > 0 Then
					Dim image = Await db.Images.FindAsync(brand.ImageId)
					If image Is Nothing Then
						image = New Image
						brand.ImageId = image.Id
						db.Images.Add(image)
					End If
					With image
						.ContentType = brand.ImageFile.ContentType
						.Original = ImageUtility.Generate(brand.ImageFile.InputStream, brand.ImageFile.ContentType)
						.Thumbnail = ImageUtility.Generate(brand.ImageFile.InputStream, brand.ImageFile.ContentType, 200, 200, StretchMode.UniformToFill)
						.Small = ImageUtility.Generate(brand.ImageFile.InputStream, brand.ImageFile.ContentType, 320, 320, StretchMode.Uniform, Drawing.Color.White)
					End With
				End If

				Await db.SaveChangesAsync()
				TempData("Message") = "Бренд изменен."
				Return RedirectToLocal(returnUrl)
			End If
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View(brand)
		End Function

		Async Function Delete(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim brand As Brand = Await db.Brands.FindAsync(id)
			If IsNothing(brand) Then
				Return HttpNotFound()
			End If
			Return View(brand)
		End Function

		<HttpPost>
		<ActionName("Delete")>
		<ValidateAntiForgeryToken>
		Async Function DeleteConfirmed(ByVal id As Guid, returnUrl As String) As Task(Of ActionResult)
			Dim brand As Brand = Await db.Brands.FindAsync(id)
			For Each item In brand.Products
				item.BrandId = Nothing
			Next

			Dim image = Await db.Images.FindAsync(brand.ImageId)
			If image IsNot Nothing Then
				db.Images.Remove(image)
			End If

			db.Brands.Remove(brand)
			Await db.SaveChangesAsync()
			TempData("Message") = "Бренд удален."
			Return RedirectToLocal(returnUrl)
		End Function

		Async Function DeleteImage(id As Guid) As Task(Of ActionResult)
			Dim entity = Await db.Brands.FindAsync(id)

			Dim image = Await db.Images.FindAsync(entity.ImageId)
			If image IsNot Nothing Then
				db.Images.Remove(image)
			End If

			entity.ImageId = Nothing

			Await db.SaveChangesAsync
			TempData("Message") = "Изображение удалено."
			Return RedirectToAction("edit", New With {id})
		End Function

		Private Function RedirectToLocal(returnUrl As String) As ActionResult
			If Url.IsLocalUrl(returnUrl) Then
				Return Redirect(returnUrl)
			End If
			Return RedirectToAction("manage")
		End Function

		Protected Friend Function ValidateSlug(entityId As Guid, slug As String, Optional suffix As Integer = 0) As String
			Dim result = If(suffix = 0, slug, String.Join("-", slug, suffix))
			If db.Set(Of Brand).AsNoTracking.Any(Function(m) m.Slug.Equals(result) And Not m.Id.Equals(entityId)) Then
				Return ValidateSlug(entityId, slug, suffix + 1)
			End If
			Return result
		End Function

		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				db.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace
