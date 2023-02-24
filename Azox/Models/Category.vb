Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

''' <summary>
''' Модель данных категории.
''' </summary>
Public Class Category
	Inherits PictorialEntity
	Implements IDatedEntity

	<Required(ErrorMessage:="Укажите имя.")>
	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Название")>
	Public Property Title As String

	<AllowHtml>
	<DataType(DataType.MultilineText)>
	<Display(Name:="Содержание")>
	<UIHint("Content")>
	Public Property Content As String

	<Display(Name:="Порядок")>
	Public Property Order As Integer?

	<Display(Name:="Черновик")>
	<UIHint("Draft")>
	Public Property Draft As Boolean

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

	<Display(Name:="Описание")>
	Public Property Description As String

	<Display(Name:="Ключевые слова")>
	Public Property Keywords As String

	<DataType(DataType.Date)>
	<Display(Name:="Дата изменения")>
	Public Property LastUpdateDate As Date = Date.Now Implements IDatedEntity.LastUpdateDate

	''' <summary>
	''' Возвращает путь категории.
	''' </summary>
	''' <param name="divider">Разделитель. Если не указывать, то в качестве разделителя будет косая черта отбитая пробелами.</param>
	Public Function GetPath(Optional divider As String = " / ") As String
		Dim titles As New List(Of String) From {Title}
		Dim parent = Me.Parent
CategoryParent:
		If Not IsNothing(parent) Then
			titles.Insert(0, parent.Title)
			parent = parent.Parent
			GoTo CategoryParent
		End If
		Return String.Join(divider, titles.ToArray)
	End Function
End Class

Partial Public Class ApplicationDbContext
	Public Property Categories As DbSet(Of Category)
End Class
