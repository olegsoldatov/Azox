Imports System.ComponentModel.DataAnnotations

Public Class ContactFormViewModel
	<Required(ErrorMessage:="Укажите имя.")>
	<StringLength(250, ErrorMessage:="Не более {1} знаков.")>
	<Display(Name:="Имя")>
	Property Name As String

	<Required(ErrorMessage:="Укажите электронную почту.")>
	<StringLength(250, ErrorMessage:="Не более {1} знаков.")>
	<DataType(DataType.EmailAddress)>
	<Display(Name:="Электронная почта")>
	Property Email As String

	<Required(ErrorMessage:="Укажите тему.")>
	<StringLength(250, ErrorMessage:="Не более {1} знаков.")>
	<Display(Name:="Тема")>
	Property Subject As String

	<Required(ErrorMessage:="Напишите сообщение.")>
	<StringLength(1000, ErrorMessage:="Не более {1} знаков.")>
	<DataType(DataType.MultilineText)>
	<Display(Name:="Сообщение")>
	Property Message As String
End Class
