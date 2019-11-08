Imports System.ComponentModel.DataAnnotations

Public Class ExternalLoginConfirmationViewModel
	<Required>
	<Display(Name:="Адрес электронной почты")>
	Public Property Email As String
End Class

Public Class ExternalLoginListViewModel
	Public Property ReturnUrl As String
End Class

Public Class SendCodeViewModel
	Public Property SelectedProvider As String
	Public Property Providers As ICollection(Of System.Web.Mvc.SelectListItem)
	Public Property ReturnUrl As String
	Public Property RememberMe As Boolean
End Class

Public Class VerifyCodeViewModel
	<Required>
	Public Property Provider As String

	<Required>
	<Display(Name:="Код")>
	Public Property Code As String

	Public Property ReturnUrl As String

	<Display(Name:="Запомнить браузер?")>
	Public Property RememberBrowser As Boolean

	Public Property RememberMe As Boolean
End Class

Public Class ForgotViewModel
	<Required>
	<Display(Name:="Адрес электронной почты")>
	Public Property Email As String
End Class

Public Class LoginViewModel
	<Required>
	<Display(Name:="Адрес электронной почты")>
	<EmailAddress>
	Public Property Email As String

	<Required>
	<DataType(DataType.Password)>
	<Display(Name:="Пароль")>
	Public Property Password As String

	<Display(Name:="Запомнить меня")>
	Public Property RememberMe As Boolean
End Class

Public Class RegisterViewModel
	<Required>
	<EmailAddress>
	<Display(Name:="Адрес электронной почты")>
	Public Property Email As String

	<Required>
	<StringLength(100, ErrorMessage:="Значение {0} должно содержать не менее {2} символов.", MinimumLength:=6)>
	<DataType(DataType.Password)>
	<Display(Name:="Пароль")>
	Public Property Password As String

	<DataType(DataType.Password)>
	<Display(Name:="Подтверждение пароля")>
	<Compare("Password", ErrorMessage:="Пароль и его подтверждение не совпадают.")>
	Public Property ConfirmPassword As String
End Class

Public Class ResetPasswordViewModel
	<Required>
	<EmailAddress>
	<Display(Name:="Адрес электронной почты")>
	Public Property Email() As String

	<Required>
	<StringLength(100, ErrorMessage:="Значение {0} должно содержать не менее {2} символов.", MinimumLength:=6)>
	<DataType(DataType.Password)>
	<Display(Name:="Пароль")>
	Public Property Password() As String

	<DataType(DataType.Password)>
	<Display(Name:="Подтверждение пароля")>
	<Compare("Password", ErrorMessage:="Пароль и его подтверждение не совпадают.")>
	Public Property ConfirmPassword() As String

	Public Property Code() As String
End Class

Public Class ForgotPasswordViewModel
	<Required>
	<EmailAddress>
	<Display(Name:="Почта")>
	Public Property Email() As String
End Class
