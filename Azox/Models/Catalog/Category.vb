Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity

Public Class Category
	<Key>
	Public Property Id As Guid = Guid.NewGuid

	<Required(ErrorMessage:="Укажите название.")>
	<StringLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Имя")>
	Public Property Name As String

	<Required(ErrorMessage:="Укажите название.")>
	<StringLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Название")>
	Public Property Title As String

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

	<Obsolete>
	<Display(Name:="Ярлык")>
	Public Property Slug As String

	<DataType(DataType.ImageUrl)>
	<Display(Name:="Изображение")>
	Public Property ImageId As Guid?

	<NotMapped>
	<DataType(DataType.Upload)>
	<Display(Name:="Файл изображения")>
	Public Property ImageFile As HttpPostedFileWrapper

	<Required(ErrorMessage:="Укажите порядок.")>
	<Display(Name:="Порядок")>
	<UIHint("Order")>
	Public Property Order As Integer

	<Display(Name:="Опубликовано")>
	<UIHint("IsPublished")>
	Public Property IsPublished As Boolean

	<Display(Name:="Продукция")>
	Public Overridable Property Products As ICollection(Of Product)

	<Display(Name:="Родительская категория")>
	Public Overridable Property Parent As Category

	<Display(Name:="Родительская категория")>
	Public Overridable Property ParentId As Guid?

	Public Overridable Property Childs As ICollection(Of Category)

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

Partial Public Class ApplicationDbContext
	Public Property Categories As DbSet(Of Category)
End Class

Public Class CategoryAdminViewModel
	Public Property Id As Guid
	<Display(Name:="Название")>
	Public Property Name As String
	Public Property Slug As String
	<Display(Name:="Продукты")>
	Public Property ProductCount As Integer
End Class

Public Class CategoryFilterViewModel
	<Display(Name:="Поиск")>
	Public Property SearchText As String
End Class
