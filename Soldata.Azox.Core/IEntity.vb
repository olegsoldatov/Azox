''' <summary>
''' Минимальный интерфейс сущности.
''' </summary>
Public Interface IEntity
	Inherits IEntity(Of Guid)
End Interface

''' <summary>
''' Минимальный интерфейс сущности.
''' </summary>
Public Interface IEntity(Of Out TKey)
	ReadOnly Property Id As TKey
End Interface
