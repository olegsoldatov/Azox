Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

''' <summary>
''' Модель данных товара.
''' </summary>
Public Class Product
    Inherits PictorialEntity
    Implements IProduct

	<Required(ErrorMessage:="Укажите название.")>
	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Название")>
	Public Property Title As String Implements IProduct.Title

	<AllowHtml>
	<DataType(DataType.MultilineText)>
	<Display(Name:="Содержание")>
	<UIHint("Content")>
	Public Property Content As String Implements IProduct.Content

    <Display(Name:="Порядок")>
	Public Property Order As Integer?

	<Display(Name:="Черновик")>
	Public Property Draft As Boolean

	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Артикул")>
	Public Property Sku As String Implements IProduct.Sku

	<Display(Name:="Популярный товар")>
	Public Property IsPopular As Boolean Implements IProduct.IsPopular

	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<HiddenInput(DisplayValue:=True)>
	Public Property CategoryName As String Implements IProduct.CategoryName

	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<HiddenInput(DisplayValue:=True)>
	Public Property BrandName As String Implements IProduct.BrandName

	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Модель")>
	Public Property ModelName As String Implements IProduct.ModelName

	<Display(Name:="Цена")>
	Public Overridable Property Offers As ICollection(Of Offer)

	<Display(Name:="Характеристики")>
	Public Overridable Property Parameters As ICollection(Of Parameter)

	<Display(Name:="Категория")>
	Public Overridable Property Category As Category

	<Display(Name:="Категория")>
	Public Overridable Property CategoryId As Guid?

	<Display(Name:="Бренд")>
	Public Overridable Property Brand As Brand

	<Display(Name:="Бренд")>
	Public Overridable Property BrandId As Guid? Implements IProduct.BrandId

	<DataType(DataType.MultilineText)>
	<Display(Name:="Описание")>
	Public Property Description As String

	<Display(Name:="Ключевые слова")>
	Public Property Keywords As String

	<Display(Name:="Поставщик")>
	Public Property Vendor As String Implements IProduct.Vendor

	<Display(Name:="Цена")>
	Public Property Price As Decimal Implements IProduct.Price

	<Display(Name:="Количество")>
	Public Property Quantity As Integer Implements IProduct.Quantity

	<Display(Name:="Распродажа")>
	Public Property IsSale As Boolean Implements IProduct.IsSale
End Class

Partial Public Class ApplicationDbContext
	Public Property Products As DbSet(Of Product)
End Class
