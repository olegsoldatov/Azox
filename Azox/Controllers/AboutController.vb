Imports System.Threading.Tasks

Namespace Controllers
    Public Class AboutController
        Inherits Controller

        Private ReadOnly PageManager As PageManager

        Public Sub New(pageManager As PageManager)
            Me.PageManager = pageManager
        End Sub

        Public Async Function Index() As Task(Of ActionResult)
            Dim page = Await PageManager.FindBySlugAsync("about")
            If IsNothing(page) Then
                Return HttpNotFound()
            End If
            Return View(page)
        End Function
    End Class
End Namespace