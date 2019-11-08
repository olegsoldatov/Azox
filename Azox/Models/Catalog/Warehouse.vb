Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class Warehouse
	<Key>
	Public Property Id As Guid = Guid.NewGuid

	<Required(ErrorMessage:="Укажите название."), StringLength(255, ErrorMessage:="Длина строки не более {1} символов."), Display(Name:="Название")>
	Public Property Name As String

	<Required(ErrorMessage:="Укажите компанию."), StringLength(255, ErrorMessage:="Длина строки не более {1} символов."), Display(Name:="Компания")>
	Public Property Company As String

	<Required(ErrorMessage:="Укажите ярлык."), StringLength(255, ErrorMessage:="Длина строки не более {1} символов."), Display(Name:="Ярлык")>
	Public Property Slug As String

	<StringLength(255, ErrorMessage:="Длина строки не более {1} символов."), Display(Name:="Город")>
	Public Property City As String

	<StringLength(255, ErrorMessage:="Длина строки не более {1} символов."), Display(Name:="Срок доставки")>
	Public Property DeliveryDays As String

	<Required(ErrorMessage:="Укажите порядок."), Display(Name:="Порядок"), UIHint("Order")>
	Public Property Order As Integer

	<Display(Name:="Опубликовано"), UIHint("IsPublished")>
	Public Property IsPublished As Boolean

	<Display(Name:="Продукты")>
	Public Overridable Property Products As ICollection(Of Product)
End Class

Public Class WarehouseFilterViewModel
	<Display(Name:="Поиск")>
	Public Property SearchText As String

	<Display(Name:="Компания")>
	Public Property Company As String
End Class

Partial Public Class ApplicationDbContext
	Public Property Warehouses As DbSet(Of Warehouse)
End Class
