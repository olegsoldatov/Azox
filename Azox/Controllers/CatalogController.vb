Imports System.Data.Entity
Imports System.Threading.Tasks
Imports System.Net
Imports System.Web.Configuration
Imports Soldata.Imaging
Imports Soldata.Web.Extensions

Namespace Controllers
    Public Class CatalogController
        Inherits Controller

        Private ReadOnly manager As New CatalogManager

        Public Async Function Index() As Task(Of ActionResult)
            Return View(Await manager.GetDocumentAsync)
        End Function

        Public Async Function Brands(id As String) As Task(Of ActionResult)
            If String.IsNullOrEmpty(id) Then
                Return View(Await manager.GetDocumentAsync)
            End If
            Dim brand = Await manager.GetBrandAsync(id)
            If IsNothing(brand) Then
                Return HttpNotFound()
            End If
            Return View("Brand", brand)
        End Function

        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing Then
                manager.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace