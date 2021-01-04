Imports System.Data.Entity
Imports System.Threading.Tasks
Imports System.Web.Caching
Imports System.Runtime.Caching

Public Class CatalogManager
	Implements IDisposable

	Private disposedValue As Boolean
	Private Const cacheKey = "CatalogDocument"
	Public ReadOnly Property Context As ApplicationDbContext
	Public ReadOnly Property MarginService As MarginService
	Public ReadOnly Property ImageManager As New ImageManager

	Public Sub New()
		_Context = New ApplicationDbContext
		_MarginService = New MarginService(_Context)
	End Sub

	Public Shared Sub Load()
		MemoryCache.Default.Remove(cacheKey)

		Using CatalogManager As New CatalogManager
			CatalogManager.GetDocument()
		End Using
	End Sub

	''' <summary>
	''' Асинхронно возвращает документ, содежащий все данные каталога. При первом обращении к методу данные сохраняются в кэш.
	''' </summary>
	''' <returns>Экземпляр класса <see cref="CatalogDocument"/>.</returns>
	Public Async Function GetDocumentAsync() As Task(Of CatalogDocument)
		Dim document = TryCast(MemoryCache.Default.Get(cacheKey), CatalogDocument)

		If document Is Nothing Then
			Context.Configuration.ProxyCreationEnabled = False

			document = New CatalogDocument With {
				.CategoryList = Await GetCategoryListAsync(),
				.BrandList = Await GetBrandListAsync()
			}

			MemoryCache.Default.Add(cacheKey, document, Now.AddMinutes(1740))

			Context.Configuration.ProxyCreationEnabled = True
		End If

		Return document
	End Function

	''' <summary>
	''' Возвращает документ, содежащий все данные каталога. При первом обращении к методу данные сохраняются в кэш.
	''' </summary>
	''' <returns>Экземпляр класса <see cref="CatalogDocument"/>.</returns>
	Public Function GetDocument() As CatalogDocument
		Return Task.Run(Function() GetDocumentAsync()).Result
	End Function

#Region "Products"

	Public ReadOnly Property Products As IQueryable(Of Product)
		Get
			Return Context.Products
		End Get
	End Property

#End Region

#Region "Categories"

	Public ReadOnly Property Categories As IQueryable(Of Category)
		Get
			Return Context.Categories
		End Get
	End Property

	Public Async Function GetCategoryListAsync() As Task(Of IReadOnlyList(Of CategoryListItem))
		Return Await Context.Categories.AsNoTracking _
			.Where(Function(x) Not x.Draft) _
			.Select(Function(x) New CategoryListItem With {
				.Id = x.Id,
				.Title = x.Title,
				.Name = x.Name,
				.Path = x.Path,
				.ParentId = x.ParentId,
				.ImageId = x.ImageId,
				.Order = x.Order}) _
			.ToListAsync
	End Function

	Public Function GetCategoryList() As IReadOnlyList(Of CategoryListItem)
		Return Task.Run(Function() GetCategoryListAsync()).Result
	End Function

	Public Function GetCategoryPath(path As String, Optional separator As String = " / ") As String
		Dim paths = path.Split("/").Select(Function(x) Guid.Parse(x))

		Return String.Join(separator, Context.Categories.AsNoTracking _
			.Join(paths, Function(x) x.Id, Function(y) y, Function(x, y) x.Title) _
			.ToList)
	End Function

	Public Async Function CategoryNameExistsAsync(name As String, Optional id As Guid? = Nothing) As Task(Of Boolean)
		If String.IsNullOrEmpty(name) Then
			Return False
		End If
		Return Await Context.Categories.AsNoTracking.AnyAsync(Function(x) x.Name = name And Not x.Id = id)
	End Function

	Public Async Function FindCategoryByIdAsync(id As Guid) As Task(Of Category)
		Return Await Context.Categories.FindAsync(id)
	End Function

	Public Async Function AddCategoryAsync(category As Category) As Task
		If IsNothing(category) Then
			Throw New ArgumentNullException(NameOf(category))
		End If
		category.Id = Guid.NewGuid
		category.Path = Await GetNewPathAsync(category)
		Context.Categories.Add(category)
		Await Context.SaveChangesAsync
	End Function

	Public Async Function UpdateCategoryAsync(category As Category) As Task
		If IsNothing(category) Then
			Throw New ArgumentNullException(NameOf(category))
		End If
		category.Path = Await GetNewPathAsync(category)
		Context.Entry(category).State = EntityState.Modified
		Await Context.SaveChangesAsync
	End Function

	Private Async Function GetNewPathAsync(category As Category) As Task(Of String)
		Dim newPath = If(category.ParentId Is Nothing, category.Id.ToString, String.Join("/", (Await Context.Categories.SingleAsync(Function(x) x.Id = category.ParentId)).Path, category.Id.ToString))
		Await Context.Categories _
					.Where(Function(x) Not x.Id = category.Id And x.Path.Contains(category.Path)) _
					.ForEachAsync(Sub(c As Category)
									  c.Path = c.Path.Replace(category.Path, newPath)
								  End Sub)
		Return newPath
	End Function

