Imports Soldata.Azox

Namespace Controllers
	Public Class DeliveriesController
		Inherits Controller

		Private ReadOnly _deliveryService As IDeliveryService

		Public Sub New()
			_deliveryService = New ApplicationDeliveryService
		End Sub

		<HttpGet>
		Public Function Create() As ActionResult
			Return View()
		End Function

		<HttpPost>
		<ValidateAntiForgeryToken>
		Public Function Create(model As DeliveryViewModel) As ActionResult

			Dim parameters As New DeliveryParameters With {
				.PostalCode = model.PostalCode,
				.Region = model.Region,
				.City = model.City
			}

			Dim delivery = _deliveryService.Create(parameters)

			Return View("Result", delivery)
		End Function
	End Class
End Namespace