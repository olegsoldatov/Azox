Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class Parameter
	<Key>
	Public Property Id As Guid

	<Required(ErrorMessage:="Укажите значение.")>
	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Значение")>
	Public Property Value As String

	<Display(Name:="Товар")>
	Public Overridable Property Product As Product

	<Display(Name:="Товар")>
	Public Overridable Property ProductId As Guid

	<Display(Name:="Параметр")>
	Public Overridable Property Attribute As Attribute

	<Required(ErrorMessage:="Укажите параметр.")>
	<Display(Name:="Параметр")>
	Public Overridable Property AttributeId As Guid
End Class

Partial Public Class ApplicationDbContext
	Public Property Parameters As DbSet(Of Parameter)
End Class