#End Region

#Region "Brands"

	Public ReadOnly Property Brands As IQueryable(Of Brand)
		Get
			Return Context.Brands
		End Get
	End Property

	Public Async Function FindBrandAsync(id As Guid?) As Task(Of Brand)
		Return Await Context.Brands.FindAsync(id)
	End Function

	Public Async Function GetBrandAsync(name As String) As Task(Of Brand)
		Return Await Context.Brands.AsNoTracking.FirstOrDefaultAsync(Function(x) x.Name = name)
	End Function

	Public Async Function CreateBrandAsync(brand As Brand) As Task(Of Brand)
		If IsNothing(brand) Then
			Throw New ArgumentNullException(NameOf(brand))
		End If
		brand.Id = Guid.NewGuid
		Context.Brands.Add(brand)
		Await Context.SaveChangesAsync
		Return brand
	End Function

	Public Async Function UpdateBrandAsync(brand As Brand) As Task(Of Brand)
		If IsNothing(brand) Then
			Throw New ArgumentNullException(NameOf(brand))
		End If
		Context.Entry(brand).State = EntityState.Modified
		Await Context.SaveChangesAsync
		Return brand
	End Function

	Public Async Function DeleteBrandAsync(brand As Brand) As Task
		If IsNothing(brand) Then
			Throw New ArgumentNullException(NameOf(brand))
		End If
		Await Context.Products.Where(Function(x) x.BrandId = brand.Id).ForEachAsync(Sub(p) p.BrandId = Nothing)
		Dim image = Await ImageManager.FindByIdAsync(brand.ImageId.GetValueOrDefault)
		If image IsNot Nothing Then
			Await ImageManager.DeleteAsync(image)
		End If
		Context.Brands.Remove(brand)
		Await Context.SaveChangesAsync
	End Function

	Public Async Function DeleteBrandRangeAsync(brands As IEnumerable(Of Brand)) As Task
		If IsNothing(brands) Then
			Throw New ArgumentNullException(NameOf(brands))
		End If
		brands.Join(Context.Products, Function(x) x.Id, Function(y) y.BrandId, Function(x, y) y).ToList.ForEach(Sub(p) p.BrandId = Nothing)
		Dim images = brands.Join(ImageManager.Images, Function(x) x.ImageId, Function(y) y.Id, Function(x, y) y).ToList
		Await ImageManager.DeleteRangeAsync(images)
		Context.Brands.RemoveRange(brands)
		Await Context.SaveChangesAsync
	End Function

	Public Async Function GetBrandListAsync() As Task(Of IReadOnlyList(Of BrandListItem))
		Return Await Context.Brands.AsNoTracking _
			.Where(Function(x) Not x.Draft) _
			.Select(Function(x) New BrandListItem With {
				.Id = x.Id,
				.Name = x.Name,
				.Title = x.Title,
				.ImageId = x.ImageId}) _
			.ToListAsync
	End Function

	Public Function GetBrandList() As IReadOnlyList(Of BrandListItem)
		Return Task.Run(Function() GetBrandListAsync()).Result
	End Function

	Public Async Function ExistsBrandAsync(brand As Brand, Optional id As Guid? = Nothing) As Task(Of Boolean)
		If IsNothing(brand) Then
			Throw New ArgumentNullException(NameOf(brand))
		End If
		Return Await Context.Brands.AsNoTracking.AnyAsync(Function(x) x.Name = brand.Name And Not x.Id = id)
	End Function

