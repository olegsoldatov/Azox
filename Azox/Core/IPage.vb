''' <summary>
''' Страница.
''' </summary>
Public Interface IPage
    ''' <summary>
    ''' Имя страницы.
    ''' </summary>
    Property Name As String

    ''' <summary>
    ''' Заголовок страницы.
    ''' </summary>
    Property Title As String

    ''' <summary>
    ''' Описание страницы.
    ''' </summary>
    Property Description As String

    ''' <summary>
    ''' Статьи.
    ''' </summary>
    Property Articles As ICollection(Of IArticle)
End Interface
