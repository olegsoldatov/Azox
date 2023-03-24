Imports System.Threading.Tasks

Public Class PageService
    Implements IPageService

    Public Function GetPageByIdAsync(id As Guid) As Task(Of IPage) Implements IPageService.GetPageByIdAsync
        Throw New NotImplementedException()
    End Function

    Public Async Function GetPageByNameAsync(name As String) As Task(Of IPage) Implements IPageService.GetPageByNameAsync
        Return New Page With {
            .Name = "page",
            .Title = "Заголовок страницы",
            .Description = "Описание страницы",
            .Articles = New List(Of IArticle) From {New Article With {.Heading = "Заголовок статьи", .Content = "..."}}
        }
    End Function
End Class
