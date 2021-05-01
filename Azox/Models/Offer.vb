Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class Offer
	<Key>
	Public Property Id As Guid

	<Required(ErrorMessage:="Укажите цену.")>
	<DataType(DataType.Currency)>
	<Display(Name:="Цена")>
	Public Property Price As Decimal

	<DataType(DataType.Currency)>
	<Display(Name:="Старая цена")>
	Public Property OldPrice As Decimal?

	<Display(Name:="Кол-во")>
	Public Property Quantity As Integer?

	<Display(Name:="Наличие")>
	Public Property Availability As OfferAvailability

	<Display(Name:="Дата изменения")>
	Public Property LastUpdateDate As Date

	<Required(ErrorMessage:="Укажите товар.")>
	<Display(Name:="Товар")>
	Public Overridable Property ProductId As Guid
	<Display(Name:="Товар")>
	Public Overridable Property Product As Product

	<Required(ErrorMessage:="Укажите склад.")>
	<Display(Name:="Склад")>
	Public Overridable Property WarehouseId As Guid
	<Display(Name:="Склад")>
	Public Overridable Property Warehouse As Warehouse
End Class

Public Enum OfferAvailability
	<Display(Name:="В наличии")>
	InStock
	<Display(Name:="На заказ")>
	PreOrder
	<Display(Name:="Нет в наличии")>
	OutOfStock
End Enum

Partial Public Class ApplicationDbContext
	Public Property Offers As DbSet(Of Offer)
End Class