#End Region

	Public Async Function GetWarehousesAsync() As Task(Of IReadOnlyCollection(Of Catalog.Models.Warehouse))
		Const key As String = "Warehouses"
		Dim warehouses = TryCast(GetCacheItem(key), IReadOnlyCollection(Of Catalog.Models.Warehouse))
		If warehouses Is Nothing Then
			warehouses = Await Context.Warehouses _
				.Where(Function(x) x.IsPublished) _
				.Select(Function(x) New Catalog.Models.Warehouse With {.Id = x.Id, .Name = x.Name, .Title = x.Title}) _
				.ToListAsync

			AddCacheItem(key, warehouses)
		End If
		Return warehouses
	End Function

	Public Function GetWarehouses() As IReadOnlyCollection(Of Catalog.Models.Warehouse)
		Return Task.Run(Function() GetWarehousesAsync()).Result
	End Function

	Public Async Function GetOffersAsync() As Task(Of IReadOnlyCollection(Of Catalog.Models.Offer))
		Const key As String = "Offers"
		Dim offers = TryCast(GetCacheItem(key), IReadOnlyCollection(Of Catalog.Models.Offer))

		If offers Is Nothing Then
			Dim entities = Await Context.Offers _
				.AsNoTracking _
				.Include(Function(x) x.Product) _
				.Where(Function(x) x.Warehouse.IsPublished) _
				.Select(Function(x) New With {x.Price, x.OldPrice, x.ProductId, x.LastUpdateDate, x.WarehouseId, x.Product.CategoryId}) _
				.ToListAsync

			offers = entities _
				.Select(Function(x) New Catalog.Models.Offer With {
					.Price = MarginService.MarginPrice(x.Price, New MarginOptions With {.WarehouseId = x.WarehouseId, .CategoryId = x.CategoryId}),
					.OldPrice = MarginService.MarginPrice(x.OldPrice, New MarginOptions With {.WarehouseId = x.WarehouseId, .CategoryId = x.CategoryId}),
					.ProductId = x.ProductId,
					.LastUpdateDate = x.LastUpdateDate}) _
				.ToList

			AddCacheItem(key, offers)
		End If
		Return offers
	End Function

	Public Function GetOffers() As IReadOnlyCollection(Of Catalog.Models.Offer)
		Return Task.Run(Function() GetOffersAsync()).Result
	End Function

	Public Async Function GetProductsAsync() As Task(Of IReadOnlyCollection(Of Catalog.Models.Product))
		Const key As String = "Products"
		Dim products = TryCast(GetCacheItem(key), IReadOnlyCollection(Of Catalog.Models.Product))
		If products Is Nothing Then
			products = Await Context.Products _
				.Where(Function(x) x.IsPublished) _
				.Select(Function(x) New Catalog.Models.Product With {.Id = x.Id, .Sku = x.Sku, .Title = x.Title, .Brand = x.BrandName, .Model = x.ModelName, .CategoryId = x.CategoryId, .CreateDate = x.CreateDate, .LastUpdateDate = x.LastUpdateDate}) _
				.AsNoTracking _
				.ToListAsync

			AddCacheItem(key, products)
		End If
		Return products
	End Function

	Public Function GetProducts() As IReadOnlyCollection(Of Catalog.Models.Product)
		Return Task.Run(Function() GetProductsAsync()).Result
	End Function

	Public Async Function GetAttributesAsync() As Task(Of IReadOnlyCollection(Of Catalog.Models.Attribute))
		Const key As String = "Attributes"
		Dim attributes = TryCast(GetCacheItem(key), IReadOnlyCollection(Of Catalog.Models.Attribute))
		If attributes Is Nothing Then
			attributes = Await Context.Attributes _
				.AsNoTracking _
				.Select(Function(x) New Catalog.Models.Attribute With {.Id = x.Id, .Name = x.Name, .Order = x.Order}) _
				.ToListAsync

			AddCacheItem(key, attributes)
		End If
		Return attributes
	End Function

	Public Function GetAttributes() As IReadOnlyCollection(Of Catalog.Models.Attribute)
		Return Task.Run(Function() GetAttributesAsync()).Result
	End Function

	Public Async Function GetParametersAsync() As Task(Of IReadOnlyCollection(Of Catalog.Models.Parameter))
		Const key As String = "Parameters"
		Dim parameters = TryCast(GetCacheItem(key), IReadOnlyCollection(Of Catalog.Models.Parameter))
		If parameters Is Nothing Then
			parameters = Await Context.Parameters _
				.AsNoTracking _
				.Select(Function(x) New Catalog.Models.Parameter With {.Value = x.Value, .AttributeId = x.AttributeId, .ProductId = x.ProductId}) _
				.ToListAsync

			AddCacheItem(key, parameters)
		End If
		Return parameters
	End Function

	Public Function GetParameters() As IReadOnlyCollection(Of Catalog.Models.Parameter)
		Return Task.Run(Function() GetParametersAsync()).Result
	End Function

	Public Async Function GetPicturesAsync() As Task(Of IReadOnlyCollection(Of Catalog.Models.Picture))
		Const key As String = "Pictures"
		Dim pictures = TryCast(GetCacheItem(key), IReadOnlyCollection(Of Catalog.Models.Picture))
		If pictures Is Nothing Then
			pictures = Await Context.Pictures _
				.AsNoTracking _
				.Select(Function(x) New Catalog.Models.Picture With {.ImageUrl = x.ImageUrl, .Order = x.Order, .ProductId = x.ProductId}) _
				.ToListAsync

			AddCacheItem(key, pictures)
		End If
		Return pictures
	End Function

	Public Function GetPictures() As IReadOnlyCollection(Of Catalog.Models.Picture)
		Return Task.Run(Function() GetPicturesAsync()).Result
	End Function

	Private Function GetCacheItem(key As String) As Object
		Return HttpRuntime.Cache(key)
	End Function

	Private Sub AddCacheItem(key As String, value As Object)
		HttpRuntime.Cache.Add(key, value, Nothing, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, Runtime.Caching.CacheItemPriority.Default, Nothing)
	End Sub

#Region "IDisposable Support"

	Protected Overridable Sub Dispose(disposing As Boolean)
		If Not disposedValue Then
			If disposing Then
				ImageManager.Dispose()
				_Context.Dispose()
			End If
		End If
		disposedValue = True
	End Sub

	Public Sub Dispose() Implements IDisposable.Dispose
		Dispose(True)
	End Sub
#End Region
End Class
