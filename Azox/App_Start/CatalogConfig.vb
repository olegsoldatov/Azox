Public Class MarginService
	Protected margins As IEnumerable(Of Margin)

	Public Sub New(db As ApplicationDbContext)
		margins = db.Margins.AsNoTracking.ToList
	End Sub

	Public Function MarginPrice(price As Decimal?, options As MarginOptions) As Decimal?
		If IsNothing(price) Then
			Return Nothing
		End If

		Dim percent = 0

		'If IsNothing(warehouseId) Then
		'	Throw New ArgumentNullException(NameOf(warehouseId))
		'End If
		'Dim warehouse = Warehouses.Include(Function(x) x.MarginGroup.Margins).AsNoTracking.SingleOrDefault(Function(x) x.Id = warehouseId)
		'If IsNothing(Warehouse) Then
		'	Return price
		'End If
		'Dim percent = warehouse.Margin

		'Dim percent = margins _
		'	.Where(Function(x) (x.Categories.Any(Function(c) c.Id = options.CategoryId.GetValueOrDefault) And x.MinPrice <= price And (x.MaxPrice >= price Or x.MaxPrice = 0)) Or (Not x.Categories.Any And x.MinPrice <= price And (x.MaxPrice >= price Or x.MaxPrice = 0)) Or x.Default) _
		'	.OrderBy(Function(x) x.Default) _
		'	.ThenByDescending(Function(x) x.Categories.Any) _
		'	.ThenBy(Function(x) x.MinPrice) _
		'	.Select(Function(x) x.Value) _
		'	.FirstOrDefault

		Return Math.Round(price.Value + CDec(price * (percent / 100)), options.Decimals)
	End Function
End Class

Public Class MarginOptions
	Public Property WarehouseId As Guid?
	Public Property CategoryId As Guid?
	Public Property Decimals As Integer = 2
End Class
