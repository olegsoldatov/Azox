Imports System.Threading.Tasks

Namespace Controllers
	Public Class AboutController
		Inherits Controller

        Private ReadOnly PageManager As PageManager

        Public Sub New(pageManager As PageManager)
            Me.PageManager = pageManager
        End Sub

        <HttpGet>
        Public Async Function Index() As Task(Of ActionResult)
            Dim aboutPage = Await PageManager.GetAboutPageAsync
            If IsNothing(aboutPage) Then
                aboutPage = New AboutPage With {.Heading = My.Settings.AboutPageHeading}
            End If
            Return View(aboutPage)
        End Function
    End Class
End Namespace