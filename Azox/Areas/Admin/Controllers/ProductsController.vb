Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks
Imports System.Web.Configuration
Imports Soldata.Imaging

Namespace Areas.Admin.Controllers
	<Authorize>
	Public Class ProductsController
		Inherits Controller

		Private ReadOnly db As New ApplicationDbContext

		Public Async Function Index(filter As ProductFilterViewModel, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 50) As Task(Of ActionResult)
			Dim entities = db.Products.AsQueryable

			' Фильтр.
			Dim parameters = Await entities.Select(Function(x) New With {x.Category, x.Brand, x.Warehouse}).ToListAsync
			ViewBag.CategoryId = New SelectList(parameters.Select(Function(x) x.Category).Where(Function(x) Not IsNothing(x)).GroupBy(Function(x) x).Select(Function(x) New With {x.Key.Id, .Name = x.Key.GetPath}).OrderBy(Function(x) x.Name), "Id", "Name")
			ViewBag.BrandId = New SelectList(parameters.Select(Function(x) x.Brand).Where(Function(x) Not IsNothing(x)).GroupBy(Function(x) x).OrderBy(Function(x) x.Key.Name).Select(Function(x) New With {x.Key.Id, x.Key.Name}), "Id", "Name")
			ViewBag.WarehouseId = New SelectList(parameters.Select(Function(x) x.Warehouse).Where(Function(x) Not IsNothing(x)).GroupBy(Function(x) x).OrderBy(Function(x) x.Key.Name).Select(Function(x) New With {x.Key.Id, x.Key.Name}), "Id", "Name")
			ViewBag.Filter = filter

			' Количество и пагинация.
			ViewBag.TotalCount = Await ApplyFilter(entities, filter).Select(Function(x) x.Id).CountAsync
			ViewBag.PageIndex = pageIndex
			ViewBag.PageCount = CInt(Math.Ceiling(ViewBag.TotalCount / pageSize))

			' Модель.
			Dim model = (Await ApplyFilter(entities, filter).Select(Function(x) New With {x.Id, x.Name, x.Sku, x.Price, x.OldPrice, x.Category, x.Brand, x.Warehouse, x.IsPublished}).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync).Select(Function(x) New ProductAdminViewModel With {.Id = x.Id, .Name = x.Name, .Sku = x.Sku, .Price = x.Price, .OldPrice = x.OldPrice, .CategoryName = If(IsNothing(x.Category), "", x.Category.Name), .BrandName = If(IsNothing(x.Brand), "", x.Brand.Name), .WarehouseName = If(IsNothing(x.Warehouse), "", x.Warehouse.Name), .IsPublished = x.IsPublished})

			Return View(model)
		End Function

		Public Async Function Create(categoryId As Guid?, brandId As Guid?, warehouseId As Guid?) As Task(Of ActionResult)
			ViewBag.CategoryId = New SelectList((Await db.Categories.ToListAsync).Select(Function(c) New With {c.Id, .Name = c.GetPath}).OrderBy(Function(a) a.Name), "Id", "Name", categoryId)
			ViewBag.BrandId = New SelectList(Await db.Brands.Select(Function(x) New With {x.Id, x.Name}).OrderBy(Function(a) a.Name).ToListAsync, "Id", "Name", brandId)
			ViewBag.WarehouseId = New SelectList(Await db.Warehouses.Select(Function(x) New With {x.Id, x.Name}).OrderBy(Function(a) a.Name).ToListAsync, "Id", "Name", warehouseId)
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View()
		End Function

		<HttpPost, ValidateAntiForgeryToken>
		Public Async Function Create(model As Product, returnUrl As String) As Task(Of ActionResult)
			If ModelState.IsValid Then
				db.Products.Add(model)

				Await AddImageAsync(model, model.ImageFile)

				Await db.SaveChangesAsync
				TempData("Message") = "Продукт добавлен."
				Return RedirectToLocal(returnUrl)
			End If
			ViewBag.CategoryId = New SelectList((Await db.Categories.ToListAsync).Select(Function(c) New With {c.Id, .Name = c.GetPath}).OrderBy(Function(a) a.Name), "Id", "Name", model.CategoryId)
			ViewBag.BrandId = New SelectList(Await db.Brands.Select(Function(x) New With {x.Id, x.Name}).OrderBy(Function(a) a.Name).ToListAsync, "Id", "Name", model.BrandId)
			ViewBag.WarehouseId = New SelectList(Await db.Warehouses.Select(Function(x) New With {x.Id, x.Name}).OrderBy(Function(a) a.Name).ToListAsync, "Id", "Name", model.WarehouseId)
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View(model)
		End Function

		Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await db.Products.FindAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			ViewBag.CategoryId = New SelectList((Await db.Categories.ToListAsync).Select(Function(c) New With {c.Id, .Name = c.GetPath}).OrderBy(Function(a) a.Name), "Id", "Name", model.CategoryId)
			ViewBag.BrandId = New SelectList(Await db.Brands.Select(Function(x) New With {x.Id, x.Name}).OrderBy(Function(a) a.Name).ToListAsync, "Id", "Name", model.BrandId)
			ViewBag.WarehouseId = New SelectList(Await db.Warehouses.Select(Function(x) New With {x.Id, x.Name}).OrderBy(Function(a) a.Name).ToListAsync, "Id", "Name", model.WarehouseId)
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View(model)
		End Function

		<HttpPost, ValidateAntiForgeryToken>
		Public Async Function Edit(model As Product, returnUrl As String) As Task(Of ActionResult)
			If ModelState.IsValid Then
				db.Entry(model).State = EntityState.Modified
				Await AddImageAsync(model, model.ImageFile)
				Await db.SaveChangesAsync
				TempData("Message") = "Продукт изменен."
				Return RedirectToLocal(returnUrl)
			End If
			ViewBag.CategoryId = New SelectList((Await db.Categories.ToListAsync).Select(Function(c) New With {c.Id, .Name = c.GetPath}).OrderBy(Function(a) a.Name), "Id", "Name", model.CategoryId)
			ViewBag.BrandId = New SelectList(Await db.Brands.Select(Function(x) New With {x.Id, x.Name}).OrderBy(Function(a) a.Name).ToListAsync, "Id", "Name", model.BrandId)
			ViewBag.WarehouseId = New SelectList(Await db.Warehouses.Select(Function(x) New With {x.Id, x.Name}).OrderBy(Function(a) a.Name).ToListAsync, "Id", "Name", model.WarehouseId)
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View(model)
		End Function

		Public Async Function Delete(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await db.Products.FindAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			Return View(model)
		End Function

		<ActionName("Delete"), HttpPost, ValidateAntiForgeryToken>
		Public Async Function DeleteConfirmed(id As Guid, returnUrl As String) As Task(Of ActionResult)
			Dim product = Await db.Products.FindAsync(id)
			Dim image = Await db.Images.FindAsync(product.ImageId)
			If Not IsNothing(image) Then
				db.Images.Remove(image)
			End If
			db.Products.Remove(product)
			Await db.SaveChangesAsync
			TempData("Message") = "Продукт удален."
			Return RedirectToLocal(returnUrl)
		End Function

		<HttpPost>
		Public Async Function DeleteChecked(id As Guid()) As Task(Of ActionResult)
			Await db.Products.Where(Function(x) id.Contains(x.Id)).ForEachAsync(Sub(x As Product) If db.Images.Find(x.ImageId) IsNot Nothing Then db.Images.Remove(db.Images.Find(x.ImageId)))
			Await db.Products.Where(Function(x) id.Contains(x.Id)).ForEachAsync(Sub(x As Product) db.Entry(x).State = EntityState.Deleted)
			Await db.SaveChangesAsync
			TempData("Message") = String.Format("Удалено продуктов: {0}", id.Length)
			Return Json(New With {.redirect = Url.Action("index")})
		End Function

		''' <summary>
		''' Применяет фильтр и сортировку к перечислению продуктов.
		''' </summary>
		''' <param name="products">Перечисление продуктов, к которому применяется фильтр.</param>
		''' <param name="filter">Фильтр.</param>
		''' <returns>Отсортированное перечисление продуктов после применения фильтра.</returns>
		Private Function ApplyFilter(Of T As Product)(products As IQueryable(Of T), filter As ProductFilterViewModel) As IOrderedQueryable(Of T)

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchString) Then
				Dim searchString = filter.SearchString.Trim.ToLower.Replace("ё", "е")
				products = products.Where(Function(x) x.Name.ToLower.Replace("ё", "е").Contains(searchString) Or x.Sku.Contains(searchString))
			End If

			' Категория.
			If Not IsNothing(filter.CategoryId) Then
				products = products.Where(Function(x) x.CategoryId = filter.CategoryId)
			End If

			' Бренд.
			If Not IsNothing(filter.BrandId) Then
				products = products.Where(Function(x) x.BrandId = filter.BrandId)
			End If

			' Склад.
			If Not IsNothing(filter.WarehouseId) Then
				products = products.Where(Function(x) x.WarehouseId = filter.WarehouseId)
			End If

			' Сортировка (по умолчанию по дате изменения).
			products = products.OrderByDescending(Function(x) x.LastUpdateDate)

			Return products
		End Function

		''' <summary>
		''' Добавляет загруженный файл изображения в модель данных.
		''' </summary>
		''' <param name="model">Модель данных</param>
		''' <param name="imageFile">Загруженный файл изображения.</param>
		''' <exception cref="ArgumentNullException"></exception>
		Private Async Function AddImageAsync(model As Product, imageFile As HttpPostedFileWrapper) As Task(Of Integer)
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
				.Small = ImageUtility.Generate(imageFile.InputStream, imageFile.ContentType, 320, 320, StretchMode.UniformToFill)
				.Medium = ImageUtility.Generate(imageFile.InputStream, imageFile.ContentType, 540, 540, StretchMode.UniformToFill)
				.Large = ImageUtility.Generate(imageFile.InputStream, imageFile.ContentType, 960, 960, StretchMode.UniformToFill)
			End With

			Return 1
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