Imports System.Threading.Tasks

Namespace Controllers
    Public Class PolicyController
        Inherits Controller

        Private ReadOnly PageManager As PageManager

        Public Sub New(pageManager As PageManager)
            Me.PageManager = pageManager
        End Sub

        <HttpGet>
        Public Async Function Index() As Task(Of ActionResult)
            Return View("Page", Await PageManager.GetPageAsync(Of PolicyPage))
        End Function
    End Class
End Namespace