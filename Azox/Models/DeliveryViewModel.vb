Imports System.ComponentModel.DataAnnotations

Public Class DeliveryViewModel
	<Display(Name:="Страна")>
	Public Property Country As String

	<Display(Name:="Почтовый индекс")>
	Public Property PostalCode As String

	<Display(Name:="Область, край")>
	Public Property Region As String

	<Required(ErrorMessage:="Укажите город, населенный пункт.")>
	<Display(Name:="Город, населенный пункт")>
	Public Property City As String
End Class
