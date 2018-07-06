''' <summary>
''' Предоставляет определение модели данных сущности.
''' </summary>
Public Interface IEntity
    ''' <summary>
    ''' Устанавливает или возвращает уникальный идентификатор.
    ''' </summary>
    ''' <returns>Структура <see cref="Guid" />, содержащая уникальный идентификатор.</returns>
    Property Id As Guid

    ''' <summary>
    ''' Устанавливает или возвращает название.
    ''' </summary>
    ''' <returns>Строка, содержащая название.</returns>
    Property Title As String
End Interface
