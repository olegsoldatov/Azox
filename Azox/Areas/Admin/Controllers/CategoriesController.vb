Imports System.Data.Entity
Imports System.Threading.Tasks
Imports System.Net
Imports System.Web.Configuration
Imports Soldata.Imaging
Imports Soldata.Web.Extensions

Namespace Areas.Admin.Controllers
	<Authorize>
	Public Class CategoriesController
		Inherits Controller

		Private ReadOnly manager As New CatalogManager

		Public Async Function Index(filter As FilterViewModel, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 50) As Task(Of ActionResult)
			Dim entities = manager.Categories.AsNoTracking

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchText) Then
				Dim s = filter.SearchText.ToLower.Replace("ё", "е")
				entities = entities.Where(Function(x) x.Title.ToLower.Replace("ё", "е").Contains(s) Or x.Name.ToLower.Replace("ё", "е").Contains(s))
			End If

			' Количество и пагинация.
			Dim count = Await entities.CountAsync
			ViewBag.Count = count
			ViewBag.PageIndex = pageIndex
			ViewBag.PageCount = CInt(Math.Ceiling(count / pageSize))

			' Сортировка.
			entities = entities.OrderBy(Function(x) x.Parent.Title).ThenBy(Function(x) x.Title)

			' Фильтр.
			ViewBag.Filter = filter

			Return View((Await entities _
				.Select(Function(x) New With {
					x.Id,
					x.Name,
					x.Title,
					x.Path,
					x.Products.Count,
					x.Order,
					x.Draft}) _
				.Skip(pageIndex * pageSize) _
				.Take(pageSize) _
				.ToListAsync) _
				.Select(Function(x) New CategoryAdminItem With {
					.Id = x.Id,
					.Title = If(manager.GetCategoryPath(x.Path), x.Title),
					.Name = x.Name,
					.Products = x.Count,
					.Order = x.Order,
					.Draft = x.Draft}))
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Index(id As Guid(), returnUrl As String, Optional delete As Boolean = False) As Task(Of ActionResult)
			If Not IsNothing(id) AndAlso id.Any Then
				If delete Then
					Await manager.Context.Categories.Where(Function(x) id.Contains(x.Id)).ForEachAsync(Sub(x As Category) If manager.Context.Images.Find(x.ImageId) IsNot Nothing Then manager.Context.Images.Remove(manager.Context.Images.Find(x.ImageId)))

					For Each category In Await manager.Context.Categories.Where(Function(x) id.Contains(x.Id)).ToListAsync
						Dim entityPath = category.Id.ToString & "/"
						Await manager.Context.Categories.Where(Function(x) x.Path.Contains(entityPath)).ForEachAsync(Sub(c As Category) c.Path = c.Path.Replace(entityPath, ""))
						Await manager.Context.Categories.Where(Function(x) x.ParentId = category.Id).ForEachAsync(Sub(c As Category) c.ParentId = category.ParentId)
						Await manager.Context.Products.Where(Function(x) x.CategoryId = category.Id).ForEachAsync(Sub(p As Product) p.CategoryId = category.ParentId)
						category.ParentId = Nothing
					Next

					Await manager.Context.Categories.Where(Function(x) id.Contains(x.Id)).ForEachAsync(Sub(x As Category) manager.Context.Entry(x).State = EntityState.Deleted)
					Await manager.Context.SaveChangesAsync
					TempData("Message") = String.Format("Удалено: {0}.", id.Length.ToString("категория", "категории", "категорий"))
				End If
			End If

			Return Redirect(returnUrl)
		End Function

		Public Async Function Create() As Task(Of ActionResult)
			ViewBag.ParentId = New SelectList((Await manager.Categories.ToListAsync).Select(Function(c) New With {c.Id, .Name = c.GetPath}).OrderBy(Function(a) a.Name), "Id", "Name")
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View()
		End Function

		<HttpPost, ValidateAntiForgeryToken>
		Public Async Function Create(model As Category) As Task(Of ActionResult)
			If Await manager.CategoryNameExistsAsync(model.Name) Then
				ModelState.AddModelError("Name", "Такое имя уже существует.")
			End If
			If ModelState.IsValid Then
				'model.Id = Guid.NewGuid
				'model.Name = ValidateSlug(model.Id, If(model.Name, model.Name.ToSlug(True)))
				'model.Path = ""
				'CatalogManager.Context.Categories.Add(model)
				'Await AddImageAsync(model, model.ImageFile)
				'Await CatalogManager.Context.SaveChangesAsync
				Await manager.AddCategoryAsync(model)
				TempData("Message") = "Категория добавлена."
				Return RedirectToAction("index")
			End If
			ViewBag.ParentId = New SelectList((Await manager.Categories.ToListAsync).Select(Function(c) New With {c.Id, .Name = c.GetPath}).OrderBy(Function(a) a.Name), "Id", "Name", model.ParentId)
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View(model)
		End Function

		Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await manager.FindCategoryByIdAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			ViewBag.ParentId = New SelectList((Await manager.Categories.Where(Function(c) Not c.Id = model.Id).ToListAsync).Select(Function(c) New With {c.Id, .Name = c.GetPath}).OrderBy(Function(a) a.Name), "Id", "Name", model.ParentId)
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View(model)
		End Function

		<HttpPost, ValidateAntiForgeryToken>
		Public Async Function Edit(model As Category, returnUrl As String) As Task(Of ActionResult)
			If Await manager.CategoryNameExistsAsync(model.Name, model.Id) Then
				ModelState.AddModelError("Name", "Такое имя уже существует.")
			End If
			If ModelState.IsValid Then
				manager.Context.Entry(model).State = EntityState.Modified
				Await AddImageAsync(model, model.ImageFile)
				Await manager.Context.SaveChangesAsync
				TempData("Message") = "Категория изменена."
				Return Redirect(returnUrl)
			End If
			ViewBag.ParentId = New SelectList((Await manager.Categories.Where(Function(c) Not c.Id = model.Id).ToListAsync).Select(Function(c) New With {c.Id, .Name = c.GetPath}).OrderBy(Function(a) a.Name), "Id", "Name", model.ParentId)
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View(model)
		End Function

		Public Async Function Delete(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await manager.FindCategoryByIdAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Delete(id As Guid, returnUrl As String) As Task(Of ActionResult)
			Dim entity = Await manager.FindCategoryByIdAsync(id)
			For Each item In entity.Products
				item.CategoryId = Nothing
			Next

			For Each item In entity.Childs
				item.ParentId = Nothing
			Next

			Dim image = Await manager.Context.Images.FindAsync(entity.ImageId)
			If image IsNot Nothing Then
				manager.Context.Images.Remove(image)
			End If

			manager.Context.Categories.Remove(entity)
			Await manager.Context.SaveChangesAsync
			TempData("Message") = "Категория удалена."
			Return Redirect(returnUrl)
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
				categories = categories.Where(Function(x) x.Name.ToLower.Replace("ё", "е").Contains(searchString) Or x.Name.ToLower.Contains(searchString))
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

			Dim image = Await manager.Context.Images.FindAsync(model.ImageId)

			If image Is Nothing Then
				image = manager.Context.Images.Add(New Image With {.Id = Guid.NewGuid})
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

		'Protected Friend Function ValidateSlug(entityId As Guid, slug As String, Optional suffix As Integer = 0) As String
		'	Dim result = If(suffix = 0, slug, String.Join("-", slug, suffix))
		'	If CatalogManager.Context.Set(Of Category).AsNoTracking.Any(Function(m) m.Name.Equals(result) And Not m.Id.Equals(entityId)) Then
		'		Return ValidateSlug(entityId, slug, suffix + 1)
		'	End If
		'	Return result
		'End Function

		<HttpGet>
		Public Function UploadCache() As ActionResult
			CatalogManager.Load()
			TempData("Message") = "Кэш обновлен."
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