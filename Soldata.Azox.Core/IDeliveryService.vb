''' <summary>
''' Служба доставки.
''' </summary>
Public Interface IDeliveryService
	''' <summary>
	''' Создает доставку.
	''' </summary>
	''' <param name="parameters">Параметры доставки.</param>
	Function Create(parameters As DeliveryParameters) As IDelivery
End Interface
