''' <summary>
''' Предоставляет определение страницы.
''' </summary>
Public Interface IEntityPage
    ''' <summary>
    ''' Описание.
    ''' </summary>
    Property Description As String
    ''' <summary>
    ''' Ключевые слова.
    ''' </summary>
    Property Keywords As String
    ''' <summary>
    ''' Имя действия (ярлык).
    ''' </summary>
    Property ActionName As String
End Interface
