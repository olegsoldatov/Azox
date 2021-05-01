Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class Brand
	Implements IImageable

	<Key>
	Public Property Id As Guid

	<Required(ErrorMessage:="Укажите название.")>
	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Название")>
	Public Property Title As String

	<DataType(DataType.MultilineText)>
	<AllowHtml>
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
	<RegularExpression("^[-\w]+$", ErrorMessage:="Используются недопустимые символы.")>
	<Display(Name:="Имя")>
	Public Property Name As String

	<Display(Name:="Продукция")>
	Public Overridable Property Products As ICollection(Of Product)

	<ScaffoldColumn(False)>
	Public Property LastUpdateDate As Date

	<DataType(DataType.MultilineText)>
	<Display(Name:="Описание")>
	Public Property Description As String

	<Display(Name:="Ключевые слова")>
	Public Property Keywords As String
End Class

Partial Public Class ApplicationDbContext
	Public Property Brands As DbSet(Of Brand)
End Class
