Imports Soldata.Azox

''' <summary>
''' Предоставляет минимальный интерфейс модели бренда.
''' </summary>
Public Interface IBrand
    Inherits IEntity

    ''' <summary>
    ''' Устанавливает или возвращает название.
    ''' </summary>
    Property Title As String

    ''' <summary>
    ''' Устанавливает или возвращает содержание.
    ''' </summary>
    Property Content As String

    ''' <summary>
    ''' Устанавливает или возвращает порядок.
    ''' </summary>
    Property Order As Integer?

    ''' <summary>
    ''' Устанавливает или возвращает отметку о публикации.
    ''' </summary>
    Property IsPublished As Boolean
End Interface
