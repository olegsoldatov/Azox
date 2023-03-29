Imports Soldata.Azox

''' <summary>
''' Предоставляет определение веб-страницы.
''' </summary>
Public Interface IPage
    Inherits IEntity

    ''' <summary>
    ''' Ярлык страницы.
    ''' </summary>
    ''' <remarks>
    ''' При реализации в производном классе должно быть обязательным и иметь уникальное значение.
    ''' </remarks>
    Property Slug As String

    ''' <summary>
    ''' Заголовок страницы.
    ''' </summary>
    Property Title As String

    ''' <summary>
    ''' Описание страницы.
    ''' </summary>
    Property Description As String

    ''' <summary>
    ''' Содержание страницы.
    ''' </summary>
    ''' <remarks>При реализации в производном классе должно разрешать использование HTML-тэгов.</remarks>
    Property Content As String
End Interface
