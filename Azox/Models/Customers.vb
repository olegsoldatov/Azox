Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class Customer
	Inherits Entity

	<Required(ErrorMessage:="Укажите имя.")>
	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Имя")>
	Public Property Name As String

	<Required(ErrorMessage:="Укажите телефон.")>
	<MaxLength(11)>
	<RegularExpression("^7\d{10}$", ErrorMessage:="Неверный формат телефона.")>
	<Display(Name:="Телефон")>
	Public Property Phone As String

	<StringLength(128, ErrorMessage:="Не более {1} символов.")>
	<EmailAddress(ErrorMessage:="Неверный формат электронной почты.")>
	<Display(Name:="Электронная почта")>
	Public Property Email As String

	<DataType(DataType.MultilineText)>
	<Display(Name:="О клиенте")>
	Public Property Comment As String

	<ScaffoldColumn(False)>
	Public Property LastUpdateDate As Date
End Class

Partial Public Class ApplicationDbContext
	Public Property Customers As DbSet(Of Customer)
End Class
