Imports Soldata.Azox
Imports Soldata.Azox.Deliveries

Public Class ApplicationDeliveryService
	Inherits DeliveryService

	Public Overrides Function Create(parameters As DeliveryParameters) As IDelivery
		Return New Delivery With {.Description = "Служба доставки", .Cost = 500}
	End Function
End Class
