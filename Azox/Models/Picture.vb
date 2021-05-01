Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity

Public Class Picture
	<Key>
	Public Property Id As Guid

	<Required(ErrorMessage:="Укажите ссылу на изображение.")>
	<DataType(DataType.ImageUrl)>
	<Display(Name:="Ссылка на изображение")>
	Public Property ImageUrl As String

	<Required(ErrorMessage:="Укажите название."), Display(Name:="Название")>
	Public Property Name As String

	<Display(Name:="Изображение")>
	Public Property ImageId As Guid?

	<NotMapped, DataType(DataType.Upload), Display(Name:="Файл изображения")>
	Public Property ImageFile As HttpPostedFileWrapper

	<Required(ErrorMessage:="Укажите порядок."), Display(Name:="Порядок"), UIHint("Order")>
	Public Property Order As Integer

	<Display(Name:="Продукт")>
	Public Overridable Property Product As Product
	<Display(Name:="Продукт")>
	Public Overridable Property ProductId As Guid
End Class

Partial Public Class ApplicationDbContext
	Public Property Pictures As DbSet(Of Picture)
End Class
