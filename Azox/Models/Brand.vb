Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

''' <summary>
''' Модель данных бренда.
''' </summary>
Public Class Brand
    Inherits PictorialEntity

    <Required(ErrorMessage:="Укажите название.")>
    <MaxLength(128, ErrorMessage:="Не более {1} символов.")>
    <Remote("Exists", "Brands", "Admin", AdditionalFields:="Id", ErrorMessageResourceType:=GetType(My.Resources.Resources), ErrorMessageResourceName:="BrandExists")>
    <Display(Name:="Название")>
    Public Property Title As String

    <DataType(DataType.MultilineText)>
    <AllowHtml>
    <Display(Name:="Содержание")>
    <UIHint("Content")>
    Public Property Content As String

    <Display(Name:="Порядок")>
    Public Property Order As Integer?

    <UIHint("IsPublished")>
    <Display(Name:="Опубликовано")>
    Public Property IsPublished As Boolean

    <Display(Name:="Продукция")>
    Public Overridable Property Products As ICollection(Of Product)

    <Display(Name:="Описание")>
    Public Property Description As String

    <Display(Name:="Ключевые слова")>
    Public Property Keywords As String
End Class

Partial Public Class ApplicationDbContext
    Public Property Brands As DbSet(Of Brand)
End Class
