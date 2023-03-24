Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

''' <summary>
''' Модель данных веб-страницы.
''' </summary>
Public Class Page
    Inherits ApplicationEntity
    Implements IPage

    <Display(Name:="Имя страницы")>
    Public Property Name As String Implements IPage.Name

    <Display(Name:="Заголовок")>
    Public Property Title As String Implements IPage.Title

    <Display(Name:="Описание")>
    Public Property Description As String Implements IPage.Description

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

    <Display(Name:="Статьи")>
    Public Property Articles As ICollection(Of IArticle) = New List(Of IArticle) Implements IPage.Articles
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
