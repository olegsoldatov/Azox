Imports System.Net
Imports System.Threading.Tasks

Namespace Controllers
    Public Class ArticlesController
        Inherits Controller

        Private ReadOnly Db As New ApplicationDbContext

        <OutputCache(Duration:=1200, VaryByParam:="id")>
        Public Async Function Details(id As Guid?) As Task(Of ActionResult)
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entity = Await Db.Articles.FindAsync(id)
            If IsNothing(entity) Then
                Return HttpNotFound()
            End If
            Return View("Article", entity)
        End Function

        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing Then
                Db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace