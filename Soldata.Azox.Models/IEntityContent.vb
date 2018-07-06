''' <summary>
''' Предоставляет определение содержания сущности.
''' </summary>
Public Interface IEntityContent
    Inherits IEntity

    ''' <summary>
    ''' Заголовок.
    ''' </summary>
    Property Heading As String
    ''' <summary>
    ''' Содержание.
    ''' </summary>
    Property Content As String
End Interface
