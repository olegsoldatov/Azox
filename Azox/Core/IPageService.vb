Imports System.Threading.Tasks

''' <summary>
''' Сервис страниц.
''' </summary>
Public Interface IPageService
    ''' <summary>
    ''' Получает страницу по идентификатору.
    ''' </summary>
    ''' <param name="id">Идентификатор страницы.</param>
    Function GetPageByIdAsync(id As Guid) As Task(Of IPage)

    ''' <summary>
    ''' Получает страницу по имени.
    ''' </summary>
    ''' <param name="name">Имя страницы.</param>
    Function GetPageByNameAsync(name As String) As Task(Of IPage)
End Interface
