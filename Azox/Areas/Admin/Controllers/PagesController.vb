Imports System.Threading.Tasks

Namespace Areas.Admin.Controllers
	Public Class PagesController
		Inherits AdminController

        Public ReadOnly Property PageManager As PageManager

		Public Sub New(pageManager As PageManager)
			Me.PageManager = pageManager
		End Sub

        <HttpGet>
		Public Async Function About() As Task(Of ActionResult)
			Dim aboutPage = Await PageManager.GetAboutPageAsync
			ViewBag.Heading = If(IsNothing(aboutPage), My.Settings.AboutPageHeading, aboutPage.Heading)
			Return View(aboutPage)
		End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function About(aboutPage As AboutPage) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Await PageManager.UpdateAsync(aboutPage)
                Alert("Страница изменена.")
                Return RedirectToAction("about")
            End If
            Return View(aboutPage)
        End Function
    End Class
End Namespace