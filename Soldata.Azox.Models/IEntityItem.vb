''' <summary>
''' Предоставляет определение сущности, как элемента коллекции.
''' </summary>
Public Interface IEntityItem
    ''' <summary>
    ''' Устанавливает или возвращает дату публикации.
    ''' </summary>
    Property PublishDate As Date

    ''' <summary>
    ''' Устанавливает или возвращает порядок.
    ''' </summary>
    Property Order As Integer

    ''' <summary>
    ''' Устанавливает или возвращает отметку о публикации.
    ''' </summary>
    Property IsPublished As Boolean
End Interface
