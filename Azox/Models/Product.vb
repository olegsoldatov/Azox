Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class Product
	Implements IImageable

	<Key>
	Public Property Id As Guid

	<Required(ErrorMessage:="Укажите название.")>
	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Название")>
	Public Property Title As String

	<AllowHtml>
	<DataType(DataType.MultilineText)>
	<Display(Name:="Содержание")>
	<UIHint("Content")>
	Public Property Content As String

	<Display(Name:="Изображение")>
	Public Property ImageId As Guid? Implements IImageable.ImageId

	<Display(Name:="Порядок")>
	Public Property Order As Integer?

	<Display(Name:="Черновик")>
	Public Property Draft As Boolean

	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Артикул")>
	Public Property Sku As String

	<Display(Name:="Популярный товар")>
	Public Property Popular As Boolean

	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<HiddenInput(DisplayValue:=True)>
	Public Property CategoryName As String

	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<HiddenInput(DisplayValue:=True)>
	Public Property BrandName As String

	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Модель")>
	Public Property ModelName As String

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
	Public Overridable Property BrandId As Guid?

	<ScaffoldColumn(False)>
	Public Property LastUpdateDate As Date

	<DataType(DataType.MultilineText)>
	<Display(Name:="Описание")>
	Public Property Description As String

	<Display(Name:="Ключевые слова")>
	Public Property Keywords As String
End Class

Partial Public Class ApplicationDbContext
	Public Property Products As DbSet(Of Product)
End Class
