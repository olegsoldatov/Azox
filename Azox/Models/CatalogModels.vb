Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity

Public Class Category
	Implements ICategory

	<Key>
	<HiddenInput(DisplayValue:=False)>
	Public Property Id As Guid Implements ICategory.Id

	<Required(ErrorMessage:="Укажите ярлык.")>
	<StringLength(128, ErrorMessage:="Не более {1} символов.")>
	<RegularExpression("^[a-zA-Z0-9-_.~/]+$", ErrorMessage:="Допускаются только латинские буквы, цифры, символы -_.~/ и без пробелов.")>
	<Display(Name:="Имя")>
	Public Property Name As String Implements ICategory.Name

	<Required(ErrorMessage:="Укажите название.")>
	<StringLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Название")>
	Public Property Title As String Implements ICategory.Title

	<HiddenInput(DisplayValue:=False)>
	Public Property Path As String Implements ICategory.Path

	<DataType(DataType.ImageUrl)>
	<Display(Name:="Изображение")>
	Public Property ImageId As Guid? Implements ICategory.ImageId

	<Display(Name:="Порядок")>
	<UIHint("Order")>
	Public Property Order As Integer? Implements ICategory.Order

	<Display(Name:="Черновик")>
	<UIHint("Draft")>
	Public Property Draft As Boolean

	<Display(Name:="Родительская категория")>
	Public Overridable Property ParentId As Guid? Implements ICategory.ParentId

	<Display(Name:="Родительская категория")>
	Public Overridable Property Parent As Category

	<Display(Name:="Товары")>
	Public Overridable Property Products As ICollection(Of Product)

	Public Overridable Property Childs As ICollection(Of Category)






	<StringLength(255, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Заголовок")>
	Public Property Heading As String

	<DataType(DataType.MultilineText)>
	<Display(Name:="Содержание")>
	<UIHint("Content")>
	<AllowHtml>
	Public Property Content As String

	<StringLength(1000, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Описание")>
	Public Property Description As String

	'<Obsolete>
	'<Display(Name:="Ярлык")>
	'Public Property Slug As String

	<NotMapped>
	<DataType(DataType.Upload)>
	<Display(Name:="Файл изображения")>
	Public Property ImageFile As HttpPostedFileWrapper

	''' <summary>
	''' Возвращает путь категории.
	''' </summary>
	''' <param name="divider">Разделитель. Если не указывать, то в качестве разделителя будет косая черта отбитая пробелами.</param>
	Public Function GetPath(Optional divider As String = " / ") As String
		Dim titles As New List(Of String) From {If(String.IsNullOrEmpty(Title), Name, Title)}
		Dim parent = Me.Parent
CategoryParent:
		If Not IsNothing(parent) Then
			titles.Insert(0, If(String.IsNullOrEmpty(parent.Title), parent.Name, parent.Title))
			parent = parent.Parent
			GoTo CategoryParent
		End If
		Return String.Join(divider, titles.ToArray)
	End Function
End Class

Public Class CategoryFilterViewModel
	<Display(Name:="Поиск")>
	Public Property SearchText As String
End Class

Partial Public Class ApplicationDbContext
	Public Property Categories As DbSet(Of Category)
End Class

Namespace Catalog.Models
	Public Class Warehouse
		Public Property Id As Guid
		Public Property Name As String
		Public Property Title As String
	End Class

	Public Class Product
		Public Property Id As Guid
		Public Property Sku As String
		Public Property Title As String
		Public Property Brand As String
		Public Property Model As String
		Public Property CategoryId As Guid?
		Public Property CreateDate As Date
		Public Property LastUpdateDate As Date
	End Class

	Public Class Offer
		Public Property Price As Decimal
		Public Property OldPrice As Decimal?
		Public Property ProductId As Guid
		Public Property LastUpdateDate As Date
	End Class

	Public Class Attribute
		Public Property Id As Guid
		Public Property Name As String
		Public Property Order As Integer
	End Class

	Public Class Parameter
		Public Property Value As String
		Public Property AttributeId As Guid
		Public Property ProductId As Guid
	End Class

	Public Class Picture
		Public Property ImageUrl As String
		Public Property Order As Integer
		Public Property ProductId As Guid
	End Class
End Namespace
