Imports System.ComponentModel.DataAnnotations
Imports Microsoft.AspNet.Identity
Imports Microsoft.Owin.Security

Public Class IndexViewModel
    Public Property HasPassword As Boolean
    Public Property Logins As IList(Of UserLoginInfo)
    Public Property PhoneNumber As String
    Public Property TwoFactor As Boolean
    Public Property BrowserRemembered As Boolean
End Class

Public Class ManageLoginsViewModel
    Public Property CurrentLogins As IList(Of UserLoginInfo)
    Public Property OtherLogins As IList(Of AuthenticationDescription)
End Class

Public Class FactorViewModel
    Public Property Purpose As String
End Class

Public Class SetPasswordViewModel
    <Required>
    <StringLength(100, ErrorMessage:="Значение {0} должно содержать символов не менее: {2}.", MinimumLength:=6)>
    <DataType(DataType.Password)>
    <Display(Name:="Новый пароль")>
    Public Property NewPassword As String

    <DataType(DataType.Password)>
    <Display(Name:="Подтверждение нового пароля")>
    <Compare("NewPassword", ErrorMessage:="Новый пароль и его подтверждение не совпадают.")>
    Public Property ConfirmPassword As String
End Class

Public Class ChangePasswordViewModel
    <Required>
    <DataType(DataType.Password)>
    <Display(Name:="Текущий пароль")>
    Public Property OldPassword As String

    <Required>
    <StringLength(100, ErrorMessage:="Значение {0} должно содержать символов не менее: {2}.", MinimumLength:=6)>
    <DataType(DataType.Password)>
    <Display(Name:="Новый пароль")>
    Public Property NewPassword As String

    <DataType(DataType.Password)>
    <Display(Name:="Подтверждение нового пароля")>
    <Compare("NewPassword", ErrorMessage:="Новый пароль и его подтверждение не совпадают.")>
    Public Property ConfirmPassword As String
End Class

Public Class AddPhoneNumberViewModel
    <Required>
    <Phone>
    <Display(Name:="Номер телефона")>
    Public Property Number As String
End Class

Public Class VerifyPhoneNumberViewModel
    <Required>
    <Display(Name:="Код")>
    Public Property Code As String

    <Required>
    <Phone>
    <Display(Name:="Номер телефона")>
    Public Property PhoneNumber As String
End Class

Public Class ConfigureTwoFactorViewModel
    Public Property SelectedProvider As String
	Public Property Providers As ICollection(Of SelectListItem)
End Class