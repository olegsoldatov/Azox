Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

' Чтобы добавить данные продукции, можно добавить дополнительные свойства в класс Product.
''' <summary>
''' Представляет модель данных продукции.
''' </summary>
Public Class Product
	Inherits Soldata.Azox.EntityFramework.Entity
	Implements IProduct

	<Display(Name:="Название"), Required(ErrorMessage:="Укажите название.")>
	Public Property Title As String

	<Display(Name:="Описание")>
	Public Property Description As String

	<Display(Name:="Изображение")>
	Public Property ImageId As Guid?

	<Display(Name:="Содержание"), DataType(DataType.MultilineText), UIHint("Content"), AllowHtml>
	Public Property Content As String

	<Display(Name:="Артикул")>
	Public Property Sku As String Implements IProduct.Sku

	<ScaffoldColumn(False)>
	Public Property SkuRaw As String Implements IProduct.SkuRaw

	<Display(Name:="Черновик"), UIHint("Draft")>
	Public Property Draft As Boolean
End Class

Partial Public Class ApplicationDbContext
	Public Property Products As DbSet(Of Product)
End Class

''' <summary>
''' Предоставляет модель представления фильтра продукции.
''' </summary>
Public Class ProductFilterViewModel
	<Display(Name:="Поиск")>
	Public Property SearchString As String
End Class
