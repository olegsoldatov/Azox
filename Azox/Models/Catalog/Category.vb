Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity

Public Class Category
	<Key>
	Public Property Id As Guid = Guid.NewGuid

	<Required(ErrorMessage:="Укажите название."), Display(Name:="Название")>
	Public Property Name As String

	<Display(Name:="Ярлык")>
	Public Property Slug As String

	<Display(Name:="Описание")>
	Public Property Description As String

	<DataType(DataType.ImageUrl), Display(Name:="Изображение")>
	Public Property ImageId As Guid?

	<NotMapped, DataType(DataType.Upload), Display(Name:="Файл изображения")>
	Public Property ImageFile As HttpPostedFileWrapper

	<Required(ErrorMessage:="Укажите порядок."), Display(Name:="Порядок"), UIHint("Order")>
	Public Property Order As Integer

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
		Dim names As New List(Of String) From {Name}
		Dim parent = Me.Parent
CategoryParent:
		If Not IsNothing(parent) Then
			names.Insert(0, parent.Name)
			parent = parent.Parent
			GoTo CategoryParent
		End If
		Return String.Join(divider, names.ToArray)
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
	Public Property SearchString As String
End Class
