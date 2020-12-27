Imports System.ComponentModel.DataAnnotations

Public Class ProductAdminItem
	Public Property Id As Guid

	<Display(Name:="Артикул")>
	<UIHint("Sku")>
	Public Property Sku As String

	<Display(Name:="Название")>
	Public Property Title As String

	<Display(Name:="Бренд")>
	<UIHint("Brand")>
	Public Property Brand As String
	Public Property BrandId As Guid?

	<Display(Name:="Категория")>
	<UIHint("Category")>
	Public Property Category As String
	Public Property CategoryId As Guid?

	<Display(Name:="Цены и остатки")>
	Public Property Offers As Integer

	<Display(Name:="Черновик")>
	<UIHint("Draft")>
	Public Property Draft As Boolean
End Class
