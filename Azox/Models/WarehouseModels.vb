Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class Warehouse
	<Key>
	Public Property Id As Guid

	<Required(ErrorMessage:="Укажите идентификатор.")>
	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Идентификатор")>
	Public Property Name As String

	<Required(ErrorMessage:="Укажите название.")>
	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Название")>
	Public Property Title As String

	<Required(ErrorMessage:="Укажите компанию.")>
	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Компания")>
	Public Property Company As String

	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Город")>
	Public Property City As String

	<Required(ErrorMessage:="Укажите наценку.")>
	<Display(Name:="Наценка")>
	<UIHint("Percent")>
	Public Property Margin As Double

	<Required(ErrorMessage:="Укажите порядок.")>
	<Display(Name:="Порядок")>
	<UIHint("Order")>
	Public Property Order As Integer

	<Display(Name:="Опубликовано")>
	<UIHint("IsPublished")>
	Public Property IsPublished As Boolean

	<Display(Name:="Группа наценок")>
	Public Overridable Property MarginGroup As MarginGroup

	<Display(Name:="Группа наценок")>
	Public Overridable Property MarginGroupId As Guid?

	<Display(Name:="Продукты")>
	Public Overridable Property Products As ICollection(Of Product)
End Class

Public Class WarehouseFilterViewModel
	Inherits FilterViewModel
	<Display(Name:="Компания")>
	Public Property Company As String
	<Display(Name:="Группы наценок")>
	Public Property MarginGroupId As Guid?
End Class

Partial Public Class ApplicationDbContext
	Public Property Warehouses As DbSet(Of Warehouse)
End Class
