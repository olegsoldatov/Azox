Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

''' <summary>
''' Модель данных бренда.
''' </summary>
Public Class Brand
    Inherits PictorialEntity
    Implements IBrand

    ''' <inheritdoc/>
    <Required(ErrorMessage:="Укажите название.")>
    <MaxLength(128, ErrorMessage:="Не более {1} символов.")>
    <Remote("Exists", "Brands", "Admin", AdditionalFields:="Id", ErrorMessageResourceType:=GetType(My.Resources.Resources), ErrorMessageResourceName:="BrandExists")>
    <Display(Name:="Название")>
    Public Property Title As String Implements IBrand.Title

    <Display(Name:="Опубликовано")>
    <UIHint("IsPublished")>
    Public Property IsPublished As Boolean

    <Display(Name:="Продукция")>
    Public Overridable Property Products As ICollection(Of Product)
End Class

Partial Public Class ApplicationDbContext
    Public Property Brands As DbSet(Of Brand)
End Class
