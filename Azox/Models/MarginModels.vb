Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity

Public Class Margin
	<Key>
	Public Property Id As Guid = Guid.NewGuid

	<Required(ErrorMessage:="Укажите минимальную цену.")>
	<DataType(DataType.Currency)>
	<Display(Name:="Мин. цена")>
	Public Property MinPrice As Decimal = Decimal.Zero

	<Required(ErrorMessage:="Укажите максимальную цену.")>
	<DataType(DataType.Currency)>
	<Display(Name:="Макс. цена")>
	Public Property MaxPrice As Decimal = Decimal.Zero

	<Required(ErrorMessage:="Укажите значение.")>
	<Display(Name:="Значение, %")>
	Public Property Value As Double = 0

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

Partial Public Class ApplicationDbContext
	Public Property Margins As DbSet(Of Margin)
End Class
