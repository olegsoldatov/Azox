Imports System.Data.Entity
Imports System.Threading.Tasks
Imports Soldata.Azox

Public Class PageManager
    Inherits EntityManager(Of Page)

    Public Sub New(store As IEntityStore(Of Page))
        MyBase.New(store)
    End Sub

    Public ReadOnly Property Pages As IQueryable(Of Page)
        Get
            Return Store.Entities
        End Get
    End Property

    Public Async Function GetListAsync(Optional offset As Integer = 0, Optional limit As Integer = 10) As Task(Of (Count As Long, Items As IReadOnlyList(Of Page)))
        Dim entities = Store.Entities

        Return (Await entities.CountAsync(), Await entities.OrderByDescending(Function(x) x.LastUpdateDate).Skip(offset).Take(limit).ToListAsync())
    End Function

    ''' <summary>
    ''' Находит страницу по ярлыку.
    ''' </summary>
    ''' <param name="slug">Ярлык.</param>
    Public Async Function FindBySlugAsync(slug As String) As Task(Of IPage)
        Return New Page With {
            .Slug = slug,
            .Title = "Заголовок страницы",
            .Description = "Описание страницы",
            .Content = "<p>Lorem ipsum dolor...</p>"
        }
    End Function

    Public Async Function GetPageAsync(Of T As {Page, New})() As Task(Of T)
        Dim page = Await Store.Entities.OfType(Of T).FirstOrDefaultAsync
        If IsNothing(page) Then
            page = New T With {.Heading = My.Settings.Item($"{GetType(T).Name}Heading")}
            Await CreateAsync(page)
        End If
        Return page
    End Function
End Class
