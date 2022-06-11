Imports System.Threading.Tasks

Namespace Areas.Admin.Controllers
    Public Class PagesController
        Inherits AdminController

        Public ReadOnly Property PageManager As PageManager

        Public Sub New(pageManager As PageManager)
            Me.PageManager = pageManager
        End Sub

        Public Function Index() As ActionResult
            Return View()
        End Function

        <HttpGet>
        Public Async Function About() As Task(Of ActionResult)
            Return View("Edit", Await PageManager.GetPageAsync(Of AboutPage))
        End Function

        <HttpGet>
        Public Async Function Contacts() As Task(Of ActionResult)
            Return View("Edit", Await PageManager.GetPageAsync(Of ContactsPage))
        End Function

        <HttpGet>
        Public Async Function Delivery() As Task(Of ActionResult)
            Return View("Edit", Await PageManager.GetPageAsync(Of DeliveryPage))
        End Function

        <HttpGet>
        Public Async Function Terms() As Task(Of ActionResult)
            Return View("Edit", Await PageManager.GetPageAsync(Of TermsPage))
        End Function

        <HttpGet>
        Public Async Function Policy() As Task(Of ActionResult)
            Return View("Edit", Await PageManager.GetPageAsync(Of PolicyPage))
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Edit(page As Page, returnUrl As String) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Await PageManager.UpdateAsync(page)
                Alert("Страница изменена.")
                Return RedirectToReturnUrl(returnUrl)
            End If
            Return View(page)
        End Function
    End Class
End Namespace