Imports System.Threading.Tasks

Namespace Controllers
    Public Class TermsController
        Inherits Controller

        Private ReadOnly PageManager As PageManager

        Public Sub New(pageManager As PageManager)
            Me.PageManager = pageManager
        End Sub

        <HttpGet>
        Public Async Function Index() As Task(Of ActionResult)
            Return View(Await PageManager.GetPageAsync(Of TermsPage))
        End Function
    End Class
End Namespace