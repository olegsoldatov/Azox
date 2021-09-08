Namespace Controllers
    Public Class DeliveriesController
        Inherits Controller

        <HttpGet>
        Public Function Create() As ActionResult
            Return View()
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Function Create(model As DeliveryViewModel) As ActionResult

            'TODO: Реализовать расчет стоимости доставки.

            Return View(model)
        End Function
    End Class
End Namespace