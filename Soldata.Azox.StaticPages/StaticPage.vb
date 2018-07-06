Imports System.ComponentModel.DataAnnotations
Imports System.Web.Mvc

''' <summary>
''' Модель данных статичной страницы.
''' </summary>
Public Class StaticPage
    <Key>
    Public Property Id As Guid

    <Required>
    <HiddenInput(DisplayValue:=False)>
    Public Property ActionName As String

    <Required>
    <HiddenInput(DisplayValue:=False)>
    Public Property ControllerName As String

    <Required(ErrorMessage:="Укажите название.")>
    <Display(Name:="Название")>
    Public Property Title As String

    <Display(Name:="Заголовок")>
    Public Property Heading As String

    <AllowHtml>
    <DataType(DataType.MultilineText)>
    <UIHint("WYSIWYG")>
    <Display(Name:="Содержание")>
    Public Property Content As String

    <DataType(DataType.MultilineText)>
    <Display(Name:="Описание")>
    Public Property Description As String

    <Display(Name:="Ключевые слова")>
    Public Property Keywords As String
End Class
