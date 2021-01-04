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

Public Class Attribute
	<Key>
	Public Property Id As Guid = Guid.NewGuid

	<Required(ErrorMessage:="Укажите имя.")>
	<StringLength(128)>
	<Display(Name:="Имя")>
	Public Property Name As String

	<Display(Name:="Ед. изм.")>
	Public Property Unit As String

	<Required(ErrorMessage:="Укажите порядок.")>
	<Display(Name:="Порядок")>
	<UIHint("Order")>
	Public Property Order As Integer

	<Display(Name:="Значения")>
	Public Overridable Property Parameters As ICollection(Of Parameter)
End Class

Public Class Parameter
	<Key>
	Public Property Id As Guid = Guid.NewGuid

	<Display(Name:="Значение")>
	Public Property Value As String

	<Display(Name:="Товар")>
	Public Overridable Property Product As Product
	<Display(Name:="Товар")>
	Public Overridable Property ProductId As Guid

	<Display(Name:="Параметр")>
	Public Overridable Property Attribute As Attribute
	<Display(Name:="Параметр")>
	Public Overridable Property AttributeId As Guid
End Class

Partial Public Class ApplicationDbContext
	Public Property Pictures As DbSet(Of Picture)
	Public Property Attributes As DbSet(Of Attribute)
	Public Property Parameters As DbSet(Of Parameter)
End Class
