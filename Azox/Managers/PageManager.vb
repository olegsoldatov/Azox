Imports System.Data.Entity
Imports System.Threading.Tasks
Imports Soldata.Azox

Public Class PageManager
    Inherits EntityManager(Of Page)

    Public Sub New(store As IEntityStore(Of Page))
        MyBase.New(store)
    End Sub

    Public Async Function GetListAsync(limit As Integer, offset As Integer) As Task(Of (Items As IEnumerable(Of Page), TotalCount As Integer, PageCount As Integer))
        Dim entities = Store.Entities

        Dim items = Await entities.OrderBy(Function(x) x.Title).Skip(limit * offset).Take(limit).AsNoTracking.ToListAsync
        Dim totalCount = Await entities.AsNoTracking.CountAsync
        Dim pageCount = CInt(Math.Ceiling(totalCount / limit))
        Return (items, totalCount, pageCount)
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
