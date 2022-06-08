Imports System.Data.Entity
Imports System.Threading.Tasks

Public Class PageManager
    Inherits EntityManager(Of Page)

    Public Sub New(context As ApplicationDbContext)
        MyBase.New(context)
    End Sub

    Public ReadOnly Property Pages As IQueryable(Of Page)
        Get
            Return Context.Set(Of Page)
        End Get
    End Property

    Public Async Function GetAboutPageAsync() As Task(Of AboutPage)
        Return Await Context.Set(Of AboutPage).SingleOrDefaultAsync
    End Function
End Class
