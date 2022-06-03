''' <summary>
''' Служба доставки.
''' </summary>
Public MustInherit Class DeliveryService
	Implements IDeliveryService

	''' <summary>
	''' Создает службу доставки.
	''' </summary>
	''' <param name="parameters">Параметры доставки.</param>
	Public MustOverride Function Create(parameters As DeliveryParameters) As IDelivery Implements IDeliveryService.Create
End Class
