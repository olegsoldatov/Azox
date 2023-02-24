Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks
Imports Soldata.Web.Extensions

Namespace Areas.Admin.Controllers
    Public Class PagesController
        Inherits AdminController

        Private ReadOnly PageManager As PageManager

        Public Sub New(pageManager As PageManager)
            Me.PageManager = pageManager
        End Sub

        Public Async Function Index(Optional pageIndex As Integer = 0, Optional pageSize As Integer = 50) As Task(Of ActionResult)
            Dim entities = Await PageManager.GetListAsync(offset:=pageIndex, limit:=pageSize)
            Pagination(entities.Count, pageIndex, pageSize)
            Return View(entities.Items)
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Index(id As Guid(), returnUrl As String, Optional delete As Boolean = False) As Task(Of ActionResult)
            If Not IsNothing(id) Then
                Dim pages = Await PageManager.Pages.Where(Function(x) id.Contains(x.Id)).ToListAsync()

                If delete Then
                    Await PageManager.DeleteRangeAsync(pages)
                    Alert(String.Format("Удалено: {0}.", pages.Count.ToString("страница", "страницы", "страниц")))
                End If
            End If
            Return Redirect(returnUrl)
        End Function

        <HttpGet>
        Public Function Create() As ActionResult
            Return View()
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Create(page As Page) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Await PageManager.CreateAsync(page)
                Alert("Страница добавлена.")
                Return RedirectToAction("index")
            End If
            Return View(page)
        End Function

        Public Async Function Edit(id As Guid?) As Task(Of ActionResult)
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim page = Await PageManager.FindByIdAsync(id)
            If IsNothing(page) Then
                Return HttpNotFound()
            End If
            Return View(page)
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

        Public Async Function Delete(id As Guid?) As Task(Of ActionResult)
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim page = Await PageManager.FindByIdAsync(id)
            If IsNothing(page) Then
                Return HttpNotFound()
            End If
            Return View(page)
        End Function

        <HttpPost>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken>
        Public Async Function DeleteConfirmed(id As Guid, returnUrl As String) As Task(Of ActionResult)
            Await PageManager.DeleteAsync(Await PageManager.FindByIdAsync(id))
            Alert("Страница удалена.")
            Return RedirectToReturnUrl(returnUrl)
        End Function

        <HttpGet>
        Public Async Function Home() As Task(Of ActionResult)
            Return View("Edit", Await PageManager.GetPageAsync(Of HomePage))
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
        Public Async Function ReturnOfGoods() As Task(Of ActionResult)
            Return View("Edit", Await PageManager.GetPageAsync(Of ReturnOfGoodsPage))
        End Function

        <HttpGet>
        Public Async Function Terms() As Task(Of ActionResult)
            Return View("Edit", Await PageManager.GetPageAsync(Of TermsPage))
        End Function

        <HttpGet>
        Public Async Function Policy() As Task(Of ActionResult)
            Return View("Edit", Await PageManager.GetPageAsync(Of PolicyPage))
        End Function
    End Class
End Namespace