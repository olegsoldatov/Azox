Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class Warehouse
    Inherits Entity

    <Required(ErrorMessage:="Укажите название.")>
    <MaxLength(128, ErrorMessage:="Не более {1} символов.")>
    <Display(Name:="Название")>
    Public Property Title As String

    <Display(Name:="Порядок")>
    Public Property Order As Integer?

    <Display(Name:="Опубликовано")>
    <UIHint("IsPublished")>
    Public Property IsPublished As Boolean

    <MaxLength(128, ErrorMessage:="Не более {1} символов.")>
    <Display(Name:="Имя", Description:="Необходимо для службы обновления цен.")>
    Public Property Name As String

    <MaxLength(128, ErrorMessage:="Не более {1} символов.")>
    <Display(Name:="Компания", Description:="Необходимо для службы обновления цен.")>
    Public Property Company As String

    <MaxLength(128, ErrorMessage:="Не более {1} символов.")>
    <Display(Name:="Город")>
    Public Property City As String

    <Required(ErrorMessage:="Укажите наценку.")>
    <Display(Name:="Наценка")>
    <UIHint("Percent")>
    Public Property Margin As Double

    <Display(Name:="Группа наценок")>
    Public Overridable Property MarginGroup As MarginGroup

    <Display(Name:="Группа наценок")>
    Public Overridable Property MarginGroupId As Guid?

    <Display(Name:="Остатки")>
    Public Overridable Property Offers As ICollection(Of Offer)
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
