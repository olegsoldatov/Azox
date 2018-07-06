''' <summary>
''' Минимальный интерфейс базовой сущности с ключевым полем типа <see cref="Guid"/>.
''' </summary>
Public Interface IEntity
	Inherits IEntity(Of Guid)
End Interface

''' <summary>
''' Минимальный интерфейс базовой сущности.
''' </summary>
Public Interface IEntity(Of Out TKey)
	ReadOnly Property Id As TKey
	Property Title As String
End Interface
