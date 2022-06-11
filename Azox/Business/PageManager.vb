Imports System.Data.Entity
Imports System.Threading.Tasks
Imports Soldata.Azox

Public Class PageManager
    Inherits Soldata.Azox.EntityManager(Of Page)

    Public Sub New(store As IEntityStore(Of Page))
        MyBase.New(store)
    End Sub

    Public Async Function GetPageAsync(Of T As {Page, New})() As Task(Of T)
        Dim page = Await Store.Entities.OfType(Of T).SingleOrDefaultAsync
        If IsNothing(page) Then
            page = New T With {.Heading = My.Settings.Item($"{GetType(T).Name}Heading")}
            Await CreateAsync(page)
        End If
        Return page
    End Function
End Class
