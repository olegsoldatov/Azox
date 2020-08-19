Imports System.Data.Entity
Imports System.Threading.Tasks
Imports System.Net
Imports System.Web.Configuration
Imports Soldata.Imaging

Namespace Controllers
	<Authorize>
	Public Class CategoriesController
		Inherits Controller

		Private ReadOnly db As New ApplicationDbContext

		Public Async Function Index(filter As CategoryFilterViewModel, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 50) As Task(Of ActionResult)
			Dim entities = db.Categories.AsQueryable

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchText) Then
				Dim s = filter.SearchText.Trim.ToLower.Replace("ё", "е")
				entities = entities.Where(Function(x) x.Name.Trim.ToLower.Replace("ё", "е").Contains(s)) ' Or x.Title.Trim.ToLower.Replace("ё", "е").Contains(s) Or x.Heading.Trim.ToLower.Replace("ё", "е").Contains(s))
			End If

			' Сортировка.
			entities = entities.OrderBy(Function(x) x.Name) '.ThenBy(Function(x) x.Title).ThenBy(Function(x) x.Heading).ThenBy(Function(x) x.Order)

			' Количество и пагинация.
			ViewBag.EntityCount = Await entities.CountAsync
			ViewBag.PageIndex = pageIndex
			ViewBag.PageCount = CInt(Math.Ceiling(ViewBag.EntityCount / pageSize))

			' Фильтр.
			ViewBag.Filter = filter

			Return View(Await entities.Skip(pageIndex * pageSize).Take(pageSize).AsNoTracking.ToListAsync)
		End Function

		Public Async Function Create() As Task(Of ActionResult)
			ViewBag.ParentId = New SelectList((Await db.Categories.ToListAsync).Select(Function(c) New With {c.Id, .Name = c.GetPath}).OrderBy(Function(a) a.Name), "Id", "Name")
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View()
		End Function

		<HttpPost, ValidateAntiForgeryToken>
		Public Async Function Create(model As Category, returnUrl As String) As Task(Of ActionResult)
			If ModelState.IsValid Then
				model.Slug = ValidateSlug(model.Id, If(model.Slug, model.Name.ToSlug(True)))
				db.Categories.Add(model)
				Await AddImageAsync(model, model.ImageFile)
				Await db.SaveChangesAsync
				TempData("Message") = "Категория добавлена."
				Return RedirectToLocal(returnUrl)
			End If
			ViewBag.ParentId = New SelectList((Await db.Categories.ToListAsync).Select(Function(c) New With {c.Id, .Name = c.GetPath}).OrderBy(Function(a) a.Name), "Id", "Name", model.ParentId)
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View(model)
		End Function

		Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await db.Categories.FindAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			ViewBag.ParentId = New SelectList((Await db.Categories.Where(Function(c) Not c.Id = model.Id).ToListAsync).Select(Function(c) New With {c.Id, .Name = c.GetPath}).OrderBy(Function(a) a.Name), "Id", "Name", model.ParentId)
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View(model)
		End Function

		<HttpPost, ValidateAntiForgeryToken>
		Public Async Function Edit(model As Category, returnUrl As String) As Task(Of ActionResult)
			If ModelState.IsValid Then
				model.Slug = ValidateSlug(model.Id, If(model.Slug, model.Name.ToSlug(True)))
				db.Entry(model).State = EntityState.Modified
				Await AddImageAsync(model, model.ImageFile)
				Await db.SaveChangesAsync
				TempData("Message") = "Категория изменена."
				Return RedirectToLocal(returnUrl)
			End If
			ViewBag.ParentId = New SelectList((Await db.Categories.Where(Function(c) Not c.Id = model.Id).ToListAsync).Select(Function(c) New With {c.Id, .Name = c.GetPath}).OrderBy(Function(a) a.Name), "Id", "Name", model.ParentId)
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View(model)
		End Function

		Public Async Function Delete(id As Guid?) As Task(Of ActionResult)
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
		Public Async Function DeleteConfirmed(id As Guid, returnUrl As String) As Task(Of ActionResult)
			Dim entity = Await db.Categories.FindAsync(id)
			For Each item In entity.Products
				item.CategoryId = Nothing
			Next

			For Each item In entity.Childs
				item.ParentId = Nothing
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

		<HttpPost>
		Public Async Function DeleteChecked(id As Guid()) As Task(Of ActionResult)
			Try
				Await db.Categories.Where(Function(x) id.Contains(x.Id)).Include(Function(x) x.Products).ForEachAsync(Sub(x As Category) x.Products.ToList.ForEach(Function(p) p.CategoryId = Nothing))

				Await db.Categories.Where(Function(x) id.Contains(x.Id)).Include(Function(x) x.Childs).ForEachAsync(Sub(x As Category) x.Childs.ToList.ForEach(Function(c) c.ParentId = Nothing))

				Await db.Categories.Where(Function(x) id.Contains(x.Id)).Select(Function(x) x.ImageId).ForEachAsync(Sub(x As Guid?) If db.Images.Find(x) IsNot Nothing Then db.Images.Remove(db.Images.Find(x)))

				Await db.Categories.Where(Function(x) id.Contains(x.Id)).ForEachAsync(Sub(c As Category) db.Entry(c).State = EntityState.Deleted)
				Await db.SaveChangesAsync

				TempData("Message") = String.Format("Удалено категорий: {0}", id.Length)
				Return Json(New With {.redirect = Url.Action("index")})
			Catch ex As Exception
				TempData("Error") = ex.Message
				Return Json(New With {.redirect = Url.Action("index")})
			End Try
		End Function

		''' <summary>
		''' Применяет фильтр и сортировку к перечислению категорий.
		''' </summary>
		''' <param name="categories">Перечисление категорий, к которому применяется фильтр.</param>
		''' <param name="filter">Фильтр.</param>
		''' <returns>Отсортированное перечисление категорий после применения фильтра.</returns>
		Private Function ApplyFilterAndOrder(categories As IQueryable(Of Category), filter As CategoryFilterViewModel) As IOrderedQueryable(Of Category)

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchText) Then
				Dim searchString = filter.SearchText.Trim.ToLower.Replace("ё", "е")
				categories = categories.Where(Function(x) x.Name.ToLower.Replace("ё", "е").Contains(searchString) Or x.Slug.ToLower.Contains(searchString))
			End If

			' Сортировка (по умолчанию по названию).
			categories = categories.OrderBy(Function(x) x.Name)

			Return categories
		End Function

		''' <summary>
		''' Добавляет загруженный файл изображения в модель данных.
		''' </summary>
		''' <param name="model">Модель данных</param>
		''' <param name="imageFile">Загруженный файл изображения.</param>
		''' <exception cref="ArgumentNullException"></exception>
		Private Async Function AddImageAsync(model As Category, imageFile As HttpPostedFileWrapper) As Task(Of Integer)
			If IsNothing(model) Then
				Throw New ArgumentNullException(NameOf(model))
			End If

			If IsNothing(imageFile) OrElse imageFile.ContentLength = 0 Then
				Return 0
			End If

			Dim image = Await db.Images.FindAsync(model.ImageId)

			If image Is Nothing Then
				image = db.Images.Add(New Image With {.Id = Guid.NewGuid})
				model.ImageId = image.Id
			End If

			With image
				.ContentType = imageFile.ContentType
				.Original = ImageUtility.Generate(imageFile.InputStream, imageFile.ContentType)
				.Thumbnail = ImageUtility.Generate(imageFile.InputStream, imageFile.ContentType, 200, 200, StretchMode.UniformToFill)
				.Small = ImageUtility.Generate(imageFile.InputStream, imageFile.ContentType, 320, 180, StretchMode.UniformToFill)
				.Medium = ImageUtility.Generate(imageFile.InputStream, imageFile.ContentType, 544, 306, StretchMode.UniformToFill)
				.Large = ImageUtility.Generate(imageFile.InputStream, imageFile.ContentType, 1200, 225, StretchMode.UniformToFill)
			End With

			Return 1
		End Function

		Private Function RedirectToLocal(returnUrl As String) As ActionResult
			If Url.IsLocalUrl(returnUrl) Then
				Return Redirect(returnUrl)
			End If
			Return RedirectToAction("index")
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