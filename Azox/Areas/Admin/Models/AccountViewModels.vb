Imports System.ComponentModel.DataAnnotations

Public Class ForgotViewModel
    <Required>
    <Display(Name:="Электронная почта")>
    Public Property Email As String
End Class

Public Class LoginViewModel
    <Required>
    <Display(Name:="Электронная почта")>
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
    <Display(Name:="Электронная почта")>
    Public Property Email As String

    <Required>
    <StringLength(100, ErrorMessage:="Не менее {2} символов.", MinimumLength:=6)>
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
    <Display(Name:="Электронная почта")>
    Public Property Email() As String

    <Required>
    <StringLength(100, ErrorMessage:="Не менее {2} символов.", MinimumLength:=6)>
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
