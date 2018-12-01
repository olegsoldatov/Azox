Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity
Imports System.Threading.Tasks
Imports Soldata.Azox

''' <summary>
''' Минимальный интерфейс продукта.
''' </summary>
Public Interface IProduct
	Inherits IEntity
	Property Sku As String
	Property SkuRaw As String
	Property ImageId As Guid?
End Interface

' Чтобы добавить данные продукции, можно добавить дополнительные свойства в класс Product.
''' <summary>
''' Представляет модель данных продукции.
''' </summary>
Public Class Product
	Inherits EntityFramework.Entity
	Implements IProduct

	<Display(Name:="Название"), Required(ErrorMessage:="Укажите название.")>
	Public Property Title As String

	<Display(Name:="Описание")>
	Public Property Description As String

	<Display(Name:="Содержание"), DataType(DataType.MultilineText), UIHint("Content"), AllowHtml>
	Public Property Content As String

	<Display(Name:="Изображение")>
	Public Property ImageId As Guid? Implements IProduct.ImageId

	<Display(Name:="Артикул")>
	Public Property Sku As String Implements IProduct.Sku

	<ScaffoldColumn(False)>
	Public Property SkuRaw As String Implements IProduct.SkuRaw

	<Display(Name:="Черновик"), UIHint("Draft")>
	Public Property Draft As Boolean
End Class

''' <summary>
''' Предоставляет управление продуктами в источнике данных.
''' </summary>
Public Class ProductManager
	Inherits ProductManager(Of Product)

	Public Sub New()
		MyBase.New(New EntityFramework.EntityStore(Of Product)(New ApplicationDbContext))
	End Sub
End Class

''' <summary>
''' В производном классе предоставляет управление продуктами в источнике данных.
''' </summary>
Public Class ProductManager(Of TProduct As {Class, IProduct})
	Inherits Soldata.Azox.EntityManager(Of TProduct)

	Public Sub New(store As IEntityStore(Of TProduct))
		MyBase.New(store)
	End Sub

	Public Overrides Async Function CreateAsync(product As TProduct) As Task(Of ManagerResult)
		If product Is Nothing Then
			Throw New ArgumentNullException(NameOf(product))
		End If

		' Приведем отображаемый артикул в системное представление.
		product.SkuRaw = SkuToRaw(product.Sku)

		Return Await MyBase.CreateAsync(product)
	End Function

	Public Overrides Async Function UpdateAsync(product As TProduct) As Task(Of ManagerResult)
		If product Is Nothing Then
			Throw New ArgumentNullException(NameOf(product))
		End If

		' Приведем отображаемый артикул в системное представление.
		product.SkuRaw = SkuToRaw(product.Sku)

		Return Await MyBase.UpdateAsync(product)
	End Function

	Public Overloads Overrides Async Function DeleteAsync(entity As TProduct) As Task(Of ManagerResult)
		Return Await MyBase.DeleteAsync(entity)
	End Function

	Public Overloads Overrides Async Function DeleteAsync(entities As IEnumerable(Of TProduct)) As Task(Of ManagerResult)
		Return Await MyBase.DeleteAsync(entities)
	End Function

	Public Overloads Overrides Sub Delete(entity As TProduct)
		MyBase.Delete(entity)
	End Sub

	Public Overloads Overrides Sub Delete(entities As IEnumerable(Of TProduct))
		MyBase.Delete(entities)
	End Sub

#Region "Вспомогательные методы"
	Public Shared Function SkuToRaw(text As String) As String
		Dim result = text.Replace("А", "A").Replace("а", "a") _
				.Replace("В", "B").Replace("в", "b") _
				.Replace("Е", "E").Replace("е", "e") _
				.Replace("И", "U").Replace("и", "u") _
				.Replace("К", "K").Replace("к", "k") _
				.Replace("М", "M").Replace("м", "m") _
				.Replace("Н", "H").Replace("н", "h") _
				.Replace("О", "O").Replace("о", "o") _
				.Replace("Р", "P").Replace("р", "p") _
				.Replace("С", "C").Replace("с", "c") _
				.Replace("Т", "T").Replace("т", "t") _
				.Replace("У", "Y").Replace("у", "y") _
				.Replace("Х", "X").Replace("х", "x")

		For i = 1 To Len(result)
			If Not Char.IsLetterOrDigit(Mid(result, i, 1)) Then
				text = Replace(text, Mid(result, i, 1), String.Empty)
			End If
		Next
		Return text.ToUpper
	End Function
#End Region
End Class

Partial Public Class ApplicationDbContext
	Public Property Products As DbSet(Of Product)
End Class

''' <summary>
''' Модель представления фильтра продукции.
''' </summary>
Public Class ProductFilterViewModel
	<Display(Name:="Поиск")>
	Public Property SearchString As String
End Class
