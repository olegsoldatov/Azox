Imports System.Threading.Tasks

Namespace Controllers
    Public Class DeliveryController
        Inherits Controller

        Private ReadOnly PageManager As PageManager

        Public Sub New(pageManager As PageManager)
            Me.PageManager = pageManager
        End Sub

        <HttpGet>
        Public Async Function Index() As Task(Of ActionResult)
            Return View(Await PageManager.GetPageAsync(Of DeliveryPage))
        End Function
    End Class
End Namespace