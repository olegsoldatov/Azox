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

Public Class ProductFilterViewModel
	<Display(Name:="Поиск")>
	Public Property SearchText As String

	<Display(Name:="Бренд")>
	Public Property BrandId As Guid?

	<Display(Name:="Производитель")>
	Public Property Vendor As String

	<Display(Name:="Категория")>
	Public Property CategoryId As Guid?

	<Display(Name:="Склад")>
	Public Property WarehouseId As Guid?

	<Display(Name:="Наличие")>
	Public Property Availability As ProductAvailability?

	<Display(Name:="Сортировать по")>
	Public Property SortBy As ProductSortBy
End Class

Public Enum ProductAvailability
	<Display(Name:="Нет в наличии")>
	OutOfStock
	<Display(Name:="В наличии")>
	InStock
	<Display(Name:="На заказ")>
	PreOrder
End Enum

Public Enum ProductSortBy
	<Display(Name:="Цене (по возрастанию)")>
	Price
	<Display(Name:="Цене (по убыванию)")>
	PriceDescending
End Enum
