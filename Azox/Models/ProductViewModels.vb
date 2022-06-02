Imports System.ComponentModel.DataAnnotations

Public Class ProductAdminItem
    Public Property Id As Guid

    <Display(Name:="Артикул")>
    Public Property Sku As String

    <Display(Name:="Название")>
    Public Property Title As String

    <Display(Name:="Бренд")>
    Public Property Brand As String
    Public Property BrandId As Guid?

    <Display(Name:="Категория")>
    Public Property Category As String
    Public Property CategoryId As Guid?

    <Display(Name:="Цены и остатки")>
    Public Property Offers As Integer

    <Display(Name:="Черновик")>
    Public Property Draft As Boolean
End Class
