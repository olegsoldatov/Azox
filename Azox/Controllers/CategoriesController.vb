Imports System.Data.Entity
Imports System.Threading.Tasks
Imports System.Net
Imports System.Web.Configuration
Imports Soldata.Imaging

Namespace Controllers
	<Authorize>
	Public Class CategoriesController
		Inherits Controller

		Private db As New ApplicationDbContext

		<AllowAnonymous>
		Async Function Index(filter As CategoryFilterViewModel, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 16) As Task(Of ActionResult)
			Dim entities = db.Categories.AsQueryable

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchText) Then
				Dim searchString = filter.SearchText.Trim
				entities = entities.Where(Function(x) x.Name.Contains(searchString))
			End If

			' Фильтр.
			ViewBag.Filter = filter

			' Сортировка.
			entities = entities.OrderBy(Function(x) x.Order)

			' Пагинация.
			ViewBag.PageIndex = pageIndex
			ViewBag.PageCount = CInt(Math.Ceiling(Await entities.CountAsync / pageSize))

			Return View(Await entities.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync)
		End Function

		Async Function Manage(filter As CategoryFilterViewModel, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 20) As Task(Of ActionResult)
			Dim entities = db.Categories.AsQueryable

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchText) Then
				Dim searchString = filter.SearchText.Trim
				entities = entities.Where(Function(x) x.Name.Contains(searchString))
			End If

			' Фильтр.
			ViewBag.Filter = filter

			' Сортировка.
			entities = entities.OrderBy(Function(x) x.Order)

			' Пагинация.
			ViewBag.PageIndex = pageIndex
			ViewBag.PageCount = CInt(Math.Ceiling(Await entities.CountAsync / pageSize))

			Return View(Await entities.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync)
		End Function

		Async Function Create() As Task(Of ActionResult)
			ViewBag.ParentId = New SelectList(Await db.Categories.Select(Function(c) New With {.Value = c.Id, .Text = c.Name}).OrderBy(Function(a) a.Text).ToListAsync, "Value", "Text")
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View()
		End Function

		<HttpPost, ValidateAntiForgeryToken>
		Async Function Create(model As Category, returnUrl As String) As Task(Of ActionResult)
			If ModelState.IsValid Then
				model.Slug = ValidateSlug(model.Id, If(model.Slug, model.Name).ToSlug(True))
				db.Categories.Add(model)

				If model.ImageFile IsNot Nothing AndAlso model.ImageFile.ContentLength > 0 Then
					Dim image As New Image With {
						.ContentType = model.ImageFile.ContentType,
						.Original = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType),
						.Thumbnail = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType, 200, 200, StretchMode.UniformToFill),
						.Small = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType, 320, 180, StretchMode.UniformToFill),
						.Medium = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType, 544, 306, StretchMode.UniformToFill),
						.Large = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType, 1200, 225, StretchMode.UniformToFill)
					}
					model.ImageId = image.Id
					db.Images.Add(image)
				End If

				Await db.SaveChangesAsync
				TempData("Message") = "Категория добавлена."
				Return RedirectToLocal(returnUrl)
			End If
			ViewBag.ParentId = New SelectList(Await db.Categories.Where(Function(c) Not c.Id = model.Id).Select(Function(c) New With {.Value = c.Id, .Text = c.Name}).OrderBy(Function(a) a.Text).ToListAsync, "Value", "Text", model.ParentId)
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View(model)
		End Function

		Async Function Edit(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await db.Categories.FindAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			ViewBag.ParentId = New SelectList(Await db.Categories.Where(Function(c) Not c.Id = model.Id).Select(Function(c) New With {.Value = c.Id, .Text = c.Name}).OrderBy(Function(a) a.Text).ToListAsync, "Value", "Text", model.ParentId)
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View(model)
		End Function

		<HttpPost, ValidateAntiForgeryToken>
		Async Function Edit(model As Category, returnUrl As String) As Task(Of ActionResult)
			If ModelState.IsValid Then
				model.Slug = ValidateSlug(model.Id, If(model.Slug, model.Name).ToSlug(True))
				db.Entry(model).State = EntityState.Modified

				If model.ImageFile IsNot Nothing AndAlso model.ImageFile.ContentLength > 0 Then
					Dim image = Await db.Images.FindAsync(model.ImageId)
					If image Is Nothing Then
						image = New Image
						model.ImageId = image.Id
						db.Images.Add(image)
					End If
					With image
						.ContentType = model.ImageFile.ContentType
						.Original = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType)
						.Thumbnail = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType, 200, 200, StretchMode.UniformToFill)
						.Small = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType, 320, 180, StretchMode.UniformToFill)
						.Medium = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType, 544, 306, StretchMode.UniformToFill)
						.Large = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType, 1200, 225, StretchMode.UniformToFill)
					End With
				End If

				Await db.SaveChangesAsync
				TempData("Message") = "Категория изменена."
				Return RedirectToLocal(returnUrl)
			End If
			ViewBag.ParentId = New SelectList(Await db.Categories.Where(Function(c) Not c.Id = model.Id).Select(Function(c) New With {.Value = c.Id, .Text = c.Name}).OrderBy(Function(a) a.Text).ToListAsync, "Value", "Text", model.ParentId)
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View(model)
		End Function

		Async Function Delete(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await db.Categories.FindAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function

		<HttpPost, ActionName("Delete"), ValidateAntiForgeryToken>
		Async Function DeleteConfirmed(id As Guid, returnUrl As String) As Task(Of ActionResult)
			Dim entity = Await db.Categories.FindAsync(id)
			For Each item In entity.Products
				item.CategoryId = Nothing
			Next

			Dim image = Await db.Images.FindAsync(entity.ImageId)
			If image IsNot Nothing Then
				db.Images.Remove(image)
			End If

			db.Categories.Remove(entity)
			Await db.SaveChangesAsync
			TempData("Message") = "Категория удалена."
			Return RedirectToLocal(returnUrl)
		End Function

		Async Function DeleteImage(id As Guid) As Task(Of ActionResult)
			Dim entity = Await db.Categories.FindAsync(id)

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
			If db.Set(Of Category).AsNoTracking.Any(Function(m) m.Slug.Equals(result) And Not m.Id.Equals(entityId)) Then
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