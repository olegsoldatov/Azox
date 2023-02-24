Imports System.Threading.Tasks

Namespace Controllers
    Public Class HomeController
        Inherits Controller

        Private ReadOnly PageManager As PageManager

        Public Sub New(pageManager As PageManager)
            Me.PageManager = pageManager
        End Sub

        <HttpGet>
        <OutputCache(CacheProfile:="Pages")>
        Public Async Function Index() As Task(Of ActionResult)
            Return View(Await PageManager.GetPageAsync(Of HomePage))
        End Function
    End Class
End Namespace
