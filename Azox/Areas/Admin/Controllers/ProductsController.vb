Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks
Imports Soldata.Web.Extensions

Namespace Areas.Admin.Controllers
	Public Class ProductsController
		Inherits AdminController

        Public ReadOnly Property ProductManager As ProductManager(Of Product)
        Public ReadOnly Property BrandManager As BrandManager
		Public ReadOnly Property CategoryManager As CategoryManager

		Public Sub New(productManager As ProductManager(Of Product), brandManager As BrandManager, categoryManager As CategoryManager)
			Me.ProductManager = productManager
			Me.BrandManager = brandManager
			Me.CategoryManager = categoryManager
		End Sub

		Public Async Function Index(filter As ProductFilter, Optional pageIndex As Integer = 0, Optional pageSize As Integer = 50) As Task(Of ActionResult)
			Dim entities = ProductManager.Products.Include(Function(x) x.Brand).Include(Function(x) x.Category).Include(Function(x) x.Offers)

			' Поиск.
			If Not String.IsNullOrEmpty(filter.SearchText) Then
				Dim s = filter.SearchText.ToLower.Replace("ё", "е")
				entities = entities.Where(Function(x) x.Title.ToLower.Replace("ё", "е").Contains(s) Or x.Sku.ToLower.Contains(s) Or x.BrandName.ToLower.Contains(s) Or x.CategoryName.ToLower.Contains(s) Or x.ModelName.ToLower.Contains(s) Or x.Vendor.ToLower.Contains(s))
			End If

			' Производитель.
			If Not IsNothing(filter.BrandId) Then
				entities = entities.Where(Function(x) x.BrandId = filter.BrandId)
			End If

			' Категория.
			If Not IsNothing(filter.CategoryId) Then
				entities = entities.Where(Function(x) x.CategoryId = filter.CategoryId)
			End If

			' Фильтр.
			ViewBag.BrandId = New SelectList(BrandManager.Brands.OrderBy(Function(x) x.Title), "Id", "Title", filter.BrandId)
			ViewBag.CategoryId = New SelectList(CategoryManager.Categories.OrderBy(Function(x) x.Title), "Id", "Title", filter.CategoryId)
			ViewBag.Filter = filter

			' Сортировка.
			entities = entities.OrderByDescending(Function(x) x.LastUpdateDate).ThenBy(Function(x) x.Title)

			Pagination(Await entities.AsNoTracking.CountAsync, pageIndex, pageSize)

			Return View(Await entities.Skip(pageIndex * pageSize).Take(pageSize).AsNoTracking.ToListAsync)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Index(id As Guid(), returnUrl As String, model As ProductChange, Optional delete As Boolean = False, Optional change As Boolean = False) As Task(Of ActionResult)
			If Not IsNothing(id) Then
				Dim tyres = Await ProductManager.Products.Where(Function(x) id.Contains(x.Id)).ToListAsync

				If change And (Not IsNothing(model.CategoryId) Or Not IsNothing(model.BrandId)) Then

					If Not IsNothing(model.BrandId) Then
						tyres.ForEach(Sub(x) x.BrandId = model.BrandId)
					End If

					If Not IsNothing(model.CategoryId) Then
						tyres.ForEach(Sub(x) x.CategoryId = model.CategoryId)
					End If

					Await ProductManager.UpdateRangeAsync(tyres)
					Alert(String.Format("Изменено: {0}.", tyres.Count.ToString("товар", "товара", "товаров")))
				ElseIf delete Then
					Await ProductManager.DeleteRangeAsync(tyres)
					Alert(String.Format("Удалено: {0}.", tyres.Count.ToString("товар", "товара", "товаров")))
				End If
			End If
			Return Redirect(returnUrl)
		End Function

		<HttpGet>
		Public Function Create() As ActionResult
			ViewBag.BrandId = New SelectList(BrandManager.Brands.OrderBy(Function(x) x.Title), "Id", "Title")
			ViewBag.CategoryId = New SelectList(CategoryManager.Categories.OrderBy(Function(x) x.Title), "Id", "Title")
			Return View()
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Create(product As Product, imageFile As HttpPostedFileWrapper) As Task(Of ActionResult)
			If Not IsNothing(imageFile) AndAlso Not imageFile.ContentType.Contains("image") Then
				ModelState.AddModelError(NameOf(Brand.ImageId), My.Resources.FileIsNotImage)
			End If
			If ModelState.IsValid Then
				Await ProductManager.UploadImageAsync(product, (imageFile?.InputStream, imageFile?.ContentType))
				Await ProductManager.CreateAsync(product)
				Alert("Товар добавлен.")
				Return RedirectToAction("index")
			End If
			ViewBag.BrandId = New SelectList(BrandManager.Brands.OrderBy(Function(x) x.Title), "Id", "Title", product.BrandId)
			ViewBag.CategoryId = New SelectList(CategoryManager.Categories.OrderBy(Function(x) x.Title), "Id", "Title", product.CategoryId)
			Return View(product)
		End Function

		Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim product = Await ProductManager.FindByIdAsync(id)
			If IsNothing(product) Then
				Return HttpNotFound()
			End If
			ViewBag.BrandId = New SelectList(BrandManager.Brands.OrderBy(Function(x) x.Title), "Id", "Title", product.BrandId)
			ViewBag.CategoryId = New SelectList(CategoryManager.Categories.OrderBy(Function(x) x.Title), "Id", "Title", product.CategoryId)
			Return View(product)
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Async Function Edit(product As Product, imageFile As HttpPostedFileWrapper, returnUrl As String) As Task(Of ActionResult)
			If Not IsNothing(imageFile) AndAlso Not imageFile.ContentType.Contains("image") Then
				ModelState.AddModelError(NameOf(Brand.ImageId), My.Resources.FileIsNotImage)
			End If
			If ModelState.IsValid Then
				Await ProductManager.UploadImageAsync(product, (imageFile?.InputStream, imageFile?.ContentType))
				Await ProductManager.UpdateAsync(product)
				Alert("Товар изменен.")
				Return RedirectToReturnUrl(returnUrl)
			End If
			ViewBag.BrandId = New SelectList(BrandManager.Brands.OrderBy(Function(x) x.Title), "Id", "Title", product.BrandId)
			ViewBag.CategoryId = New SelectList(CategoryManager.Categories.OrderBy(Function(x) x.Title), "Id", "Title", product.CategoryId)
			Return View(product)
		End Function

		Public Async Function Delete(id As Guid?) As Task(Of ActionResult)
			If IsNothing(id) Then
				Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
			End If
			Dim product = Await ProductManager.FindByIdAsync(id)
			If IsNothing(product) Then
				Return HttpNotFound()
			End If
			Return View(product)
		End Function

		<HttpPost>
		<ActionName("Delete")>
		<ValidateAntiForgeryToken>
		Public Async Function DeleteConfirmed(id As Guid, returnUrl As String) As Task(Of ActionResult)
			Dim product = Await ProductManager.FindByIdAsync(id)
			Await ProductManager.DeleteAsync(product)
			Alert("Товар удален.")
			Return RedirectToReturnUrl(returnUrl)
		End Function
	End Class
End Namespace