Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

''' <summary>
''' Модель данных веб-страницы.
''' </summary>
Public Class Page
    Inherits ApplicationEntity

    <Required(ErrorMessage:="Укажите заголовок.")>
    <MaxLength(250, ErrorMessage:="Не более {1} символов.")>
    <Display(Name:="Заголовок")>
    Public Property Heading As String

    <AllowHtml>
    <DataType(DataType.MultilineText)>
    <Display(Name:="Содержание")>
    <UIHint("Content")>
    Public Property Content As String

    <Display(Name:="SEO")>
    Public Property Seo As PageSeo
End Class

''' <summary>
''' Комплексная модель поисковой оптимизации веб-страницы.
''' </summary>
<ComplexType>
Public Class PageSeo
    <MaxLength(250, ErrorMessage:="Не более {1} символов.")>
    <Display(Name:="Название")>
    Public Property Title As String

    <DataType(DataType.MultilineText)>
    <MaxLength(250, ErrorMessage:="Не более {1} символов.")>
    <Display(Name:="Описание")>
    Public Property Description As String

    <MaxLength(250, ErrorMessage:="Не более {1} символов.")>
    <Display(Name:="Ключевые слова")>
    Public Property Keywords As String
End Class
