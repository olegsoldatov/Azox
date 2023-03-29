Imports System.ComponentModel.DataAnnotations

''' <summary>
''' Модель данных веб-страницы.
''' </summary>
Public Class Page
    Inherits ApplicationEntity
    Implements IPage

    <Required(ErrorMessage:="Укажите ярлык.")>
    <MaxLength(128, ErrorMessage:="Не более {1} символов.")>
    <RegularExpression("^[a-z0-9-~]+", ErrorMessage:="Ярлык должен содержать только латинские буквы, цифры, без пробелов и знаков препинания. Допускается дефис и тильда.")>
    <Display(Name:="Ярлык")>
    Public Property Slug As String Implements IPage.Slug

    <Display(Name:="Заголовок")>
    Public Property Title As String Implements IPage.Title

    <Display(Name:="Описание")>
    Public Property Description As String Implements IPage.Description

    <AllowHtml>
    <DataType(DataType.MultilineText)>
    <Display(Name:="Содержание")>
    <UIHint("Content")>
    Public Property Content As String Implements IPage.Content

    <Required(ErrorMessage:="Укажите заголовок.")>
    <MaxLength(250, ErrorMessage:="Не более {1} символов.")>
    <Display(Name:="Заголовок")>
    Public Property Heading As String
End Class
