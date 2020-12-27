Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class Margin
	<Key>
	Public Property Id As Guid

	<Required(ErrorMessage:="Укажите минимальную цену.")>
	<DataType(DataType.Currency)>
	<Display(Name:="Мин. цена")>
	Public Property MinPrice As Decimal

	<Required(ErrorMessage:="Укажите максимальную цену.")>
	<DataType(DataType.Currency)>
	<Display(Name:="Макс. цена")>
	Public Property MaxPrice As Decimal

	<Required(ErrorMessage:="Укажите значение.")>
	<Display(Name:="Значение, %")>
	Public Property Value As Double

	<Display(Name:="Группа")>
	Public Overridable Property Group As MarginGroup

	<Required(ErrorMessage:="Укажите группу.")>
	<Display(Name:="Группа")>
	Public Overridable Property GroupId As Guid

	<Display(Name:="По умолчанию")>
	Public Property [Default] As Boolean

	''' <summary>
	''' Делает наценку.
	''' </summary>
	''' <param name="margins">Перечисление наценок по конкретному складу.</param>
	''' <param name="price">Цена.</param>
	''' <returns>Цена с наценкой.</returns>
	Public Shared Function MarginPrice(margins As IEnumerable(Of Margin), price As Decimal) As Decimal
		If IsNothing(margins) Then
			Throw New ArgumentNullException(NameOf(margins))
		End If

		If IsNothing(price) Then
			Throw New ArgumentNullException(NameOf(price))
		End If

		Dim margin = margins.OrderBy(Function(x) x.Default).ThenBy(Function(x) x.MinPrice).Where(Function(x) (price >= x.MinPrice And price <= x.MaxPrice) Or x.Default).Select(Function(x) x.Value).FirstOrDefault

		Return Math.Round(price + CDec(price * (margin / 100)), 0)
	End Function
End Class

Public Class MarginGroup
	<Key>
	Public Property Id As Guid

	<Required(ErrorMessage:="Укажите название.")>
	<MaxLength(128, ErrorMessage:="Не более {1} символов.")>
	<Display(Name:="Название")>
	Public Property Title As String

	<Display(Name:="Наценки")>
	Public Overridable Property Margins As ICollection(Of Margin)

	<Display(Name:="Склады")>
	Public Overridable Property Warehouses As ICollection(Of Warehouse)
End Class

Partial Public Class ApplicationDbContext
	Public Property Margins As DbSet(Of Margin)
	Public Property MarginGroups As DbSet(Of MarginGroup)
End Class

'Public Class MarginService
'	Public ReadOnly Property Margins As IEnumerable(Of Margin)

'	Public Sub New()
'		Using db As New ApplicationDbContext
'			_Margins = db.Margins.AsNoTracking.Include(Function(x) x.Categories).ToListAsync.Result
'		End Using
'	End Sub

'	Public Function MarginPrice(price As Decimal?, categoryId As Guid?, Optional decimals As Integer = 2) As Decimal?
'		If IsNothing(price) Then
'			Return Nothing
'		End If

'		Dim percent = Margins _
'			.Where(Function(x) (x.Categories.Any(Function(c) c.Id = categoryId.GetValueOrDefault) And x.MinPrice <= price And (x.MaxPrice >= price Or x.MaxPrice = 0)) Or (Not x.Categories.Any And x.MinPrice <= price And (x.MaxPrice >= price Or x.MaxPrice = 0)) Or x.Default) _
'			.OrderBy(Function(x) x.Default) _
'			.ThenByDescending(Function(x) x.Categories.Any) _
'			.ThenBy(Function(x) x.MinPrice) _
'			.Select(Function(x) x.Value) _
'			.FirstOrDefault

'		Return Math.Round(price.Value + CDec(price * (percent / 100)), decimals)
'	End Function
'End Class
