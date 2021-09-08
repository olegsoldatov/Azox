''' <summary>
''' Доставка.
''' </summary>
Public Class Delivery
	Implements IDelivery

	''' <summary>
	''' Описание.
	''' </summary>
	Public Property Description As String Implements IDelivery.Description

	''' <summary>
	''' Стоимость.
	''' </summary>
	Public Property Cost As Decimal Implements IDelivery.Cost
End Class
