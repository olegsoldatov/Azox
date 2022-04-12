Imports System.Data.Entity
Imports System.Threading.Tasks

Public Class PageManager
    Inherits EntityManager(Of Page)

    Public Sub New(context As DbContext)
        MyBase.New(context)
    End Sub

    Public ReadOnly Property Pages As IQueryable(Of Page)
        Get
            Return Context.Set(Of Page)
        End Get
    End Property

    Public Async Function GetListAsync() As Task(Of IList(Of Page))
        Dim entities = Context.Set(Of Page).AsQueryable

        ' Сортировка.
        entities = entities.OrderByDescending(Function(x) x.LastUpdateDate)

        Return Await entities.ToListAsync
    End Function

    ''' <summary>
    ''' Находит страницу по абсолютному пути.
    ''' </summary>
    ''' <param name="absolutePath">Абсолютный путь.</param>
    Public Async Function FindByAbsolutePathAsync(absolutePath As String) As Task(Of Page)
        Return Await Context.Set(Of Page).SingleOrDefaultAsync(Function(x) x.AbsolutePath = absolutePath)
    End Function
End Class
