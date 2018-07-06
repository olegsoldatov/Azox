''' <summary>
''' Предоставляет в модели данных сущности поддержку черновой записи.
''' </summary>
Public Interface IDraftable
    ''' <summary>
    ''' Устанавливает или возвращает указатель черновика.
    ''' </summary>
    Property Draft As Boolean
End Interface

#Region "Устаревшие возможности"

''' <summary>
''' Предоставляет свойство <c>Draft</c> для реализации в модели данных поддержку статуса черновика.
''' </summary>
''' <remarks></remarks>
<Obsolete("Необходимо использовать интерфейс Soldata.Azox.Models.IDraftable.")>
Public Interface IDraftableEntity
    Inherits IEntity

    Property Draft As Boolean
End Interface

#End Region
