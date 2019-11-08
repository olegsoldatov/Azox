Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks
Imports System.Web.Configuration
Imports Soldata.Imaging

Namespace Controllers
	<Authorize>
	Public Class ProductsController
		Inherits Controller

		Private db As New ApplicationDbContext

		<AllowAnonymous>
		Async Function Index() As Task(Of ActionResult)
			ViewBag.Categories = Await db.Categories.ToListAsync
			Return View()
		End Function

		<AllowAnonymous, ChildActionOnly>
		Function List(filter As ProductFilterViewModel, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 12) As ActionResult
			Dim products = db.Products.Where(Function(p) p.IsPublished)

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchString) Then
				Dim searchString = filter.SearchString.Trim
				products = products.Where(Function(x) x.Name.Contains(searchString))
			End If

			' Категория.
			If Not IsNothing(filter.CategoryId) Then
				products = products.Where(Function(x) x.CategoryId = filter.CategoryId)
			End If

			' Бренд.
			If Not IsNothing(filter.BrandId) Then
				products = products.Where(Function(x) x.BrandId = filter.BrandId)
			End If

			' Фильтр.
			ViewBag.Filter = filter

			' Сортировка.
			products = products.OrderBy(Function(x) x.Order)

			' Пагинация.
			ViewBag.PageIndex = pageIndex
			ViewBag.PageCount = CInt(Math.Ceiling(products.Count / pageSize))

			ViewBag.Margins = db.Margins.ToList
			Return PartialView(products.Skip(pageIndex * pageSize).Take(pageSize).ToList)
		End Function

		<AllowAnonymous>
		Async Function Details(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await db.Products.FindAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			ViewBag.Categories = Await db.Categories.Where(Function(x) x.Parent Is Nothing).ToListAsync
			ViewBag.Margins = Await db.Margins.ToListAsync
			ViewBag.EditUrl = Url.Action("edit", "products", New With {model.Id, .returnUrl = Request.Url.AbsolutePath})
			Return View(model)
		End Function

		<AllowAnonymous>
		Async Function Category(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await db.Categories.FindAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			ViewBag.EditUrl = Url.Action("edit", "categories", New With {model.Id, .returnUrl = Request.Url.AbsolutePath})
			Return View(model)
		End Function

		<AllowAnonymous>
		Async Function Brand(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await db.Brands.FindAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			ViewBag.EditUrl = Url.Action("edit", "brands", New With {model.Id, .returnUrl = Request.Url.AbsolutePath})
			Return View(model)
		End Function

		Async Function Manage(filter As ProductFilterViewModel, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 20) As Task(Of ActionResult)
			Dim entities = db.Products.AsQueryable

			' Категория.
			If Not IsNothing(filter.CategoryId) Then
				entities = entities.Where(Function(x) x.CategoryId = filter.CategoryId)
			End If

			' Бренд.
			If Not IsNothing(filter.BrandId) Then
				entities = entities.Where(Function(x) x.BrandId = filter.BrandId)
			End If

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchString) Then
				Dim searchString = filter.SearchString.Trim
				entities = entities.Where(Function(x) x.Name.Contains(searchString) Or x.Sku.Contains(searchString))
			End If

			' Фильтр.
			ViewBag.Filter = filter
			ViewBag.CategoryId = New SelectList(Await db.Categories.Select(Function(c) New With {.Value = c.Id, .Text = c.Name}).OrderBy(Function(a) a.Text).ToListAsync, "Value", "Text", filter.CategoryId)
			ViewBag.BrandId = New SelectList(Await db.Brands.Select(Function(c) New With {.Value = c.Id, .Text = c.Name}).OrderBy(Function(a) a.Text).ToListAsync, "Value", "Text", filter.BrandId)

			' Сортировка.
			entities = entities.OrderBy(Function(x) x.Order)

			' Пагинация.
			ViewBag.PageIndex = pageIndex
			ViewBag.PageCount = CInt(Math.Ceiling(Await entities.CountAsync / pageSize))

			Return View(Await entities.OrderBy(Function(x) x.Id).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync)
		End Function

		Function Create(brandId As Guid?, categoryLd As Guid?) As ActionResult
			ViewBag.BrandId = New SelectList(db.Brands.Select(Function(x) New With {x.Id, x.Name}).OrderBy(Function(x) x.Name), "Id", "Name", brandId)
			ViewBag.CategoryId = New SelectList(db.Categories.Select(Function(x) New With {x.Id, x.Name}).OrderBy(Function(x) x.Name), "Id", "Name", categoryLd)
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View()
		End Function

		<HttpPost, ValidateAntiForgeryToken>
		Async Function Create(model As Product, returnUrl As String) As Task(Of ActionResult)
			If ModelState.IsValid Then
				db.Products.Add(model)

				If model.ImageFile IsNot Nothing AndAlso model.ImageFile.ContentLength > 0 Then
					Dim image As New Image With {
						.ContentType = model.ImageFile.ContentType,
						.Original = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType),
						.Thumbnail = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType, 200, 200, StretchMode.UniformToFill),
						.Small = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType, 320, 320, StretchMode.UniformToFill),
						.Medium = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType, 540, 540, StretchMode.UniformToFill),
						.Large = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType, 960, 960, StretchMode.UniformToFill)
					}
					model.ImageId = image.Id
					db.Images.Add(image)
				End If

				Await db.SaveChangesAsync
				TempData("Message") = "Продукт добавлен."
				Return RedirectToLocal(returnUrl)
			End If
			ViewBag.BrandId = New SelectList(db.Brands.Select(Function(x) New With {x.Id, x.Name}).OrderBy(Function(x) x.Name), "Id", "Name", model.BrandId)
			ViewBag.CategoryId = New SelectList(db.Categories.Select(Function(x) New With {x.Id, x.Name}).OrderBy(Function(x) x.Name), "Id", "Name", model.CategoryId)
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View(model)
		End Function

		Async Function Edit(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim model = Await db.Products.FindAsync(id)
			If IsNothing(model) Then
				Return HttpNotFound()
			End If
			ViewBag.BrandId = New SelectList(db.Brands.Select(Function(x) New With {x.Id, x.Name}).OrderBy(Function(x) x.Name), "Id", "Name", model.BrandId)
			ViewBag.CategoryId = New SelectList(db.Categories.Select(Function(x) New With {x.Id, x.Name}).OrderBy(Function(x) x.Name), "Id", "Name", model.CategoryId)
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View(model)
		End Function

		<HttpPost, ValidateAntiForgeryToken>
		Async Function Edit(model As Product, returnUrl As String) As Task(Of ActionResult)
			If ModelState.IsValid Then
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
						.Small = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType, 320, 320, StretchMode.UniformToFill)
						.Medium = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType, 540, 540, StretchMode.UniformToFill)
						.Large = ImageUtility.Generate(model.ImageFile.InputStream, model.ImageFile.ContentType, 960, 960, StretchMode.UniformToFill)
					End With
				End If

				Await db.SaveChangesAsync
				TempData("Message") = "Продукт изменен."
				Return RedirectToLocal(returnUrl)
			End If
			ViewBag.BrandId = New SelectList(db.Brands.Select(Function(x) New With {x.Id, x.Name}).OrderBy(Function(x) x.Name), "Id", "Name", model.BrandId)
			ViewBag.CategoryId = New SelectList(db.Categories.Select(Function(x) New With {x.Id, x.Name}).OrderBy(Function(x) x.Name), "Id", "Name", model.CategoryId)
			ViewBag.Length = CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength
			Return View(model)
		End Function

		Async Function Delete(id As Guid?) As Task(Of ActionResult)
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
		Async Function DeleteConfirmed(id As Guid, returnUrl As String) As Task(Of ActionResult)
			Dim product = Await db.Products.FindAsync(id)
			Dim image = Await db.Images.FindAsync(product.ImageId) : If Not IsNothing(image) Then db.Images.Remove(image)
			db.Products.Remove(product)
			Await db.SaveChangesAsync
			TempData("Message") = "Продукт удален."
			Return RedirectToLocal(returnUrl)
		End Function

		Private Function RedirectToLocal(returnUrl As String) As ActionResult
			If Url.IsLocalUrl(returnUrl) Then
				Return Redirect(returnUrl)
			End If
			Return RedirectToAction("manage")
		End Function

		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				db.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace