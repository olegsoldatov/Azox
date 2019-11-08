Imports System.ComponentModel.DataAnnotations

Public Class ContactFormViewModel
	<Required(ErrorMessage:="Укажите имя.")>
	<StringLength(250, ErrorMessage:="Не более {1} знаков.")>
	<Display(Name:="Имя")>
	Public Property Name As String

	'<Required(ErrorMessage:="Укажите электронную почту.")>
	<StringLength(250, ErrorMessage:="Не более {1} знаков.")>
	<DataType(DataType.EmailAddress)>
	<Display(Name:="Электронная почта")>
	Public Property Email As String

	<Required(ErrorMessage:="Это обязательное поле.")>
	<StringLength(18, ErrorMessage:="Не более {1} знаков.")>
	<DataType(DataType.PhoneNumber)>
	<Display(Name:="Телефон")>
	Public Property Phone As String

	<Required(ErrorMessage:="Укажите тему.")>
	<StringLength(250, ErrorMessage:="Не более {1} знаков.")>
	<Display(Name:="Тема")>
	Public Property Subject As String

	<Required(ErrorMessage:="Напишите сообщение.")>
	<StringLength(1000, ErrorMessage:="Не более {1} знаков.")>
	<DataType(DataType.MultilineText)>
	<Display(Name:="Сообщение")>
	Public Property Message As String

	Public Property Agree As Boolean
End Class
