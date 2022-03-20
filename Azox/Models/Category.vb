Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity
Imports Soldata.Azox

''' <summary>
''' Предоставляет модель данных категории.
''' </summary>
Public Class Category
	Implements IPage, IImageable

	<Key>
	Public Property Id As Guid Implements IPage.Id

	<Required(ErrorMessage:="Укажите имя.")>
	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Название")>
	Public Property Title As String Implements IPage.Title

	<AllowHtml>
	<DataType(DataType.MultilineText)>
	<Display(Name:="Содержание")>
	<UIHint("Content")>
	Public Property Content As String Implements IPage.Content

	<Display(Name:="Изображение")>
	Public Property ImageId As Guid? Implements IImageable.ImageId

	<Display(Name:="Порядок")>
	Public Property Order As Integer?

	<Display(Name:="Черновик")>
	Public Property Draft As Boolean

	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<RegularExpression("^[-\w]+$", ErrorMessage:="Используются недопустимые символы.")>
	<Remote("Exists", "Categories", "Admin", AdditionalFields:="Id", ErrorMessage:="Такое имя уже существует.")>
	<Display(Name:="Имя")>
	Public Property Name As String

	<Display(Name:="Тип / категория товара", Description:="Формирует название товара.")>
	Public Property TypePrefix As String

	<Display(Name:="Родительская категория")>
	Public Overridable Property Parent As Category

	<Display(Name:="Родительская категория")>
	Public Overridable Property ParentId As Guid?

	<Display(Name:="Дочерние категории")>
	Public Overridable Property Childs As ICollection(Of Category)

	<Display(Name:="Товары")>
	Public Overridable Property Products As ICollection(Of Product)

	' Линейный путь навигации, состоящий из уникальных идентификаторов родительской и дочерних категорий, разделенных косой чертой.
	<HiddenInput(DisplayValue:=False)>
	Public Property Path As String

	<ScaffoldColumn(False)>
	Public Property LastUpdateDate As Date Implements IEntity.LastUpdateDate

	<DataType(DataType.MultilineText)>
	<Display(Name:="Описание")>
	Public Property Description As String Implements IPage.Description

	<Display(Name:="Ключевые слова")>
	Public Property Keywords As String Implements IPage.Keywords

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
