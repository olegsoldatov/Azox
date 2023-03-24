Imports System.Threading.Tasks

Namespace Controllers
    Public Class AboutController
        Inherits Controller

        Private ReadOnly PageService As IPageService

        Public Sub New(pageService As IPageService)
            Me.PageService = pageService
        End Sub

        Public Async Function Index() As Task(Of ActionResult)
            Dim page = Await PageService.GetPageByNameAsync("about")
            If IsNothing(page) Then
                Return HttpNotFound()
            End If
            Return View(page)
        End Function

        <OutputCache(CacheProfile:="Pages")>
        Public Function V1() As ActionResult
            Return View()
        End Function

        <OutputCache(CacheProfile:="Pages")>
        Public Function V2() As ActionResult
            Return View()
        End Function
    End Class
End Namespace