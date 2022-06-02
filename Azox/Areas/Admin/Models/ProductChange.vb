Imports System.ComponentModel.DataAnnotations

Public Class ProductChange
    <Display(Name:="Бренд")>
    Public Property BrandId As Guid?

    <Display(Name:="Категория")>
    Public Property CategoryId As Guid?
End Class
