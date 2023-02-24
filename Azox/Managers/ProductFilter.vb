Imports System.ComponentModel.DataAnnotations

Public Class ProductFilter
    <Display(Name:="Поиск")>
    Public Property SearchText As String

    <Display(Name:="Бренд")>
    Public Property BrandId As Guid?

    <Display(Name:="Категория")>
    Public Property CategoryId As Guid?

    <Display(Name:="Производитель")>
    Public Property Vendor As String

    <Display(Name:="Склад")>
    Public Property WarehouseId As Guid?

    <Display(Name:="Сортировать по")>
    Public Property SortBy As ProductSortBy
End Class

Public Enum ProductSortBy
    <Display(Name:="Цене (по возрастанию)")>
    Price
    <Display(Name:="Цене (по убыванию)")>
    PriceDescending
End Enum
