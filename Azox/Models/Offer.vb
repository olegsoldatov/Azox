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
	Public Property Availability As ProductAvailability?

	<Display(Name:="Дата изменения")>
	Public Property LastUpdateDate As Date

	<Required(ErrorMessage:="Укажите товар.")>
	<Display(Name:="Товар")>
	Public Overridable Property ProductId As Guid
	<Display(Name:="Товар")>
	Public Overridable Property Product As Product

	<Required(ErrorMessage:="Укажите магазин / склад.")>
	<Display(Name:="Магазин / Склад")>
	Public Overridable Property WarehouseId As Guid
	<Display(Name:="Магазин / Склад")>
	Public Overridable Property Warehouse As Warehouse
End Class

Partial Public Class ApplicationDbContext
	Public Property Offers As DbSet(Of Offer)
End Class
