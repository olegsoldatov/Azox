Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class Attribute
	<Key>
	Public Property Id As Guid

	<Required(ErrorMessage:="Укажите имя.")>
	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Имя")>
	Public Property Name As String

	<Display(Name:="Ед. изм.")>
	Public Property Unit As String

	<Display(Name:="Порядок")>
	Public Property Order As Integer?

	<Display(Name:="Используется в фильтре")>
	Public Property Filterable As Boolean

	<Display(Name:="Значения")>
	Public Overridable Property Parameters As ICollection(Of Parameter)
End Class

Partial Public Class ApplicationDbContext
	Public Property Attributes As DbSet(Of Attribute)
End Class
