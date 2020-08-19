Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity

Public Class Product
	<Key>
	Public Property Id As Guid = Guid.NewGuid

	<DataType(DataType.Date)>
	<HiddenInput(DisplayValue:=False)>
	<Display(Name:="Дата добавления")>
	Public Property CreateDate As Date = Date.Now.Date

	<DataType(DataType.Date)>
	<ScaffoldColumn(False)>
	<Display(Name:="Дата изменения")>
	Public Property LastUpdateDate As Date = Date.Now.Date

	<Required(ErrorMessage:="Укажите название.")>
	<StringLength(255, ErrorMessage:="Длина строки не более {1} символов.")>
	<Display(Name:="Название")>
	Public Property Title As String

	<Required(ErrorMessage:="Укажите артикул.")>
	<StringLength(255, ErrorMessage:="Длина строки не более {1} символов.")>
	<Display(Name:="Артикул")>
	Public Property Sku As String

	<DataType(DataType.MultilineText)>
	<Display(Name:="Содержание")>
	<UIHint("Content")>
	<AllowHtml>
	Public Property Content As String

	<StringLength(255, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Производитель")>
	Public Property Vendor As String

	<Required(ErrorMessage:="Укажите цену.")>
	<DataType(DataType.Currency)>
	<Display(Name:="Цена")>
	Public Property Price As Decimal

	<DataType(DataType.Currency)>
	<Display(Name:="Старая цена")>
	Public Property OldPrice As Decimal?

	<Display(Name:="Наличие")>
	Public Property Availability As ProductAvailability

	<Required(ErrorMessage:="Укажите количество."), Display(Name:="Количество"), UIHint("Quantity")>
	Public Property AvailableQuantity As Integer

	<Display(Name:="Порядок"), UIHint("Order")>
	Public Property Order As Integer?

	<Display(Name:="Опубликовано"), UIHint("IsPublished")>
	Public Property IsPublished As Boolean

	<Display(Name:="Изображение")>
	Public Property ImageId As Guid?

	<NotMapped, DataType(DataType.Upload), Display(Name:="Файл изображения")>
	Public Property ImageFile As HttpPostedFileWrapper

	<DataType(DataType.ImageUrl), Display(Name:="URL изображения")>
	Public Property ImageUrl As String

	<Display(Name:="Категория")>
	Public Overridable Property Category As Category

	<Display(Name:="Категория")>
	Public Overridable Property CategoryId As Guid?

	<Obsolete("Убрать связь.")>
	<Display(Name:="Бренд")>
	Public Overridable Property Brand As Brand
	<Display(Name:="Бренд")>
	Public Overridable Property BrandId As Guid?

	<Display(Name:="Склад")>
	Public Overridable Property Warehouse As Warehouse

	<Display(Name:="Склад")>
	Public Overridable Property WarehouseId As Guid?

	<Display(Name:="Изображения")>
	Public Overridable Property Pictures As ICollection(Of Picture)
End Class

Public Class Picture
	<Key>
	Public Property Id As Guid = Guid.NewGuid

	<Required(ErrorMessage:="Укажите название."), Display(Name:="Название")>
	Public Property Name As String

	<Display(Name:="Изображение")>
	Public Property ImageId As Guid?

	<NotMapped, DataType(DataType.Upload), Display(Name:="Файл изображения")>
	Public Property ImageFile As HttpPostedFileWrapper

	<Required(ErrorMessage:="Укажите порядок."), Display(Name:="Порядок"), UIHint("Order")>
	Public Property Order As Integer

	<Display(Name:="Продукт")>
	Public Overridable Property Product As Product

	<Display(Name:="Продукт")>
	Public Overridable Property ProductId As Guid
End Class

Public Class Attribute
	<Key>
	Public Property Id As Guid = Guid.NewGuid

	<Required(ErrorMessage:="Укажите имя.")>
	<StringLength(128)>
	<Display(Name:="Имя")>
	Public Property Name As String

	<Display(Name:="Ед. изм.")>
	Public Property Unit As String

	<Required(ErrorMessage:="Укажите порядок.")>
	<Display(Name:="Порядок")>
	<UIHint("Order")>
	Public Property Order As Integer

	<Display(Name:="Значения")>
	Public Overridable Property Parameters As ICollection(Of Parameter)
End Class

Public Class Parameter
	<Key>
	Public Property Id As Guid = Guid.NewGuid

	<Display(Name:="Значение")>
	Public Property Value As String

	<Display(Name:="Товар")>
	Public Overridable Property Product As Product
	<Display(Name:="Товар")>
	Public Overridable Property ProductId As Guid

	<Display(Name:="Параметр")>
	Public Overridable Property Attribute As Attribute
	<Display(Name:="Параметр")>
	Public Overridable Property AttributeId As Guid
End Class

Public Enum ProductAvailability
	<Display(Name:="Нет в наличии")>
	OutOfStock
	<Display(Name:="В наличии")>
	InStock
	<Display(Name:="На заказ")>
	PreOrder
End Enum

Partial Public Class ApplicationDbContext
	Public Property Products As DbSet(Of Product)
	Public Property Pictures As DbSet(Of Picture)
	Public Property Attributes As DbSet(Of Attribute)
	Public Property Parameters As DbSet(Of Parameter)
End Class

Public Class ProductAdminViewModel
	Public Property Id As Guid
	<Display(Name:="Название")>
	Public Property Name As String
	<Display(Name:="Артикул")>
	Public Property Sku As String
	<DataType(DataType.Currency), Display(Name:="Цена")>
	Public Property Price As Decimal
	<DataType(DataType.Currency)>
	Public Property OldPrice As Decimal?
	<Display(Name:="Категория")>
	Public Property CategoryName As String
	<Display(Name:="Бренд")>
	Public Property BrandName As String
	<Display(Name:="Склад")>
	Public Property WarehouseName As String
	<UIHint("IsPublished")>
	Public Property IsPublished As Boolean
End Class

Public Class ProductFilterViewModel
	<Display(Name:="Поиск")>
	Public Property SearchText As String

	<Display(Name:="Бренд")>
	Public Property BrandId As Guid?

	<Display(Name:="Производитель")>
	Public Property Vendor As String

	<Display(Name:="Категория")>
	Public Property CategoryId As Guid?

	<Display(Name:="Склад")>
	Public Property WarehouseId As Guid?

	<Display(Name:="Наличие")>
	Public Property Availability As ProductAvailability?

	<Display(Name:="Сортировать по")>
	Public Property SortBy As ProductSortBy
End Class

Public Enum ProductSortBy
	<Display(Name:="Цене (по возрастанию)")>
	Price
	<Display(Name:="Цене (по убыванию)")>
	PriceDescending
End Enum
